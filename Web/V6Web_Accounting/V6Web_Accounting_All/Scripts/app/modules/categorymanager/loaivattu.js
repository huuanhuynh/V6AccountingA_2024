(function (module) {
    module.controller("LoaiVatTuController", LoaiVatTuController);
    LoaiVatTuController.$inject = ["$scope", "KendoGridOptionsFactory", "$state"];

    function LoaiVatTuController($scope, KendoGridOptionsFactory, $state) {
        var model = this;
        model.title = "LoaiVatTu MANAGEMENT";
        
        model.mainGridOptions = new KendoGridOptionsFactory({
            dataSource: {
                type: "json",
                transport: {
                    read: {
                        url: "http://localhost/accapi/loaivattus/list",
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
                { field: "loai_VatTu", title: "Loại vật tư", width: "120px" },
                { field: "ten_Loai", title: "Tên loại", width: "150px" },
                { field: "ten_Loai2", title: "Tên loại 2", width: "120px" },
                { field: "ngaySua", title: "Ngày sửa", width: "120px" },
                { field: "thoiGianSua", title: "Thời gian sửa", width: "120px" },
                { field: "gioKhoiTao", title: "Giờ Khởi tạo", width: "120px" },
                { field: "nguoiNhap", title: "Người tạo mới", width: "120px" },
                { field: "ngayNhap", title: "Ngày tạo mới", width: "120px" },
            
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