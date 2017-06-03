using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.Location
{
    public interface IMappingRelatedToLocation
    {
        T MapToAlkh<T>(Models.Accounting.DTO.Location model);
        T MapToLocation<T>(ALvitri model);
    }
}
