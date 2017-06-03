using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.Indenture
{
    public interface IMappingRelatedToIndenture
    {
        T MapToAlkh<T>(Models.Accounting.DTO.Indenture model);
        T MapToIndenture<T>(Alku model);
    }
}
