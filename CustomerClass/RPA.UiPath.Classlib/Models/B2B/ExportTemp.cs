using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPA.UiPath.Classlib.Models.B2B
{
    public class ExportTemp
    {
        /// <summary>
        /// 分组前的结果
        /// </summary>
        public List<MicrosoftDailyOrigin> MicrosoftDailyOrigins { get; set; }

        /// <summary>
        /// 分组后的结果
        /// </summary>
        public List<MicrosoftDailyStatistics> MicrosoftDailyStatistics { get; set; }
    }
}
