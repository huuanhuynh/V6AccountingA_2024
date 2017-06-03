using System;
using System.Collections.Generic;
using System.Text;

namespace V6ThreadLibrary
{
    public class SmsSetting : H.Setting
    {

        public SmsSetting(string iniFileName)
            : base(iniFileName)
        {
            //Nếu không gọi base có thể viết lại như sau
            //this.SettingIniFileName = iniFileName;
            //this.LoadSetting();
        }

        public void ReloadSetting()
        {
            base.LoadSetting();
        }

        //public string DataType
        //{
        //    get { return GetSetting("DataType"); }
        //    set { SetSetting("DataType", value); }
        //}

        /// <summary>
        /// Dữ liệu gửi tin nhắn
        /// </summary>
        public string DataFile
        {
            get { return GetSetting("DataFile"); }
            set { SetSetting("DataFile", value); }
        }
        //public string Data1
        //{
        //    get { return GetSetting("Data1"); }
        //    set { SetSetting("Data1", value); }
        //}
        //public string Data2
        //{
        //    get { return GetSetting("Data2"); }
        //    set { SetSetting("Data2", value); }
        //}
        
        /// <summary>
        /// Thời gian thực hiện gửi
        /// </summary>
        public string SendTimeHHMM
        {
            get { return GetSetting("SendTimeHHMM"); }
            set { SetSetting("SendTimeHHMM", value); }
        }

        public string LastRunDate
        {
            get { return GetSetting("LastRunDate"); }
            set { SetSetting("LastRunDate", value); }
        }

        /// <summary>
        /// Số lượng sheet
        /// </summary>
        public int NumOfSheet
        {
            get
            {
                int i = 0;
                int.TryParse(GetSetting("NumOfSheet"), out i);
                return i;
            }
            set { SetSetting("NumOfSheet", value.ToString()); }
        }
        ///// <summary>
        ///// Thời gian nghĩ _ giây
        ///// </summary>
        //public int SleepTime_s
        //{
        //    get
        //    {
        //        int i = 0;
        //        int.TryParse(GetSetting("SleepTime_s"), out i);
        //        return i;
        //    }
        //    set { SetSetting("SleepTime_s", value.ToString()); }
        //}
               
        public string EmailSender
        {
            get { return GetSetting("EmailSender"); }
            set { SetSetting("EmailSender", value); }
        }

        public string EmailPassword
        {
            get { return GetSetting("EmailPassword"); }
            set { SetSetting("EmailPassword", value); }
        }

        public string EmailSubject
        {
            get { return GetSetting("EmailSubject"); }
            set { SetSetting("EmailSubject", value); }
        }

        public string PortName
        {
            get { return GetSetting("PortName"); }
            set { SetSetting("PortName", value); }
        }

        public string From
        {
            get { return GetSetting("From"); }
            set { SetSetting("From", value); }
        }
        public string To
        {
            get { return GetSetting("To"); }
            set { SetSetting("To", value); }
        }

        public string XlsTemplate
        {
            get { return GetSetting("XlsTemplate"); }
            set { SetSetting("XlsTemplate", value); }
        }

        public string SendFileName
        {
            get { return GetSetting("SendFileName"); }
            set { SetSetting("SendFileName", value); }
        }
    }
}
