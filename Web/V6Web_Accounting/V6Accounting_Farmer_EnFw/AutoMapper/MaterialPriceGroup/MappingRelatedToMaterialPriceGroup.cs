using AutoMapper;
using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.MaterialPriceGroup
{
    public class MappingRelatedToMaterialPriceGroup : IMappingRelatedToMaterialPriceGroup
    {
        public T MapToAlkh<T>(Models.Accounting.DTO.MaterialPriceGroup model)
        {
            return Mapper.Map<Models.Accounting.DTO.MaterialPriceGroup, T>(model);
        }

        public T MapToMaterialPriceGroup<T>(Alnhvt2 model)
        {
            return Mapper.Map<Alnhvt2, T>(model);
        }
    }
}
