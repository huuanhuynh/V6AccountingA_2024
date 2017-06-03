using AutoMapper;
using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.Capital
{
    public class MappingRelatedToCapital : IMappingRelatedToCapital
    {
        public T MapToAlkh<T>(Models.Accounting.DTO.Capital model)
        {
            return Mapper.Map<Models.Accounting.DTO.Capital, T>(model);
        }

        public T MapToCapital<T>(Alnv model)
        {
            return Mapper.Map<Alnv, T>(model);
        }
    }
}
