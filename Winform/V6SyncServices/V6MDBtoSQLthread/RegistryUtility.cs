using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace V6MDBtoSQLthread
{
    public class RegistryUtility
    {

        //HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Run
        public static void SetRunForCurrentUser(string ProductName, string ExecutablePath, bool Run)
        {
            RegistryKey rk = Registry.CurrentUser
                .OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            try
            {
                if (Run)
                {                    
                    rk.SetValue(ProductName, ExecutablePath);
                }
                else
                {
                    rk.DeleteValue(ProductName, false);
                }
            }
            catch
            {
                throw;
            }
        }
        //HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Run
        public static void SetRunForLocalMachine(string ProductName, string ExecutablePath, bool Run)
        {
            RegistryKey rk = Registry.LocalMachine// .CurrentUser
                .OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            try
            {
                if (Run)
                {
                    rk.SetValue(ProductName, ExecutablePath);
                }
                else
                {
                    rk.DeleteValue(ProductName, false);
                }
            }
            catch
            {
                throw;
            }
        }

        public static string ReadRegistry(string ProductName, string KeyName)
        {
            if (KeyName != "")
            {
                // Mở một subkey để đọc
                RegistryKey rk = Registry.CurrentUser
                    .OpenSubKey(@"SOFTWARE\"+ProductName);
                // Nếy subkey không tồn tại sẽ trả về null
                if (rk == null)
                {
                    return null;
                }
                else
                {
                    try
                    {
                        // Nếu tồn tại giá trị thì lấy giá trị đó.
                        // hoặc trả về null.
                        return (string)rk.GetValue(KeyName.ToUpper());
                    }
                    catch (Exception e)
                    {
                        throw new Exception("RegistryU.ReadRegistry : " + e.Message);
                    }
                }
            }
            else
            {
                throw new ArgumentException
                    ("RegistryU.ReadRegistry : kiểm tra lại tham số");
            }
        }

        /// <summary>
        /// Ghi một RegValue vào HKEY_LOCAL_MACHINE\SOFTWARE\(ProductName)
        /// </summary>
        /// <param name="ProductName">Tên khóa</param>
        /// <param name="KeyName"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static bool WriteRegistry(string ProductName, string KeyName, string Value)
        {
            if (KeyName != "" && Value != "")
            {
                try
                {
                    //Tạo mới hoặc mở subkey để ghi giá trị
                    RegistryKey rk = Registry.CurrentUser
                        .CreateSubKey(@"SOFTWARE\"+ProductName);
                    rk.SetValue(KeyName.ToUpper(), Value);
                    return true;
                }
                catch// (Exception e)
                {
                    return false;
                }
            }
            else
            {
                throw new ArgumentException
                    ("RegistryU.WriteRegistry : kiểm tra lại tham số");
            }
        }

    }
}
