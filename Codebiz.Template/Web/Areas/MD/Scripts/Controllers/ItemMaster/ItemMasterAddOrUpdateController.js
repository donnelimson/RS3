MetronicApp.controller('ItemMasterAddOrUpdateController', ['$scope', '$q', 'NgTableParams', 'CommonService', '$uibModal', '$timeout', '$location', '$window', 'ItemMasterService', '$controller','PriceListService',
    function ($scope, $q, NgTableParams, CommonService, $uibModal, $timeout, $location, $window, ItemMasterService, $controller, PriceListService) {
        $scope.m = {
            Id: $location.search().id ?? 0,
            LongDescription: "",
            ShortDescription: "",
            ItemCode: "",
            BasePrice: (0).toFixed(2)
        };
        this.$onInit = function () {
            ItemMasterService.GetDetailsById({ id: $scope.m.Id }).then(function (d) {
                if (d.result != null) {
                    $scope.m = d.result;
                    $scope.isUpdate = true;
                }

            });
            loadPriceListData();
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
                ItemMasterService.AddOrUpdate({ model: $scope.m }).then(function (d) {
                    if (d.success) {
                        CommonService.successMessage(d.message);
                        $timeout(function () {
                            window.location.href = document.ItemMaster + 'Index';

                        }, 1000);
                    }
                    else {
                        CommonService.warningMessage(d.message);
                    }
                })
            },$scope.m.Id)
        }
        function loadPriceListData() {
            var initialParams = {
                count: 50
            }
            var initialSettings = {
                getData: function (params) {
                    for (var i in params.sorting()) {
                        $scope.sortColumn = i;
                        $scope.sortOrder = params.sorting()[i];
                    }
                    var d = $q.defer();
                    $scope.f.Page = params.page();
                    $scope.f.PageSize = params.count();
                    $scope.f.SortDirection = $scope.sortOrder == null ? 'asc' : $scope.sortOrder;
                    $scope.f.SortColumn = $scope.sortColumn == null ? 'Name' : $scope.sortColumn;
                    PriceListService.GetPriceListForItemMaster({ itemMasterId: $scope.m.Id }).then(function (data) {
                        $scope.resultsLength = data.totalRecordCount;
                        params.total(data.totalRecordCount);
                        let formatter = Intl.NumberFormat('en-US', { minimumFractionDigits:2});
                        for (var i = 0; i <= data.result.length - 1; i++) {
                            data.result[i].ItemCost = formatter.format(data.result[i].ItemCost);
                        }
                        d.resolve(data.result);
                        $scope.m.Items = data.result;
                    });
                    return d.promise;
                },
                counts: []
            };
            $scope.priceListTableParams = new NgTableParams(initialParams, initialSettings);
        }
    }])