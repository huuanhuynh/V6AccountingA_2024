(function (Const) {
    'use strict';
    angular.module(Const.AppModule).factory('breezeDataSource',
        [Const.DataServiceFactory,
        function (breezeDataSvc) {
            return {
                create: function(options) {
                    kendo.data.extensions = kendo.data.extensions || {};
                    var paramOpts = options;
                    function BreezeTransport(entityManager, endPoint, select) {
                        this.entityManager = entityManager;
                        this.endPoint = endPoint;
                        this.select = select;
                    }

                    function changeKendoCompToBreezeComp(operator) {

                        var op = breeze.FilterQueryOp;

                        switch (operator) {
                            case 'eq':
                                return op.Equals;
                            case 'neq':
                                return op.NotEquals;
                            case 'lt':
                                return op.LessThan;
                            case 'lte':
                                return op.LessThanOrEqual;
                            case 'gt':
                                return op.GreaterThan;
                            case 'gte':
                                return op.GreaterThanOrEqual;
                            case 'startswith':
                                return op.StartsWith;
                            case 'endswith':
                                return op.EndsWith;
                            case 'contains':
                                return op.Contains;
                        }

                        return '';
                    }

                    $.extend(BreezeTransport.prototype, {
                        read: function (options) {
                            var Predicate = breeze.Predicate;

                            var orderVal = '';
                            var sortOps = options.data.sort;

                            var filterOps = options.data.filter;

                            if (sortOps) {
                                orderVal = sortOps[0].field + " " + sortOps[0].dir;
                            } else {
                                orderVal = null;
                            }

                            var filters = [];
                            if (filterOps) {



                                for (var i = 0; i < filterOps.filters.length; i++) {
                                    var gridFilter = filterOps.filters[i];

                                    var breezeFilter = Predicate.create(gridFilter.field, changeKendoCompToBreezeComp(gridFilter.operator), gridFilter.value);
                                    filters.push(breezeFilter);
                                }

                            }
                            var pred = Predicate.and(filters);

                            var query = new breeze.EntityQuery(this.endPoint)
                            .select(this.select)
                            .where(pred)
                            .orderBy(orderVal)
                            .skip(options.data.skip)
                            .take(options.data.take)
                            .inlineCount();
                            debugger;
                            this.entityManager.executeQuery(query.select(paramOpts.fields)).then(function (xhr) {
                                options.success(xhr.results);
                            });
                        },

                        create: function (options) {
                            debugger;
                            options.success(options.data);
                        },
                        update: function (options) {
                            options.success(options.data);
                        },
                        destroy: function (options) {
                            options.success(options.data);
                        }
                    });

                    // Create the custom DataSource by extending a kendo.data.DataSource
                    // and specify an init method that wires up needed functionality.
                    kendo.data.extensions.BreezeDataSource = kendo.data.DataSource.extend({
                        init: function (options) {
                            // The endpoint and entityManager fields are required. If not specified, throw an error
                            if (!options.entityManager) {
                                throw new Error('A Breeze EntityManager object is required in order to use the DataSource with Breeze. Please specify an "entityManager" property in your options object.');
                            }

                            if (!options.endPoint) {
                                throw new Error('An "endpoint" option is required in order to work with Breeze. Please specify an "endpoint" property in your options object.');
                            }

                            // build the transport and final options objects
                            var breezeTransport = new BreezeTransport(options.entityManager, options.endPoint, options.select);
                            options = $.extend({}, { transport: breezeTransport }, options);

                            // Call the "base" DataSource init function and provide our custom transport object
                            kendo.data.DataSource.fn.init.call(this, options);
                        }
                    });
                    return new kendo.data.extensions.BreezeDataSource(options);
                }                    
            };
        }]);
}(Const));