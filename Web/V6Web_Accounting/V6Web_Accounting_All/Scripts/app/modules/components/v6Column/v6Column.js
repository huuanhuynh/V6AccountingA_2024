/**
 * @ngdoc directive
 * @name app.component.directive:v6Column
 * @restrict E
 * 
 * @param {string} field Field name of the datasource
 * @param {string} title Column header title
 * @param {string=} width Initial width of the column
 * @param {string=} [type=string] Possible options: 'integer', 'double', 'boolean', 'date', 'datetime', 'dropdown', 'string'
 * @param {string=} filterType Possible options: 'dropdown'
 * @param {string=} [hidden=false] Wether the Column is initially hidden 
 * @param {string=} [optional=true] Wether the user can hide the column
 * @param {string=} attributes Additional html attributes
 * @param {expression=} [filterable=true] To hide the filter function
 * @param {string=} hideText ???
 * @param {string=} [editable=true] Wether the column is editable
 * @param {string=} defaultValue Default value when adding a new row
 * @param {string=} minValue Min value for validation
 * @param {string=} maxValue Max value for validation
 * @param {expression=} filterEditorOption ???
 * @param {expression=} filterDatasource Datasource for dropdown filter
 * @param {string=} dropdownValue Selected value for dropdown
 * @param {string=} dropdownUrl Url for options in dropdown
 * @param {string=} dropdownRequired Required --> Rename to isRequired and use for all types
 *
 * @description
 * Grid column to show and edit different data types
 */

(function (Const) {
    'use strict';
    angular.module(Const.AppModule).directive('v6Col', [function () {
        return {            
            require: '^v6Grid',
            restrict: 'E',
            scope: {
                field: '@',
                title: '@',
                width: '@',
                type: '@',
                filterType: "@",
                hidden: '@',
                optional: '@',
                attributes: '@',
                filterable: '=?',
                hideText: '@',
                editable: '@',
                defaultValue: "@",
                minValue: "@",
                maxValue: "@",
                filterEditorOption: "=?",
                filterDatasource: '=?',
                dropdownValue: "@",
                dropdownUrl: "@",
                dropdownRequired: "@",
                aggregate: "@"
            },
            link: function (scope, element, attrs, v6Grid) {
                var kendoColumnDefinition = {                    
                    optional: attrs.optional === undefined ? true : scope.$eval(attrs.optional),
                    hidden: attrs.hidden === undefined ? false : scope.$eval(attrs.hidden),
                    field: scope.field,
                    id: scope.field,
                    title: scope.title,
                    width: scope.width,
                    editable: attrs.editable === undefined ? true : scope.$eval(attrs.editable)
                };
                //v6Grid.addColumn(kendoColumnDefinition);
            }
        };
    }]);
}(Const));