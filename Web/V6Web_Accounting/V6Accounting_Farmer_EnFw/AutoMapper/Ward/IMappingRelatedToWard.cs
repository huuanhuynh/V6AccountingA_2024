using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.Ward
{
    public interface IMappingRelatedToWard
    {
        T MapToAlkh<T>(Models.Accounting.DTO.Ward model);
        T MapToWard<T>(Alphuong model);
    }
}
