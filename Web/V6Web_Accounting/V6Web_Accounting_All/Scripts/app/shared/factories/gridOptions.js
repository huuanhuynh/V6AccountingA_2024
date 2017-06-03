(function (module) {
    module.factory("KendoGridOptionsFactory", ["GridOptions", function (GridOptions) {
        return function(data) {
            var defaultGripOptions = new GridOptions();
            if (data) {
                angular.extend(defaultGripOptions, data);
            }
            return defaultGripOptions;
        };
    }]);
    
    module.factory("GridOptions", function () {
        return function() {
            return {
                editable: "inline",
                sortable: true,
                pageable: {
                    pageSize: 10
                },
                selectable: "row"
                //height: "650px"
            };
        };
    });
})(angular.module("app.shared"))