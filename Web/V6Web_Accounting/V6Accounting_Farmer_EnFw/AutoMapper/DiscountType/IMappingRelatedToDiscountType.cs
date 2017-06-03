using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.DiscountType
{
    public interface IMappingRelatedToDiscountType
    {
        T MapToAlkh<T>(Models.Accounting.DTO.DiscountType model);
        T MapToDiscountType<T>(Alloaick model);
    }
}
