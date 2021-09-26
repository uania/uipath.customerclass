using System.Collections.Generic;

namespace CustomerClass.ProjectApproval
{
    public class Department
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartName { get; set; }

        /// <summary>
        /// 员工列表
        /// </summary>
        public List<Employee> Employees{ get; set; }
    }
}
