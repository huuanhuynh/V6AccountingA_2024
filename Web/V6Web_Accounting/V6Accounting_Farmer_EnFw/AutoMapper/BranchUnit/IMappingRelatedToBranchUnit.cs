using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.BranchUnit
{
    public interface IMappingRelatedToBranchUnit
    {
        T MapToAlkh<T>(Models.Accounting.DTO.BranchUnit model);
        T MapToBranchUnit<T>(Aldvcs model);
    }
}
