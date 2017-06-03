(function (module) {
    module.controller("MauHoaDonController", MauHoaDonController);
    MauHoaDonController.$inject = ["$scope", "KendoGridOptionsFactory", "$state"];

    function MauHoaDonController($scope, KendoGridOptionsFactory, $state) {
        var model = this;
        model.title = "MaHoaDon MANAGEMENT";
        
        model.mainGridOptions = new KendoGridOptionsFactory({
            dataSource: {
                type: "json",
                transport: {
                    read: {
                        url: "http://localhost/accapi/mauhoadons/list",
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
                { field: "maMauHoaDon", title: "Mã Mẫu Hóa Đơn", width: "120px" },
                { field: "tenMauHoaDon", title: "Tên Mẫu Hóa Đơn", width: "150px" },
                { field: "ngaySua", title: "Ngày Sửa", width: "120px" },
                { field: "thoiGianSua", title: "Thời Gian Sửa", width: "120px" },
                { field: "nguoiSua", title: "Người Sửa", width: "120px" },
                { field: "gioKhoiTao", title: "Giờ Khởi Tạo", width: "120px" },
                { field: "ngayNhap", title: "Ngày Khởi Tạo", width: "120px" },
                { field: "nguoiNhap", title: "Người tạo", width: "120px" }
            
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