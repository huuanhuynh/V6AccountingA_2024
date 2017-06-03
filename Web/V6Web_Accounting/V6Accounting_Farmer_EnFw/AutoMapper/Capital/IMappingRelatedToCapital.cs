using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.Capital
{
    public interface IMappingRelatedToCapital
    {
        T MapToAlkh<T>(Models.Accounting.DTO.Capital model);
        T MapToCapital<T>(Alnv model);
    }
}
