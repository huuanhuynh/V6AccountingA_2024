/// <reference path="../../../../mod/resource/Scripts/core/jquery/jquery-2.1.0.js" />
/// <reference path="../../../../mod/resource/Scripts/core/underscore/underscore.js" />
/// <reference path="../../../../mod/resource/Scripts/app/2_utilities.js" />
/// <reference path="../../../global/Constants.js" />


!function (V6Util, V6Names, V6Server) {
    'use strict';

    var Namespaces = V6Names.Namespaces,
        MenuModelFac = V6Names.ModelFactories.Menu,
        namespace, MenuFactory, MenuItem;

    MenuItem = function (rawItem, parent) {
        this.label;
        this.icon;
        this.description;
        this.position;
        this.parent;
        this.descendants;
        this.hasDescendants;
        this.route;
        this.hasActiveChild;
        this.metadata;
        this._routeTemplate;
        
        this.ctor.apply(this, arguments);
    };

    // Constructor
    MenuItem.prototype.ctor = function (rawItem, parent) {
        this._routeTemplate = _.template(V6Server.RoutePatterns.SubMenuPage);
        this._setFields(rawItem, parent);
    };

    MenuItem.prototype._setFields = function (rawItem, parent) {
        this.label = rawItem.label;
        this.icon = rawItem.icon;
        this.description = rawItem.description;
        this.position = rawItem.position;
        this.descendants = rawItem.descendants;
        this.hasDescendants = rawItem.hasDescendants;
        this.metadata = rawItem.metadata;
        this.parent = parent;

        if (rawItem.level > 1) {
            rawItem.route = this._routeToSubPage(rawItem.oid, rawItem.route);
        }
        this.route = V6Server.resolveUrl(rawItem.route);
    };

    // Transforms URL pattern to real URL
    // Eg: "Menu/{id}/{friendlyUrl}" => "Menu/11/Lists"
    MenuItem.prototype._routeToSubPage = function (oid, route) {
        return this._routeTemplate({ id: oid, friendlyUrl: route});
    };

    MenuFactory = function () { };

    // Converts from raw server JSON to MenuItem array
    // @param `rawArray` [json] raw server JSON
    // @return an [Array] of [MenuItem] if successfull,
    //      [null] if otherwise.
    MenuFactory.prototype.createMenuItems = function (rawArray, parent) {
        var itemArr = V6Util.unsecureArray(rawArray),
            // recursive reference to this function
            createMenuItems = MenuFactory.prototype.createMenuItems;

        if (!itemArr) { return null; }

        $.each(itemArr, function (index, rawItem) {
            var newItem;
            newItem = itemArr[index] = new MenuItem(rawItem, parent);
            if (rawItem.hasDescendants) {
                newItem.descendants = createMenuItems(rawItem.descendants, newItem);
            }
        });
        return itemArr;
    };

    //
    // Creates namespace and service
    //

    namespace = V6Util.resolveNamespace(Namespaces.Menu);
    namespace.service(MenuModelFac, MenuFactory);

}(window.V6Util, window.V6Names, window.V6Server);