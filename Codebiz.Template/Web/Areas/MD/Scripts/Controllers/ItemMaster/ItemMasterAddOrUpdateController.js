MetronicApp.controller('ItemMasterAddOrUpdateController', ['$scope', '$q', 'NgTableParams', 'CommonService', '$uibModal', '$timeout', '$location', '$window', 'ItemMasterService', '$controller','PriceListService','BrandService',
    function ($scope, $q, NgTableParams, CommonService, $uibModal, $timeout, $location, $window, ItemMasterService, $controller, PriceListService, BrandService) {
        $scope.m = {
            Id: $location.search().id ?? 0,
            LongDescription: "",
            ShortDescription: "",
            ItemCode: "",
            BasePrice: (0).toFixed(2),
            Brands: [],
            PriceLists: [],
            InventoryItems: []
        };

        $scope.b = {
            sortOrder: null,
            sortColumn:null
        }
        $scope.p = {
            sortOrder: null,
            sortColumn: null
        }
        let formatter = Intl.NumberFormat('en-US', { minimumFractionDigits: 2 });
        this.$onInit = function () {
            ItemMasterService.GetDetailsById({ id: $scope.m.Id }).then(function (d) {
                if (d.result != null) {
                    $scope.m = d.result;
                    $scope.isUpdate = true;
                    $scope.m.BasePrice = formatter.format($scope.m.BasePrice);
                }

                loadPriceListData();
                loadBrandData();
            });
            CommonService.GetAllUoM().then(function (d) {
                $scope.uoms = d.result;
            })
            BrandService.GetBrandForItemMaster({ itemMasterId: $scope.m.Id }).then(function (d) {
                $scope.brands = d.result;
            });
        }
        $scope.addInventory = function () {
            $scope.m.InventoryItems.push({Id:0});
        }
        $scope.removeInventory = function (index) {
            $scope.m.InventoryItems.splice(index, 1);
        }
        $scope.cancel = function () {
            if (!$scope.f.$pristine) {
                CommonService.cancelChanges(function () {
                    window.location.href = document.ItemMaster + 'Index';
                })
            }
            else
                window.location.href = document.ItemMaster + 'Index';
        }
        $scope.save = function () {          
            CommonService.saveOrUpdateChanges(function () {
                $scope.m.BasePrice = (typeof $scope.m.BasePrice === 'string' ? $scope.m.BasePrice.replaceAll(',', '') * 1 : $scope.m.BasePrice * 1);
                angular.forEach($scope.m.Brands, function (a, b) {
                    a.ItemCost = (typeof a.ItemCost === 'string' ? a.ItemCost.replaceAll(',', '') * 1 : a.ItemCost * 1);
                })
                angular.forEach($scope.m.PriceLists, function (a, b) {
                    a.ItemCost = (typeof a.ItemCost === 'string' ? a.ItemCost.replaceAll(',', '') * 1 : a.ItemCost * 1);
                })
                ItemMasterService.AddOrUpdate({ model: $scope.m }).then(function (d) {
                    if (d.success) {
                        CommonService.successMessage(d.message);
                        $timeout(function () {
                            window.location.href = document.ItemMaster + 'Index';

                        }, 500);
                    }
                    else {
                        CommonService.warningMessage(d.message);
                    }
                })
            },$scope.m.Id)
        }
        function loadPriceListData() {
            PriceListService.GetPriceListForItemMaster({ itemMasterId: $scope.m.Id }).then(function (data) {
                for (var i = 0; i <= data.result.length - 1; i++) {
                    data.result[i].ItemCost = formatter.format(data.result[i].ItemCost);
                }
                $scope.m.PriceLists = data.result;


            });
        }
        function loadBrandData() {
            BrandService.GetBrandForItemMaster({ itemMasterId: $scope.m.Id }).then(function (data) {
                for (var i = 0; i <= data.result.length - 1; i++) {
                    data.result[i].ItemCost = formatter.format(data.result[i].ItemCost);
                }
                $scope.m.Brands = data.result;

            });
        }
    }])