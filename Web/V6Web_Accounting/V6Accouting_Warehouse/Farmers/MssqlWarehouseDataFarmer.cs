using System.Collections;
using System.Collections.Generic;
using System.Linq;
using V6Accounting_EntityFramework;
using V6Accounting_EntityFramework.Entities;
using V6Soft.Models.Accounting;
using V6Soft.Models.Core;
using V6Soft.Models.Core.Extensions;
using V6Soft.Models.Core.ViewModels;

namespace V6Accouting_Warehouse.Farmers
{
    public class WarehouseDataFarmer : GenericDataFarmer<Alkho>, IWarehouseDataFarmer
    {
        public WarehouseDataFarmer(V6AccountingContext dbContext)
            : base(dbContext)
        {
        }

        public PagedSearchResult<Warehouse> SearchWarehouses(SearchCriteria criteria)
        {
            return GetAll().AsQueryable().OrderBy(x => x.ma_kho).Select(x => new Warehouse
            {
                //UID = x.UID.Value,
                MaKho = x.ma_kho,
                TenKho = x.ten_kho,
                TenTiengAnh = x.ten_kho2,
                TaiKhoanDaiLy = x.tk_dl,
                Stt = x.stt_ntxt,
                TenThuKho = x.thu_kho,
                DiaChi = x.dia_chi,
                Fax = x.fax,
                Email = x.email,
                MaVanChuyen = x.ma_vc,
                GioKhoiTao = x.date2.Value,
                NgayNhap = x.date0,
                //TrangThai = string.IsNullOrEmpty(x.status.Trim()) ? 0 : int.Parse(x.status)
            }).ToPagedSearchResult(criteria);
        }
    }
}
