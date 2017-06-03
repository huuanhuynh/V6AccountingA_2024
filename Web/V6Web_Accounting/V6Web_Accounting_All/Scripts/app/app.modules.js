angular.module('app', [
	//'app.components',
	'app.config',
    //'app.directives',
	//'app.factories',
	//'app.filters',
    //'app.run',
	//'app.routes',
	//'app.services',
	//'app.partials',
	
	// Theme core modules
	'ui.router',
    'ui.utils',
	'ngSanitize',
	'ui.select',
	'ngCkeditor',
	//'720kb.datepicker',
	'lr.upload',
	'purplefox.numeric',
	'ngTagsInput',
	'ngCookies'
]);

angular.module('adminApp.run', []);
angular.module('adminApp.routes', []);
angular.module('adminApp.filters', []);
angular.module('adminApp.services', []);
angular.module('adminApp.factories', []);
angular.module('adminApp.config', []);
angular.module('adminApp.directives', []);
angular.module('adminApp.controllers', []);
angular.module('adminApp.components', [
	'ui.router', 'ngMaterial', 'angular-loading-bar',
	'restangular', 'ngStorage', 'satellizer'
]);

