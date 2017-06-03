using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.ShippingMethod
{
    public interface IMappingRelatedToShippingMethod
    {
        T MapToAlkh<T>(Models.Accounting.DTO.ShippingMethod model);
        T MapToShippingMethod<T>(Alhtvc model);
    }
}
