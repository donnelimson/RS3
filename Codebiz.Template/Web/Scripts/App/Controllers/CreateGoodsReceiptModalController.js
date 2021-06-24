MetronicApp.controller('CreateGoodsReceiptModalController', ['$scope', '$uibModalInstance', 'GoodsReceiptService', 'NgTableParams',
    function ($scope, $uibModalInstance, GoodsReceiptService, NgTableParams) {
        $scope.mapSuppliers;
        function init() {
            GoodsReceiptService.GetAllSuppliers()
                .then(function (data) {
          
                    $scope.mapSuppliers = data.data;
                },
                function (error /*Error event should handle here*/) {
                    console.log("Error");
                },
                function (data /*Notify event should handle here*/) {
                });
        }
        init();

        $scope.getSupplier = function () {
            $uibModalInstance.close($scope.mapItems);
        };


        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    }]);