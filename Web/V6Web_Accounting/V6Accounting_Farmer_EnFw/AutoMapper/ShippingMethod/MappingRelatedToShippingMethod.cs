using AutoMapper;
using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.ShippingMethod
{
    public class MappingRelatedToShippingMethod : IMappingRelatedToShippingMethod
    {
        public T MapToAlkh<T>(Models.Accounting.DTO.ShippingMethod model)
        {
            return Mapper.Map<Models.Accounting.DTO.ShippingMethod, T>(model);
        }

        public T MapToShippingMethod<T>(Alhtvc model)
        {
            return Mapper.Map<Alhtvc, T>(model);
        }
    }
}
