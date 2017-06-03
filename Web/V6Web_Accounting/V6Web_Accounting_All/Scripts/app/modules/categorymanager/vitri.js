(function (module) {
    module.controller("ViTriController", ViTriController);
    ViTriController.$inject = ["$scope", "KendoGridOptionsFactory", "$state"];

    function ViTriController($scope, KendoGridOptionsFactory, $state) {
        var model = this;
        model.title = "ViTri MANAGEMENT";
        
        model.mainGridOptions = new KendoGridOptionsFactory({
            dataSource: {
                type: "json",
                transport: {
                    read: {
                        url: "http://localhost/accapi/vitris/list",
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
                { field: "naViTri", title: "Mã vị trí", width: "150px" },
                { field: "tenViTri", title: "Tên vị trí", width: "150px" },
                { field: "tenViTri2", title: "Tên vị trí 2", width: "150px" },
                { field: "sTT_NhapTruocXuatTruoc", title: "Số thứ tự nhập trước xuất trước", width: "200px" },
                { field: "maLoai", title: "Mã loại", width: "150px" },
                { field: "kieuNhap", title: "Kiểu nhập", width: "150px" },
                { field: "kieuXuat", title: "Kiểu xuất", width: "150px" },
                { field: "kieuBan", title: "Kiểu bán", width: "150px" },
                { field: "ngaySua", title: "Ngày sửa", width: "150px" },
                { field: "thoiGianSua", title: "Thời gian sửa", width: "150px" },
                { field: "nguoiSua", title: "Người sửa", width: "150px" },
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