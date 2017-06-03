using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace HaUtility.Setting
{
    public class IniSetting
    {
        protected string SettingIniFileName = "Setting.ini";
        //private System.Data.DataTable SettingTable;
        private SortedDictionary<string, string> SettingData;

        [DefaultValue(false)]
        public bool AutoSave { get { return _autoSave; } set { _autoSave = value; } }
        private bool _autoSave = false;
        private bool _haveChange = false;
        /// <summary>
        /// Khởi tạo đối tượng lưu giữ setting
        /// </summary>
        /// <param name="iniFileName">Nên sử dụng đường dẫn đầy đủ</param>
        public IniSetting(string iniFileName)
        {
            this.SettingIniFileName = iniFileName;
            LoadSetting();
        }
        public IniSetting()
        {
            LoadSetting();
        }

        public bool SetSetting(string Name, string Value)
        {
            Name = Name.Trim();
            try
            {
                SettingData[Name] = Value;

                if (_autoSave)
                    Save();
                else
                    _haveChange = true;

                return true;
            }
            catch
            {
                return false;
            }
        }
        public string GetSetting(string Name)
        {
            string s = "";
            if (SettingData.ContainsKey(Name))
                s = SettingData[Name].Trim();
            else
                SetSetting(Name, s);

            return s;
        }

        protected void LoadSetting()
        {
            //SettingTable = new System.Data.DataTable();
            SettingData = new SortedDictionary<string, string>();
            //SettingTable.Rows.Add(SettingTable.NewRow());//Chỉ add 1 dòng duy nhất!
            if (!File.Exists(this.SettingIniFileName))
                return;
            FileStream fs = new FileStream(this.SettingIniFileName, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            try
            {
                string s = "";                          // Đọc 1 dòng
                string[] info_comment;
                string info = "";                       // Chuỗi chứa thông tin
                string comment = "";
                int firstIndex = 0;                     // Vị trí dấu =
                string name, value;
                //string[] ss;                            // Tách
                while (!sr.EndOfStream)                 // Khi chưa đọc hết
                {
                    s = sr.ReadLine().Trim();           // Lấy 1 dòng
                    if (s.StartsWith(";")) continue;    // Kiểm tra
                    info_comment = s.Split(new char[] { ';' }, 2);
                    info = info_comment[0];
                    if (info_comment.Length > 1) comment = info_comment[1];
                    else comment = "";
                    firstIndex = info.IndexOf('=');
                    if (firstIndex < 1) continue;
                    name = info.Substring(0, firstIndex);
                    value = info.Substring(firstIndex + 1);
                    //ss = s.Split(';')[0].Split('=');    // Tách name value, bỏ commend sau ;
                    try
                    {
                        if (SettingData.ContainsKey(name))
                            continue;
                        // Tạo và lưu value vào cột mới (name)
                        SettingData.Add(name, value);
                    }
                    catch
                    {
                        continue;
                    }
                }
                sr.Close();
                fs.Close();
            }
            catch (Exception)
            {
                sr.Close();
                fs.Close();
            }
        }

        public void SaveSetting()
        {
            if (_haveChange) Save();
        }

        private void Save()
        {
            FileStream fs = new FileStream(SettingIniFileName, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            try
            {
                sw.WriteLine(";Setting");
                sw.WriteLine(";Huuan_huynh");
                string line = "";
                foreach (var item in SettingData)
                {
                    try
                    {
                        line = item.Key + "=" + item.Value ?? "";
                        sw.WriteLine(line);
                    }
                    catch
                    {
                        continue;
                    }
                }

                sw.Close();
                fs.Close();
            }
            catch
            {
                sw.Close();
                fs.Close();
            }
        }
    }
}
