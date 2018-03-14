using System;
using System.Data;

namespace DataAccessLayer.Interfaces
{
    public interface IInvoiceServices
    {
        DataTable GetAlct(string mact);
        DataTable GetAlct1(string mact);
        DataTable GetAlct3(string mact);
        DataTable GetAlnt();
        DataTable GetAlPost(string mact);
        void PostErrorLog(string mact, string sttRec, string mode, string message);
        decimal GetTyGia(string mant, DateTime ngayct);
        DataSet GetCheck_VC(string status, string kieuPost, string sttRec, out string message);
        DataTable GetCheck_VC_Save(string status, string kieuPost, string soct, string masonb, string sttrec, out string v6Message);
        void IncreaseSl_inAM(string am_table, string ma_ct, string sttRec);
    }
}
