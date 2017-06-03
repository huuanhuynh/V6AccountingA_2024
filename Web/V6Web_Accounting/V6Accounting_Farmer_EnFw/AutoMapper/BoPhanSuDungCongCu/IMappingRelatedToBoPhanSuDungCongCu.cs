using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.BoPhanSuDungCongCu
{
    public interface IMappingRelatedToBoPhanSuDungCongCu
    {
        T MapToAlbp<T>(Models.Accounting.DTO.BoPhanSuDungCongCu model);
        T MapToBoPhanSuDungCongCu<T>(Albpcc model);
    }
}
