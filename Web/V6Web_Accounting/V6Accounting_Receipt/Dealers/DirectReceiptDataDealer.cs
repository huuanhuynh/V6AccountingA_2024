using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.OData.Query;

using V6Soft.Accounting.Common.Dealers;
using V6Soft.Accounting.Receipt.Farmers;
using V6Soft.Common.Utils;
using DTO = V6Soft.Models.Accounting.DTO;

namespace V6Soft.Accounting.Receipt.Dealers
{
    public class DirectReceiptDataDealer : DataDealerBase, IReceiptDataDealer
    {
        protected IReceipt81DataFarmer m_ReceiptFarmer;

        public DirectReceiptDataDealer(IReceipt81DataFarmer receiptFarmer)
            : base(receiptFarmer.AsQueryable())
        {
            m_ReceiptFarmer = receiptFarmer;
        }

        [Obsolete]
        public IQueryable<DTO.Receipt> AsQueryable()
        {
            return m_ReceiptFarmer.AsQueryable();
        }

        public IQueryable<DTO.Receipt> AsQueryable(ODataQueryOptions<DTO.Receipt> queryOptions)
        {
            return (IQueryable<DTO.Receipt>)queryOptions.ApplyTo(m_ReceiptFarmer.AsQueryable());
        }

        public IQueryable<DTO.ReceiptDetail> AsQueryable(ODataQueryOptions<DTO.ReceiptDetail> queryOptions)
        {
            return (IQueryable<DTO.ReceiptDetail>)queryOptions.ApplyTo(m_ReceiptFarmer.AsQueryable());
        }

        public DTO.Receipt GetReceipt(Guid guid)
        {
            return m_ReceiptFarmer.AsQueryable().SingleOrDefault(re => re.UID.Equals(guid));
        }

        public DTO.ReceiptDetail GetReceiptDetail(Guid guid)
        {
            // m_ReceiptDetailFarmer.....
            return null;
        }

        public DTO.Receipt AddReceipt(DTO.Receipt receipt)
        {
            return null;
        }

        public DTO.ReceiptDetail AddReceiptDetail(DTO.ReceiptDetail receipt)
        {
            return null;
        }

        public DTO.Receipt UpdateReceipt(DTO.Receipt receipt)
        {
            return null;
        }

        public void Save(IList<DynamicObject> models)
        {
            
        }

        #region  Testing purpose

        public List<DTO.Alct1> TestGetAlct1()
        {
            return m_ReceiptFarmer.TestGetAlct1().ToList();
        }
        #endregion

        public IEnumerable<DTO.GettingKieuPostInInvoiceBase> GetAlPost()
        {
            return m_ReceiptFarmer.GetAlPost();
        }

        public DTO.GettingCheckVcSave GetCheckVcSave(string status, string kieu_post, string soct, string masonb, string sttrec)
        {
            return m_ReceiptFarmer.GetCheck_VC_Save( status,  kieu_post,  soct,  masonb,  sttrec);
        }

        public DTO.AlCt AlCt()
        {
            return m_ReceiptFarmer.AlCt();
        }


        public decimal GetTyGia(string mant, DateTime ngayct)
        {
            return m_ReceiptFarmer.GetTyGia(mant, ngayct);
        }

        public void PostErrorLog(string sttRec, string mode, string message = null)
        {
            m_ReceiptFarmer.PostErrorLogs(sttRec, mode, message);
        }
        public DTO.ALnt Alnt()
        {
            return m_ReceiptFarmer.ALnt();
        }

        public bool SearchAD81(string sttrec)
        {
            return m_ReceiptFarmer.SearchAD81(sttrec);
        }

        //public DataTable GetLoDate(string mavt, string makho, string sttRec, DateTime ngayct)
        //{
        //    return m_ReceiptFarmer.GetLoDate(makho, mavt, sttRec, ngayct);
        //}

        //public DataTable GetStock(string mavt, string makho, string sttRec, DateTime ngayct)
        //{
        //    return m_ReceiptFarmer.GetStock(mavt, makho, sttRec, ngayct);
        //}
        public DTO.GetLoDate13 GetLoDate13(string mavt, string makho, string malo, string sttRec, DateTime ngayct)
        {
            return m_ReceiptFarmer.GetLoDate13(mavt, makho, malo, sttRec, ngayct);
        }

        public DTO.GetGiaBan GetGiaBan(string field, string mact, DateTime ngayct, string mant, string mavt, string dvt1,
            string makh, string magia)
        {
            return m_ReceiptFarmer.GetGiaBan(field, mact, ngayct, mant, mavt, dvt1, makh, magia);
        }

        public DTO.GetLoDate GetLoDate(string mavt, string makho, string sttRec, DateTime ngayct)
        {
            return m_ReceiptFarmer.GetLoDate(makho, mavt, sttRec, ngayct);
        }

        public DTO.CheckTonXuatAm GetStock(string mavt, string makho,string sttRec, DateTime ngayct)
        {
            return m_ReceiptFarmer.GetStock(mavt, makho ,sttRec, ngayct);

        }

        public DTO.GetCheckVC GetCheckVC(string status, string kieu_post, string soct, string masonb, string sttrec)
        {
            return m_ReceiptFarmer.GetCheckVC(status, kieu_post, soct, masonb, sttrec);
        }
        public List<DTO.Ad81> LoadAd81(string sttRec)
        {
            return m_ReceiptFarmer.LoadAd81(sttRec);
        }
        //public DataTable SearchAD81(string where0, string where1, string where2)
        //{
        //    return m_ReceiptFarmer.SearchAD81(where0, where1, where2);
        //}
    }
}
