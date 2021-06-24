(function () {

    var CompanySetupService = function ($rootScope, $http, $q, $filter) {

        this.GetCompany = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/ADM/Setup/CompanySetup/GetCompany',
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };


        this.GetList = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/ADM/Setup/CompanySetup/GetList',
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.Add = function (q) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + 'api/ADM/Setup/CompanySetup/Add',
                data: q,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.SaveOrUpdate = function (q) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + 'api/ADM/Setup/CompanySetup/SaveOrUpdate',
                data: q,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };
    };

    angular.module('ADM.SETUP.SERVICE',[])
        .service('CompanySetupService', ['$rootScope', '$http', '$q', '$filter', CompanySetupService]);
}());


