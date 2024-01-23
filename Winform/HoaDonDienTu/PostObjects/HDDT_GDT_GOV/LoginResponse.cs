using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V6ThuePost.HDDT_GDT_GOV
{
    public class LoginResponse
    {
        public string token { get; set; }
        /// <summary>
        /// dd/MM/yyyy HH:mm:ss
        /// </summary>
        public string timestamp { get; set; }
        public string message { get; set; }
        public string details { get; set; }
        public string path { get; set; }
    }
}
