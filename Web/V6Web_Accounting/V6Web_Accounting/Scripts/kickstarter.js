/// <reference path="../mod/resource/Scripts/app/1_Constants.js" />
/// <reference path="../mod/resource/Scripts/lib/angularjs/1angular.js" />
/// <reference path="global/Constants.js" />

$(function () {
    'use strict';

    var Namespaces = window.V6Names.Namespaces;

    // Sets up PJAX
    //$(document).pjax('a[data-pjax]', '#pjax-container');

    angular.bootstrap(document, [Namespaces.AccountingApp]);
    //TO DO: Should consider boostrapping module Menu only, for performance purpose.
});