(function (module) {
    module.controller("TaiKhoanController", TaiKhoanController);
    TaiKhoanController.$inject = ["$scope", "KendoGridOptionsFactory", "$state"];

    function TaiKhoanController($scope, KendoGridOptionsFactory, $state) {
        var model = this;
        model.title = "TaiKhoan MANAGEMENT";
        
        model.mainGridOptions = new KendoGridOptionsFactory({
            dataSource: {
                type: "json",
                transport: {
                    read: {
                        url: "http://localhost/accapi/taikhoans/list",
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
                { field: "tai_Khoan", title: "Tài khoản", width: "120px" },
                { field: "ten_TaiKhoan", title: "Tên tài khoản", width: "150px" },
                { field: "loai_TaiKhoan", title: "Loại tài khoản", width: "120px" },
                { field: "maNgoaiTe", title: "Mã ngoại tệ", width: "120px" },
                { field: "taiKhoanCongNo", title: "Tài khoản công nợ", width: "150px" },
                { field: "taiKhoan_me", title: "Tài khoản mẹ", width: "120px" },
                { field: "bac_TaiKhoan", title: "Bậc", width: "120px" },
                { field: "nh_TaiKhoan0", title: "Nhóm tài khoản", width: "120px" },
                { field: "nh_TaiKhoan2", title: "Nhóm tài khoản 2", width: "120px" },
                { field: "ten_ngan", title: "Tên ngắn", width: "120px" },
                { field: "ten_TaiKhoan2", title: "Tên tài khoản 2", width: "120px" },
                { field: "ten_ngan2", title: "Tên ngắn 2", width: "120px" }
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