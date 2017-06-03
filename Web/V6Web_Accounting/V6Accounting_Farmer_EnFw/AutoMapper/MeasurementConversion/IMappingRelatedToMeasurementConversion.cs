using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.MeasurementConversion
{
    public interface IMappingRelatedToMeasurementConversion
    {
        T MapToAlkh<T>(Models.Accounting.DTO.MeasurementConversion model);
        T MapToMeasurementConversion<T>(ALqddvt model);
    }
}
