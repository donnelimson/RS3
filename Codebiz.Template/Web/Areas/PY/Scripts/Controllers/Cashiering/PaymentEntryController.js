MetronicApp.controller('PaymentEntryController', ['$scope', '$q', 'NgTableParams', 'CommonService', '$uibModal', '$timeout', '$location', '$window', 'CashieringService', '$controller', '$filter','ItemMasterService',
    function ($scope, $q, NgTableParams, CommonService, $uibModal, $timeout, $location, $window, CashieringService, $controller, $filter, ItemMasterService) {
        this.$onInit = function () {
            $scope.pricing = 'BP';
            ItemMasterService.GetAllItems().then(function (d) {
                $scope.items = d.result;
            })
        }
        $scope.m = {
            Items: [],
            PostingDate: $filter('date')(new Date(), 'MM/dd/yyyy'),
            Total: 0,
            TotalTender: '0.00',
            Change:'0.00'
        };
        $scope.total = 0;
        let formatter = Intl.NumberFormat('en-US', { minimumFractionDigits: 2 });
        $scope.isCash = true;
        $scope.deleteItem = function (index) {
            $scope.m.Items.splice(index, 1);
        }
        $scope.addItem = function () {
            //var modalData = {
            //    LookupType: 'ITM',
            //    SelectedData: $scope.m.Items
            //}
            //$uibModal.open({
            //    animation: true,
            //    ariaLabelledBy: 'modal-title',
            //    ariaDescribedBy: 'modal-body',
            //    templateUrl: `${document.baseUrlNoArea}ChooseFromList/GetLookup?objType=${modalData.LookupType}`,
            //    controller: 'ChooseFromListController',
            //    size: 'md',
            //    keyboard: false,
            //    backdrop: "static",
            //    windowClass: 'modal_style',
            //    modalOverflow: true,
            //    resolve: {
            //        Data: function () {
            //            return modalData;
            //        },
            //    }
            //}).result.then(function (data) {
            //    $scope.m.Items = [];
            //    console.log(data)
            //    angular.forEach(data, function (a, b) {
            //        if ($scope.pricing == 'BP') {
            //            a.ItemCost = formatter.format(a.BasePrice);
            //        }
            //        if ($scope.pricing == 'B') {
            //            var index = a.Brands.findIndex(r => r.IsDefault);
            //            if (index != -1) {
            //                a.Brand = a.Brands[index];
            //                a.ItemCost = formatter.format(a.Brand.ItemCost);
            //            }
                       
            //        }
            //        $scope.m.Items.push(a);
            //    })
            //    computeTotal();
                //console.log($scope.m.Items)


           // });
            $scope.m.Items.push({});
        }
        $scope.getItemDetail = function (index, id) {
            ItemMasterService.GetItemMasterLookUpById({ itemMasterId: id }).then(function (d) {
                $scope.m.Items[index] = d.result;
                $scope.m.Items[index].ItemCost = formatter.format(d.result.BasePrice);
                $scope.m.Items[index].MaxQty = 0;
                if (d.result.Brands.length == 1) {
                    $scope.m.Items[index].Brand = d.result.Brands[0];
                    $scope.setBrand($scope.m.Items[index]);
                }
                else {
                    $scope.m.Items[index].Brand = null;
                }
                if ($scope.pricing == 'B') {
                    var index2 = $scope.m.Items[index].Brands.findIndex(r => r.IsDefault);
                    if (index2 != -1) {
                        $scope.m.Items[index].Brand = $scope.m.Items[index].Brands[index2];
                        $scope.m.Items[index].ItemCost = formatter.format($scope.m.Items[index].Brand.ItemCost);
                        $scope.setBrand($scope.m.Items[index])
                    }
                }
            });
         
        }
        $scope.setBrand = function (row, data) {
            row.MaxQty = null;
            row.Qty = null;
            row.InvalidQty = false;
            if ($scope.pricing == 'BP') {
                row.ItemCost = formatter.format((row.BasePrice * 1));
            }
            else if ($scope.pricing == 'B') {
                row.ItemCost = formatter.format(row.Brand.ItemCost);
            }
            ItemMasterService.GetMaxQtyByItemMasterAndBrandId({ itemMasterId: row.Id, brandId: row.Brand.Id }).then(function (d) {
                row.MaxQty = d.result == null ? 0 : d.result;
            });
            computeTotal();
        }
        $scope.checkMaxQty = function (row) {
            row.InvalidQty = false;
            if (row.Qty > row.MaxQty) {
                row.InvalidQty = true;
            }
            var cost = $scope.pricing == 'BP' ? row.BasePrice : row.Brand.ItemCost;
            row.ItemCost = formatter.format((typeof cost === 'string' ? cost.replaceAll(',', '') * 1 : cost) * row.Qty);

            computeTotal();
        }
    //if ($scope.pricing == 'B') {
    //    var index = a.Brands.findIndex(r => r.IsDefault);
    //    if (index != -1) {
    //        a.Brand = a.Brands[index];
    //        a.ItemCost = formatter.format(a.Brand.ItemCost);
    //    }

    //                }
    //    }
        $scope.computeTender = function () {
            $scope.m.Change = formatter.format((typeof $scope.m.TotalTender === 'string' ? $scope.m.TotalTender.replaceAll(',', '') * 1 : $scope.m.TotalTender * 1) - (typeof $scope.m.Total === 'string' ? $scope.m.Total.replaceAll(',', '') * 1 : $scope.m.Total * 1));
        }
        function computeTotal() {
            $scope.m.Total = 0;
            angular.forEach($scope.m.Items, function (a, b) {
                $scope.m.Total += (typeof a.ItemCost === 'string' ? a.ItemCost.replaceAll(',', '') * 1 : a.ItemCost*1);
            })
            $scope.computeTender();
            //console.log($scope.total)
        }
        // showSpinnerWhileIFrameLoads();
    }])