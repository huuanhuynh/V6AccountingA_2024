using AutoMapper;
using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.LoaiNhapXuat
{
    public class MappingRelatedToLoaiNhapXuat : IMappingRelatedToLoaiNhapXuat
    {
        public T MapToAlkh<T>(Models.Accounting.DTO.LoaiNhapXuat model)
        {
            return Mapper.Map<Models.Accounting.DTO.LoaiNhapXuat, T>(model);
        }

        public T MapToLoaiNhapXuat<T>(Allnx model)
        {
            return Mapper.Map<Allnx, T>(model);
        }
    }
}
