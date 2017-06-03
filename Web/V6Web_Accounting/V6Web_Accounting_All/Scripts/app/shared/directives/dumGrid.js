!function (Const) {
    'use strict';
    var module = angular.module(Const.AppModule);

    module.directive('dumGrid', ['$rootScope', '$state', Const.DataServiceFactory,
        function ($rootScope, $state, dataSvcFac) {
            return {
                //replace: true,
                restrict: 'A',
                template: '<thead><tr><td><input data-v6typeahead type="text" class="form-control" typeahead-popup-template-url="typeahead-vattu"' +
                            'ng-model="MaVatTu" data-ta-entity="Material" data-ta-field="MaVatTu" data-ta-select="MaVatTu, TenVatTu">' +
                            '</td><td><input data-v6typeahead type="text" class="form-control" typeahead-popup-template-url="typeahead-vattu" ' +
                            'ng-model="TenVatTu" data-ta-entity="Material" data-ta-field="TenVatTu" data-ta-select="MaVatTu, TenVatTu"></td></tr></thead><tbody></tbody>',
                scope: {
                    receipt: '='
                },
                link: function ($scope, elem, attrs, ctrl, transclude) {
                    var recDetailSvc = dataSvcFac.create(Entity.ReceiptDetail),
                        tbody;


                    $scope.onSuggestionSelected = function (selected, query) {
                        if (Entity.Material == query.entity) {
                            $scope.MaVatTu = selected.MaVatTu;
                            $scope.TenVatTu = selected.TenVatTu;
                        }
                    };

                    tbody = elem.find('tbody:first');
                    elem.find('input[type=text]').on('keypress', function (evt) {
                        if (13 !== evt.which) { return; }
                        tbody.append('<tr><td>' + $scope.MaVatTu + '</td><td>' + $scope.TenVatTu + '</td></tr>');
                        $scope.$apply(function () {
                            var receiptDetail = recDetailSvc.create({ SttRec: Date.now(), MaVt: $scope.MaVatTu, MaViTri: $scope.TenVatTu });
                            if (!$scope.receipt.ReceiptDetails) {
                                $scope.receipt.setProperty('ReceiptDetails', []);
                            }
                            $scope.receipt.ReceiptDetails.push(receiptDetail);
                            $scope.MaVatTu = $scope.TenVatTu = '';
                        });
                    });
                }
            };
        }
    ]);
}(Const);