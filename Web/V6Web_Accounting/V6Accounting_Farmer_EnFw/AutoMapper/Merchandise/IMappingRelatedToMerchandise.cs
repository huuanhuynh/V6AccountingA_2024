using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.Merchandise
{
    public interface IMappingRelatedToMerchandise
    {
        T MapToAlkh<T>(Models.Accounting.DTO.Merchandise model);
        T MapToMerchandise<T>(Allo model);
    }
}
