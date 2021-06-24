MetronicApp.controller('CityTownController', ['$scope', '$q', 'NgTableParams', 'CommonService', 'CityTownService','$uibModal',
    function ($scope, $q, NgTableParams, CommonService, CityTownService,$uibModal) {
        $scope.tableForCityTown = new NgTableParams({}, { dataset: [] })

        this.$onInit = function () {
            $scope.reset();
        };

        $scope.search = function () {
            var initialSettings = {
                getData: function (params) {

                    for (var i in params.sorting()) {
                        $scope.sortColumn = i;
                        $scope.sortOrder = params.sorting()[i];
                    }
                    var d = $q.defer();

                    CityTownService.Search({
                        Page: params.page(),
                        PageSize: params.count(),
                        SortDirection: $scope.sortOrder,
                        SortColumn: $scope.sortColumn,
                        CityTown: $scope.cityTown,
                        Province: $scope.province
                    }).then(function (data) {
                        $scope.resultsLength = data.totalRecordCount;
                        params.total($scope.resultsLength);
                        d.resolve(data.data);
                    });
                    return d.promise;
                }
            };
            $scope.tableForCityTown = new NgTableParams(10, initialSettings);
        }

        //Export to Excell
        $scope.exportDataToExcelFile = function () {
            if ($scope.resultsLength > 0) {
                CommonService.showLoading();

                CityTownService.ExportDataToExcelFile({
                    SortDirection: $scope.sortOrder,
                    SortColumn: $scope.sortColumn,
                    CityTown: $scope.cityTown,
                    Province: $scope.province,
                }).then(function (data) {
                    CommonService.hideLoading();
                    window.location.href = document.FileUpload + "DownloadExcelFile?fileName=" + data.data.FileName;

                }, function (error /*Error event should handle here*/) {
                    console.log("Error");
                }, function (data /*Notify event should handle here*/) {
                    console.log("Error");
                });
            }
        };

        $scope.reset = function () {
            $scope.sortColumn = "CreatedOn";
            $scope.sortOrder = "desc";

            $scope.cityTown = "";
            $scope.province = "";
            $scope.createdBy = "";
            $scope.search();
        }
        $scope.searchWhenEnter = function ($event) {
            var keyCode = $event.which || $event.keyCode;
            if (keyCode === 13) {
                $scope.search();
            }
        }
        $scope.reactivateDeactivate = function (cityTownId, status,name) {

            CommonService.reactivateDeactivate(function () {
                CityTownService.ReactivateDeactivate({
                    cityTownId: cityTownId,
                    name: name,
                    status:status
                }).then(function (data) {
                    if (data.succeed) {
                        CommonService.successMessage(data.message);
                    }
                    else {
                        CommonService.errorMessage(data.message);
                    }
                    $scope.search();
                })
            },name,status)
            
        }
        $scope.openAddOrUpdateModal = function (cityTownId) {
            $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: 'CityTownAddOrUpdate.html',
                controller: 'CityTownAddOrUpdateController',
                size: 'xlg',
                keyboard: false,
                backdrop: "static",
                windowClass: 'modal_style',
                resolve: {
                    CityTownId: function () {
                        return cityTownId;
                    },
                }
            }).result.then(function () {
                $scope.search();
            }, function () {

            });
        }
  

    }]);
