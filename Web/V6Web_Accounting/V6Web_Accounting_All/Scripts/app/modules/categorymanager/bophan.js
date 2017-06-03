(function (module) {
    module.controller("BoPhanController", BoPhanController);
    BoPhanController.$inject = ["$scope", "KendoGridOptionsFactory", "$state"];

    function BoPhanController($scope, KendoGridOptionsFactory, $state) {
        var model = this;
        model.title = "BoPhan MANAGEMENT";
        
        model.mainGridOptions = new KendoGridOptionsFactory({
            dataSource: {
                type: "json",
                transport: {
                    read: {
                        url: "http://localhost/accapi/bophans/list",
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
                { field: "maBoPhan", title: "Mã Bộ Phận", width: "120px" },
                { field: "tenBoPhan", title: "Tên Bộ Phận", width: "150px" },
                { field: "TenBoPhan2", title: "Tên Bộ phận 2", width: "120px" },
                { field: "ngaySua", title: "Ngày sửa cuối cùng", width: "120px" },
                { field: "nguoiSua", title: "Người sửa cuối cùng", width: "120px" },
                { field: "thoiGianSua", title: "thời gian sửa", width: "120px" },
                { field: "ngayNhap", title: "Ngày tạo", width: "120px" }
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