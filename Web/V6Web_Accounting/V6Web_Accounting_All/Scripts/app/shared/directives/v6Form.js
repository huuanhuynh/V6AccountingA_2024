!function (Const) {
    'use strict';
    
    angular.module(Const.AppModule).directive('v6Form', ['$rootScope', '$state',
        function ($rootScope, $state) {
            return {
                replace: true,
                restrict: 'AE',
                transclude: true,
                template: '<form ng-transclude ng-submit="onSubmit($event)"></form>',
                link: function ($scope, elem, attrs, ctrl) {
                    
                },
                controller: ['$scope', function ($scope) {
                    $scope.showErrors = false;
                    $scope.master = {};
                    $scope.slave = {};

                    $scope.onSubmit = function ($event) {
                        $event.preventDefault();                        
                        if ($scope.v6form.$invalid) {
                            $scope.showErrors = true;
                        } else {
                            $scope.showErrors = false;
                        }
                        return false;
                    };
                }]
            };
        }
    ]);
}(Const);