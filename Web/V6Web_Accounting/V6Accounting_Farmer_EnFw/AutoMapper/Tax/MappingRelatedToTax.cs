using AutoMapper;
using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.Tax
{
    public class MappingRelatedToTax : IMappingRelatedToTax
    {
        public T MapToAlkh<T>(Models.Accounting.DTO.Tax model)
        {
            return Mapper.Map<Models.Accounting.DTO.Tax, T>(model);
        }

        public T MapToTax<T>(Althue model)
        {
            return Mapper.Map<Althue, T>(model);
        }
    }
}
