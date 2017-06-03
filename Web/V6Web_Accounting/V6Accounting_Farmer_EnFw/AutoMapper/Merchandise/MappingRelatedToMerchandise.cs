using AutoMapper;
using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.Merchandise
{
    public class MappingRelatedToMerchandise : IMappingRelatedToMerchandise
    {
        public T MapToAlkh<T>(Models.Accounting.DTO.Merchandise model)
        {
            return Mapper.Map<Models.Accounting.DTO.Merchandise, T>(model);
        }

        public T MapToMerchandise<T>(Allo model)
        {
            return Mapper.Map<Allo, T>(model);
        }
    }
}
