using AutoMapper;
using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.Location
{
    public class MappingRelatedToLocation : IMappingRelatedToLocation
    {
        public T MapToAlkh<T>(Models.Accounting.DTO.Location model)
        {
            return Mapper.Map<Models.Accounting.DTO.Location, T>(model);
        }

        public T MapToLocation<T>(ALvitri model)
        {
            return Mapper.Map<ALvitri, T>(model);
        }
    }
}
