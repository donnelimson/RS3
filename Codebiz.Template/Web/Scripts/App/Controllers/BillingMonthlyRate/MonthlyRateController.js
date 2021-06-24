angular.module('MetronicApp')
    .controller('MonthlyRateController',
        ['$scope', 'NgTableParams', 'MonthlyRateService', '$location', '$window', '$q', 'CommonService',
            function MonthlyRateController($scope, NgTableParams, MonthlyRateService, $location, $window, $q, CommonService) {
                //View MonthlyRate Details
                $scope.goToBillingMonthlyRateDetails = function (monthlyRateId) {
                    window.location.href = document.BillingMonthlyRate + "Details?monthlyRateId=" + monthlyRateId;
                };

                $scope.goToEdit = function (monthlyRateId) {
                    window.location.href = document.BillingMonthlyRate + "Edit?monthlyRateId=" + monthlyRateId;
                };
                $scope.clone = function (monthlyRateId) {
                    window.location.href = document.BillingMonthlyRate + "Clone?monthlyRateId=" + monthlyRateId;
                };

                //#region Init

                this.$onInit = function () {
                    getConsumerClasses();
                    $scope.reset();
                };

                //#endregion

                //#region scope Functions
                $scope.createdOnDatePicker = {
                    opened: false
                };

                $scope.openCreatedOnDatePicker = function () {
                    $scope.createdOnDatePicker.opened = true;
                };
                $scope.gotoCreate = function () {
                    window.location.href = document.BillingMonthlyRate + "Form";
                };

                $scope.exportDataToExcelFile = function () {
                    if ($scope.resultsLength > 0) {
                        CommonService.showLoading();

                        MonthlyRateService.ExportDataToExcelFile({
                            SortDirection: $scope.sortOrder,
                            SortColumn: $scope.sortColumn,
                            BillingPeriod: $scope.billingPeriod,
                            ConsumerClassId: $scope.consumerClassId,
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

                            MonthlyRateService.MonthlyRateIndex({
                                Page: params.page(),
                                PageSize: params.count(),
                                SortDirection: $scope.sortOrder,
                                SortColumn: $scope.sortColumn,
                                BillingPeriod: $scope.billingPeriod,
                                ConsumerClassId: $scope.consumerClassId,
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

                //Reset Table and Clear Fields
                $scope.reset = function () {
                    $scope.sortColumn = "CreatedDate";
                    $scope.sortOrder = "desc";

                    $scope.billingPeriod = "";
                    $scope.consumerClassId = null;
                    $scope.createdBy = "";
                    $scope.createdDate = "";
                    $scope.search();
                };

                $scope.searchWhenEnter = function ($event) {
                    var keyCode = $event.which || $event.keyCode;
                    if (keyCode === 13) {
                        $scope.search();
                    }
                };

                //#endregion

                //#region Private functions
                function getConsumerClasses() {
                    CommonService.GetConsumerClass({
                    }, "ConsumerClassId").then(function (data) {
                        $scope.consumerClasses = data.data;
                    }, function (error /*Error event should handle here*/) {
                        console.log("Error");
                    }, function (data /*Notify event should handle here*/) {
                    });
                }
                //#endregion
            }]);