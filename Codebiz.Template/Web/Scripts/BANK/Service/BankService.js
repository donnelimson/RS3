(function () {

    let bankService = function ($rootScope, $http, $q, $filter) {
        this.GetBankByID = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/BP/BusinessPartner/GetList',
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        }
    }


    let houseBankService = function ($rootScope, $http, $q, $filter) {
        this.GetHouseBankByID = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + `api/FIN/HouseBankAccount/GetHouseBankAccountDetails/${q}`,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        }
    }


    angular.module('BANK.SETUP.SERVICE', [])
        .service('HouseBankService', houseBankService)
        .service('BankService', houseBankService)

}())