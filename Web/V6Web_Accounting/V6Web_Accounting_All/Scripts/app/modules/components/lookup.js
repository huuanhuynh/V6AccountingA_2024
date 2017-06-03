(function (module) {
    module.controller("ComponentLookupController", ComponentLookupController);
    ComponentLookupController.$inject = ["$scope", "$modal"];

    function ComponentLookupController($scope, $modal) {
        var model = this;

        model.onClick = onClick;
        model.selectedItem = null;
        function onClick() {
            var modalInstance = $modal.open({
                templateUrl: 'app/shared/lookup/userLookup.html',
                controller: 'UserLookupController',
                size: "lg",
                controllerAs: 'model'
            });
            
            modalInstance.result.then(function (selectedItem) {
                model.selectedItem = selectedItem;
            }, function () {
                console.info('Modal dismissed at: ' + new Date());
            });
        }


    }
})(angular.module("app.component"))