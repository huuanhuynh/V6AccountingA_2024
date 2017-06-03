/* global kendo: false */

/**
 * @ngdoc directive
 * @name amtFramework.grid.directive:amtGrid
 * @restrict E
 * @scope
 *
 * @param {string} id The id of the grid. Has to be unique in the whole application as it is used for storing user preferences
 * @param {string} readUrl Url to an http POST method with signature like: 
 * <pre>
 * {"filterValues":{},
 *  "page":1,
 *  "pageSize":10,
 *  "sort":[{"field":"version","dir":"asc"}],
 *           "filter":{"logic":"and", 
 *                     "filters":[{"field":"budgetType",
 *                                 "operator":"contains",
 *                                 "value":"5"
 *                               }]
 *                           }
 *                     }
 * </pre>
 * In WebApi controller use the PagedCriteria class.
 * @param {string=} updateUrl Url to an http PUT method
 * @param {string=} deleteUrl: Url to an http DELETE method
 * @param {string=} createUrl: Url to an http POST method
 * @param {expression=} filterValues To filter the data based on external filters. {fieldname: fieldvalue, ...} Works directly with {@link amtFramework.filter.directive:amtFilter amtFilter}
 * @param {string=} primaryKey Name of the field that acts as the primary key. Used for the delete and update methods
 * @param {expression=} control reference to this control to call methods(see below)
 * @param {boolean=} [editable=true] Editable
 * @param {boolean=} [deletable=true] Deletable
 * @param {boolean=} [inlineCreate=false] Create a new item inline
 * @param {boolean=} [sortable=false] Sorting
 * @param {boolean=} [groupable=false] NOT USED YET
 * @param {boolean=} [filterable=false] Filter on columns
 * @param {boolean=} [resizable=true] Columns resizable
 * @param {boolean=} [reorderable=true] Columns reorderable
 * @param {boolean=} [pageable=true] Paging enabled
 * @param {boolean=} [tickable=false] Tickbox column visible
 * @param {boolean=} [autoBind=true] When set to false the refresh() method has to be called manually.
  *                                  Useful when the user has to set filters first.
 * @param {expression=} datasource Use an external datasource
 * 
 * @param {callback=} onRead Is called after the read has succeeded
 * @param {callback=} onDatabound Is called after the grid is databound
 * @param {callback=} onSelectedItemChange Is called after a row is selected. 
 *                                       Signature: onSelectecItemChanged(items) where items = Object[]
 * @param {callback=} onSave Is called after save was succesful.
 *                                       Signature: onSave(item) where items = Object
 * @param {callback=} onDelete Is called after delete was succesful
 *                                       Signature: onDelete(item) where item = Object
 * 
 * @param {function} refresh() refreshes the grid by calling the readUrl again
 * @param {function} getTickedItems() Gives a list of the ticked rows as objects
 * @param {function} getColumns() Gives a list of columns in the grid
 * @param {function} getAllItems() Gives a list of all rows
 * @param {function} getSelectedItems() Gives a list of all selected rows
 * @param {function} selectRowById(id) Selects the row with the given primary key
 * @param {function} changeTickedSelectAll(value) Ticks or unticks all rows depending on value (true or false)
 * @description
 * Grid with support for paging sorting filtering etc.
 * 
 */


(function () {
    "use strict";
    angular.module('amtFramework.grid')
    .directive('amtGrid', ['_', '$dialogs', '$compile', 'amtXlatSvc', 'appConfig', '$timeout', '$rootScope', function (_, $dialogs, $compile, xlatSvc, appConfig, $timeout, $rootScope) {
        return {
            restrict: 'E',
            transclude: true,
            scope: {
                id: '@',
                readUrl: '@',
                updateUrl: '@',
                deleteUrl: '@',
                createUrl: '@',
                filterValues: '=',
                primaryKey: '@',
                primaryName: '@',
                datatypeName: '@',
                control: '=',
                editable: '@',
                deletable: '@',
                editableMode: "@",
                inlineCreate: '@',
                addButtonLabel: '@',
                sortable: '@',
                groupable: '@',
                filterable: '@',
                resizable: '@',
                reorderable: '@',
                pageable: '@',
                tickable: '@',
                autoBind: '@',
                datasource: '=?',
                form: '=?',
                onRead: '&',
                onDatabound: '&',
                onSelectedItemChange: '&',
                onSave: '&',
                onDelete: '&',
                onLinkColumnClick: '&',
                visibleEditButton: '@'
            },
            templateUrl: 'Scripts/app/modules/components/v6Grid/amtGrid.html',
            controller: ['$scope', function ($scope) {
                    var self = this;
                    var isSelectedAll = false;
                    var internalEditable = false;
                    var isFirstBindDataSource = true;
                    var intDataTypeName = $scope.datatypeName;
                    if ($scope.editable) {
                        internalEditable = true;
                    }
                    if ($scope.editableMode !== "batch") {
                        internalEditable = { mode: "inline" };
                    }
                    $scope.editable = $scope.$eval($scope.editable);

                    if ($scope.id === undefined) {
                        throw new Error("id is mandatory");
                    }

                    if ($scope.primaryKey === undefined) {
                        $scope.primaryKey = "id";
                    }
                    var isUsingStaticData = $scope.datasource ? true : false;
                    $scope.hasData = true;
                    $scope.optionalColumns = [];
                    $scope.selectedColumnTitles = [];
                    $scope.internalControl = $scope.control || {};


                    var remoteDataTransport = {
                        read: {
                            url: $scope.readUrl,
                            dataType: "json",
                            type: 'POST',
                            contentType: 'application/json',
                            data: function () {
                                return { filterValues: $scope.filterValues };
                            },
                            complete: function (e) {
                                if (e.responseJSON) {
                                    handleNotification(null, e.responseJSON);
                                }
                            }
                        },
                        update: {
                            url: function (opts) {
                                return replaceParam($scope.updateUrl, opts);
                            },
                            dataType: 'json',
                            method: 'Put',
                            contentType: 'application/json',
                            data: function (request) {
                                return request;
                            },
                            complete: function (e) {
                                handleNotification("Update", e.responseJSON);
                                $scope.gridElement.data("kendoGrid").dataSource.cancelChanges();
                                $scope.gridElement.data("kendoGrid").dataSource.read();
                            }
                        },
                        destroy: {
                            url: function (opts) {
                                return replaceParam($scope.deleteUrl, opts);
                            },
                            dataType: 'json',
                            method: 'Delete',
                            contentType: 'application/json',
                            data: function (request) {
                                return request;
                            },
                            complete: function (e) {
                                handleNotification("Delete", e.responseJSON);
                                $scope.gridElement.data("kendoGrid").dataSource.cancelChanges();
                                $scope.gridElement.data("kendoGrid").dataSource.read();
                            }
                        },
                        create: {
                            url: $scope.createUrl,
                            dataType: 'json',
                            method: 'Post',
                            contentType: 'application/json',
                            data: function (request) {
                                return request;
                            },
                            complete: function (e) {
                                handleNotification("Create", e.responseJSON);
                                $scope.gridElement.data("kendoGrid").dataSource.cancelChanges();
                                $scope.gridElement.data("kendoGrid").dataSource.read();
                            }
                        },
                        parameterMap: function (data) {
                            if (data.filter && data.filter.filters && data.filter.filters.length > 0) {
                                for (var i = 0; i < data.filter.filters.length; i++) {
                                    if (data.filter.filters[i].value instanceof Date) {
                                        data.filter.filters[i].value = kendo.toString(data.filter.filters[i].value, appConfig.dateFormat.shortDateServer);
                                    }
                                }
                            }
                            return kendo.stringify(data);
                        }
                    };

                    var staticDataTransport = {
                        read: function (e) {
                            var response = {
                                result: $scope.datasource,
                                total: $scope.datasource.length
                            };
                            return e.success(response);
                        },
                        update: function (e) {
                            $timeout(function () {
                                $scope.datasource = getGrid().dataItems();
                            });
                            return e.success();
                        },
                        destroy: function (e) {
                            return e.success();
                        }
                    };

                    var gridOptions = {
                        transport: isUsingStaticData ? staticDataTransport : remoteDataTransport,
                        schema: {
                            model: {
                                id: $scope.primaryKey,
                                fields: {}
                            },
                            data: function (response) {
                                $timeout(function () {
                                    $scope.isSelectAll = false;
                                    $scope.internalControl.isSelected = $scope.isSelectAll;
                                });
                                if ($scope.onRead()) {
                                    response = $scope.onRead()({ response: response });
                                }
                                return response.result || response instanceof Array && response;
                            },
                            total: function (response) {
                                if ($scope.onRead()) {
                                    response = $scope.onRead()({ response: response });
                                }

                                return response.total || response instanceof Array && response.length;
                            }
                        }
                    };


                    var dataSource = {
                        transport: gridOptions.transport,
                        schema: gridOptions.schema,
                        pageSize: getPageSize(),
                        requestStart: function (e) {

                        },
                        requestEnd: function (e) {

                        },
                        batch: $scope.editable && $scope.editableMode === "batch" ? true : false
                    };

                    if (!isUsingStaticData) {
                        dataSource.serverPaging = true;
                        dataSource.serverFiltering = true;
                        dataSource.serverSorting = true;
                        dataSource.serverGrouping = true;
                        dataSource.navigatable = true;
                    }

                    $scope.options = {
                        dataSource: dataSource,
                        filterable: {
                            mode: "menu",
                            extra: false,
                            messages: {
                                isTrue: xlatSvc.xlat("filter.true_label"),
                                isFalse: xlatSvc.xlat("filter.false_label"),
                                and: xlatSvc.xlat("filter.and_label"),
                                clear: xlatSvc.xlat("filter.clear_label"),
                                filter: xlatSvc.xlat("filter.filter_label"),
                                info: "",
                                or: xlatSvc.xlat("filter.or_label"),
                                selectValue: xlatSvc.xlat("filter.selectValue_label"),
                                cancel: xlatSvc.xlat("filter.cancel_label"),
                                operator: xlatSvc.xlat("filter.operator_label"),
                                value: xlatSvc.xlat("filter.value_label")
                            },
                            operators: {
                                string: {
                                    contains: xlatSvc.xlat("filter.contains_label"),
                                    eq: xlatSvc.xlat("filter.eq_label"),
                                    startswith: xlatSvc.xlat("filter.startsWith_label"),
                                    endswith: xlatSvc.xlat("filter.endsWith_label"),
                                },
                                integer: {
                                    eq: xlatSvc.xlat("filter.eq_label"),
                                    gte: xlatSvc.xlat("filter.greaterThanOrEqual_label"),
                                    gt: xlatSvc.xlat("filter.greaterThan_label"),
                                    lte: xlatSvc.xlat("filter.smallerThanOrEqual_label"),
                                    lt: xlatSvc.xlat("filter.smallerThan_label")
                                },
                                double: {
                                    eq: xlatSvc.xlat("filter.eq_label"),
                                    gte: xlatSvc.xlat("filter.greaterThanOrEqual_label"),
                                    gt: xlatSvc.xlat("filter.greaterThan_label"),
                                    lte: xlatSvc.xlat("filter.smallerThanOrEqual_label"),
                                    lt: xlatSvc.xlat("filter.smallerThan_label")
                                },
                                date: {
                                    eq: xlatSvc.xlat("filter.eq_label"),
                                    gte: xlatSvc.xlat("filter.greaterThanOrEqual_label"),
                                    gt: xlatSvc.xlat("filter.greaterThan_label"),
                                    lte: xlatSvc.xlat("filter.smallerThanOrEqual_label"),
                                    lt: xlatSvc.xlat("filter.smallerThan_label")
                                },
                                dropdown: {
                                    eq: xlatSvc.xlat("filter.eq_label")
                                }
                            }
                        },
                        selectable: 'row',
                        editable: internalEditable,
                        columnResize: columnResize,
                        columnHide: columnShowHide,
                        columnShow: columnShowHide,
                        columnReorder: columnReorder,
                        dataBound: dataBound,
                        columnMenu: false,
                        columns: [],
                        change: function () {
                            var grid = this;
                            $timeout(function () {
                                var selectedRows = grid.select();
                                var selectedDataItems = [];
                                for (var i = 0; i < selectedRows.length; i++) {
                                    var dataItem = grid.dataItem(selectedRows[i]);
                                    selectedDataItems.push(dataItem);
                                }
                                if ($scope.onSelectedItemChange) {
                                    $scope.onSelectedItemChange({ items: selectedDataItems });
                                }
                                $scope.internalControl.updatedItems = $scope.internalControl.getUpdatedItems();
                            });
                        },
                        edit: function (e) {
                            if (e.container.attr("cell-disabled") === "true") {
                                this.closeCell();
                            }
                            e.container.find("input.k-input.k-textbox[data-type=number]").change(function () {
                                $timeout(function () {
                                    $scope.internalControl.updatedItems = $scope.internalControl.getUpdatedItems();
                                });
                            });

                        }
                    };


                    this.primaryKey = $scope.primaryKey;

                    $scope.columns = [];

                    this.addColumn = function (column, internal) {
                        var internalCol = internal || false;
                        if (typeof (Storage) !== "undefined") {
                            var columnWidth = localStorage.getItem($scope.id + column.id + "width");
                            if (columnWidth !== null && columnWidth !== undefined) {
                                if (columnWidth.search("px") === -1) {
                                    columnWidth += "px";
                                }
                                column.width = columnWidth;
                            }

                            var columnHidden = localStorage.getItem($scope.id + column.id + "hidden");
                            if (columnHidden !== null) {
                                column.hidden = columnHidden === "true";
                            }
                        }
                        //Insert after selectcolumn and before command column
                        var pos = $scope.options.columns.length;
                        if (($scope.editable && $scope.editableMode !== "batch") && internalCol === false) {
                            pos--;
                        }
                        //add the column to the grid
                        $scope.options.columns.splice(pos, 0, column);
                        //$scope.options.editable = false;

                        //add the column to the columns array
                        $scope.columns.splice(pos, 0, column);

                        if (column.optional) {
                            if (_.find($scope.optionalColumns, function (item) { return column.field === item.field; }) === undefined) {
                                $scope.optionalColumns.splice(pos, 0, column);
                            }
                            if (column.hidden !== true && $scope.selectedColumnTitles.indexOf(column.title) === -1) {
                                $scope.selectedColumnTitles.push(column.title);
                            }
                        }

                        //set the data type of the field in the datasource
                        if (internalCol === false && $scope.options.dataSource.schema) {
                            $scope.options.dataSource.schema.model.fields[column.field] = {
                                editable: column.editable === true,
                                type: column.type || 'string'
                            };
                        }
                    };

                    $scope.commands = [];

                    this.addCommand = function (command) {
                        $scope.commands.push(command);
                        var item = {
                            name: 'custom',
                            text: command.title,
                        };

                        if (command.route) {
                            command.type = 'route';
                        }

                        else if (command.onClick) {
                            command.type = 'click';
                        }

                        else if (command.gridClick) {
                            command.type = 'gridClick';
                        }
                    };

                    this.addAggregate = function (aggregate) {
                        if ($scope.options.dataSource.aggregate === undefined) {
                            $scope.options.dataSource.aggregate = [];
                        }
                        $scope.options.dataSource.aggregate.push(aggregate);
                    };

                    this.getDataSource = function () {
                        return $scope.datasource;
                    };
                    this.removeDynamicColumn = function (field) {
                        var dynamicColumns = _.filter($scope.options.columns, function (item) {
                            return item.field && item.field.startWith(field + "[");
                        });
                        $scope.options.columns = _.difference($scope.options.columns, dynamicColumns);
                    };

                    $scope.customCommandClick = function (delegateFunction) {
                        $scope.$parent.$evalAsync(delegateFunction);
                    };

                    //Add the add button
                    if ($scope.inlineCreate === "true") {
                        var command = {
                            title: ($scope.addButtonLabel ? $scope.addButtonLabel : xlatSvc.xlat('common.add_label')),
                            gridClick: 'create()'
                        };

                        this.addCommand(command);
                    }

                    $scope.create = function () {
                        $scope.gridElement.data("kendoGrid").addRow();
                    };

                    //Add select column
                    if ($scope.tickable) {
                        this.addColumn({
                            optional: false,
                            width: "40px",
                            template: "<input type='checkbox' ng-click='toggleSelect(dataItem)' ng-model='dataItem.ticked' />",
                            title: "<input type='checkbox' ng-checked='isSelectAll' ng-model='isSelectAll' title='Select all' ng-click='toggleSelectAll($event)' />",
                            attributes: {
                                "class": "table-cell",
                                style: "text-align: center;"
                            },
                            headerAttributes: {
                                "class": "table-cell",
                                style: "text-align: center;"
                            },
                            id: "select"
                        }, true);
                    }
                    
                    if ((!$scope.editableMode || $scope.editableMode === "inline") && $scope.editable) {
                        var commandColumns = [];
                        if (!$scope.visibleEditButton || $scope.visibleEditButton === "true") {
                            commandColumns.push(
                                {
                                    name: "edit",
                                    className: "btn btn-link",
                                    text: {
                                        edit: xlatSvc.xlat("common.edit_label"),
                                        cancel: xlatSvc.xlat("common.cancel_label"),
                                        update: xlatSvc.xlat("common.save_label")
                                    },
                                    click: function (e) {
                                        var grid = getGrid();
                                        grid.one("save", function (rowDataItem) {
                                            var item = rowDataItem && rowDataItem.model;
                                            $scope.onSave({ item: item });
                                        });
                                    }
                                }
                            );
                        }

                        var isDeletable = ($scope.deletable && $scope.deletable === "false") ? false : true;
                        if (isDeletable && $scope.editable) {
                            commandColumns.push({
                                name: "remove",
                                text: xlatSvc.xlat("common.delete_label"),
                                className: "btn btn-link",
                                click: function (e) {
                                    e.preventDefault();
                                    var row = $(e.target).closest("tr"); //get the row for deletion 
                                    var dataRow = this.dataItem(row);
                                    var gridData = $(e.target).closest("[kendo-grid]").data("kendoGrid");
                                    // message confirm when delete record
                                    var confirmation_message = xlatSvc.xlat("common.deleteRecordConfirmation_message");
                                    if ($scope.primaryName && intDataTypeName) {
                                        confirmation_message = String.format(
                                            xlatSvc.xlat("common.deleteRecordConfirmation_message_parameter"),
                                            dataRow[$scope.primaryName],
                                            intDataTypeName);
                                    }

                                    var dlg = $dialogs.confirm(xlatSvc.xlat("common.confirmationDialog_title"),
                                        confirmation_message);
                                    dlg.result.then(function () {
                                        //todo: onDelelete should be called when the sync is successful
                                        if ($scope.onDelete) {
                                            $scope.onDelete({ items: dataRow });
                                        }
                                        gridData.dataSource.remove(dataRow);

                                        if ($scope.datasource) {
                                            var deletedItemIndex = -1;
                                            for (var i = 0; i < $scope.datasource.length; i++) {
                                                if (angular.equals(dataRow[$scope.primaryKey], $scope.datasource[i][$scope.primaryKey])) {
                                                    deletedItemIndex = i;
                                                    break;
                                                }
                                            }
                                            if (deletedItemIndex !== -1) {
                                                $scope.datasource.splice(deletedItemIndex, 1);
                                            }
                                        }
                                        gridData.dataSource.sync();
                                    });
                                }
                            });
                        }
                        this.addColumn({
                            command: commandColumns,
                            title: "&nbsp;",
                            optional: false,
                            width: "200px",
                            attributes: { "class": "k-command-col" },
                            id: "command"
                        }, true);
                    }
                    else if ($scope.editableMode === "batch") {
                        var saveStateCommand = {
                            name: "save",
                            title: xlatSvc.xlat("common.saveChanges_label"),
                            gridClick: function (e) {
                                var grid = getGrid();
                                grid.saveChanges();
                            }
                        };
                        var cancelChangesCommand = {
                            name: "cancel",
                            title: xlatSvc.xlat("common.cancelChanges_label"),
                            gridClick: function (e) {
                                var grid = getGrid();
                                grid.cancelChanges();
                            }

                        };
                        //this.addCommand(cancelChangesCommand);
                        //this.addCommand(saveStateCommand);
                    }
                    //}

                    $scope.$watch('datasource', function (newValue) {
                        if (newValue && newValue.length > 0) {
                            $rootScope.$broadcast("datasourceReady");
                            if ($scope.form && !isFirstBindDataSource) {
                                $scope.form.$setDirty();
                            }
                        }
                        isFirstBindDataSource = false;
                    }, true);
                    $scope.rebind = false;
                    $scope.internalControl.rebind = function () {
                        $scope.rebind = !$scope.rebind;
                    };

                    var backupGridToolbar = null;
                    //$scope.$on("kendoWidgetCreated", function (e) {
                    //    var selector = ['amt-grid[id=', $scope.id, ']'].join('');
                    //    var gridElement = $(selector).find('#grid');
                    //    var gridToolbar = $(selector).find('.k-header.k-grid-toolbar').addClass("invisible");
                    //    if (!backupGridToolbar) {
                    //        backupGridToolbar = gridToolbar.clone();
                    //    }

                    //    var gridHeader = gridElement.find(".k-grid-header");
                    //    gridToolbar.removeClass("invisible").insertBefore(gridHeader);
                    //    console.log("kendoWidgetCreated:", e);
                    //});


                    $scope.internalControl.refresh = function () {
                        var grid = getGrid();
                        if (!grid || !grid.dataSource) {
                            return false;
                        }
                        if (isUsingStaticData) {
                            grid.dataSource.read();
                        }
                        else {
                            grid.dataSource.fetch();
                        }
                        
                        return true;
                    };

                    $scope.internalControl.getTickedItems = function () {
                        var tickedItems = [];
                        var items = getGrid()._data;
                        if (!items) {
                            return tickedItems;
                        }

                        items.forEach(function (item) {
                            if (item.ticked) {
                                tickedItems.push(item);
                            }
                        });

                        return tickedItems;
                    };

                    $scope.internalControl.getColumns = function () {
                        var cols = [];
                        _.each($scope.options.columns, function (column) {
                            if (!(column.internal)) {
                                cols.push(column);
                            }
                        });
                        return cols;
                    };
                    $scope.internalControl.getAllItems = function () {
                        var grid = getGrid();
                        if (!grid) {
                            return [];
                        }
                        return grid.dataItems("tr");

                    };

                    $scope.internalControl.getSelectedItems = function () {
                        var grid = getGrid();
                        var rows = grid.select();
                        var result = [];
                        if (rows && rows.length > 0) {
                            rows.each(function (index, row) {
                                result.push(grid.dataItem(row));
                            });
                        }
                        return result;
                    };

                    $scope.internalControl.selectRowById = function (id) {
                        var grid = getGrid();
                        var dataItems = grid.dataItems("tr");
                        var rowIndex = 0;
                        for (var i = 0; i < dataItems.length; i++) {
                            var item = dataItems[i];
                            if (item[$scope.primaryKey] === +id) {
                                rowIndex = i;
                                break;
                            }
                        }
                        var row = grid.tbody.find(">tr:not(.k-grouping-row)").eq(rowIndex);
                        grid.select(row);
                    };

                    $scope.internalControl.changeTickedSelectAll = function (value) {
                        $scope.isSelectAll = value;
                        isSelectedAll = value;
                    };

                    $scope.internalControl.getUpdatedItems = function (value) {
                        var grid = getGrid();
                        if (!grid) {
                            return 0;
                        }
                        return _.filter(grid.dataItems(), function (item) {
                            return item.dirty === true;
                        });
                    };
                    $scope.internalControl.updatedItems = $scope.internalControl.getUpdatedItems();

                    $scope.model = {};
                    $scope.isSelectAll = false;

                    $scope.toggleSelectAll = function (ev) {
                        var grid = $(ev.target).closest("[kendo-grid]").data("kendoGrid");
                        var items = grid.dataSource.data();
                        items.forEach(function (item) {
                            item.ticked = ev.target.checked;
                        });
                        isSelectedAll = !isSelectedAll;
                        $scope.isSelectAll = isSelectedAll;
                        var tickedItems = $scope.internalControl.getTickedItems();
                        $scope.internalControl.isSelected = tickedItems && tickedItems.length > 0 ? true : false;
                    };


                    $scope.toggleSelect = function (dataItem) {
                        dataItem.ticked = !dataItem.ticked;
                        if ($scope.isSelectAll) {
                            isSelectedAll = !isSelectedAll;
                            $scope.isSelectAll = isSelectedAll;
                        } else {
                            var items = (getGrid()._data);
                            if (items && items.length) {
                                if (items.length === $scope.internalControl.getTickedItems().length) {
                                    isSelectedAll = true;
                                    $scope.isSelectAll = isSelectedAll;
                                }
                            }
                        }
                        var tickedItems = $scope.internalControl.getTickedItems();
                        $scope.internalControl.isSelected = tickedItems && tickedItems.length > 0 ? true : false;
                        $scope.internalControl.tickedItems = tickedItems;
                    };



                    function columnResize() {
                        storeColumProperties("width");
                    }

                    function columnShowHide() {
                        storeColumProperties("hidden");
                    }

                    function columnReorder() {
                        storeColumProperties("index");
                    }

                    function dataBound() {
                        //Save Page Size
                        savePageSize();

                        if ($scope.onDatabound) {
                            $scope.onDatabound();
                        }



                    }

                    function savePageSize() {
                        var grid = getGrid();
                        if (!grid || !grid.dataSource) {
                            return;
                        }
                        localStorage.setItem($scope.id + 'pageSize', grid.dataSource._pageSize);
                    }

                    function getPageSize() {
                        var pageSize = 10;
                        if (typeof (Storage) !== "undefined") {
                            var setting = parseInt(localStorage.getItem($scope.id + 'pageSize'));
                            if (!isNaN(setting)) {
                                pageSize = setting;
                            }
                        }

                        if ($scope.pageable === "false") {
                            pageSize = appConfig.int_max;
                        }
                        return pageSize;
                    }

                    function getGrid() {
                        var selector = ['amt-grid[id=', $scope.id, ']'].join('');
                        return ($(selector).find('#grid').data("kendoGrid"));
                    }

                    function getGridElement() {
                        var selector = ['amt-grid[id=', $scope.id, ']'].join('');
                        return $(selector).find('#grid');
                    }
                    //Save Column Sorting


                    function storeColumProperties(propertyName) {
                        if (typeof (Storage) !== "undefined") {
                            _.each(getGrid().columns, function (col) {
                                var propertyValue = col[propertyName];
                                if (propertyValue !== undefined) {
                                    localStorage.setItem($scope.id + col.id + propertyName, propertyValue);
                                }
                            });
                        }

                    }

                    function replaceParam(pattern, params) {
                        for (var paramName in params) {
                            var paramValue = params[paramName];
                            pattern = pattern.replace("[[" + paramName + "]]", encodeURIComponent(paramValue));
                        }
                        return pattern;
                    }

                    function handleNotification(action, response) {
                        if (response.status === httpDataResult.fail) {
                            $scope.notification.show(response.errors[0].message, "error");
                        } else if (action) {
                            var message = action + " successfully";
                            $scope.notification.show(message, "success");
                        }
                    }

                    var httpDataResult = {
                        fail: "fail",
                        success: "success"
                    };

                    $scope.onLinkClick = function (dataItem) {
                        if ($scope.onLinkColumnClick) {
                            $scope.onLinkColumnClick({ item: dataItem });
                        }
                    };
                }
            ],
            link: function (scope, element, attrs) {

                //scope.selectedColumnTitles = _.map(scope.optionalColumns, function (col) {
                //    if (col.hidden === false) {
                //        return col.title;
                //    }
                //});

                scope.intAutoBind = attrs.autoBind === undefined ? true : scope.$eval(attrs.autoBind);
                scope.intPageable = attrs.pageable === undefined ? true : scope.$eval(attrs.pageable);
                scope.pageDefinition = (scope.intPageable === true ? { refresh: true, pageSizes: [10, 20, 50, 100, 200] } : false);
                scope.intSortable = attrs.sortable === undefined ? false : scope.$eval(attrs.sortable);
                scope.intGroupable = attrs.groupable === undefined ? false : scope.$eval(attrs.groupable);
                scope.intFilterable = attrs.filterable === undefined ? true : scope.$eval(attrs.filterable);
                scope.intResizable = attrs.resizable === undefined ? true : scope.$eval(attrs.resizable);
                scope.intReOrderable = attrs.reorderable === undefined ? true : scope.$eval(attrs.reorderable);

                scope.options.resizable = scope.intResizable;
                scope.options.reorderable = scope.intReOrderable;
                scope.options.pageable = scope.pageDefinition;
                scope.options.autoBind = scope.intAutoBind;
                scope.options.sortable = scope.intSortable;
                scope.options.groupable = scope.intGroupable;

                scope.notification = $(element).find("#popupNotification").kendoNotification().data("kendoNotification");
                scope.gridElement = $(element).find("#grid");

                function getGrid() {
                    var selector = ['amt-grid[id=', scope.id, ']'].join('');
                    return scope.gridElement.data('kendoGrid') || ($(selector).find('#grid').data("kendoGrid"));
                }

                scope.selectedColumnsOptions = {
                    dataSource: scope.optionalColumns,
                    dataBind: { value: scope.selectedColumns },
                    select: function (e) {
                        var selectedItem = this.dataItem(e.item);
                        var checked = e.item.find('input').is(':checked');
                        if (checked) {
                            scope.selectedColumnTitles.push(selectedItem.title);
                        }
                        else {
                            var index = scope.selectedColumnTitles.indexOf(selectedItem.title);
                            scope.selectedColumnTitles.splice(index, 1);
                        }
                    },
                    dataTextField: "title",
                    dataValueField: "title",
                    valueTemplate: xlatSvc.xlat('common.selectColumns_label')
                };

                scope.onSelectedColumnsChange = function () {
                    var grid = getGrid();
                    for (var i = 0; i < scope.columns.length; i++) {
                        var col = scope.columns[i];
                        if (col.optional === false || $.inArray(col.title, scope.selectedColumnTitles) > -1) {
                            grid.showColumn(i);
                        }
                        else {
                            grid.hideColumn(i);
                            //scope.selectedColumnTitles.push(col.title);
                        }
                    }
                };

                scope.onShowSelectedColumnsOptions = function () {
                    var countColumnOptional = 0;
                    for (var i = 0; i < scope.columns.length; i++) {
                        if (!scope.columns[i].optional) {
                            countColumnOptional++;
                        }
                    }
                    return countColumnOptional !== scope.columns.length ? true : false;
                };
            }

        };
    }
    ]);
}());
