﻿using System;
using System.IO;
using System.Threading;

namespace V6Tools
{
    public class V6FileIO
    {
        /// <summary>
        /// Upload file từ máy lên máy chủ chỉ định.
        /// </summary>
        /// <param name="info"></param>
        public static void CopyToVPN(V6IOInfo info)
        {
            CopyToVPN(info.FileName, info.FTP_SUBFOLDER, info.FTP_IP, info.FTP_USER, info.FTP_EPASS);
        }
        /// <summary>
        /// Upload file từ máy lên máy chủ chỉ định.
        /// </summary>
        /// <param name="fileName">Tên file sẽ upload.</param>
        /// <param name="subfolder">Thư mục con trên máy chủ. Vd Documents, hoặc Documents/Excels</param>
        /// <param name="ip">Địa chỉ máy chủ.</param>
        /// <param name="user"></param>
        /// <param name="ePass"></param>
        public static void CopyToVPN(string fileName, string subfolder, string ip, string user, string ePass)
        {
            try
            {
                if (string.IsNullOrEmpty(fileName)) return;
                Thread t = new Thread(o =>
                    {
                        FileInfo fi = new FileInfo(fileName);
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
                            du.Upload(fileName, subfolder);
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteToLog(string.Format("CopyToVPN {0} error: {1}", fileName, ex.Message), "V6Tools");
                        }
                    });
                t.IsBackground = true;
                t.Start();
            }
            catch (Exception ex)
            {
                Logger.WriteToLog(string.Format("CopyToVPN {0} error: {1}", fileName, ex.Message), "V6Tools");
            }
        }

        /// <summary>
        /// Download file từ server chỉ định về máy.
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static bool CopyFromVPN(V6IOInfo info)
        {
            return CopyFromVPN(info.FileName, info.FTP_SUBFOLDER, info.LOCAL_FOLDER, info.FTP_IP, info.FTP_USER, info.FTP_EPASS);
        }
        /// <summary>
        /// Download file từ server chỉ định về máy.
        /// </summary>
        /// <param name="fileName">Tên file sẽ download.</param>
        /// <param name="vpnSubFolder">Thư mục con trên máy chủ. Vd Documents, hoặc Documents/Excels</param>
        /// <param name="localFolder">Thư mục để lưu file. Vd: D:\\Documents</param>
        /// <param name="ip">Địa chỉ máy chủ.</param>
        /// <param name="user"></param>
        /// <param name="ePass"></param>
        /// <returns></returns>
        public static bool CopyFromVPN(string fileName, string vpnSubFolder, string localFolder, string ip, string user, string ePass)
        {
            try
            {
                UploadDownloadFTP du = new UploadDownloadFTP(ip, user, ePass);
                du.Download(fileName, vpnSubFolder, localFolder);
                return true;
            }
            catch (Exception ex)
            {
                Logger.WriteToLog(string.Format("CopyFromVPN {0} error: {1}", fileName, ex.Message), "V6Tools");
            }

            return false;
        }

        /// <summary>
        /// Hàm hỗ trợ copy tất cả các file trong 1 thư mục vào vị trí mới.
        /// </summary>
        /// <param name="sourceDirName"></param>
        /// <param name="destDirName"></param>
        /// <param name="copySubDirs"></param>
        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, true);
                }
            }
        }

        public static void DirectoryCopyWithParentFolder(string sourceDirName, string destDirName, bool copySubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            string newFolderPath = destDirName;
            if(dir.Parent == null) newFolderPath = Path.Combine(destDirName, dir.Name);
            DirectoryCopy(sourceDirName, newFolderPath, copySubDirs);
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

    public class V6IOInfo
    {
        public string FileName { get; set; }

        public string FTP_EPASS;
        public string FTP_USER;
        public string FTP_IP;
        public string FTP_SUBFOLDER;
        public string LOCAL_FOLDER;
    }
}
