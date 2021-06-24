angular.module('MetronicApp').controller('LifelineSubsidyController',
    ['$scope', 'LifelineSubsidyService', 'NgTableParams', '$uibModal', '$q', 'CommonService',
        function ($scope, LifelineSubsidyService, NgTableParams, $uibModal, $q, CommonService) {

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
                    LifelineSubsidyService.ExportDataToExcelFile({
                        SortDirection: $scope.sortOrder,
                        SortColumn: $scope.sortColumn,
                        Code: $scope.Code,
                        Minimum: $scope.Minimum,
                        Maximum: $scope.Maximum,
                        Discount: $scope.Discount
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

            //#region Search When Enter

            $scope.searchWhenEnter = function ($event) {
                var keyCode = $event.which || $event.keyCode;
                if (keyCode === 13) {
                    $scope.search();
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

                        LifelineSubsidyService.GetLifelineSubsidy({
                            Page: params.page(),
                            PageSize: params.count(),
                            SortDirection: $scope.sortOrder,
                            SortColumn: $scope.sortColumn,
                            Code: $scope.Code,
                            Minimum: $scope.Minimum,
                            Maximum: $scope.Maximum,
                            Discount: $scope.Discount
                        }).then(function (data) {
                            $scope.resultsLength = data.TotalRecordCount;
                            params.total($scope.resultsLength);
                            d.resolve(data.DataResult);
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
                $scope.Minimum = null;
                $scope.Maximum = null;
                $scope.Discount = null;

                $scope.search();
            }

            //#endregion

            //#region Delete

            $scope.deleteLifelineSubsidy = function (item) {
                CommonService.deleteChanges(function () {
                    LifelineSubsidyService.DeleteLifelineSubsidy({
                        id: item.LifelineSubsidyId
                    }).then(function (data) {
                        if (data.DataResult.Success) {
                            CommonService.successMessage(data.DataResult.Message);
                            $scope.search();
                        }
                        else {
                            CommonService.warningMessage(data.message);
                        }
                    });

                }, "Lifeline subsidy");
            }

             //#endregion

            //#region Reactivate / Deactivate

            $scope.toggleActiveStatus = function (item) {
                CommonService.reactivateDeactivate(function () {
                    LifelineSubsidyService.ToggleActiveStatus({ id: item.LifelineSubsidyId, status: item.IsActive })
                        .then(function (data) {
                            if (data.DataResult.Success) {
                                CommonService.successMessage(data.DataResult.Message);
                                $scope.search();
                            }
                            else {
                                CommonService.warningMessage(data.DataResult.Message);
                            }

                            $scope.search();

                        }), function (error /*Error event should handle here*/) {
                            console.log(error);
                            CommonService.errorMessage("Unexpected error occured.");
                        }, function (data /*Notify event should handle here*/) {
                        };
                }, "Lifeline subsidy", item.IsActive);
            }

            //#endregion

            //#region Open Modal

            $scope.addOrUpdate = function (id) {
                $uibModal.open({
                    animation: true,
                    ariaLabelledBy: 'modal-title',
                    ariaDescribedBy: 'modal-body',
                    templateUrl: 'AddOrUpdateModal.html',
                    controller: 'LifelineSubsidyFormController',
                    controllerAs: 'controller',
                    size: 'sm',
                    keyboard: false,
                    backdrop: "static",
                    windowClass: 'modal_style',
                    resolve: {
                        LifelineSubsidyId: function () {
                            return id;
                        }
                    }

                }).result.then(function (data) {
                    $scope.reset();
                },
                    function () {

                    });
            };

            //#endregion
        }
    ]
)
angular.module('MetronicApp').controller('LifelineSubsidyFormController',
    ['$scope', 'LifelineSubsidyService', '$uibModalInstance', 'CommonService', 'LifelineSubsidyId',
        function ($scope, LifelineSubsidyService, $uibModalInstance, CommonService, LifelineSubsidyId) {

            //#region Variables
            $scope.formSubmitted = false;

            $scope.data = {
                LifelineSubsidyId: LifelineSubsidyId,
                Code: "",
                Minimum: null,
                Maximum: null,
                Discount: null
            }

            //#endregion

            //#region Initialization

            this.$onInit = function () {

                if (LifelineSubsidyId != 0) {
                    $scope.isUpdate = true;

                    LifelineSubsidyService.GetDetails({ id: LifelineSubsidyId }).then(function (data) {
                        $scope.data = data.DataResult;
                    }, function (error /*Error event should handle here*/) {
                        console.log("Error");
                    }, function (data /*Notify event should handle here*/) {
                    });
                }
                else {
                    $scope.isUpdate = false;
                }
            }

            //#endregion

            //#region Save  Lifeline Subsidy

            $scope.saveData = function () {
                $scope.formSubmitted = true;
                if ($scope.addOrUpdateForm.$valid) {
                    CommonService.saveOrUpdateChanges(function () {
                        LifelineSubsidyService.SaveData({ model: $scope.data }).then(function (data) {
                            if (data.DataResult.Success) {
                                CommonService.successMessage(data.DataResult.Message);
                                $uibModalInstance.close();
                            } else {
                                CommonService.warningMessage(data.DataResult.Message);
                            }
                        }), function (error /*Error event should handle here*/) {
                            console.log(error);
                            CommonService.errorMessage("Unexpected error occured.");
                        }, function (data /*Notify event should handle here*/) {
                        }
                    }, LifelineSubsidyId)
                }
            }

            //#endregion

            //#region Cancel Changes

            $scope.cancel = function () {

                if ($scope.addOrUpdateForm.$dirty) {
                    CommonService.cancelChanges(function () {
                        $uibModalInstance.dismiss('cancel');
                    });
                } else {
                    $uibModalInstance.dismiss('cancel');
                }
            }

            //#endregion

            $scope.roundOff = function (data) {
                if (data != null || data != undefined) {
                    var decimalValue = parseFloat(data);
                    $scope.data.Discount = decimalValue.toFixed(2);
                }
            }
        }
    ]
);