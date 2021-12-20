using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPA.UiPath.Classlib.Models.B2B
{
    public class MicrosoftDailyOrigin
    {
        /// <summary>
        /// 报名时间
        /// </summary>
        public string RegistrationTime { get; set; }

        /// <summary>
        /// 渠道名称
        /// </summary>
        public string ChannelName { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 公司职位
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 是否报名
        /// </summary>
        public bool IsRegistered { get; set; }
    }
}
