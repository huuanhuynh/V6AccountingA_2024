(function (module) {
    module.controller("KheUocController", KheUocController);
    KheUocController.$inject = ["$scope", "KendoGridOptionsFactory", "$state"];

    function KheUocController($scope, KendoGridOptionsFactory, $state) {
        var model = this;
        model.title = "KheUoc MANAGEMENT";
        
        model.mainGridOptions = new KendoGridOptionsFactory({
            dataSource: {
                type: "json",
                transport: {
                    read: {
                        url: "http://localhost/accapi/kheuocs/list",
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
                { field: "maKheUoc", title: "mã khế ước", width: "120px" },
                { field: "tenKheUoc", title: "tên khế ước", width: "150px" },
                { field: "tenKheUoc2", title: "tên khế ước 2", width: "120px" },
                { field: "nhomKheUoc1", title: "Nhóm khê ước 1", width: "150px" },
                { field: "nhomKheUoc2", title: "Nhóm khê ước 2", width: "150px" },
                { field: "nhomKheUoc3", title: "Nhóm khê ước 3", width: "150px" },
                { field: "ngaySua", title: "ngày sửa", width: "100px" },
                { field: "thoiGianSua", title: "thời gian sửa", width: "100px" },
                { field: "nguoiSua", title: "người sửa", width: "100px" },
                { field: "ngayNhap", title: "ngày tạo", width: "100px" }
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