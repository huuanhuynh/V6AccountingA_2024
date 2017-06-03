!function (Const) {
    'use strict';

    var module = angular.module(Const.AppModule),
        buildColumnOption = function (jqTemplate) {
        var columns = [];

        jqTemplate.each(function (index, elem) {
            if (elem.nodeName != 'COL') { return; }
            var col = {
                field: elem.getAttribute('field'),
                title: elem.getAttribute('title'),
                width: elem.getAttribute('width')
            };
            columns.push(col);
        });

        return columns;
    },

    buildDatasourceOption = function (readUrl) {
        return {
            type: "json",
            transport: {
                read: {
                    url: readUrl,
                    type: "POST",
                    contentType: "application/json"
                },
                parameterMap: function (data, type) {
                    return kendo.stringify(data);
                }
            },
            schema: {
                data: "Data",
                total: "Total"
            },
            pageSize: 10,
            serverPaging: true,
            serverFiltering: true,
            serverSorting: true
        };
    },

    link = function ($scope, element, attrs, jqColTemplate) {
        //*
        var model = {},
            //jqElem = $(element),
            //colTpl = $('script:first', jqElem).html(),
            readUrl = attrs.getUrl;

        $scope.model = model;

        model.title = "CUSTOMER MANAGEMENT";
        model.mainGridOptions = {
            editable: "inline",
            sortable: true,
            pageable: {
                pageSize: 10
            },
            selectable: "row",
            //height: "650px"
            dataSource: buildDatasourceOption(readUrl),            
            columns: buildColumnOption(jqColTemplate),
            //toolbar: [{ text: "Add new record", click: "showTest" }],
            //toolbar: kendo.template(document.getElementById("template").innerHTML),
            change: function () {
                var selectedRow = this.select();
                model.selectedItem = this.dataItem(selectedRow[0]);
                $scope.$apply();
            },
            remove: function (e) {
                console.log("Removing", e.model.name);
                model.testdata.splice(0, 4);
                this.dataItems(model.testdata);
            }
        };
        //*/
    }



    module.directive('v6Grid', ['$rootScope', '$state',
        function ($rootScope, $state) {
            return {
                //replace: true,
                restrict: 'A',
                transclude: true,
                template: '<kendo-grid options="model.mainGridOptions" />',
                link: function ($scope, elem, attrs, ctrl, transclude) {
                    var jqColTemplate = null;

                    // Get column tempate
                    transclude(function (clone) {
                        angular.forEach(clone, function (cloneEl) {
                            if (cloneEl.nodeName != 'SCRIPT') { return; }
                            jqColTemplate = $(cloneEl.innerHTML);
                        });
                    });

                    link($scope, elem, attrs, jqColTemplate);
                },
                controller: ['$scope', function ($scope) {
                    
                }]
            };
        }
    ]);
}(Const);