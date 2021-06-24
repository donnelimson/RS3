angular.module('MetronicApp')
    .controller('SupportingDocumentsController',
        ['$scope', 'SupportingDocumentsService', 'NgTableParams', '$uibModal', '$window', 'CommonService', '$timeout', '$q',
            function ($scope, SupportingDocumentsService, NgTableParams, $uibModal, $window, CommonService, $timeout, $q) {

                //#region Variable Defaults

                $scope.requestTransactionTypes = {};

                //#endregion

                //#region Init

                this.$onInit = function () {
                    $scope.reset();
                    getRequestTransactionTypes();
                };

                //#endregion

                //#region Scope functions

                $scope.exportDataToExcelFile = function () {
                    if ($scope.resultsLength > 0) {
                        CommonService.showLoading();
                        SupportingDocumentsService.ExportDataToExcelFile({
                            TransactionTypeId: $scope.transactionTypeId,
                            SortDirection: $scope.sortOrder,
                            SortColumn: $scope.sortColumn,
                            CreatedBy: $scope.createdBy,
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

                $scope.search = function () {
                    var initialSettings = {
                        getData: function (params) {
                            for (var i in params.sorting()) {
                                $scope.sortColumn = i;
                                $scope.sortOrder = params.sorting()[i];
                            }

                            var d = $q.defer();

                            SupportingDocumentsService.SearchSupportingDocument({
                                TransactionTypeId: $scope.transactionTypeId,
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
                    $scope.transactionTypeId = null;
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

                $scope.addOrUpdate = function (id) {
                    CommonService.showLoading();
                    $timeout(function () {
                        if (id === 0) {
                            $window.location.href = document.SupportingDocuments + "Create";
                        }
                        else {
                            $window.location.href = document.SupportingDocuments + "Edit/" + id;
                        }
                    }, 500);
                };

                $scope.delete = function (id,name) {
                    CommonService.deleteChanges(function () {
                        SupportingDocumentsService.DeleteSupportingDocumentById({ id: id, name: name })
                            .then(function (data) {
                                CommonService.successMessage(data.message);
                                $scope.search();

                            }), function (error /*Error event should handle here*/) {
                                console.log(error);
                                CommonService.errorMessage("Unexpected error occured.");
                            }, function (data /*Notify event should handle here*/) {
                            };
                    },name);
                };

                $scope.viewDetails = function (supportingDocumentId) {
                    $window.location.href = document.SupportingDocuments + "Details/" + supportingDocumentId;
                };

                //#endregion

                //#region Private functions

                function getRequestTransactionTypes() {
                    CommonService.GetRequestTransactionTypes({
                    }).then(function (data) {
                        $scope.requestTransactionTypes = data.data;

                    }, function (error /*Error event should handle here*/) {
                        console.log("Error");
                    }, function (data /*Notify event should handle here*/) {
                    });
                }

                //#endregion
            }]);