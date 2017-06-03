using AutoMapper;
using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.ForeignExchangeRate
{
    public class MappingRelatedToForeignExchangeRate : IMappingRelatedToForeignExchangeRate
    {
        public T MapToAlkh<T>(Models.Accounting.DTO.ForeignExchangeRate model)
        {
            return Mapper.Map<Models.Accounting.DTO.ForeignExchangeRate, T>(model);
        }

        public T MapToForeignExchangeRate<T>(ALtgnt model)
        {
            return Mapper.Map<ALtgnt, T>(model);
        }
    }
}
