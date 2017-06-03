using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.EquipmentType
{
    public interface IMappingRelatedToEquipmentType
    {
        T MapToAlkh<T>(Models.Accounting.DTO.PhanLoaiCongCu model);
        T MapToEquipmentType<T>(ALplcc model);
    }
}
