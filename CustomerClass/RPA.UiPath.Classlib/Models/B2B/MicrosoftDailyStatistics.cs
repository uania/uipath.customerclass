using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPA.UiPath.Classlib.Models.B2B
{
    public class MicrosoftDailyStatistics
    {
        /// <summary>
        /// 来源
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// 渠道名称
        /// </summary>
        public string ChannelName { get; set; }

        /// <summary>
        /// 报名数量
        /// </summary>
        public int NumberOfRegistration { get; set; }
    }
}
