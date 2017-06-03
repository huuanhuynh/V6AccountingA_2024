using System;
using V6Soft.Accounting.Common.Entities;
using V6Soft.Models.Accounting.Warehouse;

namespace V6Accouting_Warehouse.Extension
{
    public static class WarehouseModelConverter
    {
        public static Alkho ToEntity(this AddingWarehouse warehouse)
        {
            return new Alkho
            {
                UID = warehouse.UID,
                ma_kho = warehouse.MaKho,
                ten_kho = warehouse.TenKho,
                ten_kho2 = warehouse.TenTiengAnh,
                tk_dl = warehouse.TaiKhoanDaiLy,
                stt_ntxt = byte.Parse(warehouse.Stt.ToString()),
                date0 = DateTime.Now,
                time0 = DateTime.Now.ToString("hh:mm:ss"),
                user_id0 = byte.Parse("3"),
                status = warehouse.TrangThai ? "1" : "0",
                ma_dvcs = warehouse.MaDonVi,
                date_yn = warehouse.CoTheoDoiLoHang ? "1" : "0",
                lo_yn = warehouse.CoTheoDoiLoHang ? "1" : "0",
                NH_DVCS1 = warehouse.NhomDonViCoSo
            };
        }
    }
}
