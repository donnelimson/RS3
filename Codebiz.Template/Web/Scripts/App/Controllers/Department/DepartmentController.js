angular.module('MetronicApp')

    .controller('DepartmentController',
        ['$scope', 'DepartmentService', 'NgTableParams', '$q', 'CommonService', '$window', '$timeout',
            function ($scope, DepartmentService, NgTableParams, $q, CommonService, $window, $timeout) {

                //Other defaults
                $scope.createdOnDatePicker = {
                    opened: false
                };
                //Init
                this.$onInit = function () {
                    $scope.reset();
                };

                $scope.exportDataToExcelFile = function () {
                    if ($scope.resultsLength > 0) {
                        CommonService.showLoading();
                        DepartmentService.ExportDataToExcelFile({
                            SortDirection: $scope.sortOrder,
                            SortColumn: $scope.sortColumn,
                            Code: $scope.Code,
                            Name: $scope.Name,
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

                //#region View

                $scope.search = function () {
                    var initialSettings = {
                        getData: function (params) {
                            for (var i in params.sorting()) {
                                $scope.sortColumn = i;
                                $scope.sortOrder = params.sorting()[i];
                            }

                            var d = $q.defer();

                            DepartmentService.GetDepartment({
                                Page: params.page(),
                                PageSize: params.count(),
                                SortDirection: $scope.sortOrder,
                                SortColumn: $scope.sortColumn,
                                Code: $scope.Code,
                                Name: $scope.Name,
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
                    $scope.Code = "";
                    $scope.Name = "";
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

                //#region Activate/Deactivate

                $scope.toggleActiveStatus = function (id, isActive,name) {
                    if (isActive) {
                        CommonService.reActivate(function () {
                            DepartmentService.ToggleDepartmentActiveStatus({ Id: id, IsActive: isActive })
                                .then(function (data) {
                                    if (data.success) {
                                        CommonService.successMessage(data.message);
                                        $scope.search();
                                    }
                                    else {
                                        CommonService.warningMessage(data.message);
                                    }
                                }), function (error /*Error event should handle here*/) {
                                    console.log(error);
                                    CommonService.errorMessage("Unexpected error occured.");
                                }, function (data /*Notify event should handle here*/) {
                                };
                        },name);
                    } else {
                        CommonService.deActivate(function () {
                            DepartmentService.ToggleDepartmentActiveStatus({ Id: id, IsActive: isActive, Name: name })
                                .then(function (data) {
                                    if (data.success) {
                                        CommonService.successMessage(data.message);
                                        $scope.search();
                                    }
                                    else {
                                        CommonService.warningMessage(data.message);
                                    }
                                }), function (error /*Error event should handle here*/) {
                                    console.log(error);
                                    CommonService.errorMessage("Unexpected error occured.");
                                }, function (data /*Notify event should handle here*/) {
                                };
                        },name);
                    }
                };

                //#endregion

                //#region Delete

                $scope.deleteDepartment = function (departmentId,name) {
                    CommonService.deleteChanges(function () {
                        DepartmentService.departmentDelete({ id: departmentId, name: name }).then(function (data) {
                            if (data.success) {
                                CommonService.successMessage(data.message);
                                $scope.search();
                            }
                            else {
                                CommonService.warningMessage(data.message);
                            }
                        });
                    },name);
                };

                //#endregion

                //#region DatePicker

                $scope.openCreatedOnDatePicker = function () {
                    $scope.createdOnDatePicker.opened = true;
                };

                //#endregion


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
                            $window.location.href = document.Department + "Create";
                        }
                        else {
                            $window.location.href = document.Department + "Edit/" + id;
                        }
                    }, 500);
                };

                $scope.viewOfficeDetails = function (id) {
                    $window.location.href = document.Department + "ViewDepartmentDetails/" + id;
                }

            }]);