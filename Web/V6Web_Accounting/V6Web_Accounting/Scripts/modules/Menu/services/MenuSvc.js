/// <reference path="../../../../mod/resource/Scripts/app/1_Constants.js" />
/// <reference path="../../../../mod/resource/Scripts/app/2_utilities.js" />
/// <reference path="../../../../mod/resource/Scripts/app/services/UrlService.js" />
/// <reference path="../../../global/Constants.js" />


!function (V6Util, V6Names) {
    'use strict';

    var Namespaces = V6Names.Namespaces,
        MenuSvc = V6Names.Services.Menu,
        MenuModelFac = V6Names.ModelFactories.Menu,
        UrlSvc = V6Names.Services.Url,
        namespace, MenuService;

    MenuService = function (connector, promiseProvider, modelFactory, urlService) {
        this._menuTree = null;
        this._connector = connector;
        this._promiseProvider = promiseProvider;
        this._modelFactory = modelFactory;
        this._apiUrls = urlService.getApiUrls('menu');

    };
    
    /// Requests server for menu tree with specified level.
    /// @param `level` [Integer]
    /// @return A promise of [MenuItem] array
    MenuService.prototype.fetchMenuTree = function (level) {
        // Must use a separate defer because cannot chain $http promise.
        var deferred = this._promiseProvider.defer(),
            getMenuTreeUrl;

        // Gets from cache if available
        if (this._menuTree) {
            deferred.resolve(this._menuTree);
            return deferred.promise;
        }

        getMenuTreeUrl = this._apiUrls['getmenutree'];
        this._connector
            .get(getMenuTreeUrl + '?level=' + level, { cache: false })
            .success(function (data) {
                this._menuTree = this._modelFactory.createMenuItems(data);
                deferred.resolve(this._menuTree);
            }.bind(this))
            .error(function (data) {
                deferred.reject('Error: ' + data);
            }.bind(this));

        return deferred.promise;
    };

    
    /// Requests server for sub menu items of the specified menu Oid.
    /// @param `parentOid` [Integer]
    /// @return A promise of [MenuItem] array
    MenuService.prototype.fetchMenuChildren = function (parentOid) {
        var deferred = this._promiseProvider.defer(),
            getMenuChildrenUrl;

        // TODO: Consider if should have cache here
        // Cache?
        
        getMenuChildrenUrl = this._apiUrls['getmenuchildren'];
        this._connector
            .get(getMenuChildrenUrl + '?menuId=' + parentOid, { cache: false })
            .success(function (data) {
                var menuItems = this._modelFactory.createMenuItems(data);
                deferred.resolve(menuItems);
            }.bind(this))
            .error(function (data) {
                deferred.reject('Error: ' + data);
            }.bind(this));

        return deferred.promise;
    }

    //
    // Creates namespace and service
    //

    namespace = V6Util.resolveNamespace(Namespaces.Menu);
    namespace.service(MenuSvc,
        ['$http', '$q', MenuModelFac, UrlSvc, MenuService]);

}(window.V6Util, window.V6Names, window.ResourceConstants);