/// <reference path="../../../../mod/resource/Scripts/app/2_utilities.js" />
/// <reference path="../../../global/Constants.js" />
/// <reference path="../services/MenuSvc.js" />

!function (V6Util, V6Names) {
    'use strict';

    var TreeLevels = 2,
        Namespaces = V6Names.Namespaces,
        MenuTreeCtrl = V6Names.Controllers.MenuTree,
        MenuSvc = V6Names.Services.Menu,
        namespace, menuTree;

    menuTree = function ($scope, menuSvc) {
        menuSvc.fetchMenuTree(TreeLevels).then(function (menuTree) {
            $scope.menuTree = menuTree;
        });
    };

    namespace = V6Util.resolveNamespace(Namespaces.Menu);
    namespace.controller(MenuTreeCtrl,
            ['$scope', MenuSvc, menuTree]);

}(window.V6Util, window.V6Names);