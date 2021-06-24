angular.module('MetronicApp')

    .controller('UnitsController',
        ['$scope', 'BillingUnitService', 'NgTableParams', '$uibModal', '$q', 'CommonService',
            function ($scope, BillingUnitService, NgTableParams, $uibModal, $q, CommonService) {

                //Other defaults
                $scope.createdOnDatePicker = {
                    opened: false
                };
                //Init
                this.$onInit = function () {
                    $scope.reset();
                    getOffices();
                };

                $scope.exportDataToExcelFile = function () {
                    if ($scope.resultsLength > 0) {
                        CommonService.showLoading();
                        BillingUnitService.ExportDataToExcelFile({
                            SortDirection: $scope.sortOrder,
                            SortColumn: $scope.sortColumn,
                            Units: $scope.Units,
                            Office: $scope.Office,
                            AppUser: $scope.AppUser,
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

                //#region SEARCH Units
                $scope.search = function () {
                    var initialSettings = {
                        getData: function (params) {
                            for (var i in params.sorting()) {
                                $scope.sortColumn = i;
                                $scope.sortOrder = params.sorting()[i];
                            }

                            var d = $q.defer();

                            BillingUnitService.GetUnits({
                                Page: params.page(),
                                PageSize: params.count(),
                                SortDirection: $scope.sortOrder,
                                SortColumn: $scope.sortColumn,
                                Units: $scope.Units,
                                Office: $scope.Office,
                                AppUser: $scope.AppUser,
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

                $scope.reset = function () {
                    $scope.Units = "";
                    $scope.AppUser = "";
                    $scope.Office = "";
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

                //#region Open Modal
                $scope.openUnitsSaveModal = function (unitsId) {
                    $uibModal.open({
                        animation: true,
                        ariaLabelledBy: 'modal-title',
                        ariaDescribedBy: 'modal-body',
                        templateUrl: 'UnitsSaveModal.html',
                        controller: 'UnitsSaveModalController',
                        controllerAs: 'controller',
                        size: 'md',
                        keyboard: false,
                        backdrop: "static",
                        windowClass: 'modal_style',
                        resolve: {
                            UnitsId: function () {
                                return unitsId;
                            }
                        }
                    }).result.then(function (data) {
                        $scope.reset();
                    },
                        function () {

                        });
                };
                //#endregion

                //#region Get Office
                function getOffices() {
                    CommonService.GetOfficesLookUp({
                    }).then(function (data) {
                        $scope.offices = data.data;
                    },
                        function (error /*Error event should handle here*/) {
                            console.log("Error");
                        },
                        function (data /*Notify event should handle here*/) {
                        });
                }

                function getMeterReader() {
                    BillingUnitService.GetMeterReaders({
                        officeId: OfficeId
                    }).then(function (data) {
                        $scope.offices = data.data;
                    },
                        function (error /*Error event should handle here*/) {
                            console.log("Error");
                        },
                        function (data /*Notify event should handle here*/) {
                        });
                }
                //#endregion

                //#region Delete
                $scope.deleteUnits = function (unitsId, name) {
                    CommonService.deleteChanges(function () {
                        BillingUnitService.unitsDelete({ id: unitsId }).then(function () {
                            CommonService.successMessage("Units was successfully deleted.");
                            $scope.search(true);
                        });
                    },name);
                };
                //#endregion

                //#region Deactivate/Reactivate
                $scope.toggleActiveStatus = function (id, isActive, name) {
                    if (isActive) {
                        CommonService.reActivate(function () {
                            BillingUnitService.ToggleUnitsActiveStatus({ Id: id, IsActive: isActive })
                                .then(function (data) {
                                    CommonService.successMessage(data.message);
                                    $scope.search();

                                }), function (error /*Error event should handle here*/) {
                                    console.log(error);
                                    CommonService.errorMessage("Unexpected error occured.");
                                }, function (data /*Notify event should handle here*/) {
                                };
                        },name);
                    } else {
                        CommonService.deActivate(function () {
                            BillingUnitService.ToggleUnitsActiveStatus({ Id: id, IsActive: isActive })
                                .then(function (data) {
                                    CommonService.successMessage(data.message);
                                    $scope.search();

                                }), function (error /*Error event should handle here*/) {
                                    console.log(error);
                                    CommonService.errorMessage("Unexpected error occured.");
                                }, function (data /*Notify event should handle here*/) {
                                };
                        }, name);
                    }
                };
                //#endregion

            }]);