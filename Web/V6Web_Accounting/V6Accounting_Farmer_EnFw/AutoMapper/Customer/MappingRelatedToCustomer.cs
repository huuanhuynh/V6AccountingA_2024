using AutoMapper;

using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.Customer
{
    public class MappingRelatedToCustomer : IMappingRelatedToCustomer
    {
        public T MapToAlkh<T>(Models.Accounting.DTO.Customer model)
        {
            return Mapper.Map<Models.Accounting.DTO.Customer, T>(model);
        }

        public T MapToCustomer<T>(Alkh model)
        {
            return Mapper.Map<Alkh, T>(model);
        }
    }
}
