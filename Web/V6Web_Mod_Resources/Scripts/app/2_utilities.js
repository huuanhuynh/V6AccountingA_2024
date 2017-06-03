//
// Index:
// #Array
// #Angular
//

!function (window) {
    var V6Util = window.V6Util = window.V6Util || {}; // Creates new object if not existing.

    //
    // #Array
    //

    /// Extracts array instance in ASP.NET Web API response.
    /// @param `wrappedArray` [object] expected format:
    ///      { $id: 0, $values: [] }
    /// @return [Array] or [null]
    V6Util.unsecureArray = function (wrappedArray) {
        if (!wrappedArray) {
            return null;
        }

        if (wrappedArray.hasOwnProperty('$values')) {
            return wrappedArray['$values'];
        }
        return null;
    };

    //
    // #Angular
    //

    V6Util.resolveNamespace = function (namespace, dependencies) {
        // Caches to local variable to avoid repetitively looking up the whole scope.
        var angular = window.angular,
            resolvedNamespace;
        if (!angular) {
            throw 'Angular library has not been loaded!';
        }

        // Tries getting available namespace
        try {
            resolvedNamespace = angular.module(namespace);
        } catch (ex) {
            // Creates new namspace
            resolvedNamespace = angular.module(namespace, dependencies || []);
        }
        return resolvedNamespace;
    };
}(window);