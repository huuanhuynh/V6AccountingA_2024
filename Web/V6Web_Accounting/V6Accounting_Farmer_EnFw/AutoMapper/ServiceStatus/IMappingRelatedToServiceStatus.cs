using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.ServiceStatus
{
    public interface IMappingRelatedToServiceStatus
    {
        T MapToAlkh<T>(Models.Accounting.DTO.ServiceStatus model);
        T MapToServiceStatus<T>(ALttvt model);
    }
}
