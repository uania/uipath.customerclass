using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Mail;

namespace CustomerClass.ProjectApproval
{
    public class MatchDaoKuan : CodeActivity
    {
        /// <summary>
        /// 到款通知邮件列表
        /// </summary>
        [Category("Input")]
        [RequiredArgument]
        public InArgument<List<MailMessage>> Messages { get; set; }

        /// <summary>
        /// 匹配到的到款信息
        /// </summary>
        [Category("Output")]
        public OutArgument<List<MessageNoticeInfo>> MessageInfos { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var res = new List<MessageNoticeInfo>();
            //浦发银行匹配金额和公司名称
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"(\d{4}年\d{2}月\d{2}日),贵公司帐户\(.*\)存入人民币([\d|,|\.]+)余额([\d|,|\.]+),尾号\d+,(.*)。");
            var tmpMessages = Messages.Get(context);
            foreach (var message in tmpMessages)
            {

                var match = reg.Match(message.Body);
                var row = new MessageNoticeInfo
                {
                    MessageId = message.Headers.Get("Message-ID"),
                    MessageBody = message.Body,
                    DaoKuanDate = Convert.ToDateTime(match.Groups[1].Value),
                    Amount = Convert.ToDecimal(match.Groups[2].Value),
                    CompanyName = match.Groups[4].Value
                };
                res.Add(row);
            }
            MessageInfos.Set(context, res);
        }
    }
}
