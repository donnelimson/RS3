angular.module('MetronicApp')

    .controller('PurokController',
        ['$scope', 'PurokService', 'NgTableParams', '$uibModal', '$q', 'CommonService',
            function ($scope, PurokService, NgTableParams, $uibModal, $q, CommonService) {

                //#region Initialization

                this.$onInit = function () {
                    $scope.reset();
                };

                //#endregion

                //#region Export Data To Excel File

                $scope.exportDataToExcelFile = function () {
                    if ($scope.resultsLength > 0) {
                        CommonService.showLoading();
                        PurokService.ExportDataToExcelFile({
                            SortDirection: $scope.sortOrder,
                            SortColumn: $scope.sortColumn,
                            Code: $scope.Code,
                            Name: $scope.Name,
                            Barangay: $scope.Barangay
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

                //#region Search

                $scope.search = function () {
                    var initialSettings = {
                        getData: function (params) {
                            for (var i in params.sorting()) {
                                $scope.sortColumn = i;
                                $scope.sortOrder = params.sorting()[i];
                            }
                            var d = $q.defer();

                            PurokService.GetPuroks({
                                Page: params.page(),
                                PageSize: params.count(),
                                SortDirection: $scope.sortOrder,
                                SortColumn: $scope.sortColumn,
                                Code: $scope.Code,
                                Name: $scope.Name,
                                Barangay: $scope.Barangay
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

                //#endregion

                //#region Reset 

                $scope.reset = function () {
                    $scope.Code = "";
                    $scope.Name = "";
                    $scope.Barangay = "";
                    $scope.sortColumn = "CreatedDate";
                    $scope.sortOrder = "desc";
                    $scope.search();
                };

                //#endregion

                //#region Open Modal

                $scope.openPurokAddModal = function (purokId) {
                    $uibModal.open({
                        animation: true,
                        ariaLabelledBy: 'modal-title',
                        ariaDescribedBy: 'modal-body',
                        templateUrl: 'PurokModalPage.html',
                        controller: 'PurokModalController',
                        controllerAs: 'controller',
                        size: 'md',
                        keyboard: false,
                        backdrop: "static",
                        windowClass: 'modal_style',
                        resolve: {
                            PurokId: function () {
                                return purokId;
                            }
                        }

                    }).result.then(function (data) {
                        $scope.reset();
                    },
                        function () {

                        });
                };

                //#endregion

                //#region Reactivate / Deactivate

                $scope.toggleActiveStatus = function (item, isActive) {
                    if (!item.IsActive) {
                        CommonService.reActivate(function () {
                            PurokService.TogglePurokActiveStatus({ id: item.PurokId, isActive: isActive, name: item.Name })
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
                        }, item.Name);
                    }
                    else {
                        CommonService.deActivate(function () {
                            PurokService.TogglePurokActiveStatus({ id: item.PurokId, isActive: isActive , name: item.Name })
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
                        }, item.Name);
                    }
                };

                //#endregion

                //#region Delete

                $scope.deletePurok = function (item) {
                    CommonService.deleteChanges(function () {
                        PurokService.purokDelete({ id: item.PurokId, name: item.Name }).then(function (data) {
                            if (data.success) {
                                CommonService.successMessage(data.message);
                                $scope.search(true);
                            }
                            else {
                                CommonService.warningMessage(data.message);
                            }
                        });
                    }, item.Name);
                };

                //#endregion

                //#region Search When Enter

                $scope.searchWhenEnter = function ($event) {
                    var keyCode = $event.which || $event.keyCode;
                    if (keyCode === 13) {
                        $scope.search();
                    }
                };

                //#endregion

            }]);