using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.MaterialPriceGroup
{
    public interface IMappingRelatedToMaterialPriceGroup
    {
        T MapToAlkh<T>(Models.Accounting.DTO.MaterialPriceGroup model);
        T MapToMaterialPriceGroup<T>(Alnhvt2 model);
    }
}
