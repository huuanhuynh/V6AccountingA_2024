/// <reference path="../1_Constants.js" />
/// <reference path="../2_utilities.js" />

!function (V6Util, V6Names) {
    'use strict';

    var namespace, ViewSettingsService,
        Selector = "#page-settings";

    ViewSettingsService = function () { };

    ViewSettingsService.prototype.getSettings = function () {
        return $(Selector).data();
    };
    
    namespace = V6Util.resolveNamespace(V6Names.Namespaces.Resource);
    namespace.service(V6Names.Services.ViewSettings, function () {
        return new ViewSettingsService();
    });
}(window.V6Util, window.V6Names);