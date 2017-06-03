(function (module) {
    module.controller("SanPhamTrungGianController", SanPhamTrungGianController);
    SanPhamTrungGianController.$inject = ["$scope", "KendoGridOptionsFactory", "$state"];

    function SanPhamTrungGianController($scope, KendoGridOptionsFactory, $state) {
        var model = this;
        model.title = "SanPhamTrungGian MANAGEMENT";
        
        model.mainGridOptions = new KendoGridOptionsFactory({
            dataSource: {
                type: "json",
                transport: {
                    read: {
                        url: "http://localhost/accapi/sanphamtrunggians/list",
                        type: "POST",
                        contentType: "application/json"
                    },
                    parameterMap: function (data, type) {
                        return kendo.stringify(data);
                    }
                },
                schema: {
                    data: "data",
                    total: "total"
                },
                pageSize: 10,
                serverPaging: true,
                serverFiltering: true,
                serverSorting: true
            },
            //toolbar: [{ text: "Add new record", click: "showTest" }],
            toolbar: kendo.template(document.getElementById("template").innerHTML),
            change: function () {
                var selectedRow = this.select();
                model.selectedItem = this.dataItem(selectedRow[0]);
                $scope.$apply();
            },
            remove: function (e) {
                console.log("Removing", e.model.name);
                model.testdata.splice(0, 4);
                this.dataItems(model.testdata);
            },
            columns: [
                { field: "maVatTutg", title: "Mã vật tư trung gian", width: "160px" },
                { field: "partNo", title: "Part No", width: "120px" },
                { field: "tenVatTutg", title: "Tên vật tư trung gian", width: "150px" },
                { field: "donViTinh", title: "Đơn vị tính", width: "120px" },
                { field: "vatTuTonKho", title: "Vật tư tồn kho", width: "120px" },
                { field: "giaTon", title: "Giá tồn kho", width: "120px" },
                { field: "taiKhoanVatTu", title: "Tài khoản vật tư", width: "120px" },
                { field: "taiKhoanDoanhThu", title: "tài khoản doanh thu", width: "150px" },
                { field: "taiKhoanTraLai", title: "Tài khoản trả lại", width: "120px" },
                { field: "taiKhoanGiaVon", title: "Tài khoản giá vốn", width: "120px" },
                { field: "taiKhoan_cl_vt", title: "Tài khoản chênh lệch", width: "160px" },
                { field: "nhomVatTu1", title: "Nhóm vật tư 1", width: "120px" },
                { field: "nhomVatTu2", title: "Nhóm vật tư 2", width: "120px" },
                { field: "nhomVatTu3", title: "Nhóm vật tư 3", width: "120px" },
                { field: "soLuongToiThieu", title: "Số lượng tối thiểu", width: "120px" },
                { field: "soLuongToiDa", title: "Số lượng tối đa", width: "120px" },
                { field: "ngaySua", title: "Ngày sửa", width: "120px" },
                { field: "thoiGianSua", title: "thời gian sửa", width: "120px" },
                { field: "nguoiSua", title: "Người sửa", width: "120px" },
                { field: "ngayKhoiTao", title: "Ngày khởi tạo", width: "120px" },
                { field: "gioKhoiTao", title: "Giờ khởi tạo", width: "120px" },
                { field: "nguoiNhap", title: "Người tạo mới", width: "120px" }
                
                
            ]
        });

        //model.toolbarOptions = {
        //    items: [
        //        { type: "button", text: "Add new record", click: showTest, attributes: { "class": "k-grid-add" } }
        //    ]
        //};

        //huuan add // 08/10/2015
        //document.getElementById("anc").on("click", function (e) {
        //    debugger;
        //    //e.preventDefault();
        //    $state.go("warehouse.add");
        //});
    }
})(angular.module("app"))