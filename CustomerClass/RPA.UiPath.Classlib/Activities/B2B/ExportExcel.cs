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
using NPOI.SS.Util;
using System.Linq;
using RPA.UiPath.Classlib.Common.Tools;

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

        /// <summary>
        /// origin from
        /// </summary>
        [Category("Input")]
        [RequiredArgument]
        public InArgument<string> From { get; set; }

        [Category("Output")]
        public OutArgument<List<string>> FilePaths { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            try
            {
                var exportTemps = ExportTemps.Get(context);
                var workbookFolder = WorkbookFolder.Get(context);
                var sheet1Name = Sheet1Name.Get(context);
                var sheet2Name = Sheet2Name.Get(context);
                var from = From.Get(context);

                if (!Directory.Exists(workbookFolder))
                {
                    Directory.CreateDirectory(workbookFolder);
                }

                var n = 1;
                var filePaths = new List<string>();

                foreach (var item in exportTemps)
                {
                    string fileNamePrefix;
                    if (string.IsNullOrWhiteSpace(item.MessageTitle))
                    {
                        fileNamePrefix = $"第{n}条会议";
                    }
                    else
                    {
                        //去除非法文件名字符
                        fileNamePrefix = item.MessageTitle.Replace("\\", "")
                            .Replace("/", "")
                            .Replace(":", "")
                            .Replace("*", "")
                            .Replace("?", "")
                            .Replace("\"", "")
                            .Replace("<", "")
                            .Replace(">", "")
                            .Replace("|", "")
                            .Trim();
                        if (string.IsNullOrWhiteSpace(fileNamePrefix))
                        {
                            fileNamePrefix = $"第{n}条会议";
                        }
                    }
                    ToolsFactory.Logger.WriteLog(fileNamePrefix);
                    var fileName = $"{fileNamePrefix}{n}{DateTime.Now:yyyyMMddHHmmss}.xlsx";
                    var workbookPath = $"{workbookFolder}\\{fileName}";
                    IWorkbook workbook = new XSSFWorkbook();
                    var summarySheet = workbook.CreateSheet(sheet1Name);
                    var eventhubSheet = workbook.CreateSheet(sheet2Name);

                    //头样式
                    ICellStyle cellstyle = workbook.CreateCellStyle();
                    cellstyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.SkyBlue.Index;
                    cellstyle.FillPattern = FillPattern.SolidForeground;
                    cellstyle.VerticalAlignment = VerticalAlignment.Center;
                    cellstyle.Alignment = HorizontalAlignment.Center;

                    //奇数行样式
                    ICellStyle oddCellstyle = workbook.CreateCellStyle();
                    oddCellstyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.PaleBlue.Index;
                    oddCellstyle.FillPattern = FillPattern.SolidForeground;
                    oddCellstyle.VerticalAlignment = VerticalAlignment.Center;
                    oddCellstyle.Alignment = HorizontalAlignment.Center;

                    //偶数行样式
                    var evenCellstyle = workbook.CreateCellStyle();
                    evenCellstyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Turquoise.Index;
                    evenCellstyle.FillPattern = FillPattern.SolidForeground;
                    evenCellstyle.VerticalAlignment = VerticalAlignment.Center;
                    evenCellstyle.Alignment = HorizontalAlignment.Center;

                    //合并行的样式
                    var mergeCellstyle = workbook.CreateCellStyle();
                    mergeCellstyle.VerticalAlignment = VerticalAlignment.Center;
                    mergeCellstyle.Alignment = HorizontalAlignment.Center;

                    //创建表头
                    var summaryHeaders = new string[] { "来源", "报名渠道", "报名数量" };
                    var eventhubHeaders = new string[] { "报名时间", "报名渠道", "公司", "公司职位", "是否报名" };
                    CraeteHeaderRow(summarySheet, cellstyle, summaryHeaders);
                    CraeteHeaderRow(eventhubSheet, cellstyle, eventhubHeaders);

                    //添加数据库来源
                    var dbRow = summarySheet.CreateRow(1);
                    var dbCell0 = dbRow.CreateCell(0);
                    dbCell0.CellStyle = oddCellstyle;
                    dbCell0.SetCellValue("数据库");
                    var dbCell1 = dbRow.CreateCell(1);
                    dbCell1.CellStyle = oddCellstyle;
                    dbCell1.SetCellValue("");
                    var dbCell2 = dbRow.CreateCell(2);
                    dbCell2.CellStyle = oddCellstyle;
                    dbCell2.SetCellValue("");

                    //填写内容
                    for (var i = 0; i < item.MicrosoftDailyStatistics.Count; i++)
                    {
                        //第一行表头 第二行数据库空行
                        var rowNumber = i + 2;
                        var row = summarySheet.CreateRow(rowNumber);
                        var cellStyle = rowNumber % 2 == 0 ? evenCellstyle : oddCellstyle;
                        row.CreateCell(0).CellStyle = mergeCellstyle;
                        var cell1 = row.CreateCell(1);
                        cell1.CellStyle = cellStyle;
                        cell1.SetCellValue(item.MicrosoftDailyStatistics[i].ChannelName);
                        var cell2 = row.CreateCell(2);
                        cell2.CellStyle = cellStyle;
                        cell2.SetCellValue(item.MicrosoftDailyStatistics[i].NumberOfRegistration);
                    }
                    if (item.MicrosoftDailyStatistics.Count > 0)
                    {
                        //设置表格第一列的合并数据
                        summarySheet.GetRow(2).GetCell(0).SetCellValue(from);
                    }
                    //总计
                    var totalSummary = item.MicrosoftDailyStatistics.Sum(r => r.NumberOfRegistration);
                    var totalRow = summarySheet.CreateRow(item.MicrosoftDailyStatistics.Count + 2);
                    var totalStyle = (item.MicrosoftDailyStatistics.Count + 2) % 2 == 0 ? evenCellstyle : oddCellstyle;
                    var totalCell0 = totalRow.CreateCell(0);
                    totalCell0.CellStyle = totalStyle;
                    totalCell0.SetCellValue("合计");
                    totalRow.CreateCell(1).CellStyle = totalStyle;
                    var totalCell2 = totalRow.CreateCell(2);
                    totalCell2.CellStyle = totalStyle;
                    totalCell2.SetCellValue(totalSummary);

                    //合并单元格
                    if (item.MicrosoftDailyStatistics.Count > 1)
                    {
                        var originRegion = new CellRangeAddress(2, item.MicrosoftDailyStatistics.Count + 1, 0, 0);
                        summarySheet.AddMergedRegion(originRegion);
                    }

                    for (var i = 0; i < item.MicrosoftDailyOrigins.Count; i++)
                    {
                        var rowNumber = i + 1;
                        var row = eventhubSheet.CreateRow(rowNumber);
                        var style = rowNumber % 2 == 0 ? evenCellstyle : oddCellstyle;
                        var rowCell0 = row.CreateCell(0);
                        rowCell0.CellStyle = style;
                        rowCell0.SetCellValue(item.MicrosoftDailyOrigins[i].RegistrationTime);
                        var rowCell1 = row.CreateCell(1);
                        rowCell1.CellStyle = style;
                        rowCell1.SetCellValue(item.MicrosoftDailyOrigins[i].ChannelName);
                        var rowCell2 = row.CreateCell(2);
                        rowCell2.CellStyle = style;
                        rowCell2.SetCellValue(item.MicrosoftDailyOrigins[i].CompanyName);
                        var rowCell3 = row.CreateCell(3);
                        rowCell3.CellStyle = style;
                        rowCell3.SetCellValue(item.MicrosoftDailyOrigins[i].Title);
                        var rowCell4 = row.CreateCell(4);
                        rowCell4.CellStyle = style;
                        rowCell4.SetCellValue(item.MicrosoftDailyOrigins[i].IsRegistered ? "是" : "否");
                    }

                    //保存文件
                    using (var stream = new FileStream(workbookPath, FileMode.Create, FileAccess.Write))
                    {
                        workbook.Write(stream);
                    }
                    filePaths.Add(workbookPath);
                    n++;
                }

                FilePaths.Set(context, filePaths);
            }
            catch (Exception ex)
            {
                ToolsFactory.Logger.WriteLog(ex);
                ToolsFactory.Logger.WriteLog("============");
                throw new Exceptions.TerminalFlowException(ex.Message, "意料之外的excel数据", ex);
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
