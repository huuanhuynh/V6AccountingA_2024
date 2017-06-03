using System.ServiceProcess;
using V6Soft.Services.Assistant;

namespace V6Services_Assistant
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new AssistantWindowsService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
