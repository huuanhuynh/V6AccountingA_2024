!function (Const, window) {
    'use strict';

    var DataService = function (logger, $q, $http) {
        this._logger = logger;
        this._$q = $q;
        this._$http = $http;
    };

    DataService.prototype.get = function (url, data, config) {
        return this._request('GET', url, data, config);
    };

    DataService.prototype.post = function (url, data, config) {
        return this._request('POST', url, data, config);
    };

    DataService.prototype._request = function (method, url, data, config) {
        return this._$http({
            method: method,
            url: url,
            data: data,
            config: config
        });
    };

    angular.module(Const.AppModule).factory(Const.HttpDataService, [Const.LoggerService, '$q', '$http', function (logger, $q, $http) {
        return new DataService(logger, $q, $http);
    }]);

}(Const, window);