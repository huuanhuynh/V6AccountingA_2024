
!function (V6Util, V6Names) {
    'use strict';

    var Namespaces = V6Names.Namespaces,
        DynamicModelCtrl = V6Names.Controllers.DynamicModel,
        DynamicModelSvc = V6Names.Services.DynamicModel,
        namespace, DynamicModelController, fetchData;

    DynamicModelController = function ($scope, dynModelSvc) {
        dynModelSvc.fetchData().then(function (model) {
            $scope.items = model.items;
            $scope.columns = model.columns;
        });
    };
    
    namespace = V6Util.resolveNamespace(Namespaces.Base);
    namespace.controller(DynamicModelCtrl,
            ['$scope', DynamicModelSvc,
                function ($scope, dynModelSvc) {
                    return new DynamicModelController($scope, dynModelSvc);
                }
            ]);

}(window.V6Util, window.V6Names);