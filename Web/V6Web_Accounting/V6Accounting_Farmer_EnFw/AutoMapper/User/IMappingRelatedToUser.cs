using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.User
{
    public interface IMappingRelatedToUser
    {
        T MapToV6User<T>(Models.Core.Membership.Dto.User model);
        T MapToUser<T>(V6User model);
    }
}
