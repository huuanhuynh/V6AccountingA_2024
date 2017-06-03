using System.Collections.Generic;
using AutoMapper;

using V6Soft.Accounting.Farmers.EnFw.Entities;

namespace V6Soft.Accounting.Farmers.EnFw.AutoMapper.Configuration
{
    public class MyProfile : Profile
    {
        public const string VIEW_MODEL = "MyProfileNameHere";
        protected string ProfileName
        {
            get { return VIEW_MODEL; }
        }

        protected override void Configure()
        {
            CreateMaps();
        }

        private static void CreateMaps()
        {
            //Creating configuration to convert from Customer to Alkh
            Mapper.CreateMap<Models.Accounting.DTO.Customer, Alkh>();
                //.ForMember(ent => ent.ModifiedUserId, opts => opts.MapFrom(dto => (int) dto.ModifiedUserId))
                //.ForMember(ent => ent.CreatedUserId, opts => opts.MapFrom(dto => (int) dto.CreatedUserId))
                //.ForMember(ent => ent.HanThanhToan,
                //    opts => opts.MapFrom(dto => (byte) (dto.HanThanhToan == false ? 0 : 1)));
                //.ForMember(ent => ent.LaKhachHang, opts => opts.MapFrom(dto => (dto.LaKhachHang == false ? 0 : 1)))
                //.ForMember(ent => ent.LaNhaCungCap, opts => opts.MapFrom(dto => (byte?)(dto.LaNhaCungCap == false ? 0 : 1)))
                //.ForMember(ent => ent.LaNhanVien, opts => opts.MapFrom(dto => (byte?)(dto.LaNhanVien == false ? 0 : 1)));
            
            //Creating configuration to convert from Alkh to Customer
            Mapper.CreateMap<Alkh, Models.Accounting.DTO.Customer>();
                //.ForMember(dto => dto.ModifiedUserId, opts => opts.MapFrom(ent => (int)ent.ModifiedUserId))
                //.ForMember(dto => dto.CreatedUserId, opts => opts.MapFrom(ent => (int)ent.CreatedUserId))
                //.ForMember(dto => dto.HanThanhToan, opts => opts.MapFrom(ent => ent.HanThanhToan > 0))
                //.ForMember(dto => dto.LaKhachHang, opts => opts.MapFrom(ent => ent.LaKhachHang.HasValue && ent.LaKhachHang.Value > 0))
                //.ForMember(dto => dto.LaNhaCungCap, opts => opts.MapFrom(ent => ent.LaNhaCungCap.HasValue && ent.LaNhaCungCap.Value > 0))
                //.ForMember(dto => dto.LaNhanVien, opts => opts.MapFrom(ent => ent.LaNhanVien.HasValue && ent.LaNhanVien.Value > 0));

            Mapper.CreateMap<AM81, Models.Accounting.DTO.Receipt>();
            Mapper.CreateMap<Models.Accounting.DTO.Receipt, AM81>();

            //ErrorCode
            Mapper.CreateMap<ErrorCode, Models.Accounting.DTO.ErrorCode>();
            Mapper.CreateMap<Models.Accounting.DTO.ErrorCode, ErrorCode>();

            Mapper.CreateMap<Models.Accounting.DTO.ReceiptDetail, AD81>();
            Mapper.CreateMap<AD81, Models.Accounting.DTO.ReceiptDetail>();

            Mapper.CreateMap<Albp, Models.Accounting.DTO.Department>();  // thằng đứng trước là thằng source, thằng sau là destination
            Mapper.CreateMap<Models.Accounting.DTO.Department, Albp>();

            Mapper.CreateMap<V6User, Models.Core.Membership.Dto.User>();
            Mapper.CreateMap<Models.Core.Membership.Dto.User, V6User>();

            Mapper.CreateMap<Albpcc, Models.Accounting.DTO.BoPhanSuDungCongCu>();
            Mapper.CreateMap<Models.Accounting.DTO.BoPhanSuDungCongCu, Albpcc>();

            Mapper.CreateMap<ALtgcc, Models.Accounting.DTO.LyDoTangGiamCongCu>();
            Mapper.CreateMap<Models.Accounting.DTO.LyDoTangGiamCongCu, ALtgcc>();

            Mapper.CreateMap<ALplcc, Models.Accounting.DTO.PhanLoaiCongCu>();
            Mapper.CreateMap<Models.Accounting.DTO.PhanLoaiCongCu, ALplcc>();

            Mapper.CreateMap<ALnhCC, Models.Accounting.DTO.PhanNhomCongCu>();
            Mapper.CreateMap<Models.Accounting.DTO.PhanNhomCongCu, ALnhCC>();

            Mapper.CreateMap<ALnhtk0, Models.Accounting.DTO.AccountType>();
            Mapper.CreateMap<Models.Accounting.DTO.AccountType, ALnhtk0>();

            Mapper.CreateMap<ALnhtk, Models.Accounting.DTO.PhanNhomTieuKhoan>();
            Mapper.CreateMap<Models.Accounting.DTO.PhanNhomTieuKhoan, ALnhtk>();

            Mapper.CreateMap<Altk0, Models.Accounting.DTO.TaiKhoan>();
            Mapper.CreateMap<Models.Accounting.DTO.TaiKhoan, Altk0>();

            Mapper.CreateMap<ALtknh, Models.Accounting.DTO.BankAccount>();
            Mapper.CreateMap<Models.Accounting.DTO.BankAccount, ALtknh>();

            Mapper.CreateMap<ALtknh, Models.Accounting.DTO.BankAccount>();
            Mapper.CreateMap<Models.Accounting.DTO.BankAccount, ALtknh>();

            Mapper.CreateMap<Alnv, Models.Accounting.DTO.Capital>();
            Mapper.CreateMap<Models.Accounting.DTO.Capital, Alnv>();

            Mapper.CreateMap<ALtgnt, Models.Accounting.DTO.ForeignExchangeRate>();
            Mapper.CreateMap<Models.Accounting.DTO.ForeignExchangeRate, ALtgnt>();

            Mapper.CreateMap<Alck, Models.Accounting.DTO.Discount>();
            Mapper.CreateMap<Models.Accounting.DTO.Discount, Alck>();

            // Mapping Alct1
            //Mapper.CreateMap<Alct1, Models.Accounting.DTO.Alct1>();
            //Mapper.CreateMap<Models.Accounting.DTO.Alct1, Alct1>();

            // Mapping Alct
            Mapper.CreateMap<Alct, Models.Accounting.DTO.AlCt>();
            Mapper.CreateMap<Models.Accounting.DTO.AlCt, Alct>();

            // Mapping Alnt
            Mapper.CreateMap<Alnt, Models.Accounting.DTO.ALnt>();
            Mapper.CreateMap<Models.Accounting.DTO.ALnt, Alnt>();

            // Mapping alPost
            Mapper.CreateMap<ALpost, Models.Accounting.DTO.AlPost>();
            Mapper.CreateMap<Models.Accounting.DTO.AlPost, ALpost>();

            Mapper.CreateMap<Alloaick, Models.Accounting.DTO.DiscountType>();
            Mapper.CreateMap<Models.Accounting.DTO.DiscountType, Alloaick>();

            Mapper.CreateMap<Alphuong, Models.Accounting.DTO.Ward>();
            Mapper.CreateMap<Models.Accounting.DTO.Ward, Alphuong>();

            Mapper.CreateMap<Alquan, Models.Accounting.DTO.District>();
            Mapper.CreateMap<Models.Accounting.DTO.District, Alquan>();

            Mapper.CreateMap<Alqg, Models.Accounting.DTO.Nation>();
            Mapper.CreateMap<Models.Accounting.DTO.Nation, Alqg>();

            Mapper.CreateMap<ALvitri, Models.Accounting.DTO.Location>();
            Mapper.CreateMap<Models.Accounting.DTO.Location, ALvitri>();

            Mapper.CreateMap<ALloaivt, Models.Accounting.DTO.MaterialType>();
            Mapper.CreateMap<Models.Accounting.DTO.MaterialType, ALloaivt>();

            Mapper.CreateMap<ALnhvt, Models.Accounting.DTO.MaterialGroup>();
            Mapper.CreateMap<Models.Accounting.DTO.MaterialGroup, ALnhvt>();

            Mapper.CreateMap<Models.Accounting.DTO.Material, ALvt>()
                .ForMember(ent => ent.ChoPhepSuaTaiKhoanVatTu, opts => opts.MapFrom(dto => (byte)(dto.ChoPhepSuaTaiKhoanVatTu == false ? 0 : 1)))
                .ForMember(ent => ent.CreatedUserId, opts => opts.MapFrom(dto => (byte)dto.CreatedUserId))
                .ForMember(ent => ent.ModifiedUserId, opts => opts.MapFrom(dto => (byte)dto.ModifiedUserId));

            Mapper.CreateMap<ALvt, Models.Accounting.DTO.Material>()
                .ForMember(dto => dto.ChoPhepSuaTaiKhoanVatTu, opts => opts.MapFrom(ent => ent.ChoPhepSuaTaiKhoanVatTu.HasValue && ent.ChoPhepSuaTaiKhoanVatTu.Value > 0))
                .ForMember(dto => dto.CreatedUserId, opts => opts.MapFrom(ent => (int)ent.CreatedUserId))
                .ForMember(dto => dto.ModifiedUserId, opts => opts.MapFrom(ent => (int)ent.ModifiedUserId));

            Mapper.CreateMap<Altinh, Models.Accounting.DTO.Province>();
            Mapper.CreateMap<Models.Accounting.DTO.Province, Altinh>();

            Mapper.CreateMap<Almagia, Models.Accounting.DTO.PriceCode>();
            Mapper.CreateMap<Models.Accounting.DTO.PriceCode, Almagia>();

            Mapper.CreateMap<Alnhkh2, Models.Accounting.DTO.CustomerPriceGroup>();
            Mapper.CreateMap<Models.Accounting.DTO.CustomerPriceGroup, Alnhkh2>();

            Mapper.CreateMap<Alnhvt2, Models.Accounting.DTO.MaterialPriceGroup>();
            Mapper.CreateMap<Models.Accounting.DTO.MaterialPriceGroup, Alnhvt2>();

            Mapper.CreateMap<Aldvt, Models.Accounting.DTO.MeasurementUnit>();
            Mapper.CreateMap<Models.Accounting.DTO.MeasurementUnit, Aldvt>();

            Mapper.CreateMap<Alloaivc, Models.Accounting.DTO.ServiceType>();
            Mapper.CreateMap<Models.Accounting.DTO.ServiceType, Alloaivc>();

            Mapper.CreateMap<ALvttg, Models.Accounting.DTO.IntermediateProduct>();
            Mapper.CreateMap<Models.Accounting.DTO.IntermediateProduct, ALvttg>();

            Mapper.CreateMap<ALqddvt, Models.Accounting.DTO.MeasurementConversion>();
            Mapper.CreateMap<Models.Accounting.DTO.MeasurementConversion, ALqddvt>();

            Mapper.CreateMap<ALttvt, Models.Accounting.DTO.ServiceStatus>();
            Mapper.CreateMap<Models.Accounting.DTO.ServiceStatus, ALttvt>();

            Mapper.CreateMap<ALMAUHD, Models.Accounting.DTO.InvoiceTemplate>();
            Mapper.CreateMap<Models.Accounting.DTO.InvoiceTemplate, ALMAUHD>();

            Mapper.CreateMap<Alnhku, Models.Accounting.DTO.IndentureGroup>();
            Mapper.CreateMap<Models.Accounting.DTO.IndentureGroup, Alnhku>();

            Mapper.CreateMap<Alku, Models.Accounting.DTO.Indenture>();
            Mapper.CreateMap<Models.Accounting.DTO.Indenture, Alku>();

            Mapper.CreateMap<Alvc, Models.Accounting.DTO.Shipment>();
            Mapper.CreateMap<Models.Accounting.DTO.Shipment, Alvc>();

            Mapper.CreateMap<Alhttt, Models.Accounting.DTO.PaymentMethod>();
            Mapper.CreateMap<Models.Accounting.DTO.PaymentMethod, Alhttt>();

            Mapper.CreateMap<Alhtvc, Models.Accounting.DTO.ShippingMethod>();
            Mapper.CreateMap<Models.Accounting.DTO.ShippingMethod, Alhtvc>();

            Mapper.CreateMap<Althue, Models.Accounting.DTO.Tax>();
            Mapper.CreateMap<Models.Accounting.DTO.Tax, Althue>();

            Mapper.CreateMap<Alkho, Models.Accounting.DTO.Warehouse>();
            Mapper.CreateMap<Models.Accounting.DTO.Warehouse, Alkho>();

            Mapper.CreateMap<Allnx, Models.Accounting.DTO.LoaiNhapXuat>();
            Mapper.CreateMap<Models.Accounting.DTO.LoaiNhapXuat, Allnx>();

            Mapper.CreateMap<Allo, Models.Accounting.DTO.Merchandise>();
            Mapper.CreateMap<Models.Accounting.DTO.Merchandise, Allo>();

            Mapper.CreateMap<Aldvcs, Models.Accounting.DTO.BranchUnit>();
            Mapper.CreateMap<Models.Accounting.DTO.BranchUnit, Aldvcs>();

            Mapper.CreateMap<V6option, Models.Accounting.DTO.V6Option>();
            Mapper.CreateMap<Models.Accounting.DTO.V6Option, V6option>();
        }
    }
}
