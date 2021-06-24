angular.module('MetronicApp')

    .controller('RouteController',
        ['$scope', 'RouteService', 'NgTableParams', '$uibModal', '$q', 'CommonService',
            function ($scope, RouteService, NgTableParams, $uibModal, $q, CommonService) {

                //Other defaults
                $scope.createdOnDatePicker = {
                    opened: false
                };

                this.$onInit = function () {
                    $scope.reset();
                };

                //#region Export to Excel
                $scope.exportDataToExcelFile = function () {
                    if ($scope.resultsLength > 0) {
                        if ($scope.sortColumn === "CreatedDate") {
                            $scope.sortColumn = "RouteCode";
                        }
                        CommonService.showLoading();
                        RouteService.ExportDataToExcelFile({
                            Description: $scope.description,
                            RouteCode: $scope.routeCode,
                            BookNo: $scope.bookNo,
                            Province: $scope.province,
                            CityTown: $scope.municipality,
                            Barangay: $scope.barangay,
                            SortDirection: $scope.sortOrder,
                            SortColumn: $scope.sortColumn,
                            CreatedBy: $scope.createdBy,
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
                            RouteService.SearchRoute({
                                Description: $scope.description,
                                RouteCode: $scope.routeCode,
                                BookNo: $scope.bookNo,
                                Province: $scope.province,
                                CityTown: $scope.municipality,
                                Barangay: $scope.barangay,
                                Page: params.page(),
                                PageSize: params.count(),
                                SortDirection: $scope.sortOrder,
                                SortColumn: $scope.sortColumn,
                                CreatedBy: $scope.createdBy,
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
                    $scope.description = "";
                    $scope.routeCode = "";
                    $scope.bookNo = "";
                    $scope.province = "";
                    $scope.barangay = "";
                    $scope.municipality = null;
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

                //#endregion

                //#region Delete
                $scope.deleteRoute = function (routeId,name) {
                    CommonService.deleteChanges(function () {
                        RouteService.DeleteRoute({ id: routeId }).then(function () {
                            CommonService.successMessage("Route was successfully deleted.");
                            $scope.search();
                        });
                    },name);
                };
                //#endregion

                //#region Activate/Deactivate
                $scope.toggleActiveStatus = function (id, isActive,name) {
                    if (isActive) {
                        CommonService.reActivate(function () {
                            RouteService.ToggleRouteActiveStatus({ Id: id, IsActive: isActive, Name: name })
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
                            RouteService.ToggleRouteActiveStatus({ Id: id, IsActive: isActive, Name: name })
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

                //#region Modal Add/Update
                $scope.openRouteSaveModal = function (routeId) {
                    $uibModal.open({
                        animation: true,
                        ariaLabelledBy: 'modal-title',
                        ariaDescribedBy: 'modal-body',
                        templateUrl: 'RouteSaveModal.html',
                        controller: 'RouteSaveModalController',
                        controllerAs: 'controller',
                        size: 'md',
                        keyboard: false,
                        backdrop: "static",
                        windowClass: 'modal_style',
                        resolve: {
                            DepartmentId: function () {
                                return routeId;
                            }
                        }
                    }).result.then(function (data) {
                        $scope.reset();
                    },
                        function () {
                        });
                };
                //#endregion

              

            }]);