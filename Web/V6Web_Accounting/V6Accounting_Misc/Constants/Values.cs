using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace V6Soft.Accounting.Misc.Constants
{
    public class Values
    {
        /// <summary>
        ///     Service Information
        /// </summary>
        public static class ServiceInformation
        {
            
            /// <summary>
            ///     Assistant Service Address Formula
            /// </summary>
            public const string AssistantServiceAddressFormula = @"net.tcp://localhost:14229/AssistantService"; 

            /// <summary>
            ///     Asisstant Service Binding Name, which is in web.config file.
            /// </summary>
            public const string AssistantServiceBindingName = "NetTcpBinding_IAssistantService";
        }
    }
}
