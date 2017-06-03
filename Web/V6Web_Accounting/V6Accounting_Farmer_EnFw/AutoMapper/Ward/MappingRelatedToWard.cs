using AutoMapper;
using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.Ward
{
    public class MappingRelatedToWard : IMappingRelatedToWard
    {
        public T MapToAlkh<T>(Models.Accounting.DTO.Ward model)
        {
            return Mapper.Map<Models.Accounting.DTO.Ward, T>(model);
        }

        public T MapToWard<T>(Alphuong model)
        {
            return Mapper.Map<Alphuong, T>(model);
        }
    }
}
