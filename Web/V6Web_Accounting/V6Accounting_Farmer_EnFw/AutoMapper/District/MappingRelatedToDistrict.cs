using AutoMapper;
using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.District
{
    public class MappingRelatedToDistrict : IMappingRelatedToDistrict
    {
        public T MapToAlkh<T>(Models.Accounting.DTO.District model)
        {
            return Mapper.Map<Models.Accounting.DTO.District, T>(model);
        }

        public T MapToDistrict<T>(Alquan model)
        {
            return Mapper.Map<Alquan, T>(model);
        }
    }
}
