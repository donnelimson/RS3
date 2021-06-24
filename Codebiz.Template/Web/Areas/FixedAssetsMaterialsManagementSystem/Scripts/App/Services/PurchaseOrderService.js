
angular.module("MetronicApp").factory('PurchaseOrderService', ['$rootScope', '$http', '$q', '$filter',
    function ($rootScope, $http, $q, $filter) {
        return {

            GetAllPurchaseOrder: function (data) {
                var d = $q.defer();
                $http({
                    method: 'GET',
                    url: document.baseUrl + 'PurchaseOrder/Search',
                    data: data
                })
                    .then(function successCallback(response) {
                        d.resolve(response.data);
                    }, function errorCallback(error) {
                        d.reject(error);
                    });

                return d.promise;
            },

            SearchPurchaseOrder: function (data) {
                var d = $q.defer();
                $http({
                    method: 'POST',
                    url: document.baseUrl + 'PurchaseOrder/Search',
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