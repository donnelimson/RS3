
angular.module("MetronicApp").factory('AccountsPayableService', ['$rootScope', '$http', '$q', '$filter',
    function ($rootScope, $http, $q, $filter) {
        return {

            EditDraft: function (data) {
                var d = $q.defer();
                $http({
                    method: 'POST',
                    url: document.baseUrl + 'Ordering/EditDraft',
                    data: data
                })

                    .then(function successCallback(response) {
                        d.resolve(response.data);
                    }, function errorCallback(error) {
                        d.reject(error);
                    });

                return d.promise
            },


            SearchAccountsPayable: function (data) {
                var d = $q.defer();
                $http({
                    method: 'POST',
                    url: document.baseUrl + 'AccountsPayable/Search',
                    data: data
                })

                    .then(function successCallback(response) {
                        d.resolve(response.data);
                    }, function errorCallback(error) {
                        d.reject(error);
                    });

                return d.promise
            },

            InitTable: function (data) {
                var d = $q.defer();
                $http({
                    method: 'GET',
                    url: document.baseUrl + 'AccountsPayable/Search',
                    data: data
                })

                    .then(function successCallback(response) {
                        d.resolve(response.data);
                    }, function errorCallback(error) {
                        d.reject(error);
                    });

                return d.promise
            },


            ImportGoodsReceipt: function (data) {
                var d = $q.defer();
                $http({
                    method: 'POST',
                    url: document.baseUrl + 'AccountsPayable/ImportFromGoodsReceipt',
                    data: data
                })

                    .then(function successCallback(response) {
                        d.resolve(response.data);
                    }, function errorCallback(error) {
                        d.reject(error);
                    });

                return d.promise
            },


        }
    }]);