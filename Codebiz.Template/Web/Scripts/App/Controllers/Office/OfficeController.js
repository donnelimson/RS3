angular.module('MetronicApp')

    .controller('OfficeController',
        ['$scope', 'OfficeService', 'NgTableParams', '$timeout', '$window', '$q', 'CommonService',
            function ($scope, OfficeService, NgTableParams, $timeout, $window, $q, CommonService) {

                //Other defaults
                $scope.createdOnDatePicker = {
                    opened: false
                };

                //Init
                this.$onInit = function () {
                    $scope.reset();
                }

                //Export Office Data to exel 
                $scope.exportDataToExcelFile = function () {
                    if ($scope.resultsLength > 0) {
                        CommonService.showLoading();

                        OfficeService.ExportDataToExcelFile({
                            Code: $scope.Code,
                            Name: $scope.Name,
                            Address: $scope.Address,
                            IsActive: $scope.IsActive,
                            CreatedOnFrom: getDateRangePickerValue(1, $scope.createdDate),
                            CreatedOnTo: getDateRangePickerValue(2, $scope.createdDate),
                            SortDirection: $scope.sortOrder,
                            SortColumn: $scope.sortColumn
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

                //Search Office

                $scope.search = function () {
                    var initialSettings = {
                        getData: function (params) {
                            for (var i in params.sorting()) {
                                $scope.sortColumn = i;
                                $scope.sortOrder = params.sorting()[i];
                            }
                            var d = $q.defer();
                            var filter = params.filter();

                            OfficeService.GetOffices({
                                Page: params.page(),
                                PageSize: params.count(),
                                SortDirection: $scope.sortOrder,
                                SortColumn: $scope.sortColumn,
                                Code: $scope.Code,
                                Name: $scope.Name,
                                Address: $scope.Address,
                                IsActive: $scope.IsActive,
                                CreatedBy: $scope.CreatedBy,
                                CreatedOnFrom: getDateRangePickerValue(1, $scope.createdDate),
                                CreatedOnTo: getDateRangePickerValue(2, $scope.createdDate),
                            }).then(function (data) {
                                $scope.resultsLength = data.totalRecordCount;
                                params.total($scope.resultsLength);
                                d.resolve(data.data);
                            });
                            return d.promise;
                        }
                    };

                    $scope.tableParams = new NgTableParams(10, initialSettings);

                }

                //Reset filter fields
                $scope.reset = function () {
                    $scope.Code = "";
                    $scope.Name = "";
                    $scope.Address = "";
                    $scope.CreatedBy = "";
                    $scope.createdDate = null;

                    $scope.sortColumn = "CreatedDate";
                    $scope.sortOrder = "desc";
                    $scope.search();

                }

                $scope.toggleActiveStatus = function (item, isActive) {
                    if (!item.IsActive) {
                        CommonService.reActivate(function () {
                            OfficeService.ToggleOfficeActiveStatus({ id: item.OfficeId, isActive: isActive, name: item.Name })
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
                                }
                        },
                            item.Name)
                    } else {
                        CommonService.deActivate(function () {
                            OfficeService.ToggleOfficeActiveStatus({ id: item.OfficeId, isActive: isActive, name: item.Name })
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
                                }
                        },
                            item.Name)
                    }
                }

                $scope.deleteOffice = function (item) {
                    CommonService.deleteChanges(function () {
                        OfficeService.officeDelete({ id: item.OfficeId, name: item.Name }).then(function (data) {
                            if (data.result) {
                                CommonService.successMessage(data.message);
                                $scope.search(true);
                            } else {
                                CommonService.warningMessage(data.message);
                            }
                        })
                    },item.Name)
                }

                $scope.addOrUpdate = function (id) {
                    CommonService.showLoading();
                    $timeout(function () {
                        if (id === 0) {
                            $window.location.href = document.Office + "Create";
                        }
                        else {
                            $window.location.href = document.Office + "Edit/" + id;
                        }
                    }, 500);
                };

                $scope.viewOfficeDetails = function (id) {
                    $window.location.href = document.Office + "ViewOfficeDetails/" + id;
                }

                $scope.openCreatedOnDatePicker = function () {
                    $scope.createdOnDatePicker.opened = true;
                }

                $scope.searchWhenEnter = function ($event) {
                    var keyCode = $event.which || $event.keyCode;
                    if (keyCode === 13) {
                        $scope.search();
                    }
                }

                // #endregion

            }]);