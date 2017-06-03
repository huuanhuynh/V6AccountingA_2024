/// <reference path="../1_Constants.js" />
/// <reference path="../2_utilities.js" />

!function (V6Util, V6Names) {
    'use strict';

    var namespace, UrlService,
        Prefix = "#url-container-";

    UrlService = function () {};
        
    UrlService.prototype.getApiUrls = function (moduleName) {
        return $(Prefix + moduleName).data();
    };


    namespace = V6Util.resolveNamespace(V6Names.Namespaces.Resource);
    namespace.service(V6Names.Services.Url, UrlService);
}(window.V6Util, window.V6Names);