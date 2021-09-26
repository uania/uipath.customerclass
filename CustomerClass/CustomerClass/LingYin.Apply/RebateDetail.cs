using System;

namespace CustomerClass.LingYin.Apply
{
    public class RebateDetail
    {
        /// <summary>
        /// 序号 唯一标识
        /// </summary>
        public string SerialNo { get; set; }

        /// <summary>
        /// 签单年份
        /// </summary>
        public int BillDate { get; set; }

        /// <summary>
        /// 开票金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 开票PO号
        /// </summary>
        public string PONumber { get; set; }
        
        /// <summary>
        /// AM
        /// </summary>
        public string AM { get; set; }

        /// <summary>
        /// 开票日期
        /// </summary>
        public DateTime MakeDate{ get; set; }

        /// <summary>
        /// 开票号码
        /// </summary>
        public string MakeNumber { get; set; }
    }
}
