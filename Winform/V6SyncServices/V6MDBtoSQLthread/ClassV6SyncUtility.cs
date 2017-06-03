using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace V6MDBtoSQLthread
{
    class V6SyncUtility
    {
        #region ==== Registry ====

       

        public static void StartRunOnStartUp()
        {
            try
            {
                RegistryUtility.SetRunForCurrentUser(
                    Application.ProductName, Application.ExecutablePath, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void StopRunOnStartUp()
        {
            try
            {
                RegistryUtility.SetRunForCurrentUser(
                    Application.ProductName, Application.ExecutablePath, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void StartRunForLocalMachineOnStartUp()
        {
            try
            {
                RegistryUtility.SetRunForLocalMachine(
                    Application.ProductName, Application.ExecutablePath, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void StopRunForLocalMachineOnStartUp()
        {
            try
            {
                RegistryUtility.SetRunForLocalMachine(
                    Application.ProductName, Application.ExecutablePath, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //const string _Date_num = "Date_num";
        //public static void SetDate_numReg(int date_num)
        //{
        //    try
        //    {
        //        RegistryUtility.WriteRegistry(
        //            Application.ProductName, _Date_num, date_num.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        //public static int GetDate_num()
        //{
        //    try
        //    {
        //        return int.Parse(RegistryUtility.ReadRegistry(Application.ProductName, _Date_num));
        //    }
        //    catch
        //    {
        //        SetSleepTimeReg(10);
        //        return 10;
        //    }
        //}

        //const string _SleepTime = "SleepTime";
        //public static void SetSleepTimeReg(int secs)
        //{
        //    try
        //    {
        //        RegistryUtility.WriteRegistry(
        //            Application.ProductName, _SleepTime, secs.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        //public static int GetSleepTime()
        //{
        //    try
        //    {
        //        return int.Parse(RegistryUtility.ReadRegistry(Application.ProductName, _SleepTime));
        //    }
        //    catch
        //    {
        //        SetSleepTimeReg(10);
        //        return 10;
        //    }
        //}

        const string _RunOnStartUpKey = "RunOnStartUp";
        public static void SetRunOnStartUp(bool value)
        {
            try
            {
                RegistryUtility.WriteRegistry(
                    Application.ProductName, _RunOnStartUpKey, value.ToString());
                if (CheckRunOnStartUp())
                {
                    StartRunOnStartUp();
                }
                else
                {
                    StopRunOnStartUp();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void SetRunForLocalMachineOnStartUp(bool value)
        {
            try
            {   
                if (value == true)
                {
                    StartRunForLocalMachineOnStartUp();
                }
                else
                {
                    StopRunForLocalMachineOnStartUp();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static bool CheckRunOnStartUp()
        {
            try
            {
                if (RegistryUtility.ReadRegistry(Application.ProductName, _RunOnStartUpKey)
                    == true.ToString())
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //const string _RunThreadOnAppStart = "RunThreadOnAppStart";
        //public static void SetRunThreadOnAppStart(bool value)
        //{
        //    try
        //    {
        //        RegistryUtility.WriteRegistry(
        //            Application.ProductName, _RunThreadOnAppStart, value.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        //public static bool CheckRunThreadOnAppStart()
        //{
        //    try
        //    {
        //        if (RegistryUtility.ReadRegistry(Application.ProductName, _RunThreadOnAppStart)
        //            == true.ToString())
        //            return true;
        //        else
        //            return false;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        const string _ShowOnStartUpKey = "ShowOnStartUp";
        public static bool CheckShowOnStartUp()
        {
            try
            {
                if (RegistryUtility.ReadRegistry(Application.ProductName, _ShowOnStartUpKey)
                    == true.ToString())
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return true;
            }

        }
        public static bool SetShowOnStartUp(bool value)
        {
            try
            {
                return RegistryUtility.WriteRegistry(
                    Application.ProductName, _ShowOnStartUpKey, value.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
                
        #endregion Registry

        #region ==== EnableSetting.ini ====
        public static void ReadAndSetEnableSetting(Control c, string safeSettingFileName)
        {
            if (!File.Exists(Application.StartupPath + "\\"+safeSettingFileName))
            {
                CreateFileEnableSetting(c,safeSettingFileName);
                return;//Chưa có chỉ cần tạo không cần đọc
            }

            FileStream fs = new FileStream(Application.StartupPath + "\\" + safeSettingFileName,
                                                                    FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            try
            {
                string s = "";
                string[] ss;
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine().Trim();
                    if (s.StartsWith(";")) continue;
                    ss = s.Split(';')[0].Split('=');
                    try
                    {
                        if (ss.Length >= 2)
                        {
                            FindAndSetControlEnable(c, ss[0].Trim(), ss[1].Trim() == "1");
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
            catch (Exception ex)
            {
                sr.Close();
                fs.Close();
                MessageBox.Show(ex.Message);
            }
        }
        static void FindAndSetControlEnable(Control c, string name, bool enable)
        {
            if (c.Name == name)
            {
                SetControlEnable(c, enable);
                return;
            }
            foreach (Control item in c.Controls)
            {
                FindAndSetControlEnable(item, name, enable);
            }
        }
        static void SetControlEnable(Control c, bool enable)
        {
            if (c.Name != "")
            {
                c.Enabled = enable;
            }
        }
        static void CreateFileEnableSetting(Control c, string safeSettingFileName)
        {
            string str = CreateAllEnableSettingString(c).Trim();
            FileStream fs = new FileStream(safeSettingFileName, FileMode.CreateNew);
            StreamWriter sw = new StreamWriter(fs);
            try
            {
                sw.Write(str);
                sw.Close();
                fs.Close();
            }
            catch
            {
                sw.Close();
                fs.Close();
            }
            //MessageBox.Show("CreateFileEnableSetting Xong" );
        }
        static string CreateAllEnableSettingString(Control c)
        {
            string strSetting = "";
            try
            {                
                {
                    if(c.Enabled)
                        strSetting += c.Name + "=1;\r\n";
                    else
                        strSetting += c.Name + "=0;\r\n";
                }
            }
            catch { }
            
            foreach (Control item in c.Controls)
            {
                strSetting +=
                CreateAllEnableSettingString(item);
            }
            return strSetting;
        }
        #endregion end EnableSetting.ini
    }
}
