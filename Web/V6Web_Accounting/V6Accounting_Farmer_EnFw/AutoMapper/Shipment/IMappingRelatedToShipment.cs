using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.Shipment
{
    public interface IMappingRelatedToShipment
    {
        T MapToAlkh<T>(Models.Accounting.DTO.Shipment model);
        T MapToShipment<T>(Alvc model);
    }
}
