using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Management;
using V6AccountingBusiness;
using V6SqlConnect;
using V6Tools;
using V6Tools.V6Convert;

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

        public static string Seri = "";
        public static string Key = "";
        /// <summary>
        /// Tạo số Seri chương trình.
        /// </summary>
        /// <param name="path"></param>
        public static string GetSeri(string path)
        {
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

        public static string GetSeriClient()
        {
            var result = "";
            try
            {
                string checkcode = "";
                SqlParameter[] prList =
                {
                    new SqlParameter("@name", V6Login.ClientName), 
                    new SqlParameter("@seri", Seri), 
                    new SqlParameter("@key", Key), 
                };
                var data = SqlConnect.Select("V6ONLINES", "*", "name=@name and seri=@seri and [key]=@key", "", "", prList).Data;
                if (data != null && data.Rows.Count == 1)
                {
                    var row = data.Rows[0];
                    checkcode = row["CheckCode"].ToString().Trim();
                }
                result = UtilityHelper.EnCrypt(V6Login.ClientName + "0" + checkcode);
                //result = ConvertStringToHex(result);
            }
            catch
            {

            }
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

        public static bool CheckLicenseV6Online(string seri, string key)
        {
            try
            {
                SqlParameter[] prList =
                {
                    new SqlParameter("@name", V6Login.ClientName), 
                    new SqlParameter("@seri", seri), 
                    new SqlParameter("@key", key), 
                };
                var data = SqlConnect.Select("V6ONLINES", "*", "name=@name and seri=@seri and [key]=@key", "", "", prList).Data;
                if (data != null && data.Rows.Count == 1)
                {
                    var row = data.Rows[0];//.ToDataDictionary();

                    var seri0 = License.ConvertHexToString(seri);
                    var mahoa_seri0 = UtilityHelper.EnCrypt(seri0);
                    var key0 = License.ConvertHexToString(key);
                    var check_seri = mahoa_seri0 == key0;

                    var allow = 1 == ObjectAndString.ObjectToInt(row["ALLOW"]);
                    var eCodeName = (row["CODE_NAME"] ?? "").ToString().Trim();
                    var rCodeName = eCodeName == "" ? "" : UtilityHelper.DeCrypt(eCodeName);
                    //var allow = row["Allow"].ToString().Trim();
                    var checkCode = row["CHECKCODE"].ToString().Trim();
                    var is_allow =
                            allow
                            && rCodeName.Length > V6Login.ClientName.Length + 1
                            && rCodeName.StartsWith(V6Login.ClientName)
                            && rCodeName.Substring(V6Login.ClientName.Length, 1) == "1"
                            && rCodeName.EndsWith(checkCode);

                    return allow && check_seri && is_allow;
                }
                else
                {
                    InsertLicenseV6OnlineTemp(seri, key);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Check ok thi update
        /// </summary>
        /// <param name="seri"></param>
        /// <param name="key"></param>
        /// <param name="code_name"></param>
        /// <returns></returns>
        public static bool CheckNewLicenseV6Online(string seri, string key, string code_name)
        {
            try
            {
                SqlParameter[] prList =
                {
                    new SqlParameter("@name", V6Login.ClientName), 
                    new SqlParameter("@seri", seri), 
                    new SqlParameter("@key", key), 
                };
                var data = SqlConnect.Select("V6ONLINES", "*", "name=@name and seri=@seri and [key]=@key", "", "", prList).Data;
                if (data != null && data.Rows.Count == 1)
                {
                    var row = data.Rows[0];//.ToDataDictionary();

                    var seri0 = License.ConvertHexToString(seri);
                    var mahoa_seri0 = UtilityHelper.EnCrypt(seri0);
                    var key0 = License.ConvertHexToString(key);
                    var check_seri = mahoa_seri0 == key0;

                    var rCodeName = code_name == "" ? "" : UtilityHelper.DeCrypt(code_name);
                    var checkCode = row["CHECKCODE"].ToString().Trim();
                    var is_allow =
                            rCodeName.Length > V6Login.ClientName.Length + 1
                            && rCodeName.StartsWith(V6Login.ClientName)
                            && rCodeName.Substring(V6Login.ClientName.Length, 1) == "1"
                            && rCodeName.EndsWith(checkCode);

                    if (check_seri && is_allow)
                    {
                        UpdateLicenseV6OnlineCodeName(seri, key, code_name);
                        return true;
                    }
                }
                else
                {
                    InsertLicenseV6OnlineTemp(seri, key);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog(ex.Message);
            }
            return false;
        }

        public static void InsertLicenseV6OnlineTemp(string seri, string key)
        {
            var checkCode = SqlConnect.GetServerDateTime().ToString("yyyyMMddHH:mm:ss");
            var NAME = V6Login.ClientName;
            IDictionary<string, object> data = new SortedDictionary<string, object>();
            IDictionary<string, object> keys = new SortedDictionary<string, object>();
            data.Add("NAME", NAME);
            data.Add("SERI", seri);
            data.Add("KEY", key);
            keys.AddRange(data);
            data.Add("CHECKCODE", checkCode);
            data.Add("CODE_NAME", UtilityHelper.EnCrypt(NAME + "0" + checkCode));

            var d = V6BusinessHelper.Delete("V6ONLINES", keys);
            bool b = V6BusinessHelper.Insert("V6ONLINES", data);
        }

        private static void UpdateLicenseV6OnlineCodeName(string seri, string key, string codeName)
        {
            var NAME = V6Login.ClientName;
            IDictionary<string, object> keys = new SortedDictionary<string, object>();
            keys.Add("NAME", NAME);
            keys.Add("SERI", seri);
            keys.Add("KEY", key);
            IDictionary<string, object> data = new SortedDictionary<string, object>();
            data.Add("ALLOW", 1);
            data.Add("CODE_NAME", codeName);
            data.Add("V6UPDATE", V6Login.V6UpdateInfo);
            
            int u = V6BusinessHelper.Update("V6ONLINES", data, keys);
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
