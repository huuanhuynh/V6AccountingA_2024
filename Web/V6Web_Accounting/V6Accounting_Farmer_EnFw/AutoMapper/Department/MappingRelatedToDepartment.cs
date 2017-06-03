using AutoMapper;
using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.Department
{
    public class MappingRelatedToDepartment : IMappingRelatedToDepartment
    {
        public T MapToAlbp<T>(Models.Accounting.DTO.Department model)
        {
            return Mapper.Map<Models.Accounting.DTO.Department, T>(model);
        }

        public T MapToDepartment<T>(Albp model)
        {
            return Mapper.Map<Albp, T>(model);
        }
    }
}
