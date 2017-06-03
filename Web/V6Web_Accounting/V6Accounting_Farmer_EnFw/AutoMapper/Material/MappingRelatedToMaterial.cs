using AutoMapper;
using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.Material
{
    public class MappingRelatedToMaterial : IMappingRelatedToMaterial
    {
        public T MapToAlkh<T>(Models.Accounting.DTO.Material model)
        {
            return Mapper.Map<Models.Accounting.DTO.Material, T>(model);
        }

        public T MapToMaterial<T>(ALvt model)
        {
            return Mapper.Map<ALvt, T>(model);
        }
    }
}
