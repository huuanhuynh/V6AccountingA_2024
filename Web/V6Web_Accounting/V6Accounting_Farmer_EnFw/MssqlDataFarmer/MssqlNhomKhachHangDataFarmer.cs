using System.Collections.Generic;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Accounting.NhomKhachHang.Farmers;
using V6Soft.Common.Utils.Linq;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.INhomKhachHangDataFarmer"/>
    /// </summary>
    public class MssqlNhomKhachHangDataFarmer : EnFwDataFarmerBase<ALnhkh, Models.Accounting.DTO.NhomKhachHang>, INhomKhachHangDataFarmer
    {
        public MssqlNhomKhachHangDataFarmer(IV6AccountingContext dbContext)
            : base(dbContext)
        {
        }

        public PagedSearchResult<Models.Accounting.DTO.NhomKhachHang> GetNhomKhachHangs(SearchCriteria criteria)
        {
            // TODO: Should test with empty table to ensure this method returns NULL.
            if (null == criteria.Sort)
            {
                criteria.Sort = new List<SortDescriptor>()
                {
                    new SortDescriptor("loai_nh")
                };
            }
            return FindByCriteria(criteria);
        }

        protected override Models.Accounting.DTO.NhomKhachHang ToAppModel(ALnhkh dbModel)
        {
            var appModel = new Models.Accounting.DTO.NhomKhachHang()
            {
                LoaiNhom = dbModel.loai_nh,
                MaNhom = dbModel.ma_nh,
                TenNhom = dbModel.ten_nh,
                TenNhom2 = dbModel.ten_nh2,
                TrangThai = dbModel.status,
                CheckSync = dbModel.CHECK_SYNC,
            }; 
            return appModel;
        }

        protected override ALnhkh ToEntityModel(Models.Accounting.DTO.NhomKhachHang appModel)
        {
            var dbModel = new ALnhkh()
            {
                UID = appModel.UID,
                loai_nh = appModel.LoaiNhom,
                ma_nh = appModel.MaNhom,
                ten_nh = appModel.TenNhom,
                ten_nh2 = appModel.TenNhom2,
                status = appModel.TrangThai,
                CHECK_SYNC = appModel.CheckSync,
                date0 = appModel.CreatedDate,
                time0 = appModel.CreatedTime,
                date2 = appModel.ModifiedDate,
                time2 = appModel.ModifiedTime,
                user_id0 = (byte)appModel.CreatedUserId,
                user_id2 = (byte)appModel.ModifiedUserId
            };
            return dbModel;
        }
    }
}
