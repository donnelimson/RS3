MetronicApp.controller('BarangayController', ['$scope', '$q', 'NgTableParams', 'CommonService','BarangayService','$uibModal',
    function ($scope, $q, NgTableParams, CommonService, BarangayService, $uibModal) {
        $scope.tableForBarangay = new NgTableParams({}, { dataset: [] })

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

                    BarangayService.Search({
                        Page: params.page(),
                        PageSize: params.count(),
                        SortDirection: $scope.sortOrder,
                        SortColumn: $scope.sortColumn,
                        Barangay: $scope.barangayName
                    }).then(function (data) {
                        $scope.resultsLength = data.totalRecordCount;
                        params.total($scope.resultsLength);
                        d.resolve(data.data);
                    });
                    return d.promise;
                }
            };
            $scope.tableForBarangay = new NgTableParams(10, initialSettings);
        }

        //Export to Excel
        $scope.exportDataToExcelFile = function () {
            if ($scope.resultsLength > 0) {
                CommonService.showLoading();

                BarangayService.ExportDataToExcelFile({
                    SortDirection: $scope.sortOrder,
                    SortColumn: $scope.sortColumn,
                    Barangay: $scope.barangayName,
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

            $scope.barangayName = "";
            $scope.search();
        }

        //$scope.search();
        //$scope.reset = function () {
        //    $scope.barangayName = "";
        //    $scope.search();
        //}
        $scope.searchWhenEnter = function ($event) {
            var keyCode = $event.which || $event.keyCode;
            if (keyCode === 13) {
                $scope.search();
            }
        }
        $scope.reactivateDeactivate = function (barangayId, status, name) {
            CommonService.reactivateDeactivate(function () {
                    BarangayService.ReactivateDeactivate({
                        barangayId: barangayId,
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
        $scope.openAddOrUpdateModal = function (barangayId) {
            $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: 'BarangayAddOrUpdate.html',
                controller: 'BarangayAddOrUpdateController',
                size: 'md',
                keyboard: false,
                backdrop: "static",
                windowClass: 'modal_style',
                resolve: {
                    BarangayId: function () {
                        return barangayId;
                    },
                }
            }).result.then(function () {
                $scope.search();
            }, function () {

            });
        }


     
    }]);
