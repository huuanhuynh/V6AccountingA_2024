using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.PhanNhomCongCu
{
    public interface IMappingRelatedToPhanNhomCongCu
    {
        T MapToALnhCC<T>(Models.Accounting.DTO.PhanNhomCongCu model);
        T MapToPhanNhomCongCu<T>(ALnhCC model);
    }
}
