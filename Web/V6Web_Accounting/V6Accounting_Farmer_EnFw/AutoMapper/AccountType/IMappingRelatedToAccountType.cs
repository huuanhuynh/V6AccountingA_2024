using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.AccountType
{
    public interface IMappingRelatedToAccountType
    {
        T MapToALnhtk0<T>(Models.Accounting.DTO.AccountType model);
        T MapToAccountType<T>(ALnhtk0 model);
    }
}
