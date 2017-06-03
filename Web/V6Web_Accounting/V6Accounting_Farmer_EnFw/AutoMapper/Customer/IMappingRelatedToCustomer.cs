using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.Customer
{
    public interface IMappingRelatedToCustomer
    {
        T MapToAlkh<T>(Models.Accounting.DTO.Customer model);
        T MapToCustomer<T>(Alkh model);
    }
}
