MetronicApp.controller('PositionSaveModalController', ['$scope', '$uibModalInstance', 'PositionService', 'CommonService', 'PositionId',
    function ($scope, $uibModalInstance, PositionService, CommonService, PositionId) {

        $scope.formSubmitted = false;
        $scope.position = {
            PositionId: null,
            Code: "",
            Name: "",
            IsManager: false,
            IsHeadOfficer: false
        }

        //Other defaults
        function init() {
                if (PositionId == null || PositionId == 0) {
                    $scope.addOrEdit = "Add";
                    $scope.saveOrUpdate = "Save";

                } else {
                    $scope.addOrEdit = "Edit";
                    $scope.saveOrUpdate = "Update";

                    PositionService.GetEditPosition({
                        positionId: PositionId
                    }).then(function (data) {
                        $scope.position = data.data;
                    },
                        function (error /*Error event should handle here*/) {
                            console.log("Error");
                        },
                        function (data /*Notify event should handle here*/) {
                        });
                }
        }
        init();

        //SAVE POSITION
        $scope.savePosition = function () {
            $scope.formSubmitted = true;
            if ($scope.savePositionForm.$valid) {
                if (PositionId == 0 || PositionId == null) {
                    CommonService.saveChanges(function() {
                        PositionService.AddPosition({
                            model: $scope.position
                        }).then(function(data) {
                            if (data.success) {
                                CommonService.successMessage(data.message);
                                $uibModalInstance.close();
                            } else {
                                CommonService.warningMessage(data.message);
                            }
                        }), function(error /*Error event should handle here*/) {
                            console.log(error);
                            CommonService.errorMessage("Unexpected error occured.");
                        }, function(data /*Notify event should handle here*/) {
                        }
                    });

                } else {
                    CommonService.updateChanges(function() {
                        PositionService.UpdatePosition({
                            model: $scope.position
                        }).then(function(data) {
                            if (data.success) {
                                CommonService.successMessage(data.message);
                                $uibModalInstance.close();
                            } else {
                                CommonService.warningMessage(data.message);
                            }
                        }), function(error /*Error event should handle here*/) {
                            console.log(error);
                            CommonService.errorMessage("Unexpected error occured.");
                        }, function(data /*Notify event should handle here*/) {
                        }
                    })
                }
            }
        }

        $scope.forManager = function (boolValue) {
            if (boolValue) {
                $scope.position.IsManager = boolValue;
                $scope.position.IsHeadOfficer = false;
            }
        }

        $scope.forHeadOfficer = function (boolValue) {
            if (boolValue) {
                $scope.position.IsHeadOfficer = boolValue;
                $scope.position.IsManager = false;
            }
        }

        //CANCEL BUTTON
        $scope.cancel = function (data) {
            if (!$scope.savePositionForm.$dirty) {
                $uibModalInstance.dismiss('cancel');
            } else {
                CommonService.cancelChanges(function () {
                    $uibModalInstance.dismiss('cancel');
                });
            }
        }
    }]);