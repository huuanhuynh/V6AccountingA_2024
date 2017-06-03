(function (module) {
    module.controller("QuanHuyenController", QuanHuyenController);
    QuanHuyenController.$inject = ["$scope", "KendoGridOptionsFactory", "$state"];

    function QuanHuyenController($scope, KendoGridOptionsFactory, $state) {
        var model = this;
        model.title = "QuanHuyen MANAGEMENT";
        
        model.mainGridOptions = new KendoGridOptionsFactory({
            dataSource: {
                type: "json",
                transport: {
                    read: {
                        url: "http://localhost/accapi/quanhuyens/list",
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
                { field: "maQuan", title: "Mã quận", width: "120px" },
                { field: "ten_quan", title: "Tên quận", width: "150px" },
                { field: "ngaySua", title: "Ngày sửa", width: "120px" },
                { field: "thoiGianSua", title: "Thời gian sửa", width: "120px" },
                { field: "nguoiSua", title: "Người sửa", width: "120px" },
                { field: "ngayKhoiTao", title: "Ngày khởi tạo", width: "120px" },
                { field: "gioKhoiTao", title: "Giờ khởi tạo", width: "120px" },
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