using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

using AutoMapper.QueryableExtensions;
using V6Soft.Accounting.Farmers.EnFw.AutoMapper;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Accounting.Receipt.Farmers;

using DTO = V6Soft.Models.Accounting.DTO;
using System.Data;
using System.Data.Common;
using V6Soft.Accounting.Farmers.EnFw.Extension;
using AutoMapper;


namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    public class MssqlReceipt81DataFarmer : MssqlReceiptDataFarmerBase<AM81, DTO.Receipt>, IReceipt81DataFarmer
    {
        private IModelMapper m_ModelMapper;

        private DbConnection Connection
        {
            get
            {
                return m_DbContext.Database.Connection;
            }
        }

        public MssqlReceipt81DataFarmer(IV6AccountingContext dbContext, IModelMapper modelMapper)
            : base(dbContext)
        {
            m_ModelMapper = modelMapper;
        }

        public new IList<DTO.Receipt> GetAll()
        {
            throw new NotImplementedException();
        }

        public new Models.Core.ViewModels.PagedSearchResult<DTO.Receipt> FindByCriteria(Models.Core.SearchCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public DTO.Receipt Add(DTO.Receipt entity)
        {
            throw new NotImplementedException();
        }

        public bool Edit(DTO.Receipt entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<DTO.Receipt> AsQueryable()
        {
            return m_Dbset.ProjectTo<DTO.Receipt>();
        }

        public IEnumerable<DTO.Alct1> TestGetAlct1()
        {
            return GetAlct1();
        }

        public IEnumerable<DTO.GettingKieuPostInInvoiceBase> GetAlPost()
        {
            return AlPost();
        }

        public DTO.GettingCheckVcSave GetCheck_VC_Save(string status, string kieu_post, string soct, string masonb, string sttrec)
        {
            return GetCheckVcSave(status, kieu_post, soct, masonb, sttrec);
        }

        public DTO.AlCt AlCt()
        {
            var entity = this.Alct;
            return m_ModelMapper.Map<Alct, DTO.AlCt>(entity);
        }

        decimal GetTyGia(string mant, DateTime ngayct)
        {
            return this.GetTyGia(mant, ngayct);
        }

        public void PostErrorLogs(string sttRec, string mode, string message = null)
        {
            PostErrorLog(sttRec, mode, message);

        }
        public DTO.ALnt ALnt()
        {
            var alct1Entity = this.Alnt;
            return m_ModelMapper.Map<Alnt, DTO.ALnt>(alct1Entity);
        }
        protected override DTO.Receipt ToAppModel(AM81 model)
        {
            throw new NotImplementedException();
        }

        protected override AM81 ToEntityModel(DTO.Receipt model)
        {
            throw new NotImplementedException();
        }

        public DTO.GetCheckVC GetCheckVC(string status, string kieu_post, string soct, string masonb, string sttrec)
        {
            return GetCheckVc(status,kieu_post,soct,masonb,sttrec);
        }

        public bool SearchAD81(string sttrec)
        {
            var connectionString = m_DbContext.Database.Connection.ConnectionString;
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Ma_ct", "SOA"),
                new SqlParameter("@Stt_rec", sttrec),
                //new SqlParameter("@UserID", V6LoginInfo.UserId),   // error UserId
            };
            ExecuteSqlStoredProcedure("VPA_CHECK_VC_SAVE",
                connectionString, parameters);
            return true;
        }

        public DTO.GetLoDate13 GetLoDate13(string mavt, string makho, string malo, string sttRec, DateTime ngayct)
        {
            var Mact = "SOA";
            var connectionString = m_DbContext.Database.Connection.ConnectionString;
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@cKey1", string.Format("Ma_vt = '"+mavt+"' and Ma_kho = '"+makho+"' and Ma_lo = '"+malo+"'")),
                new SqlParameter("@cKey2", ""),
                new SqlParameter("@cKey3", ""),   
                new SqlParameter("@cStt_rec",sttRec ),
                new SqlParameter("@dBg",ngayct),
            };
            var storeResult = ExecuteSqlStoredProcedure("VPA_EdItems_DATE_STT_REC",
                  connectionString, parameters).Tables[0];
            return DataTableHelper.ConvertToObject<DTO.GetLoDate13>(storeResult);
        }

        public DTO.GetLoDate GetLoDate(string mavt, string makho, string sttRec, DateTime ngayct)

        {
            var connectionString = m_DbContext.Database.Connection.ConnectionString;
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@cKey1", string.Format("Ma_vt = '" + mavt + "' and Ma_kho = '" + makho + "'")),
                new SqlParameter("@cKey2", ""),
                new SqlParameter("@cKey3", ""),
                new SqlParameter("@cStt_rec", sttRec),
                new SqlParameter("@dBg", ngayct)
            };
            var storeResult = ExecuteSqlStoredProcedure("VPA_EdItems_DATE_STT_REC", connectionString, parameters).Tables[0];
            return DataTableHelper.ConvertToObject<DTO.GetLoDate>(storeResult);
        }
    

        public DTO.CheckTonXuatAm GetStock(string mavt, string makho, string sttRec, DateTime ngayct)
        {
            var connectionString = m_DbContext.Database.Connection.ConnectionString;
            var Mact = "SOA";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Type", 1),
                new SqlParameter("@Ngay_ct", ngayct),
                new SqlParameter("@ma_ct", Mact),
                new SqlParameter("@Stt_rec", sttRec),
                new SqlParameter("@Advance", string.Format("a.MA_VT='" + mavt + "' AND a.MA_KHO='" + makho + "'"))
            };
            var storeResult = ExecuteSqlStoredProcedure("VPA_CheckTonXuatAm", connectionString, parameters).Tables[0];
            var checkTonXuatAmResult = DataTableHelper.ConvertToObject<DTO.CheckTonXuatAm>(storeResult);
            return checkTonXuatAmResult;
        }

        public DataTable SearchReceipt(string where0Ngay, string where1AM, string where2AD, string where3NhVt, string where4Dvcs)
        {
            string template =
                "Select a.*, b.Ma_so_thue, b.Ten_kh AS Ten_kh,f.Ten_nvien AS Ten_nvien,g.Ten_httt AS Ten_httt"
                + "\nFROM " + AM + " a LEFT JOIN Alkh b ON a.Ma_kh=b.Ma_kh LEFT JOIN alnvien f ON a.Ma_nvien=f.Ma_nvien"
                + "\n LEFT JOIN alhttt AS g ON a.Ma_httt = g.Ma_httt  JOIN "
                + "\n (SELECT Stt_rec FROM " + AM + " WHERE Ma_ct = '" + Mact + "'"
                + "\n {0} {1} {2}) AS m ON a.Stt_rec = m.Stt_rec"
                + "\n ORDER BY a.ngay_ct, a.so_ct, a.stt_rec";
            if (where0Ngay.Length > 0) where0Ngay = "And " + where0Ngay;
            if (where1AM.Length > 0) where1AM = "And " + where1AM;
            var p2Template =
                "\n--{0}{1}\nAnd Stt_rec in (SELECT Stt_rec FROM " + AD + " WHERE Ma_ct = '" + Mact + "' {2}"
                + "\n		And Ma_vt IN (SELECT Ma_vt FROM Alvt WHERE 1 = 1 {3})"
                + "\n		{4})";//" And Ma_kho_i IN (SELECT Ma_kho FROM Alkho WHERE 1 = 1 {4})"
            if (where2AD.Length > 0 || where3NhVt.Length > 0 || where4Dvcs.Length > 0)
            {
                if (where2AD.Length > 0) where2AD = "And " + where2AD;
                if (where3NhVt.Length > 0) where3NhVt = "And " + where3NhVt;
                if (where4Dvcs.Length > 0) where4Dvcs
                    = string.Format("	And Ma_kho_i IN (SELECT Ma_kho FROM Alkho WHERE 1 = 1 and {0})", where4Dvcs);
                p2Template = string.Format(p2Template, "", "", where2AD, where3NhVt, where4Dvcs);
            }
            else
            {
                p2Template = "";
            }

            var sql = string.Format(template, where0Ngay, where1AM, p2Template);
            DataTable tbl = null; //SqlConnect.ExecuteDataset(CommandType.Text, sql, Connection).Tables[0];
            
            return tbl;
        }


        public DTO.GetGiaBan GetGiaBan(string field, string mact, DateTime ngayct,
            string mant, string mavt, string dvt1, string makh, string magia)
        {   
            var connectionString = m_DbContext.Database.Connection.ConnectionString;
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@cField", field),
                new SqlParameter("@cVCID", mact),
                new SqlParameter("@dPrice", ngayct),
                new SqlParameter("@cFC",mant),
                new SqlParameter("@cItem",mavt),
                new SqlParameter("@cUOM",dvt1),
                new SqlParameter("@cCust",makh),
                new SqlParameter("@cMaGia",magia),
            };
            var storeResult = ExecuteSqlStoredProcedure("VPA_GetSOIDPrice",
                  connectionString, parameters).Tables[0];
            var checkTonXuatAmResult = DataTableHelper.ConvertToObject<DTO.GetGiaBan>(storeResult);
            return checkTonXuatAmResult;
        }
        //public DataTable SearchAD81(string where0, string where1, string where2)
        //{
        //    var connectionString = m_DbContext.Database.Connection.ConnectionString;
        //    if (where0.Length > 0) where0 = "And " + where0;
        //    if (where1.Length > 0) where1 = "And " + where1;
        //    if (where2.Length > 0) where2 = "And " + where2;
        //    var storeResult =
        //        ExecuteStoredProcedure("Select a.*, b.Ma_so_thue, b.Ten_kh AS Ten_kh,f.Ten_nvien AS Ten_nvien,g.Ten_httt AS Ten_httt"
        //                               + "\nFROM " + AM +
        //                               " a LEFT JOIN Alkh b ON a.Ma_kh=b.Ma_kh LEFT JOIN alnvien f ON a.Ma_nvien=f.Ma_nvien"
        //                               + "\n LEFT JOIN alhttt AS g ON a.Ma_httt = g.Ma_httt  JOIN "
        //                               + "\n (SELECT Stt_rec FROM " + AM + " WHERE Ma_ct = '" + Mact + "'"
        //                               + "\n {0}"
        //                               + "\n And Stt_rec in (SELECT Stt_rec FROM " + AD + " WHERE Ma_ct = '" + Mact +
        //                               "' {1}"
        //                               + "\n		And Ma_vt IN (SELECT Ma_vt FROM Alvt WHERE 1 = 1 {2}"
        //                               + "\n))AS m ON a.Stt_rec = m.Stt_rec",connectionString,where0,where1,where2);
        //    return storeResult;
        //}
        public List<DTO.Ad81> LoadAd81(string sttRec)
        {
            var connectionString = m_DbContext.Database.Connection.ConnectionString;
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@sttRec", sttRec)
            };
            var query = "SELECT c.*,d.Ten_vt AS Ten_vt, c.So_luong1*0 as Ton13 FROM [" + AD
                        + "] c LEFT JOIN Alvt d ON c.Ma_vt= d.Ma_vt  Where c.stt_rec = @sttRec";
            var dataResult = ExecuteSqlQuery(query, connectionString, parameters).Tables[0];
            return DataTableHelper.ConvertToList<DTO.Ad81>(dataResult).ToList();
        }
    }
}
