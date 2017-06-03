!function (Const, Entity, window) {
    'use strict';

    var emFactory;

    emFactory = function (breeze, $rootScope) {
        // Convert properties between server-side PascalCase and client-side camelCase
        //breeze.NamingConvention.camelCase.setAsDefault();
        breeze.NamingConvention.none.setAsDefault();

        // Identify the endpoint for the remote data service
        //var serviceRoot = window.location.protocol + '//' + window.location.host + '/';
        //var serviceName = serviceRoot + 'breeze/breeze'; // breeze Web API controller
        var serviceUrl = '/services/api/breeze',
            manager = new breeze.EntityManager(serviceUrl);
        manager.serviceUrl = serviceUrl;

        breeze.Validator.required = function (context) {
            var valFn = function(v, ctx) {
                return v != null;
            };
            return new breeze.Validator("required", valFn, context);
        };

        // register the new validator so that metadata can find it. 
        breeze.Validator.registerFactory(breeze.Validator.required, "required");

        manager.fetchMetadata()
            .then(function () {
                angular.forEach(Entity, function (value, key) {
                    // Entity name is as well resource name
                    manager.metadataStore.setEntityTypeForResourceName(value, value);
                });
                $rootScope.metadataFetched = true;
                $rootScope.$emit('metadataFetched');
            })
            .catch(function (error) {
                console.error(error);
            });
        // the "factory" services exposes two members
        return manager;
    };

    angular.module(Const.AppModule).factory(Const.BreezeEnManager, ['breeze', '$rootScope', emFactory]);

}(Const, Entity, window);