angular.module('MetronicApp').controller('ReportSignatoryController',
    ['$scope', 'ReportSignatoryService', 'NgTableParams', '$timeout', '$window', '$q', 'CommonService',
        function ($scope, ReportSignatoryService, NgTableParams, $timeout, $window, $q, CommonService) {

            //Init
            this.$onInit = function () {
                $scope.reset();
            }

            //Export Office Data to exel 
            $scope.exportDataToExcelFile = function () {
                if ($scope.resultsLength > 0) {
                    CommonService.showLoading();

                    ReportSignatoryService.ExportDataToExcelFile({
                        Category: $scope.Category,
                        Report: $scope.Report,
                        Signatory: $scope.Signatory,
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

            //Search Search Report Signatories
            $scope.search = function () {
                var initialSettings = {
                    getData: function (params) {
                        for (var i in params.sorting()) {
                            $scope.sortColumn = i;
                            $scope.sortOrder = params.sorting()[i];
                        }
                        var d = $q.defer();

                        ReportSignatoryService.SearchReportSignatories({
                            Category: $scope.Category,
                            Report: $scope.Report,
                            Signatory: $scope.Signatory,

                            Page: params.page(),
                            PageSize: params.count(),
                            SortDirection: $scope.sortOrder,
                            SortColumn: $scope.sortColumn,
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
                $scope.Category = "";
                $scope.Report = "";
                $scope.Signatory = "";

                $scope.sortColumn = "ActionOn";
                $scope.sortOrder = "desc";

                $scope.search();
            }

            $scope.searchWhenEnter = function ($event) {
                var keyCode = $event.which || $event.keyCode;
                if (keyCode === 13) {
                    $scope.search();
                }
            }

            $scope.addOrUpdate = function (id) {
                CommonService.showLoading();
                if (id === 0) {
                    $window.location.href = document.ReportSignatory + "Create";
                }
                else {
                    $window.location.href = document.ReportSignatory + "Edit/" + id;
                }
            };

            $scope.deleteReportSignatory = function (item) {
                CommonService.deleteChanges(function () {
                    ReportSignatoryService.DeleteReportSignatory({ id: item.ReportSignatoryId, category: item.Category }).then(function (data) {
                        if (data.result) {
                            CommonService.successMessage(data.message);
                            $scope.search(true);
                        } else {
                            CommonService.warningMessage(data.message);
                        }
                    })
                }, item.Category)
            }

            $scope.toggleActiveStatus = function (item) {
                CommonService.reactivateDeactivate(function () {
                    ReportSignatoryService.ToggleActiveStatus({ id: item.ReportSignatoryId, isActive: item.IsActive, category: item.Category }).then(function (data) {
                        if (data.success) {
                            CommonService.successMessage(data.message);
                            $scope.search(true);
                        } else {
                            CommonService.warningMessage(data.message);
                        }
                    });
                }, item.Category, item.IsActive);
            }

        }
    ]);