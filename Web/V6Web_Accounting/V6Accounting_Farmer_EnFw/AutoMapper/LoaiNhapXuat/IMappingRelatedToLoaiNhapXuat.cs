using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.LoaiNhapXuat
{
    public interface IMappingRelatedToLoaiNhapXuat
    {
        T MapToAlkh<T>(Models.Accounting.DTO.LoaiNhapXuat model);
        T MapToLoaiNhapXuat<T>(Allnx model);
    }
}
