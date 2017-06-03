using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace V6MDBtoSQLthread
{
    class Setting
    {
        private string SettingIniFileName;
        private System.Data.DataTable SettingTable;

        /// <summary>
        /// Khởi tạo đối tượng lưu giữ setting
        /// </summary>
        /// <param name="iniFile">Nên sử dụng đường dẫn đầy đủ</param>
        public Setting(string iniFile)
        {
            this.SettingIniFileName = iniFile;
            LoadSetting();
        }

        public bool SetSetting(string Name, string Value)
        {
            Name = Name.Trim();
            try
            {
                if (SettingTable.Columns.Contains(Name))
                {
                    SettingTable.Rows[0][Name] = Value;
                }
                else
                {
                    SettingTable.Columns.Add(Name);
                    SettingTable.Rows[0][Name] = Value;
                }
                SaveSetting();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public string GetSetting(string Name)
        {
            string s = null;
            if (SettingTable.Columns.Contains(Name))
                s = SettingTable.Rows[0][Name].ToString().Trim();
            return s;
        }

        private void LoadSetting()
        {
            SettingTable = new System.Data.DataTable();
            SettingTable.Rows.Add(SettingTable.NewRow());//Chỉ add 1 dòng duy nhất!
            if(!File.Exists(this.SettingIniFileName))
                return;
            FileStream fs = new FileStream(this.SettingIniFileName, FileMode.Open);
            StreamReader sr = new StreamReader(fs);            
            try
            {
                //fs = new FileStream(this.SettingIniFileName, FileMode.Open);
                //sr = new StreamReader(fs);

                string s = "";                          // Đọc 1 dòng
                string[] ss;                            // Tách
                while (!sr.EndOfStream)                 // Khi chưa đọc hết
                {
                    s = sr.ReadLine().Trim();           // Lấy 1 dòng
                    if (s.StartsWith(";")) continue;    // Kiểm tra
                    ss = s.Split(';')[0].Split('=');    // Tách name value
                    try
                    {
                        if (ss.Length >= 2)             // Kiểm tra có name && value
                        {
                                                        // Làm việc với name && value
                            if(SettingTable.Columns.Contains(ss[0].Trim()))
                                continue;
                                                        // Tạo và lưu value vào cột mới (name)
                            SettingTable.Columns.Add(ss[0]);
                            SettingTable.Rows[0][ss[0]] = ss[1];
                        }
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

        private void SaveSetting()
        {
            FileStream fs = new FileStream(SettingIniFileName,FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            try
            {
                sw.WriteLine(";V6Sync setting");
                sw.WriteLine(";V6Soft-Anhh");
                string line = "";
                for (int i = 0; i < SettingTable.Columns.Count; i++)
                {
                    try
                    {
                        line = SettingTable.Columns[i].ColumnName
                                        + "="
                                        + SettingTable.Rows[0][i].ToString();

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
