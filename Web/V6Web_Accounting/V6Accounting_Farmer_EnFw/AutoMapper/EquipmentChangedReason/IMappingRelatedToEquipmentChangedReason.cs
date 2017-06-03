using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.EquipmentChangedReason
{
    public interface IMappingRelatedToLyDoTangGiamCongCu
    {
        T MapToAlkh<T>(Models.Accounting.DTO.LyDoTangGiamCongCu model);
        T MapToLyDoTangGiamCongCu<T>(ALtgcc model);
    }
}
