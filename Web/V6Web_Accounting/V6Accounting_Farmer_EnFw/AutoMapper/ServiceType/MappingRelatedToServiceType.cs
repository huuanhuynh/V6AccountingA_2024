using AutoMapper;
using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.ServiceType
{
    public class MappingRelatedToServiceType : IMappingRelatedToServiceType
    {
        public T MapToAlkh<T>(Models.Accounting.DTO.ServiceType model)
        {
            return Mapper.Map<Models.Accounting.DTO.ServiceType, T>(model);
        }

        public T MapToServiceType<T>(Alloaivc model)
        {
            return Mapper.Map<Alloaivc, T>(model);
        }
    }
}
