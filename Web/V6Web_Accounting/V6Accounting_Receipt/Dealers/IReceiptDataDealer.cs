using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.OData.Query;
using V6Soft.Accounting.Common.Dealers;

using DTO = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.Receipt.Dealers
{
    public interface IReceiptDataDealer : IODataFriendly<DTO.Receipt>
    {
        IQueryable<DTO.Receipt> AsQueryable(ODataQueryOptions<DTO.Receipt> queryOptions);
        IQueryable<DTO.ReceiptDetail> AsQueryable(ODataQueryOptions<DTO.ReceiptDetail> queryOptions);
        DTO.Receipt GetReceipt(Guid guid);
        DTO.ReceiptDetail GetReceiptDetail(Guid guid);
        DTO.Receipt AddReceipt(DTO.Receipt receipt);
        DTO.ReceiptDetail AddReceiptDetail(DTO.ReceiptDetail receipt);
        DTO.Receipt UpdateReceipt(DTO.Receipt receipt);
        List<DTO.Alct1> TestGetAlct1();
        DTO.GettingCheckVcSave GetCheckVcSave(string status, string kieu_post, string soct, string masonb, string sttrec);
        IEnumerable<DTO.GettingKieuPostInInvoiceBase> GetAlPost();
        DTO.AlCt AlCt();
        DTO.ALnt Alnt();
        bool SearchAD81(string sttrec);
        decimal GetTyGia(string mant, DateTime ngayct);
        void PostErrorLog(string sttRec, string mode, string message = null);

        //DataTable GetLoDate(string mavt, string makho, string sttRec, DateTime ngayct);
        //DataTable GetStock(string mavt, string makho, string sttRec, DateTime ngayct);
        DTO.GetLoDate13 GetLoDate13(string mavt, string makho, string malo, string sttRec, DateTime ngayct);

        DTO.GetGiaBan GetGiaBan(string field, string mact, DateTime ngayct,
            string mant, string mavt, string dvt1, string makh, string magia);

        DTO.GetLoDate GetLoDate(string mavt, string makho, string sttRec, DateTime ngayct);
        DTO.CheckTonXuatAm GetStock(string mavt, string makho, string sttRec, DateTime ngayct);
        DTO.GetCheckVC GetCheckVC(string status, string kieu_post, string soct, string masonb, string sttrec);
        List<DTO.Ad81> LoadAd81(string sttRec);
      //  DataTable SearchAD81(string where0, string where1, string where2);

    }
}
