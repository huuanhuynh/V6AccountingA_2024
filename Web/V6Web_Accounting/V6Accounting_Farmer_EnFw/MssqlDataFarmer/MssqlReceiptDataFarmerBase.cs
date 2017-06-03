using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using V6Soft.Accounting.Farmers.EnFw.Extension;
using V6Soft.Models.Core;
using DTO = V6Soft.Models.Accounting.DTO;
using Entity = V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    public abstract class MssqlReceiptDataFarmerBase<TEntity, TV6Model>
        : EnFwDataFarmerBase<TEntity, TV6Model>
        where TEntity : class
        where TV6Model : V6Model
    {
        protected MssqlReceiptDataFarmerBase(Entity.IV6AccountingContext context)
            : base(context)
        {
            // Load AM
            // Load AD
        }

        public virtual string Mact { get; set; } // = "SOA";
        public virtual string CodeMact { get; set; }
        public string V6Message = "";

        //protected V6TableStruct _amStruct, _adStruct, _ad2Struct;
        protected Entity.Alnt _alnt; //DataTable
        protected DataTable _alpost;

        protected Entity.Alct _alct;
        protected IEnumerable<DTO.Alct1> _alct1;
        public static DataRow UserInfo { get; set; }
        
        /// <summary>
        /// Tên bảng dữ liệu AM
        /// </summary>
        public string AM
        {
            get
            {
                return Alct.MPhdbf;
            }
        }

        /// <summary>
        /// Tên bảng dữ liệu AD
        /// </summary>
        public string AD
        {
            get
            {
                return Alct.MCtdbf;
            }
        }

        public Entity.Alnt Alnt
        {
            get
            {
                var Mant = "VND";
                var a = _alnt ?? (_alnt = m_DbContext.Set<Entity.Alnt>().OrderBy(m => m.MaNt == Mant).FirstOrDefault());
                return a;
            }
        }

        public Entity.Alct Alct
        {
            get
            {
                var Mact = "SOA";
                if (_alct == null)
                {
                    _alct = m_DbContext.Set<Entity.Alct>().Where(ct => ct.MaCT == Mact).FirstOrDefault();
                }
                return _alct;
            }
        }

        /// <summary>
        /// DataTable
        /// </summary>
        public virtual IEnumerable<DTO.Alct1> Alct1
        {
            get
            {
                _alct1 = _alct1 ?? GetAlct1();
                return _alct1;
            }
        }

        /// <summary>
        /// Lấy trường động tren form chi tiết
        /// </summary>
        /// <returns>Một DataTable</returns>
        public IEnumerable<DTO.Alct1> GetAlct1()
        {
            var maCt = new SqlParameter("@ma_ct", "SOA");
            var listFix = new SqlParameter("@list_fix", string.Empty);
            var orderFix = new SqlParameter("@order_fix", string.Empty);
            var vVarFix = new SqlParameter("@vvar_fix", string.Empty);
            var typeFix = new SqlParameter("@type_fix", string.Empty);
            var checkVVarFix = new SqlParameter("@checkvvar_fix", string.Empty);
            var notEmptyFix = new SqlParameter("@notempty_fix", string.Empty);
            var fDecimalFix = new SqlParameter("@fdecimal_fix", string.Empty);
            //object[] parameters = { maCt, listFix, orderFix, vVarFix, typeFix, checkVVarFix, notEmptyFix, fDecimalFix };

            var result = Execute<DTO.Alct1>("exec VPA_GET_AUTO_COLULMN @ma_ct, @list_fix, @order_fix, @vvar_fix, @type_fix, @checkvvar_fix, @notempty_fix, @fdecimal_fix", maCt, listFix, orderFix, vVarFix, typeFix, checkVVarFix, notEmptyFix, fDecimalFix).ToList();
            return result;
        }

        /// <summary>
        /// Kiểu post gì, user chọn trên form.
        /// </summary>
        /// <returns>DataTable</returns>
        public IEnumerable<DTO.GettingKieuPostInInvoiceBase> AlPost()
        {
            {
                var Mact = "SOA";
                return m_DbContext.Set<Entity.ALpost>()
                        .Where(x => x.MaCT == Mact)
                        .OrderBy(y => y.KieuPost)
                        .Select(x => new DTO.GettingKieuPostInInvoiceBase()
                        {
                            KieuPost = x.KieuPost,
                            TenPost = x.TenPost,
                            TenPost2 = x.TenPost2,
                        });
            }
        }

        /// <summary>
        /// Các trường động, cho lọc AM
        /// </summary>
        public string ADV_AM
        {
            get
            {
                return Alct.AdvAm;
            }
        }

        /// <summary>
        /// Các trường động, cho lọc AD
        /// </summary>
        public string ADV_AD
        {
            get
            {
                return Alct.AdvAd;
            }
        }

        public void PostErrorLog(string sttRec, string mode, string message = null)
        {
            var connectionString = m_DbContext.Database.Connection.ConnectionString;
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@ma_ct", "SOA"),
                new SqlParameter("@stt_rec", string.Empty),
                new SqlParameter("@mode", string.Empty),
                new SqlParameter("@message", string.Empty),
                new SqlParameter("@message_id", string.Empty),
            };
            ExecuteSqlStoredProcedure("VPA_V6_PostErrorLog",
                connectionString, parameters);
        }

        public decimal GetTyGia(string mant, DateTime ngayct)
        {
            var mantParam = new SqlParameter("@ma_nt", mant);
            var ngayctParam = new SqlParameter("@ngay_ct", ngayct);
            decimal tygia = m_DbContext.Database.SqlQuery<decimal>(
                "Select dbo.VFA_GetRates(@ma_nt, @ngay_ct)",
                mantParam, ngayctParam).Single();
            return tygia;
        }

        public DTO.GetCheckVC GetCheckVc(string status, string kieu_post, string soct, string masonb, string sttrec)
        {
            try
            {
                var connectionString = m_DbContext.Database.Connection.ConnectionString;
                var parameters = new List<SqlParameter>
            {
                new SqlParameter("@status", status),
                new SqlParameter("@kieu_post", kieu_post),
                new SqlParameter("@Stt_rec", sttrec),
            };
                var storeResult = ExecuteSqlStoredProcedure("VPA_TA1_CHECK_VC", connectionString, parameters).Tables[0];
                return DataTableHelper.ConvertToObject<DTO.GetCheckVC>(storeResult);
            }
            catch (Exception ex)
            {
                V6Message = ex.Message;
                return null;
            }
        }

        public DTO.GettingCheckVcSave GetCheckVcSave(string status, string kieu_post, string soct, string masonb, string sttrec)
            {
            var connectionString = m_DbContext.Database.Connection.ConnectionString;
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@status", status),
                new SqlParameter("@kieu_post", kieu_post),
                new SqlParameter("@So_ct", soct),
                new SqlParameter("@Ma_sonb", masonb),
                new SqlParameter("@Stt_rec", sttrec),
            };
            var storeResult = ExecuteSqlStoredProcedure("VPA_CHECK_VC_SAVE", connectionString, parameters).Tables[0];
            return DataTableHelper.ConvertToObject<DTO.GettingCheckVcSave>(storeResult);
            //var connectionString = m_DbContext.Database.Connection.ConnectionString;
            //var parameters = new List<SqlParameter>
            //{
            //    new SqlParameter("@cKey1", string.Format("Ma_vt = '" + mavt + "' and Ma_kho = '" + makho + "'")),
            //    new SqlParameter("@cKey2", ""),
            //    new SqlParameter("@cKey3", ""),
            //    new SqlParameter("@cStt_rec", sttRec),
            //    new SqlParameter("@dBg", ngayct)
            //};
            //var storeResult = ExecuteSqlStoredProcedure("VPA_EdItems_DATE_STT_REC", connectionString, parameters).Tables[0];
            //return DataTableHelper.ConvertToObject<DTO.GetLoDate>(storeResult);
        }
    }
}
