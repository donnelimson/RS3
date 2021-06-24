
angular.module("MetronicApp").factory('GoodsReceiptService', ['$rootScope', '$http', '$q', '$filter',
    function ($rootScope, $http, $q, $filter) {
        return {

 

            ImportToGoodsReceipt: function (data) {
                var d = $q.defer();
                $http({
                    method: 'GET',
                    url: document.baseUrl + 'GoodsReceipt/ExportGoodsReceipt',
                    data: data
                })
                    .then(function successCallback(response) {
                        d.resolve(response.data);
                    }, function errorCallback(error) {
                        d.reject(error);
                    });

                return d.promise;
            },

            SaveGoodsReceipt: function (data) {
                var d = $q.defer();
                $http({
                    method: 'POST',
                    url: '/GoodsReceipt/SaveGoodsReceipt',
                    data: data
                })
                    .then(function successCallback(response) {
                        d.resolve(response.data);
                    }, function errorCallback(error) {
                        d.reject(error);
                    });

                return d.promise;
            },

            CompleteGoodsReceipt: function (data) {
                var d = $q.defer();
                $http({
                    method: 'POST',
                    url: '/GoodsReceipt/CompleteGoodsReceipt',
                    data: data
                })
                    .then(function successCallback(response) {
                        d.resolve(response.data);
                    }, function errorCallback(error) {
                        d.reject(error);
                    });

                return d.promise;
            },



            SaveCreatedGoodsReceipt: function (data) {
                var d = $q.defer();
                $http({
                    method: 'POST',
                    url: document.baseUrl + 'GoodsReceipt/SaveGoodsReceipt',
                    data: data
                })
                    .then(function successCallback(response) {
                        d.resolve(response.data);
                    }, function errorCallback(error) {
                        d.reject(error);
                    });

                return d.promise;
            },

            
            GetGoodsRecieptIdForShortcutManual: function (data) {
                var d = $q.defer();
                $http({
                    method: 'POST',
                    url: document.baseUrl + 'GoodsReceipt/GetGoodsReceiptIdForShortcutManual',
                    data: data
                })
                    .then(function successCallback(response) {
                        d.resolve(response.data);
                    }, function errorCallback(error) {
                        d.reject(error);
                    });

                return d.promise;
            },
            GetGoodsRecieptIdForShortcutEdit: function (data) {
                var d = $q.defer();
                $http({
                    method: 'POST',
                    url: document.baseUrl + 'GoodsReceipt/GetGoodsReceiptIdForShortcutEdit',
                    data: data
                })
                    .then(function successCallback(response) {
                        d.resolve(response.data);
                    }, function errorCallback(error) {
                        d.reject(error);
                    });

                return d.promise;
            },
            GetPurchaseOrderIdForShortcut: function (data) {
                var d = $q.defer();
                $http({
                    method: 'POST',
                    url: document.baseUrl + 'GoodsReceipt/PurchaseOrderIdForShortcut',
                    data: data
                })
                    .then(function successCallback(response) {
                        d.resolve(response.data);
                    }, function errorCallback(error) {
                        d.reject(error);
                    });

                return d.promise;
            },

            EditGoodsReceipt: function (data) {
                var d = $q.defer();
                $http({
                    method: 'POST',
                    url: '/GoodsReceipt/UpdateGoodsReceipt',
                    data: data
                })
                    .then(function successCallback(response) {
                        d.resolve(response.data);
                    }, function errorCallback(error) {
                        d.reject(error);
                    });

                return d.promise;
            },
           
            TriggerGetSaveGoodsReceipt: function (data) {
                var d = $q.defer();
                $http({
                    method: 'POST',
                    url: document.baseUrl + 'GoodsReceipt/Edit',
                    data: data
                })

                    .then(function successCallback(response) {
                        d.resolve(response.data);
                    }, function errorCallback(error) {
                        d.reject(error);
                    });

                return d.promise;
            },

            GetSavedGoodsReceipt: function (data) {
                var d = $q.defer();
                $http({
                    method: 'GET',
                    url: document.baseUrl + 'GoodsReceipt/GetSavedGoodsReceipt',
                    data: data
                })

                    .then(function successCallback(response) {
                        d.resolve(response.data);
                    }, function errorCallback(error) {
                        d.reject(error);
                    });

                return d.promise;
            },


            GetAllSuppliers: function (data) {
                var d = $q.defer();
                $http({
                    method: 'GET',
                    url: document.baseUrl + 'GoodsReceipt/GetAllSuppliers',
                    data: data
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
                    url: document.baseUrl + 'GoodsReceipt/IndexView',
                    data: data
                })
                    .then(function successCallback(response) {
                        d.resolve(response.data);
                    }, function errorCallback(error) {
                        d.reject(error);
                    });

                return d.promise;
            },

            GoEditGoodsReceipt: function (data) {
                var d = $q.defer();
                $http({
                    method: 'GET',
                    url: document.baseUrl + 'GoodsReceipt/Edit',
                    data: data
                })
                    .then(function successCallback(response) {
                        d.resolve(response.data);
                    }, function errorCallback(error) {
                        d.reject(error);
                    });

                return d.promise;
            },
            SearchGoodsReceipt: function (data) {
                var d = $q.defer();
                $http({
                    method: 'POST',
                    url: document.baseUrl + 'GoodsReceipt/Search',
                    data: data
                })
                    .then(function successCallback(response) {
                        d.resolve(response.data);
                    }, function errorCallback(error) {
                        d.reject(error);
                    });

                return d.promise;
            },

            GetItemsForForward: function (data) {
                var d = $q.defer();
                $http({
                    method: 'POST',
                    url: '/GoodsReceipt/GetGoodsReceiptItemForForward',
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