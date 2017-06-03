using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.Province
{
    public interface IMappingRelatedToProvince
    {
        T MapToAlkh<T>(Models.Accounting.DTO.Province model);
        T MapToProvince<T>(Altinh model);
    }
}
