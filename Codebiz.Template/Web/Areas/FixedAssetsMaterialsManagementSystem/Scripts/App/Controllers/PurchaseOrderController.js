MetronicApp.controller('PurchaseOrderController', ['$scope', '$uibModal', 'NgTableParams', '$q', 'PurchaseOrderService',
    function ($scope, $uibModal, NgTableParams, $q,PurchaseOrderService) {
        $scope.tableParamsPurchaseOrder = new NgTableParams({}, { dataset: [] });
        $scope.dataToInsert = [];

        $scope.search = function () {
            var initialSettings = {
                getData: function (params) {

                    for (var i in params.sorting()) {
                        $scope.sortColumn = i;
                        $scope.sortOrder = params.sorting()[i];
                    }
                    var d = $q.defer();
                    PurchaseOrderService.SearchPurchaseOrder({
                        page: params.page(),
                        pageSize: params.count(),
                        sortOrder: $scope.sortOrder,
                        sortColumn: $scope.sortColumn,
                        purchaseOrderCode: $scope.purchaseOrderCode,
                        status: $scope.status,
                        supplier: $scope.supplier                  
                    }).then(function (data) {
                  
                        params.total(data.totalRecordCount);
                        d.resolve(data.data);
                    });
                    return d.promise;
                }


            };
            $scope.tableParamsPurchaseOrder = new NgTableParams(10, initialSettings);
        }
        $scope.search();

        $scope.reset = function () {
            $scope.purchaseOrderCode = "";
            $scope.status = "";
            $scope.supplier = "";
            $scope.search();
        }

        $scope.printPurchaseOrder = function (purchaseOrderCode, supplierId, orderId) {
            $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: 'PurchaseOrderViewerModal.html',
                controller: 'ItemDetailsController',
                controllerAs: 'controller',
                size: 'lg',
                keyboard: true,
                backdrop: "static",
                windowClass: 'app-modal-window',
                resolve: {
                    PurchaseOrderCode: function () {
                        return purchaseOrderCode;
                    },
                    SupplierId: function () {
                        return supplierId;
                    },
                    OrderId: function () {
                        return orderId;
                    },
                    Items: function () {
                        return $scope.dataToInsert;
                    }
                }

            }).result.then(function (data) {

            }, function () {

            });

        }
     

    }]);