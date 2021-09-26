using System;

namespace CustomerClass.ProjectApproval
{
    public class ProjectInfo
    {

        /// <summary>
        /// 合同编号
        /// </summary>
        public string PONumber { get; set; }

        /// <summary>
        /// 合同金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 收到合同的时间
        /// </summary>
        public DateTime ReceiveTime { get; set; }

        /// <summary>
        /// 合同截止日期
        /// </summary>
        public DateTime ExpirationTime { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartName { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 收款方式
        /// </summary>
        public string PaymentType { get; set; }

        /// <summary>
        /// 付款周期
        /// </summary>
        public string PaymentCycle { get; set; }

        /// <summary>
        /// 折扣提醒
        /// </summary>
        public string DiscountsReminder { get; set; }

        /// <summary>
        /// 合同名称
        /// </summary>
        public string ContractName { get; set; }

        /// <summary>
        /// 合同文档路径
        /// </summary>
        public string ContractDocsPath { get; set; }
    }
}
