angular.module('MetronicApp')
    .controller('PoleController',
        ['$scope', 'NgTableParams', 'PoleService', '$location', '$window', '$q', 'CommonService', '$uibModal',
            function PoleController($scope, NgTableParams, PoleService, $location, $window, $q, CommonService, $uibModal) {
                //Other defaults
                $scope.createdOnDatePicker = {
                    opened: false
                };

                $scope.poles = [];
                //#region Init
                this.$onInit = function () {
                    $scope.reset();
                };
                $scope.goToCreateUpdate = function (poleId) {
                    if (poleId != 0) {
                        window.location.href = document.Pole + "Edit?poleId=" + poleId;
                    }
                    else {
                        window.location.href = document.Pole + "Create";
                    }
                
                };

                $scope.openCreatedOnDatePicker = function () {
                    $scope.createdOnDatePicker.opened = true;
                };

                //#region Google Map
                $scope.viewMap = function (lat,lng) {
                    $uibModal.open({
                        animation: true,
                        ariaLabelledBy: 'modal-title',
                        ariaDescribedBy: 'modal-body',
                        templateUrl: 'MapModal.html',
                        controller: 'MapController',
                        controllerAs: 'map',
                        size: 'lg',
                        keyboard: false,
                        backdrop: "static",
                        windowClass: 'modal_style',
                        resolve: {
                            lat: function () {
                                return lat;
                            },
                            lng: function () {
                                return lng;
                            },
                            forView: function () {
                                return true;
                            }
                        }
                    });
                }

                //#region Google Map
                $scope.viewPoles = function () {
                    $uibModal.open({
                        animation: true,
                        ariaLabelledBy: 'modal-title',
                        ariaDescribedBy: 'modal-body',
                        templateUrl: 'MapModal.html',
                        controller: 'ViewMapController',
                        controllerAs: 'map',
                        size: 'xl',
                        keyboard: false,
                        backdrop: "static",
                        windowClass: 'modal_style',
                        resolve: {
                            forView: function () {
                                return true;
                            },
                            Data: function () {
                                return $scope.poles;
                            }
                        }
                    });
                }
                //#endregion
                //Export to Excell
                $scope.exportDataToExcelFile = function () {
                    if ($scope.resultsLength > 0) {
                        CommonService.showLoading();

                        PoleService.ExportDataToExcelFile({
                            SortDirection: $scope.sortOrder,
                            SortColumn: $scope.sortColumn,
                            PoleNo: $scope.poleNo,
                            Code: $scope.code,
                            Location: $scope.location,
                            Item: $scope.item,
                            CreatedBy: $scope.createdBy,
                            CreatedOnFrom: getDateRangePickerValue(1, $scope.createdDate),
                            CreatedOnTo: getDateRangePickerValue(2, $scope.createdDate),
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

                //Search Pole
                $scope.search = function () {
                    var initialSettings = {
                        getData: function (params) {
                            for (var i in params.sorting()) {
                                $scope.sortColumn = i;
                                $scope.sortOrder = params.sorting()[i];
                            }
                            var d = $q.defer();

                            PoleService.PoleIndex({
                                Page: params.page(),
                                PageSize: params.count(),
                                SortDirection: $scope.sortOrder,
                                SortColumn: $scope.sortColumn,
                                PoleNo: $scope.poleNo,
                                Code: $scope.code,
                                Location: $scope.location,
                                Item: $scope.item,
                                CreatedBy: $scope.createdBy,
                                CreatedOnFrom: getDateRangePickerValue(1, $scope.createdDate),
                                CreatedOnTo: getDateRangePickerValue(2, $scope.createdDate),
                            }).then(function (data) {
                                $scope.resultsLength = data.totalRecordCount;
                                params.total($scope.resultsLength);
                                $scope.poles = data.data;
                                d.resolve(data.data);
                            });
                            return d.promise;
                        }
                    };
                    $scope.tableParams = new NgTableParams(10, initialSettings);
                };

                //Reset Table and Clear Fields
                $scope.reset = function () {
                    $scope.sortColumn = "DateCreated";
                    $scope.sortOrder = "desc";

                    $scope.poleNo = "";
                    $scope.location = "";
                    $scope.code = "";
                    $scope.item = "";
                    $scope.createdBy = "";
                    $scope.createdDate = "";
                    $scope.search();
                };

                //Search when click enter
                $scope.searchWhenEnter = function ($event) {
                    var keyCode = $event.which || $event.keyCode;
                    if (keyCode === 13) {
                        $scope.search();
                    }
                };

                //Delete Pole
                $scope.deletePole = function (id, name) {
                    CommonService.deleteChanges(function () {
                        PoleService.Delete({ id: id, name: name }).then(function (data) {
                            if (data.ajaxResult.Success) {
                                CommonService.successMessage(data.ajaxResult.Message);
                            }
                            else {
                                CommonService.warningMessage(data.ajaxResult.Message);
                            }
                            $scope.search();
                        });
                    }, name);
                };

                //Toggle Active status 
                $scope.toggleActiveStatus = function (id, isActive, name) {
                    if (isActive) {
                        CommonService.reActivate(function () {
                            reactivateActivate(id, isActive, name);
                        }, name);
                    } else {
                        CommonService.deActivate(function () {
                            reactivateActivate(id, isActive, name);
                        }, name);
                    }
                };

                function reactivateActivate(id, isActive, name) {
                    PoleService.TogglePoleActiveStatus({ Id: id, IsActive: isActive, Name: name })
                        .then(function (data) {
                            CommonService.successMessage(data.message);
                            $scope.search();

                        }), function (error /*Error event should handle here*/) {
                            console.log(error);
                            CommonService.errorMessage("Unexpected error occured.");
                        }, function (data /*Notify event should handle here*/) {
                        };
                }

            }]);