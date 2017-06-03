using AutoMapper;
using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.CustomerPriceGroup
{
    public class MappingRelatedToCustomerPriceGroup : IMappingRelatedToCustomerPriceGroup
    {
        public T MapToAlkh<T>(Models.Accounting.DTO.CustomerPriceGroup model)
        {
            return Mapper.Map<Models.Accounting.DTO.CustomerPriceGroup, T>(model);
        }

        public T MapToCustomerPriceGroup<T>(Alnhkh2 model)
        {
            return Mapper.Map<Alnhkh2, T>(model);
        }
    }
}
