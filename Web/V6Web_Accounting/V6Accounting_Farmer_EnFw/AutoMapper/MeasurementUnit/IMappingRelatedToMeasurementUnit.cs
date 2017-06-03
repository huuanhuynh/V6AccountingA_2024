using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.MeasurementUnit
{
    public interface IMappingRelatedToMeasurementUnit
    {
        T MapToAlkh<T>(Models.Accounting.DTO.MeasurementUnit model);
        T MapToMeasurementUnit<T>(Aldvt model);
    }
}
