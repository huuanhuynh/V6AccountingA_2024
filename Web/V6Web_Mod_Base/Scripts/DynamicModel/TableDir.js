
!function (V6Util, V6Names) {
    'use strict';

    var Namespaces = V6Names.Namespaces,
        DynamicModelSvc = V6Names.Services.DynamicModel,
        namespace, DynamicTableDirective;

    DynamicTableDirective = function (dynamicModelSvc) {
        this._dynamicModelSvc = dynamicModelSvc;
        this._routeTemplate = _.template(V6Server.RoutePatterns.SubMenuPage);
    };

    // Angular event which is invoked before data-binding
    DynamicTableDirective.prototype.compile = function (menuTreeElem, attrs) {
        return {
            pre: function () { },
            post: this.postLink.bind(this)
            // We need to bind `this` to postLink to make it work properly
        }
    };

    // Angular event which is invoked after data-binding
    DynamicTableDirective.prototype.postLink = function (scope, menuTreeElem) {

    };
    

    namespace = V6Util.resolveNamespace(Namespaces.Base);
    namespace.directive(V6Names.Directives.DynamicTable,
        [DynamicModelSvc, function (dynamicModelSvc) {
            return new DynamicTableDirective(dynamicModelSvc);
        }
    ]);

}(window.V6Util, window.V6Names);