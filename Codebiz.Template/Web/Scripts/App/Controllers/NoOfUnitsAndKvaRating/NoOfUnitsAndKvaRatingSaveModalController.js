MetronicApp.controller('NoOfUnitsAndKvaRatingSaveModalController', ['$scope', '$uibModalInstance', 'NoOfUnitsAndKvaRatingService', 'CommonService', 'NoOfUnitsKvaId', '$timeout',
    function ($scope, $uibModalInstance, NoOfUnitsAndKvaRatingService, CommonService, NoOfUnitsKvaId, $timeout) {

        $scope.formSubmitted = false;
        $scope.noOfUnitsAndKvaRating = {
            NoOfUnitsKvaId: null,
            NoOfUnits: null,
            KvaRating: null,
            EnergyDeposit: null,
            MonthlyRental: null,
            InatallationFee: null,
            TransformerLoss: null,
            DemandEnergy: null,
            CoreLoss: null
        };

        var noOfUnitsAndKvaRatingDetails = angular.copy($scope.noOfUnitsAndKvaRating);

        //Other defaults
        function init() {
            if (NoOfUnitsKvaId === null || NoOfUnitsKvaId === 0) {
                $scope.isUpdate = false;
            } else {
                $scope.isUpdate = true;

                NoOfUnitsAndKvaRatingService.GetEditNoOfUnitsAndKvaRating({
                    noOfUnitsKvaId: NoOfUnitsKvaId
                }).then(function (data) {
                    noOfUnitsAndKvaRatingDetails = data.data;
                    $scope.noOfUnitsAndKvaRating = data.data;
                    $timeout(function () {
                        $scope.addZero();
                        $scope.addZeroMonthlyRental();
                        $scope.addZeroInstallationFee();
                        $scope.addZeroTransformerLoss();
                        $scope.addZeroDemandEnergy();
                        $scope.addZeroCoreLoss();
                        $scope.addZeroKVA();
                    },100)
           
                }
                );
            }
       
        }
        init();

        $scope.saveNoOfUnitsAndKvaRating = function () {
            $scope.formSubmitted = true;
            if ($scope.saveNoOfUnitsAndKvaRatingForm.$valid && !$scope.saveNoOfUnitsAndKvaRatingForm.KvaRating.$error.pattern) {
                if (NoOfUnitsKvaId === 0 || NoOfUnitsKvaId === null) {
                    CommonService.saveChanges(function () {
                        addOrUpdate();
                    });

                } else {
                    CommonService.updateChanges(function () {
                        addOrUpdate();
                    });
                }
            }
        };


        $scope.kvaRatingChange = function () {
            if ($scope.noOfUnitsAndKvaRating.KvaRating > 100) {
                $scope.saveNoOfUnitsAndKvaRatingForm.KvaRating.$error.pattern = true;
            }
            else {
                $scope.saveNoOfUnitsAndKvaRatingForm.KvaRating.$error.pattern = false;
            }
        };

        //CANCEL BUTTON
        $scope.cancel = function (data) {
            if (!$scope.saveNoOfUnitsAndKvaRatingForm.$dirty) {
                $uibModalInstance.dismiss('cancel');

            } else {
                CommonService.cancelChanges(function () {
                    $uibModalInstance.dismiss('cancel');
                });
            }
        };

        $scope.addZero = function () {
            var textField = document.getElementById('txtAmountTransfer');
            textField.value = parseFloat(textField.value).toFixed(2) == "NaN" ? "" : parseFloat(textField.value).toFixed(2);
        };

        $scope.addZeroMonthlyRental = function () {
            var textField = document.getElementById('txtMonthlyRental');
            textField.value = parseFloat(textField.value).toFixed(2) == "NaN" ? "" : parseFloat(textField.value).toFixed(2);
        };

        $scope.addZeroInstallationFee = function () {
            var textField = document.getElementById('txtInstallationFee');
            textField.value = parseFloat(textField.value).toFixed(2) == "NaN" ? "" : parseFloat(textField.value).toFixed(2);
        };
        $scope.addZeroTransformerLoss = function () {
            var textField = document.getElementById('txtTransformerLoss');
            textField.value = parseFloat(textField.value).toFixed(2) == "NaN" ? "" : parseFloat(textField.value).toFixed(2);
        };
        $scope.addZeroDemandEnergy = function () {
            var textField = document.getElementById('txtDemandEnergy');
            textField.value = parseFloat(textField.value).toFixed(2) == "NaN" ? "" : parseFloat(textField.value).toFixed(2);
        };
        $scope.addZeroCoreLoss = function () {
            var textField = document.getElementById('txtCoreLoss');
            textField.value = parseFloat(textField.value).toFixed(2) == "NaN" ? "" : parseFloat(textField.value).toFixed(2);
        };
        $scope.addZeroKVA = function () {
            var textField = document.getElementById('kva');
            textField.value = parseFloat(textField.value).toFixed(2) == "NaN" ? "" : parseFloat(textField.value).toFixed(2);
        };

        function addOrUpdate() {
            NoOfUnitsAndKvaRatingService.AddUpdateNoOfUnitsAndKvaRating({
                model: $scope.noOfUnitsAndKvaRating
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