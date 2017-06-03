!function (Const, window) {
    'use strict';

    var DataService = function ($q, breeze, bzEnManager, logger) {
        this._$q = $q;
        this._breeze = breeze;
        this._bzEnManager = bzEnManager;
        this._logger = logger;
    };
        //var serviceName = 'breeze/todos'; // route to the same origin Web Api controller

        // *** Cross origin service example  ***
        // When data server and application server are in different origins
        //var serviceName = 'http://sampleservice.breezejs.com/api/todos/';

        //var manager = new this._breeze.EntityManager(serviceName);


    DataService.prototype.addPropertyChangeHandler = function (entityName, handler) {
        // Actually adds any 'entityChanged' event handler
        // call handler when an entity property of any entity changes
        return this._bzEnManager.entityChanged.subscribe(function (changeArgs) {
            var action = changeArgs.entityAction,
                entity = changeArgs.entity;
            if (entity === entityName && action === this._breeze.EntityAction.PropertyChange) {
                handler(changeArgs);
            }
        });
    };

    DataService.prototype.create = function (entityName, initialValues) {
        return this._bzEnManager.createEntity(entityName, initialValues);
    };

    DataService.prototype.remove = function (entityModel) {
        if (entityModel) {
            var aspect = entityModel.entityAspect;
            if (aspect.isBeingSaved && aspect.entityState.isAdded()) {
                // wait to delete added entity while it is being saved  
                setTimeout(function () { remove(entityModel); }, 100);
                return;
            }
            aspect.setDeleted();
            saveChanges();
        }
    };

    DataService.prototype.detach = function (entity) {
        if (!entity) { return; }
        this._bzEnManager.detachEntity(entity);
    };

    //Obsolete
    DataService.prototype.get = function (entityResource) {
        var query = this._breeze.EntityQuery
            .from(entityResource);
        //.orderBy("CreatedAt");

        var queryFailed = function (error) {
            this._logger.error(error.message, "Query failed");
            return this._$q.reject(error); // so downstream promise users know it failed
        }.bind(this);

        var promise = this._bzEnManager.executeQuery(query).catch(queryFailed);
        return promise;
    };

    DataService.prototype.createQuery = function (entityResource) {
        return this._breeze.EntityQuery.from(entityResource);
    };

    DataService.prototype.executeQuery = function (query) {
        var queryFailed = function (error) {
            this._logger.error(error.message, "Query failed");
            return this._$q.reject(error); // so downstream promise users know it failed
        }.bind(this);

        return this._bzEnManager.executeQuery(query).catch(queryFailed);
    };

    DataService.prototype.hasChanges = function () {
        return this._bzEnManager.hasChanges();
    };

    DataService.prototype.handleSaveValidationError = function (error) {
        var message = "Not saved due to validation error";
        try { // fish out the first error
            var firstErr = error.entityErrors[0];
            message += ": " + firstErr.errorMessage;
        } catch (e) { /* eat it for now */ }
        return message;
    };

    DataService.prototype.removePropertyChangeHandler = function (handler) {
        // Actually removes any 'entityChanged' event handler
        return this._bzEnManager.entityChanged.unsubscribe(handler);
    };

    DataService.prototype.saveChanges = function () {
        var svcContext = this;
        var saveSucceeded = function (saveResult) {
            svcContext._logger.success("# of items saved = " + saveResult.entities.length);
            svcContext._logger.log(saveResult);
        };

        var saveFailed = function (error) {
            var reason = error.message;
            var detail = error.detail;

            if (error.entityErrors) {
                reason = svcContext.handleSaveValidationError(error);
            } else if (detail && detail.ExceptionType &&
                detail.ExceptionType.indexOf('OptimisticConcurrencyException') !== -1) {
                // Concurrency error 
                reason =
                    "Another user, perhaps the server, " +
                    "may have deleted one or all of the models." +
                    " You may have to restart the app.";
            } else {
                reason = "Failed to save changes: " + reason +
                    " You may have to restart the app.";
            }

            svcContext._logger.error(error, reason);
            // DEMO ONLY: discard all pending changes
            // Let them see the error for a second before rejecting changes
            //$timeout(function () {
            //    this._bzEnManager.rejectChanges();
            //}, 1000);
            return svcContext._$q.reject(error); // so downstream promise users know it failed
        };

        return this._bzEnManager.saveChanges()
            .then(saveSucceeded)
            .catch(saveFailed);
    };

    angular.module(Const.AppModule).factory(Const.BreezeDataService, ['$q', 'breeze', Const.BreezeEnManager, Const.LoggerService, function ($q, breeze, bzEnManager, logger) {
        return new DataService($q, breeze, bzEnManager, logger);
    }]);

}(Const, window);