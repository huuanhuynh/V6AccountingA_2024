
!function (V6Util, V6Names) {
    'use strict';
    
    var Namespaces = V6Names.Namespaces,
        Services = V6Names.Services,
        DynamicModelSvc = Services.DynamicModel,
        DynamicModelFac = V6Names.ModelFactories.DynamicModel,
        UrlSvc = Services.Url,
        SettingsSvc = Services.ViewSettings,
        namespace, DynamicModelService;


    DynamicModelService = function (connector, promiseProvider, urlService,
        settingsService, dynModelFactory) {
        this._menuTree = null;
        this._connector = connector;
        this._modelFactory = dynModelFactory;
        this._promiseProvider = promiseProvider;
        this._settings = settingsService.getSettings();

        // Gets api names for specified module name.
        this._apiUrls = urlService.getApiUrls(
            this._settings['moduleName']);

    };

    /// Requests server for data.
    DynamicModelService.prototype.fetchData = function () {
        // Must use a separate defer because cannot chain $http promise.
        var deferred = this._promiseProvider.defer(),
            getDataUrl, getDataApiName;

        // Gets from cache if available
        if (this._menuTree) {
            deferred.resolve(this._menuTree);
            return deferred.promise;
        }

        getDataApiName = this._settings['apiName'];
        getDataUrl = this._apiUrls[getDataApiName];
        this._connector
            .get(getDataUrl, { cache: false })
            .success(function (data) {
                var model = this._modelFactory.createDynamicModel(data);
                deferred.resolve(model);
            }.bind(this))
            .error(function (data) {
                deferred.reject('Error: ' + data);
            }.bind(this));

        return deferred.promise;
    };

    //
    // Creates namespace and service
    //

    namespace = V6Util.resolveNamespace(Namespaces.Base);    
    namespace.service(DynamicModelSvc,
        ['$http', '$q', UrlSvc, SettingsSvc, DynamicModelFac, DynamicModelService]);

}(window.V6Util, window.V6Names);