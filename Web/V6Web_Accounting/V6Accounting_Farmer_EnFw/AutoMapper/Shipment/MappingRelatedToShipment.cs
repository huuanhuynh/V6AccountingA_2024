using AutoMapper;
using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.Shipment
{
    public class MappingRelatedToShipment : IMappingRelatedToShipment
    {
        public T MapToAlkh<T>(Models.Accounting.DTO.Shipment model)
        {
            return Mapper.Map<Models.Accounting.DTO.Shipment, T>(model);
        }

        public T MapToShipment<T>(Alvc model)
        {
            return Mapper.Map<Alvc, T>(model);
        }
    }
}
