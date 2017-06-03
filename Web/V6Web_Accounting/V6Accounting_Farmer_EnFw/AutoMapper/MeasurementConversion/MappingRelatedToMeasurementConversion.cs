using AutoMapper;
using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.MeasurementConversion
{
    public class MappingRelatedToMeasurementConversion : IMappingRelatedToMeasurementConversion
    {
        public T MapToAlkh<T>(Models.Accounting.DTO.MeasurementConversion model)
        {
            return Mapper.Map<Models.Accounting.DTO.MeasurementConversion, T>(model);
        }

        public T MapToMeasurementConversion<T>(ALqddvt model)
        {
            return Mapper.Map<ALqddvt, T>(model);
        }
    }
}
