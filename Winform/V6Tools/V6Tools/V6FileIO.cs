using System;
using System.IO;
using System.Threading;

namespace V6Tools
{
    public class V6FileIO
    {
        public static void CopyToVPN(string file, string ip, string user, string ePass, string subfolder = null)
        {
            try
            {
                Thread t = new Thread(o =>
                    {
                        FileInfo fi = new FileInfo(file);
                        var s = 0;
                        while (IsFileLocked(fi))
                        {
                            s++;
                            if (s == 3600) return;
                            Thread.Sleep(1000);
                        }

                        try
                        {
                            UploadDownloadFTP du = new UploadDownloadFTP(ip, user, ePass);
                            du.Upload(file, subfolder);
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteToLog(string.Format("CopyToVPN {0} error: {1}", file, ex.Message), "V6Tools");
                        }
                    });
                t.IsBackground = true;
                t.Start();
            }
            catch (Exception ex)
            {
                Logger.WriteToLog(string.Format("CopyToVPN {0} error: {1}", file, ex.Message), "V6Tools");
            }
        }

        /// <summary>
        /// Kiểm tra một file đang bị khóa, sử dụng bởi chương trình khác hoặc không tồn tại.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }
    }
}
