!function (angular, V6Util, V6Constants) {
    'use strict';

    var MenuNs = V6Constants.Namespaces.Menu;

    namespace = V6Util.resolveNamespace(MenuNs);
    namespace.directive(MenuTreeDir, function ($compile) {
        return new MenuTreeView($compile);
    });

}(window.angular, window.V6Util, window.V6Constants);