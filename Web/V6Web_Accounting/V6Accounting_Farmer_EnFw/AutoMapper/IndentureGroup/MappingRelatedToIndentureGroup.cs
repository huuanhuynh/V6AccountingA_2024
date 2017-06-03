using AutoMapper;
using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.IndentureGroup
{
    public class MappingRelatedToIndentureGroup : IMappingRelatedToIndentureGroup
    {
        public T MapToAlkh<T>(Models.Accounting.DTO.IndentureGroup model)
        {
            return Mapper.Map<Models.Accounting.DTO.IndentureGroup, T>(model);
        }

        public T MapToIndentureGroup<T>(Alnhku model)
        {
            return Mapper.Map<Alnhku, T>(model);
        }
    }
}
