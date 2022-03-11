angular.module('MetronicApp')
    .controller('ChooseFromListController',
        ['$scope', 'NgTableParams', '$uibModalInstance', '$q', 'CommonService','Data','ChooseFromListService','$timeout',
            function ($scope, NgTableParams, $uibModalInstance, $q, CommonService, Data, ChooseFromListService, $timeout) {
                $scope.module = Data.Module;
                $scope.selectedData = [];
                if (Data.SelectedData != null) {
                    $scope.selectedData = angular.copy(Data.SelectedData);
                }
             
                $scope.f = {
                    Page: 1,
                    PageSize: 10,
                    SortDirection: "",
                    SortColumn: "",
                    Searcher: ""
                }
                $scope.onSelectClick = function (e, d, index) {
                    if (e.ctrlKey) {
                        if (d.selectedRow == null) {
                            d.selectedRow = d.Id;
                            $scope.selectedData.push(d);
                        }
               
                    } else {
                        if (d.selectedRow != null) {
                            d.selectedRow = null;
                            $scope.selectedData.splice(index, 1);
                        }
                        else {
                            $scope.selectedData = [];
                            for (var i = 0; i <= $scope.data.length - 1; i++) {
                                $scope.data[i].selectedRow = null;
                            }
                            d.selectedRow = d.Id;
                            $scope.selectedData.push(d);
                        }
           
                    }
                }
                $scope.select = function (d) {
                    $uibModalInstance.close($scope.selectedData);
                }
                $scope.onSelectDoubleClick = function (d,index) {
                    d.selectedRow = index;
                    $scope.selectedData.push(d);
                    $uibModalInstance.close($scope.selectedData);
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
                    if (Data.LookupType == "ITM") {
                        ChooseFromListService.GetItemMasterLookUp({
                            filter: $scope.f,
                        }).then(function (data) {
                            //console.log(data.result)
                            $scope.resultsLength = data.totalRecordCount;
                            params.total(data.totalRecordCount);
                            $scope.data = data.result;
                            d.resolve(data.result);
                            $timeout(function () {
                                if ($scope.selectedData.length > 0) {
                                    for (var i = 0; i <= $scope.selectedData.length - 1; i++) {
                                        var x = $scope.data.findIndex(r => r.Id == $scope.selectedData[i].selectedRow);
                                        if (x != -1) {
                                            $scope.data[x]["selectedRow"] = $scope.selectedData[i].selectedRow;

                                        }
                                    }
                                }
                            },10)
                          
                            $scope.currentPage = params.page();
                            $scope.tableCount = params.count();
                            $scope.offSetCount = data.result.length;
                   
                        });
                    }
          
              
                }

     }]);