using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.Material
{
    public interface IMappingRelatedToMaterial
    {
        T MapToAlkh<T>(Models.Accounting.DTO.Material model);
        T MapToMaterial<T>(ALvt model);
    }
}
