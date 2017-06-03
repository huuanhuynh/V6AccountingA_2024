!function (Const, angular) {
    var receiptCtrl;

    receiptCtrl = function ($scope, $state, dataSvc) {
        angular.extend($scope, $state.current.data);

        $scope.lookup = function (evt) {
            if (13 != evt.keyCode) { return; } // Only handles Enter key
            var manager = new LookupModal({
                dataService: dataSvc,
                target: evt.target
            });
        };

        $scope.save = function () {
            var data = $scope.models.Receipt;
        };

    };

    angular.module(Const.AppModule).registerCtrl("ReceiptController",
        ["$scope", "$state", Const.HttpDataService, receiptCtrl]);

}(Const, angular);