MetronicApp.controller('PriceListAddOrUpdateController', ['$scope', '$q', 'NgTableParams', 'CommonService', '$uibModal', '$timeout', '$location', '$window', 'PriceListService', '$controller',
    function ($scope, $q, NgTableParams, CommonService, $uibModal, $timeout, $location, $window, PriceListService, $controller) {
        $scope.f = {
            Searcher: "",
            Page: 1,
            PageSize: 50,
        }
        let formatter = Intl.NumberFormat('en-US', { minimumFractionDigits: 2 });
        this.$onInit = function () {
            if ($location.search().id != null) {
                PriceListService.GetDetailsById({ id: $location.search().id }).then(function (d) {
                    $scope.m = d.result;
                    $scope.isUpdate = true;
                    loadAllItems();
                })
            }
            else
              loadAllItems();

        }
        $scope.m = {
            Id: 0,
            LongDescription: "",
            ShortDescription: "",
            ItemCode: "",
            Items: [],
            IsActive:false
        };
        $scope.cancel = function () {
            if (!$scope.f.$pristine) {
                CommonService.cancelChanges(function () {
                    window.location.href = document.PriceList + 'Index';
                })
            }
            else
                window.location.href = document.PriceList + 'Index';
        }
        $scope.save = function () {

            CommonService.saveOrUpdateChanges(function () {
                for (var i = 0; i <= $scope.m.Items.length - 1;i++) {
                    $scope.m.Items[i].ItemCost = (typeof $scope.m.Items[i].ItemCost  === 'string')  ? $scope.m.Items[i].ItemCost.replaceAll(',', '' ) : $scope.m.Items[i].ItemCost; 
                }
                PriceListService.AddOrUpdate({ model: $scope.m }).then(function (d) {
                    if (d.success) {
                        CommonService.successMessage(d.message);
                        $timeout(function () {
                            window.location.href = document.PriceList + 'Index';

                        }, 1000);
                    }
                    else {
                        CommonService.warningMessage(d.message);
                    }
                })
            }, $scope.m.Id)
        }
        function loadAllItems() {
            var initialParams = {
                count:50
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
                    $scope.f.SortColumn = $scope.sortColumn == null ? 'LongDescription' : $scope.sortColumn;
                    $scope.f.Id = $scope.isUpdate ? $scope.m.Id : null;
                    PriceListService.GetAllItemsForPriceList({ filter: $scope.f }).then(function (data) {
                        $scope.resultsLength = data.totalRecordCount;
                        params.total(data.totalRecordCount);
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
            $scope.tableParams = new NgTableParams(initialParams, initialSettings);
        }
    }])