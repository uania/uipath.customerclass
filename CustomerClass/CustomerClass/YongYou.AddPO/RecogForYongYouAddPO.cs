using System;

namespace CustomerClass.YongYou.AddPO
{
    public class RecogForYongYouAddPO : POCategory
    {
        /// <summary>
        /// 执行日期
        /// </summary>
        public DateTime ExecDate { get; set; }

        /// <summary>
        /// 执行日期的格式化日期输出
        /// </summary>
        public string ExecDateFormat
        {
            get
            {
                return ExecDate.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 状态描述
        /// </summary>
        public string StatusText
        {
            get
            {
                return Status == 1 ? "成功" : "失败";
            }
        }
    }
}
