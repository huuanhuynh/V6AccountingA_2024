using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using V6Soft.Accounting.Common.Farmers;

using DTO = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.Receipt.Farmers
{
    public interface IReceipt81DataFarmer : IDataFarmerBase<DTO.Receipt>
    {
        // TODO: Should put this method in IDataFarmerBase
        /// <summary>
        /// 
        /// </summary>
        IQueryable<DTO.Receipt> AsQueryable();
        decimal GetTyGia(string mant, DateTime ngayct);
        IEnumerable<DTO.Alct1> TestGetAlct1();
        IEnumerable<DTO.GettingKieuPostInInvoiceBase> GetAlPost();
        DTO.GettingCheckVcSave GetCheck_VC_Save(string status, string kieu_post, string soct, string masonb, string sttrec);
        DTO.AlCt AlCt();
       void PostErrorLogs(string sttRec, string mode, string message = null);
        DTO.ALnt ALnt();
        bool SearchAD81(string sttrec);

        //DataTable GetLoDate(string mavt, string makho, string sttRec, DateTime ngayct);
        //DataTable GetStock(string mavt, string makho, string sttRec, DateTime ngayct);
        DTO.GetLoDate13 GetLoDate13(string mavt, string makho, string malo, string sttRec, DateTime ngayct);

        DTO.GetGiaBan GetGiaBan(string field, string mact, DateTime ngayct, string mant, string mavt, string dvt1, string makh,
            string magia);

        DTO.GetLoDate GetLoDate(string mavt, string makho, string sttRec, DateTime ngayct);
        DTO.CheckTonXuatAm GetStock(string mavt, string makho, string sttRec, DateTime ngayct);
        DTO.GetCheckVC GetCheckVC(string status, string kieu_post, string soct, string masonb, string sttrec);
        List<DTO.Ad81> LoadAd81(string sttRec);
       // DataTable SearchAD81(string where0, string where1, string where2);
    }
}
