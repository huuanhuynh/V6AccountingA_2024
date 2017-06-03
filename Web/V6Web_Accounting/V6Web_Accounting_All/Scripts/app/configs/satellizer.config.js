export function SatellizerConfig($authProvider) {
	'ngInject';

	$authProvider.httpInterceptor = function() {
		return true;
	}

	$authProvider.loginUrl = '/api/v1/login';
	$authProvider.signupUrl = '/api/v1/register';
	$authProvider.tokenRoot = 'data';//compensates success response macro

}
