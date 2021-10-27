using System;

namespace CustomerClass.ProjectApproval
{
    public class RecogForProject : ProjectInfo
    {
        /// <summary>
        /// 执行情况描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime ExecDate { get; set; }

        /// <summary>
        /// 执行时间的格式化输出
        /// </summary>
        public string ExecDateFormat
        {
            get
            {
                return ExecDate.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        /// <summary>
        /// 执行状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 执行状态说明
        /// 1=成功
        /// 2=失败
        /// 3=已存在的po
        /// </summary>
        public string StatusText { get
            {
                var res = string.Empty;
                switch (Status)
                {
                    case 1:
                        res = "成功";
                        break;
                    case 2:
                        res = "失败";
                        break;
                    case 3:
                        res = "已存在的PO";
                        break;
                }
                return res;
            }
        }
    }
}
