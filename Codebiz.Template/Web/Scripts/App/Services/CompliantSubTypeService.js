angular.module("MetronicApp").factory('CompliantSubTypeService', ['$rootScope', '$http', '$q', '$filter',
    function ($rootScope, $http, $q, $filter) {
        return {

            CompliantSubTypeIndex: function (params) {
                var d = $q.defer();
                $http({
                    method: 'POST',
                    url: document.baseUrl + 'CompliantType/CompliantSubTypeIndex',
                    params: params
                })
           .then(function successCallback(response) {
               d.resolve(response.data);
           }, function errorCallback(error) {
               d.reject(error);
           });

                return d.promise;
            },

            AddOrUpdate: function (data) {
                var d = $q.defer();
                $http({
                    method: 'POST',
                    url: document.baseUrl + 'CompliantSubType/AddOrUpdate',
                    data: data
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