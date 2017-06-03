(function (module) {
    module.controller("NhomGiaVatTuController", NhomGiaVatTuController);
    NhomGiaVatTuController.$inject = ["$scope", "KendoGridOptionsFactory", "$state"];

    function NhomGiaVatTuController($scope, KendoGridOptionsFactory, $state) {
        var model = this;
        model.title = "NhomGiaVatTu MANAGEMENT";
        
        model.mainGridOptions = new KendoGridOptionsFactory({
            dataSource: {
                type: "json",
                transport: {
                    read: {
                        url: "http://localhost/accapi/nhomgiavattus/list",
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
                { field: "maNhom", title: "Mã nhóm", width: "120px" },
                { field: "tenNhom", title: "Tên nhóm", width: "150px" },
                { field: "tenNhom2", title: "Tên Nhóm 2", width: "150px" },
                { field: "ngaySua", title: "Ngày sửa", width: "150px" },
                { field: "thoiGianSua", title: "Thời gian sửa", width: "150px" },
                { field: "nguoiSua", title: "Người sửa", width: "150px" },
                { field: "nguoiNhap", title: "Người tạo", width: "150px" },
                { field: "ngayKhoiTao", title: "ngày khởi tạo", width: "150px" },
                { field: "gioKhoiTao", title: "Thời gian khởi tạo", width: "150px" },
                { field: "tenNhom", title: "tên bô phận", width: "150px" },
                { field: "tenNhom", title: "tên bô phận", width: "150px" },
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