(function (module) {
    module.controller("NhomVatTuController", NhomVatTuController);
    NhomVatTuController.$inject = ["$scope", "KendoGridOptionsFactory", "$state"];

    function NhomVatTuController($scope, KendoGridOptionsFactory, $state) {
        var model = this;
        model.title = "NhomVatTu MANAGEMENT";
        
        model.mainGridOptions = new KendoGridOptionsFactory({
            dataSource: {
                type: "json",
                transport: {
                    read: {
                        url: "http://localhost/accapi/nhomvattus/list",
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
                { field: "loaiNhom", title: "Loại Nhóm", width: "120px" },
                { field: "maNhom", title: "Mã nhóm", width: "120px" },
                { field: "tenNhom", title: "Tên Nhóm", width: "120px" },
                { field: "tenNhom2", title: "Tên nhóm 2", width: "120px" },
                { field: "ngaySua", title: "Ngày sửa", width: "120px" },
                { field: "thoiGianSua", title: "Thời gian sửa", width: "120px" },
                { field: "nguoiSua", title: "Người sửa", width: "120px" },
                { field: "ngayKhoiTao", title: "Ngày tạo", width: "120px" },
                { field: "gioKhoiTao", title: "Thời gian khởi tạo", width: "120px" },
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