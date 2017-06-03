using AutoMapper;
using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.AccountType
{
    public class MappingRelatedToAccountType : IMappingRelatedToAccountType
    {
        public T MapToALnhtk0<T>(Models.Accounting.DTO.AccountType model)
        {
            return Mapper.Map<Models.Accounting.DTO.AccountType, T>(model);
        }

        public T MapToAccountType<T>(ALnhtk0 model)
        {
            return Mapper.Map<ALnhtk0, T>(model);
        }
    }
}
