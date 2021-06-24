
angular.module("MetronicApp").factory('OrderingService', ['$rootScope', '$http', '$q', '$filter',
    function ($rootScope, $http, $q, $filter) {
        return {

            EditDraft: function (data) {
                var d = $q.defer();
                $http({
                    method: 'POST',
                    url: document.Ordering + 'EditDraft',
                    data: data
                })

                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });

                return d.promise
            },


            GetMaxOrderId: function (data) {
                var d = $q.defer();
                $http({
                    method: 'POST',
                    url: document.Ordering + 'GetMaxOrderId',
                    data: data
                })

                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });

                return d.promise
            },


            GetOrderIdForShortCut: function (data) {
                var d = $q.defer();
                $http({
                    method: 'POST',
                    url: document.Ordering + 'GetOrderIdForShortCut',
                    data: data
                })

                    .then(function successCallback(response) {
                        d.resolve(response.data);
                    }, function errorCallback(error) {
                        d.reject(error);
                    });

                return d.promise
            },
       

            GetSuppliers: function (data) {
                var d = $q.defer();
                $http({
                    method: 'GET',
                    url: document.Ordering + 'GetSuppliers',
                    data: data
                })

                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });

                return d.promise;
            },

            GetSavedOrders: function (data) {
                var d = $q.defer();
                $http({
                    method: 'GET',
                    url: document.Ordering + 'GetSavedOrders',
                    data: data
                })

                    .then(function successCallback(response) {
                        d.resolve(response.data);
                    }, function errorCallback(error) {
                        d.reject(error);
                    });

                return d.promise;
            },
        

            GetItemList: function (params) {
                var d = $q.defer();
                $http({
                    method: 'GET',
                    url: document.Ordering + 'GetItemList',
                    params:params
                })

                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });

                return d.promise;
            },

            SearchProduct: function (params) {
                var d = $q.defer();
                $http({
                    method: 'GET',
                    url: document.Ordering + 'SearchProduct',
                    params: params
                })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });

                return d.promise;
            },
          
            GetItemCost: function (params) {
                var d = $q.defer();
                $http({
                    method: 'GET',
                    url: document.Ordering + 'GetItemCost',
                    params: params
                })

                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });

                return d.promise;
            },

            SaveOrder: function (data) {
                var d = $q.defer();
                $http({
                    method: 'POST',
                    url: document.Ordering + 'SaveOrder',
                    data: data
                })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });

                return d.promise;
            },

            CancelOrder: function (data) {
                var d = $q.defer();
                $http({
                    method: 'POST',
                    url: document.Ordering + 'CancelOrder',
                    data: data
                })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });

                return d.promise;
            },

            CompleteOrder: function (orderId) {
                var d = $q.defer();
                $http({
                    method: 'POST',
                    url: document.Ordering + 'CompleteOrder',
                    data: orderId
                })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });

                return d.promise;
            },

            InitTable: function (data) {
                var d = $q.defer();
                $http({
                    method: 'GET',
                    url: document.Ordering + 'Search',
                    data: data
                })
                    .then(function successCallback(response) {
                        d.resolve(response.data);
                    }, function errorCallback(error) {
                        d.reject(error);
                    });

                return d.promise;
            },

            SearchOrder: function (data) {
                var d = $q.defer();
                $http({
                    method: 'POST',
                    url: document.Ordering + 'Search',
                    data: data
                })
                    .then(function successCallback(response) {
                        d.resolve(response.data);
                    }, function errorCallback(error) {
                        d.reject(error);
                    });

                return d.promise;
            },
            GetAllItems: function (data) {
                var d = $q.defer();
                $http({
                    method: 'POST',
                    url: document.Ordering + 'GetAllMasterItem',
                    data: data
                })
                    .then(function successCallback(response) {
                        d.resolve(response.data);
                    }, function errorCallback(error) {
                        d.reject(error);
                    });

                return d.promise;
            },
            FilterBySupplier: function (data) {
                var d = $q.defer();
                $http({
                    method: 'POST',
                    url: document.Ordering + 'FilterBySupplier',
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