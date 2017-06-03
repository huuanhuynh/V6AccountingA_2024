using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.Account
{
    public interface IMappingRelatedToAccount
    {
        T MapToAlkh<T>(Models.Accounting.DTO.TaiKhoan model);
        T MapToAccount<T>(Altk0 model);
    }
}
