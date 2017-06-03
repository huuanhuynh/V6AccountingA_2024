/// <reference path="app/global/1_Constants.js" />

$(function () {
    'use strict';

    var Namespaces = window.V6Names.Namespaces;

    // Sets up PJAX
    //$(document).pjax('a[data-pjax]', '#pjax-container');

    angular.bootstrap(document, [Namespaces.AccountingApp, Namespaces.Base]);
    //TO DO: Should consider boostrapping module Menu only, for performance purpose.
});