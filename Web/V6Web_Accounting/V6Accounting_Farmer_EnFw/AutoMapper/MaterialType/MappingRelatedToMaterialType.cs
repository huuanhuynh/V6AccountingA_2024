using AutoMapper;
using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.MaterialType
{
    public class MappingRelatedToMaterialType : IMappingRelatedToMaterialType
    {
        public T MapToAlkh<T>(Models.Accounting.DTO.MaterialType model)
        {
            return Mapper.Map<Models.Accounting.DTO.MaterialType, T>(model);
        }

        public T MapToMaterialType<T>(ALloaivt model)
        {
            return Mapper.Map<ALloaivt, T>(model);
        }
    }
}
