using System.Collections.Generic;
using V6Tools;

namespace V6AccountingBusiness.V6Init.User
{
    public class UserXmlInfo
    {
        private IDictionary<string, string> _infoData = new SortedDictionary<string, string>(); 
        public UserXmlInfo(IDictionary<string, object> data)
        {
            _infoData.AddRange(data);
        }

        public string Email
        {
            get{ if (_infoData.ContainsKey("EMAIL")) return _infoData["EMAIL"]; return ""; }
        }
        public string EmailPassword
        {
            get { if (_infoData.ContainsKey("EMAILPASSWORD")) return UtilityHelper.DeCrypt(_infoData["EMAILPASSWORD"]); return ""; }
        }
    }
}
