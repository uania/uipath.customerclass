using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomerClass.ProjectApproval
{
    public class MessageNoticeInfo
    {
        /// <summary>
        /// 邮件id
        /// </summary>
        public string MessageId { get; set; }

        /// <summary>
        /// 邮件内容
        /// </summary>
        public string MessageBody { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 到款金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 到款日期
        /// </summary>
        public DateTime DaoKuanDate { get; set; }

        /// <summary>
        /// 到款详情列表
        /// </summary>
        public List<MessageDetailInfo> Details { get; set; }

        /// <summary>
        /// 是否有到款列表
        /// </summary>
        public bool HasDetails
        {
            get
            {
                return Details != null && Details.Any();
            }
        }
    }
}
