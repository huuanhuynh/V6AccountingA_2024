export function RoutesConfig($stateProvider, $urlRouterProvider) {
	'ngInject';

	let getView = (viewName) => {
		return `./views/app/pages/${viewName}/${viewName}.page.html`;
	};

	$urlRouterProvider.otherwise('/dashboard');

	$stateProvider
		.state('app', {
			abstract: true,
            data: {
				auth: true
			},//{auth: true} would require JWT auth
			views: {
				header: {
					templateUrl: getView('header')
				},
				footer: {
					templateUrl: getView('footer')
				},
				main: {}
			}
		})
		.state('app.landing', {
            url: '/',
			data: {
				auth: false
			},
            views: {
                'main@': {
                    templateUrl: getView('landing')
                }
            }
        })
		
        .state('app.front_dashboard', {
            url: '/dashboard',
            views: {
                'main@': {
                    templateUrl: getView('front_dashboard')
                }
            }
        })
        .state('app.front_dash_play_games', {
            url: '/dashboard/play-games',
            views: {
                'main@': {
                    templateUrl: getView('front_dash_play_games')
                }
            }
        })
        .state('app.front_dash_sweepstakes', {
            url: '/dashboard/sweepstakes',
            views: {
                'main@': {
                    templateUrl: getView('front_dash_sweepstakes')
                }
            }
        })
        .state('app.front_dash_redeem_points', {
            url: '/dashboard/redeem-points',
            views: {
                'main@': {
                    templateUrl: getView('front_dash_redeem_points')
                }
            }
        })
        .state('app.front_dash_rewards', {
            url: '/dashboard/rewards',
            views: {
                'main@': {
                    templateUrl: getView('front_dash_rewards')
                }
            }
        })
        .state('app.front_dash_invite_friends', {
            url: '/dashboard/invite-friend',
            views: {
                'main@': {
                    templateUrl: getView('front_dash_invite_friends')
                }
            }
        })
        .state('app.front_change_password', {
            url: '/dashboard/update-password',
            views: {
                'main@': {
                    templateUrl: getView('front_change_password')
                }
            }
        })
        .state('app.front_dash_profile', {
            url: '/dashboard/update-profile',
            views: {
                'main@': {
                    templateUrl: getView('front_dash_profile')
                }
            }
        })
        .state('app.admin-login', {
            url: '/dashboard/login',
            views: {
                'main@': {
                    templateUrl: getView('admin-login')
                }
            }
        })
        .state('app.front_dash_earn_points', {
            url: '/dashboard/earn-points',
            views: {
                'main@': {
                    templateUrl: getView('front_dash_earn_points')
                }
            }
        });
}
