﻿using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using RPA.UiPath.Classlib.Models.B2B;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.B2B
{
    class Export
    {
        public void Excute(List<ExportTemp> exportTemps)
        {
            var workbookFolder = "d:\\SCRM";
            var sheet1Name = "【Summary】";
            var sheet2Name = "【ssss】";
            var from = "sajgofijaerogije";

            if (!Directory.Exists(workbookFolder))
            {
                Directory.CreateDirectory(workbookFolder);
            }

            var n = 1;
            foreach (var item in exportTemps)
            {
                var fileName = $"{n}{DateTime.Now:yyyyMMddHHmmss}.xlsx";
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
                cellstyle.BorderBottom = BorderStyle.Thin;
                cellstyle.BorderLeft = BorderStyle.Thin;
                cellstyle.BorderRight = BorderStyle.Thin;
                cellstyle.BorderTop = BorderStyle.Thin;

                //奇数行样式
                ICellStyle oddCellstyle = workbook.CreateCellStyle();
                oddCellstyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.PaleBlue.Index;
                oddCellstyle.FillPattern = FillPattern.SolidForeground;
                oddCellstyle.VerticalAlignment = VerticalAlignment.Center;
                oddCellstyle.Alignment = HorizontalAlignment.Center;
                oddCellstyle.BorderBottom = BorderStyle.Thin;
                oddCellstyle.BorderLeft = BorderStyle.Thin;
                oddCellstyle.BorderRight = BorderStyle.Thin;
                oddCellstyle.BorderTop = BorderStyle.Thin;

                //偶数行样式
                var evenCellstyle = workbook.CreateCellStyle();
                //evenCellstyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
                //evenCellstyle.FillPattern = FillPattern.SolidForeground;
                evenCellstyle.VerticalAlignment = VerticalAlignment.Center;
                evenCellstyle.Alignment = HorizontalAlignment.Center;
                evenCellstyle.BorderBottom = BorderStyle.Thin;
                evenCellstyle.BorderLeft = BorderStyle.Thin;
                evenCellstyle.BorderRight = BorderStyle.Thin;
                evenCellstyle.BorderTop = BorderStyle.Thin;

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
                    var rowNumber = i + 2;
                    var row = summarySheet.CreateRow(rowNumber);
                    var cellStyle = rowNumber % 2 == 0 ? evenCellstyle : oddCellstyle;
                    var cell1 = row.CreateCell(0);
                    cell1.CellStyle = mergeCellstyle;
                    var cell2 = row.CreateCell(1);
                    cell2.CellStyle = cellStyle;
                    cell2.SetCellValue(item.MicrosoftDailyStatistics[i].ChannelName);
                    var cell3 = row.CreateCell(2);
                    cell3.CellStyle = cellStyle;
                    cell3.SetCellValue(item.MicrosoftDailyStatistics[i].NumberOfRegistration);
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
                //var totalRegion = new CellRangeAddress(item.MicrosoftDailyStatistics.Count + 2, item.MicrosoftDailyStatistics.Count + 2, 0, 1);
                //summarySheet.AddMergedRegion(totalRegion);

                if (item.MicrosoftDailyStatistics.Count > 1)
                {
                    var originRegion = new CellRangeAddress(2, item.MicrosoftDailyStatistics.Count + 1, 0, 0);
                    summarySheet.AddMergedRegion(originRegion);
                }

                for (var i = 0; i < item.MicrosoftDailyOrigins.Count; i++)
                {
                    var rowNumber = i + 1;
                    var row = eventhubSheet.CreateRow(rowNumber);
                    row.RowStyle = rowNumber % 2 == 0 ? evenCellstyle : oddCellstyle;
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
                n++;
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
