using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.District
{
    public interface IMappingRelatedToDistrict
    {
        T MapToAlkh<T>(Models.Accounting.DTO.District model);
        T MapToDistrict<T>(Alquan model);
    }
}
