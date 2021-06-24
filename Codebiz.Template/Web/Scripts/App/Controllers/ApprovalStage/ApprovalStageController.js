angular.module('MetronicApp')
    .controller('ApprovalStageController',
        ['$scope', 'ApprovalStageService', 'NgTableParams', '$uibModal', '$window', 'CommonService', '$timeout', '$q',
            function ($scope, ApprovalStageService, NgTableParams, $uibModal, $window, CommonService, $timeout, $q) {

                //#region Variable Defaults

                $scope.approvalStages = {};
                $scope.createdOnDatePicker = {
                    opened: false
                };

                //#endregion

                //#region Init

                this.$onInit = function () {
                    $scope.reset();
                };

                //#endregion

                //#region Scope functions

                $scope.exportDataToExcelFile = function () {
                    if ($scope.resultsLength > 0) {
                        CommonService.showLoading();
                        ApprovalStageService.ExportDataToExcelFile({
                            Name: $scope.name,
                            Description: $scope.description,
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

                //Search applicant
                $scope.search = function () {
                    var initialSettings = {
                        getData: function (params) {
                            for (var i in params.sorting()) {
                                $scope.sortColumn = i;
                                $scope.sortOrder = params.sorting()[i];
                            }

                            var d = $q.defer();

                            ApprovalStageService.SearchApprovalStage({
                                Name: $scope.name,
                                Description: $scope.description,
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
                    $scope.description = "";
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
                    CommonService.showLoading();
                    $timeout(function () {
                        if (id === 0) {
                            $window.location.href = document.ApprovalStage + "Create";
                        }
                        else {
                            $window.location.href = document.ApprovalStage + "Edit/" + id;
                        }
                    }, 500);
                };

                $scope.viewDetails = function (id) {
                    $window.location.href = document.ApprovalStage + "View/" + id;
                };

                $scope.delete = function (id, name,canDel) {
                    if (canDel) {
                        ApprovalStageService.DeleteApprovalStage(function () {
                            ApprovalStageService.DeleteApprovalStageById({ id: id })
                                .then(function (data) {
                                    if (data.success) {
                                        CommonService.successMessage(data.message);
                                        $scope.search();
                                    }
                                    else {
                                        CommonService.warningMessage("Unable to delete, " + name + " is in use!");
                                    }
                                 

                                }), function (error /*Error event should handle here*/) {
                                    console.log(error);
                                    CommonService.errorMessage("Unexpected error occured.");
                                }, function (data /*Notify event should handle here*/) {
                                };
                        }, name);
                    }
                    else {
                        CommonService.warningMessage("Unable to delete, "+name+" is in use!");
                    }
                };

                $scope.toggleActiveStatus = function (item) {
                    if (!item.CanDelete && item.IsActive) {
                        CommonService.warningMessage("Unable to deactivate, " + item.Name + " is in use");
                    }
                    else {
                        CommonService.reactivateDeactivate(function () {
                            ApprovalStageService.ToggleApprovalStageActiveStatus({ id: item.ApprovalStageId })
                                .then(function (data) {
                                    if (data.success) {
                                        CommonService.successMessage(data.message);
                                        $scope.search();
                                    }
                                    else {
                                        CommonService.warningMessage("Unable to deactivate, " + item.Name + " is in use");
                                    }

                                }), function (error /*Error event should handle here*/) {
                                    console.log(error);
                                    CommonService.errorMessage("Unexpected error occured.");
                                }, function (data /*Notify event should handle here*/) {
                                };
                        }, item.Name, item.IsActive);
                    }
                   
                };

                //#endregion

                //#region Private functions

                //#endregion
            }]);