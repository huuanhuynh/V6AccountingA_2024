﻿using System.Collections.Generic;
using V6Tools;

namespace V6AccountingBusiness.V6Init.User
{
    /// <summary>
    /// Cần nâng cấp phần value.
    /// </summary>
    public class UserXmlInfo
    {
        private SortedDictionary<string, string> _infoData = new SortedDictionary<string, string>(); 
        public UserXmlInfo(IDictionary<string, object> data)
        {
            _infoData.AddRange(data);
        }

        public SortedDictionary<string, string> DataDic { get { return _infoData; } }

        public string Email
        {
            get{ if (_infoData.ContainsKey("EMAIL")) return _infoData["EMAIL"]; return ""; }
        }
        public string EmailPassword
        {
            get { if (_infoData.ContainsKey("EMAILPASSWORD")) return UtilityHelper.DeCrypt(_infoData["EMAILPASSWORD"]); return ""; }
        }
        public string TEN_NLB_LOGIN
        {
            get { if (_infoData.ContainsKey("TEN_NLB_LOGIN")) return _infoData["TEN_NLB_LOGIN"]; return ""; }
        }
        public string TEN_NLB_LOGIN2
        {
            get { if (_infoData.ContainsKey("TEN_NLB_LOGIN2")) return _infoData["TEN_NLB_LOGIN2"]; return ""; }
        }
        
    }
}
