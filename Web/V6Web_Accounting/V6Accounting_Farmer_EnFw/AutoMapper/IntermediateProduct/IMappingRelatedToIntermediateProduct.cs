using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.IntermediateProduct
{
    public interface IMappingRelatedToIntermediateProduct
    {
        T MapToAlkh<T>(Models.Accounting.DTO.IntermediateProduct model);
        T MapToIntermediateProduct<T>(ALvttg model);
    }
}
