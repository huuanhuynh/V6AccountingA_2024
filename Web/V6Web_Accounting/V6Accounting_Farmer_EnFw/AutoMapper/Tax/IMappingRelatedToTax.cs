using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.Tax
{
    public interface IMappingRelatedToTax
    {
        T MapToAlkh<T>(Models.Accounting.DTO.Tax model);
        T MapToTax<T>(Althue model);
    }
}
