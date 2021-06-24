
angular.module("MetronicApp").factory('PersonnelService', ['$rootScope', '$http', '$q', function ($rootScope, $http, $q) {
    return {
        FindPersonnels: function (params) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrl + 'Personnel/FindRespondents',
                params: params
            })
            .then(function successCallback(response) {
                d.resolve(response.data);
            }, function errorCallback(error) {
                d.reject(error);
            });

            return d.promise;
        },
        PersonnelPhoto: function (params) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrl + 'Personnel/PersonnelPhoto',
                params: params
            })
            .then(function successCallback(response) {
                d.resolve(response.data);
            }, function errorCallback(error) {
                d.reject(error);
            });

            return d.promise;
        },
        SearchPersonnelByAccountNumber: function (params) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrl + 'Personnel/SearchPersonnelByAccountNumber',
                params: params
            })
            .then(function successCallback(response) {
                d.resolve(response.data);
            }, function errorCallback(error) {
                d.reject(error);
            });

            return d.promise;
        },
        GetPersonnelDetailsByAccountNumberAndEndorsement: function (params) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrl + 'Personnel/GetPersonnelDetailsByAccountNumberAndEndorsement',
                params: params
            })
            .then(function successCallback(response) {
                d.resolve(response.data);
            }, function errorCallback(error) {
                d.reject(error);
            });

            return d.promise;
        },
        GetPersonnelEndorsementDetails: function (params) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrl + 'Personnel/GetPersonnelEndorsementDetails',
                params: params
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });

            return d.promise;
        },
        GetPersonnelSalaryMatrixBySalaryGradeAndSalaryStep: function (params) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrl + 'Personnel/GetPersonnelSalaryMatrixBySalaryGradeAndSalaryStep',
                params: params
            })
            .then(function successCallback(response) {
                d.resolve(response.data);
            }, function errorCallback(error) {
                d.reject(error);
            });

            return d.promise;
        },
    }
}]);