(function (module) {
    module.controller("DanhMucThueSuatController", DanhMucThueSuatController);
    DanhMucThueSuatController.$inject = ["$scope", "KendoGridOptionsFactory", "$state"];

    function DanhMucThueSuatController($scope, KendoGridOptionsFactory, $state) {
        var model = this;
        model.title = "DanhMucThueSuat MANAGEMENT";
        
        model.mainGridOptions = new KendoGridOptionsFactory({
            dataSource: {
                type: "json",
                transport: {
                    read: {
                        url: "http://localhost/accapi/danhmucthuesuats/list",
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
                { field: "maThue", title: "Mã Thuế", width: "120px" },
                { field: "ten_thue", title: "Tên thuế", width: "200px" },
                { field: "thueSuat", title: "Thuế suất", width: "150px" },
                { field: "taiKhoan_thue_no", title: "Tài khoảng thuế nợ", width: "150px" },
                { field: "taiKhoan_thue_co", title: "Tài khoản thuế có", width: "150px" },
                { field: "ten_thue2", title: "Tên thuế 2 ", width: "150px" },
                { field: "ngaySua", title: "Ngày sửa sau cùng", width: "150px" },
                { field: "thoiGianSua", title: "Thời gian sửa", width: "150px" },
                { field: "nguoiSua", title: "Người sửa", width: "150px" },
                { field: "ngayKhoiTao", title: "Ngày khởi tạo", width: "150px" },
                { field: "nguoiNhap", title: "Người tạo", width: "150px" },
                { field: "gioKhoiTao", title: "Giờ khởi tạo", width: "150px" }
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