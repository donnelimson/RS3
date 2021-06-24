MetronicApp.controller('GoodsReceiptForwardController', ['$scope', 'GoodsReceiptCode','SupplierDescription','Items','GoodsReceiptId', '$uibModalInstance', 'NgTableParams','CommonService','AccountsPayableService',
    function ($scope, GoodsReceiptCode, SupplierDescription, Items, GoodsReceiptId, $uibModalInstance, NgTableParams, CommonService, AccountsPayableService) {
        $scope.dataToInsert = [];
        $scope.totalQuantity = 0;
        $scope.totalCost=0;

        function init() {
            $scope.dataToInsert.splice(0, 1);
            $scope.dataToInsert = Items;
            $scope.goodsReceiptCode = GoodsReceiptCode;

            for (var i = 0; i <= Items.length - 1; i++) {
                $scope.totalQuantity += Items[i].Quantity;
                $scope.totalCost += Items[i].TotalCost;
            }
        }
        init();

        $scope.sendToAccountsPayable = function () {
            $scope.totalQuantity = 0;
            $scope.totalCost = 0;
            for (var i = 0; i <= Items.length - 1; i++) {
                $scope.totalQuantity += Items[i].Quantity;
                  $scope.totalCost += Items[i].TotalCost;
            }


            AccountsPayableService.ImportGoodsReceipt({
                goodsReceiptId: GoodsReceiptId,
                totalQuantity:  $scope.totalQuantity,
                totalCost: $scope.totalCost,
                goodsReceiptCode: GoodsReceiptCode,
            }).then(function (data) {
                if (data.data == true) {
                    CommonService.warningMessage("G.R is already sent to A/P");
                } else {
                    CommonService.successMessage("G.R successfully sent to A/P");
                }
            })
        }
    }]);