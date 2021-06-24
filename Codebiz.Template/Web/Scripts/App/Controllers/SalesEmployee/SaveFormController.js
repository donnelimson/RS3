angular.module('MetronicApp')
    .controller('SaveFormController', ['$scope', '$uibModalInstance', 'SalesEmployeeService', 'CommonService', 'SalesEmployeeId', '$uibModal',
        function ($scope, $uibModalInstance, SalesEmployeeService, CommonService, SalesEmployeeId, $uibModal) {


            //#region Variables
            $scope.formSubmitted = false;

            $scope.data = {
                SalesEmployeeId: SalesEmployeeId,
                EmployeeId: null,
                CommissionGroupId: null,
                Commission: null,
                Remarks: "",
                IsActive: false
            }

            $scope.employeeDetails = [];

            //#region Initialization

            this.$onInit = function () {
           
                if (SalesEmployeeId != 0) {
                    $scope.addOrEdit = "Edit";
                    $scope.addOrUpdate = "Update";
                    CommonService.showLoading();

                    SalesEmployeeService.GetDetails({ id: SalesEmployeeId }).then(function (data) {
                        $scope.data = data.DataResult;
                        $scope.employeeDetails = $scope.data.EmployeeDetails;
                        GetCommissionGroups($scope.data.CommissionGroupId);
                      
                    }, function (error /*Error event should handle here*/) {
                        console.log("Error");
                    }, function (data /*Notify event should handle here*/) {
                    });
                    CommonService.hideLoading();
                }
                else {
                    $scope.addOrEdit = "Add";
                    $scope.addOrUpdate = "Add";
                    GetCommissionGroups(null);
                }
            }

            //#endregion

            //#region Save 

            $scope.saveData = function () {
                console.log($scope.data.SalesEmployeeId);
                $scope.formSubmitted = true;
                if ($scope.saveForm.$valid) {
                    CommonService.saveOrUpdateChanges(function () {
                        SalesEmployeeService.SaveData({ model: $scope.data }).then(function (data) {
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
                    }, $scope.data.SalesEmployeeId)
                }
            }

            //#endregion

            //#region Cancel Changes

            $scope.cancel = function () {

                if ($scope.saveForm.$dirty) {
                    CommonService.cancelChanges(function () {
                        $uibModalInstance.dismiss('cancel');
                    });
                } else {
                    $uibModalInstance.dismiss('cancel');
                }
            }

            //#endregion

            //#region Modal Search App User

            $scope.searchAppUser = function (data) {
                $uibModal.open({
                    animation: true,
                    ariaLabelledBy: 'modal-title',
                    ariaDescribedBy: 'modal-body',
                    templateUrl: 'SearchEmployee.html',
                    controller: 'SearchEmployeeController',
                    size: 'lg',
                    keyboard: false,
                    backdrop: "static",
                    windowClass: 'modal_dialog',
                    resolve: {
                        Data: function () {
                            return {
                                AppUsers: data,
                                Title: "Employee",
                                IsDriver: false,
                                IsJO: false
                            }
                        }
                    }
                }).result.then(function (data) {
                    $scope.employeeDetails = data;
                    $scope.data.EmployeeId = $scope.employeeDetails.AppUserId;

                    $scope.saveForm.$dirty = true;
                }, function () {

                });
            };
            //#endregion

            $scope.addZero = function () {
                var textField = document.getElementById('com');
                if (textField.value != '') {
                    textField.value = parseFloat(textField.value).toFixed(2);
                }
            };

            //#region Private Function

            function GetCommissionGroups(id) {
                SalesEmployeeService.GetCommissionGroups({
                }, "CommissionGroupId").then(function (data) {
                    $scope.commissionGroups = data.data;
                    if (id != null) {
                        $scope.data.CommissionGroupId = id;
                        $scope.saveForm.$dirty = false;
                    }
                }, function (error /*Error event should handle here*/) {
                    console.log("Error");
                }, function (data /*Notify event should handle here*/) {
                });
            }

            //#endregion

            //#region Validations

            $scope.checkWhenChange = function (data, $event) {

                var keyCode = $event.which || $event.keyCode;
                if (keyCode === 13) {
                    $scope.checkPattern(data);
                }

                if (keyCode === 9) {
                    $scope.checkPattern(data);
                }
            }

            $scope.checkPattern = function (data) {
                $scope.expiredLicense = false;
                $scope.invalidCalendar = false;
                $scope.date = $filter('date')(new Date(), "dd/MM/yyyy").substring(6, 11);

                $scope.year = $filter('date')(new Date(), "dd/MM/yyyy").substring(6, 11);
                $scope.month = $filter('date')(new Date(), "dd/MM/yyyy").substring(3, 5);
                $scope.day = $filter('date')(new Date(), "dd/MM/yyyy").substring(0, 2);

                $scope.lastYear = $scope.month + "/" + $scope.day + "/" + $scope.year;

                if (data != undefined) {

                    $scope.dataValue = data;

                    if ($scope.dataValue.length = 10) {
                        var year = data.substring(6, 11);
                        var month = data.substring(0, 2);
                        var day = data.substring(3, 5);

                        var dataYear = year + month + day;
                        var lastYear = $scope.year + $scope.month + $scope.day;
                        var currentDate = $scope.date + $scope.month + $scope.day;

                        if (month > 12 || day > 31 || year.length < 4) {
                            $scope.invalidCalendar = true;
                        }

                        if (!$scope.invalidCalendar) {

                            var valid = dataYear >= lastYear;

                            if (!valid) {
                                $scope.expiredLicense = true;
                            }
                        }
                    }
                    else {
                        $scope.invalidCalendar = true;
                    }
                }
            }

            //#endregion
        }
    ]
);