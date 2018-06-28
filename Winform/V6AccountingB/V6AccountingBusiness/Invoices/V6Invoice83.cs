using System;
using System.Collections.Generic;
using System.Data;
using DataAccessLayer.Implementations.Invoices;
using V6Init;
using V6SqlConnect;

namespace V6AccountingBusiness.Invoices
{
    /// <summary>
    /// SOC: Phiếu bán lẻ
    /// </summary>
    public class V6Invoice83 : V6InvoiceBase
    {
        private readonly Invoice83Services Service83 = new Invoice83Services();

        public V6Invoice83():base("SOC", "00SOC")
        {
            
        }

        public override string PrintReportProcedure
        {
            get { return "ASOCTSOC"; }
        }

        public override string Name { get { return "Phiếu bán lẻ"; } }
        
        public bool InsertInvoice(SortedDictionary<string, object> am,
            List<SortedDictionary<string, object>> adList,
            List<SortedDictionary<string, object>> adList3,
            bool post)
        {
            return Service83.InsertInvoice(V6Login.UserId, AMStruct, ADStruct, AD3Struct,
                am, adList, adList3, WRITE_LOG, out V6Message, post);
        }
        
        public bool UpdateInvoice(SortedDictionary<string, object> am,
            List<SortedDictionary<string, object>> adList,
            List<SortedDictionary<string, object>> adList3,
            SortedDictionary<string,object> keys, bool post)
        {
            return Service83.UpdateInvoice(V6Login.UserId, AMStruct, ADStruct, AD3Struct,
                am, adList, adList3, keys, WRITE_LOG, out V6Message, post);
        }

        public DataTable SearchAM(string where0Ngay, string where1AM, string where2AD, string where3NhVt, string where4Dvcs)
        {
            var filterKho = V6Login.GetFilterKho("MA_KHO");
            if (!string.IsNullOrEmpty(filterKho))
            {
                where4Dvcs += (string.IsNullOrEmpty(where4Dvcs) ? "" : " and ") + filterKho;
            }
            return Service83.SearchAM(AM_TableName, AD_TableName, Mact, where0Ngay, where1AM, where2AD, where3NhVt, where4Dvcs);
        }

        public DataTable LoadAd(string sttRec)
        {
            return Service83.LoadAd(AD_TableName, sttRec);
        }
        public DataTable LoadAD3(string sttRec)
        {
            return Service83.LoadAD3(AD3_TableName, sttRec);
        }
        
        public bool DeleteInvoice(string sttrec)
        {
            try
            {
                return Service83.DeleteInvoice(V6Login.UserId, Mact, sttrec);
            }
            catch (Exception ex)
            {
                V6Message = ex.Message;
                return false;
            }
        }

        public DataRow GetGiaBan(string field, string mact, DateTime ngayct,
            string mant, string mavt, string dvt1, string makh, string magia)
        {
            try
            {
                var data = Service83.GetGiaBan(field, mact, ngayct, mant, mavt, dvt1, makh, magia);
                return data.Rows[0];

                //return "{gia_ban_nt:" + data.Rows[0]["gia_ban_nt"] + ",gia_nt2:" + data.Rows[0]["gia_nt2"] + ",error:0,message:''}";
            }
            catch (Exception ex)
            {
                //return "{gia_ban_nt:0,gia_nt2:0,error:1,message:\"GetPrice: " + ToJSstring(ex.Message) + "\"}";
                throw new Exception("V6Invoice83 GetGiaBan " + ex.Message);
            }
        }
        
    }

}
