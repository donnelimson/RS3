MetronicApp.controller('RegionSaveModalController', ['$scope', '$uibModalInstance', 'RegionService', 'CommonService', 'RegionId',
    function ($scope, $uibModalInstance, RegionService, CommonService, RegionId) {

        //#region Other defaults

        $scope.formSubmitted = false;
        $scope.region = {
            Name: "",
            Abbreviation: ""
        }
        var regionDetails = angular.copy($scope.region);

        //#endregion

        //#region Initialization

        this.$onInit = function () {

            if (RegionId == null || RegionId == 0) {
                $scope.addOrEdit = "Add";
                $scope.isEditing = false;

            } else {
                $scope.addOrEdit = "Edit";
                $scope.isEditing = true;

                RegionService.GetEditRegions({
                    regionId: RegionId
                }).then(function (data) {
                    $scope.region = data.data;
                    regionDetails = data.data;
                },
                    function (error /*Error event should handle here*/) {
                        console.log("Error");
                    },
                    function (data /*Notify event should handle here*/) {
                    });
            }
        }
        //#region Save Region

        $scope.saveRegion = function () {
            $scope.formSubmitted = true;
            if ($scope.saveRegionForm.$valid) {
                if (RegionId == 0 || RegionId == null) {
                    CommonService.saveChanges(function () {
                        RegionService.AddRegion({
                            model: $scope.region
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
                } else {
                    CommonService.updateChanges(function () {
                        RegionService.UpdateRegion({
                            model: $scope.region
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

        //#region Cancel Changes

        $scope.cancel = function (data) {
            if (!$scope.saveRegionForm.$dirty) {
                $uibModalInstance.dismiss('cancel');
            } else {
                CommonService.cancelChanges(function () {
                    $uibModalInstance.dismiss('cancel');
                });
            }
        }

        //#endregion

    }]);