MetronicApp.controller('SurchargeSaveModalController', ['$scope', '$uibModalInstance', 'NgTableParams', 'SurchargeService', 'CommonService', 'SurchargeId', '$timeout',
    function ($scope, $uibModalInstance, NgTableParams, SurchargeService, CommonService, SurchargeId, $timeout) {


        //#region Set Variables
        $scope.formSubmitted = false;
            $scope.surcharge = {
                SurchargeId: null,
                ConsumerClassId: null,
                YearOfDelinquency: null,
                RatePerMonth: null
        };
        var SurchargeDetails = angular.copy($scope.surcharge);
        //#endregion

        //Other defaults
        function init() {
            GetConsumerClasses();
            if (SurchargeId == null || SurchargeId == 0) {
                GetConsumerClasses();
                $scope.addOrEdit = "Add";
            } else {
                GetConsumerClasses();
                $scope.addOrEdit = "Edit";

                SurchargeService.GetEditSurcharge({
                    surchargeId: SurchargeId
                }).then(function (data) {
                    var data = data.data;
                    $scope.surcharge.SurchargeId = data.SurchargeId;
                    $scope.surcharge.ConsumerClassId = data.ConsumerClassId;
                    $scope.surcharge.YearOfDelinquency = data.YearOfDelinquency;
                    $scope.surcharge.RatePerMonth = data.RatePerMonth;
                    SurchargeDetails = data;
                }
                );
            }
        }

        init();

        //SAVE Surcharge
        $scope.saveSurcharge = function () {
            $scope.formSubmitted = true;
            if ($scope.saveSurchargeForm.$valid && $scope.surcharge.ConsumerClassId !== null) {
                if (SurchargeId == 0 || SurchargeId == null) {
                    CommonService.saveChanges(function () {
                        SurchargeService.AddSurcharge({
                            model: $scope.surcharge
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
                        SurchargeService.UpdateSurcharge({
                            model: $scope.surcharge
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

        //CANCEL BUTTON
        $scope.cancel = function (data) {
            console.log($scope.surcharge);
            console.log(SurchargeDetails);
            if (angular.equals($scope.surcharge, SurchargeDetails) || $scope.surcharge.ConsumerClassId === 0) {
                $uibModalInstance.dismiss('cancel');

            } else {
                CommonService.cancelChanges(function () {
                    $uibModalInstance.dismiss('cancel');
                });
            }
        }

        //#region Local Functions
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