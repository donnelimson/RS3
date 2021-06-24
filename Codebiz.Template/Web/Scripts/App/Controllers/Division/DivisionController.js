angular.module('MetronicApp')
    .controller('DivisionController',
        ['$scope', 'DivisionService', 'NgTableParams', '$uibModal', '$window', '$timeout', '$q', 'CommonService', '$location',
            function ($scope, DivisionService, NgTableParams, $uibModal, $window, $timeout, $q, CommonService, $location) {

                $scope.goToCreateOrEdit = function (id, isView) {
                    if (id !== 0 && !isView) {
                        $location.path("Edit/" + id);
                    }
                    else if (id !== 0 && isView) {
                        $location.path("View/" + id);
                    }
                    else {
                        $location.path("New");
                    }
                };
                $scope.Division = [];

                $scope.Categories = {};
                $scope.Positions = {};
                $scope.createdOnDatePicker = {
                    opened: false
                };
                //Init
                this.$onInit = function () {
                    $scope.reset();
                    getCategories();
                    positionLookUp();
                };

                //#region SearchExport
                $scope.exportDataToExcelFile = function () {
                    if ($scope.resultsLength > 0) {
                        if ($scope.sortColumn === "CreatedDate") {
                            $scope.sortColumn = "CreatedDate";
                        }

                        CommonService.showLoading();
                        DivisionService.ExportDataToExcelFile({
                            DivisionName: $scope.DivisionName,
                            DivisionCode: $scope.DivisionCode,
                            CategoryId: $scope.CategoryId,
                            PositionId: $scope.PositionId,
                            IsActive: $scope.IsActive,
                            SortDirection: $scope.sortOrder,
                            SortColumn: $scope.sortColumn,
                            CreatedOnFrom: getDateRangePickerValue(1, $scope.createdDate),
                            CreatedOnTo: getDateRangePickerValue(2, $scope.createdDate),
                            IsForExcelExport: true
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

                $scope.search = function (init) {
                    var initialSettings = {
                        getData: function (params) {
                            for (var i in params.sorting()) {
                                $scope.sortColumn = i;
                                $scope.sortOrder = params.sorting()[i];
                            }
                            var d = $q.defer();
                            var filter = params.filter();

                            DivisionService.SearchDivision({
                                Page: params.page(),
                                PageSize: params.count(),
                                SortOrder: $scope.sortOrder,
                                SortColumn: $scope.sortColumn,
                                divisionCode: $scope.DivisionCode,
                                divisionName: $scope.DivisionName,
                                categoryId: $scope.CategoryId,
                                positionId: $scope.PositionId,
                                createdOnFrom: getDateRangePickerValue(1, $scope.createdDate),
                                createdOnTo: getDateRangePickerValue(2, $scope.createdDate)
                            }).then(function (data) {
                                $scope.Division = data.DataResult;
                                $scope.resultsLength = data.TotalRecordCount;
                                params.total($scope.resultsLength);
                                d.resolve(data.DataResult);
                            });
                            return d.promise;
                        }
                    };
                    $scope.tableParams = new NgTableParams(10, initialSettings);
                };


                //Reset filter fields
                $scope.reset = function () {
                    $scope.DivisionName = "";
                    $scope.DivisionCode = "";
                    $scope.CategoryId = null;
                    $scope.PositionId = null;
                    $scope.createdDate = null;
                    $scope.sortOrder = "desc";
                    $scope.sortColumn = "CreatedDate";

                    $scope.search(true);
                };

                $scope.searchWhenEnter = function ($event) {
                    var keyCode = $event.which || $event.keyCode;
                    if (keyCode === 13) {
                        $scope.search();
                    }
                };

                //#region Delete
                $scope.deleteDivision = function (divisionId, name) {
                    CommonService.deleteChanges(function () {
                        DivisionService.DeleteDivision({ id: divisionId, name: name }).then(function (data) {
                            CommonService.successMessage(data.message);
                            $scope.search(true);
                        });
                    }, name);
                };

                //#endregion

                //#region Reactivate / Deactivate
                $scope.toggleActiveStatus = function (id, isActive, name) {
                    if (isActive) {
                        CommonService.reActivate(function () {
                            reactivateDeactivate(id, isActive, name);
                        }, name);
                    } else {
                        CommonService.deActivate(function () {
                            reactivateDeactivate(id, isActive, name);
                        }, name);
                    }
                };

                function reactivateDeactivate(id, isActive, name) {
                    console.log(name);
                    DivisionService.ToggleDivisionStatus({ id: id, isActive: isActive, name: name })
                        .then(function (data) {
                            CommonService.successMessage(data.message);
                            $scope.search();
                        }), function (error /*Error event should handle here*/) {
                            console.log(error);
                            CommonService.errorMessage("Unexpected error occured.");
                        }, function (data /*Notify event should handle here*/) {
                        };
                }
                //#endregion

                function getCategories() {
                    DivisionService.GetCategories({
                    }, "CategoryId").then(function (data) {
                        $scope.Categories = data.data;
                    }, function (error /*Error event should handle here*/) {
                        console.log("Error");
                    }, function (data /*Notify event should handle here*/) {
                    });
                }

                function positionLookUp() {
                    CommonService.GetPositionLookUp({
                    },"PositionId").then(function (data) {
                        $scope.Positions = data.data;
                    }, function (error /*Error event should handle here*/) {
                        console.log("Error");
                    }, function (data /*Notify event should handle here*/) {
                    });
                }
                //#endregion
            }]);
  
