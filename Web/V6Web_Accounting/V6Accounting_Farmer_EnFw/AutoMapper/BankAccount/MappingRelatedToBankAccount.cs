using AutoMapper;
using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.BankAccount
{
    public class MappingRelatedToBankAccount : IMappingRelatedToBankAccount
    {
        public T MapToAlkh<T>(Models.Accounting.DTO.BankAccount model)
        {
            return Mapper.Map<Models.Accounting.DTO.BankAccount, T>(model);
        }

        public T MapToBankAccount<T>(ALtknh model)
        {
            return Mapper.Map<ALtknh, T>(model);
        }
    }
}
