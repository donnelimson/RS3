angular.module('MetronicApp').controller('SitioController', ['$scope', 'SitioService', 'NgTableParams', '$uibModal', '$q', 'CommonService',
    function ($scope, SitioService, NgTableParams, $uibModal, $q, CommonService) {

        //#region Initialization

        this.$onInit = function () {
            $scope.reset();
        };

        //#endregion

        //#region Export Data to Excel

        $scope.exportDataToExcelFile = function () {
            if ($scope.resultsLength > 0) {
                CommonService.showLoading();
                SitioService.ExportDataToExcelFile({
                    SortDirection: $scope.sortOrder,
                    SortColumn: $scope.sortColumn,
                    Code: $scope.code,
                    Name: $scope.name,
                    Purok: $scope.purok,
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

        //#region Search

        $scope.search = function () {
            var initialSettings = {
                getData: function (params) {
                    for (var i in params.sorting()) {
                        $scope.sortColumn = i;
                        $scope.sortOrder = params.sorting()[i];
                    }
                    var d = $q.defer();

                    SitioService.GetSitios({
                        Page: params.page(),
                        PageSize: params.count(),
                        SortDirection: $scope.sortOrder,
                        SortColumn: $scope.sortColumn,
                        Code: $scope.code,
                        Name: $scope.name,
                        Purok: $scope.purok,
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

        //#endregion

        //#region Reset

        $scope.reset = function () {
            $scope.code = "";
            $scope.name = "";
            $scope.purok = "";
            $scope.createdDate = null;

            $scope.sortOrder = "desc";
            $scope.sortColumn = "CreatedDate";

            $scope.search();
        };

        //#endregion

        //#region Reactivate / Deactivate

        $scope.toggleActiveStatus = function (item, isActive) {
            if (!item.IsActive) {
                CommonService.reActivate(function () {
                    SitioService.ToggleSitioActiveStatus({ id: item.SitioId, isActive, name: item.Name })
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
                    SitioService.ToggleSitioActiveStatus({ id: item.SitioId, isActive, name: item.Name })
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

        $scope.delete = function (item) {
            CommonService.deleteChanges(function () {
                SitioService.sitioDelete({ id: item.SitioId, name: item.Name }).then(function (data) {
                    if (data.success) {
                        CommonService.successMessage(data.message);
                        $scope.search(true);
                    } else {
                        CommonService.warningMessage(data.message);
                    }
                });
            }, item.Name);
        };

        //#endregion

        //#region Search when Enter

        $scope.searchWhenEnter = function ($event) {
            var keyCode = $event.which || $event.keyCode;
            if (keyCode === 13) {
                $scope.search();
            }
        };

        //#endregion

        //#region Open Modal page

        $scope.openSitioSaveModal = function (sitioId) {
            $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: 'SitioSaveModal.html',
                controller: 'SitioSaveModalController',
                controllerAs: 'controller',
                size: 'md',
                keyboard: false,
                backdrop: "static",
                windowClass: 'modal_style',
                resolve: {
                    SitioId: function () {
                        return sitioId;

                    }
                }
            }).result.then(function (data) {
                $scope.reset();
            },
                function () {

                });
        };

        //#endregion

    }
]);