(function (module) {
    module.controller("LyDoTangGiamCongCuController", LyDoTangGiamCongCuController);
    LyDoTangGiamCongCuController.$inject = ["$scope", "KendoGridOptionsFactory", "$state"];

    function LyDoTangGiamCongCuController($scope, KendoGridOptionsFactory, $state) {
        var model = this;
        model.title = "LyDoTangGiamCongCu MANAGEMENT";
        
        model.mainGridOptions = new KendoGridOptionsFactory({
            dataSource: {
                type: "json",
                transport: {
                    read: {
                        url: "http://localhost/accapi/lydotanggiamcongcus/list",
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
                { field: "loai_tg_cc", title: "Loại tăng giảm công cụ", width: "150px" },
                { field: "ma_TGCungCap", title: "Mã tăng giảm công cụ", width: "150px" },
                { field: "ten_tg_cc", title: "Lý do tăng giảm công cụ", width: "150px" },
                { field: "ten_tg_cc2", title: "Lý do tăng giảm công cụ 2", width: "160px" },
                { field: "ngaySua", title: "ngày sửa", width: "120px" },
                { field: "thoiGianSua", title: "thời gian sửa", width: "120px" },
                { field: "nguoiSua", title: "Người sửa", width: "120px" },
                { field: "ngayKhoiTao", title: "Ngày khởi tạo", width: "120px" },
                { field: "gioKhoiTao", title: "Giờ khởi tạo", width: "120px" },
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