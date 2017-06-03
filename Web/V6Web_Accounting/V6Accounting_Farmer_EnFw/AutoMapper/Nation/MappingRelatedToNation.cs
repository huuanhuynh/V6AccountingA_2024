using AutoMapper;
using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.Nation
{
    public class MappingRelatedToNation : IMappingRelatedToNation
    {
        public T MapToAlkh<T>(Models.Accounting.DTO.Nation model)
        {
            return Mapper.Map<Models.Accounting.DTO.Nation, T>(model);
        }

        public T MapToNation<T>(Alqg model)
        {
            return Mapper.Map<Alqg, T>(model);
        }
    }
}
