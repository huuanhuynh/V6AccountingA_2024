(function (Const) {
    'use strict';
    angular.module(Const.AppModule).factory('breezeDataSource',
        [Const.DataServiceFactory,
        function (breezeDataSvc) {
            return {
                create: function (paramOpts) {
                    var exports = kendo.data.breeze = {};
                    var Predicate = breeze.Predicate;
                    var Operators = breeze.FilterQueryOp;

                    function BreezeTransport(options) {
                        var meta, typeObj, resourceName;

                        if (!options.query) {
                            throw new Error("Please specify a Breeze EntityQuery via `query` option");
                        }
                        this.query = options.query;
                        this.resourceName = resourceName = this.query.resourceName;
                        this.dataSvc = breezeDataSvc.create(resourceName);

                        meta = breezeDataSvc.getMetadataStore();
                        this.typeName = meta.getEntityTypeNameForResourceName(resourceName);
                        this.typeObj = meta.getEntityType(this.typeName);
                    }

                    function makeOperator(op) {
                        return {
                            eq: Operators.Equals,
                            neq: Operators.NotEquals,
                            lt: Operators.LessThan,
                            lte: Operators.LessThanOrEqual,
                            gt: Operators.GreaterThan,
                            gte: Operators.GreaterThanOrEqual,
                            startswith: Operators.StartsWith,
                            endswith: Operators.EndsWith,
                            contains: Operators.Contains,
                        }[op];
                    }

                    function makeFilters(args) {
                        var filters = args.filters.map(function (f) {
                            var field = f.field;
                            var operator = makeOperator(f.operator);
                            var value = f.value;
                            return Predicate.create(field, operator, value);
                        });
                        if (args.logic == "and") return Predicate.and(filters);
                        if (args.logic == "or") return Predicate.or(filters);
                        throw new Error("Unsupported predicate logic " + args.logic);
                    }

                    $.extend(BreezeTransport.prototype, {
                        read: function (options) {
                            //console.log("READ", options);
                            var query = this.query;
                            var args = options.data;
                            if (args.filter) {
                                query = query.where(makeFilters(args.filter));
                            }
                            if (args.sort && args.sort.length > 0) {
                                query = query.orderBy(args.sort.map(function (col) {
                                    return col.field + (col.dir == "desc" ? " desc" : "");
                                }).join(", "));
                            }
                            if (args.page) {
                                query = query
                                    .skip(args.skip)
                                    .take(args.take)
                                    .inlineCount();
                            }
                            try {
                                this.dataSvc.executeQuery(query.select(paramOpts.fields)).then(function (data) {
                                    options.success(this._makeResults(data));
                                }.bind(this));
                            } catch (ex) {
                                console.error(ex);
                            }
                        },
                        create: function (options) {
                            //console.log("CREATE", options);
                            this._saveChanges();
                        },
                        update: function (options) {
                            //console.log("UPDATE", options);
                            this._saveChanges();
                        },
                        destroy: function (options) {
                            //console.log("DESTROY", options);
                            this._saveChanges();
                        },

                        _createEntity: function (item) {
                            return this.dataSvc.create(this.typeName, item);
                        },
                        _saveChanges: (function () {
                            // throttle, since we will get multiple calls even in
                            // "batch" mode.
                            var timer = null;
                            return function () {
                                clearTimeout(timer);
                                setTimeout(function () {
                                    this.dataSvc.saveChanges();
                                }.bind(this), 10);
                            }.bind(this);
                        })(),

                        _makeResults: function (data) {
                            debugger;
                            var resourceName = this.query.resourceName;

                            // with the metadata, some complex objects are returned on
                            // which we can't call ObservableArray/Object (would
                            // overrun the stack).

                            var props = this.typeObj.dataProperties;
                            var a = data.results.map(function (rec) {
                                var obj = {},
                                    entity = this._createEntity(rec);
                                props.forEach(function (prop) {
                                    obj[prop.name] = entity[prop.name];
                                });
                                obj = new kendo.data.Model(obj);
                                syncItems(obj, entity);
                                return obj;
                            }.bind(this));

                            a = new kendo.data.ObservableArray(a);
                            a.bind("change", function (ev) {
                                switch (ev.action) {
                                    case "remove":
                                        ev.items.forEach(function (item) {
                                            item.__breezeEntity.entityAspect.setDeleted();
                                        });
                                        break;
                                    case "add":
                                        ev.items.forEach(function (item) {
                                            var entity = this._createEntity(iem);
                                            syncItems(item, entity);
                                        }.bind(this));
                                        break;
                                }
                            }.bind(this));
                            a.total = data.inlineCount;
                            return a;
                        },

                        _makeSchema: function () {
                            var typeObj = this.typeObj,
                                model = { fields: {} },
                                schema = {
                                    total: function (data) {
                                        return data.total;
                                    }
                                };

                            if (typeObj.keyProperties) {
                                if (typeObj.keyProperties.length == 1) {
                                    model.id = typeObj.keyProperties[0].name;
                                } else if (typeObj.keyProperties.length > 1) {
                                    console.error("Multiple-key ID not supported");
                                }
                            }
                            typeObj.dataProperties.forEach(function (prop) {
                                var type = "string";
                                if (prop.dataType.isNumeric) {
                                    type = "number";
                                }
                                else if (prop.dataType.isDate) {
                                    type = "date";
                                }
                                else if (prop.dataType.name == "Boolean") {
                                    type = "boolean";
                                }
                                model.fields[prop.name] = {
                                    type: type,
                                    defaultValue: prop.defaultValue,
                                    nullable: prop.isNullable,
                                };
                            });
                            schema.model = model;
                            return schema;
                        }
                    });

                    exports.Source = kendo.data.DataSource.extend({
                        init: function (options) {
                            var transport = new BreezeTransport(options);
                            options = $.extend({}, {
                                transport: transport,
                                schema: transport._makeSchema(),
                                batch: true
                            }, options);
                            kendo.data.DataSource.prototype.init.call(this, options);
                        }
                    });
                    return new kendo.data.breeze.Source(paramOpts);
                    function syncItems(observable, entity) {
                        var protect = Mutex();
                        observable.bind({
                            "change": protect(function (ev) {
                                if (ev.field) {
                                    entity[ev.field] = observable[ev.field];
                                } else {
                                    console.error("Unhandled ObservableObject->Breeze change event", ev);
                                }
                            })
                        });
                        entity.entityAspect.propertyChanged.subscribe(protect(function (ev) {
                            observable.set(ev.propertyName, ev.newValue);
                        }));
                        observable.__breezeEntity = entity;
                    }

                    function Mutex() {
                        var locked = false;
                        return function (f) {
                            return function () {
                                if (!locked) {
                                    locked = true;
                                    try { f.apply(this, arguments) }
                                    finally { locked = false }
                                }
                            };
                        };
                    }
                }

            };
        }]);
}(Const));