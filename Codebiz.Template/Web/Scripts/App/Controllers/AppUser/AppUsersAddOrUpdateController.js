angular.module('MetronicApp')
.controller('ApprovalStageAddOrUpdateController',
        ['$scope', 'AppUserService', '$window', 'CommonService', '$timeout', '$location',
function ($scope, AppUserService, $window, CommonService, $timeout, $location) {

    //#region Variable defaults

    $scope.appUser = {
        AppUserId: 0,
        FirstName: "",
        MiddleName: "",
        LastName: "",
        Suffix: "",
        Username: "",
        Email: "",
        SendActivationLink: false,
        PositionId: null,
        IsActive: false,
        UserGroupIds: []
    };

    var appUserDetails = angular.copy($scope.appUser);

    $scope.positions = [];
    $scope.offices = [];

    $scope.isUpdate = $location.absUrl().includes('Edit');
    $scope.formSubmitted = false;

    //#endregion

    //#region Init

    this.$onInit = function () {
        getPositions();
        getOffices();
        getAppUserId();
    };

    //#endregion

    //#region Scope functions

    $scope.save = function () {
        $scope.formSubmitted = true;

        if ($scope.appUserForm.$valid) {
            CommonService.saveChanges(function () {
                    AppUserService.AddUpdateAppUser({
                        model: $scope.appUser
                    }).then(function (data) {
                        if (data.success) {
                            CommonService.successMessage(data.message);
                            $timeout(function () {
                                $window.location.href = document.AppUsers;
                            }, 800);
                        }
                        else {
                            CommonService.warningMessage(data.message);
                        }

                    }), function (error /*Error event should handle here*/) {
                        console.log(error);
                        CommonService.errorMessage("Unexpected error occured.");
                    }, function (data /*Notify event should handle here*/) {
                    };
            });
        }
    };

    $scope.cancel = function () {
        if (angular.equals($scope.appUser, appUserDetails)) {
            $window.location.href = document.AppUsers;
        }
        else {
            CommonService.cancelChanges(function () {
                $window.location.href = document.AppUsers;
            });
        }
    };

    $scope.addUserGroup = function (id) {
        if (userGroup[id]) {
            $scope.appUser.push();
        }
    };

    //#endregion

    //#region Non-scope functions

    function getPositions() {
        CommonService.GetAllPositions({
        }).then(function (data) {
            $scope.positions = data.data;

        }, function (error /*Error event should handle here*/) {
            console.log("Error");
        }, function (data /*Notify event should handle here*/) {
        });
    }

    function getOffices() {
        CommonService.GetAllAreas({
        }).then(function (data) {
            $scope.offices = data.data;

        }, function (error /*Error event should handle here*/) {
            console.log("Error");
        }, function (data /*Notify event should handle here*/) {
        });
    }

    function getAppUserDetails() {
        AppUserService.GetAppUserDetailsById({
            id: $scope.appUser.AppUserId

        }).then(function (data) {
            $scope.appUser = data.data;
            appUserDetails = angular.copy();

        }, function (error /*Error event should handle here*/) {
            console.log("Error");
        }, function (data /*Notify event should handle here*/) {
        });
    }

    function getAppUserId() {
        if ($scope.isUpdate) {
            var locationUrl = $location.url().replace(/\//g, ".").split(".");
            $scope.appUser.ApprovalStageId = locationUrl[locationUrl.length - 1];
            getAppUserDetails();
        }
    }

    //#endregion

}]);