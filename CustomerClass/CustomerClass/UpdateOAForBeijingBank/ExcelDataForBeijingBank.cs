using System;

namespace CustomerClass.UpdateOAForBeijingBank
{
    public class ExcelDataForBeijingBank
    {
        /// <summary>
        /// 数据唯一标识
        /// </summary>
        public string DataId { get; set; }

        /// <summary>
        /// 到款日期
        /// </summary>
        public DateTime DaoKuanDate{ get; set; }

        /// <summary>
        /// 到款金额
        /// </summary>
        public decimal DaoKuanAmount { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
    }
}
