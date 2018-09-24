﻿using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.CSharp;
using V6AccountingBusiness;
using V6Controls.Forms;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6Controls
{
    public class V6ControlsHelper
    {
        public static bool DisableLookup { get; set; }

        private static readonly string V6SoftLocalAppData_Directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "V6Soft");
        private static readonly string SystemUserAppdataLocalTemp_Directory = Path.GetTempPath();
        /// <summary>
        /// Tạo thư mục tạm cho chương trình.
        /// </summary>
        /// <returns>Đường dẫn thư mục.</returns>
        public static string CreateV6SoftLocalAppDataDirectory()
        {
            try
            {
                if (!Directory.Exists(V6SoftLocalAppData_Directory)) Directory.CreateDirectory(V6SoftLocalAppData_Directory);
            }
            catch
            {
                //
            }
            return V6SoftLocalAppData_Directory;
        }

        public static void CreateKtmpDirectory()
        {
            string path = V6Options.K_TMP;
            if (!string.IsNullOrEmpty(path) && !Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static void DeleteAllFileInV6SoftLocalAppData()
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(V6SoftLocalAppData_Directory);

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
            }
            catch
            {
                //
            }
        }

        public static void DeleteAllRptTempFiles()
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(SystemUserAppdataLocalTemp_Directory);

                foreach (FileInfo file in di.GetFiles("*.rpt"))
                {
                    file.Delete();
                }
            }
            catch
            {
                //
            }
        }

        public static void DeleteAllSubDirectoriesInV6SoftLocalAppData()
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(SystemUserAppdataLocalTemp_Directory);

                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }
            }
            catch
            {
                //
            }
        }

        /// <summary>
        /// Tạo một Assembly từ code.
        /// </summary>
        /// <param name="name_space"></param>
        /// <param name="class_name"></param>
        /// <param name="dllName">Tên file dll, không bao gồm .dll</param>
        /// <param name="using_text"></param>
        /// <param name="method_text">Các hàm, nếu không có trả về null</param>
        /// <returns></returns>
        public static Type CreateProgram(string name_space, string class_name, string dllName, string using_text, string method_text)
        {
            if (string.IsNullOrEmpty(method_text)) return null;

            string output = "";
            try
            {
                var using_text0 =
                    "using System;"
                    + "using System.Collections.Generic;"
                    + "using System.Data;"
                    + "using System.Drawing;"
                    + "using System.Windows.Forms;"
                    + "using System.Data.SqlClient;"
                    + "using System.IO;"
                    + "using V6Init;"
                    + "using V6SqlConnect;"
                    + "using V6AccountingBusiness;"
                    + "using V6Controls;"
                    + "using V6Controls.Forms;"
                    + "using V6ReportControls;"
                    ;
                using_text = using_text0 + using_text;

                var src = new StringBuilder();
                src.Append(using_text);
                //src.AppendLine("using System;");
                //src.AppendLine("using System.Windows.Forms;");
                //src.AppendLine("using System.Drawing;");
                src.AppendLine("");
                src.AppendLine("namespace " + name_space + "{"); // open namespace
                src.AppendLine("public class " + class_name + "{ "); // open class
                src.Append(method_text);
                src.AppendLine(" ");
                src.AppendLine("}"); //end class
                src.AppendLine("}"); //end namespace

                CSharpCodeProvider provider = new CSharpCodeProvider();
                CompilerParameters parameters = new CompilerParameters();
                // Reference to System.Drawing library
                parameters.ReferencedAssemblies.Add("System.dll");
                parameters.ReferencedAssemblies.Add("System.Data.dll");
                parameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
                parameters.ReferencedAssemblies.Add("mscorlib.dll");
                parameters.ReferencedAssemblies.Add("System.Drawing.dll");
                parameters.ReferencedAssemblies.Add("System.Xml.dll");

                parameters.ReferencedAssemblies.Add(Application.StartupPath + "\\V6Tools.dll");
                parameters.ReferencedAssemblies.Add(Application.StartupPath + "\\V6Structs.dll");
                parameters.ReferencedAssemblies.Add(Application.StartupPath + "\\V6SqlConnect.dll");
                parameters.ReferencedAssemblies.Add(Application.StartupPath + "\\V6Controls.dll");
                parameters.ReferencedAssemblies.Add(Application.StartupPath + "\\V6AccountingBusiness.dll");
                parameters.ReferencedAssemblies.Add(Application.StartupPath + "\\V6ControlManager.dll");


                // True - memory generation, false - external file generation
                parameters.GenerateInMemory = false;
                // True - exe file generation, false - dll file generation
                parameters.GenerateExecutable = false;
                
                CreateV6SoftLocalAppDataDirectory();
                string path = Path.Combine(V6SoftLocalAppData_Directory, dllName + DateTime.Now.Ticks + ".dll");
                parameters.OutputAssembly = path;
                
                parameters.CompilerOptions = "/target:library /optimize";

                CompilerResults results = provider.CompileAssemblyFromSource(parameters, src.ToString());
                System.Collections.Specialized.StringCollection sc = results.Output;
                foreach (string s in sc)
                {
                    Console.WriteLine(s);
                    output += s + "\r\n";
                }
                Assembly assembly = results.CompiledAssembly;
                Type program = assembly.GetType(name_space + "." + class_name);
                return program;
                //ChangeColor = program.GetMethod("ChangeColor");
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.WriteExLog(MethodBase.GetCurrentMethod().DeclaringType + ".CreateProgram\r\n" + output + "\r\n" + method_text, ex);
            }

            V6ControlFormHelper.ShowWarningMessage("Lỗi mã mở rộng:\r\n" + output);

            return null;
        }

        /// <summary>
        /// Gọi hàm trong type
        /// </summary>
        /// <param name="program">Đối tượng class đưa vào.</param>
        /// <param name="methodName">Hàm trong class.</param>
        /// <param name="All_Objects">Đối tượng tham số. key là tên của tham số hàm.</param>
        /// <returns></returns>
        public static object InvokeMethodDynamic(Type program, string methodName, IDictionary<string, object> All_Objects)
        {
            if (program == null) return null;
            All_Objects["All_Objects"] = All_Objects;
            var method = program.GetMethod(methodName);
            if (method != null)
            {
                var parameters = method.GetParameters();
                var listObj = new List<object>();
                foreach (ParameterInfo info in parameters)
                {
                    if (All_Objects.ContainsKey(info.Name))
                    {
                        listObj.Add(All_Objects[info.Name]);
                    }
                    else
                    {
                        listObj.Add(null);
                    }
                }
                return method.Invoke(null, listObj.ToArray());
            }
            return null;
        }

        public static void SetBrotherFields(V6VvarTextBox txt, IDictionary<string, string> brothers)
        {
            var dataRow = txt.Data;
            if (dataRow == null) return;
            Form f = txt.FindForm();
            foreach (KeyValuePair<string, string> item in brothers)
            {
                Control c = V6ControlFormHelper.GetControlByName(f, item.Key);
                var value = dataRow[item.Value];
                if (c is V6NumberTextBox)
                {
                    ((V6NumberTextBox) c).Value = ObjectAndString.ObjectToDecimal(value);
                }
                else if (c is V6DateTimeColor)
                {
                    ((V6DateTimeColor)c).Value = ObjectAndString.ObjectToDate(value);
                }
                else if (c is DateTimePicker)
                {
                    ((DateTimePicker)c).Value = ObjectAndString.ObjectToFullDateTime(value);
                }
                else if (c != null)
                {
                    c.Text = value.ToString().Trim();
                }
            }
        }

        /// <summary>
        /// Hàm cổ trong Standar DAO
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="lstTable"></param>
        /// <returns></returns>
        public static DataTable KiemTraBangTonTai(string tableName, List<MyDataTable> lstTable)
        {
            if (tableName != "" && lstTable != null)
            {
                if (tableName.IndexOf("@", StringComparison.Ordinal) == -1)
                {
                    tableName = "@" + tableName;
                }
                if (lstTable.All(item => item.TableName != tableName)) return null;
                var result = lstTable.Find(tbl => tbl.TableName == tableName);
                return result.ObjTable;
            }
            else
            {
                throw new ArgumentException("KiemTraBangTonTai : tham số không hợp lệ");
            }
        }
        /// <summary>
        /// Lấy thông tin trong bảng V6Lookup
        /// </summary>
        /// <param name="vVar"></param>
        /// <returns></returns>
        public static StandardConfig LayThongTinCauHinh(string vVar)
        {
            var lstConfig = new StandardConfig();
            try
            {
                SqlParameter[] plist = {new SqlParameter("@p", vVar)};
                var executeResult = V6BusinessHelper.Select("V6Lookup", "*", "vVar=@p", "", "", plist);

                if (executeResult.Data != null && executeResult.Data.Rows.Count > 0)
                {
                    var tbl = executeResult.Data;
                    var row = tbl.Rows[0];

                    lstConfig.Vvar = row["VVar"].ToString().Trim();
                    lstConfig.TableName = row["vMa_file"].ToString().Trim();
                    lstConfig.Vorder = row["vOrder"].ToString().Trim();
                    lstConfig.FieldName = row["vValue"].ToString().Trim();
                    lstConfig.VLfScatter = row["vLfScatter"].ToString().Trim();
                    lstConfig.VWidths = (row["vWidths"].ToString().Trim());
                    lstConfig.VFields = (row["vFields"].ToString().Trim());
                    lstConfig.EFields = row["eFields"].ToString().Trim();
                    lstConfig.VHeaders = (row["vHeaders"].ToString().Trim());
                    lstConfig.EHeaders = (row["eHeaders"].ToString().Trim());
                    lstConfig.VUpdate = (row["vUpdate"].ToString().Trim());
                    lstConfig.VTitle = (row["vTitle"].ToString().Trim());
                    lstConfig.ETitle = (row["eTitle"].ToString().Trim());
                    lstConfig.VTitlenew = (row["VTitlenew"].ToString().Trim());
                    lstConfig.ETitlenew = (row["ETitlenew"].ToString().Trim());
                    lstConfig.LargeYn = Convert.ToInt32(row["Large_yn"]) == 1;
                    lstConfig.LoadAutoComplete = row["LOAD_AUTO"].ToString().Trim() == "1";
                    lstConfig.F3 = row["F3"].ToString().Trim() == "1";
                    lstConfig.F4 = row["F4"].ToString().Trim() == "1";
                    try
                    {
                        lstConfig.V1Title = (row["v1Title"].ToString().Trim());
                    }
                    catch
                    {
                        lstConfig.V1Title = ("Không có tiêu đề!");
                    }
                    try
                    {
                        lstConfig.E1Title = (row["e1Title"].ToString().Trim());
                    }
                    catch
                    {
                        lstConfig.E1Title = ("No title!");
                    }
                    try
                    {
                        lstConfig.VSearch = (row["v_Search"].ToString().Trim());
                    } //index 18
                    catch
                    {
                        lstConfig.VSearch = ("1=1 or 'a' ");
                    }
                }
                else
                {
                    lstConfig.NoInfo = true;
                }
            }
            catch (Exception ex)
            {
                lstConfig.Error = true;
                Logger.WriteToLog(string.Format("{0}.{1} {2}",
                    MethodBase.GetCurrentMethod().DeclaringType,
                    MethodBase.GetCurrentMethod().Name, ex.Message));
            }
            return lstConfig;
        }

        public static AldmConfig GetAldmConfig(string ma_dm)
        {
            AldmConfig lstConfig = new AldmConfig();
            try
            {
                SqlParameter[] plist = { new SqlParameter("@p", ma_dm) };
                var executeResult = V6BusinessHelper.Select("Aldm", "*", "Ma_dm=@p", "", "", plist);

                if (executeResult.Data != null && executeResult.Data.Rows.Count > 0)
                {
                    var tbl = executeResult.Data;
                    var row = tbl.Rows[0];
                    lstConfig = new AldmConfig(row.ToDataDictionary());
                }
                else
                {
                    lstConfig.NoInfo = true;
                }
            }
            catch (Exception ex)
            {
                lstConfig.Error = true;
                Logger.WriteToLog(string.Format("{0}.{1} {2}",
                    MethodBase.GetCurrentMethod().DeclaringType,
                    MethodBase.GetCurrentMethod().Name, ex.Message));
            }
            return lstConfig;
        }

        public static AldmConfig GetAldmConfigByTableName(string table_name)
        {
            AldmConfig lstConfig = new AldmConfig();
            try
            {
                SqlParameter[] plist = { new SqlParameter("@p", table_name) };
                var executeResult = V6BusinessHelper.Select("Aldm", "*", "Table_name=@p", "", "", plist);

                if (executeResult.Data != null && executeResult.Data.Rows.Count > 0)
                {
                    var tbl = executeResult.Data;
                    var row = tbl.Rows[0];
                    lstConfig = new AldmConfig(row.ToDataDictionary());
                }
                else
                {
                    lstConfig.NoInfo = true;
                }
            }
            catch (Exception ex)
            {
                lstConfig.Error = true;
                Logger.WriteToLog(string.Format("{0}.{1} {2}",
                    MethodBase.GetCurrentMethod().DeclaringType,
                    MethodBase.GetCurrentMethod().Name, ex.Message));
            }
            return lstConfig;
        }

        /// <summary>
        /// Lấy config trong bảng V6lookup bằng vVar. Nếu không có dữ liệu kiểm tra config.NoInfo.
        /// </summary>
        /// <param name="vVar"></param>
        /// <returns></returns>
        public static V6lookupConfig GetV6lookupConfig(string vVar)
        {
            V6lookupConfig lstConfig = new V6lookupConfig();
            try
            {
                SqlParameter[] plist = { new SqlParameter("@p", vVar) };
                var executeResult = V6BusinessHelper.Select("V6lookup", "*", "vVar=@p", "", "", plist);

                if (executeResult.Data != null && executeResult.Data.Rows.Count > 0)
                {
                    var tbl = executeResult.Data;
                    var row = tbl.Rows[0];
                    lstConfig = new V6lookupConfig(row.ToDataDictionary());
                }
                else
                {
                    lstConfig.NoInfo = true;
                }
            }
            catch (Exception ex)
            {
                lstConfig.Error = true;
                Logger.WriteToLog(string.Format("{0}.{1} {2}",
                    MethodBase.GetCurrentMethod().DeclaringType,
                    MethodBase.GetCurrentMethod().Name, ex.Message));
            }
            return lstConfig;
        }

        /// <summary>
        /// Lấy config trong bảng V6lookup bằng table_name. Nếu không có dữ liệu kiểm tra config.NoInfo.
        /// </summary>
        /// <param name="vMa_file">vMa_file</param>
        /// <returns></returns>
        public static V6lookupConfig GetV6lookupConfigByTableName(string vMa_file)
        {
            V6lookupConfig lstConfig = new V6lookupConfig();
            try
            {
                SqlParameter[] plist = { new SqlParameter("@p", vMa_file) };
                var executeResult = V6BusinessHelper.Select("V6lookup", "*", "vMa_file=@p", "", "", plist);

                if (executeResult.Data != null && executeResult.Data.Rows.Count > 0)
                {
                    var tbl = executeResult.Data;
                    var row = tbl.Rows[0];
                    lstConfig = new V6lookupConfig(row.ToDataDictionary());
                }
                else
                {
                    lstConfig.NoInfo = true;
                }
            }
            catch (Exception ex)
            {
                lstConfig.Error = true;
                Logger.WriteToLog(string.Format("{0}.{1} {2}",
                    MethodBase.GetCurrentMethod().DeclaringType,
                    MethodBase.GetCurrentMethod().Name, ex.Message));
            }
            return lstConfig;
        }

        /// <summary>
        /// Lấy thông tin V6Valid cho chứng từ theo ma_ct.
        /// </summary>
        /// <param name="ma_ct"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public static V6ValidConfig GetV6ValidConfig(string ma_ct, int attribute)
        {
            V6ValidConfig lstConfig = new V6ValidConfig();
            try
            {
                SqlParameter[] plist = { new SqlParameter("@p1", ma_ct), new SqlParameter("@p2", attribute) };
                var executeResult = V6BusinessHelper.Select("V6Valid", "*", "[ma_ct]=@p1 and [attribute]=@p2", "", "", plist);

                if (executeResult.Data != null && executeResult.Data.Rows.Count > 0)
                {
                    var tbl = executeResult.Data;
                    var row = tbl.Rows[0];
                    lstConfig = new V6ValidConfig(row.ToDataDictionary());
                }
                else
                {
                    lstConfig.NoInfo = true;
                }
            }
            catch (Exception ex)
            {
                lstConfig.Error = true;
                Logger.WriteToLog(string.Format("{0}.{1} {2}",
                    MethodBase.GetCurrentMethod().DeclaringType,
                    MethodBase.GetCurrentMethod().Name, ex.Message));
            }
            return lstConfig;
        }
        
        /// <summary>
        /// Lấy thông tin V6Valid cho danh mục theo tableName.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static V6ValidConfig GetV6ValidConfigDanhMuc(string tableName)
        {
            V6ValidConfig lstConfig = new V6ValidConfig();
            try
            {
                SqlParameter[] plist = { new SqlParameter("@p", tableName) };
                var executeResult = V6BusinessHelper.Select("V6Valid", "*", "attribute=3 and [TABLE_NAME]=@p", "", "", plist);

                if (executeResult.Data != null && executeResult.Data.Rows.Count > 0)
                {
                    var tbl = executeResult.Data;
                    var row = tbl.Rows[0];
                    lstConfig = new V6ValidConfig(row.ToDataDictionary());
                }
                else
                {
                    lstConfig.NoInfo = true;
                }
            }
            catch (Exception ex)
            {
                lstConfig.Error = true;
                Logger.WriteToLog(string.Format("{0}.{1} {2}",
                    MethodBase.GetCurrentMethod().DeclaringType,
                    MethodBase.GetCurrentMethod().Name, ex.Message));
            }
            return lstConfig;
        }

        public static String[] SliptString(string inputString, char typeChar)
        {
            if (inputString != "" && typeChar != ' ')
            {
                return inputString.Split(typeChar);
            }

            throw new ArgumentException("SliptString : tham số không hợp lệ");
        }

        public static void ThietLapTruongHienThiTrongDataGridView(
                DataGridView dgv,
                string lstStringFieldName,
                string lstStringFieldHeaders,
                string lstStringFieldWidth          )
        {
            if (String.IsNullOrEmpty(lstStringFieldWidth)) lstStringFieldWidth = "100";

            if (lstStringFieldHeaders != "" && lstStringFieldName != "" && lstStringFieldWidth != "")
            {
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    dgv.Columns[i].Visible = false;
                }

                // Lấy các cột được phép hiển thị trong DataGridView
                try
                {
                    String[] lstFieldName = SliptString(lstStringFieldName, ',');
                    List<string> lstFieldHeader = SliptString(lstStringFieldHeaders, ',').ToList();
                    List<string> lstFieldWidth = SliptString(lstStringFieldWidth, ',').ToList();
                    while (lstFieldWidth.Count < lstFieldName.Length)
                    {
                        lstFieldWidth.Add(lstFieldWidth[0]);
                    }
                    while (lstFieldHeader.Count < lstFieldName.Length)
                    {
                        lstFieldHeader.Add(lstFieldName[lstFieldHeader.Count]);
                    }
                    //int numColumns = dgv.Columns.Count - 1;
                    //int displayNum = lstDisplayHeader.Length - 1;
                    for (int i = 0; i < lstFieldName.Length; i++)
                    {
                        var field = lstFieldName[i].Trim();
                        var dataGridViewColumn = dgv.Columns[field];
                        if (dataGridViewColumn != null)
                        {
                            dataGridViewColumn.Visible = true;
                            dataGridViewColumn.DisplayIndex = i;
                            dataGridViewColumn.HeaderText = lstFieldHeader[i];
                            dataGridViewColumn.Width = Int32.Parse(lstFieldWidth[i]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("ThietLapTruongHienThiTrongDataGridView:\n" + ex.Message);
                }
            }
            else
            {
                throw new ArgumentException("V6ControlsHelper.ThietLapTruongHienThiTrongDataGridView : tham số không hợp lệ");
            }
        }

        public static SortedDictionary<string, object> DataGridViewRowToDataDictionary(DataGridViewRow row)
        {
            if (row == null) return null;
            var DataDic = new SortedDictionary<string, object>();
            for (int i = 0; i < row.DataGridView.Columns.Count; i++)
            {
                DataDic.Add(row.DataGridView.Columns[i].DataPropertyName.ToUpper(), row.Cells[i].Value);
            }
            return DataDic;
        }
    }

    
    
}
