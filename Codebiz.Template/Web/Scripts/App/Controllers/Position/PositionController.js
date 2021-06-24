angular.module('MetronicApp').controller('PositionController',
    ['$scope', 'PositionService', 'NgTableParams', '$uibModal', '$q', 'CommonService',
        function ($scope, PositionService, NgTableParams, $uibModal, $q, CommonService) {

            //#region Other defaults

            $scope.createdOnDatePicker = {
                opened: false
            };

            //#endregion

            //#region Initialization

            this.$onInit = function () {
                $scope.search();
                $scope.sortColumn = "CreatedDate";
                $scope.sortOrder = "desc";
            }

            //#endregion

            //#region Export Data to Excel

            $scope.exportDataToExcelFile = function () {
                if ($scope.resultsLength > 0) {
                    CommonService.showLoading();
                    PositionService.ExportDataToExcelFile({
                        SortDirection: $scope.sortOrder,
                        SortColumn: $scope.sortColumn,
                        code: $scope.Code,
                        name: $scope.Name,
                        IsActive: $scope.IsActive,
                        CreatedBy: $scope.CreatedBy,
                        createdOnFrom: getDateRangePickerValue(1, $scope.createdDate),
                        createdOnTo: getDateRangePickerValue(2, $scope.createdDate)
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

            //#endregion

            //#region Search 

            $scope.search = function (init) {
                var initialSettings = {
                    getData: function (params) {
                        for (var i in params.sorting()) {
                            $scope.sortColumn = i;
                            $scope.sortOrder = params.sorting()[i];
                        }
                        var d = $q.defer();

                        PositionService.GetPositions({
                            Page: params.page(),
                            PageSize: params.count(),
                            SortDirection: $scope.sortOrder,
                            SortColumn: $scope.sortColumn,
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

            }

            //#endregion

            //#region Reset filter fields

            $scope.reset = function () {
                $scope.Code = "";
                $scope.Name = "";
                $scope.CreatedBy = "";
                $scope.createdDate = null;

                $scope.search();
            }

            //#endregion

            //#region OPEN SAVE/UPDATE MODAL

            $scope.openPositionSaveModal = function (positionId) {
                $uibModal.open({
                    animation: true,
                    ariaLabelledBy: 'modal-title',
                    ariaDescribedBy: 'modal-body',
                    templateUrl: 'PositionSaveModal.html',
                    controller: 'PositionSaveModalController',
                    controllerAs: 'controller',
                    size: 'md',
                    keyboard: false,
                    backdrop: "static",
                    windowClass: 'modal_style',
                    resolve: {
                        PositionId: function () {
                            return positionId;
                        }
                    }
                }).result.then(function (data) {
                    $scope.search();
                },
                    function () {

                    });
            }

            //#endregion

            //#region Reactivate/ Deactivate

            $scope.toggleActiveStatus = function (item, isActive) {
                if (!item.IsActive) {
                    CommonService.reActivate(function () {
                        PositionService.TogglePositionActiveStatus({ id: item.PositionId, isActive: isActive, name: item.Name })
                            .then(function (data) {
                                if (data.success) {
                                    CommonService.successMessage(data.message);
                                    $scope.search();
                                }
                                else {
                                    CommonService.warningMessage(data.message);
                                }
                            }), function (error /*Error event should handle here*/) {
                                console.log(error);
                                CommonService.errorMessage("Unexpected error occured.");
                            }, function (data /*Notify event should handle here*/) {
                            }
                    },
                        item.Name)
                } else {
                    CommonService.deActivate(function () {
                        PositionService.TogglePositionActiveStatus({ id: item.PositionId, isActive: isActive, name: item.Name})
                            .then(function (data) {
                                if (data.success) {
                                    CommonService.successMessage(data.message);
                                    $scope.search();
                                }
                                else {
                                    CommonService.warningMessage(data.message);
                                }
                            }), function (error /*Error event should handle here*/) {
                                console.log(error);
                                CommonService.errorMessage("Unexpected error occured.");
                            }, function (data /*Notify event should handle here*/) {
                            }
                    },
                        item.Name)
                }
            }

            //#endregion

            //#region DELETE POSITION

            $scope.deletePosition = function (item) {
                CommonService.deleteChanges(function () {
                    PositionService.positionDelete({ id: item.PositionId, name: item.Name }).then(function (data) {
                        if (data.result) {
                            CommonService.successMessage(data.message);
                            $scope.search();
                        }
                        else {
                            CommonService.warningMessage(data.message);
                        }
                    }, function (error /*Error event should handle here*/) {
                        console.log(error);
                        CommonService.errorMessage("Unexpected error occured.");
                    }, function (data /*Notify event should handle here*/) {
                    })
                }, item.Name)
            }

            //#endregion

            //#region OPEN DATE RANGE TIME PICKER

            $scope.openCreatedOnDatePicker = function () {
                $scope.createdOnDatePicker.opened = true;
            }

            //#endregion

            //#region Search when enter

            $scope.searchWhenEnter = function ($event) {
                var keyCode = $event.which || $event.keyCode;
                if (keyCode === 13) {
                    $scope.search();
                }
            }

            //#endregion

        }
    ]
);