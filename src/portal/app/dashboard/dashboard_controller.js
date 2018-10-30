'use strict';

angular.module('eudora.dashboard', [])

.controller('dashboardCtrl', function($scope, $routeParams, $http, $mdDialog) {

    var idRevend = $routeParams.id;
    $scope.isLoading = true;
    $scope.selected = [];
    $scope.revendedora = "Maria Joaquim";

    $scope.makeChart = function(cat) {

        var categorias = cat.map(a => a.categoria);
        var values = cat.map(a => a.valorTotal);
        var option = {
            responsive: false,
            title: {
                display: true,
                text: 'Compras por categoria',
                fontSize: 16
            },
            legend:{
                position: 'bottom'
            }
        };

        var ctx = document.getElementById("myChart");
        var myChart = new Chart(ctx, {
            type: 'doughnut',
            options: option,
            data: {
                labels: categorias,
                datasets: [{
                    data: values,
                    backgroundColor: ['#40004b','#762a83','#9970ab','#c2a5cf','#e7d4e8','#d9f0d3','#a6dba0','#5aae61','#1b7837','#00441b'],
                    borderWidth: 1
                }]
            }
        });
    }

    $http.get('https://eudora-09-api.azurewebsites.net/api/representante/dashboard/'+idRevend)
         .then(function (response) {
            var data = response.data
            $scope.revendedora = data.representante;
            $scope.ultimasCompras = data.ultimasCompras;
            $scope.recomendacoes = data.recomendacoes;

            //format date to human readable
            var a = $scope.ultimasCompras.forEach(
                purchase => purchase.formattedDate = new Date(purchase.dat_Captacao)
            );

            $scope.starred = true;
            var a = $scope.recomendacoes.forEach(function (r){
                r.isChecked = false;

                r.starred = $scope.starred;
                if($scope.starred) {
                    $scope.starred = false;
                }
            });

            $scope.makeFakeCiclosChart();
            $scope.makeChart(data.categorias);

         }).catch(function(error){
            console.log(error);
         }).finally(function(){
            $scope.isLoading = false;
         });


    $scope.onChange = function(r) {
        if(r.isChecked) {
            $scope.selected.push(r);
        } else {
            var index = $scope.selected.indexOf(r);
            if (index !== -1) $scope.selected.splice(index, 1);
        }
    }

    $scope.send = function(r) {
        var str = encodeURI(r.descricao+" - R$ "+r.valor);

        $http.get("https://eudora-functionapp.eudora-api.p.azurewebsites.net/api/MessageSend?data=*" +
            str + "*&tel="+$scope.revendedora.telefone+"&code=TkY2y6h/noODxXWaqxipNtQErtGxYFfWhAGAigx2x5japNa1Vgj/OA==")
            .then(function(){

            }).catch(function(error){
                console.log(error)
            });

        //send the image
        $http.get("https://eudora-functionapp.eudora-api.p.azurewebsites.net/api/MessageSend?data=" +
             r.imagemProduto + "&tel="+$scope.revendedora.telefone+"&code=TkY2y6h/noODxXWaqxipNtQErtGxYFfWhAGAigx2x5japNa1Vgj/OA==")
            .then(function(){

            }).catch(function(error){
                console.log(error)
            });
    }

    $scope.sendAllRecom = function() {
        $scope.selected.forEach(function(r) {
             $scope.send(r);
        });
        $mdDialog.show(
              $mdDialog.alert()
                .parent(angular.element(document.querySelector('#popupContainer')))
                .clickOutsideToClose(true)
                .title('Enviar recomendações')
                .textContent('Recomendações enviadas com sucesso.')
                .ariaLabel('Alert Dialog Demo')
                .ok('Ok')
//                .targetEvent(ev)
            );
    }

    $scope.showConfirm = function() {
        if ($scope.selected.length > 0) {
            var confirm = $mdDialog.confirm()
                .title('Enviar recomendações')
                .textContent('Gostaria de recomendar '+$scope.selected.length+' item(s) à revendedora '+$scope.revendedora.nome+'?')
//                .targetEvent(ev)
                .ok('Enviar')
                .cancel('Cancelar');

            $mdDialog.show(confirm).then(function() {
              $scope.sendAllRecom();
            }, function() {
              $scope.status = 'You decided to keep your debt.';
            });
        } else {
            $mdDialog.show(
                  $mdDialog.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title('Enviar recomendações')
                    .textContent('Selecione pelo menos um produto para indicar.')
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Ok')
//                    .targetEvent(ev)
                );
        }
    };


    $scope.makeFakeCiclosChart = function() {
        var numCiclos = ['Ciclo 8', 'Ciclo 9', 'Ciclo 10', 'Ciclo 11', 'Ciclo 12', 'Ciclo 13'];
        var valueCiclos = [500, 1400, 1600, 1800, 1000, 350];
        var option = {
            responsive: false,
            title: {
                display: true,
                text: 'Últimos 6 Ciclos',
                fontSize: 16
            },
            legend:{
                display:false
            }
        };

        var ctx = document.getElementById("myCicleChart");
        var myCicleChart = new Chart(ctx, {
            type: 'bar',
            options: option,
            data: {
                labels: numCiclos,
                datasets: [{
                    data: valueCiclos,
                    backgroundColor: ['#40004b','#762a83','#9970ab','#c2a5cf','#e7d4e8','#d9f0d3',
                                      '#a6dba0','#5aae61','#1b7837','#00441b']
                }]
            }
        });
    }

});