(function (Const) {
    'use strict';
    angular.module(Const.AppModule).directive('v6Grid', [
        function () {
            return {
                transclude: true,
                restrict: 'E',
                scope: {
                    id: '@',
                    primaryKey: '@',
                    readUrl: '@',
                    updateUrl: '@',
                    createUrl: '@',
                    deleteUrl: '@',
                    editable: '@',
                    sortable: '@',
                    groupable: '@',
                    filterable: '@',
                    pageable: '@'
                },
                templateUrl: 'Scripts/app/modules/components/v6Grid/v6Grid.html',
                controller: ['$scope', function ($scope) {
                    $scope.columns = [
                        { field: "MaKhachHang", title: 'Mã Khách Hàng' },
                        { field: "TenKhachHang", title: 'Tên Khách Hàng' },
                        { field: 'DiaChi', title: 'Địa Chỉ' },
                        { field: 'CreatedDate', title: 'Ngày Tạo', format: "{0:dd/MM/yyyy}", }
                    ];
                    $scope.options = {
                        dataSource: {
                            type: "odata",
                            transport: {
                                read: {
                                    url: $scope.readUrl,
                                    dataType: "json",
                                    type: 'get',
                                    contentType: 'application/json',
                                    complete: function (e) {
                                        
                                        if (e.responseJSON) {
                                            handleNotification('Loading data', e);
                                        }
                                    }
                                },
                                create: {
                                    url: function (data) {
                                        return crudServiceBaseUrl;
                                    },
                                    type: 'post',
                                    dataType: 'json'
                                },
                                update: {
                                    url: function (data) {
                                        return crudServiceBaseUrl + "(" + data.CustomerID + ")";
                                    },
                                    type: "put",
                                    dataType: "json"
                                },
                                destroy: {
                                    url: function (data) {
                                        return crudServiceBaseUrl + "(" + data.CustomerID + ")";
                                    },
                                    dataType: "json"
                                }
                            },
                            batch: true,
                            serverPaging: true,
                            serverSorting: true,
                            serverFiltering: true,
                            pageSize: 10,
                            schema: {
                                data: function (data) {
                                    return data.Results;
                                },
                                total: function (data) {
                                    return data.InlineCount;
                                },
                                model: {
                                    id: '$id',
                                    fields: {
                                        MaKhachHang: { type: 'string', editable: true, nullable: false },
                                        DiaChi: { type: 'string', editable: true, nullable: false },
                                        TenKhachHang: { type: 'string', editable: true, nullable: false },
                                        CreatedDate: { type: 'date', editable: true, nullable: false}
                                    }
                                }
                            },
                            error: function (e) {
                                alert(e.xhr.responseText);
                            }
                        },
                        sortable: true,
                        pageable: true,
                        filterable: true,
                        editable: true,
                        columns: $scope.columns
                    };
                    function handleNotification(action, e) {
                        debugger;
                        if (e.xhr !== undefined) {
                            $scope.notification.show(e.xhr.responseText, "error");
                        } else if (action) {
                            var message = action + " successfully";
                            $scope.notification.show(message, "success");
                        }
                    }
                }],
                link: function (scope, element, attrs) {
                    scope.notification = $(element).find("#popupNotification").kendoNotification().data("kendoNotification");
                }

            };

        }]);
})(Const);