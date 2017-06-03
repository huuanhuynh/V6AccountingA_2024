using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.Nation
{
    public interface IMappingRelatedToNation
    {
        T MapToAlkh<T>(Models.Accounting.DTO.Nation model);
        T MapToNation<T>(Alqg model);
    }
}
