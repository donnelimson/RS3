angular.module('MetronicApp')
    .controller('UsersController',
        ['$scope', 'UserService', 'NgTableParams', '$window', 'CommonService', '$q',
            function ($scope, UserService, NgTableParams, $window, CommonService, $q) {

                //#region Variable Defaults

                $scope.appUserStatuses = [];

                //#endregion

                //#region Init

                this.$onInit = function () {
                    getAppUserStatuses();
                    $scope.reset();
                };

                //#endregion

                //#region Scope functions

                $scope.exportDataToExcelFile = function () {
                    if ($scope.resultsLength > 0) {
                        CommonService.showLoading();
                        UserService.ExportDataToExcelFile({
                            Name: $scope.name,
                            Username: $scope.username,
                            Email: $scope.email,
                            Position: $scope.position,
                            Office: $scope.office,
                            Department: $scope.department,
                            AppUserStatus: $scope.appUserStatus,
                            CreatedBy: $scope.createdBy,
                            SortDirection: $scope.sortOrder,
                            SortColumn: $scope.sortColumn,
                            CreatedOnFrom: getDateRangePickerValue(1, $scope.createdDate),
                            CreatedOnTo: getDateRangePickerValue(2, $scope.createdDate)
                        }).then(function (data) {
                            CommonService.hideLoading();
                            window.location.href = document.FileUpload + "DownloadExcelFile?fileName=" + data.data.FileName;

                        }, function (error /*Error event should handle here*/) {
                            console.log("Error");
                        }, function (data /*Notify event should handle here*/) {
                            console.log("Error");
                        });
                    }
                };

                $scope.search = function () {
                    var initialSettings = {
                        getData: function (params) {
                            for (var i in params.sorting()) {
                                $scope.sortColumn = i;
                                $scope.sortOrder = params.sorting()[i];
                            }

                            var d = $q.defer();

                            UserService.SearchAppUser({
                                Name: $scope.name,
                                Username: $scope.username,
                                Email: $scope.email,
                                Position: $scope.position,
                                Office: $scope.office,
                                Department: $scope.department,
                                AppUserStatus: $scope.appUserStatus,
                                CreatedBy: $scope.createdBy,
                                Page: params.page(),
                                PageSize: params.count(),
                                SortDirection: $scope.sortOrder,
                                SortColumn: $scope.sortColumn,
                                CreatedOnFrom: getDateRangePickerValue(1, $scope.createdDate),
                                CreatedOnTo: getDateRangePickerValue(2, $scope.createdDate)
                            }).then(function (data) {
                                $scope.resultsLength = data.totalRecordCount;
                                params.total($scope.resultsLength);
                                d.resolve(data.data);
                            });

                            return d.promise;
                        }
                    };

                    $scope.tableParams = new NgTableParams(10, initialSettings);
                };

                $scope.reset = function () {
                    $scope.name = "";
                    $scope.username = "";
                    $scope.email = "";
                    $scope.position = "";
                    $scope.office = "";
                    $scope.department = "";
                    $scope.appUserStatus = null;
                    $scope.createdBy = "";
                    $scope.createdDate = null;

                    $scope.sortOrder = "desc";
                    $scope.sortColumn = "CreatedDate";

                    $scope.search();
                };

                $scope.searchWhenEnter = function ($event) {
                    var keyCode = $event.which || $event.keyCode;
                    if (keyCode === 13) {
                        $scope.search();
                    }
                };

                $scope.addOrUpdate = function (id) {
                    if (id === 0) {
                        $window.location.href = document.AppUsers + "Create";
                    }
                    else {
                        $window.location.href = document.AppUsers + "Edit/" + id;
                    }
                };

                $scope.toggleActiveStatus = function (user) {
                    var selection = user.IsActive ? 2 : 1;

                    UserService.ReactivateDeactivateAppUser(function () {
                        UserService.ToggleAppUserActiveStatus({ id: user.AppUserId })
                            .then(function (data) {
                                CommonService.successMessage(data.message);
                                $scope.search();

                            }), function (error /*Error event should handle here*/) {
                                console.log(error);
                                CommonService.errorMessage("Unexpected error occured.");
                            }, function (data /*Notify event should handle here*/) {
                            };
                    }, user.Username, selection);
                };

                $scope.resendActivationLink = function (id) {
                    CommonService.confirmAction(function () {
                        UserService.ResendActivationLink({ id: id })
                            .then(function (data) {
                                CommonService.successMessage(data.message);
                                $scope.search();

                            }), function (error /*Error event should handle here*/) {
                                console.log(error);
                                CommonService.errorMessage("Unexpected error occured.");
                            }, function (data /*Notify event should handle here*/) {
                            };
                    });
                };

                $scope.sendResetPasswordLink = function (id) {
                    CommonService.confirmAction(function () {
                        UserService.SendResetPasswordLink({ id: id })
                            .then(function (data) {
                                CommonService.successMessage(data.message);
                                $scope.search();

                            }), function (error /*Error event should handle here*/) {
                                console.log(error);
                                CommonService.errorMessage("Unexpected error occured.");
                            }, function (data /*Notify event should handle here*/) {
                            };
                    });
                };

                $scope.sendUnlockingAccountLink = function (id) {
                    CommonService.confirmAction(function () {
                        UserService.SendUnlockingAccountLink({ id: id })
                            .then(function (data) {
                                CommonService.successMessage(data.message);
                                $scope.search();

                            }), function (error /*Error event should handle here*/) {
                                console.log(error);
                                CommonService.errorMessage("Unexpected error occured.");
                            }, function (data /*Notify event should handle here*/) {
                            };
                    });
                };

                //#endregion

                //#region Non-scope functions

                function getAppUserStatuses() {
                    UserService.GetAppUserStatuses({
                    }, "StatusId").then(function (data) {
                        $scope.appUserStatuses = data.data;
                    }, function (error /*Error event should handle here*/) {
                        console.log("Error");
                    }, function (data /*Notify event should handle here*/) {
                    });
                }

                //#endregion

            }]);