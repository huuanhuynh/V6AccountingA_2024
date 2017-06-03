using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.PhanNhomTieuKhoan
{
    public interface IMappingRelatedToPhanNhomTieuKhoan
    {
        T MapToAlkh<T>(Models.Accounting.DTO.PhanNhomTieuKhoan model);
        T MapToPhanNhomTieuKhoan<T>(ALnhtk model);
    }
}
