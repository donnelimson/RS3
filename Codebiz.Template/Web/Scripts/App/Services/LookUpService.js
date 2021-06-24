angular.module("MetronicApp").factory('LookUpService', ['$http', '$q',
    function ($http, $q) {
        return {
            SearchAppUserLookUp: function (args) {
                var d = $q.defer();
                $http({
                    method: 'GET',
                    url: document.AppUsers + "SearchAppUserLookUp",
                    params: args
                })
                    .then(function successCallback(response) {
                        d.resolve(response.data);
                    },
                        function errorCallback(error) {
                            d.reject(error);
                        });

                return d.promise;
            },


            SearchAppUsersModal: function (data) {
                var d = $q.defer();
                $http({
                    method: 'POST',
                    url: document.AppUsers + "SearchAppUserModal",
                    data: data
                })
                    .then(function successCallback(response) {
                        d.resolve(response.data);
                    },
                        function errorCallback(error) {
                            d.reject(error);
                        });

                return d.promise;
            },

            SearchAppUserByDivision: function (data) {
                var d = $q.defer();
                $http({
                    method: 'POST',
                    url: document.AppUsers + "SearchAppUserByDivision",
                    data: data
                })
                    .then(function successCallback(response) {
                        d.resolve(response.data);
                    },
                        function errorCallback(error) {
                            d.reject(error);
                        });

                return d.promise;
            },

            SearchAppUserByHeadOfficer: function (data) {
                var d = $q.defer();
                $http({
                    method: 'POST',
                    url: document.AppUsers + "SearchAppUserByHeadOfficer",
                    data: data
                })
                    .then(function successCallback(response) {
                        d.resolve(response.data);
                    },
                        function errorCallback(error) {
                            d.reject(error);
                        });

                return d.promise;
            },

            GetAllPositions: function (args) {
                var d = $q.defer();
                $http({
                    method: 'GET',
                    url: document.AppUsers + "GetAllPositions",
                    params: args
                })
                    .then(function successCallback(response) {
                        d.resolve(response.data);
                    },
                        function errorCallback(error) {
                            d.reject(error);
                        });

                return d.promise;
            },

            GetAllAreas: function (args) {
                var d = $q.defer();
                $http({
                    method: 'GET',
                    url: document.AppUsers + "GetAllAreas",
                    params: args
                })
                    .then(function successCallback(response) {
                        d.resolve(response.data);
                    },
                        function errorCallback(error) {
                            d.reject(error);
                        });

                return d.promise;
            },

            GetRouteLookUpByProvinceIdCityTownId: function (data) {
                var d = $q.defer();
                $http({
                    method: 'POST',
                    url: document.Route + "GetRouteLookUpByProvinceIdCityTownId",
                    data: data
                })
                    .then(function successCallback(response) {
                        d.resolve(response.data);
                    },
                        function errorCallback(error) {
                            d.reject(error);
                        });

                return d.promise;
            },

        }
    }]);