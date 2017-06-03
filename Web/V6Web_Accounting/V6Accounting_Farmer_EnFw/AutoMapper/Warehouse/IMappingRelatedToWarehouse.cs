using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.Warehouse
{
    public interface IMappingRelatedToWarehouse
    {
        T MapToAlkh<T>(Models.Accounting.DTO.Warehouse model);
        T MapToWarehouse<T>(Alkho model);
    }
}
