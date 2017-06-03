using AutoMapper;
using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.EquipmentType
{
    public class MappingRelatedToEquipmentType : IMappingRelatedToEquipmentType
    {
        public T MapToAlkh<T>(Models.Accounting.DTO.PhanLoaiCongCu model)
        {
            return Mapper.Map<Models.Accounting.DTO.PhanLoaiCongCu, T>(model);
        }

        public T MapToEquipmentType<T>(ALplcc model)
        {
            return Mapper.Map<ALplcc, T>(model);
        }
    }
}
