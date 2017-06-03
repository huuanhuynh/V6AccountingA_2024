(function (module) {
    module.controller("TaiKhoanNganHangController", TaiKhoanNganHangController);
    TaiKhoanNganHangController.$inject = ["$scope", "KendoGridOptionsFactory", "$state"];

    function TaiKhoanNganHangController($scope, KendoGridOptionsFactory, $state) {
        var model = this;
        model.title = "Tai Khoan Ngan Hang MANAGEMENT";
        
        model.mainGridOptions = new KendoGridOptionsFactory({
            dataSource: {
                type: "json",
                transport: {
                    read: {
                        url: "http://localhost/accapi/taikhoannganhangs/list",
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
                { field: "taiKhoan", title: "Tài Khoản", width: "100px" },
                { field: "taiKhoanNganHang", title: "Tài Khoản Ngân Hàng", width: "150px" },
                { field: "tenTaiKhoanNganHang", title: "Tên Tài Khoản Ngân Hàng", width: "160px" },
                { field: "tenTaiKhoanNganHang2", title: "Tên Tài Khoản Ngân Hàng 2", width: "170px" },
                { field: "diaChi", title: "Địa chỉ", width: "120px" },
                { field: "ngayKhoiTao", title: "Ngày Khởi tạo", width: "120px" },
                { field: "gioKhoiTao", title: "Giờ Khởi Tạo", width: "120px" },
                { field: "nguoiNhap", title: "Người tạo", width: "120px" },
                { field: "trangThai", title: "Trạng Thái", width: "120px" },
                { field: "ngaySua", title: "Ngày Sửa", width: "120px" },
                { field: "thoiGianSua", title: "Thời Gian Sửa", width: "120px" },
                { field: "nguoiSua", title: "Người Sửa", width: "120px" }
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