!function () {
    'use strict';

    var V6Names = window.V6Names = window.V6Names || {},
        Namespaces = V6Names.Namespaces = V6Names.Namespaces || {},
        Services = V6Names.Services = V6Names.Services || {},
        ModelFactories = V6Names.ModelFactories = V6Names.ModelFactories || {},
        Directives = V6Names.Directives = V6Names.Directives || {},
        Controllers = V6Names.Controllers = V6Names.Controllers || {};

    Namespaces.AccountingApp = 'accApp';
    Namespaces.Menu = 'accApp.menu';

    Controllers.MenuTree = 'menuTreeCtrl';
    Controllers.SubMenuItems = 'subMenuItemsCtrl';
    Services.Menu = 'menuSvc';
    ModelFactories.Menu = 'menuModelFac';
    Directives.MenuTree = 'v6MenuTree';

}();