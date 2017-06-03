!function (Const, window) {
    'use strict';

    var loggerService = function ($log) {
        return {
            error: function (message, title) {
                //toastr.error(message, title);
                $log.error("Error: " + message);
            },

            info: function (message, title) {
                //toastr.info(message, title);
                $log.info("Info: " + message);
            },

            log: function (message) {
                $log.log(message);
            },

            success: function (message, title) {
                //toastr.success(message, title);
                $log.info("Success: " + message);
            },

            warning: function (message, title) {
                //toastr.warning(message, title);
                $log.warn("Warning: " + message);
            }
        };
    };

    angular.module(Const.AppModule).factory(Const.LoggerService, ['$log', loggerService]);

}(Const, window);