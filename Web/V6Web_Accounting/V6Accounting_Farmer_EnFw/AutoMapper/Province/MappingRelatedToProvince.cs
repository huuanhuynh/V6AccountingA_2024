using AutoMapper;
using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.Province
{
    public class MappingRelatedToProvince : IMappingRelatedToProvince
    {
        public T MapToAlkh<T>(Models.Accounting.DTO.Province model)
        {
            return Mapper.Map<Models.Accounting.DTO.Province, T>(model);
        }

        public T MapToProvince<T>(Altinh model)
        {
            return Mapper.Map<Altinh, T>(model);
        }
    }
}
