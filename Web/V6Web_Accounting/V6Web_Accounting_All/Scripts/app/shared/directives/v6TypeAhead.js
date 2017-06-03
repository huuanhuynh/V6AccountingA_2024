!function (Const) {
    'use strict';

    angular.module(Const.AppModule).directive('v6typeahead', ['breeze', Const.BreezeEnManager, Const.DataServiceFactory, Const.LoggerService,
        function (breeze, enManager, dataSvcFac, logger) {
            return {
                restrict: 'A',
                replace: true,
                template: '<input type="text" onsuggestionselected="onSuggestionSelected(selected, query)" models="models" ' +
                                'uib-typeahead="result[field] for result in getSuggestion($viewValue, this)" ' +
                                'typeahead-min-length="2" typeahead-wait-ms="500" typeahead-on-select="onSelected($item, $model, $label)"> ',
                scope: {
                    onsuggestionselected: '&',
                    models: '='
                },
                link: function ($scope, elem, attrs, ctrl) {
                    var fetchFn,
                        resource = attrs['taEntity'],
                        field = attrs['taField'],
                        dataSvc = dataSvcFac.create(resource);
                    
                    $scope.onSelected = function ($item, $model, $label) {
                        var query;
                        if (!$scope.onsuggestionselected) { return; }

                        query = {
                            field: field,
                            entity: resource
                        };
                        $scope.onsuggestionselected({ selected: $item, query: query });
                        
                    };

                    $scope.field = field;

                    $scope.getSuggestion = function ($viewValue) {
                        var selectedFields = attrs['taSelect'],
                            query = dataSvc.asQueryable();
                            /*
                            query = breeze.EntityQuery
                              .from(resource)
                              .where(field, breeze.FilterQueryOp.Contains, $viewValue)
                              .take(11)
                              .select(selectedFields)
                            //*/
                        ;

                        query = query.where(field, breeze.FilterQueryOp.Contains, $viewValue)
                            .take(11)
                            .select(selectedFields);
                        return dataSvc.executeQuery(query)
                            .then(function (response) {
                                var suggestions = enManager;

                                if (!response) { return; }
                                
                                suggestions = response.results;

                                return suggestions;
                            })
                            .catch(function (error) {
                                logger.error(error);
                            });
                    };

                    /*
                    $(elem).typeahead(
                      {
                          hint: true,
                          highlight: true,
                          minLength: 2
                      },
                      {
                          name: 'apiResource',
                          async: true,
                          limit: 10,
                          source: fetchFn
                      });
                    //*/
                }
            };
        }
    ]);

}(Const);