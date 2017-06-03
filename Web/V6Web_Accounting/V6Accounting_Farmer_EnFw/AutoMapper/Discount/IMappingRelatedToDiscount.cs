using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.Discount
{
    public interface IMappingRelatedToDiscount
    {
        T MapToAlkh<T>(Models.Accounting.DTO.Discount model);
        T MapToDiscount<T>(Alck model);
    }
}
