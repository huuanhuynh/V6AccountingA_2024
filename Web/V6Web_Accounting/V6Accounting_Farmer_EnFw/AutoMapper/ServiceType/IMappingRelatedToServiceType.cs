using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.ServiceType
{
    public interface IMappingRelatedToServiceType
    {
        T MapToAlkh<T>(Models.Accounting.DTO.ServiceType model);
        T MapToServiceType<T>(Alloaivc model);
    }
}
