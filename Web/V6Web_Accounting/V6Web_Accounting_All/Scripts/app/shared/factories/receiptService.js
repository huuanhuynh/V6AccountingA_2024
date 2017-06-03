!function (Const, window) {
    'use strict';

    function dataservice($http, $q, $timeout, breeze, bzEnManager, logger) {
        
        var addPropertyChangeHandler = function (handler) {
            // Actually adds any 'entityChanged' event handler
            // call handler when an entity property of any entity changes
            return bzEnManager.entityChanged.subscribe(function (changeArgs) {
                var action = changeArgs.entityAction;
                if (action === breeze.EntityAction.PropertyChange) {
                    handler(changeArgs);
                }
            });
        };

        var create = function (initialValues) {
            return bzEnManager.createEntity('Alkh', initialValues);
        };

        var remove = function (customerModel) {
            if (customerModel) {
                var aspect = customerModel.entityAspect;
                if (aspect.isBeingSaved && aspect.entityState.isAdded()) {
                    // wait to delete added entity while it is being saved  
                    setTimeout(function () { remove(customerModel); }, 100);
                    return;
                }
                aspect.setDeleted();
                saveChanges();
            }
        };

        var get = function () {
            var query = breeze.EntityQuery
                .from("customers");
            //.orderBy("CreatedAt");

            var queryFailed = function (error) {
                logger.error(error.message, "Query failed");
                return $q.reject(error); // so downstream promise users know it failed
            }

            var promise = bzEnManager.executeQuery(query).catch(queryFailed);
            return promise;
        };

        var hasChanges = function () {
            return bzEnManager.hasChanges();
        };

        var handleSaveValidationError = function (error) {
            var message = "Not saved due to validation error";
            try { // fish out the first error
                var firstErr = error.entityErrors[0];
                message += ": " + firstErr.errorMessage;
            } catch (e) { /* eat it for now */ }
            return message;
        };

        var removePropertyChangeHandler = function (handler) {
            // Actually removes any 'entityChanged' event handler
            return bzEnManager.entityChanged.unsubscribe(handler);
        };

        var saveChanges = function () {
            var saveSucceeded = function (saveResult) {
                logger.success("# of items saved = " + saveResult.entities.length);
                logger.log(saveResult);
            };

            var saveFailed = function (error) {
                var reason = error.message;
                var detail = error.detail;

                if (error.entityErrors) {
                    reason = handleSaveValidationError(error);
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

                logger.error(error, reason);

                return $q.reject(error); // so downstream promise users know it failed
            };

            return bzEnManager.saveChanges()
                .then(saveSucceeded)
                .catch(saveFailed);
        };

        return {
            addPropertyChangeHandler: addPropertyChangeHandler,
            removePropertyChangeHandler: removePropertyChangeHandler,
            create: create,
            remove: remove,
            get: get,
            hasChanges: hasChanges,
            saveChanges: saveChanges
        };

    };

    angular.module(Const.AppModule).factory(Const.BreezeDataService, ['$http', '$q', '$timeout', 'breeze', Const.BreezeEnManager, Const.LoggerService, dataservice]);

}(Const, window);