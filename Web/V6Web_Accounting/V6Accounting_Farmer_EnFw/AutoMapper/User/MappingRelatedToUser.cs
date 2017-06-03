using AutoMapper;
using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.User
{
    public class MappingRelatedToUser :IMappingRelatedToUser
    {
        public T MapToV6User<T>(Models.Core.Membership.Dto.User model)
        {
            return Mapper.Map<Models.Core.Membership.Dto.User, T>(model);
        }

        public T MapToUser<T>(V6User model)
        {
            return Mapper.Map<V6User, T>(model);
        }
    }
}
