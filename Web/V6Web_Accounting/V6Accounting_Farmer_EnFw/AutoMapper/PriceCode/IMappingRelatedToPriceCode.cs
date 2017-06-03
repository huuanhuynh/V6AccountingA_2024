using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.PriceCode
{
    public interface IMappingRelatedToPriceCode
    {
        T MapToAlmagia<T>(Models.Accounting.DTO.PriceCode model);
        T MapToPriceCode<T>(Almagia model);
    }
}
