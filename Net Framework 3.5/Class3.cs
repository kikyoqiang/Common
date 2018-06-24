using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Net_Framework_3._5
{
    class Class3
    {
        static void Main22()
        {
           string pathStr = @"E:\0Project\0FaBu\" + DateTime.Now.ToString("HH_mm_ss") + ".xls";
            //建立空白工作簿
            IWorkbook workbook = new HSSFWorkbook();
            //在工作簿中：建立空白工作表
            ISheet sheet = workbook.CreateSheet();
            //在工作表中：建立行，参数为行号，从0计
            IRow row = sheet.CreateRow(0);
            //在行中：建立单元格，参数为列号，从0计
            ICell cell = row.CreateCell(1);
            //设置单元格内容
            cell.SetCellValue("清远透析机病人使用记录");

            ICellStyle style = workbook.CreateCellStyle();
            //设置单元格的样式：水平对齐居中
            style.Alignment = HorizontalAlignment.Center;
            style.VerticalAlignment = VerticalAlignment.Center;
            //新建一个字体样式对象
            IFont font = workbook.CreateFont();
            //设置字体加粗样式
            font.Boldweight = short.MaxValue;
            //使用SetFont方法将字体样式添加到单元格样式中 
            style.SetFont(font);
            //将新的样式赋给单元格
            cell.CellStyle = style;

            //设置单元格的高度
            //row.Height = 30 * 20;
            //设置单元格的宽度
            //sheet.SetColumnWidth(0, 30 * 256);
            //sheet.SetColumnWidth(0,  30 * 256);

            //设置一个合并单元格区域，使用上下左右定义CellRangeAddress区域
            //CellRangeAddress四个参数为：起始行，结束行，起始列，结束列
            sheet.AddMergedRegion(new CellRangeAddress(0, 1, 1, 6));

            //通过Cell的CellFormula向单元格中写入公式
            //注：直接写公式内容即可，不需要在最前加'='
            //ICell cell2 = sheet.CreateRow(1).CreateCell(0);
            //cell2.CellFormula = "HYPERLINK(\"测试图片.jpg\",\"测试图片.jpg\")";

            //将工作簿写入文件
            using (FileStream fs = new FileStream(pathStr, FileMode.Create, FileAccess.Write))
            {
                workbook.Write(fs);
            }
            Console.ReadKey();
        }

    }
}
