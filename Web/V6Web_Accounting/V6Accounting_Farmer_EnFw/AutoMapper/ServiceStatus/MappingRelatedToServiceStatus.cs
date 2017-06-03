using AutoMapper;
using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.ServiceStatus
{
    public class MappingRelatedToServiceStatus : IMappingRelatedToServiceStatus
    {
        public T MapToAlkh<T>(Models.Accounting.DTO.ServiceStatus model)
        {
            return Mapper.Map<Models.Accounting.DTO.ServiceStatus, T>(model);
        }

        public T MapToServiceStatus<T>(ALttvt model)
        {
            return Mapper.Map<ALttvt, T>(model);
        }
    }
}
