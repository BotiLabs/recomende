'use strict';

angular.module('eudora.list', [])

.controller('listCtrl', function($scope, $http) {

    $scope.goToDashboard = function(idRevendedora) {
        window.location.href = "#/dashboard/" + idRevendedora;
    }

    $scope.showBtn = false;
    $scope.people = null;
    $scope.isLoading = true;

    $http.get('https://eudora-09-api.azurewebsites.net/api/representante')
         .then(function (response) {

            $scope.people = response.data;
         }).catch(function(error){
            console.log(error);
         }).finally(function(){
            $scope.isLoading = false;

         });

    $scope.showBtnSend = function() {
        $scope.showBtn = true;
    }




});