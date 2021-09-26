using System.Collections.Generic;

namespace CustomerClass.ProjectApproval
{
    public class Company
    {
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 部门列表
        /// </summary>
        public List<Department> Departments { get; set; }
    }
}
