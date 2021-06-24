angular.module('MetronicApp')
    .controller('SurchargeController',
        ['$scope', '$http', 'SurchargeService', 'NgTableParams', '$uibModal', '$window', '$timeout', '$q', 'CommonService',
            function ($scope, $http, SurchargeService, NgTableParams, $uibModal, $window, $timeout, $q, CommonService) {

                //#region Variable Defaults


                //#endregionmodelValue
                $scope.createdOnDatePicker = {
                    opened: false
                };
                //#region Init

                this.$onInit = function () {
                    $scope.search(true);
                    GetConsumerClasses();
                    $scope.reset();
                };
                //#endregion
                

                $scope.exportDataToExcelFile = function () {
                    if ($scope.resultsLength > 0) {
                        CommonService.showLoading();
                        SurchargeService.ExportDataToExcelFile({
                            SortDirection: $scope.sortOrder,
                            SortColumn: $scope.sortColumn,
                            ConsumerClass: $scope.consumerClass,
                            YearOfDelinquency: $scope.YearOfDelinquency,
                            RatePerMonth: $scope.RatePerMonth,
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

                //#region View
                $scope.search = function () {
                    var initialSettings = {
                        getData: function (params) {
                            for (var i in params.sorting()) {
                                $scope.sortColumn = i;
                                $scope.sortOrder = params.sorting()[i];
                            }
                            var d = $q.defer();

                            SurchargeService.GetSurcharge({
                                ConsumerClass: $scope.consumerClass,
                                YearOfDelinquency: $scope.YearOfDelinquency,
                                RatePerMonth: $scope.RatePerMonth,
                                Page: params.page(),
                                PageSize: params.count(),
                                SortDirection: $scope.sortOrder,
                                SortColumn: $scope.sortColumn,
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
                        $scope.YearOfDelinquency = "";
                        $scope.RatePerMonth = "";
                        $scope.consumerClass = null;
                        $scope.createdDate = null;
                        $scope.CreatedBy = "";
                        $scope.sortOrder = "desc";
                        $scope.sortColumn = "CreatedDate";
                        $scope.search(true);
                    },

                    $scope.searchWhenEnter = function ($event) {
                        var keyCode = $event.which || $event.keyCode;
                        if (keyCode === 13) {
                            $scope.search();
                        }
                    };

                    //#endregion

                //#region Add/Update Modal
                $scope.openSurchargeSaveModal = function (surchargeId) {
                    $uibModal.open({
                        animation: true,
                        ariaLabelledBy: 'modal-title',
                        ariaDescribedBy: 'modal-body',
                        templateUrl: 'SurchargeSaveModal.html',
                        controller: 'SurchargeSaveModalController',
                        controllerAs: 'controller',
                        size: 'md',
                        keyboard: false,
                        backdrop: "static",
                        windowClass: 'modal_style',
                        resolve: {
                            SurchargeId: function () {
                                return surchargeId;
                            }
                        }
                    }).result.then(function (data) {
                        $scope.reset();
                    },
                        function () {

                        });
                },
                //#endregion

                //#region OPEN DATE RANGE TIME PICKER
                $scope.openCreatedOnDatePicker = function () {
                    $scope.createdOnDatePicker.opened = true;
                },

                    $scope.searchWhenEnter = function ($event) {
                        var keyCode = $event.which || $event.keyCode;
                        if (keyCode === 13) {
                            $scope.search();
                        }
                    };
                //#endregion

                //#region Activate/Deactivate
                $scope.toggleActiveStatus = function (id, isActive,name) {
                    if (isActive) {
                        CommonService.reActivate(function () {
                            SurchargeService.ToggleSurchargeActiveStatus({ Id: id, IsActive: isActive })
                                .then(function (data) {
                                    CommonService.successMessage(data.message);
                                    $scope.search();

                                }), function (error /*Error event should handle here*/) {
                                    console.log(error);
                                    CommonService.errorMessage("Unexpected error occured.");
                                }, function (data /*Notify event should handle here*/) {
                                };
                        }, name);
                    } else {
                        CommonService.deActivate(function () {
                            SurchargeService.ToggleSurchargeActiveStatus({ Id: id, IsActive: isActive })
                                .then(function (data) {
                                    CommonService.successMessage(data.message);
                                    $scope.search();

                                }), function (error /*Error event should handle here*/) {
                                    console.log(error);
                                    CommonService.errorMessage("Unexpected error occured.");
                                }, function (data /*Notify event should handle here*/) {
                                };
                        },name);
                    }
                };
         //#endregion

                //#region Delete
                $scope.deleteSurcharge = function (surchargeId,name) {
                    CommonService.deleteChanges(function () {
                        SurchargeService.DeleteSurcharge({ id: surchargeId }).then(function () {
                            CommonService.successMessage("Surcharge was successfully deleted.");
                            $scope.search(true);
                        });
                    },name);
                };
                //#endregion

                //#region Private Functions
                    function GetConsumerClasses() {
                        SurchargeService.GetConsumerClasses({
                        }).then(function (data) {
                            $scope.consumerClasses = data.data;
                            $scope.consumerClasses = $scope.consumerClasses.filter(s => s.Description === 'Residential' || s.Description === 'Low Voltage' || s.Description === 'High Voltage');
                        }, function (error /*Error event should handle here*/) {
                            console.log("Error");
                        }, function (data /*Notify event should handle here*/) {
                        });
                }




                //#endregion

            }]);