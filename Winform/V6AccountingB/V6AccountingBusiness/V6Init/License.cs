using System;
using System.IO;
using System.Management;
using System.Net;
using V6Tools;

namespace V6Init
{
    public static class License
    {
        /// <summary>
        /// Lấy serial ổ đĩa
        /// </summary>
        /// <param name="drive">C</param>
        /// <returns></returns>
        public static string GetVolumeSerialNumber(string drive)
        {
            try
            {
                ManagementObject disk =
                    new ManagementObject("win32_logicaldisk.deviceid=\"" + drive + ":\"");
                disk.Get();
                var a = disk["VolumeSerialNumber"].ToString();

                return a;
            }
            catch
            {
                return "";
            }
        }
        private static string _path = "";
        public static string Seri = "";
        public static string Key = "";
        /// <summary>
        /// Tạo số Seri chương trình.
        /// </summary>
        /// <param name="path"></param>
        public static string GetSeri(string path)
        {
            _path = path;
            var result = "";
            try
            {
                path = Path.GetFullPath(path);
                var path_root = Path.GetPathRoot(path);
                var volume_seri = GetVolumeSerialNumber(path_root.Substring(0, 1));
                result = UtilityHelper.EnCrypt(volume_seri + path);
                result = ConvertStringToHex(result);
            }
            catch
            {
                
            }
            Seri = result;
            return result;
        }

        public static bool CheckLicenseKey(string seri, string key)
        {
            try
            {
                Seri = seri;
                Key = key;
                var seri0 = ConvertHexToString(seri);
                var mahoa_seri0 = UtilityHelper.EnCrypt(seri0);
                var key0 = ConvertHexToString(key);
                var result = mahoa_seri0 == key0;
                if (result)
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog(ex.Message);
            }
            return false;
        }

        public static string ReadLicenseKey(string dir)
        {
            string result = "";
            string name = "V6license.key";
            var fullFileName = Path.Combine(dir, name);
            if (File.Exists(fullFileName))
            {
                FileStream fs = new FileStream(fullFileName, FileMode.Open);
                try
                {
                    StreamReader sr = new StreamReader(fs);
                    result = sr.ReadLine();
                }
                catch
                {
                    result = "";
                }
                fs.Close();
            }
            return result;
        }
        public static bool WriteLicenseKey(string key)
        {
            string fileName = "V6license.key";
            {
                FileStream fs = new FileStream(fileName, FileMode.Create);
                try
                {
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine(key);
                    sw.Close();
                }
                catch
                {
                    fs.Close();
                    return false;
                }                
            }
            return true;
        }

        public static string ConvertStringToHex(string asciiString)
        {
            string hex = "";
            foreach (char c in asciiString)
            {
                int tmp = c;
                hex += String.Format("{0:X}", (uint)tmp);
            }
            return hex;
        }

        public static string ConvertHexToString(string HexValue)
        {
            string StrValue = "";
            while (HexValue.Length > 0)
            {
                try
                {
                    StrValue += System.Convert.ToChar(System.Convert.ToUInt32(HexValue.Substring(0, 2), 16)).ToString();
                    HexValue = HexValue.Substring(2, HexValue.Length - 2);
                }
                catch //(Exception)
                {
                    return "";
                }
            }
            return StrValue;
        }

    }
}
