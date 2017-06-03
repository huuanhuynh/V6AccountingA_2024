using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.BankAccount
{
    public interface IMappingRelatedToBankAccount
    {
        T MapToAlkh<T>(Models.Accounting.DTO.BankAccount model);
        T MapToBankAccount<T>(ALtknh model);
    }
}
