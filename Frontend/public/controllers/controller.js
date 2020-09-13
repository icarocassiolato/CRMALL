var myApp = angular.module('myApp', []);

myApp.controller('AppCtrl', ['$scope', '$http', function($scope, $http) {

const serverHost = 'http://localhost:49306/api';

function formatarData(dataParametro){
    var data = new Date(dataParametro),
        dia  = data.getDate().toString(),
        diaF = (dia.length == 1) ? '0'+dia : dia,
        mes  = (data.getMonth()+1).toString(),
        mesF = (mes.length == 1) ? '0'+mes : mes,
        anoF = data.getFullYear();
    return anoF+"-"+mesF+"-"+diaF;
}

var refresn = function () {
    $http.get(serverHost + '/cliente').success(function (response) {
        $scope.contactlist = response;
        $scope.contact = "";
    });
};
refresn();

$scope.addContact = function (nome, nascimento, sexo, cep, endereco, numero, complemento, bairro, cidade, estado) {         
    var valores = '?nome=' + nome + '&' +
      'nascimento=' + formatarData(nascimento) + '&' +
      'sexo=' + sexo + '&' +
      'cep=' + cep + '&' +
      'endereco=' + endereco + '&' +
      'numero=' + numero + '&' +
      'complemento=' + complemento + '&' +
      'bairro=' + bairro + '&' +
      'cidade=' + cidade + '&' +
      'estado=' + estado;

      $http.post(serverHost + '/cliente' + valores, $scope.contact).success(function (response) {
        refresn();
        $.smkAlert({ text: "Adicionado com sucesso!", type:'success', position:'bottom-right'});
    });
};

$scope.remove = function(id) {
    $.smkConfirm({
        text:'tem certeza que deseja deletar o registro?',
        accept:'Sim',
        cancel:'Não'
    },function(res) {
        if(res) {
            $http.delete(serverHost + '/cliente?ID=' + id).success(function (response) {
                refresn();
            });
        }
    });
};

$scope.edit = function (id) {
    $http.get(serverHost + '/cliente?ID=' + id).success(function (response) {
        $scope.contact = response[0];
    });
};

$scope.update = function (nome, nascimento, sexo, cep, endereco, numero, complemento, bairro, cidade, estado) {
    var valores = '&nome=' + nome + '&' +
      'nome=' + nome + '&' + 
      'nascimento=' + formatarData(nascimento) + '&' +
      'sexo=' + sexo + '&' +
      'cep=' + cep + '&' +
      'endereco=' + endereco + '&' +
      'numero=' + numero + '&' +
      'complemento=' + complemento + '&' +
      'bairro=' + bairro + '&' +
      'cidade=' + cidade + '&' +
      'estado=' + estado;

    $http.put(serverHost + '/cliente?ID='+ $scope.contact.id + valores, $scope.contact).success(function (response) {
       refresn();
        $.smkAlert({ text: "Editado com sucesso!", type:'warning', position:'bottom-right'});
    });
};

$scope.deselect = function () {
    $scope.contact = "";
};

$scope.searchID = function (chave) {
    $http.get(serverHost + '/cliente?ID=' + chave).success(function (response) {
        $scope.contactlist = response;
        $scope.contact = "";
    });
};

$scope.pesquisaCEP = function (value) {    
    var request = new XMLHttpRequest();
    request.open('get', 'https://viacep.com.br/ws/' + value + '/json/', true);
    request.send();
    request.onload = function () {
        var data = JSON.parse(this.response);
        $scope.contact.endereco = data.logradouro;
        $scope.contact.bairro = data.bairro;
        $scope.contact.cidade = data.localidade;
        $scope.contact.estado = data.uf;
    }
};

}]);
