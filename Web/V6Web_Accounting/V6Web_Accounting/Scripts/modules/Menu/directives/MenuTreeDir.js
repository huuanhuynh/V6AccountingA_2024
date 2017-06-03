/// <reference path="../../../../mod/resource/Scripts/core/jquery/jquery-2.1.0.js" />
/// <reference path="../../../../mod/resource/Scripts/app/2_utilities.js" />
/// <reference path="../../../global/Constants.js" />


!function (V6Util, V6Names, location) {
    'use strict';
    
    var Selectors = {
        WaitingMenuItem: '.menu-tree__item--waiting',

        // A dropdown menu does not close if clicking on its items,
        // or if it is marked with `keep-open` CSS class.
        Unclosable: '.menu-tree__dropdown_item *, .keep-open *'
    };

    var Namespaces = V6Names.Namespaces,
        MenuTreeDir = V6Names.Directives.MenuTree,
        namespace, MenuTreeDirective;

    MenuTreeDirective = function () {
        this._routeTemplate = _.template(V6Server.RoutePatterns.SubMenuPage);
    };

    // Angular event which is invoked before data-binding
    MenuTreeDirective.prototype.compile = function (menuTreeElem, attrs) {
        return {
            pre: function () { },
            post: this.postLink.bind(this)
            // We need to bind `this` to postLink to make it work properly
        }
    }

    // Angular event which is invoked after data-binding
    MenuTreeDirective.prototype.postLink = function (scope, menuTreeElem) {
        var unregister;

        scope.isMyPage = this._isMyPage;

        unregister = scope.$watch('menuTree', function (menuTree) {
            var domItem;
            if (!menuTree) { return; }

            menuTreeElem.children(Selectors.WaitingMenuItem).remove();
            unregister(); // stop $watch-ing `menuTree`

            this._setupEventHandlers(menuTreeElem);
        }.bind(this));
    };

    MenuTreeDirective.prototype._isMyPage = function (item) {
        var isMine = (location.pathname == item.route);
        if (isMine) {
            // Alerts parent that one of his children is active.
            item.parent.hasActiveChild = true;
        }
        return isMine;
    };

    // Transforms route patterns to actual routes.
    MenuTreeDirective.prototype._resolveRoute = function (menuTree) {
        angular.forEach(menuTree, function (item) {
            if (item.level > 1) {
                item.route = this._routeToSubPage(item.oid, item.route);
            }
        }, this);
    };

    // Transforms URL pattern to real URL.
    // Eg: "Menu/{id}/{friendlyUrl}" => "Menu/11/Lists"
    MenuTreeDirective.prototype._routeToSubPage = function (oid, route) {
        return this._routeTemplate({ id: oid, friendlyUrl: route });
    };

    // Registers DOM event handlers.
    MenuTreeDirective.prototype._setupEventHandlers = function (menuTreeElem) {
        this._preventAutoClose(menuTreeElem);
    };

    // Prevents dropdown menu from closing when clicking outside menu tree
    // Allows closing only when click at the opened menu item.
    MenuTreeDirective.prototype._preventAutoClose = function (menuTreeElem) {
        $(menuTreeElem).on({
            "shown.bs.dropdown": function () { this.closable = false; },
            "click": function (evt) {
                var jTarget = $(evt.target);
                
                if (!jTarget.is(Selectors.Unclosable)) {
                    this.closable = true;
                }
            },
            "hide.bs.dropdown": function (evt) {
                if (!this.closable) {
                    evt.preventDefault();
                }
            }
        });
    };

    namespace = V6Util.resolveNamespace(Namespaces.Menu);
    namespace.directive(MenuTreeDir, function () {
        return new MenuTreeDirective();
    });

}(window.V6Util, window.V6Names, window.location);