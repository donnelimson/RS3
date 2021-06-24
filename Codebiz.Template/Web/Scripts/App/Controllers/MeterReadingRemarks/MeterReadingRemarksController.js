angular.module('MetronicApp')
    .controller('MeterReadingRemarksController',
        ['$scope', 'MeterReadingRemarksService', 'NgTableParams', '$uibModal', '$window', '$timeout', '$q', 'CommonService',
            function ($scope, MeterReadingRemarksService, NgTableParams, $uibModal, $window, $timeout, $q, CommonService) {

                $scope.createdOnDatePicker = {
                    opened: false
                };
                //Init
                this.$onInit = function () {
                    $scope.reset();
                };

                //#region SearchExport
                $scope.exportDataToExcelFile = function () {
                    if ($scope.resultsLength > 0) {
                        if ($scope.sortColumn === "CreatedDate") {
                            $scope.sortColumn = "CreatedDate";
                        }

                        CommonService.showLoading();
                        MeterReadingRemarksService.ExportDataToExcelFile({
                            Code: $scope.Code,
                            Name: $scope.Name,
                            IsActive: $scope.IsActive,
                            SortDirection: $scope.sortOrder,
                            SortColumn: $scope.sortColumn,
                            CreatedBy: $scope.CreatedBy,
                            CreatedOnFrom: getDateRangePickerValue(1, $scope.createdDate),
                            CreatedOnTo: getDateRangePickerValue(2, $scope.createdDate),
                            IsForExcelExport: true
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

                $scope.search = function (init) {
                    var initialSettings = {
                        getData: function (params) {
                            for (var i in params.sorting()) {
                                $scope.sortColumn = i;
                                $scope.sortOrder = params.sorting()[i];
                            }
                            var d = $q.defer();
                            var filter = params.filter();

                            MeterReadingRemarksService.SearchMeterReadingRemarks({
                                page: params.page(),
                                pageSize: params.count(),
                                sortOrder: $scope.sortOrder,
                                sortColumn: $scope.sortColumn,
                                SortDirection: $scope.sortOrder,
                                code: $scope.Code,
                                name: $scope.Name,
                                IsActive: $scope.IsActive,
                                CreatedBy: $scope.CreatedBy,
                                createdOnFrom: getDateRangePickerValue(1, $scope.createdDate),
                                createdOnTo: getDateRangePickerValue(2, $scope.createdDate)
                            }).then(function (data) {
                                $scope.resultsLength = data.totalRecordCount;
                                params.total($scope.resultsLength);
                                d.resolve(data.data);
                            });
                            return d.promise;
                        }
                    };
                    $scope.tableParams = new NgTableParams(10, initialSettings);
                };

                //Reset filter fields
                $scope.reset = function () {
                    $scope.Code = "";
                    $scope.Name = "";
                    $scope.CreatedBy = "";
                    $scope.createdDate = null;

                    $scope.sortOrder = "desc";
                    $scope.sortColumn = "CreatedDate";

                    $scope.search(true);
                };

                $scope.searchWhenEnter = function ($event) {
                    var keyCode = $event.which || $event.keyCode;
                    if (keyCode === 13) {
                        $scope.search();
                    }
                };

                //#endregion

                //#region open save/update modal
                $scope.openMeterReadingRemarksSaveModal = function (meterReadingRemarksId) {
                    $uibModal.open({
                        animation: true,
                        ariaLabelledBy: 'modal-title',
                        ariaDescribedBy: 'modal-body',
                        templateUrl: 'MeterReadingRemarksSaveModal.html',
                        controller: 'MeterReadingRemarksSaveModalController',
                        controllerAs: 'controller',
                        size: 'md',
                        keyboard: false,
                        backdrop: "static",
                        windowClass: 'modal_style',
                        resolve: {
                            MeterReadingRemarksId: function () {
                                return meterReadingRemarksId;
                            }
                        }
                    }).result.then(function (data) {
                        $scope.reset();
                    },
                        function () {

                        });
                };
                //#endregion

                //#region Delete
                $scope.deleteMeterReadingRemarks = function (meterReadingRemarksId, name) {
                    CommonService.deleteChanges(function () {
                        MeterReadingRemarksService.DeleteMeterReadingRemarks({ id: meterReadingRemarksId, name: name }).then(function (data) {
                            CommonService.successMessage(data.message);
                            $scope.search(true);
                        });
                    }, name);
                };

                //#endregion

                //#region Reactivate / Deactivate
                $scope.toggleActiveStatus = function (id, isActive, name) {
                    if (isActive) {
                        CommonService.reActivate(function () {
                            reactivateDeactivate(id, isActive, name);
                        }, name);
                    } else {
                        CommonService.deActivate(function () {
                            reactivateDeactivate(id, isActive, name);
                        }, name);
                    }
                };
                //#endregion

                function reactivateDeactivate(id, isActive, name) {
                    MeterReadingRemarksService.ToggleMeterReadingRemarksStatus({ id: id, isActive: isActive, name: name })
                        .then(function (data) {
                            CommonService.successMessage(data.message);
                            $scope.search();
                        }), function (error /*Error event should handle here*/) {
                            console.log(error);
                            CommonService.errorMessage("Unexpected error occured.");
                        }, function (data /*Notify event should handle here*/) {
                        };
                }
            }]);