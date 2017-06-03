(function (module) {
    module.controller("KhoHangController", KhoHangController);
    KhoHangController.$inject = ["$scope", "KendoGridOptionsFactory", "$state"];

    function KhoHangController($scope, KendoGridOptionsFactory, $state) {
        var model = this;
        model.title = "KhoHang MANAGEMENT";
        
        model.mainGridOptions = new KendoGridOptionsFactory({
            dataSource: {
                type: "json",
                transport: {
                    read: {
                        url: "http://localhost/accapi/khohangs/list",
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
                { field: "maKho", title: "Mã kho", width: "120px" },
                { field: "maDonVi", title: "Mã đơn vị", width: "120px" },
                { field: "tenKho", title: "Tên Kho", width: "120px" },
                { field: "taiKhoanDaiLy", title: "Tài khoản đại lý", width: "120px" },
                { field: "tenKho2", title: "tên kho 2", width: "120px" },
                { field: "ngaySua", title: "Ngày sửa", width: "120px" },
                { field: "thoiGianSua", title: "Thời Gian sửa", width: "120px" },
                { field: "nguoiSua", title: "người sửa", width: "120px" },
                { field: "gioKhoiTao", title: "Giờ Khởi tạo", width: "120px" },
                { field: "nguoiNhap", title: "Người tạo", width: "120px" },
                { field: "ngayKhoiTao", title: "Ngày khởi tạo", width: "150px" }
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