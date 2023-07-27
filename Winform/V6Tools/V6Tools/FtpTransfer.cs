using System;
using System.IO;
using System.Net;
using System.Text;

namespace V6Tools
{
    /// <summary>
    /// Quản lý kết nối FTP.
    /// </summary>
    public class FtpTransfer
    {
        /// <summary>
        /// ip máy chủ.
        /// </summary>
        private string ftpServerIP;
        private string ftpPassword;
        private string ftpUserID;

        /// <summary>
        /// Quản lý kết nối FTP
        /// </summary>
        /// <param name="ip">ip máy chủ</param>
        /// <param name="user">username trên máy chủ</param>
        /// <param name="ePass">password mã hóa</param>
        public FtpTransfer(string ip, string user, string ePass)
        {
            ftpServerIP = ip;
            ftpUserID = user;
            ftpPassword = UtilityHelper.DeCrypt(ePass);
        }

        public string FtpServerIP
        {
            get
            {
                return ftpServerIP;
            }
            set
            {
                ftpServerIP = value;
            }
        }
        
        public string FtpUserID
        {
            get
            {
                return ftpUserID;
            }
            set
            {
                ftpUserID = value;
            }
        }

        /// <summary>
        /// Đưa tập tin lên server FTP
        /// </summary>
        /// <param name="filename">Đường dẫn tập tin ở máy trạm.</param>
        /// <param name="ftpSubFolder">đường dẫn thư mục của server FTP. abc hoặc abc/def</param>
        public void Upload(string filename, string ftpSubFolder = null)
        {
            Stream requestStream = null;
            FileInfo fileInfo = new FileInfo(filename);
            if (!string.IsNullOrEmpty(ftpSubFolder) && !ftpSubFolder.StartsWith("/"))
            {
                ftpSubFolder = "/" + ftpSubFolder;
            }
            string url = "ftp://" + ftpServerIP + ftpSubFolder + "/" + fileInfo.Name;
            
            FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(new Uri(url));
            if (string.IsNullOrEmpty(FtpUserID))
            {
                ftpWebRequest.Credentials = new NetworkCredential("anonymous", "huuan_huynh@yahoo.com");//anonymous
            }
            else
            {
                ftpWebRequest.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
            }
            ftpWebRequest.KeepAlive = false;
            ftpWebRequest.Method = "STOR";
            ftpWebRequest.UseBinary = true;
            ftpWebRequest.ContentLength = fileInfo.Length;
            int count1 = 2048;
            byte[] buffer = new byte[count1];
            FileStream fileStream = fileInfo.OpenRead();
            try
            {
                requestStream = ftpWebRequest.GetRequestStream();
                for (int count2 = fileStream.Read(buffer, 0, count1);
                    count2 != 0;
                    count2 = fileStream.Read(buffer, 0, count1))
                {
                    requestStream.Write(buffer, 0, count2);
                }

                requestStream.Close();
                fileStream.Close();
            }
            catch (Exception ex)
            {
                if (requestStream != null) requestStream.Close();
                throw;
            }
            //return message;
        }

        public void DeleteFTP(string fileName)
        {
            FtpWebResponse ftpWebResponse = null;
            try
            {
                string url1 = "ftp://" + ftpServerIP + "/" + fileName;
                FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(new Uri(url1));
                ftpWebRequest.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                ftpWebRequest.KeepAlive = false;
                ftpWebRequest.Method = "DELE";
                string str2 = string.Empty;
                ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();
                long contentLength = ftpWebResponse.ContentLength;
                Stream responseStream = ftpWebResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream);
                str2 = streamReader.ReadToEnd();
                streamReader.Close();
                if (responseStream != null) responseStream.Close();
                ftpWebResponse.Close();
            }
            catch (Exception ex)
            {
                if (ftpWebResponse != null) ftpWebResponse.Close();
                throw new Exception("FTP_Upload_Download.DeleteFTP : " + ex.Message);
            }
        }

        public string[] GetFilesDetailList()
        {
            FtpWebResponse ftpWebResponse = null;
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(new Uri("ftp://" + ftpServerIP + "/"));
                ftpWebRequest.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                ftpWebRequest.Method = "LIST";
                WebResponse response = ftpWebRequest.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                for (string str = streamReader.ReadLine(); str != null; str = streamReader.ReadLine())
                {
                    stringBuilder.Append(str);
                    stringBuilder.Append("\n");
                }
                stringBuilder.Remove(stringBuilder.ToString().LastIndexOf("\n", StringComparison.Ordinal), 1);
                streamReader.Close();
                response.Close();
                return stringBuilder.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                throw new Exception("FTP_Upload_Download.GetFilesDetailList : " + ex.Message);
            }
        }

        public string[] GetFileList()
        {
            WebResponse response = null;
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(new Uri("ftp://" + ftpServerIP + "/"));
                ftpWebRequest.UseBinary = true;
                ftpWebRequest.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                ftpWebRequest.Method = "NLST";
                response = ftpWebRequest.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                for (string str = streamReader.ReadLine(); str != null; str = streamReader.ReadLine())
                {
                    stringBuilder.Append(str);
                    stringBuilder.Append("\n");
                }
                stringBuilder.Remove(stringBuilder.ToString().LastIndexOf('\n'), 1);
                streamReader.Close();
                response.Close();
                return stringBuilder.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                if (response != null) response.Close();
                throw new Exception("FTP_Upload_Download.GetFileList : " + ex.Message);
            }
        }

        /// <summary>
        /// Lưu file từ FTP folder về local.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="subFolder">Thư mục con trong thư mục FTP</param>
        /// <param name="localFolder"></param>
        public void Download(string fileName, string subFolder, string localFolder)
        {
            FtpWebResponse ftpWebResponse = null;
            FileStream fileStream = null;
            string localSaveFile = Path.Combine(localFolder, fileName);
            string tempSaveFile = localFolder + ".tem";
            try
            {

                if (File.Exists(tempSaveFile)) File.Delete(tempSaveFile);
                fileStream = new FileStream(tempSaveFile, FileMode.Create); // đường dẫn dấu nối \
                if (subFolder != null)
                {
                    while (subFolder.StartsWith("/")) subFolder = subFolder.Substring(1);
                    while (subFolder.EndsWith("/")) subFolder = subFolder.Substring(0, subFolder.Length - 1);
                }
                FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + subFolder + "/" + fileName));
                ftpWebRequest.Method = "RETR";
                ftpWebRequest.UseBinary = true;
                ftpWebRequest.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();
                Stream responseStream = ftpWebResponse.GetResponseStream();
                long contentLength = ftpWebResponse.ContentLength;
                int count1 = 2048;
                byte[] buffer = new byte[count1];
                for (int count2 = responseStream.Read(buffer, 0, count1);
                    count2 > 0;
                    count2 = responseStream.Read(buffer, 0, count1))
                {
                    fileStream.Write(buffer, 0, count2);
                }
                responseStream.Close();
                fileStream.Close();
                ftpWebResponse.Close();
                // Nếu đã download thành công
                // Xóa file local nếu có.
                if (File.Exists(tempSaveFile)) File.Delete(tempSaveFile);
                // copy file temp thành file local.
                File.Copy(tempSaveFile, localSaveFile, true);
            }
            catch (Exception ex)
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }
                if (ftpWebResponse != null) ftpWebResponse.Close();
                throw new Exception("FTP_Upload_Download.Download : " + ex.Message);
            }
        }

        public long GetFileSize(string filename, string subFolder)
        {
            FtpWebResponse ftpWebResponse = null;
            long contentLength = -1;
            try
            {
                if (subFolder != null)
                {
                    while (subFolder.StartsWith("/")) subFolder = subFolder.Substring(1);
                    while (subFolder.EndsWith("/")) subFolder = subFolder.Substring(0, subFolder.Length - 1);
                }
                FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + subFolder + "/" + filename));
                ftpWebRequest.Method = "SIZE";
                ftpWebRequest.UseBinary = true;
                ftpWebRequest.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();
                Stream responseStream = ftpWebResponse.GetResponseStream();
                contentLength = ftpWebResponse.ContentLength;
                if (responseStream != null) responseStream.Close();
                ftpWebResponse.Close();
            }
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    if (ftpWebResponse != null) ftpWebResponse.Close();
                    throw;
                }
                if (ftpWebResponse != null) ftpWebResponse.Close();
            }
            catch (Exception ex)
            {
                if (ftpWebResponse != null) ftpWebResponse.Close();
                throw new Exception("FTP_Upload_Download : " + ex.Message);
            }
            return contentLength;
        }

        public void Rename(string currentFilename, string newFilename)
        {
            FtpWebResponse ftpWebResponse = null;
            try
            {
                FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + currentFilename));
                ftpWebRequest.Method = "RENAME";
                ftpWebRequest.RenameTo = newFilename;
                ftpWebRequest.UseBinary = true;
                ftpWebRequest.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();
                var response_stream = ftpWebResponse.GetResponseStream();
                if (response_stream != null) response_stream.Close();
                ftpWebResponse.Close();
            }
            catch (Exception ex)
            {
                if (ftpWebResponse != null) ftpWebResponse.Close();
                throw new Exception("FTP_Upload_Download : " + ex.Message);
            }
        }

        public void MakeDir(string dirName)
        {
            FtpWebResponse ftpWebResponse = null;
            try
            {
                FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + dirName));
                ftpWebRequest.Method = "MKD";
                ftpWebRequest.UseBinary = true;
                ftpWebRequest.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();
                var response_stream = ftpWebResponse.GetResponseStream();
                if (response_stream != null) response_stream.Close();
                ftpWebResponse.Close();
            }
            catch (Exception ex)
            {
                if (ftpWebResponse != null) ftpWebResponse.Close();
                throw new Exception("FTP_Upload_Download : " + ex.Message);
            }
        }

        public bool CheckConnection(out string message)
        {
            WebResponse ftpWebResponse = null;
            try
            {
                string url = "ftp://" + ftpServerIP;
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                ftpWebResponse = request.GetResponse();
                ftpWebResponse.Close();
                message = null;
                
                return true;
            }
            catch (WebException ex)
            {
                if (ftpWebResponse != null) ftpWebResponse.Close();
                message = ex.Message;
                return false;
            }
        }

    }
}
