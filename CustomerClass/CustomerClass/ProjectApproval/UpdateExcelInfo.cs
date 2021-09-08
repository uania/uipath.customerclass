namespace CustomerClass.ProjectApproval
{
    public class UpdateExcelInfo
    {
        /// <summary>
        /// po号
        /// </summary>
        public string PO { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        public string InvoiceNo { get; set; }

        /// <summary>
        /// 到款金额
        /// </summary>
        public decimal ExcelAmount { get; set; }

        /// <summary>
        /// 折扣金额
        /// </summary>
        public decimal ExcelDiscount { get; set; }

        /// <summary>
        /// 到款日期
        /// </summary>
        public string ExcelDaoKuanDate { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string ExcelCompany { get; set; }

        /// <summary>
        /// 记录所在行
        /// </summary>
        public int RowIndex { get; set; }

        /// <summary>
        /// 记录所在sheet
        /// </summary>
        public string SheetName { get; set; }

        /// <summary>
        /// 到款银行
        /// </summary>
        public string BankName { get; set; }
    }
}
