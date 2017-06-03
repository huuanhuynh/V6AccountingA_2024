/// <reference path="../mod/resource/Scripts/app/1_Constants.js" />
/// <reference path="../mod/resource/Scripts/lib/angularjs/1angular.js" />
/// <reference path="global/Constants.js" />

!function () {
    'use strict';

    var Namespaces = window.V6Names.Namespaces;

    // Sets up Underscore template engine
    _.templateSettings = {
        interpolate: /\{(.+?)\}/g
    };

    angular.module(Namespaces.AccountingApp, []);

}();