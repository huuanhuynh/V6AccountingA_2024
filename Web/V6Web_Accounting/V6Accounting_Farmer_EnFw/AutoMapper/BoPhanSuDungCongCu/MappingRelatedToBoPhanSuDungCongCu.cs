using AutoMapper;
using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.BoPhanSuDungCongCu
{
    public class MappingRelatedToBoPhanSuDungCongCu : IMappingRelatedToBoPhanSuDungCongCu
    {
        public T MapToAlbp<T>(Models.Accounting.DTO.BoPhanSuDungCongCu model)
        {
            return Mapper.Map<Models.Accounting.DTO.BoPhanSuDungCongCu, T>(model);
        }

        public T MapToBoPhanSuDungCongCu<T>(Albpcc model)
        {
            return Mapper.Map<Albpcc, T>(model);
        }
    }
}
