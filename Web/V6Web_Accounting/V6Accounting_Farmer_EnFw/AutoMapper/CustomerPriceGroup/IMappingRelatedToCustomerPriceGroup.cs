using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.CustomerPriceGroup
{
    public interface IMappingRelatedToCustomerPriceGroup
    {
        T MapToAlkh<T>(Models.Accounting.DTO.CustomerPriceGroup model);
        T MapToCustomerPriceGroup<T>(Alnhkh2 model);
    }
}
