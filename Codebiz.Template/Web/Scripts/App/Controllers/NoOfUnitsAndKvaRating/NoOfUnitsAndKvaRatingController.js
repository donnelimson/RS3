angular.module('MetronicApp')
    .controller('NoOfUnitsAndKvaRatingController',
        ['$scope', 'NoOfUnitsAndKvaRatingService', 'NgTableParams', '$uibModal', '$q', 'CommonService',
            function ($scope, NoOfUnitsAndKvaRatingService, NgTableParams, $uibModal, $q, CommonService) {

                //Other defaults
                $scope.createdOnDatePicker = {
                    opened: false
                };
                //Init
                this.$onInit = function () {
                    $scope.reset();
                };

                //#region View

                $scope.exportDataToExcelFile = function () {
                    if ($scope.resultsLength > 0) {
                        CommonService.showLoading();
                        NoOfUnitsAndKvaRatingService.ExportDataToExcelFile({
                            SortDirection: $scope.sortOrder,
                            SortColumn: $scope.sortColumn,
                            NoOfUnits: $scope.NoOfUnits,
                            KvaRating: $scope.KvaRating,
                            CreatedBy: $scope.CreatedBy,
                            CreatedOnFrom: getDateRangePickerValue(1, $scope.createdDate),
                            CreatedOnTo: getDateRangePickerValue(2, $scope.createdDate)
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

                $scope.search = function () {
                    var initialSettings = {
                        getData: function (params) {
                            for (var i in params.sorting()) {
                                $scope.sortColumn = i;
                                $scope.sortOrder = params.sorting()[i];
                            }

                            var d = $q.defer();

                            NoOfUnitsAndKvaRatingService.GetNoOfUnitsAndKvaRating({
                                Page: params.page(),
                                PageSize: params.count(),
                                SortDirection: $scope.sortOrder,
                                SortColumn: $scope.sortColumn,
                                NoOfUnits: $scope.NoOfUnits,
                                KvaRating: $scope.KvaRating,
                                CreatedBy: $scope.CreatedBy,
                                CreatedOnFrom: getDateRangePickerValue(1, $scope.createdDate),
                                CreatedOnTo: getDateRangePickerValue(2, $scope.createdDate)
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
                    $scope.NoOfUnits = "";
                    $scope.KvaRating = "";
                    $scope.CreatedBy = "";
                    $scope.createdDate = null;

                    $scope.sortOrder = "desc";
                    $scope.sortColumn = "CreatedDate";

                    $scope.search();
                };

                $scope.searchWhenEnter = function ($event) {
                    var keyCode = $event.which || $event.keyCode;
                    if (keyCode === 13) {
                        $scope.search();
                    }
                };

                //#endregion

                //#region Add/Update Modal
                $scope.openNoOfUnitsAndKvaRatingSaveModal = function (noOfUnitsKvaId) {
                    $uibModal.open({
                        animation: true,
                        ariaLabelledBy: 'modal-title',
                        ariaDescribedBy: 'modal-body',
                        templateUrl: 'NoOfUnitsAndKvaRatingSaveModal.html',
                        controller: 'NoOfUnitsAndKvaRatingSaveModalController',
                        controllerAs: 'controller',
                        size: 'md',
                        keyboard: false,
                        backdrop: "static",
                        windowClass: 'modal_style',
                        resolve: {
                            NoOfUnitsKvaId: function () {
                                return noOfUnitsKvaId;
                            }
                        }
                    }).result.then(function (data) {
                        $scope.reset();
                    },
                        function () {

                        });
                },
                    //#endregion

                    //#region Activate/Deactivate
                    $scope.toggleActiveStatus = function (id, isActive) {
                        CommonService.reactivateDeactivate(function () {
                            NoOfUnitsAndKvaRatingService.ToggleNoOfUnitsAndKvaRatingActiveStatus({ Id: id, IsActive: isActive })
                                .then(function (data) {
                                    if (data.success) {
                                        CommonService.successMessage(data.message);
                                        $scope.search();
                                    }
                                    else {
                                        CommonService.warningMessage(data.message);
                                        $scope.search();
                                    }
                                }), function (error /*Error event should handle here*/) {
                                    console.log(error);
                                    CommonService.errorMessage("Unexpected error occured.");
                                }, function (data /*Notify event should handle here*/) {
                                };
                        }, "No. of Units and KVA Rating", !isActive);
                    };

                //#endregion

                //#region Delete
                $scope.deleteNoOfUnitsAndKvaRating = function (noOfUnitsKvaId) {
                    CommonService.deleteChanges(function () {
                        NoOfUnitsAndKvaRatingService.DeleteNoOfUnitsAndKvaRating({ id: noOfUnitsKvaId }).then(function (data) {
                            if (data.result) {
                              
                                CommonService.successMessage(data.message);
                                $scope.search();
                            }
                            else {
                                CommonService.warningMessage(data.message);
                                $scope.search();
                            }
                        }), function (error /*Error event should handle here*/) {
                            console.log(error);
                            CommonService.errorMessage("Unexpected error occured.");
                        }, function (data /*Notify event should handle here*/) {
                        };
                    }, "No. of Units and KVA Rating");
                };
                //#endregion

                //#region OPEN DATE RANGE TIME PICKER
                $scope.openCreatedOnDatePicker = function () {
                    $scope.createdOnDatePicker.opened = true;
                };

                $scope.searchWhenEnter = function ($event) {
                    var keyCode = $event.which || $event.keyCode;
                    if (keyCode === 13) {
                        $scope.search();
                    }
                };
                //#endregion
            }]);