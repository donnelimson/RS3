MetronicApp.controller('PurposeIndexController', ['$scope', '$q', 'NgTableParams', 'CommonService', '$uibModal', '$timeout', '$location', '$window', 'PurposeService',
    function ($scope, $q, NgTableParams, CommonService, $uibModal, $timeout, $location, $window, PurposeService) {
        this.$onInit = function () {
            $scope.reset();
            PurposeService.GetTransactionTypesForPurose({
            }).then(function (data) {
                $scope.transactionTypes = data.result;
            }, function (error /*Error event should handle here*/) {
                console.log("Error");
            }, function (data /*Notify event should handle here*/) {
            });
        }
        //    getDateRangePickerValue(2, $scope.createdDate)
        $scope.search = function () {
            var initialSettings = {
                getData: function (params) {
                    for (var i in params.sorting()) {
                        $scope.sortColumn = i;
                        $scope.sortOrder = params.sorting()[i];
                    }
                    var d = $q.defer();
                    $scope.f.Page = params.page();
                    $scope.f.PageSize = params.count();
                    $scope.f.SortDirection = $scope.sortOrder == null ? 'desc' : $scope.sortOrder;
                    $scope.f.SortColumn = $scope.sortColumn == null ? 'ActionOn' : $scope.sortColumn;
                    $scope.f.CreatedOnFrom = getDateRangePickerValue(1, $scope.f.ActionOn);
                    $scope.f.CreatedOnTo = getDateRangePickerValue(2, $scope.f.ActionOn);
                    PurposeService.Search({
                        filter: $scope.f
                    }).then(function (data) {
                        $scope.resultsLength = data.totalRecordCount;
                        params.total(data.totalRecordCount);
                        d.resolve(data.result);
                    });
                    return d.promise;
                }
            };
            $scope.tableParams = new NgTableParams(10, initialSettings);
        }
        $scope.reset = function () {
            $scope.f = {
                TransactionTypeId:null,
                ActionOn: null,
                Page: null,
                Purpose:"",
                PageSize: null,
                SortDirection: 'desc',
                SortColumn: 'ActionOn',
                CreatedOnFrom: null,
                CreatedOnTo: null
            }
            $scope.search();
        }
        $scope.exportDataToExcelFile = function () {
            console.log($scope.resultsLength)
            if ($scope.resultsLength > 0) {
                $scope.f.SortDirection = $scope.sortOrder == null ? 'desc' : $scope.sortOrder;
            //    $scope.f.SortColumn = $scope.sortColumn == null ? 'Created' : $scope.sortColumn == 'CreatedBy' ? 'Created' : $scope.sortColumn == 'AccountName' ? 'Account' : $scope.sortColumn;
                $scope.f.CreatedOnFrom = getDateRangePickerValue(1, $scope.createdDate);
                $scope.f.CreatedOnTo = getDateRangePickerValue(2, $scope.createdDate);
                CommonService.showLoading();
                PurposeService.ExportDataToExcelFile({
                    filter: $scope.f
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
        $scope.delete = function (id, name) {

            CommonService.deleteChanges(function () {
                ContestableApplicationService.Delete({ id: id }).then(function (data) {
                    if (data.success) {
                        CommonService.successMessage(data.message);
                    }
                    else {
                        CommonService.warningMessage(data.message);
                    }
                    $scope.search();
                });
            }, name);
        }
        $scope.edit = function (id) {
            window.location.href = document.Purpose + "AddOrUpdate?Id=" + id;
        }
        $scope.viewDetails = function (id) {
            $window.localStorage.setItem("parentConsumerForm", document.CSAChangeOfMeter);
            $window.location.href = document.CSAChangeOfMeter + "Details/" + id;
        }
        $scope.searchWhenEnter = function ($event) {
            var keyCode = $event.which || $event.keyCode;
            if (keyCode === 13) {
                $scope.search();
            }
        }
        $scope.recommendForApproval = function (id) {
            CommonService.approvalOfRecord(function () {
                ContestableApplicationService.RecommendForApproval({
                    id: id
                }).then(function (data) {
                    if (data.success) {
                        CommonService.successMessage(data.message);
                    }
                    else {
                        CommonService.warningMessage(data.message);
                    }
                    $scope.search();
                });
            }, true);
        }
        $scope.delete = function (id, name) {
            CommonService.deleteChanges(function () {
                ChangeOfMeterService.Delete({ id: id }).then(function (data) {
                    if (data.success) {
                        CommonService.successMessage(data.message);
                    }
                    else {
                        CommonService.warningMessage(data.message);
                    }
                    $scope.search();
                });
            }, name);
        }

        $scope.viewAccountDetails = function (id) {
            $window.open(document.CSAMemberAccounts
                + "Details?accountId=" + id
                + "&forView=" + true, '_blank');
        };
    }])