using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.ComponentModel;
using System.Data;
using RPA.UiPath.Classlib.Models.B2B;
using RPA.UiPath.Classlib.Common.Tools;

namespace RPA.UiPath.Classlib.Activities.B2B
{
    public class Dt2ExportTemp : CodeActivity
    {
        /// <summary>
        /// 需要导出的数据
        /// </summary>
        [Category("Input")]
        [RequiredArgument]
        public InArgument<DataTable> ExtractTable { get; set; }

        /// <summary>
        /// 工作薄路径
        /// </summary>
        [Category("Input")]
        [RequiredArgument]
        public InArgument<string> MessageTitle { get; set; }

        [Category("Output")]
        public OutArgument<ExportTemp> ExportTemp { get; set; }


        protected override void Execute(CodeActivityContext context)
        {
            try
            {
                var extractTable = ExtractTable.Get(context);
                var messageTitle = MessageTitle.Get(context);
                
                var exportTemp = new ExportTemp
                {
                    MessageTitle = messageTitle,
                    MicrosoftDailyOrigins = new List<MicrosoftDailyOrigin>(),
                    MicrosoftDailyStatistics = new List<MicrosoftDailyStatistics>()
                };

                if (extractTable != null && extractTable.Rows.Count > 0)
                {
                    foreach (DataRow currRow in extractTable.Rows)
                    {
                        var tmpOrigin = new MicrosoftDailyOrigin
                        {
                            RegistrationTime = currRow["报名时间"].ToString(),
                            ChannelName = currRow["报名渠道"].ToString(),
                            CompanyName = currRow["公司"].ToString(),
                            IsRegistered = currRow["报名"].ToString() == "是",
                            Title = currRow["公司职位"].ToString()
                        };
                        exportTemp.MicrosoftDailyOrigins.Add(tmpOrigin);
                    }
                }

                ExportTemp.Set(context, exportTemp);
            }
            catch (Exception ex)
            {
                ToolsFactory.Logger.WriteLog(ex);
                ToolsFactory.Logger.WriteLog("============");
                throw new Exceptions.TerminalFlowException(ex.Message, "抓取的表格格式异常", ex);
            }
        }
    }
}
