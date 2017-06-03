(function (module) {
    module.config(['$stateProvider', '$urlRouterProvider', '$ocLazyLoadProvider', function ($stateProvider, $urlRouterProvider, $ocLazyLoadProvider) {

        // Redirect any unmatched url
        $urlRouterProvider.otherwise("/dashboard");
        
        $stateProvider
            // Dashboard
            .state('dashboard', {
                url: "/dashboard",
                templateUrl: V6Server.resolveUrl("~/Dashboard"),
                data: { pageTitle: 'Dashboard', pageSubTitle: 'statistics & reports' },
                controller: "DashboardController",
                controllerAs: "model",
                resolve: {
                    loadModule: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load(V6Server.resolveUrl("~/Scripts/app/modules/dashboard/dashboard.js"));
                    }]
                }
            })
            .state('receipt', {
                url: "/receipt",
                templateUrl: V6Server.resolveUrl("~/Receipt/Detail"),
                controller: "ReceiptController",
                controllerAs: "model",
                resolve: {
                    load: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load(V6Server.resolveUrl("~/Scripts/app/modules/receipt/receipt.js"));
                    }]
                }
            })
            //Component
            .state('component', {
                abstract: true,
                url: "/component",
                template: '<ui-view />'
            })
            .state('component.grid', {
                url: "/grid",
                templateUrl: V6Server.resolveUrl("~/Component/Grid"),
                data: { pageTitle: 'Grid', pageSubTitle: 'Angular kendo grid' },
                controller: "ComponentGridController",
                controllerAs: "model"
            })
            .state('component.lookup', {
                url: "/lookup",
                templateUrl: V6Server.resolveUrl("~/Component/Lookup"),
                data: { pageTitle: 'Lookup', pageSubTitle: 'Modal lookup' },
                controller: "ComponentLookupController",
                controllerAs: "model"
            })

            //Categories management
            .state('warehouse', {
                abstract: true,
                url: '/categories/warehouse',
                template: '<ui-view />'
            })
            .state('warehouse.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/Warehouse/List"),
                data: { pageTitle: 'Warehouse', pageSubTitle: 'Listing' },
                controller: "WarehouseController",
                controllerAs: "model"
            })
            .state('warehouse.add', {
                url: '/add',
                templateUrl: V6Server.resolveUrl("~/Warehouse/Add"),
                data: { pageTitle: 'Warehouse', pageSubTitle: 'Adding' },
                controller: "WarehouseController",
                controllerAs: "model"
            })
            //Categories management customer
            .state('customer', {
                abstract: true,
                url: '/categories/customer',
                template: '<ui-view />'
            })
            .state('customer.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/Customer/List"),
                data: { pageTitle: 'Customer', pageSubTitle: 'Listing' },
                controller: "CustomerController",
                controllerAs: "model"
            })
            .state('customer.new', {
                url: '/new',
                templateUrl: V6Server.resolveUrl("~/Customer/Detail"),
                data: { pageTitle: 'Add New Customer', pageSubTitle: 'Adding New Customer' },
                controller: "CustomerController",
                controllerAs: "model"
            })
            //Categories management tai khoang ngan hàng
            .state('taikhoannganhang', {
                abstract: true,
                url: '/categories/taikhoannganhang',
                template: '<ui-view />'
            })
            .state('taikhoannganhang.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/TaiKhoanNganHang/List"),
                data: { pageTitle: 'TaiKhoanNganHang', pageSubTitle: 'Listing' },
                controller: "TaiKhoanNganHangController",
                controllerAs: "model"
            })
            //Categories management danh mục khế ước
            .state('kheuoc', {
                abstract: true,
                url: '/categories/kheuoc',
                template: '<ui-view />'
            })
            .state('kheuoc.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/KheUoc/List"),
                data: { pageTitle: 'KheUoc', pageSubTitle: 'Listing' },
                controller: "KheUocController",
                controllerAs: "model"
            })
            //Categories management danh muc nhom khe uoc
            .state('nhomkheuoc', {
                abstract: true,
                url: '/categories/nhomkheuoc',
                template: '<ui-view />'
            })
            .state('nhomkheuoc.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/NhomKheUoc/List"),
                data: { pageTitle: 'NhomKheUoc', pageSubTitle: 'Listing' },
                controller: "NhomKheUocController",
                controllerAs: "model"
            })
            //Categories management danh muc nhom khach hang
            .state('nhomkhachhang', {
                abstract: true,
                url: '/categories/nhomkhachhang',
                template: '<ui-view />'
            })
            .state('nhomkhachhang.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/NhomKhachHang/List"),
                data: { pageTitle: 'NhomKhachHang', pageSubTitle: 'Listing' },
                controller: "NhomKhachHangController",
                controllerAs: "model"
            })
            //Categories management danh muc bo phan
            .state('bophan', {
                abstract: true,
                url: '/categories/bophan',
                template: '<ui-view />'
            })
            .state('bophan.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/BoPhan/List"),
                data: { pageTitle: 'BoPhan', pageSubTitle: 'Listing' },
                controller: "BoPhanController",
                controllerAs: "model"
            })
            //Categories management danh muc chiet khau
            .state('chietkhau', {
                abstract: true,
                url: '/categories/chietkhau',
                template: '<ui-view />'
            })
            .state('chietkhau.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/ChietKhau/List"),
                data: { pageTitle: 'ChietKhau', pageSubTitle: 'Listing' },
                controller: "ChietKhauController",
                controllerAs: "model"
            })

            //Categories management danh muc hinhthucthanhtoan
            .state('hinhthucthanhtoan', {
                abstract: true,
                url: '/categories/hinhthucthanhtoan',
                template: '<ui-view />'
            })
            .state('hinhthucthanhtoan.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/HinhThucThanhToan/List"),
                data: { pageTitle: 'HinhThucThanhToan', pageSubTitle: 'Listing' },
                controller: "HinhThucThanhToanController",
                controllerAs: "model"
            })

            //Categories management danh muc LoaiChietKhau
            .state('loaichietkhau', {
                abstract: true,
                url: '/categories/loaichietkhau',
                template: '<ui-view />'
            })
            .state('loaichietkhau.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/LoaiChietKhau/List"),
                data: { pageTitle: 'LoaiChietKhau', pageSubTitle: 'Listing' },
                controller: "LoaiChietKhauController",
                controllerAs: "model"
            })

            //Categories management danh muc nhomgiakhachhang
            .state('nhomgiakhachhang', {
                abstract: true,
                url: '/categories/nhomgiakhachhang',
                template: '<ui-view />'
            })
            .state('nhomgiakhachhang.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/NhomGiaKhachHang/List"),
                data: { pageTitle: 'NhomGiaKhachHang', pageSubTitle: 'Listing' },
                controller: "NhomGiaKhachHangController",
                controllerAs: "model"
            })

            //Categories management danh muc nhomgiavattu
            .state('nhomgiavattu', {
                abstract: true,
                url: '/categories/nhomgiavattu',
                template: '<ui-view />'
            })
            .state('nhomgiavattu.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/NhomGiaVatTu/List"),
                data: { pageTitle: 'NhomGiaVatTu', pageSubTitle: 'Listing' },
                controller: "NhomGiaVatTuController",
                controllerAs: "model"
            })

            //Categories management danh muc nhanvien
            .state('nhanvien', {
                abstract: true,
                url: '/categories/nhanvien',
                template: '<ui-view />'
            })
            .state('nhanvien.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/NhanVien/List"),
                data: { pageTitle: 'NhanVien', pageSubTitle: 'Listing' },
                controller: "NhanVienController",
                controllerAs: "model"
            })
               //Categories management danh muc ma gia
            .state('danhmucmagia', {
                abstract: true,
                url: '/categories/danhmucmagia',
                template: '<ui-view />'
            })
            .state('danhmucmagia.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/DanhMucMaGia/List"),
                data: { pageTitle: 'DanhMucMaGia', pageSubTitle: 'Listing' },
                controller: "DanhMucMaGiaController",
                controllerAs: "model"
            })
               //Categories management danh muc  thue suat
            .state('danhmucthuesuat', {
                abstract: true,
                url: '/categories/danhmucthuesuat',
                template: '<ui-view />'
            })
            .state('danhmucthuesuat.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/DanhMucThueSuat/List"),
                data: { pageTitle: 'DanhMucThueSuat', pageSubTitle: 'Listing' },
                controller: "DanhMucThueSuatController",
                controllerAs: "model"
            })

                 //Categories management danh muc  mauhoadon
            .state('mauhoadon', {
                abstract: true,
                url: '/categories/mauhoadon',
                template: '<ui-view />'
            })
            .state('mauhoadon.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/MauHoaDon/List"),
                data: { pageTitle: 'MauHoaDon', pageSubTitle: 'Listing' },
                controller: "MauHoaDonController",
                controllerAs: "model"
            })
                 //Categories management danh muc  tinhthanh
            .state('tinhthanh', {
                abstract: true,
                url: '/categories/tinhthanh',
                template: '<ui-view />'
            })
            .state('tinhthanh.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/TinhThanh/List"),
                data: { pageTitle: 'TinhThanh', pageSubTitle: 'Listing' },
                controller: "TinhThanhController",
                controllerAs: "model"
            })
                 //Categories management danh muc  phuongxa
            .state('phuongxa', {
                abstract: true,
                url: '/categories/phuongxa',
                template: '<ui-view />'
            })
            .state('phuongxa.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/PhuongXa/List"),
                data: { pageTitle: 'PhuongXa', pageSubTitle: 'Listing' },
                controller: "PhuongXaController",
                controllerAs: "model"
            })

                   //Categories management danh muc  vattu
            .state('vattu', {
                abstract: true,
                url: '/categories/vattu',
                template: '<ui-view />'
            })
            .state('vattu.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/VatTu/List"),
                data: { pageTitle: 'VatTu', pageSubTitle: 'Listing' },
                controller: "VatTuController",
                controllerAs: "model"
            })
                   //Categories management danh muc  nhomvattu
            .state('nhomvattu', {
                abstract: true,
                url: '/categories/nhomvattu',
                template: '<ui-view />'
            })
            .state('nhomvattu.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/NhomVatTu/List"),
                data: { pageTitle: 'NhomVatTu', pageSubTitle: 'Listing' },
                controller: "NhomVatTuController",
                controllerAs: "model"
            })
                   //Categories management danh muc  lohang
            .state('lohang', {
                abstract: true,
                url: '/categories/lohang',
                template: '<ui-view />'
            })
            .state('lohang.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/LoHang/List"),
                data: { pageTitle: 'LoHang', pageSubTitle: 'Listing' },
                controller: "LoHangController",
                controllerAs: "model"
            })
                   //Categories management danh muc  khohang
            .state('khohang', {
                abstract: true,
                url: '/categories/khohang',
                template: '<ui-view />'
            })
            .state('khohang.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/KhoHang/List"),
                data: { pageTitle: 'KhoHang', pageSubTitle: 'Listing' },
                controller: "KhoHangController",
                controllerAs: "model"
            })
                   //Categories management danh muc  donvitinh
            .state('donvitinh', {
                abstract: true,
                url: '/categories/donvitinh',
                template: '<ui-view />'
            })
            .state('donvitinh.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/DonViTinh/List"),
                data: { pageTitle: 'DonViTinh', pageSubTitle: 'Listing' },
                controller: "DonViTinhController",
                controllerAs: "model"
            })
                   //Categories management danh muc  loaivattu
            .state('loaivattu', {
                abstract: true,
                url: '/categories/loaivattu',
                template: '<ui-view />'
            })
            .state('loaivattu.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/LoaiVatTu/List"),
                data: { pageTitle: 'LoaiVatTu', pageSubTitle: 'Listing' },
                controller: "LoaiVatTuController",
                controllerAs: "model"
            })
                   //Categories management danh muc  tinhtrangdichvu
            .state('tinhtrangdichvu', {
                abstract: true,
                url: '/categories/tinhtrangdichvu',
                template: '<ui-view />'
            })
            .state('tinhtrangdichvu.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/TinhTrangDichVu/List"),
                data: { pageTitle: 'TinhTrangDichVu', pageSubTitle: 'Listing' },
                controller: "TinhTrangDichVuController",
                controllerAs: "model"
            })
                   //Categories management danh muc  vitri
            .state('vitri', {
                abstract: true,
                url: '/categories/vitri',
                template: '<ui-view />'
            })
            .state('vitri.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/ViTri/List"),
                data: { pageTitle: 'ViTri', pageSubTitle: 'Listing' },
                controller: "ViTriController",
                controllerAs: "model"
            })
                   //Categories management danh muc  QuyDoiDonViTinh
            .state('quydoidonvitinh', {
                abstract: true,
                url: '/categories/quydoidonvitinh',
                template: '<ui-view />'
            })
            .state('quydoidonvitinh.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/QuyDoiDonViTinh/List"),
                data: { pageTitle: 'QuyDoiDonViTinh', pageSubTitle: 'Listing' },
                controller: "QuyDoiDonViTinhController",
                controllerAs: "model"
            })
                   //Categories management danh muc  vanchuyen
            .state('vanchuyen', {
                abstract: true,
                url: '/categories/vanchuyen',
                template: '<ui-view />'
            })
            .state('vanchuyen.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/VanChuyen/List"),
                data: { pageTitle: 'VanChuyen', pageSubTitle: 'Listing' },
                controller: "VanChuyenController",
                controllerAs: "model"
            })
                   //Categories management danh muc  loaidichvu
            .state('loaidichvu', {
                abstract: true,
                url: '/categories/loaidichvu',
                template: '<ui-view />'
            })
            .state('loaidichvu.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/LoaiDichVu/List"),
                data: { pageTitle: 'LoaiDichVu', pageSubTitle: 'Listing' },
                controller: "LoaiDichVuController",
                controllerAs: "model"
            })
                   //Categories management danh muc  hinhthucvanchuyen
            .state('hinhthucvanchuyen', {
                abstract: true,
                url: '/categories/hinhthucvanchuyen',
                template: '<ui-view />'
            })
            .state('hinhthucvanchuyen.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/HinhThucVanChuyen/List"),
                data: { pageTitle: 'HinhThucVanChuyen', pageSubTitle: 'Listing' },
                controller: "HinhThucVanChuyenController",
                controllerAs: "model"
            })
                   //Categories management danh muc  quocgia
            .state('quocgia', {
                abstract: true,
                url: '/categories/quocgia',
                template: '<ui-view />'
            })
            .state('quocgia.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/QuocGia/List"),
                data: { pageTitle: 'QuocGia', pageSubTitle: 'Listing' },
                controller: "QuocGiaController",
                controllerAs: "model"
            })
                   //Categories management danh muc  loainhapxuat
            .state('loainhapxuat', {
                abstract: true,
                url: '/categories/loainhapxuat',
                template: '<ui-view />'
            })
            .state('loainhapxuat.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/LoaiNhapXuat/List"),
                data: { pageTitle: 'LoaiNhapXuat', pageSubTitle: 'Listing' },
                controller: "LoaiNhapXuatController",
                controllerAs: "model"
            })
                   //Categories management danh muc  sanphamtrunggian
            .state('sanphamtrunggian', {
                abstract: true,
                url: '/categories/sanphamtrunggian',
                template: '<ui-view />'
            })
            .state('sanphamtrunggian.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/SanPhamTrungGian/List"),
                data: { pageTitle: 'SanPhamTrungGian', pageSubTitle: 'Listing' },
                controller: "SanPhamTrungGianController",
                controllerAs: "model"
            })

                  //Categories management danh muc  nguonvon
            .state('nguonvon', {
                abstract: true,
                url: '/categories/nguonvon',
                template: '<ui-view />'
            })
            .state('nguonvon.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/NguonVon/List"),
                data: { pageTitle: 'NguonVon', pageSubTitle: 'Listing' },
                controller: "NguonVonController",
                controllerAs: "model"
            })
                  //Categories management danh muc  lydotanggiamcongcu
            .state('lydotanggiamcongcu', {
                abstract: true,
                url: '/categories/lydotanggiamcongcu',
                template: '<ui-view />'
            })
            .state('lydotanggiamcongcu.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/LyDoTangGiamCongCu/List"),
                data: { pageTitle: 'LyDoTangGiamCongCu', pageSubTitle: 'Listing' },
                controller: "LyDoTangGiamCongCuController",
                controllerAs: "model"
            })
                  //Categories management danh muc  bophansudungcongcu
            .state('bophansudungcongcu', {
                abstract: true,
                url: '/categories/bophansudungcongcu',
                template: '<ui-view />'
            })
            .state('bophansudungcongcu.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/BoPhanSuDungCongCu/List"),
                data: { pageTitle: 'BoPhanSuDungCongCu', pageSubTitle: 'Listing' },
                controller: "BoPhanSuDungCongCuController",
                controllerAs: "model"
            })
                  //Categories management danh muc  phannhomcongcu
            .state('phannhomcongcu', {
                abstract: true,
                url: '/categories/phannhomcongcu',
                template: '<ui-view />'
            })
            .state('phannhomcongcu.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/PhanNhomCongCu/List"),
                data: { pageTitle: 'PhanNhomCongCu', pageSubTitle: 'Listing' },
                controller: "PhanNhomCongCuController",
                controllerAs: "model"
            })
                  //Categories management danh muc  phanloaicongcu
            .state('phanloaicongcu', {
                abstract: true,
                url: '/categories/phanloaicongcu',
                template: '<ui-view />'
            })
            .state('phanloaicongcu.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/PhanLoaiCongCu/List"),
                data: { pageTitle: 'PhanLoaiCongCu', pageSubTitle: 'Listing' },
                controller: "PhanLoaiCongCuController",
                controllerAs: "model"
            })

                    //Categories management danh muc  tieukhoan
            .state('tieukhoan', {
                abstract: true,
                url: '/categories/tieukhoan',
                template: '<ui-view />'
            })
            .state('tieukhoan.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/TieuKhoan/List"),
                data: { pageTitle: 'TieuKhoan', pageSubTitle: 'Listing' },
                controller: "TieuKhoanController",
                controllerAs: "model"
            })
                    //Categories management danh muc  taikhoan
            .state('taikhoan', {
                abstract: true,
                url: '/categories/taikhoan',
                template: '<ui-view />'
            })
            .state('taikhoan.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/TaiKhoan/List"),
                data: { pageTitle: 'TaiKhoan', pageSubTitle: 'Listing' },
                controller: "TaiKhoanController",
                controllerAs: "model"
            })
                    //Categories management danh muc  phanloaitaikhoan
            .state('phanloaitaikhoan', {
                abstract: true,
                url: '/categories/phanloaitaikhoan',
                template: '<ui-view />'
            })
            .state('phanloaitaikhoan.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/PhanLoaiTaiKhoan/List"),
                data: { pageTitle: 'PhanLoaiTaiKhoan', pageSubTitle: 'Listing' },
                controller: "PhanLoaiTaiKhoanController",
                controllerAs: "model"
            })
                    //Categories management danh muc  phannhomtieukhoan
            .state('phannhomtieukhoan', {
                abstract: true,
                url: '/categories/phannhomtieukhoan',
                template: '<ui-view />'
            })
            .state('phannhomtieukhoan.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/PhanNhomTieuKhoan/List"),
                data: { pageTitle: 'PhanNhomTieuKhoan', pageSubTitle: 'Listing' },
                controller: "PhanNhomTieuKhoanController",
                controllerAs: "model"
            })
                       //Categories management danh muc  ngoaite
            .state('ngoaite', {
                abstract: true,
                url: '/categories/ngoaite',
                template: '<ui-view />'
            })
            .state('ngoaite.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/NgoaiTe/List"),
                data: { pageTitle: 'NgoaiTe', pageSubTitle: 'Listing' },
                controller: "NgoaiTeController",
                controllerAs: "model"
            })
                       //Categories management danh muc  donvicoso
            .state('donvicoso', {
                abstract: true,
                url: '/categories/donvicoso',
                template: '<ui-view />'
            })
            .state('donvicoso.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/DonViCoSo/List"),
                data: { pageTitle: 'DonViCoSo', pageSubTitle: 'Listing' },
                controller: "DonViCoSoController",
                controllerAs: "model"
            })
                        //Categories management danh muc  tygiangoaite
            .state('tygiangoaite', {
                abstract: true,
                url: '/categories/tygiangoaite',
                template: '<ui-view />'
            })
            .state('tygiangoaite.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/TyGiaNgoaiTe/List"),
                data: { pageTitle: 'TyGiaNgoaiTe', pageSubTitle: 'Listing' },
                controller: "TyGiaNgoaiTeController",
                controllerAs: "model"
            })

                        //Categories management danh muc  quanhuyen
            .state('quanhuyen', {
                abstract: true,
                url: '/categories/quanhuyen',
                template: '<ui-view />'
            })
            .state('quanhuyen.list', {
                url: '/list',
                templateUrl: V6Server.resolveUrl("~/QuanHuyen/List"),
                data: { pageTitle: 'QuanHuyen', pageSubTitle: 'Listing' },
                controller: "QuanHuyenController",
                controllerAs: "model"
            })
        ;
    }]);
})(angular.module("app"))