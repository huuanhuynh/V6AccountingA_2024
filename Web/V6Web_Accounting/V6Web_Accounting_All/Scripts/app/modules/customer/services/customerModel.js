(function (Const) {
    'use strict';
    angular.module(Const.AppModule).factory('customerModel', function () {
        return kendo.data.Model.define({
            id: "MaKhachHang",
            fields: {
                MaKhachHang: { type: 'string', editable: false, nullable: false },
                TenKhachHang: { type: 'string', editable: true, nullable: false },
                CreatedDate: { type: 'date', editable: true, nullable: false }
            }
        });
    });
})(Const);