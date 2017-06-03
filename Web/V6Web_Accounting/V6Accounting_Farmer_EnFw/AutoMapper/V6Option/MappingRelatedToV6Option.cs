using AutoMapper;
using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.V6Option
{
    public class MappingRelatedToV6Option : IMappingRelatedToV6Option
    {
        public T MapTov6option<T>(Models.Accounting.DTO.V6Option model)
        {
            return Mapper.Map<Models.Accounting.DTO.V6Option, T>(model);
        }

        public T MapToV6Option<T>(V6option model)
        {
            return Mapper.Map<V6option, T>(model);
        }
    }
}
