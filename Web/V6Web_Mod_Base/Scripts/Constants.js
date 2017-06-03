!function () {
    'use strict';

    var V6Names = window.V6Names = window.V6Names || {},
        Namespaces = V6Names.Namespaces = V6Names.Namespaces || {},
        Controllers = V6Names.Controllers = V6Names.Controllers || {},
        ModelFactories = V6Names.ModelFactories = V6Names.ModelFactories || {},
        Services = V6Names.Services = V6Names.Services || {},
        Directives = V6Names.Directives = V6Names.Directives || {};

    Namespaces.Base = 'accApp.base';

    Services.DynamicModel = 'dynModelSvc';
    Directives.DynamicTable = 'v6DynamicTable';
    Controllers.DynamicModel = 'dynModelCtrl';
    ModelFactories.DynamicModel = 'dynModelFac';
}();