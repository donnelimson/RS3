(function () {
    'use strict';
    var app = angular.module('MetronicApp');
    app.controller('ApprovalController', ApprovalController);
    ApprovalController.$inject = ['$scope', 'NgTableParams', 'ApprovalService', '$window', '$q', 'CommonService', '$uibModal'];

    function ApprovalController($scope, NgTableParams, approvalService, $window, $q, commonService, $uibModal) {

        //#region Other defaults

        $scope.resultsLength = 0;

        //#endregion

        //#region Initialization

        this.$onInit = function () {
            getApprovalStatus();
            getTransactionTypes();
            $scope.reset();
        }

        //#endregion

        //#region Search Approval

        $scope.search = function () {
            var initialSettings = {
                getData: function (params) {
                    for (var i in params.sorting()) {
                        $scope.sortColumn = i;
                        $scope.sortOrder = params.sorting()[i];
                    }
                    var d = $q.defer();

                    approvalService.ApprovalIndex({
                        Page: params.page(),
                        PageSize: params.count(),
                        SortDirection: $scope.sortOrder,
                        SortColumn: $scope.sortColumn,
                        TransactionTypeId: $scope.transactionTypeId,
                        TransactionCode: $scope.referenceNo,
                        StatusId: $scope.statusId,
                        ActionDateFrom: getDateRangePickerValue(1, $scope.actionDate),
                        ActionDateTo: getDateRangePickerValue(2, $scope.actionDate),
                        CreatedOnFrom: getDateRangePickerValue(1, $scope.createdDate),
                        CreatedOnTo: getDateRangePickerValue(2, $scope.createdDate),
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

        //#endregion

        //#region Export Data File

        $scope.exportDataToExcelFile = function () {
            if ($scope.resultsLength > 0) {
                if ($scope.sortColumn === "CreatedOn") {
                    $scope.sortColumn = "CreatedOn";
                }
                commonService.showLoading();
                approvalService.ExportDataToExcelFile({
                    SortDirection: $scope.sortOrder,
                    SortColumn: $scope.sortColumn,
                    AccountNo: $scope.accountNo,
                    TransactionTypeId: $scope.transactionTypeId,
                    TransactionCode: $scope.referenceNo,
                    StatusId: $scope.statusId,
                    ActionDateFrom: getDateRangePickerValue(1, $scope.actionDate),
                    ActionDateTo: getDateRangePickerValue(2, $scope.actionDate),
                    CreatedOnFrom: getDateRangePickerValue(1, $scope.createdDate),
                    CreatedOnTo: getDateRangePickerValue(2, $scope.createdDate),
                }).then(function (data) {
                    commonService.hideLoading();
                    window.location.href = document.FileUpload + "DownloadExcelFile?fileName=" + data.data.FileName;

                }, function (error /*Error event should handle here*/) {
                    console.log("Error");
                }, function (data /*Notify event should handle here*/) {
                    console.log("Error");
                });
            }
            $scope.sortColumn = "CreatedOn";
        };

        //#endregion

        $scope.reset = function () {
            $scope.accountNo = "";
            $scope.name = "";
            $scope.consumerTypeId = null;
            $scope.transactionTypeId = null;
            $scope.statusId = null;
            $scope.actionDate = "";
            $scope.createdDate = "";
            $scope.sortColumn = null;
            $scope.sortOrder = null;

            $scope.sortColumn = "CreatedOn";
            $scope.sortOrder = "desc";
            $scope.search();
        }

        $scope.searchWhenEnter = function ($event) {
            var keyCode = $event.which || $event.keyCode;
            if (keyCode === 13) {
                $scope.search();
            }
        };

        $scope.process = function (approvalId, approvalApproverId, transactionTypeId) {
            $window.location.href = document.Approval + "Process?approvalId=" + approvalId + "&approvalApproverId=" + approvalApproverId + "&transactionTypeId=" + transactionTypeId;
        };

        $scope.gotoDetails = function (data) {
            approvalService.GoToDetails(data);
        };

        $scope.approvalResult = function (ApprovalId) {
            $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: '_ApprovalResultModal.html',
                controller: 'ApprovalResultController',
                size: 'xlg',
                keyboard: false,
                backdrop: "static",
                windowClass: 'modal_style',
                modalOverflow: true,
                resolve: {
                    ApprovalId: function () {
                        return ApprovalId;
                    },
                }
            }).result.then(function () {
                init();
            }, function () {
            });
        };


        //#region Private Functions

        function getApprovalStatus() {
            approvalService.GetApprovalStatus().then(function (data) {
                $scope.approvalStatus = data.data;
            },
                function (error /*Error event should handle here*/) {
                    console.log("Error");
                },
                function (data /*Notify event should handle here*/) {
                })
        }

        function getTransactionTypes() {
            approvalService.GetTransactionType().then(function (data) {
                $scope.transactionType = data.data;
            },
                function (error /*Error event should handle here*/) {
                    console.log("Error");
                },
                function (data /*Notify event should handle here*/) {
                })
        }

        //#endregion

    }
})();
