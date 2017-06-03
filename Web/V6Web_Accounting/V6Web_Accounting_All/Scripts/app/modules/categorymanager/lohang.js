(function (module) {
    module.controller("LoHangController", LoHangController);
    LoHangController.$inject = ["$scope", "KendoGridOptionsFactory", "$state"];

    function LoHangController($scope, KendoGridOptionsFactory, $state) {
        var model = this;
        model.title = "LoHang MANAGEMENT";
        
        model.mainGridOptions = new KendoGridOptionsFactory({
            dataSource: {
                type: "json",
                transport: {
                    read: {
                        url: "http://localhost/accapi/lohangs/list",
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
                { field: "maVatTu", title: "Mã vật tư", width: "120px" },
                { field: "maLo", title: "Mã lô", width: "150px" },
                { field: "tenLo", title: "Tên lô 2", width: "150px" },
                { field: "tenLo2", title: "tên lô", width: "150px" },
                { field: "maKhachHang", title: "Mã Khách Hàng", width: "150px" },
                { field: "ngayNhap", title: "Ngày nhập", width: "150px" },
                { field: "ngaySanXuat", title: "Ngày sản xuất", width: "150px" },
                { field: "ngay_BD_SuDung", title: "Ngày bắt đầu sử dụng", width: "150px" },
                { field: "ngayKiemTra", title: "Ngày kiểm tra", width: "150px" },
                { field: "ngayHetHanSuDung", title: "Ngày hết hạn sử dụng", width: "150px" },
                { field: "ngayHetHanBaoHanh", title: "Ngày hết hạn bảo hành", width: "150px" },
                { field: "maVatTu2", title: "Mã Vật tư", width: "150px" },
                { field: "nguoiSua", title: "Người sửa", width: "150px" },
                { field: "thoiGianSua", title: "Thời gian sửa", width: "150px" },
                { field: "ngaySua", title: "Ngày sửa", width: "150px" },
                { field: "nguoiNhap", title: "Người tạo", width: "150px" },
                { field: "gioKhoiTao", title: "Giờ khởi tạo", width: "150px" },
                { field: "ngayKhoiTao", title: "ngày khỏi tạo", width: "150px" }
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