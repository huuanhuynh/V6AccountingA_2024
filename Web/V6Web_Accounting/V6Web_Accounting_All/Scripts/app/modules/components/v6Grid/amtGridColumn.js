/**
 * @ngdoc directive
 * @name amtFramework.grid.directive:amtGridColumn
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

(function () {
    "use strict";
    angular.module('amtFramework.grid')
        .directive('amtGridColumn', [
            'appConfig', 'amtXlatSvc', '$compile', function (appConfig, xlatSvc, $compile) {

                return {
                    require: '^amtGrid',
                    restrict: 'E',
                    transclude: true,
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
                        dropdownValue: "@",
                        dropdownUrl: "@",
                        dropdownRequired: "@",
                        aggregate: "@",
                        maxLength: "@"
                    },
                    templateUrl: 'Scripts/app/modules/components/v6Grid/amtGridColumn.html',
                    controller: ['$scope', function ($scope) {

                        $scope.kendoColumnDefinition = {

                        };

                        this.setFilterDefinition = function (filterEditor, operators) {
                            $scope.filterable = {
                                ui: filterEditor,
                                operators: operators
                            };
                        };
                    }],
                    link: function (scope, elem, attrs, amtGridCtrl) {
                        scope.kendoColumnDefinition.optional = attrs.optional === undefined ? true : scope.$eval(attrs.optional);
                        scope.kendoColumnDefinition.hidden = attrs.hidden === undefined ? false : scope.$eval(attrs.hidden);
                        scope.kendoColumnDefinition.field = scope.field;
                        scope.kendoColumnDefinition.id = scope.field;
                        scope.kendoColumnDefinition.title = scope.title;
                        scope.kendoColumnDefinition.width = scope.width;
                        scope.kendoColumnDefinition.editable = attrs.editable === undefined ? true : scope.$eval(attrs.editable);

                        if (attrs.aggregate) {
                            scope.kendoColumnDefinition.aggregates = [];
                            scope.kendoColumnDefinition.aggregates.push(scope.aggregate);
                            amtGridCtrl.addAggregate({
                                field: scope.field,
                                aggregate: scope.aggregate
                            });
                            switch (scope.type) {
                                case "integer":
                                    scope.kendoColumnDefinition.footerTemplate = "<div class='text-right'> #= kendo.toString(" + scope.aggregate + ", 'n0') #</div>";
                                    break;
                                case "double":
                                    scope.kendoColumnDefinition.footerTemplate = "<div class='text-right'> #= kendo.toString(" + scope.aggregate + ", 'n2') #</div>";
                                    break;
                                default:
                                    scope.kendoColumnDefinition.footerTemplate = "<div class='text-right'> #= " + scope.aggregate + " #</div>";
                                    break;
                            }
                        }

                        var buildIntegerTemplate = function () {

                            var intTemplate = '#if(' + scope.field + ' !== null){#<div class="text-right">#: kendo.toString(' + scope.field + ', "n0") #</div>#} #';

                            return intTemplate;
                        };

                        var buildIntegerEditor = function (container, options) {
                            var input = $("<input class='k-input k-textbox' data-role='numerictextbox'/>");
                            input.attr("data-type", "number");
                            input.attr("name", options.field);
                            input.appendTo(container);
                            input.kendoNumericTextBox({
                                spinners: false,
                                min: scope.minValue,
                                change: function () {
                                    var value = this.value();
                                    if (value === null && !angular.isUndefined(scope.minValue)) {
                                        this.value(scope.minValue);
                                    }
                                }
                            });
                        };

                        var buildBooleanTemplate = function () {
                            return '<div class="text-center"><input type="checkbox" #=' + scope.field + ' ? "checked=checked" : "" # disabled="disabled"></input></div>';
                        };

                        var buildBooleanEditor = function (container, options) {
                            var input = $("<input/>");
                            input.attr("type", "checkbox");
                            input.attr("name", options.field);
                            input.appendTo(container);
                        };

                        var buildDoubleTemplate = function () {
                            return '<div class="text-right">#= ' + scope.field + ' !== null ? kendo.toString(' + scope.field + ', "n2") : "" #</div>';
                        };

                        var buildDateTemplate = function () {
                            return '<div class="pull-right">#= ' + scope.field + ' !== null ? kendo.toString(new Date(' + scope.field + '), "' + appConfig.dateFormat.shortDate + '") : ""   #</div>';
                        };

                        var buildDateTimeTemplate = function () {
                            return '<div class="pull-right">#= ' + scope.field + ' !== null ? kendo.toString(new Date(' + scope.field + '), "' + appConfig.dateFormat.shortDateTime + '") : ""   #</div>';
                        };

                        var buildDateEditor = function (container, options) {
                            var model = options.model;
                            model[scope.field] = kendo.parseDate(model[scope.field]);
                            var input = $("<input />");
                            input.appendTo(container);
                            input.kendoDatePicker({
                                format: appConfig.dateFormat.shortDate,
                                value: model[scope.field],
                                change: function () {
                                    model[scope.field] = this.value();
                                }
                            });

                        };

                        var buildDateTimeEditor = function (container, options) {
                            var model = options.model;
                            model[scope.field] = kendo.parseDate(model[scope.field]);
                            var input = $("<input />");
                            input.appendTo(container);
                            input.kendoDateTimePicker({
                                format: appConfig.dateFormat.shortDateTime,
                                value: model[scope.field],
                                change: function () {
                                    model[scope.field] = this.value();
                                }
                            });
                        };


                        var buildDropDownListTemplate = function () {
                            return '<span>#= ' + scope.field + '?' + scope.field + ':""#</span>';
                        };

                        var buildDropdownListEditor = function (container, options) {
                            var model = options.model;
                            var input = $('<input />');
                            if (scope.dropdownRequired) {
                                input.attr("required", true);
                            }
                            input.appendTo(container);
                            input.kendoComboBox({
                                autoBind: false,
                                filter: "contains",
                                dataTextField: "name",
                                dataValueField: "key",
                                valuePrimitive: true,
                                value: model[scope.dropdownValue],
                                text: model[scope.field],
                                dataSource: {
                                    serverFiltering: true,
                                    transport: {
                                        read: scope.dropdownUrl,
                                        parameterMap: function (data, type) {
                                            data.searchText = '';
                                            if (data.filter && data.filter.filters.length > 0) {
                                                data.searchText = data.filter.filters[0].value;
                                                delete data.filter;
                                            }
                                            return data;
                                        }
                                    },
                                    requestEnd: function (e) {
                                        this.data(e.response.data && e.response.data.result || []);
                                        scope.filterDatasource = e.response.data && e.response.data.result || [];

                                    }
                                },
                                change: function (e) {
                                    model.dirty = true; // tell amtGrid know that row is dirty
                                    model[scope.field] = this.dataItem().name;
                                    model[scope.dropdownValue] = this.dataItem().key;
                                }
                            });

                        };

                        var buildTextEditor = function (container, options) {
                            var input = $("<input class='k-input k-textbox' />");
                            if (scope.maxLength) {
                                input.attr("maxlength", scope.maxLength);
                            }
                            input.attr("name", options.field);
                            input.appendTo(container);
                        };

                        var template;
                        var editor;
                        var filterable = "text";
                        if (scope.type) {
                            switch (scope.type) {
                                case "integer":
                                    template = buildIntegerTemplate();
                                    editor = buildIntegerEditor;
                                    filterable = {
                                        ui: function (element) {
                                            element.kendoNumericTextBox(scope.filterEditorOption || {});
                                        }
                                    };
                                    break;

                                case "double":
                                    template = buildDoubleTemplate();
                                    editor = buildIntegerEditor;
                                    break;

                                case "boolean":
                                    template = buildBooleanTemplate();
                                    editor = buildBooleanEditor;
                                    break;

                                case "date":
                                    template = buildDateTemplate();
                                    editor = buildDateEditor;
                                    filterable = {
                                        ui: function (element) {
                                            element.kendoDatePicker({
                                                format: appConfig.dateFormat.shortDate
                                            });
                                        }
                                    };
                                    break;

                                case "datetime":
                                    template = buildDateTimeTemplate();
                                    editor = buildDateTimeEditor;
                                    filterable = "date";
                                    break;

                                case "dropdown":
                                    template = buildDropDownListTemplate();
                                    editor = buildDropdownListEditor;
                                    scope.type = "string";
                                    break;
                                default:
                                    template = "";
                                    editor = buildTextEditor;
                            }

                        } else {
                            template = "";
                            editor = buildTextEditor;
                        }

                        scope.kendoColumnDefinition.type = scope.type;
                        scope.kendoColumnDefinition.template = template;
                        scope.kendoColumnDefinition.editor = editor;

                        scope.kendoColumnDefinition.filterable = scope.filterable === undefined ? filterable : scope.filterable;

                        if (scope.attributes) {
                            if (!scope.kendoColumnDefinition.attributes) {
                                scope.kendoColumnDefinition.attributes = {};
                            }
                            var attrString = scope.attributes;
                            var attrJson = JSON.parse(attrString);
                            for (var attrname in attrJson) {
                                scope.kendoColumnDefinition.attributes[attrname] = attrJson[attrname];
                            }
                        }

                        amtGridCtrl.addColumn(scope.kendoColumnDefinition);

                    }
                };
            }
        ]);
}());