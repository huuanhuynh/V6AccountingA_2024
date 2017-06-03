!function (Const, window, ngModule) {
    'use strict';

    var DataServiceDelegate = function (bzDataSvc, bzEnManager, entityName) {
        this.dataSvc = bzDataSvc;
        this.bzEnManager = bzEnManager;
        this.entityName = entityName;
        this.entityResource = entityName; //Assume that entity name is same with entity resource
    };
    
    DataServiceDelegate.prototype.addPropertyChangeHandler = function (handler) {
        return this.dataSvc.entityChanged.subscribe(this.entityName, handler);
    };

    DataServiceDelegate.prototype.create = function (initialValues) {
        return this.bzEnManager.createEntity(this.entityName, initialValues);
    };

    DataServiceDelegate.prototype.remove = function (customerModel) {
        this.dataSvc.remove(customerModel);
    };

    DataServiceDelegate.prototype.detach = function (customerModel) {
        this.dataSvc.detach(customerModel);
    };

    DataServiceDelegate.prototype.asQueryable = function () {
        return this.dataSvc.createQuery(this.entityResource);
    };

    /// Executes the specified query
    /// @param query: an intance of EntityQuery, should be the return value of 'asQueryable' function.
    /// @param managed: add response results to Breeze cache.
    DataServiceDelegate.prototype.executeQuery = function (query, managed) {
        var entityName = this.entityName,
            managed = !!managed; //parse to boolean

        return this.dataSvc.executeQuery(query)
            .then(function (response) {
                if (!managed || !response.results) { return response; }
                
                // Add query results to entity manager
                angular.forEach(response.results, function (item) {
                    response.entityManager.createEntity(entityName, item);
                }.bind(this));

                return response;
            });
    };

    DataServiceDelegate.prototype.hasChanges = function () {
        return this.dataSvc.hasChanges();
    };
        
    DataServiceDelegate.prototype.removePropertyChangeHandler = function (handler) {
        return this.dataSvc.removePropertyChangeHandler(handler);
    };

    DataServiceDelegate.prototype.saveChanges = function () {
        return this.dataSvc.saveChanges();
    };

    ngModule.factory(Const.DataServiceFactory, [Const.BreezeDataService, Const.BreezeEnManager,
        function (bzDataSvc, bzEnManager) {
            return {
                create: function (entityName) { return new DataServiceDelegate(bzDataSvc, bzEnManager, entityName); },
                getMetadataStore: function () { return bzEnManager.metadataStore; }
            };
    }]);

}(Const, window, angular.module(Const.AppModule));