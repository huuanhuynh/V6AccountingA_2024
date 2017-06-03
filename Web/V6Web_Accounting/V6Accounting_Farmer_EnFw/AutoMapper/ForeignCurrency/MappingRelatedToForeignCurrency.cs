using AutoMapper;
using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.ForeignCurrency
{
    public class MappingRelatedToForeignCurrency : IMappingRelatedToForeignCurrency
    {
        public T MapToAlkh<T>(Models.Accounting.DTO.ForeignCurrency model)
        {
            return Mapper.Map<Models.Accounting.DTO.ForeignCurrency, T>(model);
        }

        public T MapToForeignCurrency<T>(Alnt model)
        {
            return Mapper.Map<Alnt, T>(model);
        }
    }
}
