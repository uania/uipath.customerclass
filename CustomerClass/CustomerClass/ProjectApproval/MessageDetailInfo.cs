using System;
using System.Collections.Generic;

namespace CustomerClass.ProjectApproval
{
    public class MessageDetailInfo
    {
        /// <summary>
        /// 详情邮件id
        /// </summary>
        public string MessageId { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string MessageBody { get; set; }

        /// <summary>
        /// 到款日期
        /// </summary>
        public DateTime DaoKuanDate { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        public string InvoiceNo { get; set; }

        /// <summary>
        /// 到款金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 折扣金额
        /// </summary>
        public decimal DiscountAmount { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// PO号
        /// </summary>
        public string PO { get; set; }

        /// <summary>
        /// 银行名称
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// 从发票明细表中得到的明细
        /// </summary>
        public List<UpdateExcelInfo> UpdateExcelInfos { get; set; }

        /// <summary>
        /// 记录所在sheet
        /// </summary>
        public string SheetName { get; set; }
    }
}
