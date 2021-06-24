angular.module('MetronicApp')
    .controller('SearchItemController', ['$scope', '$uibModalInstance', 'PoleService', 'NgTableParams', '$q', 'CommonService', 'Data',
        function ($scope, $uibModalInstance, PoleService, NgTableParams, $q, CommonService, Data) {

            $scope.itemsTable = new NgTableParams({}, { dataset: [] });
            $scope.itemList = [];

            $scope.title = Data.Title;

            if (Data.Items != undefined || Data.Items != null) {
                if (Data.Items.length != 0) {
                    $scope.items = Data.Items;
                    $scope.itemsSelected = Data.Items;
                }
                else {
                    $scope.items = [];
                }
            }

            this.$onInit = function () {
                $scope.sortColumn = "ItemGroupId";
                $scope.sortOrder = "desc";
                getItems();
            }


            function getItems() {
                var initialSettings = {
                    counts: [],
                    getData: function (params) {
                        for (var i in params.sorting()) {
                            $scope.sortColumn = i;
                            $scope.sortOrder = params.sorting()[i];
                        }
                        var d = $q.defer();
                        var filter = params.filter();
                        var searchFilter = {
                            Page: params.page(),
                            PageSize: params.count(),
                            SortDirection: $scope.sortOrder,
                            SortColumn: $scope.sortColumn,
                        };
                        PoleService.GetPolesLookUpInMasterItem({
                            filter: searchFilter,
                            code: filter["ItemCode"],
                            name: filter["ItemName"],
                        }).then(function (data) {
                            params.total(data.filteredRecordCount);
                            $scope.totalRecordCount = data.totalRecordCount;
                            $scope.dataResult = data.data;
                            $scope.filteredRecordCount = data.filteredRecordCount;
                            $scope.itemList  = data.data;
                            $scope.currentPage = params.page();
                            $scope.tableCount = params.count();
                            $scope.offSetCount = data.data.length;

                            d.resolve(data.data);

                        });
                        return d.promise;
                    }
                };
                $scope.itemsTable = new NgTableParams($scope.pageSize, initialSettings);
            }

            $scope.clear = function () {

                $scope.itemsSelected = null;
                getItems();
                $scope.higlightRow(null)
            }

            $scope.goSelect = function () {
                if ($scope.itemsSelected != undefined || $scope.itemsSelected != null) {
                    $uibModalInstance.close($scope.itemsSelected);
                }
                else {
                    var warining = Type === 'item' ? "Noting is selected. Please select an item!" :
                        "Noting is selected. Please select a " + Type + "!";
                    CommonService.warningMessage(warining)
                }
            }

            $scope.higlightRow = function (row) {
                $scope.items = row;
                $scope.itemsSelected = row;
            }

            $scope.selectItem = function (row) {
                $scope.goSelect();
            }

            $scope.close = function () {
                $uibModalInstance.dismiss('cancel');
            };
        }
    ]
    );