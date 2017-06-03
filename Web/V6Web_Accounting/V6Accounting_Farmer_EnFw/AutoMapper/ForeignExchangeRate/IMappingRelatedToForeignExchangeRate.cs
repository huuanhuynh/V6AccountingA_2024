using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.ForeignExchangeRate
{
    public interface IMappingRelatedToForeignExchangeRate
    {
        T MapToAlkh<T>(Models.Accounting.DTO.ForeignExchangeRate model);
        T MapToForeignExchangeRate<T>(ALtgnt model);
    }
}
