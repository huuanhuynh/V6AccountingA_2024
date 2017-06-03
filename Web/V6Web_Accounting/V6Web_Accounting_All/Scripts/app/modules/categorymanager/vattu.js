(function (module) {
    module.controller("VatTuController", VatTuController);
    VatTuController.$inject = ["$scope", "KendoGridOptionsFactory", "$state"];

    function VatTuController($scope, KendoGridOptionsFactory, $state) {
        var model = this;
        model.title = "VatTu MANAGEMENT";
        
        model.mainGridOptions = new KendoGridOptionsFactory({
            dataSource: {
                type: "json",
                transport: {
                    read: {
                        url: "http://localhost/accapi/vattus/list",
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
                { field: "maVatTu", title: "Mã vật tư", width: "120px" },
                { field: "partNo", title: "Part No", width: "120px" },
                { field: "tenVatTu", title: "Tên vật tư", width: "120px" },
                { field: "donViTinh", title: "Đơn vị tính", width: "120px" },
                { field: "vatTuTonKho", title: "Vật tư tồn kho", width: "120px" },
                { field: "giaTon", title: "Giá tồn", width: "120px" },
                { field: "taiKhoanVatTu", title: "Tài khoản Vật tư", width: "120px" },
                { field: "taiKhoanDoanhThu", title: "Tài khoản Doanh Thu", width: "150px" },
                { field: "taiKhoanGiaVon", title: "Tài Khoản Gia Vốn", width: "150px" },
                { field: "taiKhoanTraLai", title: "Tài Khoản Trả Lại", width: "120px" },
                { field: "taiKhoan_CL_VT", title: "Tài Khoản chênh lệch", width: "150px" },
                { field: "nhomVatTu1", title: "Nhóm 1", width: "120px" },
                { field: "nhomVatTu2", title: "Nhóm 2", width: "120px" },
                { field: "nhomVatTu3", title: "Nhom 3", width: "120px" },
                { field: "soLuongToiThieu", title: "Số Lượng tối thiểu", width: "120px" },
                { field: "soLuongToiDa", title: "Số lượng tối đa", width: "120px" },
                { field: "tenVatTu2", title: "Tên Vật Tư 2", width: "120px" },
                { field: "giaTon", title: "Giá Tồn", width: "120px" },
                { field: "nguoiSua", title: "Người sửa", width: "120px" },
                { field: "thoiGianSua", title: "Thời gian sửa", width: "120px" },
                { field: "ngaySua", title: "Ngày sửa", width: "120px" },
                { field: "ngayKhoiTao", title: "Ngày khởi tạo", width: "150px" },
                { field: "gioKhoiTao", title: "Giờ khởi tạo", width: "150px" },
                { field: "nguoiNhap", title: "Người tạo mới", width: "150px" }
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