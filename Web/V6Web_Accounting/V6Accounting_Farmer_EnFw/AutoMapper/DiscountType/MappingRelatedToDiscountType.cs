using AutoMapper;
using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.DiscountType
{
    public class MappingRelatedToDiscountType : IMappingRelatedToDiscountType
    {
        public T MapToAlkh<T>(Models.Accounting.DTO.DiscountType model)
        {
            return Mapper.Map<Models.Accounting.DTO.DiscountType, T>(model);
        }

        public T MapToDiscountType<T>(Alloaick model)
        {
            return Mapper.Map<Alloaick, T>(model);
        }
    }
}
