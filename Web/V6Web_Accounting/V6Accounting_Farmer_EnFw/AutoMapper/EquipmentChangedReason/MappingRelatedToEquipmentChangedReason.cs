using AutoMapper;
using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.EquipmentChangedReason
{
    public class MappingRelatedToLyDoTangGiamCongCu : IMappingRelatedToLyDoTangGiamCongCu
    {
        public T MapToAlkh<T>(Models.Accounting.DTO.LyDoTangGiamCongCu model)
        {
            return Mapper.Map<Models.Accounting.DTO.LyDoTangGiamCongCu, T>(model);
        }

        public T MapToLyDoTangGiamCongCu<T>(ALtgcc model)
        {
            return Mapper.Map<ALtgcc, T>(model);
        }
    }
}
