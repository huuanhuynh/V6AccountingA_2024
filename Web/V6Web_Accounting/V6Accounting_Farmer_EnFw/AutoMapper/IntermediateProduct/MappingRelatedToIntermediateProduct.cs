using AutoMapper;
using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.IntermediateProduct
{
    public class MappingRelatedToIntermediateProduct : IMappingRelatedToIntermediateProduct
    {
        public T MapToAlkh<T>(Models.Accounting.DTO.IntermediateProduct model)
        {
            return Mapper.Map<Models.Accounting.DTO.IntermediateProduct, T>(model);
        }

        public T MapToIntermediateProduct<T>(ALvttg model)
        {
            return Mapper.Map<ALvttg, T>(model);
        }
    }
}
