(function (module) {
    module.controller("LoaiChietKhauController", LoaiChietKhauController);
    LoaiChietKhauController.$inject = ["$scope", "KendoGridOptionsFactory", "$state"];

    function LoaiChietKhauController($scope, KendoGridOptionsFactory, $state) {
        var model = this;
        model.title = "LoaiChietKhau MANAGEMENT";
        model.mainGridOptions = new KendoGridOptionsFactory({
            dataSource: {
                type: "json",
                transport: {
                    read: {
                        url: "http://localhost/accapi/loaichietkhaus/list",
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
                { field: "maLoai", title: "Mã loại", width: "120px" },
                { field: "ten_Loai", title: "Tên Loại", width: "150px" },
                { field: "ten_Loai2", title: "Tên Loại 2", width: "150px" },
                { field: "ngaySua", title: "Ngày sửa", width: "150px" },
                { field: "thoiGianSua", title: "Thời gian sửa", width: "150px" },
                { field: "nguoiSua", title: "Người sửa", width: "150px" },
                { field: "ngayNhap", title: "Ngày nhập", width: "150px" },
                { field: "gioKhoiTao", title: "Giờ Khởi tạo", width: "150px" },
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