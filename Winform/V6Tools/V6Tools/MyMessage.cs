using System;
using System.Collections.Generic;
using System.Xml;

namespace V6Tools
{
    /// <summary>
    /// Class đối tượng 'MyMessage' (thông báo riêng) để việc thay đổi các thông báo
    /// trên chương trình một cách dễ dàng, các thông báo được định nghĩa dưới file xml
    /// và được đối tượng MyMessage lấy lên theo key là 'MessageName'
    /// </summary>
    public class MyMessage : Dictionary<string,Vm>
    {
        string _xml = "MyMessage.xml";
        string _lang = "V";
        /// <summary>
        /// V: Việt Nam, E: English
        /// </summary>
        public string messageLang {
            get
            {
                return _lang;
            }
            set
            {
                _lang = value;
                ResetML();
            }
        }
        //V6MessageDictionary m_MessageDic;

               
        public MyMessage(string XmlFile, string lang):base()
        {
            _xml = XmlFile;
            _lang = lang;
            ReadXml();
        }
        private void ReadXml()
        {
            XmlTextReader reader = new XmlTextReader(_xml);
            this.M = new SortedList<string, string>();
            try
            {
                while (reader.Read())
                {
                    if(reader.Name == "MyMessage")
                    {
                        Vm m = new Vm();
                        m.Name = reader.GetAttribute("MessageName");
                        m.MessageV = reader.GetAttribute("MessageV");
                        m.MessageE = reader.GetAttribute("MessageE");
                        //m_MessageDic.Add(m.Name, m);
                        this.Add(m.Name, m);
                        if (_lang == "E")
                            M.Add(m.Name, m.MessageE);
                        else M.Add(m.Name, m.MessageV);
                    }
                }
                reader.Close();
            }
            catch (Exception)
            {
                reader.Close();
                throw;
            }
        }
        /// <summary>
        /// Lấy Message theo ngôn ngữ đã định với từ khóa MessageName
        /// </summary>
        /// <param name="name">từ khóa</param>
        /// <returns>Message?</returns>
        public string getMyMessage(string name)
        {
            return getMyMessage(name, _lang);
        }
        public string gM(string name)
        {
            return getMyMessage(name, _lang);
        }
        
        /// <summary>
        /// Lấy message theo ngôn ngữ mong muốn
        /// </summary>
        /// <param name="name"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public string getMyMessage(string name, string lang)
        {
            //return getMyMessage(MessageName, Lang, xmlFileName);
            if (lang == "E")
                return GetMessage(name, V6Tools.Lang.E);
            else
                return GetMessage(name, V6Tools.Lang.V);
        }

        
        public string GetMessage(string name, Lang lang)
        {
            if(this.ContainsKey(name))
            {
                Vm m = this[name];
                if (lang == Lang.V)
                    return m.MessageV;
                else if (lang == Lang.E)
                    return m.MessageE;
                else return m.MessageV;
            }
            else
            {
                return name;
            }
        }

        /// <summary>
        /// Lấy Message theo ngôn ngữ đã định dựa theo key M[string key]
        /// </summary>
        public SortedList<string, string> M { get; private set; }
        private void ResetML()
        {
            this.M = new SortedList<string, string>();
            if (_lang == "E")
            {
                foreach (var item in this)
                {
                    M.Add(item.Key, item.Value.MessageE);
                }
            }
            else
            {
                foreach (var item in this)
                {
                    M.Add(item.Key, item.Value.MessageV);
                }
            }
        }

        public string Close { get { return M[MName.Close]; } }
        public string CloseQ { get { return M[MName.CloseQ]; } }
        public string DataLoading { get { return M[MName.DataLoading]; } }
        public string Exit { get { return M[MName.Exit]; } }
        public string ExitQ { get { return M[MName.ExitQ]; } }
        public string MainExit { get { return M[MName.MainExit]; } }
        public string MainExitQ { get { return M[MName.MainExitQ]; } }
        public string Finish { get { return M[MName.Finish]; } }
        public string ExportFinish { get { return M[MName.ExportFinish]; } }
        public string ExportFail { get { return M[MName.ExportFail]; } }
        public string ExportToExcel { get { return M[MName.ExportToExcel]; } }
        public string Error { get { return M[MName.Error]; } }
        public string FormatGridError { get { return M[MName.FormatGridError]; } }
        public string GridColumn { get { return M[MName.GridColumn]; } }
        public string NoData { get { return M[MName.NoData]; } }
        public string NoDefine { get { return M[MName.NoDefine]; } }
        public string ConnectionError { get { return M[MName.ConnectionError]; } }
        public string QueryingError { get { return M[MName.QueryingError]; } }
        public string ReportError { get { return M[MName.ReportError]; } }
        public string XmlError { get { return M[MName.XmlError]; } }
        public string FormLoad { get { return M[MName.FormLoad]; } }
        public string SetLang { get { return M[MName.SetLang]; } }
        public string V6Soft { get { return M[MName.V6Soft]; } }
        public string PrintGrid { get { return M[MName.PrintGrid]; } }
        public string PrintAlignment { get { return M[MName.PrintAlignment]; } }
        public string NoExist { get { return M[MName.NoExist]; } }
        public string HardExit { get { return M[MName.HardExit]; } }
        public string FormClose { get { return M[MName.FormClose]; } }
        public string TrungNhom { get { return M[MName.TrungNhom]; } }
        public string LoiNhom { get { return M[MName.LoiNhom]; } }
        public string TmpTableErr { get { return M[MName.TmpTableErr]; } }
        public string Over31 { get { return M[MName.Over31]; } }
        /**=============File xml mẫu: MyMessage.xml================
         * 
<?xml version="1.0"?>
<Root>		
	<MyMessage	MessageName='Exit'	        MessageV='Thoát chương trình?'	    MessageE='Exit program?'    />
	<MyMessage	MessageName='ExportFinish'	MessageV='Xuất file hoàn tất!'	    MessageE='Export finish!'   />
	<MyMessage	MessageName='ExportToExcel'	MessageV='Xuất ra Excel! '			MessageE='Export to Excel! '/>
	<MyMessage	MessageName='NoData'		MessageV='Không có dữ liệu!'		MessageE='No data!'			/>
	<MyMessage	MessageName='DataError'		MessageV='Lỗi truy vấn dữ liệu'		MessageE='Error querying data'	/>
	<MyMessage	MessageName='ReportError'	MessageV='Lỗi tạo báo cáo!'			MessageE='Make report error!'	/>
	<MyMessage	MessageName='XmlError'		MessageV='Lỗi xml'		            MessageE='Xml error'        />
	<MyMessage	MessageName='FormLoad'		MessageV='Khởi động form: '			MessageE='Form load: '		/>
	<MyMessage	MessageName='SetLang'		MessageV='Thiết lập ngôn ngữ: '		MessageE='Set language: '	/>
	<MyMessage	MessageName='PrintGrid'		MessageV='In bảng thô'				MessageE='Print grid'		/>
	<MyMessage	MessageName='Print'	        MessageV='Bạn có muốn...'		    MessageE='Do you want...age'/>
	<MyMessage	MessageName='PrintFinish'	MessageV='In xong'					MessageE='Print grid finish'/>	
</Root>	
         *
         *  Ngoài MessageV, MessageE cũng có thể tạo thêm các Message?[*]
         * */

    }

    public enum Lang
    { V,E }

    /// <summary>
    /// Danh sách các MessageName đã được hỗ trợ.
    /// </summary>
    public static class MName
    {
        public const string Close = "Close";
        public const string CloseQ = "CloseQ";
        public const string DataLoading = "DataLoading";
        public const string Exit = "Exit";
        public const string ExitQ = "ExitQ";
        public const string MainExit = "MainExit";
        public const string MainExitQ = "MainExitQ";
        public const string Finish = "Finish";
        public const string ExportFinish = "ExportFinish";
        public const string ExportFail = "ExportFail";
        public const string ExportToExcel = "ExportToExcel";
        public const string Error = "Error";
        public const string FormatGridError = "FormatGridError";
        public const string GridColumn = "GridColumn";
        public const string NoData = "NoData";
        public const string NoDefine = "NoDefine";
        public const string ConnectionError = "ConnectionError";
        public const string QueryingError = "QueryingError";
        public const string ReportError = "ReportError";
        public const string XmlError = "XmlError";
        public const string FormLoad = "FormLoad";
        public const string SetLang = "SetLang";
        public const string V6Soft = "V6Soft";
        public const string PrintGrid = "PrintGrid";
        public const string PrintAlignment = "PrintAlignment";
        public const string NoExist = "NoExist";
        public const string HardExit = "HardExit";
        public const string FormClose = "FormClose";
        public const string TrungNhom = "TrungNhom";
        public const string LoiNhom = "LoiNhom";
        public const string TmpTableErr = "TmpTableErr";
        public const string Over31 = "Over31";
    }

    public class V6MessageDictionary:Dictionary<string,Vm>
    {
        public bool AddMessage(Vm m)
        {
            try
            {
                if (m != null)
                {
                    this.Add(m.Name, m);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }

    public class Vm
    {
        public string Name { get; set; }
        public string MessageV { get; set; }
        public string MessageE { get; set; }
    }
}
