using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.MaterialGroup
{
    public interface IMappingRelatedToMaterialGroup
    {
        T MapToAlkh<T>(Models.Accounting.DTO.MaterialGroup model);
        T MapToMaterialGroup<T>(ALnhvt model);
    }
}
