using AutoMapper;
using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.PriceCode
{
    public class MappingRelatedToPriceCode : IMappingRelatedToPriceCode
    {
        public T MapToAlmagia<T>(Models.Accounting.DTO.PriceCode model)
        {
            return Mapper.Map<Models.Accounting.DTO.PriceCode, T>(model);
        }

        public T MapToPriceCode<T>(Almagia model)
        {
            return Mapper.Map<Almagia, T>(model);
        }
    }
}
