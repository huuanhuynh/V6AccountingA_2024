namespace V6AccoutingB_Services
{
    public interface IApplicationServices
    {
        bool m_FlagCheckConnectFinish { get; set; }
        bool m_FlagCheckConnectSuccess { get; set; }
        string m_ExMessage { get; set; }
        void DoCheckConect();
    }
}
