using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;


namespace V6Tools
{
    public class XmlConfig
    {
        public string errorsMessage = "";
        public ReportInfo m_ReportInfo { get; private set; }
        public KeyDefine m_KeyDefine { get; private set; }
        public ControlInfoDictionary m_ListControlInfo { get; private set; }
        public ControlInfoDictionary m_ListLockTextbox { get; private set; }
        public ControlInfoDictionary m_ListLockButton { get; private set; }
        public ControlInfoDictionary m_ListDynamicControl { get; private set; }
        public ListReportParam m_ListReportParam { get; private set; }
        public ListReportParam m_ListReportDataField { get; private set; }
        public GridFormatDictionary m_GridFormatDictionary { get; private set; }

        public DynamicControlParamDictionary m_DynamicProcParamDictionary { get; private set; }
        public DynamicControlParamDictionary m_DynamicReportParamDictionary { get; private set; }
        
        public XmlConfig(string fileName)
        {
            ReadXmlConfig(fileName);
        }

        public void ReadXmlConfig(string fileName)
        {
            errorsMessage = "";
            if (File.Exists(fileName))
            {
                XmlTextReader reader = new XmlTextReader(fileName);
                try
                {
                    m_ReportInfo = new ReportInfo();
                    m_KeyDefine = new KeyDefine();
                    m_ListControlInfo = new ControlInfoDictionary();
                    m_ListLockTextbox = new ControlInfoDictionary();
                    m_ListLockButton = new ControlInfoDictionary();
                    m_ListDynamicControl = new ControlInfoDictionary();
                    m_ListReportParam = new ListReportParam();
                    m_ListReportDataField = new ListReportParam();
                    m_GridFormatDictionary = new GridFormatDictionary();
                    m_DynamicProcParamDictionary = new DynamicControlParamDictionary();
                    m_DynamicReportParamDictionary = new DynamicControlParamDictionary();
                    while (reader.Read())
                    {
                        try
                        {
                            switch (reader.Name)
                            {
                                case "ReportInfo":
                                    m_ReportInfo.ReadReportInfo(reader);
                                    break;
                                case "KeyDefine":
                                    m_KeyDefine.ReadKeys(reader);
                                    break;
                                case "Control":
                                    m_ListControlInfo.AddControlInfo(reader);
                                    break;
                                case "DynamicControl":
                                    m_ListDynamicControl.AddDynamicControlInfo(reader);
                                    break;
                                case "LockTextbox":
                                    m_ListLockTextbox.AddControlInfo(reader);
                                    break;
                                case "LockButton":
                                    m_ListLockButton.AddControlInfo(reader);
                                    break;
                                case "ReportParam":
                                    m_ListReportParam.AddReportParamInfo(reader);
                                    break;
                                case "ReportDataField":
                                    m_ListReportDataField.AddReportParamInfo(reader);
                                    break;
                                case "GridFormat":
                                    m_GridFormatDictionary.AddGridFormatInfo(reader);
                                    break;
                                case "DynamicProcParam":
                                    m_DynamicProcParamDictionary.AddParam(reader);
                                    break;
                                case "DynamicReportParam":
                                    m_DynamicReportParamDictionary.AddParam(reader);
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            errorsMessage += "\n" + reader.Name + ":" + ex.Message;
                        }
                    }
                    
                    reader.Close();
                }
                catch (Exception ex)
                {
                    reader.Close();
                    errorsMessage=("ReadXmlConfig: " + ex.Message);
                }
            }
            else
            {
                errorsMessage = ("Không thấy file: " + fileName);
            }

            if(errorsMessage.Length>0)
                System.Windows.Forms.MessageBox. Show("All erors: " + errorsMessage);
        }
        
    }


    public class GridFormatDictionary: Dictionary<string, GridFormat>
    {
        /// <summary>
        /// Cần có hình thức gom lỗi cho nhưng hàm Add...
        /// </summary>
        /// <param name="reader"></param>
        public void AddGridFormatInfo(XmlReader reader)
        {
            GridFormat info = new GridFormat();
            try
            {
                info.Name = reader.GetAttribute("Name");
                info.TextV = reader.GetAttribute("TextV");
                info.TextE = reader.GetAttribute("TextE");
                info.Alignment = reader.GetAttribute("Alignment");
                string stemp = reader.GetAttribute("Decimal");
                int itemp;
                info.DecimalPlace = (stemp != null && int.TryParse(stemp, out itemp)) ? itemp : -1;
                info.Format = reader.GetAttribute("Format");
                Add(info.Name, info);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("AddGridFormatInfo name:{0}, V:{1}, E:{2}, Exc:{3}",
                    info.Name, info.TextV, info.TextE, ex.Message));
            }
        }
    }
    public class ListReportParam : List<ReportParam> {
        public void AddReportParamInfo(XmlReader reader)
        {
            ReportParam info = new ReportParam();
            try
            {
                info.ParameterField = reader.GetAttribute("ParameterField");
                info.ParameterV = reader.GetAttribute("ParameterV");
                info.ParameterE = reader.GetAttribute("ParameterE");
                Add(info);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("AddReportParamInfo: Name:{0}, V:{1}, E:{2}, Exc:{3}",
                     info.ParameterField, info.ParameterV, info.ParameterE, ex.Message));
            }
        }
    }
    
    
    public class ControlInfoDictionary : Dictionary<string, ControlInfo> {
        public void AddControlInfo(XmlReader reader)
        {
            ControlInfo info = new ControlInfo();
            try
            {
                string s = "";
                info.Name = reader.GetAttribute("Name");
                info.Text = reader.GetAttribute("Text");
                info.TextV = reader.GetAttribute("TextV");
                info.TextE = reader.GetAttribute("TextE");

                s = reader.GetAttribute("Lock");
                if (s != null) s = s.ToLower();
                info.Lock = "1" == s || "true" == s || "yes" == s || "ok" == s;
                Add(info.Name, info);
            }
            catch (Exception ex)
            {   
                throw new Exception(string.Format("AddReportParamInfo: Name:{0}, V:{1}, E:{2}, Exc:{3}",
                    info.Name, info.TextV, info.TextE, ex.Message));
            }
        }

        public void AddDynamicControlInfo(XmlReader reader)
        {
            ControlInfo info = new ControlInfo();

            try
            {
                string s = ""; int i = 0;

                info.Name = reader.GetAttribute("Name");
                info.Text = reader.GetAttribute("Text");
                info.TextV = reader.GetAttribute("TextV");
                info.TextE = reader.GetAttribute("TextE");

                s = reader.GetAttribute("Lock");
                if (!string.IsNullOrEmpty(s)) s = s.ToLower();
                info.Lock = "1" == s || "true" == s || "yes" == s || "ok" == s;

                info.Type = reader.GetAttribute("Type") ?? "";

                s = reader.GetAttribute("Column");
                if (!int.TryParse(s, out i)) i = 0;
                info.Column = i;

                s = reader.GetAttribute("Row");
                if (!int.TryParse(s, out i)) i = 0;
                info.Row = i;

                s = reader.GetAttribute("AddX");
                if (!int.TryParse(s, out i)) i = 0;
                info.AddX = i;

                s = reader.GetAttribute("AddY");
                if (!int.TryParse(s, out i)) i = 0;
                info.AddY = i;

                s = reader.GetAttribute("SizeW");
                if (!int.TryParse(s, out i)) i = 0;
                info.SizeW = i;

                s = reader.GetAttribute("SizeH");
                if (!int.TryParse(s, out i)) i = 0;
                info.SizeH = i;

                s = reader.GetAttribute("MaxLength");
                if (!int.TryParse(s, out i)) i = 0;
                info.MaxLength = i;

                s = reader.GetAttribute("AutoSize");
                if (!string.IsNullOrEmpty(s)) s = s.ToLower();
                info.AutoSize = "1" == s || "true" == s || "yes" == s || "ok" == s;

                info.vVar = reader.GetAttribute("vVar");
                info.Key = reader.GetAttribute("Key");
                info.Key2 = reader.GetAttribute("Key2");
                info.Items = reader.GetAttribute("Items");
                info.Format = reader.GetAttribute("Format");

                info.ParentName = reader.GetAttribute("ParentName");
                info.Exe = reader.GetAttribute("Exe");

                Add(info.Name, info);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("AddReportParamInfo: Name:{0}, V:{1}, E:{2}, Exc:{3}",
                    info.Name, info.TextV, info.TextE, ex.Message));
            }
        }

    }

    //public class DynamicControlInfoDictionary : Dictionary<string, DynamicControlInfo>
    //{
    //}

    
    public class GridFormat : Info
    {
        //Name,TextV,TextE :Base
        public string Alignment { get; set; }
        public int DecimalPlace { get; set; }
        public string Format { get; set; }
        

        public string GetText(string lang)
        {
            if (lang == "E") return TextE;
            else return TextV;
        }
    }
    public class ReportParam : Info
    {
        public string ParameterField
        {
            get { return Name; }
            set { Name = value; }
        }
        public string ParameterV
        {
            get { return TextV; }
            set { TextV = value; }
        }
        public string ParameterE
        {
            get { return TextE; }
            set { TextE = value; }
        }
    }

    public class KeyDefine : Info
    {
        public string Type
        {
            get { return Name; }
            set { Name = value; }
        }
        public string Key
        {
            get { return Text; }
            set { Text = value; }
        }
        public string Key2
        {
            get { return TextV; }
            set { TextV = value; }
        }
        public string Key3
        {
            get { return TextE; }
            set { TextE = value; }
        }

        public void ReadKeys(XmlReader reader)
        {
            Type = reader.GetAttribute("Type");
            Key = reader.GetAttribute("Key");
            Key2 = reader.GetAttribute("Key2");
            Key3 = reader.GetAttribute("Key3");
        }
    }
    
    
    /// <summary>
    /// Dùng cho cả Control và DynamicControl
    /// </summary>
    public class ControlInfo : Info
    {
        public bool Lock { get; set; }
        public string GetText(string lang)
        {
            if (lang == "V")
                return TextV;
            else if (lang == "E")
                return TextE;
            else
                return Text;
        }

        // Dùng cho Dynamic controls
        public string Type { get; set; }
        public int Column { get; set; }
        public int AddX { get; set; }
        public int AddY { get; set; }
        public int Row { get; set; }
        public int SizeW { get; set; }
        public int SizeH { get; set; }
        public int MaxLength { get; set; }
        public bool AutoSize { get; set; }
        public string vVar { get; set; }
        public string Key { get; set; }
        public string Key2 { get; set; }
        public string Items { get; set; }
        public string Format { get; set; }
        
        //Dùng cho menu
        public string ParentName { get; set; }
        public string Exe { get; set; }
    }

    public class Info
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public string TextV { get; set; }
        public string TextE { get; set; }
       
    }


    public class DynamicControlParamDictionary: List<DynamicControlParam>
    {
        public void AddParam(XmlReader reader)
        {
            DynamicControlParam param = new DynamicControlParam();
            try
            {
                param.Name = reader.GetAttribute("Name");
                param.ControlName = reader.GetAttribute("ControlName");
                Add(param);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("AddReportParamInfo: {0}, {1}, Exc:{2}",
                    param.Name, param.ControlName, ex.Message));
            }
        }
    }
    public class DynamicControlParam
    {
        public string Name { get; set; }
        public string ControlName { get; set; }
    }

    public class ReportInfo
    {
        public ReportInfo() { }
        public ReportInfo(string reportFile, string reportProc, string pVoucherList)
        {
            ReportFile = reportFile;
            ReportProc = reportProc;
            PVoucherList = pVoucherList;
        }

        public void ReadReportInfo(XmlTextReader reader)
        {
            //int i = 0;
            //System.Windows.Forms.MessageBox. Show("Test " + ++i);
            DefaultGroup = reader.GetAttribute("DefaultGroup");
            //System.Windows.Forms.MessageBox. Show("Test " + ++i);
            DocumentFilter = reader.GetAttribute("DocumentFilter");
            //System.Windows.Forms.MessageBox. Show("Test " + ++i);
            GroupType = reader.GetAttribute("GroupType");
            //System.Windows.Forms.MessageBox. Show("Test " + ++i);
            GroupTable = reader.GetAttribute("GroupTable");
            //System.Windows.Forms.MessageBox. Show("Test " + ++i);
            GroupIDField = reader.GetAttribute("GroupIDField");
            //System.Windows.Forms.MessageBox. Show("Test " + ++i);
            GroupKeyField = reader.GetAttribute("GroupKeyField");
            //System.Windows.Forms.MessageBox. Show("Test " + ++i);
            GroupSelect = reader.GetAttribute("GroupSelect");
            //System.Windows.Forms.MessageBox. Show("Test " + ++i);
            ReportFile = reader.GetAttribute("ReportFile");
            //System.Windows.Forms.MessageBox. Show("Test " + ++i);
            ReportProc = reader.GetAttribute("ReportProc");
            //System.Windows.Forms.MessageBox. Show("Test " + ++i);
            ReportProc2 = reader.GetAttribute("ReportProc2");
            //System.Windows.Forms.MessageBox. Show("Test " + ++i);
            //System.Windows.Forms.MessageBox. Show("Test " + ++i);
            int t = 60;
            int.TryParse(reader.GetAttribute("RefreshTime"), out t);
            RefreshTime = t;
            //System.Windows.Forms.MessageBox. Show("Test " + ++i);
            ProcVar1 = reader.GetAttribute("ProcVar1");
            //System.Windows.Forms.MessageBox. Show("Test " + ++i);
            ProcVar2 = reader.GetAttribute("ProcVar2");
            //System.Windows.Forms.MessageBox. Show("Test " + ++i);
            PVoucherList = reader.GetAttribute("pVoucherList");
            //System.Windows.Forms.MessageBox. Show("Test " + ++i);
            Mavtdk = reader.GetAttribute("Mavtdk");
            //System.Windows.Forms.MessageBox. Show("Test " + ++i);
            Mavtbh = reader.GetAttribute("Mavtbh");
            //System.Windows.Forms.MessageBox. Show("Test " + ++i);
            Nhom_pk = reader.GetAttribute("Nhom_pk");

            //System.Windows.Forms.MessageBox. Show("Test " + ++i);

            string s = reader.GetAttribute("Variables")??"";
            string[] ss0 = s.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            Variables = new Dictionary<string, string>();
            foreach (var item in ss0)
            {
                string[] ss1 = item.Split(':');
                if (ss1.Length == 2 && !Variables.ContainsKey(ss1[0]))
                {
                    Variables.Add(ss1[0], ss1[1]);
                }
            }

               // .ToList
               //.Select(part => part.Split('='))
               //.ToDictionary(split => split[0], split => split[1]);
        }

        public string DefaultGroup { get; private set; }
        public string DocumentFilter { get; private set; }
        public string GroupType { get; private set; }
        public string GroupTable { get; private set; }
        public string GroupIDField { get; private set; }
        public string GroupKeyField { get; private set; }
        public string GroupSelect { get; private set; }
        public string ProcVar1 { get; private set; }
        public string ProcVar2 { get; private set; }
        public int RefreshTime { get; set; }
        /// <summary>
        /// Tên tập tin .rpt
        /// </summary>
        [DefaultValue("")]
        public string ReportFile { get; private set; }

        /// <summary>
        /// Tên Stored Proceduce trong sql
        /// </summary>
        [DefaultValue("")]
        public string ReportProc { get; private set; }

        /// <summary>
        /// Tên Stored Proceduce trong sql
        /// </summary>
        [DefaultValue("")]
        public string ReportProc2 { get; private set; }

        /// <summary>
        /// pVoucherList...
        /// </summary>
        [DefaultValue("")]
        public string PVoucherList { get; private set; }
        [DefaultValue("")]
        public string Mavtdk { get; private set; }
        [DefaultValue("")]
        public string Mavtbh { get; private set; }
        [DefaultValue("")]
        public string Nhom_pk { get; private set; }
        public Dictionary<string, string> Variables { get; private set; }
    }
}
