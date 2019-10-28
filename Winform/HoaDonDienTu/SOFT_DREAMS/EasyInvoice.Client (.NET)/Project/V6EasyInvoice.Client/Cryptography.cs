// Decompiled with JetBrains decompiler
// Type: EasyInvoice.Client.Cryptography
// Assembly: EasyInvoice.Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9D591C1A-17CB-4CA4-A650-8459834356C1
// Assembly location: E:\Copy\Code\HoaDonDienTu\SOFT_DREAM_VNPTSTYLE\EasyInvoice.Client (.NET)\EasyInvoice.Client.dll

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace V6EasyInvoice.Client
{
    public class Cryptography
    {
        private static readonly byte[] OptionalEntropy = new byte[13]
    {
      (byte) 73,
      (byte) 118,
      (byte) 97,
      (byte) 110,
      (byte) 32,
      (byte) 77,
      (byte) 101,
      (byte) 100,
      (byte) 118,
      (byte) 101,
      (byte) 100,
      (byte) 101,
      (byte) 118
    };

        public static byte[] Protect(byte[] data)
        {
            try
            {
                return ProtectedData.Protect(data, Cryptography.OptionalEntropy, DataProtectionScope.LocalMachine);
            }
            catch (CryptographicException ex)
            {
                Console.WriteLine("Data was not encrypted. An error occurred.");
                Console.WriteLine(ex.ToString());
                return (byte[])null;
            }
        }

        public static byte[] Unprotect(byte[] data)
        {
            try
            {
                return ProtectedData.Unprotect(data, Cryptography.OptionalEntropy, DataProtectionScope.LocalMachine);
            }
            catch (CryptographicException ex)
            {
                Console.WriteLine("Data was not decrypted. An error occurred.");
                Console.WriteLine(ex.ToString());
                return (byte[])null;
            }
        }

        public static string Encrypt(string plain)
        {
            return Convert.ToBase64String(Cryptography.Protect(Encoding.Unicode.GetBytes(plain)));
        }

        public static string Decrypt(string base64String)
        {
            return Encoding.Unicode.GetString(Cryptography.Unprotect(Convert.FromBase64String(base64String)));
        }

        public static bool IsEncrypted(string base64String)
        {
            return Cryptography.Unprotect(Convert.FromBase64String(base64String)) != null;
        }

        public static string Encrypt<T>(string value, string password, string salt) where T : SymmetricAlgorithm, new()
        {
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, Encoding.Unicode.GetBytes(salt));
            SymmetricAlgorithm symmetricAlgorithm = (SymmetricAlgorithm)Activator.CreateInstance<T>();
            int cb1 = symmetricAlgorithm.KeySize >> 3;
            byte[] bytes1 = rfc2898DeriveBytes.GetBytes(cb1);
            int cb2 = symmetricAlgorithm.BlockSize >> 3;
            byte[] bytes2 = rfc2898DeriveBytes.GetBytes(cb2);
            ICryptoTransform encryptor = symmetricAlgorithm.CreateEncryptor(bytes1, bytes2);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream, Encoding.Unicode))
                        streamWriter.Write(value);
                }
                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }

        public static string Decrypt<T>(string text, string password, string salt) where T : SymmetricAlgorithm, new()
        {
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, Encoding.Unicode.GetBytes(salt));
            SymmetricAlgorithm symmetricAlgorithm = (SymmetricAlgorithm)Activator.CreateInstance<T>();
            int cb1 = symmetricAlgorithm.KeySize >> 3;
            byte[] bytes1 = rfc2898DeriveBytes.GetBytes(cb1);
            int cb2 = symmetricAlgorithm.BlockSize >> 3;
            byte[] bytes2 = rfc2898DeriveBytes.GetBytes(cb2);
            ICryptoTransform decryptor = symmetricAlgorithm.CreateDecryptor(bytes1, bytes2);
            using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(text)))
            {
                using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader streamReader = new StreamReader((Stream)cryptoStream, Encoding.Unicode))
                        return streamReader.ReadToEnd();
                }
            }
        }

        public static string Encrypt<T>(string value, byte[] password, byte[] salt) where T : SymmetricAlgorithm, new()
        {
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, salt, 128);
            SymmetricAlgorithm symmetricAlgorithm = (SymmetricAlgorithm)Activator.CreateInstance<T>();
            int cb1 = symmetricAlgorithm.KeySize >> 3;
            byte[] bytes1 = rfc2898DeriveBytes.GetBytes(cb1);
            int cb2 = symmetricAlgorithm.BlockSize >> 3;
            byte[] bytes2 = rfc2898DeriveBytes.GetBytes(cb2);
            ICryptoTransform encryptor = symmetricAlgorithm.CreateEncryptor(bytes1, bytes2);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream, Encoding.Unicode))
                        streamWriter.Write(value);
                }
                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }

        public static string Decrypt<T>(string text, byte[] password, byte[] salt) where T : SymmetricAlgorithm, new()
        {
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, salt, 128);
            SymmetricAlgorithm symmetricAlgorithm = (SymmetricAlgorithm)Activator.CreateInstance<T>();
            int cb1 = symmetricAlgorithm.KeySize >> 3;
            byte[] bytes1 = rfc2898DeriveBytes.GetBytes(cb1);
            int cb2 = symmetricAlgorithm.BlockSize >> 3;
            byte[] bytes2 = rfc2898DeriveBytes.GetBytes(cb2);
            ICryptoTransform decryptor = symmetricAlgorithm.CreateDecryptor(bytes1, bytes2);
            using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(text)))
            {
                using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader streamReader = new StreamReader((Stream)cryptoStream, Encoding.Unicode))
                        return streamReader.ReadToEnd();
                }
            }
        }
    }
}
