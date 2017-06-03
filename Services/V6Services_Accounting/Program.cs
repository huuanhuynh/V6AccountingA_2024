using System.ServiceProcess;

using V6Soft.Services.Accounting;


namespace V6Services_Accounting
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new AccountingWindowsService() 
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
