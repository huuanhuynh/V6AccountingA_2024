using System;
using Microsoft.Win32;

namespace HaUtility.Helper
{
    public static class RegistryHelper
    {
        /// <summary>
        /// Đọc giá trị lưu trong registry
        /// </summary>
        /// <param name="productName">Tên định danh chương trình</param>
        /// <param name="KeyName">Tên biến trong registry)</param>
        /// <returns>Giá trị kiểu chuỗi</returns>
        public static string ReadRegistry(string productName, string KeyName)
        {
            if (KeyName != "")
            {
                // Opening the registry key
                RegistryKey rk = Registry.CurrentUser;
                // Open a subKey as read-only
                RegistryKey sk1 = rk.OpenSubKey(@"SOFTWARE\" + productName);
                // If the RegistrySubKey doesn""t exist -> (null)
                if (sk1 == null)
                {
                    return null;
                }
                else
                {
                    try
                    {
                        // If the RegistryKey exists I get its value
                        // or null is returned.
                        return (string)sk1.GetValue(KeyName.ToUpper());
                    }
                    catch (Exception e)
                    {
                        throw new Exception("UtilityHelper.ReadRegistry : " + e.Message);
                    }
                }
            }
            else
            {
                throw new ArgumentException("UtilityHelper.ReadRegistry : kiểm tra lại tham số");
            }
        }

        public static bool CheckRegistry(string productName, string KeyName)
        {
            if (KeyName != "")
            {
                // Opening the registry key
                RegistryKey rk = Registry.CurrentUser;
                // Open a subKey as read-only
                RegistryKey sk1 = rk.OpenSubKey(@"SOFTWARE\" + productName);
                // If the RegistrySubKey doesn""t exist -> (null)
                if (sk1 == null)
                {
                    return false;
                }
                else
                {
                    try
                    {
                        // If the RegistryKey exists I get its value
                        // or null is returned.
                        return true;
                    }
                    catch (Exception e)
                    {
                        throw new Exception("UtilityHelper.ReadRegistry : " + e.Message);
                    }
                }
            }
            else
            {
                throw new ArgumentException("UtilityHelper.ReadRegistry : kiểm tra lại tham số");
            }
        }

        /// <summary>
        /// Ghi giá trị vào Registry để thiết lập biến ngôn ngữ
        /// </summary>
        /// <param name="productName">Tên định danh chương trình</param>
        /// <param name="KeyName">Tên biến chứa giá trị</param>
        /// <param name="Value">Giá trị cần thiết lập</param>
        /// <returns>Trạng thái thiết lập (true - false)</returns>
        public static bool WriteRegistry(string productName, string KeyName, string Value)
        {
            if (KeyName != "" && Value != "")
            {
                try
                {
                    // Setting
                    RegistryKey rk = Registry.CurrentUser; //Ghi vào thẻ CURRENT_USER
                    // I have to use CreateSubKey 
                    // (create or open it if already exits), 
                    // ""cause OpenSubKey open a subKey as read-only
                    RegistryKey sk1 = rk.CreateSubKey(@"SOFTWARE\" + productName);
                    // Save the value
                    if (sk1 != null) sk1.SetValue(KeyName.ToUpper(), Value);
                    return true;
                }
                catch (Exception e)
                {
                    Logger.WriteToLog("UtilityHelper.WriteRegistry :" + e.Message);
                    return false;
                }
            }
            else
            {
                throw new ArgumentException("UtilityHelper.WriteRegistry : kiểm tra lại tham số");
            }
        }

        /// <summary>
        /// Kiểm tra xem chương trình có được đăng ký vào startup của windows ?
        /// </summary>
        /// <param name="programName">Tên chương trình</param>
        /// <returns>True - false</returns>
        public static bool CheckProgramStartUp(string programName)
        {
            // The path to the key where Windows looks for startup applications
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (rkApp.GetValue(programName) == null)
            {
                // The value doesn't exist, the application is not set to run at startup
                return false;
            }
            else
            {
                // The value exists, the application is set to run at startup
                return true;
            }
        }

        /// <summary>
        /// Set hoặc unset chương trình trong registry
        /// </summary>
        /// <param name="programName">Tên chương trình</param>
        /// <param name="isRunAtStartup">Tham số để set startup (true - false)</param>
        /// <param name="ApplicationExecutablePath"></param>
        public static void SetOrUnSetStartUpProgram(string programName, bool isRunAtStartup, string ApplicationExecutablePath)
        {
            // The path to the key where Windows looks for startup applications
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (isRunAtStartup)
            {
                // Add the value in the registry so that the application runs at startup
                rkApp.SetValue(programName, ApplicationExecutablePath);
            }
            else
            {
                // Remove the value from the registry so that the application doesn't start
                rkApp.DeleteValue(programName, false);
            }
        }

    }
}
