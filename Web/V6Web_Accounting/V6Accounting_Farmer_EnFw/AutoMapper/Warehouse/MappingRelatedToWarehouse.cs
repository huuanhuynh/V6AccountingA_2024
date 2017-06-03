using AutoMapper;
using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.Warehouse
{
    public class MappingRelatedToWarehouse : IMappingRelatedToWarehouse
    {
        public T MapToAlkh<T>(Models.Accounting.DTO.Warehouse model)
        {
            return Mapper.Map<Models.Accounting.DTO.Warehouse, T>(model);
        }

        public T MapToWarehouse<T>(Alkho model)
        {
            return Mapper.Map<Alkho, T>(model);
        }
    }
}
