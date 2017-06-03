!function (Const, Entity, angular) {
    'use strict';
    var module = angular.module(Const.AppModule);

    module.directive('v6ReceiptDetail', ['breeze', '$rootScope', Const.DataServiceFactory, Const.LoggerService,
        function (breeze, $rootScope, dataSvcFac, logger) {
            var customerSvc, receiptSvc,
                customerEntity;

            return {
                restrict: 'A',
                transclude: 'element',
                scope: true,
                replace: false,
                compile: function (element, attr, linker) {
                    return function link($scope, $elem) {
                        var currentReceipt,
                            offFn;

                        offFn = $rootScope.$on('metadataFetched', function () {
                            offFn(); // Deregister this event
                            receiptSvc = dataSvcFac.create(Entity.Receipt);

                            currentReceipt = receiptSvc.create({ SttRec: Date.now(), MaCt: Date.now() });
                            $scope.models = {
                                Receipt: currentReceipt
                            };
                        });

                        $scope.onSuggestionSelected = function(selected, query) {
                            if (Entity.Customer == query.entity) {
                                currentReceipt.MaKh = selected.MaKhachHang;
                                currentReceipt.DiaChi = selected.DiaChi;
                                currentReceipt.MaSoThue = selected.MaSoThue;
                            }
                        };

                        $scope.save = function() {
                            receiptSvc.saveChanges();
                        };
                        $scope.gridControl = {};
                        // Get directive content and compile as output template.
                        linker($scope, function(clone) {
                            var jqElem = angular.element(clone);
                            $elem.after(jqElem);
                        }); //linker
                    }; //link
                }
            }; // return
        } //function
    ]);

}(Const, Entity, angular);