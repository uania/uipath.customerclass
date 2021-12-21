using RPA.UiPath.Classlib.Models.B2B;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using NPOI;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Text;

namespace RPA.UiPath.Classlib.Activities.B2B
{
    public class ExportExcel : CodeActivity
    {
        /// <summary>
        /// 需要导出的数据
        /// </summary>
        [Category("Input")]
        [RequiredArgument]
        public InArgument<List<ExportTemp>> ExportTemps { get; set; }

        /// <summary>
        /// 工作薄路径
        /// </summary>
        [Category("Input")]
        [RequiredArgument]
        public InArgument<string> WorkbookFolder { get; set; }

        /// <summary>
        /// Sheet name
        /// </summary>
        [Category("Input")]
        [RequiredArgument]
        public InArgument<string> Sheet1Name { get; set; }

        /// <summary>
        /// Sheet name
        /// </summary>
        [Category("Input")]
        [RequiredArgument]
        public InArgument<string> Sheet2Name { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var exportTemps = ExportTemps.Get(context);
            var workbookFolder = WorkbookFolder.Get(context);
            var sheet1Name = Sheet1Name.Get(context);
            var sheet2Name = Sheet2Name.Get(context);

            if (!Directory.Exists(workbookFolder))
            {
                Directory.CreateDirectory(workbookFolder);
            }

            foreach (var item in exportTemps)
            {
                var fileName = $"{DateTime.Now:yyyyMMddHHmmss)}.xlsx";
                var workbookPath = $"{workbookFolder}{fileName}";
                IWorkbook workbook = new XSSFWorkbook();
                var summarySheet = workbook.GetSheet(sheet1Name);
                var eventhubSheet = workbook.GetSheet(sheet2Name);

                //样式
                ICellStyle cellstyle = workbook.CreateCellStyle();

                cellstyle.BorderBottom = BorderStyle.Thin;

                cellstyle.BorderLeft = BorderStyle.Thin;

                cellstyle.BorderRight = BorderStyle.Thin;

                cellstyle.BorderTop = BorderStyle.Thin;

                cellstyle.VerticalAlignment = VerticalAlignment.Center;

                cellstyle.Alignment = HorizontalAlignment.Center;

                //创建表头
                var summaryHeaders = new string[] { "来源", "报名渠道", "报名数量" };
                var eventhubHeaders = new string[] { "报名时间", "报名渠道", "公司", "公司职位", "是否报名" };
                CraeteHeaderRow(summarySheet, cellstyle, summaryHeaders);
                CraeteHeaderRow(eventhubSheet, cellstyle, eventhubHeaders);

                //填写内容
                for (var i = 0; i < item.MicrosoftDailyStatistics.Count; i++)
                {
                    var rowNumber = i + 1;
                    var row = summarySheet.CreateRow(rowNumber);
                    row.CreateCell(0).SetCellValue(item.MicrosoftDailyStatistics[i].ChannelName);
                    row.CreateCell(1).SetCellValue(item.MicrosoftDailyStatistics[i].ChannelName);
                    row.CreateCell(2).SetCellValue(item.MicrosoftDailyStatistics[i].NumberOfRegistration);
                }

                for (var i = 0; i < item.MicrosoftDailyOrigins.Count; i++)
                {
                    var rowNumber = i + 1;
                    var row = summarySheet.CreateRow(rowNumber);
                    row.CreateCell(0).SetCellValue(item.MicrosoftDailyOrigins[i].RegistrationTime);
                    row.CreateCell(1).SetCellValue(item.MicrosoftDailyOrigins[i].ChannelName);
                    row.CreateCell(2).SetCellValue(item.MicrosoftDailyOrigins[i].CompanyName);
                    row.CreateCell(3).SetCellValue(item.MicrosoftDailyOrigins[i].Title);
                    row.CreateCell(4).SetCellValue(item.MicrosoftDailyOrigins[i].IsRegistered ? "是" : "否");
                }

                //保存文件
                using (var stream = new FileStream(workbookPath, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(stream);
                }
            }
        }

        /// <summary>
        /// 创建表头
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="style"></param>
        /// <param name="headers"></param>
        private void CraeteHeaderRow(ISheet sheet, ICellStyle style, string[] headers)
        {
            var row = sheet.CreateRow(0);

            for (var i = 0; i < headers.Length; i++)
            {
                var cell = row.CreateCell(i);
                cell.CellStyle = style;
                cell.SetCellValue(headers[i]);
                int width = Encoding.UTF8.GetBytes(headers[i]).Length;
                sheet.SetColumnWidth(i, (width + 1) * 256);
            }
        }
    }
}
