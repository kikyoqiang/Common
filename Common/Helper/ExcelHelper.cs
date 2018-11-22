using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace Common
{
    public class ExcelHelper
    {
        /// <summary>
        /// 将DataTable数据导入到excel中
        /// </summary>
        /// <param name="data">要导入的数据</param>
        /// <param name="isColumnWritten">DataTable的列名是否要导入</param>
        /// <param name="fileName">文件名</param>
        /// <param name="sheetName">要导入的excel的sheet的名称</param>
        /// <returns>导入数据行数(包含列名那一行)</returns>
        public static bool DataTableToExcel(DataTable data, string fileName, string sheetName, bool isColumnWritten)
        {
            FileStream fs = null;
            try
            {
                int dotIndex = fileName.LastIndexOf(".");
                if (dotIndex < 0)
                {
                    // MessageManager.ShowErrorMsg("没有指定文件的扩展名!", "错误");
                    return false;
                }
                string fileExtName = fileName.Substring(dotIndex);

                if (fileExtName != ".xls")
                {
                    //MessageManager.ShowErrorMsg("不支持的文件类型!", "错误");
                    return false;
                }


                int i = 0;
                int j = 0;
                int count = 0;
                ISheet sheet = null;

                fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);

                IWorkbook workbook = new HSSFWorkbook();


                if (workbook != null)
                {
                    sheet = workbook.CreateSheet(sheetName);
                }
                else
                {
                    return false;
                }

                if (isColumnWritten == true) //写入DataTable的列名
                {
                    IRow row = sheet.CreateRow(0);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(data.Columns[j].ColumnName);
                    }
                    count = 1;
                }
                else
                {
                    count = 0;
                }

                for (i = 0; i < data.Rows.Count; ++i)
                {
                    IRow row = sheet.CreateRow(count);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                    }
                    ++count;
                }
                workbook.Write(fs); //写入到excel
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return false;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }

        /// <summary>
        /// 将excel中的数据导入到DataTable中
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="sheetName">excel工作薄sheet的名称</param>
        /// <param name="isFirstRowColumn">第一行是否是DataTable的列名</param>
        /// <returns>返回的DataTable</returns>
        public static DataTable ExcelToDataTable(string fileName, string sheetName, bool isFirstRowColumn)
        {
            FileStream fs = null;
            try
            {
                int dotIndex = fileName.LastIndexOf(".");
                if (dotIndex < 0)
                {
                    //MessageManager.ShowErrorMsg("没有指定文件的扩展名!", "错误");
                    return null;
                }
                string fileExtName = fileName.Substring(dotIndex);

                if (fileExtName != ".xls")
                {
                    //MessageManager.ShowErrorMsg("不支持的文件类型!", "错误");
                    return null;
                }

                ISheet sheet = null;
                DataTable data = new DataTable();
                int startRow = 0;
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);

                IWorkbook workbook = new HSSFWorkbook(fs);

                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                    if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }

                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                dataRow[j] = row.GetCell(j).ToString();
                        }
                        data.Rows.Add(dataRow);
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                //MessageManager.ShowErrorMsg(ex.Message, "错误");
                return null;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }



        private HSSFPatriarch patriarch = null;

        private string exportFilePath = string.Format("{0}{1}.xls", AppDomain.CurrentDomain.BaseDirectory, DateTime.Now.ToString("mmfff"));

        private IWorkbook workbook = null;
        private List<ISheet> listSheet = new List<ISheet>();

        private int columnWidth = -1;
        private int iSheetCount = 0;

        #region ExcelHelper
        /// <summary>
        /// 构造函数
        /// <param name="filePath">默认的保存的文件名</param>
        /// <param name="defaultColumnWidth">列宽,不传递的话使用默认列宽</param>
        /// </summary>
        public ExcelHelper(string filePath, int columnWidth = -1)
        {
            exportFilePath = filePath;
            this.columnWidth = columnWidth;

            workbook = new HSSFWorkbook();
        }
        #endregion

        #region SaveFile
        /// <summary> 保存生成文件 </summary>
        public bool SaveFile(string filePath = "")
        {
            if (filePath.IsNotEmpty())
                exportFilePath = filePath;

            Stream fs = null;
            try
            {
                MemoryStream ms = new MemoryStream();
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;

                fs = new FileStream(exportFilePath, FileMode.Create, FileAccess.Write);
                byte[] data = ms.ToArray();

                fs.Write(data, 0, data.Length);
                fs.Flush();
            }
            catch (System.Exception ex)
            {
                LogHelper.Instance.WriteError("导出Excel最后生成文件失败", ex);
                return false;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }

            return true;
        }
        #endregion

        #region InsertDataTable
        /// <summary>
        /// 向指定的sheet添加DataTable数据
        /// </summary>
        /// <param name="dtInput">输入数据</param>
        /// <param name="iSheetNO">sheet编号(从0开始)</param>
        /// <param name="bInsertHeader">是否写入DataTable列名</param>
        /// <returns></returns>
        public bool InsertDataTable(DataTable dtInput, int iSheetNO, bool bInsertHeader = true)
        {
            try
            {
                int startRowNo = 0;

                if (listSheet[iSheetNO].LastRowNum != 0)
                    startRowNo = listSheet[iSheetNO].LastRowNum + 5;

                //列头处理
                if (bInsertHeader)
                {
                    IRow headerRow = listSheet[iSheetNO].CreateRow(startRowNo);
                    foreach (DataColumn column in dtInput.Columns)
                    {
                        ICell tmpCell = headerRow.CreateCell(column.Ordinal);
                        tmpCell.CellStyle.WrapText = true;
                        tmpCell.SetCellValue(column.ColumnName);
                    }

                    startRowNo++;
                }

                //数据处理
                int rowIndex = startRowNo;
                float f = 0.00f;
                foreach (DataRow row in dtInput.Rows)
                {
                    IRow dataRow = listSheet[iSheetNO].CreateRow(rowIndex);

                    foreach (DataColumn column in dtInput.Columns)
                    {
                        string strTmp = row.GetStringValue(column.ColumnName);
                        if (strTmp.IsNullOrEmpty() ||
                            column.ColumnName.IndexOf("身份证") >= 0 ||
                            column.ColumnName.IndexOf("病案号") >= 0 ||
                            column.ColumnName.IndexOf("住院号") >= 0)
                        {
                            ICell tmpCell = dataRow.CreateCell(column.Ordinal, CellType.String);
                            tmpCell.SetCellValue(strTmp);
                        }
                        else if (float.TryParse(strTmp, out f))
                        {
                            ICell tmpCell = dataRow.CreateCell(column.Ordinal, CellType.Numeric);
                            tmpCell.SetCellValue((double)strTmp.ToDecimal());
                        }
                        else
                        {
                            ICell tmpCell = dataRow.CreateCell(column.Ordinal, CellType.String);
                            tmpCell.SetCellValue(strTmp);
                        }
                    }

                    rowIndex++;
                }
            }
            catch (System.Exception ex)
            {
                LogHelper.Instance.WriteError("导出Excel插入DataTable到指定sheet失败", ex);
                throw;
            }

            return true;
        }
        #endregion

        #region InsertGridView
        /// <summary>
        /// 导出DataGridView的数据（仅导出可见列的数据）YFC
        /// </summary>
        /// <param name="grav">要导出的DataGridView</param>
        /// <returns></returns>
        public bool InsertGridView(System.Windows.Forms.DataGridView grav, int iSheetNO)
        {
            if (grav == null) return false;

            List<string> remove = new List<string>();
            DataTable table = new DataTable();

            for (int i = 0; i < grav.Columns.Count; i++)
            {
                string headertext = grav.Columns[i].HeaderText;
                if (grav.Columns[i].Visible == false)
                {
                    remove.Add(headertext);
                }
                table.Columns.Add(headertext);
            }

            for (int j = 0; j < grav.Rows.Count; j++)
            {
                DataRow newrow = table.NewRow();
                for (int k = 0; k < grav.Columns.Count; k++)
                {
                    newrow[k] = grav.Rows[j].Cells[k].Value;
                }
                table.Rows.Add(newrow);
            }

            for (int i = 0; i < remove.Count; i++)
            {
                if (table.Columns.Contains(remove[i]))
                {
                    table.Columns.Remove(remove[i]);
                }
            }

            return InsertDataTable(table, iSheetNO);
        }
        #endregion

        #region InsertPicture
        /// <summary>
        /// 插入图片,切设置图片显示范围。
        /// </summary>
        /// <param name = "PicturePath">图片的绝对物理路径</param>
        /// <param name = "pictureType">图片类型</param>
        /// <param name = "startColNo">左上角所在列号(从0开始)</param>
        /// <param name = "startRowNo">左上角所在行号(从0开始)</param>
        /// <param name = "endColNo">右下角所在列号(从0开始)</param>
        /// <param name = "endRowNo">右下角所在行号(从0开始)</param>
        /// <param name = "originalSize">是否按图片原始大小显示,如果是的话传入的右下角参数无效</param>
        public bool InsertPicture(string picturePath, PictureType pictureType, int startColNo, int startRowNo, int endColNo, int endRowNo, int iSheetNO, bool originalSize = false)
        {
            try
            {
                if (!File.Exists(picturePath))
                    return false;

                if (originalSize)
                {
                    endColNo = startColNo + 1;
                    endRowNo = startRowNo + 1;
                }

                //将图片加入Workbook
                byte[] bytes = System.IO.File.ReadAllBytes(picturePath);
                int pictureIdx1 = workbook.AddPicture(bytes, PictureType.JPEG);

                //获取存在的Sheet，必须在AddPicture之后
                if (patriarch == null)
                {
                    patriarch = (HSSFPatriarch)listSheet[iSheetNO].CreateDrawingPatriarch();
                }
                //插入图片
                HSSFClientAnchor anchor = new HSSFClientAnchor(0, 0, 1023, 0, startColNo, startRowNo, endColNo, endRowNo);
                HSSFPicture pict = (HSSFPicture)patriarch.CreatePicture(anchor, pictureIdx1);

                if (originalSize)
                {
                    pict.Resize();
                }
            }
            catch (System.Exception ex)
            {
                LogHelper.Instance.WriteError("导出Excel插入图片失败", ex);
                return false;
            }

            return true;
        }
        #endregion

        #region 添加一个sheet
        /// <summary>
        /// 为文档添加一个sheet,传入文件名为空或者不传入的话将使用默认文件名
        /// </summary>
        public bool AddSheet(string strSheetName = "")
        {
            try
            {
                ISheet newSheet = workbook.CreateSheet();

                newSheet.DefaultRowHeight = (short)(500);
                if (columnWidth != -1)
                    newSheet.DefaultColumnWidth = columnWidth;

                if (!strSheetName.IsNullOrEmpty())
                    workbook.SetSheetName(iSheetCount, strSheetName);

                listSheet.Add(newSheet);
                iSheetCount++;
            }
            catch (System.Exception ex)
            {
                LogHelper.Instance.WriteError("", ex);
                return false;
            }

            return true;
        }
        #endregion

        public void SetColumnWidth(int iSheetNO, short colIndex, int value)
        {
            if (listSheet.Count > iSheetNO)
                listSheet[iSheetNO].SetColumnWidth(colIndex, value);
        }

        public void InsertText(int iSheetNO, short rowIndex, short colIndex, string value)
        {
            IRow row = listSheet[iSheetNO].GetRow(rowIndex);
            if (row == null)
            {
                //当前行还未存在,创建它
                row = listSheet[iSheetNO].CreateRow(rowIndex);
            }

            ICell cell = row.GetCell(colIndex);
            if (cell == null)
            {
                //当前单元格还未存在,创建它
                cell = row.CreateCell(colIndex, CellType.String);
            }

            cell.CellStyle.WrapText = true;
            cell.SetCellValue(value);
        }
    }
}
