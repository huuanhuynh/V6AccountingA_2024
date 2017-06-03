/// <reference path="../../../../mod/resource/Scripts/app/2_utilities.js" />
/// <reference path="../../../global/Constants.js" />
/// <reference path="../services/MenuSvc.js" />

!function (V6Util, V6Names) {
    'use strict';

    var TreeLevels = 2,
        Namespaces = V6Names.Namespaces.Menu,
        SubMenuItemsCtrl = V6Names.Controllers.SubMenuItems,
        MenuSvc = V6Names.Services.Menu,
        namespace, subMenuItemsPage;

    subMenuItemsPage = function ($scope, menuSvc) {
        menuSvc.fetchMenuChildren(TreeLevels).then(function (menuItems) {
            $scope.menuItems = menuItems;
        });
    };

    namespace = V6Util.resolveNamespace(Namespaces.Menu);
    namespace.controller(SubMenuItemsCtrl,
            ['$scope', MenuSvc, subMenuItemsPage]);

}(window.V6Util, window.V6Names);