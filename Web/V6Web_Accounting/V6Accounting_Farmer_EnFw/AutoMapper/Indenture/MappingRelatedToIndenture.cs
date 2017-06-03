using AutoMapper;
using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.Indenture
{
    public class MappingRelatedToIndenture : IMappingRelatedToIndenture
    {
        public T MapToAlkh<T>(Models.Accounting.DTO.Indenture model)
        {
            return Mapper.Map<Models.Accounting.DTO.Indenture, T>(model);
        }

        public T MapToIndenture<T>(Alku model)
        {
            return Mapper.Map<Alku, T>(model);
        }
    }
}
