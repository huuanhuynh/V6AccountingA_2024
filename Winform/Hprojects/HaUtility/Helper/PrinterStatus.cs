using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing.Printing;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;

namespace HaUtility.Helper
{
    public class PrinterStatus
    {
        #region [DLL Import...]
        [DllImport("kernel32.dll", EntryPoint = "GetLastError", SetLastError = false,
            ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 GetLastError();

        [DllImport("winspool.drv", EntryPoint = "GetJob", SetLastError = true,
            CharSet = CharSet.Auto, ExactSpelling = false,
            CallingConvention = CallingConvention.StdCall)]
        public static extern bool GetJob(Int32 hPrinter,
                    Int32 dwJobId, Int32 Level,
                    IntPtr lpJob, Int32 cbBuf,
                    ref Int32 lpbSizeNeeded);

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string Name);

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool GetDefaultPrinter(StringBuilder pszBuffer, ref int size);
        #endregion [DLL Import...]

        public static string GetDefaultPrinterName()
        {
            StringBuilder dp = new StringBuilder(256);
            int size = dp.Capacity;
            if (GetDefaultPrinter(dp, ref size))
            {
                return (String.Format("{0}", dp.ToString().Trim()));
            }
            else
            {
                return "";
            }
        }

        public static string[] GetListOfPrinters()
        {
            List<string> Plist = new List<string>();

            foreach (String printer in PrinterSettings.InstalledPrinters)
            {
                Plist.Add(printer);
            }
            return Plist.ToArray();
        }

        /// <summary>
        /// Checks the printer.
        /// </summary>
        /// <param name="printerName">The printername.</param>
        /// <returns></returns>
        public static bool CheckPrinterOnline(string printerName)
        {
            bool online = false;
            try
            {
                PrintDocument printDocument = new PrintDocument();
                printDocument.PrinterSettings.PrinterName = printerName;
                online = printDocument.PrinterSettings.IsValid;
            }
            catch
            {
                online = false;
            }
            return online;
        }

        #region [GetError...]
        [DllImport("KERNEL32", CharSet = CharSet.Auto, BestFitMapping = true)]
        //[ResourceExposure(ResourceScope.None)]
        public static extern int FormatMessage(int dwFlags, IntPtr lpSource,
        int dwMessageId, int dwLanguageId, StringBuilder lpBuffer,
        int nSize, IntPtr va_list_arguments);

        // Gets an error message for a Win32 error code.
        internal static String GetMessage(int errorCode)
        {
            StringBuilder sb = new StringBuilder(512);
            int result = FormatMessage(1,
            IntPtr.Zero, errorCode, 0, sb, sb.Capacity, IntPtr.Zero);
            if (result != 0)
            {
                // result is the # of characters copied to the StringBuilder.
                String s = sb.ToString();
                return s;
            }
            else
            {
                return String.Format("unknown Win32 error code {0:x}", errorCode);
            }
        }
        #endregion [GetError...]

        #region =============================Win32_Printer======================================
        /// <summary>
        /// Some PropertiesName: Name, PrinterStatus, Status
        /// </summary>
        /// <param name="PropertiesName"></param>
        /// <returns></returns>
        public static string GetDefaultPrinterProperties(string PropertiesName)
        {
            string strReturn = "";
            ManagementObjectCollection moReturn;
            ManagementObjectSearcher moSearch;

            moSearch = new ManagementObjectSearcher("Select * from Win32_Printer");
            moReturn = moSearch.Get();

            foreach (ManagementObject mo in moReturn)
            {
                if ((bool)mo["Default"])//<= chi lay Default printer
                {
                    strReturn = string.Format("{0}", mo[PropertiesName]);
                    //strReturn += "\n";
                }
            }
            return strReturn;
        }
        public static string printerStatus()
        {
            string strReturn = "";
            ManagementObjectCollection moReturn;
            ManagementObjectSearcher moSearch;

            moSearch = new ManagementObjectSearcher("Select * from Win32_Printer");
            moReturn = moSearch.Get();

            foreach (ManagementObject mo in moReturn)
            {
                if ((bool)mo["Default"])
                {
                    strReturn += string.Format("{0}:\t\n PrinterStatus {1} - Status {2} - StatusInfo {3} - PaperTypesAvailable {4} - SpoolEnabled {5}",
                                        mo["Name"], mo["PrinterStatus"], mo["Status"], mo["StatusInfo"], mo["PaperTypesAvailable"], mo["SpoolEnabled"]);
                    //strReturn += "\n";
                }
            }

            return strReturn;
        }

        public static StringCollection GetPrintersCollection()
        {
            StringCollection printerNameCollection = new StringCollection();
            string searchQuery = "SELECT * FROM Win32_Printer";
            ManagementObjectSearcher searchPrinters =
                  new ManagementObjectSearcher(searchQuery);
            ManagementObjectCollection printerCollection = searchPrinters.Get();
            foreach (ManagementObject printer in printerCollection)
            {
                printerNameCollection.Add(printer.Properties["Name"].Value.ToString());
            }
            return printerNameCollection;
        }

        public static int hasJob(string printerName, string checkJobName)
        {
            int intReturn = 0;
            //StringCollection printJobCollection = new StringCollection();
            string searchQuery = "SELECT * FROM Win32_PrintJob";

            /*searchQuery can also be mentioned with where Attribute,
                but this is not working in Windows 2000 / ME / 98 machines 
                and throws Invalid query error*/
            ManagementObjectSearcher searchPrintJobs =
                      new ManagementObjectSearcher(searchQuery);
            ManagementObjectCollection prntJobCollection = searchPrintJobs.Get();
            foreach (ManagementObject prntJob in prntJobCollection)
            {
                System.String jobName = prntJob.Properties["Name"].Value.ToString();

                //Job name would be of the format [Printer name], [Job ID]

                char[] splitArr = new char[1];
                splitArr[0] = Convert.ToChar(",");
                string prnterName = jobName.Split(splitArr)[0];
                string documentName = prntJob.Properties["Document"].Value.ToString();
                if (String.Compare(prnterName, printerName, true) == 0)
                {
                    if (documentName == checkJobName)
                    {
                        intReturn++; ;
                    }
                }
            }

            return intReturn;
        }
        public static StringCollection GetPrintJobsCollection(string printerName)
        {
            StringCollection printJobCollection = new StringCollection();
            string searchQuery = "SELECT * FROM Win32_PrintJob";

            /*searchQuery can also be mentioned with where Attribute,
                but this is not working in Windows 2000 / ME / 98 machines 
                and throws Invalid query error*/
            ManagementObjectSearcher searchPrintJobs =
                      new ManagementObjectSearcher(searchQuery);
            ManagementObjectCollection prntJobCollection = searchPrintJobs.Get();
            foreach (ManagementObject prntJob in prntJobCollection)
            {
                System.String jobName = prntJob.Properties["Name"].Value.ToString();

                //Job name would be of the format [Printer name], [Job ID]

                char[] splitArr = new char[1];
                splitArr[0] = Convert.ToChar(",");
                string prnterName = jobName.Split(splitArr)[0];
                string documentName = prntJob.Properties["Document"].Value.ToString();
                if (String.Compare(prnterName, printerName, true) == 0)
                {
                    printJobCollection.Add(documentName);
                }
            }
            return printJobCollection;
        }

        public static int ResumePrintJob(string printerName, string cancelJobName)
        {
            int intReturn = 0;
            //bool isActionPerformed = false;
            string searchQuery = "SELECT * FROM Win32_PrintJob";
            ManagementObjectSearcher searchPrintJobs =
                     new ManagementObjectSearcher(searchQuery);
            ManagementObjectCollection prntJobCollection = searchPrintJobs.Get();
            foreach (ManagementObject prntJob in prntJobCollection)
            {
                System.String jobName = prntJob.Properties["Name"].Value.ToString();
                //Job name would be of the format [Printer name], [Job ID]

                char[] splitArr = new char[1];
                splitArr[0] = Convert.ToChar(",");
                string prnterName = jobName.Split(splitArr)[0];
                int prntJobID = Convert.ToInt32(jobName.Split(splitArr)[1]);
                string documentName = prntJob.Properties["Document"].Value.ToString();
                if (String.Compare(prnterName, printerName, true) == 0)
                {
                    if (documentName.ToLower() == cancelJobName.ToLower())
                    {
                        prntJob.InvokeMethod("Resume", null);
                        intReturn++;
                    }
                }
            }
            return intReturn;
        }

        public static int PausePrintJob(string printerName, string cancelJobName)
        {
            int intReturn = 0;
            //bool isActionPerformed = false;
            string searchQuery = "SELECT * FROM Win32_PrintJob";
            ManagementObjectSearcher searchPrintJobs =
                     new ManagementObjectSearcher(searchQuery);
            ManagementObjectCollection prntJobCollection = searchPrintJobs.Get();
            foreach (ManagementObject prntJob in prntJobCollection)
            {
                System.String jobName = prntJob.Properties["Name"].Value.ToString();
                //Job name would be of the format [Printer name], [Job ID]

                char[] splitArr = new char[1];
                splitArr[0] = Convert.ToChar(",");
                string prnterName = jobName.Split(splitArr)[0];
                int prntJobID = Convert.ToInt32(jobName.Split(splitArr)[1]);
                string documentName = prntJob.Properties["Document"].Value.ToString();
                if (String.Compare(prnterName, printerName, true) == 0)
                {
                    if (documentName.ToLower() == cancelJobName.ToLower())
                    {
                        intReturn++;
                    }
                }
            }
            return intReturn;
        }
        public static bool PausePrintJob(string printerName, int printJobID)
        {
            bool isActionPerformed = false;
            string searchQuery = "SELECT * FROM Win32_PrintJob";
            ManagementObjectSearcher searchPrintJobs =
                     new ManagementObjectSearcher(searchQuery);
            ManagementObjectCollection prntJobCollection = searchPrintJobs.Get();
            foreach (ManagementObject prntJob in prntJobCollection)
            {
                System.String jobName = prntJob.Properties["Name"].Value.ToString();
                //Job name would be of the format [Printer name], [Job ID]

                char[] splitArr = new char[1];
                splitArr[0] = Convert.ToChar(",");
                string prnterName = jobName.Split(splitArr)[0];
                int prntJobID = Convert.ToInt32(jobName.Split(splitArr)[1]);
                string documentName = prntJob.Properties["Document"].Value.ToString();
                if (String.Compare(prnterName, printerName, true) == 0)
                {
                    if (prntJobID == printJobID)
                    {
                        prntJob.InvokeMethod("Pause", null);
                        isActionPerformed = true;
                        break;
                    }
                }
            }
            return isActionPerformed;
        }

        public static bool CancelPrintJob(string printerName, string cancelJobName)
        {
            bool isActionPerformed = false;
            string searchQuery = "SELECT * FROM Win32_PrintJob";
            ManagementObjectSearcher searchPrintJobs =
                   new ManagementObjectSearcher(searchQuery);
            ManagementObjectCollection prntJobCollection = searchPrintJobs.Get();
            foreach (ManagementObject prntJob in prntJobCollection)
            {
                System.String jobName = prntJob.Properties["Name"].Value.ToString();
                //Job name would be of the format [Printer name], [Job ID]

                char[] splitArr = new char[1];
                splitArr[0] = Convert.ToChar(",");
                string prnterName = jobName.Split(splitArr)[0];
                int prntJobID = Convert.ToInt32(jobName.Split(splitArr)[1]);
                string documentName = prntJob.Properties["Document"].Value.ToString();
                if (String.Compare(prnterName, printerName, true) == 0)
                {
                    if (documentName == cancelJobName)
                    {
                        prntJob.Delete();
                        isActionPerformed = true;
                    }
                }
            }
            return isActionPerformed;
        }
        public static bool CancelPrintJob(string printerName, int printJobID)
        {
            bool isActionPerformed = false;
            string searchQuery = "SELECT * FROM Win32_PrintJob";
            ManagementObjectSearcher searchPrintJobs =
                   new ManagementObjectSearcher(searchQuery);
            ManagementObjectCollection prntJobCollection = searchPrintJobs.Get();
            foreach (ManagementObject prntJob in prntJobCollection)
            {
                System.String jobName = prntJob.Properties["Name"].Value.ToString();
                //Job name would be of the format [Printer name], [Job ID]

                char[] splitArr = new char[1];
                splitArr[0] = Convert.ToChar(",");
                string prnterName = jobName.Split(splitArr)[0];
                int prntJobID = Convert.ToInt32(jobName.Split(splitArr)[1]);
                string documentName = prntJob.Properties["Document"].Value.ToString();
                if (String.Compare(prnterName, printerName, true) == 0)
                {
                    if (prntJobID == printJobID)
                    {
                        //performs a action similar to the cancel 

                        //operation of windows print console

                        prntJob.Delete();
                        isActionPerformed = true;
                        break;
                    }
                }
            }
            return isActionPerformed;
        }
        #endregion W32_Printer

        #region [Useless...]
        //private static string kiemTraLoiMayIn(string serverName)
        //{            
        //    PrintServer myPrintServer = new PrintServer(serverName);

        //    // List the print server's queues
        //    PrintQueueCollection myPrintQueues = myPrintServer.GetPrintQueues();
        //    String printQueueNames = "My Print Queues:\n\n";
        //    foreach (PrintQueue pq in myPrintQueues)
        //    {
        //        printQueueNames += "\t" + pq.Name + "\n";
        //    }
        //    //Console.WriteLine(printQueueNames);
        //    return printQueueNames;
        //}
        ////PrintServer myPrintServer = new PrintServer(@"\\theServer");
        //// Check for possible trouble states of a printer using the flags of the QueueStatus property

        //internal static void SpotTroubleUsingQueueAttributes(ref String statusReport,  PrintQueue pq)
        //{
        //    if ((pq.QueueStatus & PrintQueueStatus.PaperProblem) == PrintQueueStatus.PaperProblem)
        //    {
        //        statusReport = statusReport + "Has a paper problem. ";
        //    }
        //    if ((pq.QueueStatus & PrintQueueStatus.NoToner) == PrintQueueStatus.NoToner)
        //    {
        //        statusReport = statusReport + "Is out of toner. ";
        //    }
        //    if ((pq.QueueStatus & PrintQueueStatus.DoorOpen) == PrintQueueStatus.DoorOpen)
        //    {
        //        statusReport = statusReport + "Has an open door. ";
        //    }
        //    if ((pq.QueueStatus & PrintQueueStatus.Error) == PrintQueueStatus.Error)
        //    {
        //        statusReport = statusReport + "Is in an error state. ";
        //    }
        //    if ((pq.QueueStatus & PrintQueueStatus.NotAvailable) == PrintQueueStatus.NotAvailable)
        //    {
        //        statusReport = statusReport + "Is not available. ";
        //    }
        //    if ((pq.QueueStatus & PrintQueueStatus.Offline) == PrintQueueStatus.Offline)
        //    {
        //        statusReport = statusReport + "Is off line. ";
        //    }
        //    if ((pq.QueueStatus & PrintQueueStatus.OutOfMemory) == PrintQueueStatus.OutOfMemory)
        //    {
        //        statusReport = statusReport + "Is out of memory. ";
        //    }
        //    if ((pq.QueueStatus & PrintQueueStatus.PaperOut) == PrintQueueStatus.PaperOut)
        //    {
        //        statusReport = statusReport + "Is out of paper. ";
        //    }
        //    if ((pq.QueueStatus & PrintQueueStatus.OutputBinFull) == PrintQueueStatus.OutputBinFull)
        //    {
        //        statusReport = statusReport + "Has a full output bin. ";
        //    }
        //    if ((pq.QueueStatus & PrintQueueStatus.PaperJam) == PrintQueueStatus.PaperJam)
        //    {
        //        statusReport = statusReport + "Has a paper jam. ";
        //    }
        //    if ((pq.QueueStatus & PrintQueueStatus.Paused) == PrintQueueStatus.Paused)
        //    {
        //        statusReport = statusReport + "Is paused. ";
        //    }
        //    if ((pq.QueueStatus & PrintQueueStatus.TonerLow) == PrintQueueStatus.TonerLow)
        //    {
        //        statusReport = statusReport + "Is low on toner. ";
        //    }
        //    if ((pq.QueueStatus & PrintQueueStatus.UserIntervention) == PrintQueueStatus.UserIntervention)
        //    {
        //        statusReport = statusReport + "Needs user intervention. ";
        //    }

        //    // Check if queue is even available at this time of day
        //    // The method below is defined in the complete example.
        //    //ReportAvailabilityAtThisTime(ref statusReport, pq);
        //}
        #endregion [Useless...]
    }
}
