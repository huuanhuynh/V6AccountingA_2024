using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.Department
{
    public interface IMappingRelatedToDepartment
    {
        T MapToAlbp<T>(Models.Accounting.DTO.Department model);
        T MapToDepartment<T>(Albp model);
    }
}
