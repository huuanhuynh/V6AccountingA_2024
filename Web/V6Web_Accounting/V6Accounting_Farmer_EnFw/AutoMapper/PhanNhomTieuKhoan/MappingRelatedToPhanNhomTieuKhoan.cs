using AutoMapper;
using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.PhanNhomTieuKhoan
{
    public class MappingRelatedToPhanNhomTieuKhoan : IMappingRelatedToPhanNhomTieuKhoan
    {
        public T MapToAlkh<T>(Models.Accounting.DTO.PhanNhomTieuKhoan model)
        {
            return Mapper.Map<Models.Accounting.DTO.PhanNhomTieuKhoan, T>(model);
        }

        public T MapToPhanNhomTieuKhoan<T>(ALnhtk model)
        {
            return Mapper.Map<ALnhtk, T>(model);
        }
    }
}
