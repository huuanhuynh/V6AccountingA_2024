using AutoMapper;
using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.Discount
{
    public class MappingRelatedToDiscount : IMappingRelatedToDiscount
    {
        public T MapToAlkh<T>(Models.Accounting.DTO.Discount model)
        {
            return Mapper.Map<Models.Accounting.DTO.Discount, T>(model);
        }

        public T MapToDiscount<T>(Alck model)
        {
            return Mapper.Map<Alck, T>(model);
        }
    }
}
