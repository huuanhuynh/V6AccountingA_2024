using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.CSharp;
using V6Controls.Forms;
using V6Init;
using V6Tools.V6Convert;

namespace V6Controls
{
    public class V6ControlsHelper
    {
        /// <summary>
        /// (True) Tắt lookup để đóng form. (False) Bật lookup lại nếu đã tắt khi vào form.
        /// </summary>
        public static bool DisableLookup { get; set; }

        
        /// <summary>
        /// Tạo thư mục tạm cho chương trình.
        /// </summary>
        /// <returns>Đường dẫn thư mục.</returns>
        public static string CreateV6SoftLocalAppDataDirectory()
        {
            try
            {
                if (!Directory.Exists(V6Setting.V6SoftLocalAppData_Directory)) Directory.CreateDirectory(V6Setting.V6SoftLocalAppData_Directory);
            }
            catch
            {
                //
            }
            return V6Setting.V6SoftLocalAppData_Directory;
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
                if (!Directory.Exists(V6Setting.V6SoftLocalAppData_Directory))
                {
                    Directory.CreateDirectory(V6Setting.V6SoftLocalAppData_Directory);
                    return;
                }

                DirectoryInfo di = new DirectoryInfo(V6Setting.V6SoftLocalAppData_Directory);
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

        /// <summary>
        /// <para>Temp rpt file causes the Load Report Failed Error.</para>
        /// <para>https://archive.sap.com/discussions/thread/996568</para>
        /// </summary>
        public static void DeleteAllRptTempFiles()
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(V6Setting.SystemUserAppDataLocalTemp_Directory);

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
                DirectoryInfo di = new DirectoryInfo(V6Setting.SystemUserAppDataLocalTemp_Directory);

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
                    //+ "using System.Linq;"
                    + "using System.Windows.Forms;"
                    + "using System.Data.SqlClient;"
                    + "using System.IO;"
                    + "using V6Init;"
                    + "using V6Structs;"
                    + "using V6SqlConnect;"
                    + "using V6AccountingBusiness;"
                    + "using V6AccountingBusiness.Invoices;"
                    + "using V6Controls;"
                    + "using V6Controls.Controls;"
                    + "using V6Controls.Forms;"
                    + "using V6ReportControls;"
                    + "using V6ControlManager.FormManager.ChungTuManager;"
                    + "using V6ControlManager.FormManager.ReportManager.Filter;"
                    + "using V6ControlManager.FormManager.ReportManager.ReportR;"
                    + "using V6ControlManager.FormManager.ReportManager.XuLy;"
                    + "using V6Tools;"
                    + "using V6Tools.V6Convert;"
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
                string path = Path.Combine(V6Setting.V6SoftLocalAppData_Directory, dllName + DateTime.Now.Ticks + ".dll");
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
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("InvokeMethodDynamic " + ex.Message);
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

        ///// <summary>
        ///// Hàm cổ trong Standar DAO
        ///// </summary>
        ///// <param name="tableName"></param>
        ///// <param name="lstTable"></param>
        ///// <returns></returns>
        //public static DataTable KiemTraBangTonTai(string tableName, List<MyDataTable> lstTable)
        //{
        //    if (tableName != "" && lstTable != null)
        //    {
        //        if (tableName.IndexOf("@", StringComparison.Ordinal) == -1)
        //        {
        //            tableName = "@" + tableName;
        //        }
        //        if (lstTable.All(item => item.TableName != tableName)) return null;
        //        var result = lstTable.Find(tbl => tbl.TableName == tableName);
        //        return result.ObjTable;
        //    }
        //    else
        //    {
        //        throw new ArgumentException("KiemTraBangTonTai : tham số không hợp lệ");
        //    }
        //}
        
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

        public static FlyLabelForm FlyLabel_Form;
        public static FlyTextForm FlyText_Form;
        /// <summary>
        /// Tạo sẵn form nổi vvar name.
        /// </summary>
        public static void CreateFlyLabelForm()
        {
            if (FlyLabel_Form == null)
            {
                FlyLabel_Form = new FlyLabelForm();
                FlyLabel_Form.Top = -FlyLabel_Form.Height;
                FlyLabel_Form.Show();
            }
        }
        public static void CreateFlyTextForm()
        {
            if (FlyText_Form == null)
            {
                FlyText_Form = new FlyTextForm();
                FlyText_Form.Top = -FlyText_Form.Height;
                FlyText_Form.Show();
            }
        }

        /// <summary>
        /// Hiển thị một form TopMost chứa thông báo, (Không ảnh hưởng đến focus đang làm việc).
        /// </summary>
        /// <param name="owner">Form chủ.</param>
        public static void ShowVvarName(V6VvarTextBox owner)
        {
            if (FlyLabel_Form == null) return;
            //CreateVvarNameForm();
            FlyLabel_Form.TargetControl = owner;
            string nfield = V6Setting.IsVietnamese ? owner.LookupInfo.VName : owner.LookupInfo.VName2;
            if (!string.IsNullOrEmpty(nfield))
            {
                var sss = ObjectAndString.SplitString(nfield);
                if (owner.Data == null)
                {
                    FlyLabel_Form.StopShow();
                }
                else if (owner.Data.Table.Columns.Contains(sss[0]))
                {
                    FlyLabel_Form.Message = ObjectAndString.ObjectToString(owner.Data[sss[0]]);
                }
            }
        }
        
        public static void ShowColorText(V6ColorTextBox txtBox)
        {
            if (FlyText_Form == null) return;
            FlyText_Form.TargetControl = txtBox;
            
            if (TextRenderer.MeasureText(txtBox.Text, txtBox.Font).Width > txtBox.Width)
            {
                FlyText_Form.Message = txtBox.Text;
            }
            else
            {
                FlyText_Form.StopShow();
            }
        }
        
        /// <summary>
        /// Hiển thị một form TopMost chứa thông báo, (Không ảnh hưởng đến focus đang làm việc).
        /// </summary>
        /// <param name="owner">Form chủ.</param>
        public static void ShowVvarName(V6LookupData owner)
        {
            if (FlyLabel_Form == null) return;
            //CreateVvarNameForm();
            FlyLabel_Form.TargetControl = owner;
            string nfield = V6Setting.IsVietnamese ? owner.LookupInfo.VName : owner.LookupInfo.VName2;
            if (!string.IsNullOrEmpty(nfield))
            {
                var sss = ObjectAndString.SplitString(nfield);
                if (owner.Data == null)
                {
                    FlyLabel_Form.StopShow();
                }
                else if (owner.Data.Table.Columns.Contains(sss[0]))
                {
                    FlyLabel_Form.Message = ObjectAndString.ObjectToString(owner.Data[sss[0]]);
                }
            }
        }

        public static void ShowLookupTextBoxName(V6LookupTextBox owner)
        {
            if (FlyLabel_Form == null) return;
            //CreateVvarNameForm();
            FlyLabel_Form.TargetControl = owner;
            string nfield = V6Setting.IsVietnamese ? owner.LookupInfo.VName : owner.LookupInfo.VName2;
            if (!string.IsNullOrEmpty(nfield))
            {
                var sss = ObjectAndString.SplitString(nfield);
                if (owner.Data == null)
                {
                    FlyLabel_Form.StopShow();
                }
                else if (owner.Data.Table.Columns.Contains(sss[0]))
                {
                    FlyLabel_Form.Message = ObjectAndString.ObjectToString(owner.Data[sss[0]]);
                }
            }
        }
        public static void ShowLookupProcName(V6LookupProc owner)
        {
            if (FlyLabel_Form == null) return;
            //CreateVvarNameForm();
            FlyLabel_Form.TargetControl = owner;
            string nfield = V6Setting.IsVietnamese ? owner.LookupInfo.VName : owner.LookupInfo.VName2;
            if (!string.IsNullOrEmpty(nfield))
            {
                var sss = ObjectAndString.SplitString(nfield);
                if (owner.Data == null)
                {
                    FlyLabel_Form.StopShow();
                }
                else if (owner.Data.ContainsKey(sss[0].ToUpper()))
                {
                    FlyLabel_Form.Message = ObjectAndString.ObjectToString(owner.Data[sss[0].ToUpper()]);
                }
            }
        }

        /// <summary>
        /// 'a' #date# num
        /// </summary>
        /// <param name="value"></param>
        /// <param name="oper">= like start</param>
        /// <returns></returns>
        public static string GenRowFilterStringValue(object value, string oper = "=")
        {
            string s;
            bool like = oper == "like";
            bool start = oper == "start";
            if (value != null)
            {
                switch (value.GetType().ToString())
                {
                    case "System.DateTime":
                    case "System.DateTime?":
                    case "System.Nullable`1[System.DateTime]":
                        try
                        {
                            s = "#" + ((DateTime)value)
                                .ToString("MM/dd/yyyy") + "#";
                        }
                        catch
                        {
                            //if (allowNull)
                            //    s = "null";
                            //else
                                s = "#" + V6Setting.M_SV_DATE.ToString("MM/dd/yyyy") + "#";
                        }
                        break;
                    case "System.Boolean":
                        s = (bool)value ? "1" : "0";
                        break;
                    case "System.Byte":
                    case "System.Int16":
                    case "System.Int32":
                    case "System.Int64":
                    case "System.Decimal":
                    case "System.Double":
                        try
                        {
                            s = value.ToString(); //.Replace(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, ".");
                        }
                        catch
                        {
                            s = "0";
                        }
                        break;
                    case "System.DBNull":
                        s = "null";
                        break;
                    default:
                        s = "'" + (like? "%" : "")
                            + value.ToString().Trim().Replace("'", "''")
                            + (like || start ? "%" : "") + "'";
                        break;
                }
            }
            //else if (allowNull)
            //{
            //    s = "null";
            //}
            else
            {
                s = "''";
            }

            return s;
        
        }
    }//end class
}
