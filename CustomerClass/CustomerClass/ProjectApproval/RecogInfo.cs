using System;

namespace CustomerClass.ProjectApproval
{
    public class RecogInfo
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
        /// 发票号
        /// </summary>
        public string InvoiceNo { get; set; }

        /// <summary>
        /// PO号
        /// </summary>
        public string PO { get; set; }

        /// <summary>
        /// 到款日期
        /// </summary>
        public DateTime DaoKuanDate { get; set; }

        /// <summary>
        /// 到款日期格式化输出
        /// </summary>
        public string DaoKuanDateStr
        {
            get
            {
                return DaoKuanDate.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        /// <summary>
        /// 银行名称
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// 到款金额
        /// </summary>
        public decimal DaoKuanAmount { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 执行日期
        /// </summary>
        public DateTime ExecDate { get; set; }

        /// <summary>
        /// 执行日期格式化输出
        /// </summary>
        public string ExecDateStr
        {
            get
            {
                return ExecDate.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        /// <summary>
        /// 状态 给程序使用
        /// 1 不再执行
        /// 2 需要再次处理
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 执行状态说明
        /// </summary>
        public string StatusText
        {
            get
            {
                return Status == 1 ? "不再执行" : "等待下次执行";
            }
        }

        /// <summary>
        /// 执行结果
        /// </summary>
        public int Result { get; set; }

        /// <summary>
        /// 执行结果 方便运维查看执行结果
        /// 1 成功
        /// 2 等待下次执行
        /// 3 失败
        /// </summary>
        public string ResultText
        {
            get
            {
                if(Result == 1)
                {
                    return "成功";
                }
                else if(Result == 2)
                {
                    return "等待下次执行";
                }
                else
                {
                    return "失败";
                }
            }
        }
    }
}
