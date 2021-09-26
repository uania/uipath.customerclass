using System;

namespace CustomerClass.LingYin.Apply
{
    public class RecogForLingYin : RebateDetail
    {
        /// <summary>
        /// 状态
        /// 1=成功
        /// 2=失败
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 状态的说明
        /// </summary>
        public string StatusText
        {
            get
            {
                if (Status == 1)
                {
                    return "成功";
                }
                else
                {
                    return "失败";
                }
            }
        }

        /// <summary>
        /// 执行日期
        /// </summary>
        public DateTime ExecDate { get; set; }

        /// <summary>
        /// 执行日期格式化字符串
        /// </summary>
        public string ExecDateStr
        {
            get
            {
                return ExecDate.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        /// <summary>
        /// 异常描述
        /// </summary>
        public string Description { get; set; }
    }
}
