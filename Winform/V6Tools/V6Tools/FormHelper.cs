using System;
using System.Collections.Generic;
using System.Xml;
//using Microsoft.ApplicationBlocks.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace V6Tools
{
    //Sẽ dời qua project cần sử dụng.
    public class FormHelper
    {
        //public static void setcontextMenuStripText(ContextMenuStrip ct, string Name, string Text)
        //{
        //    //Duyệt qua menuContex
        //    foreach (ToolStripMenuItem Ctrl in ct.Items)
        //    {
        //        if(ChangeMenuItemText(Ctrl, Name, Text)) break;
        //    }
        //}

        public string GetXmlValue(string XmlFile, string Key)
        {
            //string xmlFile = APPPATH + xmlFileName;
            XmlTextReader reader = new XmlTextReader(XmlFile.ToLower());
            try
            {
                string value = "";
                while (reader.Read())
                {
                    if (reader.Name.ToUpper() == Key.ToUpper())
                    {
                        value = reader.GetAttribute("value");
                        break;
                    }                    
                }
                reader.Close();
                return value;
            }
            catch
            {
                reader.Close();
                return "";
            }
        }

        public static void SetMenuText(MenuStrip menuStrip, ControlInfoDictionary controlInfoDictionary, string lang)
        {
            foreach (ToolStripMenuItem item in menuStrip.Items)
            {
                ChangeMenuItemText(item, controlInfoDictionary, lang);
            }
        }
        public static void SetMenuText(ContextMenuStrip cMenuStrip, ControlInfoDictionary controlInfos, string lang)
        {
            foreach (ToolStripMenuItem item in cMenuStrip.Items)
            {
                ChangeMenuItemText(item, controlInfos, lang);
            }
        }
        internal static void ChangeMenuItemText(ToolStripMenuItem parentItem, ControlInfoDictionary controlInfoDictionary, string lang)
        {
            try
            {
                if (parentItem != null)// && parentItem.GetType().ToString() != "ToolStripSeparator")
                {
                    if(controlInfoDictionary.ContainsKey(parentItem.Name))
                    {
                        ControlInfo info = controlInfoDictionary[parentItem.Name];
                        parentItem.Text = info.GetText(lang);
                    }

                    for (int i = 0; i < parentItem.DropDownItems.Count; i++)
                    {
                        string s = parentItem.DropDownItems[i].GetType().ToString();
                        if (s != "System.Windows.Forms.ToolStripSeparator")
                            ChangeMenuItemText((ToolStripMenuItem)parentItem.DropDownItems[i], controlInfoDictionary, lang);
                    }
                }
                
            }
            catch
            {
                string s = parentItem.GetType().ToString();
            }
        }

        public static void SetAllFormControlInfo(Control form, ContextMenuStrip menu, XmlConfig config, string lang)
        {
            SetAllFormControlInfo(form, config, lang);
            SetMenuText(menu, config.m_ListControlInfo, lang);
        }
       
        public static void SetAllFormControlInfo(Control form, XmlConfig config, string lang)
        {
            SetFormControlsAndDynamicControlsText(form, config, lang);
            //SetFormControlsText(control, config.m_ListControlInfo, lang);
            //SetFormControlInfos(control, config.m_ListDynamicControl, lang);//for dynamic
            SetFormLockTextboxs(form, config.m_ListLockTextbox);
            SetFormLockButtons(form, config.m_ListLockButton);
        }

        public static void SetFormControlsAndDynamicControlsText(Control form, XmlConfig config, string lang)
        {
            //Ghep 2 dic
            ControlInfoDictionary infos = new ControlInfoDictionary();
            if (config.m_ListControlInfo!=null)
            foreach (var item in config.m_ListControlInfo)
            {
                infos.Add(item.Key, item.Value);
            }

            if (config.m_ListDynamicControl!=null)
            foreach (var item in config.m_ListDynamicControl)
            {
                if (item.Value.Type.ToUpper() == "TEXTBOX" || item.Value.Type.ToUpper() == "DATETIMEPICKER"
                    || item.Value.Text == "ID1" || item.Value.Text == "ID2")
                {

                }
                else
                {
                    infos.Add(item.Key, item.Value);
                }
            }
            //Gọi thằng dưới
            SetFormControlsText(form, infos, lang);
        }

        
        public static void SetFormControlsText(Control control, ControlInfoDictionary infos, string lang)
        {
            try
            {
                string type = control.GetType().ToString();
                if (type == "System.Windows.Forms.MenuStrip")
                {
                    SetMenuText((MenuStrip)control, infos, lang);
                }
                else if (type == "System.Windows.Forms.ContextMenuStrip")
                {
                    SetMenuText((ContextMenuStrip)control, infos, lang);
                }
                else
                {
                    if (infos.ContainsKey(control.Name))
                    {
                        ControlInfo info = infos[control.Name];

                        try
                        {
                            if (lang == "V")
                            {
                                control.Text = info.TextV;
                            }
                            else
                            {
                                control.Text = info.TextE;
                            }
                        }
                        catch
                        {

                        }
                    }

                    foreach (Control item in control.Controls)
                    {
                        SetFormControlsText(item, infos, lang);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox. Show("SetFormControlsText: " + ex.Message);
            }
        }

        public static void SetFormLockTextboxs(Control control, ControlInfoDictionary infos)
        {
            try
            {
                if (infos.ContainsKey(control.Name))
                {
                    try
                    {
                        ControlInfo info = infos[control.Name];
                        if (!string.IsNullOrEmpty(info.Text))
                        {
                            control.Text = info.Text;
                            control.Enabled = false;
                        }
                        else
                        {
                            control.Enabled = true;
                        }
                        //Neu bat buoc phai khoa
                        if (info.Lock)
                            control.Enabled = false;
                    }
                    catch
                    {

                    }
                }

                foreach (Control item in control.Controls)
                {
                    SetFormLockTextboxs(item, infos);
                }
            }
            catch (Exception ex)
            {
                MessageBox. Show("SetFormLockTextboxs: " + ex.Message);
            }
        }

        public static void SetFormLockButtons(Control control, ControlInfoDictionary infos)
        {
            try
            {
                if (infos.ContainsKey(control.Name))
                {
                    try
                    {
                        ControlInfo info = infos[control.Name];
                        if (!string.IsNullOrEmpty(info.Text))
                        {
                            control.Text = info.Text;
                        }

                        //Neu bat buoc phai khoa
                        if (info.Lock)
                            control.Enabled = false;
                    }
                    catch
                    {

                    }
                }

                foreach (Control item in control.Controls)
                {
                    SetFormLockButtons(item, infos);
                }
            }
            catch (Exception ex)
            {
                MessageBox. Show("SetFormLockButtons: " + ex.Message);
            }
        }

        public static ToolStripMenuItem FindParent(ToolStripMenuItem menu, string name)
        {
            string type;
            ToolStripMenuItem find = null;
            foreach (ToolStripItem item in menu.DropDownItems)
            {
                type = item.GetType().ToString();
                if (type == "System.Windows.Forms.ToolStripMenuItem")
                {
                    if (item.Name == name) return (ToolStripMenuItem)item;
                }
            }
            //Neu khong tim thay thi tim sau hon
            foreach (ToolStripItem item in menu.DropDownItems)
            {
                type = item.GetType().ToString();
                if (type == "System.Windows.Forms.ToolStripMenuItem")
                {
                    find = FindParent((ToolStripMenuItem)item, name);
                    if (find != null) return find;
                }
            }
            return null;
        }
        public static ToolStripMenuItem FindParent(MenuStrip menuStrip, string name)
        {
            //int i;
            string type;
            foreach (ToolStripItem item in menuStrip.Items)
            {
                
                type = item.GetType().ToString();
                if (type == "System.Windows.Forms.ToolStripMenuItem")
                if (item.Name == name)
                    return (ToolStripMenuItem)item;
            }
            //Neu khong thay thi tim trong nhanh con
            ToolStripMenuItem find = null;
            foreach (ToolStripItem item in menuStrip.Items)
	        {
                type = item.GetType().ToString();
                if (type == "System.Windows.Forms.ToolStripMenuItem")
                {
                    find = FindParent((ToolStripMenuItem)item,name);
                    if(find!=null) return find;
                }
	        }
            return null;
        }
        
        public static void AddDynamicMenuItems(MenuStrip menuStrip, ControlInfoDictionary controlInfos,
            string userName, string EpassWord, string lang)
        {
            foreach (var item in controlInfos)
            {
                ControlInfo info = item.Value;
                switch (info.Type.ToUpper())
                {
                    case "MENU":
                        ToolStripMenuItem menu = new ToolStripMenuItem();
                        menu.Name = info.Name;
                        menu.Text = info.GetText(lang);
                        if(!string.IsNullOrEmpty(info.Exe))
                        menu.Click += (object sender, EventArgs e) =>
                            {
                                try
                                {
                                    if (File.Exists(info.Exe.Trim()))
                                    {
                                        //  \' \" \\ \0 \a \b \f \n \r \t \v
                                        EpassWord = EpassWord.Replace("\'", "<single/>");
                                        EpassWord = EpassWord.Replace("\"", "<double/>");
                                        EpassWord = EpassWord.Replace("\\", "<except/>");
                                        EpassWord = EpassWord.Replace("\a", "<alert/>");
                                        EpassWord = EpassWord.Replace("\b", "<bip/>");
                                        EpassWord = EpassWord.Replace("\f", "<flag/>");
                                        EpassWord = EpassWord.Replace("\n", "<newline/>");
                                        EpassWord = EpassWord.Replace("\r", "<return/>");
                                        EpassWord = EpassWord.Replace("\t", "<tab/>");
                                        EpassWord = EpassWord.Replace("\v", "<v/>");
                                        if (info.Exe.EndsWith(" "))
                                            System.Diagnostics.Process.Start(info.Exe.Trim());
                                        else
                                            System.Diagnostics.Process.Start(info.Exe, string.Format("\"{0}\" \"{1}\" \"{2}\"", userName, EpassWord, lang));
                                    }
                                    else
                                    {
                                        MessageBox. Show("Không tìm thấy: " + info.Exe.Trim());
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox. Show("OpenApplicationError: " + ex.Message);
                                }
                            };
                        //Add to parent
                        try
                        {
                            ToolStripMenuItem parent = FindParent(menuStrip, info.ParentName);
                            if(parent != null)
                                parent.DropDownItems.Add(menu);
                        }
                        catch
                        {

                        }
                        break;
                    default: break;
                }
            }
        }
        public static ToolStripMenuItem FindParent(ContextMenuStrip menuStrip, string name)
        {
            //int i;
            string type;
            foreach (ToolStripItem item in menuStrip.Items)
            {

                type = item.GetType().ToString();
                if (type == "System.Windows.Forms.ToolStripMenuItem")
                    if (item.Name == name)
                        return (ToolStripMenuItem)item;
            }
            //Neu khong thay thi tim trong nhanh con
            ToolStripMenuItem find = null;
            foreach (ToolStripItem item in menuStrip.Items)
            {
                type = item.GetType().ToString();
                if (type == "System.Windows.Forms.ToolStripMenuItem")
                {
                    find = FindParent((ToolStripMenuItem)item, name);
                    if (find != null) return find;
                }
            }
            return null;
        }


        public static void AddDynamicMenuItems(ContextMenuStrip menuStrip, ControlInfoDictionary controlInfos,
            string userName, string passWord, string lang)
        {
            foreach (var item in controlInfos)
            {
                ControlInfo info = item.Value;
                switch (info.Type.ToUpper())
                {
                    case "MENU":
                        ToolStripMenuItem menu = new ToolStripMenuItem();
                        menu.Name = info.Name;
                        menu.Text = info.GetText(lang);
                        if (!string.IsNullOrEmpty(info.Exe))
                            menu.Click += (object sender, EventArgs e) =>
                            {
                                try
                                {
                                    if (File.Exists(info.Exe.Trim()))
                                    {
                                        if (info.Exe.EndsWith(" "))
                                            System.Diagnostics.Process.Start(info.Exe.Trim());
                                        else
                                            System.Diagnostics.Process.Start(info.Exe, string.Format("\"{0}\" \"{1}\" \"{2}\"", userName, passWord, lang));
                                    }
                                    else
                                    {
                                        MessageBox. Show("Không tìm thấy: " + info.Exe.Trim());
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox. Show("OpenApplicationError: " + ex.Message);
                                }
                            };
                        //Add to parent
                        try
                        {
                            ToolStripMenuItem parent = FindParent(menuStrip, info.ParentName);
                            if (parent != null)
                                parent.DropDownItems.Add(menu);
                        }
                        catch
                        {

                        }
                        break;
                    default: break;
                }
            }
        }

        


        #region ==== GenKeys ====
        /// <summary>
        /// Tạo một key AND
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns>" AND FieldName LIKE Values%"</returns>
        /// 
        public static string GenManyLikeKey(SortedList<string, string> list)
        {
            string keys = "";
            for (int i = 0; i < list.Count; i++)
            {
                keys += GenOneLikeKey(list.Keys[i], list.Values[i]);
            }
            return keys;
        }
        public static string RemoveSqlInjection(string value)
        {
            value = value.ToLower();
            value = value.Replace("'", "''");
            value = value.Replace("drop", "");
            value = value.Replace("delete", "");
            value = value.Replace("insert", "");
            value = value.Replace("update", "");
            value = value.Replace("select", "");
            return value;
        }
        public static string GenOneMatchKey(string field, string value)
        {
            if (value == "") return "";
            string key = "";
            value = RemoveSqlInjection(value);
            try
            {
                if (!string.IsNullOrEmpty(value))
                {                    
                    key = " AND " + field + " = '" + value + "'";
                }
            }
            catch { }
            return key;
        }
        public static string GenOneLikeKey(string field, string value)
        {
            if (value == "") return "";
            string key = "";
            value = RemoveSqlInjection(value);
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (!value.Contains("%") && !value.Contains("_"))
                    {
                        value = "%" + value + "%";
                    }
                    key = " AND " + field + " LIKE '" + value + "'";
                }
            }
            catch { }
            return key;
        }
        #endregion

        public static bool checkSQLConnection(string strCon)
        {
            try
            {
                SqlConnection sc = new SqlConnection(strCon);
                sc.Open();
                sc.Close();
                return true;
            }
            catch 
            {
                return false;
            }
            
        }

        public static void SetGridHeaderTextAndFormat(DataGridView dgv, GridFormatDictionary infos, MyMessage myMessage, string lang)
        {
            string errorsMessage = "", current = "";
            bool error = false;
            try
            {
                // Duyet qua tung info. neu giong ten trong grid thi l
                foreach (var info in infos)
                {
                    bool match = false;
                    int index = -1, infoIndex = 0;
                    bool isNumber = false;
                    foreach (DataGridViewColumn colum in dgv.Columns)
                    {
                        index++;
                        try
                        {
                            isNumber = int.TryParse(info.Key, out infoIndex);
                            if (colum.Name.ToUpper() == info.Key.ToUpper() || (isNumber&&index == infoIndex))
                            {
                                match = true;
                                current = colum.Name;
                                GridFormat gF = info.Value;
                                colum.HeaderText = gF.GetText(lang);

                                if (gF.DecimalPlace >= 0)
                                {
                                    colum.DefaultCellStyle.Format = "N" + gF.DecimalPlace;
                                    colum.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                }
                                if (!string.IsNullOrEmpty(gF.Format))
                                {
                                    colum.DefaultCellStyle.Format = gF.Format;
                                }
                                if(!string.IsNullOrEmpty(gF.Alignment))
                                {
                                    switch (gF.Alignment.ToUpper())
	                                {
                                        case "L":
                                            colum.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                                            break;
                                        case "R":
                                            colum.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                            break;
                                        case "C":
                                            colum.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                                            break;
		                                default:
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                
                            }
                        }
                        catch (Exception ex)
                        {
                            error = true;
                            errorsMessage += string.Format("\n{0} {1}", current, ex.Message);
                        }
                    }
                    if(!match)
                    {
                        error = true;
                        errorsMessage += string.Format("\n{0} [{1}] {2}",
                                myMessage.GridColumn, info.Key, myMessage.NoExist);
                    }
                }
                
                if (error)
                    throw new Exception(myMessage.FormatGridError);
            }
            catch (Exception ex)
            {
                MessageBox. Show("SetGridHeaderTextAndFormat: " + ex.Message + errorsMessage,
                    myMessage.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        public static string initLang()
        {
            string s="V";
            try
            {
                if (UtilityHelper.ReadRegistry("DFLANG") != null)
                {
                    s = UtilityHelper.ReadRegistry("DFLANG");
                    if (s != "V" && s != "E")//Chỉ hổ trợ VE
                        s = "V";
                }
                else
                {
                    //không đọc được thì tạo mặc định V
                    UtilityHelper.WriteRegistry("DFLANG", "V");
                    s = "V";
                }
                return s;
            }
            catch (Exception ex)
            {
                MessageBox. Show("InitLanguage: " + ex.Message);
                return s;
            }            
        }

        /// <summary>
        /// Lấy đường dẫn có sẵn dấu \
        /// </summary>
        /// <returns></returns>
        public static string getAppPath()
        {
            string APPPATH = Application.StartupPath;
            APPPATH += "\\";
            return APPPATH;
        }
        public static string GetRootPathReportFile()
        {
            try
            {
                string curDirectory = getAppPath();                
                return curDirectory + "V6RPT\\";
            }
            catch (Exception)
            {
                throw;
            }
        }
        
    }
}
