MetronicApp.controller('UnitsSaveModalController', ['$scope', '$uibModalInstance', 'NgTableParams', 'BillingUnitService', 'CommonService', 'UnitsId', '$timeout',
    function ($scope, $uibModalInstance, NgTableParams, BillingUnitService, CommonService, UnitsId, $timeout) {

        $scope.formSubmitted = false;
        $scope.units = {
            BillingUnitId: null,
            Unit: "",
            OfficeId: null,
            MeterReaderId: null
        };
        var unitDetails = angular.copy($scope.units);
        //#region init Functions
        function init() {
            CommonService.GetOfficesLookUp({
            }).then(function (data) {
                $scope.offices = data.data;
                if (UnitsId === null || UnitsId === 0) {
                    $scope.addOrEdit = "Add";
                } else {
                    $scope.addOrEdit = "Edit";
                    BillingUnitService.GetUnitsDetails({
                        unitId: UnitsId
                    }).then(function (data) {
                        $scope.units = data.data;
                        $scope.units.Units = data.data.Unit;
                        getMeterRead(data.data.MeterReaderId);
                        unitDetails = angular.copy($scope.units);
                    },
                        function (error /*Error event should handle here*/) {
                            console.log("Error");
                        },
                        function (data /*Notify event should handle here*/) {
                        });
                }


            },
                function (error /*Error event should handle here*/) {
                    console.log("Error");
                },
                function (data /*Notify event should handle here*/) {
                });

        }

        init();
        //#endregion

        //#region Save
        $scope.saveUnit = function () {
            $scope.formSubmitted = true;
            if ($scope.saveUnitsForm.$valid) {
                if (UnitsId === 0 || UnitsId === null) {
                    CommonService.saveChanges(function () {
                        BillingUnitService.AddUnits({
                            model: $scope.units
                        }).then(function (data) {
                            if (data.success) {
                                CommonService.successMessage(data.message);
                                $uibModalInstance.close();
                            } else {
                                CommonService.warningMessage(data.message);
                            }
                        }), function (error /*Error event should handle here*/) {
                            console.log(error);
                            CommonService.errorMessage("Unexpected error occured.");
                        }, function (data /*Notify event should handle here*/) {
                        }
                    });

                } else {
                    CommonService.updateChanges(function () {
                        BillingUnitService.UpdateUnits({
                            model: $scope.units
                        }).then(function (data) {
                            if (data.success) {
                                CommonService.successMessage(data.message);
                                $uibModalInstance.close();
                            } else {
                                CommonService.warningMessage(data.message);
                            }
                        }), function (error /*Error event should handle here*/) {
                            console.log(error);
                            CommonService.errorMessage("Unexpected error occured.");
                        }, function (data /*Notify event should handle here*/) {
                        }
                    })
                }
            }
        }

        //#endregion

        //#region getMeterReader
        $scope.getMeterReader = function () {
            console.log($scope.units.BillingUnitId);
            BillingUnitService.GetAllPositionsByOfficeId({
                officeId: $scope.units.OfficeId,
                unitsId: 0
            }).then(function (data) {
                $scope.positions = data.result;
              
            },
                function (error /*Error event should handle here*/) {
                    console.log("Error");
                },
                function (data /*Notify event should handle here*/) {
                });
        },

            //#endregion

            //#region CANCEL BUTTON
            $scope.cancel = function (data) {
                if (angular.equals($scope.units, unitDetails)) {
                    $uibModalInstance.dismiss('cancel');

                } else {
                    CommonService.cancelChanges(function () {
                        $uibModalInstance.dismiss('cancel');
                    });
                }
            }
        //#endregion

        //#region Get Meter Reader Function
        function getMeterRead(meterReaderId) {
            BillingUnitService.GetAllPositionsByOfficeId({
                officeId: $scope.units.OfficeId,
                unitsId: $scope.units.MeterReaderId
            }).then(function (data) {
                $scope.positions = data.result;
                $scope.units.MeterReaderId = meterReaderId;
            },
                function (error /*Error event should handle here*/) {
                    console.log("Error");
                },
                function (data /*Notify event should handle here*/) {
                });
        }
        //#endregion

    }]);