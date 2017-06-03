using AutoMapper;
using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.MeasurementUnit
{
    public class MappingRelatedToMeasurementUnit : IMappingRelatedToMeasurementUnit
    {
        public T MapToAlkh<T>(Models.Accounting.DTO.MeasurementUnit model)
        {
            return Mapper.Map<Models.Accounting.DTO.MeasurementUnit, T>(model);
        }

        public T MapToMeasurementUnit<T>(Aldvt model)
        {
            return Mapper.Map<Aldvt, T>(model);
        }
    }
}
