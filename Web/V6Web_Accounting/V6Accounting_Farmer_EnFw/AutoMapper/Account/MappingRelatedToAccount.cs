using AutoMapper;
using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.Account
{
    public class MappingRelatedToAccount : IMappingRelatedToAccount
    {
        public T MapToAlkh<T>(Models.Accounting.DTO.TaiKhoan model)
        {
            return Mapper.Map<Models.Accounting.DTO.TaiKhoan, T>(model);
        }

        public T MapToAccount<T>(Altk0 model)
        {
            return Mapper.Map<Altk0, T>(model);
        }
    }
}
