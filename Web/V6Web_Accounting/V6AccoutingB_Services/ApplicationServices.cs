using System;
using V6AccountingBusiness;
using V6SqlConnect;

namespace V6AccoutingB_Services
{
    public class ApplicationServices : IApplicationServices
    {
        public bool m_FlagCheckConnectFinish { get; set; }
        public bool m_FlagCheckConnectSuccess { get; set; }
        public string m_ExMessage { get; set; }
        private string StartupPath { get; set; }

        public ApplicationServices(string startupPath)
        {
            StartupPath = startupPath;
            m_ExMessage = string.Empty;
            m_FlagCheckConnectFinish = false;
            m_FlagCheckConnectSuccess = false;
        }

        public void DoCheckConect()
        {
            try
            {
                if (!V6BusinessHelper.StartSqlConnect(StartupPath, "V6Soft"))
                    throw new Exception(V6Text.SecretKeyFail);
                if (!SqlConnect.ConnectionOk)
                    throw new Exception(V6Text.NoConnection);
                m_FlagCheckConnectSuccess = true;
            }
            catch (Exception ex)
            {
                m_ExMessage = ex.Message;
                m_FlagCheckConnectSuccess = false;
            }
            m_FlagCheckConnectFinish = true;
        }
    }
}
