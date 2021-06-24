angular.module('MetronicApp').controller('ApproverLabelController',
    ['$scope', 'ApproverLabelService', 'NgTableParams', '$uibModal', '$window', 'CommonService', '$timeout', '$q',
        function ($scope, ApproverLabelService, NgTableParams, $uibModal, $window, CommonService, $timeout, $q) {

            //#region Init

            this.$onInit = function () {
                $scope.reset();
            };

            //#endregion

            //#region Export

            $scope.exportDataToExcelFile = function () {
                if ($scope.resultsLength > 0) {
                    CommonService.showLoading();
                    ApproverLabelService.ExportDataToExcelFile({
                        Name: $scope.name,
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

                        ApproverLabelService.SearchApproverLabel({
                            Name: $scope.name,
                            Page: params.page(),
                            PageSize: params.count(),
                            SortDirection: $scope.sortOrder,
                            SortColumn: $scope.sortColumn
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

            //#region Reset Field

            $scope.reset = function () {
                $scope.name = "";

                $scope.sortOrder = "desc";
                $scope.sortColumn = "ActionOn";

                $scope.search();
            };

            //#endregion

            //#region Delete

            $scope.deleteItem = function (item) {
                CommonService.deleteChanges(function () {
                    ApproverLabelService.Delete({ id: item.ApproverLableId, name: item.Name }).then(function (data) {
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

            //#region Reactivate / Deactivate

            $scope.toggleActiveStatus = function (item) {
                CommonService.reactivateDeactivate(function () {
                    ApproverLabelService.ToggleActiveStatus({ id: item.ApproverLableId, isActive: item.IsActive, name: item.Name })
                        .then(function (data) {
                            if (data.success) {
                                CommonService.successMessage(data.message);
                                $scope.search();
                            }
                            else {
                                CommonService.warningMessage(data.message);
                                $scope.search();
                            }
                        }), function (error /*Error event should handle here*/) {
                            console.log(error);
                            CommonService.errorMessage("Unexpected error occured.");
                        }, function (data /*Notify event should handle here*/) {
                        };
                }, item.Name, item.IsActive);
            }

            //#endregion

            //#region Search When Enter

            $scope.searchWhenEnter = function ($event) {
                var keyCode = $event.which || $event.keyCode;
                if (keyCode === 13) {
                    $scope.search();
                }
            };

            //#endregion

            $scope.addOrUpdate = function (id) {
                $window.location.href = document.ApproverLabel + "Form";
            };
        }
    ]);