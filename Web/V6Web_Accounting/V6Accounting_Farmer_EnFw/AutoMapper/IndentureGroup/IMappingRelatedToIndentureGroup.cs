using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.IndentureGroup
{
    public interface IMappingRelatedToIndentureGroup
    {
        T MapToAlkh<T>(Models.Accounting.DTO.IndentureGroup model);
        T MapToIndentureGroup<T>(Alnhku model);
    }
}
