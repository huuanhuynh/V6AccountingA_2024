using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using V6Tools;
using V6Tools.V6Convert;
using V6Tools.V6Export;

namespace V6DBF2EXCELTEMPLATE
{
    static class Program
    {
        static NumberFormatInfo nfi;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length > 5)
            {
                //V6DBF2XLSW.exe TABLE1.dbf TABLE2.dbf Template.xls Save.xls Field_excel1.txt EXCEL2.XML
                nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
                nfi.NumberGroupSeparator = " ";
                nfi.NumberDecimalSeparator = ",";

                string fileDbf1 = args[0];
                string fileDbf2 = args[1];
                string fileTemplate = args[2];
                string fileSave = args[3];
                string fileFields = args[4];
                string fileXml = args[5];
                var setting = new ExportExcelSetting();
                setting.xlsTemplateFile = fileTemplate;
                setting.saveFile = fileSave;
                var dbf1 = ParseDBF.ReadDBF(fileDbf1);
                var dbf2 = ParseDBF.ReadDBF(fileDbf2);
                DataTable data = Data_Table.FromTCVNtoUnicode(dbf1);
                DataTable data2 = Data_Table.FromTCVNtoUnicode(dbf2);
                setting.data = CookingDataForExcel(data);
                setting.data2 = CookingDataForExcel(data2);
                //setting.reportParameters = ReportDocumentParameters;
                //setting.albcConfigData = _albcConfig.DATA;
                string[] columns = ObjectAndString.SplitString(File.ReadAllText(fileFields));
                setting.columns = columns;


                bool insertRow = false, drawLine = false;
                DataSet ds = new DataSet();
                ds.ReadXml(fileXml);
                var parameters = new SortedDictionary<string, object>();
                if (ds.Tables.Count > 0)
                {
                    var paramTable = ds.Tables[0];
                    foreach (DataRow row in paramTable.Rows)
                    {
                        var type = row["type"].ToString().Trim();
                        var KEY = row["key"].ToString().Trim().ToUpper();
                        var content = row["content"].ToString().Trim();
                        if (type == "0")
                        {
                            if (KEY == "FIRSTCELL")
                                setting.SetFirstCell(content);// firstCell = content;
                            else if (KEY == "DRAWLINE")
                                drawLine = content == "1";
                            else if (KEY == "INSERTROW")
                                insertRow = content == "1";
                            else if (KEY == "BOLD_YN")
                            {
                                setting.BOLD_YN = content == "1";
                            }
                            else if (KEY == "BOLD_CONDITION") // "FIELD;=;VALUE"
                            {
                                var sss = ObjectAndString.SplitString(content);
                                if (sss.Length == 3)
                                {
                                    setting.BOLD_CONDITION = new Condition(sss[0], sss[1], sss[2]);
                                }
                                
                            }
                        }
                        //else if (type == "1") //Lay value trong parameter
                        //{
                        //    if (ExportExcelTemplate_ReportDocumentParameters == null) continue;
                        //    // 1 Nhóm ký tự giữa hai dấu ngoặc móc.
                        //    // Nếu không có ? sẽ lấy 1 nhóm từ đầu đến cuối.
                        //    // vd chuỗi "{123} {456}". có ? được 2 nhóm. không có ? được 1.
                        //    if (content.Contains("{") && content.Contains("}"))
                        //    {
                        //        var regex = new Regex("{(.+?)}");
                        //        foreach (Match match in regex.Matches(content))
                        //        {
                        //            var matchGroup0 = match.Groups[0].Value;
                        //            var matchContain = match.Groups[1].Value;
                        //            var matchColumn = matchContain.ToUpper();
                        //            var matchFormat = "";
                        //            if (matchContain.Contains(":"))
                        //            {
                        //                int _2dotIndex = matchContain.IndexOf(":", StringComparison.InvariantCulture);
                        //                matchColumn = matchContain.Substring(0, _2dotIndex).ToUpper();
                        //                matchFormat = matchContain.Substring(_2dotIndex + 1);
                        //            }
                        //            if (ExportExcelTemplate_ReportDocumentParameters.ContainsKey(matchColumn)
                        //                && ExportExcelTemplate_ReportDocumentParameters[matchColumn] is DateTime && matchFormat == "")
                        //            {
                        //                matchFormat = "dd/MM/yyyy";
                        //            }
                        //            if (ExportExcelTemplate_ReportDocumentParameters.ContainsKey(matchColumn))
                        //                content = content.Replace(matchGroup0,
                        //                    ObjectAndString.ObjectToString(ExportExcelTemplate_ReportDocumentParameters[matchColumn], matchFormat));
                        //        }
                        //        parameters.Add(KEY, content);
                        //    }
                        //    else
                        //    {
                        //        var P_KEY = content.ToUpper();
                        //        if (ExportExcelTemplate_ReportDocumentParameters.ContainsKey(P_KEY))
                        //        {
                        //            parameters.Add(KEY, ExportExcelTemplate_ReportDocumentParameters[P_KEY]);
                        //        }
                        //    }
                        //}
                        else if (type == "2" && setting.data2 != null
                            && setting.data2.Rows.Count > 0) //Lay value trong tbl2
                        {
                            var excel_row = setting.data2.Rows[0];

                            if (content.Contains("{") && content.Contains("}"))
                            {
                                var regex = new Regex("{(.+?)}");
                                foreach (Match match in regex.Matches(content))
                                {
                                    var matchGroup0 = match.Groups[0].Value;
                                    var matchContain = match.Groups[1].Value;
                                    var matchColumn = matchContain;
                                    var matchFormat = "";
                                    if (matchContain.Contains(":"))
                                    {
                                        int _2dotIndex = matchContain.IndexOf(":", StringComparison.InvariantCulture);
                                        matchColumn = matchContain.Substring(0, _2dotIndex);
                                        matchFormat = matchContain.Substring(_2dotIndex + 1);
                                    }
                                    if (setting.data2.Columns.Contains(matchColumn))
                                    {
                                        if (setting.data2.Columns[matchColumn].DataType == typeof(DateTime) && matchFormat == "")
                                        {
                                            matchFormat = "dd/MM/yyyy";
                                        }
                                        content = content.Replace(matchGroup0, ObjectAndString.ObjectToString(excel_row[matchColumn], matchFormat));
                                    }
                                }
                                if (parameters.ContainsKey(KEY))
                                {
                                    MessageBox.Show("Trùng khóa cấu hình excel: key=" + KEY);
                                    continue;
                                }
                                parameters.Add(KEY, content);
                            }
                            else
                            {
                                if (setting.data2.Columns.Contains(content))
                                {
                                    parameters.Add(KEY, excel_row[content]);
                                }
                            }
                        }
                        //else if (type == "3")//V6Soft.V6SoftValue
                        //{
                        //    if (content.Contains("{") && content.Contains("}"))
                        //    {
                        //        var regex = new Regex("{(.+?)}");

                        //        foreach (Match match in regex.Matches(content))
                        //        {
                        //            var MATCH_KEY = match.Groups[1].Value.ToUpper();
                        //            if (V6Soft.V6SoftValue.ContainsKey(MATCH_KEY))
                        //                content = content.Replace(match.Groups[0].Value,
                        //                    ObjectAndString.ObjectToString(V6Soft.V6SoftValue[MATCH_KEY]));
                        //        }
                        //        parameters.Add(KEY, content);
                        //    }
                        //    else
                        //    {
                        //        var P_KEY = content.ToUpper();
                        //        if (V6Soft.V6SoftValue.ContainsKey(P_KEY))
                        //        {
                        //            parameters.Add(KEY, V6Soft.V6SoftValue[P_KEY]);
                        //        }
                        //    }
                        //}
                    }
                }
                else
                {
                    //Không có thông tin xml
                }


                
                V6Tools.V6Export.ExportData.ToExcelTemplate(setting, columns, null,
                    parameters, nfi, insertRow, drawLine);

                
                
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
        }


        /// <summary>
        /// Copy dữ liệu đưa vào, đổi date 1900 về Dbnull.
        /// <para>Trim cuối chuỗi.</para>
        /// </summary>
        /// <param name="data">Dữ liệu đầu vào.</param>
        /// <returns>Dữ liệu kết quả.</returns>
        public static DataTable CookingDataForExcel(DataTable data)
        {
            if (data == null) return null;
            DateTime _1900 = new DateTime(1900, 1, 1);
            DataTable result = data.Copy();
            List<DataColumn> strColumnList = new List<DataColumn>();
            List<DataColumn> dateColumnList = new List<DataColumn>();
            foreach (DataColumn column in result.Columns)
            {
                if (ObjectAndString.IsStringType(column.DataType))
                {
                    strColumnList.Add(column);
                }
                else if (ObjectAndString.IsDateTimeType(column.DataType))
                {
                    dateColumnList.Add(column);
                }
            }

            foreach (DataRow row in result.Rows)
            {
                foreach (DataColumn column in dateColumnList)
                {
                    if (ObjectAndString.ObjectToFullDateTime(row[column]).Date == _1900) row[column] = DBNull.Value;
                }

                foreach (DataColumn column in strColumnList)
                {
                    row[column] = row[column].ToString().TrimEnd();
                }
            }
            return result;
        }




    }
}
