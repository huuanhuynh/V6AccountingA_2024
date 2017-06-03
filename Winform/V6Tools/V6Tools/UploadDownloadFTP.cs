using System;
using System.IO;
using System.Net;
using System.Text;

namespace V6Tools
{
    public class UploadDownloadFTP
    {
        private string ftpServerIP;
        private string ftpPassword;
        private string ftpUserID;

        public UploadDownloadFTP(string ip, string user, string ePass)
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

        public string FtpPassword
        {
            get
            {
                return ftpPassword;
            }
            set
            {
                ftpPassword = value;
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
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="ftpSubFolder">abc hoặc abc/def</param>
        public void Upload(string filename, string ftpSubFolder = null)
        {
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
                Stream requestStream = ftpWebRequest.GetRequestStream();
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
                throw new Exception("FTP_Upload_Download.Upload : " + ex.Message);
            }
        }

        public void DeleteFTP(string fileName)
        {
            try
            {
                string url1 = "ftp://" + ftpServerIP + "/" + fileName;
                FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(new Uri(url1));
                ftpWebRequest.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                ftpWebRequest.KeepAlive = false;
                ftpWebRequest.Method = "DELE";
                string str2 = string.Empty;
                FtpWebResponse ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();
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
                throw new Exception("FTP_Upload_Download.DeleteFTP : " + ex.Message);
            }
        }

        public string[] GetFilesDetailList()
        {
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
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(new Uri("ftp://" + ftpServerIP + "/"));
                ftpWebRequest.UseBinary = true;
                ftpWebRequest.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                ftpWebRequest.Method = "NLST";
                WebResponse response = ftpWebRequest.GetResponse();
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
                throw new Exception("FTP_Upload_Download.GetFileList : " + ex.Message);
            }
        }

        public void Download(string filePath, string fileName)
        {
            try
            {
                FileStream fileStream = new FileStream(filePath + "\\" + fileName, FileMode.Create);
                FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + fileName));
                ftpWebRequest.Method = "RETR";
                ftpWebRequest.UseBinary = true;
                ftpWebRequest.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                FtpWebResponse ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();
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
            }
            catch (Exception ex)
            {
                throw new Exception("FTP_Upload_Download.Download : " + ex.Message);
            }
        }

        public long GetFileSize(string filename)
        {
            long contentLength;
            try
            {
                FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + filename));
                ftpWebRequest.Method = "SIZE";
                ftpWebRequest.UseBinary = true;
                ftpWebRequest.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                FtpWebResponse ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();
                Stream responseStream = ftpWebResponse.GetResponseStream();
                contentLength = ftpWebResponse.ContentLength;
                if (responseStream != null) responseStream.Close();
                ftpWebResponse.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("FTP_Upload_Download : " + ex.Message);
            }
            return contentLength;
        }

        public void Rename(string currentFilename, string newFilename)
        {
            try
            {
                FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + currentFilename));
                ftpWebRequest.Method = "RENAME";
                ftpWebRequest.RenameTo = newFilename;
                ftpWebRequest.UseBinary = true;
                ftpWebRequest.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                FtpWebResponse ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();
                var response_stream = ftpWebResponse.GetResponseStream();
                if (response_stream != null) response_stream.Close();
                ftpWebResponse.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("FTP_Upload_Download : " + ex.Message);
            }
        }

        public void MakeDir(string dirName)
        {
            try
            {
                FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + dirName));
                ftpWebRequest.Method = "MKD";
                ftpWebRequest.UseBinary = true;
                ftpWebRequest.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                FtpWebResponse ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();
                var response_stream = ftpWebResponse.GetResponseStream();
                if (response_stream != null) response_stream.Close();
                ftpWebResponse.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("FTP_Upload_Download : " + ex.Message);
            }
        }
    }
}
