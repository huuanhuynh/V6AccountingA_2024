using System;
using System.Collections.Generic;
using System.IO;

namespace H
{
    public class Setting
    {
        protected string SettingIniFileName = "Setting.ini";
        //private System.Data.DataTable SettingTable;
        private SortedDictionary<string, string> SettingData;
        private SortedDictionary<string, string> SettingComment;
        public bool _autoSave = false;
        private bool _haveChange = false;
        /// <summary>
        /// Khởi tạo đối tượng lưu giữ setting
        /// </summary>
        /// <param name="iniFileName">Nên sử dụng đường dẫn đầy đủ</param>
        public Setting(string iniFileName)
        {
            this.SettingIniFileName = iniFileName;
            LoadSetting();
        }
        public Setting()
        {
            LoadSetting();
        }

        public bool SetSetting(string Name, string Value, string comment = null)
        {
            Name = Name.Trim();
            try
            {
                SettingData[Name] = Value;
                if (comment != null)
                {
                    SettingComment[Name] = comment;
                }
                
                if(_autoSave)
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

        public string GetComment(string Name)
        {
            string s = "";
            if (SettingComment.ContainsKey(Name))
                s = SettingData[Name].Trim();

            return s;
        }

        protected void LoadSetting()
        {
            SettingData = new SortedDictionary<string, string>();
            SettingComment = new SortedDictionary<string, string>();
            
            if(!File.Exists(this.SettingIniFileName)) return;

            FileStream fs = new FileStream(this.SettingIniFileName, FileMode.Open);
            StreamReader sr = new StreamReader(fs);            
            try
            {
                while (!sr.EndOfStream)
                {
                    var line = (sr.ReadLine()??"").Trim();              // Đọc 1 dòng
                    if (line.StartsWith(";")) continue;                 // Kiểm tra
                    var info_comment = line.Split(new[] { ';' }, 2);
                    var info = info_comment[0];                         // Chuỗi chứa thông tin
                    var comment = info_comment.Length > 1 ? info_comment[1] : "";
                    var firstIndex = info.IndexOf('=');                 // Vị trí dấu =
                    if (firstIndex<1) continue;
                    var name = info.Substring(0, firstIndex);
                    var value = info.Substring(firstIndex + 1);
                    
                    SettingData[name] = value;
                    SettingComment[name] = comment;
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
                        line = string.Format("{0}={1}", item.Key, item.Value ?? "");
                        if (SettingComment.ContainsKey(item.Key) && !string.IsNullOrEmpty(SettingComment[item.Key]))
                        {
                            line += ";" + SettingComment[item.Key];
                        }
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
