
!function (V6Util, V6Names, V6Server) {
    'use strict';
    
    var Namespaces = V6Names.Namespaces,
        DynamicModelFac = V6Names.ModelFactories.DynamicModel,
        DynamicModel, DynamicModelFactory, namespace;

    DynamicModel = function () {
        this.columns;
        this.items;
    };



    DynamicModelFactory = function () { };

    // Converts from raw server JSON to DynamicModel instance
    // @param `rawArray` [json] raw server JSON
    // @return an [Array] of [MenuItem] if successfull,
    //      [null] if otherwise.
    DynamicModelFactory.prototype.createDynamicModel = function (rawModel) {
        var model = new DynamicModel(),
            attemptResult, items, fields;

        model.columns = V6Util.unsecureArray(rawModel.columns);
        model.items = items = V6Util.unsecureArray(rawModel.valueContainer.items);
        model.columns.shift();

        for (var i = 0; i < items.length; i++) {
            fields = items[i].fields = V6Util.unsecureArray(items[i].fields);
            fields.shift();
        }

        return model;
    };

    //
    // Creates namespace and service
    //

    namespace = V6Util.resolveNamespace(Namespaces.Base);
    namespace.service(DynamicModelFac, DynamicModelFactory);

}(window.V6Util, window.V6Names, window.V6Server);