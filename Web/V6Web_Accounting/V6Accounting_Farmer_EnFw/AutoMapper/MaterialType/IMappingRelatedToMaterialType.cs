using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.MaterialType
{
    public interface IMappingRelatedToMaterialType
    {
        T MapToAlkh<T>(Models.Accounting.DTO.MaterialType model);
        T MapToMaterialType<T>(ALloaivt model);
    }
}
