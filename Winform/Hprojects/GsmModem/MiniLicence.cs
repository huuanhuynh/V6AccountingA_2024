using GSM;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniLicence
{
    class MiniLicence
    {
        /// <summary>
        /// Có bản quyền, chưa kiểm hạn sử dụng
        /// </summary>
        public static bool Ls = false;
        /// <summary>
        /// Limit
        /// </summary>
        private static bool Lm = false;
        /// <summary>
        /// Có bản quyền còn hạn sử dụng
        /// </summary>
        public static bool LA { get { return (Ls == true) && (Lm == false); } }
        
        public static string Hkey = "";
        /// <summary>
        /// Mã ổ đĩa 1c
        /// </summary>
        public static string IKey = "";
        /// <summary>
        /// Mã m
        /// </summary>
        public static string GKey = "";
        /// <summary>
        /// Mã d
        /// </summary>
        public static string Dkey = "";
        public static string NotRegOrLimitM = "Chưa được đăng ký hoặc hết hạn.";

        public static bool CheckLicence()
        {
            return CheckLicence(ReadLicence());
        }
        public static bool CheckLicence(string hkey)
        {
            try
            {
                
                MachineInfo.GetInfo GI = new MachineInfo.GetInfo();
                string DisN = GI.GetVolumeSerial(Application.StartupPath.Substring(0, 1));
                //string Hex7Seri = GsmEncoding.Encode7BitHex(DisN);
                string CryDN = HuuanEncrypt.MaHoa1Chieu(DisN, (byte)(DisN.Length));
                string[] infos = GetKeyInfo(hkey, CryDN);

                if (infos[0] == CryDN)
                {
                    Ls = true;
                    DateTime todate = DateTime.ParseExact(HuuanEncrypt.DeCrypt(infos[2], infos[0]), "d/M/yyyy", null);
                    if (todate > DateTime.Today)
                        Lm = false;
                    else
                        Lm = true;
                }
                else Ls = false;

                IKey = infos[0];
                GKey = infos[1];
                Dkey = infos[2];

                WriteLicence(hkey);

                return true;
            }
            catch
            {
                Ls = false;
                IKey = "";
                GKey = "";
                Dkey = "";
                return false;
            }
        }

        private static string[] GetKeyInfo(string HexKey2, string CryD)
        {
            string Ekeyi = GsmEncoding.Decode8BitHex(HexKey2);
            string KeyinfoString = HuuanEncrypt.DeCrypt(Ekeyi, CryD);
            string[] infos = KeyinfoString.Split(new string[] { "<H@>", ">%h<" }, StringSplitOptions.None);
            return infos;
        }

        private static string ReadLicence()
        {
            Hkey = ReadRegistryHkey(Application.ProductName,"Hkey");
            return Hkey;
        }
        private static void WriteLicence(string hkey)
        {
            WriteRegistry(Application.ProductName, "Hkey", hkey);
        }

        #region ==== Registry ====
        public static string ReadRegistryHkey(string ProductName, string KeyName)
        {
            if (KeyName != "")
            {
                // Mở một subkey để đọc
                RegistryKey rk = Registry.LocalMachine
                    .OpenSubKey(@"SOFTWARE\" + ProductName);
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
                    RegistryKey rk = Registry.LocalMachine
                        .CreateSubKey(@"SOFTWARE\" + ProductName);
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
        #endregion rsk
    }
}
