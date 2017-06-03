using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.ForeignCurrency
{
    public interface IMappingRelatedToForeignCurrency
    {
        T MapToAlkh<T>(Models.Accounting.DTO.ForeignCurrency model);
        T MapToForeignCurrency<T>(Alnt model);
    }
}
