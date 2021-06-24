MetronicApp.controller('MeterReadingRemarksSaveModalController', ['$scope', '$uibModalInstance', 'NgTableParams', 'MeterReadingRemarksService', 'CommonService', 'MeterReadingRemarksId', '$timeout',
    function ($scope, $uibModalInstance, NgTableParams, MeterReadingRemarksService, CommonService, MeterReadingRemarksId, $timeout) {

        $scope.formSubmitted = false;

        $scope.meterReadingRemarks = {
            MeterReadingRemarksId: null,
            Code: "",
            Name: ""
        };
        var meterReadingRemarksDetails = angular.copy($scope.meterReadingRemarks);

        //Initialize
        function init() {
            if (MeterReadingRemarksId === null || MeterReadingRemarksId === 0) {
                $scope.addOrEdit = "Add";
            } else {
                $scope.addOrEdit = "Edit";
                MeterReadingRemarksService.GetDetailsForUpdate({
                    meterReadingRemarksId: MeterReadingRemarksId
                }).then(function (data) {
                    $scope.meterReadingRemarks = data.data;
                    $scope.meterReadingRemarks.MeterReadingRemarksId = data.data.MeterReadingRemarkId;
                    meterReadingRemarksDetails = angular.copy($scope.meterReadingRemarks);
                }
                );
            }
        }
        init();

        $scope.saveMeterReadingRemarks = function () {
            $scope.formSubmitted = true;
            if ($scope.saveMeterReadingRemarksForm.$valid) {
                if (MeterReadingRemarksId === 0 || MeterReadingRemarksId === null) {
                    CommonService.saveChanges(function () {
                        addUpdate();
                    });
                }
                else {
                    CommonService.updateChanges(function () {
                        addUpdate();
                    });
                }
            }
        };

        //CANCEL BUTTON
        $scope.cancel = function (data) {
            $scope.meterReadingRemarks.Code = $scope.meterReadingRemarks.Code != undefined ? $scope.meterReadingRemarks.Code : "";
            $scope.meterReadingRemarks.Name = $scope.meterReadingRemarks.Name != undefined ? $scope.meterReadingRemarks.Name : "";

            if (angular.equals($scope.meterReadingRemarks, meterReadingRemarksDetails)) {
                $uibModalInstance.dismiss('cancel');

            } else {
                CommonService.cancelChanges(function () {
                    $uibModalInstance.dismiss('cancel');
                });
            }
        };

        function addUpdate() {
            MeterReadingRemarksService.AddUpdateMeterReadingRemarks({
                model: $scope.meterReadingRemarks
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
            };
        }
    }]);