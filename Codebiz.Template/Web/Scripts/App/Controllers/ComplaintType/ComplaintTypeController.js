angular.module('MetronicApp')

    .controller('ComplaintTypeController',
        ['$scope', 'ComplaintTypeService', 'NgTableParams', '$uibModal', '$q', 'CommonService', '$timeout', '$window',
            function ($scope, ComplaintTypeService, NgTableParams, $uibModal, $q, CommonService, $timeout, $window) {

                $scope.createdOnDatePicker = {
                    opened: false
                };
                //Init
                this.$onInit = function () {
                    $scope.reset();
                };

                //#region Export to Excel
                $scope.exportDataToExcelFile = function () {
                    if ($scope.resultsLength > 0) {
                        CommonService.showLoading();
                        ComplaintTypeService.ExportDataToExcelFile({
                            SortDirection: $scope.sortOrder,
                            SortColumn: $scope.sortColumn,
                            ComplaintType: $scope.ComplaintType,
                            ComplaintSubType: $scope.ComplaintSubType,
                            CreatedBy: $scope.CreatedBy,
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
                //#endregion

                //#region View
                $scope.search = function () {
                    var initialSettings = {
                        getData: function (params) {
                            for (var i in params.sorting()) {
                                $scope.sortColumn = i;
                                $scope.sortOrder = params.sorting()[i];
                            }

                            var d = $q.defer();

                            ComplaintTypeService.SearchComplaintType({
                                Page: params.page(),
                                PageSize: params.count(),
                                SortDirection: $scope.sortOrder,
                                SortColumn: $scope.sortColumn,
                                ComplaintType: $scope.ComplaintType,
                                ComplaintSubType: $scope.ComplaintSubType,
                                CreatedBy: $scope.CreatedBy,
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

                //Reset filter fields
                $scope.reset = function () {
                    $scope.ComplaintType = "";
                    $scope.ComplaintSubType = "";
                    $scope.CreatedBy = "";
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

                //#endregion

                //#region Add/Update
                $scope.addOrUpdateComplaintType = function (id) {
                    CommonService.showLoading();
                    $timeout(function () {
                        if (id === 0) {
                            $window.location.href = document.ComplaintType + "Create";
                        }
                        else {
                            $window.location.href = document.ComplaintType + "Edit/" + id;
                        }
                    }, 500);
                };
                //#endregion

                //#region Delete
                $scope.DeleteComplaintType = function (complaintTypeId,name) {
                    CommonService.deleteChanges(function () {
                        ComplaintTypeService.DeleteComplaintType({ id: complaintTypeId }).then(function (result) {
                            if (result.success) {
                                CommonService.successMessage(result.message);
                                $scope.search();
                            }
                            else {
                                CommonService.warningMessage(result.message);
                            }
                        });
                    },name);
                };
                //#endregion

                //#region Activate/Deactivate
                $scope.toggleActiveStatus = function (id, isActive,name) {
                    if (isActive) {
                        CommonService.reActivate(function () {
                            ComplaintTypeService.ToggleComplaintTypeActiveStatus({ Id: id, IsActive: isActive })
                                .then(function (data) {
                                    CommonService.successMessage(data.message);
                                    $scope.search();

                                }), function (error /*Error event should handle here*/) {
                                    console.log(error);
                                    CommonService.errorMessage("Unexpected error occured.");
                                }, function (data /*Notify event should handle here*/) {
                                };
                        },name);
                    } else {
                        CommonService.deActivate(function () {
                            ComplaintTypeService.ToggleComplaintTypeActiveStatus({ Id: id, IsActive: isActive })
                                .then(function (data) {
                                    CommonService.successMessage(data.message);
                                    $scope.search();

                                }), function (error /*Error event should handle here*/) {
                                    console.log(error);
                                    CommonService.errorMessage("Unexpected error occured.");
                                }, function (data /*Notify event should handle here*/) {
                                };
                        },name);
                    }
                };
                //#endregion
            }]);