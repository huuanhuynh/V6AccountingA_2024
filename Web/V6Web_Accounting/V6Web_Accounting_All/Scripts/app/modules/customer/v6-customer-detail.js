!function (Const, angular) {
    'use strict';
    var module = angular.module(Const.AppModule);

    module.directive('v6CustomerDetail', ['breeze', Const.BreezeEnManager, Const.LoggerService, '$compile',
        function (breeze, enManager, logger, $compile) {
            return {
                restrict: 'A',
                transclude: 'element',
                scope: true,
                replace: false,
                compile : function(element, attr, linker){
                    return function link($scope, $elem) {  
                        $scope.onSuggestionSelected = function (selected, query, $scope) {
                            $scope.models[query.entity] = angular.extend($scope.models[query.entity], selected);
                        };

                        linker($scope, function (clone) {
                            var jqElem = angular.element(clone);

                            $elem.after(jqElem);
                        }); //linker
                    } //link
                }
            }; // return
        } //function
    ]);

}(Const, angular);