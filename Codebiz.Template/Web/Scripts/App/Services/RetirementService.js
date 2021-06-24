
angular.module("MetronicApp").factory('RetirementService', ['$rootScope', '$http', '$q', '$filter', function ($rootScope, $http, $q, $filter) {
    return {
        GetRetirements: function (params) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrl + 'Order/GetRetirements',
                params: params
            })
            .then(function successCallback(response) {
                d.resolve(response.data);
            }, function errorCallback(error) {
                d.reject(error);
            });

            return d.promise;
        },

        GetRetirementById: function (params) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrl + 'Order/GetById',
                params: params
            })
            .then(function successCallback(response) {
                d.resolve(response.data);
            }, function errorCallback(error) {
                d.reject(error);
            });

            return d.promise;
        },

        AddOrUpdateRetirementOrder: function (data) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrl + 'Order/AddOrUpdateRetirement',
                data: data
            })
            .then(function successCallback(response) {
                d.resolve(response.data);
            }, function errorCallback(error) {
                d.reject(error);
            });

            return d.promise;
        },

        GetRetirementOrderDetails: function (params) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrl + 'Order/GetOrderDetails',
                params: params
            })
            .then(function successCallback(response) {

                _.forEach(response.data.data, function (value) {
                    if (value.RetirementDate !== null) { value.RetirementDate = new Date(value.RetirementDate) };
                    if (value.RetirementDate !== null) { value.RetirementDateFormatted = $filter('date')(new Date(value.RetirementDate), "MM/dd/yyyy") };
                    if (value.AuthorityDate !== null) { value.AuthorityDate = new Date(value.AuthorityDate) };
                    if (value.AuthorityDate !== null) { value.AuthorityDateFormatted = $filter('date')(new Date(value.AuthorityDate), "MM/dd/yyyy") };
                    if (value.EffectivityDate !== null) { value.EffectivityDate = new Date(value.EffectivityDate) };
                    if (value.EffectivityDate !== null) { value.EffectivityDateFormatted = $filter('date')(new Date(value.EffectivityDate), "MM/dd/yyyy") };
                });

                d.resolve(response.data);
            }, function errorCallback(error) {
                d.reject(error);
            });

            return d.promise;
        },

        GetRetirementTypeLookup: function (params) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrl + 'Order/GetRetirementTypesLookup',
                params: params
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });

            return d.promise;
        },

        CancelRetirementOrder: function (data) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrl + 'Order/CancelRetirement',
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