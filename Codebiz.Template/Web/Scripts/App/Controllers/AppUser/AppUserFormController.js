angular.module('MetronicApp')
    .controller('AppUserFormController',
        ['$scope', 'AppUserService', '$uibModal', '$window', 'CommonService','$timeout','blockUI',
            function ($scope, AppUserService, $uibModal, $window, $cs,$timeout,blockUI) {

                var appUserId = angular.element("#AppUserId").val() == '' ? 0 : parseInt(angular.element("#AppUserId").val());
                $scope.forUpdate = appUserId > 0;

                $scope.m = {
                    AppUserId: appUserId,
                    EmployeeId: null,
                    EmployeeNo: "",
                    Username: "",
                    IsActive: true,
                    SelectedUserGroups: [],
                    RoleId: null,
                    FirstName: "",
                    MiddleName: "",
                    LastName: "",
                    Email:""
                }

                $scope.employeeDetails = null;
                $scope.userGroups = [];

                //#region Variable Defaults

                this.$onInit = function () {
           
                    AppUserService.GetAllUserGroup({}).then(function (data) {
                        $scope.userGroups = data.data;
                        $cs.GetRolesForSelect2LookUp().then(function (d) {
                            $scope.roles = d.result;
                            if ($scope.forUpdate) {
                                GetAppUsersDetails();
                            }
                        });
                    });
                  
                 
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

    

                $scope.btnSave = function () {
                    $scope.f.$valid = true;
                    $scope.formSubmitted = true;
               

                    if (angular.element('.ng-invalid').length) {
                        angular.element('.ng-invalid')[1].focus();
                        $scope.f.$valid = false
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
                        //    console.log(data.data)
                            $scope.m = data.data;
                            //console.log($scope.m)
                            //$scope.searchAndValidateEmployee($scope.m.EmployeeNo, false);

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

               

                //#endregion

            }]);