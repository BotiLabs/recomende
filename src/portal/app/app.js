'use strict';

// Declare app level module which depends on views, and components
angular.module('eudora', [
  'ngRoute',
  'ngMaterial',
  'eudora.list',
  'eudora.dashboard',
  'angularCharts',
  'ngMdIcons'

]).
config(['$locationProvider', '$routeProvider','$mdThemingProvider',
    function($locationProvider, $routeProvider,$mdThemingProvider) {
        $locationProvider.hashPrefix('');

        $routeProvider.
            when('/list', {
                templateUrl: 'list/list.html',
                controller: 'listCtrl'
            }).
            when('/dashboard/:id', {
                templateUrl: 'dashboard/dashboard.html',
                controller: 'dashboardCtrl'
            }).
            otherwise({
            redirectTo: '/list'
            });

//            $mdThemingProvider.theme('default').dark();

}]);
