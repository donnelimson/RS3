angular.module('MetronicApp')
    .controller('SaveFormController', ['$scope', '$uibModalInstance', 'EmployeeService', 'CommonService', '$filter', '$uibModal', 'EmployeeId','$timeout',
        function ($scope, $uibModalInstance, EmployeeService, CommonService, $filter, $uibModal, EmployeeId, $timeout) {

            //#region Variables

            $scope.formSubmitted = false;
            $scope.isEditing = false;

            $scope.data = {
                EmployeeId: null,
                EmployeeNo: "",
                LicenseNo: "",
                Restriction: ""
            }

            $scope.DriversInformation = [];
            $scope.employeeDetails = [];
            $scope.restrictions = [];

            //#endregion

            //#region Initialization

            this.$onInit = function () {

                $timeout(function () {
                    $scope.f.$dirty = false;
                })

                if (EmployeeId > 0) {
                    $scope.isEditing = true;

                    EmployeeService.GetDetails({ employeeId: EmployeeId }).then(function (data) {
                        $scope.data = data.data;
                        $('#ExpiryDate').datepicker("setDate", $scope.data.ExpiryDate);
                        $scope.data.ExpiryDate = formatDate($scope.data.ExpiryDate);
                        $scope.restrictions = $scope.data.Restriction.split(',');
                        $scope.employeeDetails.PositionName = $scope.data.PositionName;
                        $scope.employeeDetails.FullName = $scope.data.FullName;

                        $scope.f.$dirty = false;

                    }, function (error /*Error event should handle here*/) {
                        console.log("Error");
                    }, function (data /*Notify event should handle here*/) {
                    });

                }
            }

            //#endregion

            //#region Modal Search Employee

            $scope.searchEmployee = function (fromValidation, code) {
                var modalData = {
                    LookupType: 'EMPWOL',
                    SearchText: fromValidation ? code : ''
                }
                $uibModal.open({
                    animation: true,
                    ariaLabelledBy: 'modal-title',
                    ariaDescribedBy: 'modal-body',
                    templateUrl: `${document.baseUrlNoArea}Lookups/ChooseFromList/GetLookup?objType=${modalData.LookupType}`,
                    controller: 'ChooseFromListCtrl',
                    size: 'md',
                    keyboard: true,
                    backdrop: "static",
                    windowClass: 'modal_dialog',
                    modalOverflow: true,
                    resolve: {
                        modalData: function () {
                            return modalData;
                        },
                    }
                }).result.then(function (data) {

                    $scope.searchAndValidateEmployee(data.EmployeeNo);

                    $scope.data.EmployeeNo = data.EmployeeNo;
                    $scope.data.EmployeeId = data.EmployeeId;
                    $scope.employeeDetails = data;

                    $scope.f.$dirty = false;
                    $scope.invalidEmployee = false;

                }, function () {

                });
            };

            $scope.searchEmployeeByNo = function (value, $event) {
                $scope.employeeDetails = null;
                $scope.invalidEmployee = false;
                var keyCode = $event.which || $event.keyCode;
                if (keyCode === 13 || keyCode === 9) {
                    $scope.searchAndValidateEmployee(value);
                }
            }

            $scope.searchAndValidateEmployee = function (value) {
                if ($scope.employeeDetails == null && !$scope.invalidEmployee) {
                    $scope.f.$valid = false;
                    EmployeeService.GetEmployeeByCode({
                        code: value,
                        forLicense: true
                    }).then(data => {
                        if (data.data != null) {

                            $scope.employeeDetails = data.data;

                            if (data.data.LicenseNo != null) {
                                $scope.invalidEmployee = true;
                                $scope.invalidMessage = "Employee already has a license no."
                                $scope.employeeDetails = [];
                            }
                        }
                        else {
                            $scope.invalidEmployee = true;
                            $scope.invalidMessage = "Employee doesn't exist."
                            $scope.searchEmployee(true, value);
                        }
                    });
                }
            }

            //#endregion

            $scope.setRestriction = function () {
                var data = $scope.restrictions;

                $uibModal.open({
                    animation: true,
                    ariaLabelledBy: 'modal-title',
                    ariaDescribedBy: 'modal-body',
                    templateUrl: 'Restriction.html',
                    controller: 'RestrictionController',
                    size: 'm',
                    keyboard: false,
                    backdrop: "static",
                    windowClass: 'modal_dialog',
                    modalOverflow: true,
                    resolve: {
                        restrictions: function () {
                            return data;
                        }
                    }
                }).result.then(function (data) {

                    $scope.restrictions = data;
                    $scope.data.Restriction = data.toString();
                    $scope.f.$dirty = true;
                }, function (error /*Error event should handle here*/) {
                    console.log("Error.");
                }, function (data /*Notify event should handle here*/) {
                    console.log("Error");
                });
            };

            //#region Save

            $scope.saveData = function () {
                $scope.formSubmitted = true;
                if ($scope.f.$valid && !$scope.expiredLicense && !$scope.invalidCalendar && !$scope.invalidEmployee) {
                    CommonService.saveOrUpdateChanges(function () {
                        EmployeeService.SaveEmployeeLicenseData({ model: $scope.data }).then(function (data) {
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
                    }, EmployeeId)
                }
            }

            //#endregion

            //#region Cancel Changes

            $scope.cancel = function () {
                if ($scope.f.$dirty) {
                    CommonService.cancelChanges(function () {
                        $uibModalInstance.dismiss('cancel');
                    });
                } else {
                    $uibModalInstance.dismiss('cancel');
                }
            }

            //#endregion

            //#region validations

            $scope.checkPattern = function (data) {
                $scope.expiredLicense = false;
                $scope.invalidCalendar = false;
                $scope.exceedMaxDate = false;
               
                $scope.date = $filter('date')(new Date(), "dd/MM/yyyy").substring(6, 11);

                $scope.year = $filter('date')(new Date(), "dd/MM/yyyy").substring(6, 11) - 1;
                $scope.month = $filter('date')(new Date(), "dd/MM/yyyy").substring(3, 5);
                $scope.day = $filter('date')(new Date(), "dd/MM/yyyy").substring(0, 2);

                if (data != undefined) {
                    $scope.f.$dirty = true;
                    $scope.dataValue = data;

                    if ($scope.dataValue.length = 10) {
                        var year = data.substring(6, 11);
                        var month = data.substring(0, 2);
                        var day = data.substring(3, 5);

                        var dataYear = year + month + day;
                        var currentDate = $scope.date + $scope.month + $scope.day;

                        if (month > 12 || day > 31 || year.length < 4) {
                            $scope.invalidCalendar = true;
                        }

                        if (!$scope.invalidCalendar) {

                            var validDate = dataYear >= currentDate;

                            if (!validDate) {
                                $scope.expiredLicense = true;
                            }
                            if (year > ($scope.year + parseInt(6))) {
                                $scope.exceedMaxDate = true;
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
    ]);