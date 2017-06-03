using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.V6Option
{
    public interface IMappingRelatedToV6Option
    {
        T MapTov6option<T>(Models.Accounting.DTO.V6Option model);
        T MapToV6Option<T>(V6option model);
    }
}
