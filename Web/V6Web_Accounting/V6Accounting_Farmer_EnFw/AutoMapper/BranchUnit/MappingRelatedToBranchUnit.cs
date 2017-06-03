using AutoMapper;
using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.BranchUnit
{
    public class MappingRelatedToBranchUnit : IMappingRelatedToBranchUnit
    {
        public T MapToAlkh<T>(Models.Accounting.DTO.BranchUnit model)
        {
            return Mapper.Map<Models.Accounting.DTO.BranchUnit, T>(model);
        }

        public T MapToBranchUnit<T>(Aldvcs model)
        {
            return Mapper.Map<Aldvcs, T>(model);
        }
    }
}
