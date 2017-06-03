using AutoMapper;
using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.PhanNhomCongCu
{
    public class MappingRelatedToPhanNhomCongCu : IMappingRelatedToPhanNhomCongCu
    {
        public T MapToALnhCC<T>(Models.Accounting.DTO.PhanNhomCongCu model)
        {
            return Mapper.Map<Models.Accounting.DTO.PhanNhomCongCu, T>(model);
        }

        public T MapToPhanNhomCongCu<T>(ALnhCC model)
        {
            return Mapper.Map<ALnhCC, T>(model);
        }
    }
}
