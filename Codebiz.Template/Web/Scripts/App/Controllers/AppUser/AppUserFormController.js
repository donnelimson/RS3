angular.module('MetronicApp')
    .controller('AppUserFormController',
        ['$scope', 'AppUserService', '$uibModal', '$window', 'CommonService',
            function ($scope, AppUserService, $uibModal, $window, $cs) {

                var appUserId = angular.element("#AppUserId").val() == '' ? 0 : parseInt(angular.element("#AppUserId").val());
                $scope.forUpdate = appUserId > 0;

                $scope.m = {
                    AppUserId: appUserId,
                    EmployeeId: null,
                    EmployeeNo: "",
                    Username: "",
                    IsActive: true,
                    SelectedUserGroups: []
                }

                $scope.employeeDetails = null;
                $scope.userGroups = [];

                //#region Variable Defaults

                this.$onInit = function () {
                    GetAllUserGroup();
                };

                $scope.selectUserGroup = function (row) {
                    if (row.IsSelected) {
                        $scope.m.SelectedUserGroups.push(row.Id);
                    }
                    else {
                        var index = $scope.m.SelectedUserGroups.findIndex(r =>  r == row.Id);
                        if (index != - 1) {
                            $scope.m.SelectedUserGroups.splice(index, 1);
                        }
                    }

                    checkIfAllSelected();
                }

                $scope.selectAllUserGroup = function (value) {
                    $scope.m.SelectedUserGroups = [];
                    for (var i = 0; i < $scope.userGroups.length; i++) {
                        $scope.userGroups[i].IsSelected = false;
                        if (value) {
                            $scope.userGroups[i].IsSelected = true;
                            $scope.m.SelectedUserGroups.push($scope.userGroups[i].Id);
                        }
                    }
                }

                $scope.cancel = function () {
                    if (!$scope.f.$dirty) {
                        $window.location.href = document.AppUsers;
                    }
                    else {
                        $cs.cancelChanges(function () {
                            $window.location.href = document.AppUsers;
                        });
                    }
                };

                $scope.searchEmployee = function (fromValidation, code) {
                    var modalData = {
                        LookupType: 'EMP',
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

                        $scope.m.EmployeeNo = data.EmployeeNo;
                        $scope.m.EmployeeId = data.EmployeeId;

                        $scope.f.$dirty = true;

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
                    AppUserService.GetEmployeeByCode({ code: value }).then(data => {
                        if (data.data != null) {
                            $scope.employeeDetails = data.data;
                        }
                        else {
                            $scope.invalidEmployee = true;
                            $scope.searchEmployee(true, value);
                        }
                    });
                }

                $scope.btnSave = function () {
                    $scope.f.$valid = true;
                    $scope.formSubmitted = true;
                    $scope.searchAndValidateEmployee($scope.m.EmployeeNo);

                    if (angular.element('.ng-invalid').length) {
                        angular.element('.ng-invalid')[1].focus();
                        $scope.f.$valid = false
                    }
                    else if ($scope.invalidEmployee) {
                        $scope.f.$valid = false;
                    }

                    if ($scope.f.$valid) {
                        $cs.saveOrUpdateChanges(function () {
                            AppUserService.SaveAppUser({ model: $scope.m }).then(function (d) {
                                if (d.success) {
                                    $cs.successMessage(d.message);
                                    $window.location.href = document.AppUsers + "Index"; // back to list
                                }
                                else {
                                    $cs.warningMessage(d.message);
                                }
                            })
                        }, $scope.m.AppUserId)
                    }
                }

                //#region Private Functions

                function GetAppUsersDetails() {
                    AppUserService.GetAppUsersDetailsById({ appUserId: $scope.m.AppUserId })
                        .then(function (data) {

                            $scope.m = data.data;
                            $scope.searchAndValidateEmployee($scope.m.EmployeeNo, false);

                            for (var i = 0; i < $scope.m.SelectedUserGroups.length; i++) {
                                var index = $scope.userGroups.findIndex(r => r.Id == $scope.m.SelectedUserGroups[i]);
                                if (index != - 1) {
                                    $scope.userGroups[index].IsSelected = true;
                                }
                            }
                            checkIfAllSelected();

                        }, function (error /*Error event should handle here*/) {
                            console.log("Error");
                        }, function (data /*Notify event should handle here*/) {
                        });
                }

                function checkIfAllSelected() {
                    $scope.selectAll = $scope.userGroups.findIndex(r => !r.IsSelected) == -1;
                }

                function GetAllUserGroup() {
                    AppUserService.GetAllUserGroup({}).then(function (data) {
                        $scope.userGroups = data.data;

                        if ($scope.forUpdate) {
                            GetAppUsersDetails();
                        }
                    }, function (error /*Error event should handle here*/) {
                        console.log("Error");
                    }, function (data /*Notify event should handle here*/) {
                    });
                }

                //#endregion

            }]);