using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace V6Tools
{
    /// <summary>
    /// Để sử dụng lớp này chương trình cần có các thư viện đi kèm sau:
    /// 7z.dll SevenZipSharp.dll [7za.exe]
    /// Nếu chưa có các thư viện trên có thể dùng hàm WriteAllResources để tạo.
    /// </summary>
    public class V67z
    {
        #region ==== Variables ====
        private static string _DllPath = Application.StartupPath;

       
        #endregion end var
        
        #region ==== Unzip File ====

        /// <summary>
        /// Giải nén tập tin.
        /// </summary>
        /// <param name="zipfile">Tập tin cần giải nén.</param>
        /// <param name="password">Mật khẩu giải nén.</param>
        /// <param name="path">Nơi đến của tập tin sau giải nén.</param>
        public static void Unzip(string zipfile, string path, string password)
        {
            try
            {
                WriteAllResources();
                if (File.Exists(zipfile))
                {
                    Extractor e = new Extractor(zipfile, path, password);
                    e.StartExtract();

                    while (!e._finish)
                    {
                        ;
                    }
                }
                else
                {
                    MessageBox. Show("Tập tin không tồn tại:\n" + zipfile,
                            "V6Soft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            catch (Exception ex)
            {
                MessageBox. Show("Unzip.\n" + ex.Message, "V6Soft", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Giải nén tập tin.
        /// </summary>
        /// <param name="zipfile">Tập tin cần giải nén.</param>
        /// <param name="path">Nơi đến của tập tin sau giải nén.</param>
        public static void Unzip(string zipfile, string path)
        {
            Unzip(zipfile, null, path);
        }
        
        /// <summary>
        /// Giải nén ngay tại đây.
        /// </summary>
        /// <param name="zipfile">Tập tin cần giải nén.</param>
        public static void Unzip(string zipfile)
        {
            Unzip(zipfile, Directory.GetParent(zipfile).FullName);
        }
        
        /// <summary>
        /// Tạo thư mục ngay đó và giải nén.
        /// </summary>
        /// <param name="zipfile">Tập tin cần giải nén.</param>
        public static void UnzipF(string zipfile)
        {
            UnzipF(zipfile, null);
        }
        /// <summary>
        /// Tạo thư mục ngay đó và giải nén.
        /// </summary>
        /// <param name="zipfile">Tập tin cần giải nén.</param>
        /// <param name="password">Mật khẩu giải nén.</param>
        public static void UnzipF(string zipfile, string password)
        {
            Unzip(zipfile, password, Path.GetFileNameWithoutExtension(zipfile));
        }

        #endregion end unzip file

        #region ==== Zip Folder ====

        //Tao tu dien
        private static void GetFileDictionary(Dictionary<string, string> FileDictionary, string path, string parent)
        {
            if (!parent.EndsWith("\\"))
            {
                parent += "\\";
            }
            string[] Files = Directory.GetFiles(path);
            string s = "";
            foreach (string file in Files)
            {

                if (file.ToLower().StartsWith(parent.ToLower()))
                {
                    s = file.Substring(parent.Length);
                }
                FileDictionary.Add(s, file);
            }
            string[] Dirs = Directory.GetDirectories(path);
            foreach (string dir in Dirs)
            {
                GetFileDictionary(FileDictionary, dir, parent);
            }
        }

        /// <summary>
        /// Nén tất cả tập tin trong thư mục và tất cả thư mục con của nó.
        /// </summary>
        /// <param name="path">Thư mục sẽ được nén.</param>
        /// <param name="zipfile">Tâp tin nén sẽ tạo.</param>
        public static void ZipFolder(string path, string zipfile)
        {
            try
            {
                WriteAllResources();
                if (path.EndsWith("\\"))
                {
                    path = path.TrimEnd('\\');
                }

                if (Directory.Exists(path))
                {
                    Dictionary<string, string> Filedic = new Dictionary<string, string>();
                    string parent = Directory.GetParent(path).FullName;
                    GetFileDictionary(Filedic, path, parent);

                    if (!zipfile.ToLower().EndsWith(".7z"))
                    {
                        zipfile += ".7z";
                    }

                    Compressor c = new Compressor(zipfile, Filedic);
                    c.StartCompress();

                    while (!c._finish)
                    {
                        ;
                    }
                }
                else
                {
                    MessageBox. Show("Thư mục không tồn tại:\n" + path,
                        "V6Soft", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            catch (Exception ex)
            {
                MessageBox. Show("V67zZip.\n" + ex.Message, "V6Soft", MessageBoxButtons.OK, MessageBoxIcon.Error);
                WriteAllResources();
            }
        }

        /// <summary>
        /// Nén thư mục, tạo tập tin nén ngay đó.
        /// </summary>
        /// <param name="path">Thư mục sẽ được nén.</param>
        public static void ZipFolder(string path)
        {
            ZipFolder(path, path);
        }

        #endregion end Zip Folder

        #region ==== Zip Files ====

        #region ==== ZipAllFilesInFolder ====
        /// <summary>
        /// Nén các file trong 1 thư mục, không nén thư mục con và các file trong đó!
        /// </summary>
        /// <param name="zipfile">Tập tin nén được tạo ra.</param>
        /// <param name="path">Đường dẫn đến thư mục.</param>
        public static void ZipAllFilesInFolder(string zipfile, string path)
        {
            ZipAllFilesInFolder(zipfile, path, false);
        }

        /// <summary>
        /// Nén các file trong 1 thư mục, không nén thư mục con và các file trong đó!
        /// </summary>
        /// <param name="zipfile">Tập tin nén được tạo ra.</param>
        /// <param name="path">Đường dẫn đến thư mục.</param>
        /// <param name="wait">Chờ đến khi nén xong mới thoát khỏi hàm.</param>
        public static void ZipAllFilesInFolder(string zipfile, string path, bool wait)
        {
            ZipAllFilesInFolder(zipfile, path, "*", wait);
        }

        /// <summary>
        /// Nén các file trong 1 thư mục, không nén thư mục con và các file trong đó!
        /// </summary>
        /// <param name="zipfile">Tập tin nén được tạo ra.</param>
        /// <param name="path">Đường dẫn đến thư mục.</param>
        /// <param name="searchPattern">Lọc tập tin
        /// ???.* (Tập tin có 3 ký tự và phần mở rộng),
        /// *.?* (Tất cả tập tin có phần mở rộng),
        /// * (Tất cả tập tin)</param>
        public static void ZipAllFilesInFolder(string zipfile, string path, string searchPattern)
        {
            ZipAllFilesInFolder(zipfile, path, searchPattern, false);
        }

        /// <summary>
        /// Nén các file trong 1 thư mục, không nén thư mục con và các file trong đó!
        /// </summary>
        /// <param name="zipfile">Tập tin nén được tạo ra.</param>
        /// <param name="path">Đường dẫn đến thư mục.</param>
        /// <param name="searchPattern">Lọc tập tin
        /// ???.* (Tập tin có 3 ký tự và phần mở rộng),
        /// *.?* (Tất cả tập tin có phần mở rộng),
        /// * (Tất cả tập tin)</param>
        /// <param name="wait">Chờ đến khi nén xong mới thoát khỏi hàm.</param>
        public static void ZipAllFilesInFolder(string zipfile, string path, string searchPattern, bool wait)
        {
            try
            {
                //List<string> listFiles = new List<string>();
                string directory = Directory.GetParent(path).FullName;
                string[] files = Directory.GetFiles(directory, searchPattern);

                ZF(zipfile, wait, files);
            }
            catch (Exception ex)
            {
                MessageBox. Show("ZipFile.\n" + ex.Message, "V6Soft", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion ==== ZipAllFilesInFolder ====

        #region ==== ZipFiles ====

        /// <summary>
        /// Nén nhiều tập tin vào 1
        /// </summary>
        /// <param name="zipfile"></param>
        /// <param name="wait">Chờ nén xong mới kết thúc hàm?</param>
        /// <param name="files"></param>
        public static void ZipFiles(string zipfile, bool wait, params string[] files)
        {
            try
            {
                ZF(zipfile, wait, files);
            }
            catch (Exception ex)
            {
                MessageBox. Show("ZipFile.\n" + ex.Message, "V6Soft", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Nén nhiều tập tin lại thành 1 tập tin nén .7z
        /// ZipFiles(zipFile, file1[, file2[...]])
        /// </summary>
        /// <param name="zipfile">Tập tin nén kết quả.</param>
        /// <param name="files">Các tập tin sẽ được nén.</param>
        public static void ZipFiles(string zipfile, params string[] files)
        {
            try
            {
                ZF(zipfile, false, files);
            }
            catch (Exception ex)
            {
                MessageBox. Show("ZipFile.\n" + ex.Message, "V6Soft", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Nén 1 tập tin.
        /// </summary>
        /// <param name="zipfile">Tập tin nén tạo ra.</param>
        /// <param name="wait">Chờ nén xong mới kết thúc hàm?</param>
        /// <param name="file">Tập tin cần nén.</param>
        public static void ZipFile(string zipfile, bool wait, string file)
        {
            try
            {
                if (File.Exists(file))
                {
                    ZF(zipfile, wait, file);
                }
                else
                {
                    throw new Exception("Không tồn tại tập tin [" + file + "]!");
                }
            }
            catch (Exception ex)
            {
                MessageBox. Show("ZipFile.\n" + ex.Message, "V6Soft", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
        /// Nén 1 tập tin.
        /// </summary>
        /// <param name="zipfile">Tập tin nén tạo ra.</param>
        /// <param name="file">Tập tin cần nén.</param>
        public static void ZipFile(string zipfile, string file)
        {
            try
            {                
                if (File.Exists(file))
                {
                    ZF(zipfile, false, file);
                }
                else
                {
                    throw new Exception("Không tồn tại tập tin [" + file + "]!");
                }
            }
            catch (Exception ex)
            {
                MessageBox. Show("ZipFile.\n" + ex.Message, "V6Soft", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
        }
        
        /// <summary>
        /// Tạo tập tin nén ngay tại thư mục chứa.
        /// </summary>
        /// <param name="file">Tập tin sẽ được nén.</param>
        public static void ZipFile(string file)
        {
            ZipFile(file, file);
        }
        #endregion ==== ZipFiles ====


        /// <summary>
        /// Gửi lệnh nén - throw Exception
        /// </summary>
        /// <param name="zipfile">Tập tin nén tạo thành</param>
        /// <param name="wait">Chờ nén xong mới kết thúc hàm?</param>
        /// <param name="files">Có thể là một mảng string hoặc các string cách nhau bởi dấu (,)</param>
        static void ZF(string zipfile, bool wait, params string[] files)
        {
            WriteAllResources();
            if (!zipfile.ToLower().EndsWith(".7z"))
            {
                zipfile += ".7z";
            }
            Compressor c = new Compressor(zipfile, files);            
            c.StartCompress();

            if(wait)
            while (!c._finish)
            {
                ;//Thread.Sleep(500);
            }
        }
        
        #endregion end Zip Files

        #region ==== 7za Command line ====
        /// <summary>
        /// Sử dụng command line của 7z:
        /// 7z a archive1.zip subdir\
        /// adds all files and subfolders from folder subdir to archive archive1.zip.
        /// The filenames in archive will contain subdir\ prefix.
        /// 7z e archive.zip -oc:\soft *.cpp -r
        /// extracts all *.cpp files from archive archive.zip to c:\soft folder.
        /// </summary>
        /// <param name="Command">VD: "a C:\tozfile.7z C:\fileorfoldertozip"</param>
        public static void Run7z(string Command)
        {
            //System.Diagnostics.Process.Start(command);            
            _R7z(Command);
        }
        private static void _R7z(string Command)
        {
            WriteAllResources();
            var p = new Process
            {
                StartInfo =
                {
                    FileName = _DllPath + "\\7za",
                    Arguments = Command
                }
            };
            p.Start();
        }
        #endregion end 7za Command line

        #region ===== Write Resources =====
        
        public static void WriteAllResources()
        {
            if (!File.Exists(_DllPath + "\\SevenZipSharp.dll"))
            {
                WriteSevenZipSharp();
            }
            if (!File.Exists(_DllPath + "\\7z.dll"))
            {
                Write7z();
            }
            if (!File.Exists(_DllPath + "\\7z64.dll"))
            {
                Write7z64();
            }
            if (!File.Exists(_DllPath + "\\7za.exe"))
            {
                Write7za();
            }
        }

        private static void Write7za()
        {
            try
            {
                WriteResource(Properties.Resources._7za, _DllPath +
                    "\\7za.exe");
            }
            catch
            {
            }
        }
        private static void Write7z()
        {
            try
            {
                WriteResource(Properties.Resources._7z, _DllPath +
                    "\\7z.dll");
            }
            catch 
            {                
            }
        }
        private static void Write7z64()
        {
            try
            {
                WriteResource(Properties.Resources._7z64, _DllPath +
                    "\\7z64.dll");
            }
            catch
            {
            }
        }
        private static void WriteSevenZipSharp()
        {
            try
            {
                WriteResource(Properties.Resources.SevenZipSharp, _DllPath +
                    "\\SevenZipSharp.dll");
            }
            catch
            {
            }
        }
        private static void WriteResource(byte[] rc, string filename)
        {            
            FileStream fs = new FileStream(filename, FileMode.Create);
            fs.Write(rc, 0, rc.Length);
            fs.Close();
        }
        
        #endregion ===== Write Resources =====


        #region ==== Private Class ====
        private class Platform
        {
            [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool IsWow64Process([In] IntPtr hProcess, [Out] out bool lpSystemInfo);

            public static bool Is64Bit()
            {
                if (IntPtr.Size == 8 || (IntPtr.Size == 4 && Is32BitProcessOn64BitProcessor()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            private static bool Is32BitProcessOn64BitProcessor()
            {
                bool retVal;

                IsWow64Process(Process.GetCurrentProcess().Handle, out retVal);

                return retVal;
            }
        }

        private class Compressor
        {
            private enum CompressMode
            {
                Files,
                Folder
            }

            public SevenZip.SevenZipCompressor _SZC;
            public bool _finish = false;
            public byte _finish_percent = 0;
            private CompressMode _compressmode;

            private string _zipfile;
            private string[] _files;
            private Dictionary<string, string> _FileDictionary;

            /// <summary>
            /// Khởi tạo đối tượng nén file.
            /// </summary>
            /// <param name="zipfile"></param>
            /// <param name="files"></param>
            public Compressor(string zipfile, params string[] files)
            {
                _zipfile = zipfile;
                _files = files;
                _compressmode = CompressMode.Files;
            }
            /// <summary>
            /// Khởi tạo đối tượng nén thư mục.
            /// </summary>
            /// <param name="zipfile"></param>
            /// <param name="filedic"></param>
            public Compressor(string zipfile, Dictionary<string, string> filedic)
            {
                _zipfile = zipfile;
                _FileDictionary = filedic;
                _compressmode = CompressMode.Folder;
            }

            public void StartCompress()
            {
                Compress();
                //Thread r = new Thread(Compress);
                //r.Start();
            }
            
            private void Compress()
            {
                _finish = false;
                
                if (Platform.Is64Bit())
                    SevenZip.SevenZipCompressor.SetLibraryPath(_DllPath + @"\7z64.dll");
                else
                    SevenZip.SevenZipCompressor.SetLibraryPath(_DllPath + @"\7z.dll");

                _SZC = new SevenZip.SevenZipCompressor();
                
                _SZC.Compressing += new EventHandler<SevenZip.ProgressEventArgs>(_SZC_Compressing);
                _SZC.CompressionFinished += new EventHandler<EventArgs>(_SZC_CompressionFinished);
                //_SZC.FileCompressionStarted += new EventHandler<SevenZip.FileNameEventArgs>(_SZC_FileCompressionStarted);
                //_SZC.FileCompressionFinished += new EventHandler<EventArgs>(_SZC_FileCompressionFinished);


                _SZC.TempFolderPath = Path.GetTempPath();
                _SZC.ArchiveFormat = SevenZip.OutArchiveFormat.SevenZip;
                _SZC.CompressionMethod = SevenZip.CompressionMethod.Default;

                switch (_compressmode)
                {
                    case CompressMode.Files:
                        _SZC.BeginCompressFiles(_zipfile, _files);
                        break;
                    case CompressMode.Folder:
                        _SZC.CompressFileDictionary(_FileDictionary, _zipfile);
                        break;
                    default:
                        break;
                }
            }
                        
            void _SZC_Compressing(object sender, SevenZip.ProgressEventArgs e)
            {
                _finish_percent = e.PercentDone;
            }

            void _SZC_CompressionFinished(object sender, EventArgs e)
            {                
                _finish = true;
            }
        }

        private class Extractor
        {
            public SevenZip.SevenZipExtractor _SZE;
            public bool _finish = false;
            public byte _finish_percent = 0;
            //private CompressMode _compressmode;

            private string _zipfile;
            private string _password;
            private string _path;
            //private Dictionary<string, string> _FileDictionary;

            /// <summary>
            /// Khởi tạo đối tượng nén file.
            /// </summary>
            /// <param name="zipfile"></param>
            /// <param name="files"></param>
            public Extractor(string zipfile, string path)
            {
                _zipfile = zipfile;
                _path = path;
                _password = null;
            }
            public Extractor(string zipfile, string path, string password)
            {
                _zipfile = zipfile;
                _path = path;
                _password = password;
            }

            public void StartExtract()
            {
                Extract();
                //Thread r = new Thread(Extract);
                //r.Start();
            }
            
            private void Extract()
            {
                _finish = false;

                if (Platform.Is64Bit())
                    SevenZip.SevenZipExtractor.SetLibraryPath(_DllPath + @"\7z64.dll");
                else
                    SevenZip.SevenZipExtractor.SetLibraryPath(_DllPath + @"\7z.dll");
                
                if (string.IsNullOrEmpty(_password))
                {
                    _SZE = new SevenZip.SevenZipExtractor(_zipfile);
                }
                else
                {
                    _SZE = new SevenZip.SevenZipExtractor(_zipfile, _password);
                }

                _SZE.Extracting += new EventHandler<SevenZip.ProgressEventArgs>(_SZE_Extracting);
                _SZE.ExtractionFinished += new EventHandler<EventArgs>(_SZE_ExtractionFinished);

                _SZE.BeginExtractArchive(_path);
            }

            void _SZE_ExtractionFinished(object sender, EventArgs e)
            {
                _finish = true;
            }

            void _SZE_Extracting(object sender, SevenZip.ProgressEventArgs e)
            {
                _finish_percent = e.PercentDone;
            }
        }
        #endregion ==== Class ====

        #region ==== Fuctions ====
        
        #endregion
    }
}
