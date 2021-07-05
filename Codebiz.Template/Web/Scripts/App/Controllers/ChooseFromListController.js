angular.module('MetronicApp')
    .controller('ChooseFromListController',
        ['$scope', 'NgTableParams', '$uibModalInstance', '$q', 'CommonService','Data','ChooseFromListService','$timeout',
            function ($scope, NgTableParams, $uibModalInstance, $q, CommonService, Data, ChooseFromListService, $timeout) {
                $scope.module = Data.Module;
                $scope.f = {
                    Page: 1,
                    PageSize: 10,
                    SortDirection: "",
                    SortColumn: "",
                    Searcher: ""
                }
                $scope.onSelectClick = function (d, index) {
                    $scope.selectedRow = index;
                    $scope.selectedData = d;
                }
                $scope.select = function (d) {
                    $uibModalInstance.close($scope.selectedData);
                }
                $scope.onSelectDoubleClick = function (d) {
                    $uibModalInstance.close(d);
                }
                $scope.navigate = function () {
                    var initialSettings = {
                        counts:[],
                        getData: function (params) {
                            for (var i in params.sorting()) {
                                $scope.sortColumn = i;
                                $scope.sortOrder = params.sorting()[i];
                            }
                            var d = $q.defer();
                            $scope.f.Page = params.page();
                            $scope.f.PageSize = params.count();
                            $scope.f.SortDirection = $scope.sortOrder == null ? 'desc' : $scope.sortOrder;
                            $scope.f.SortColumn = $scope.sortColumn == null ? 'Id' : $scope.sortColumn;
                            loadList(d, params);
                            return d.promise;
                           
                        }
                    };
                    $scope.tableParams = new NgTableParams(10, initialSettings);
                }
                $scope.close = function () {
                    $uibModalInstance.dismiss('cancel');
                }
                this.$onInit = function () {
                    $scope.navigate();
                  
                }
                function loadList(d, params, initialSettings) {
                    if (Data.LookupType == "APU") {
                        ChooseFromListService.GetAllAppuserForCFL({
                            filter: $scope.f,
                            roleId: Data.RoleId,
                        }).then(function (data) {
                            $scope.resultsLength = data.totalRecordCount;
                            params.total(data.totalRecordCount);
                            d.resolve(data.result);
                            $scope.currentPage = params.page();
                            $scope.tableCount = params.count();
                            $scope.offSetCount = data.result.length;
                        });
                    } else if (Data.LookupType == "TCK") {
                        ChooseFromListService.GetMyTickets({
                            filter: $scope.f
                        }).then(function (data) {
                            $scope.resultsLength = data.totalRecordCount;
                            params.total(data.totalRecordCount);
                            d.resolve(data.result);
                            $scope.currentPage = params.page();
                            $scope.tableCount = params.count();
                            $scope.offSetCount = data.result.length;
                        });
                    }
          
              
                }

     }]);