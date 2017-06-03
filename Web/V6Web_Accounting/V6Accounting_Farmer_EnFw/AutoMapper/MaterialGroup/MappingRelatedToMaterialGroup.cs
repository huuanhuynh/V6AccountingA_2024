using AutoMapper;
using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.MaterialGroup
{
    public class MappingRelatedToMaterialGroup : IMappingRelatedToMaterialGroup
    {
        public T MapToAlkh<T>(Models.Accounting.DTO.MaterialGroup model)
        {
            return Mapper.Map<Models.Accounting.DTO.MaterialGroup, T>(model);
        }

        public T MapToMaterialGroup<T>(ALnhvt model)
        {
            return Mapper.Map<ALnhvt, T>(model);
        }
    }
}
