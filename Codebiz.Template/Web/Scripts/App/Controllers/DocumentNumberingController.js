MetronicApp.controller('DocumentNumberingController', ['$scope', '$q', 'NgTableParams', 'CommonService', '$timeout', '$location', '$window', 'DocumentNumberingService',
    function ($scope, $q, NgTableParams, CommonService, $timeout, $location, $window, DocumentNumberingService) {
        $scope.documentNumberingTableParams = new NgTableParams({}, { dataset: [] });
        $scope.resultsLength = 0;
        this.$onInit = function () {
            getAllTransactionTypeForDocumentNumbering();
            $scope.reset();
        }

        //functions
        function getAllTransactionTypeForDocumentNumbering() {
            var fieldName = "TransactionTypeId";
            DocumentNumberingService.GetAllTransactionTypeForDocumentNumbering({}, fieldName).then(function (data) {
                $scope.transactionTypes = data.result
            });
        }
        //scopes
        $scope.search = function () {
            var initialSettings = {
                getData: function (params) {
                    for (var i in params.sorting()) {
                        $scope.sortColumn = i;
                        $scope.sortOrder = params.sorting()[i];
                    }
                    var d = $q.defer();

                    DocumentNumberingService.Search({
                        TransactionTypeId: $scope.transactionTypeId,
                        ActionBy: $scope.actionBy,
                        Page: params.page(),
                        PageSize: params.count(),
                        SortDirection: $scope.sortOrder,
                        SortColumn: $scope.sortColumn,
                        CreatedOnFrom: getDateRangePickerValue(1, $scope.actionDate),
                        CreatedOnTo: getDateRangePickerValue(2, $scope.actionDate),
                    }).then(function (data) {
                        $scope.resultsLength = data.filteredRecordCount;
                        params.total(data.filteredRecordcount);
                        d.resolve(data.result);
                    });
                    return d.promise;
                }
            };
            $scope.documentNumberingTableParams = new NgTableParams(10, initialSettings);
        }
        $scope.searchWhenEnter=function($event){
            var keyCode = $event.which || $event.keyCode;
            if (keyCode === 13) {
                $scope.search();
            }
        }
        $scope.exportDataToExcelFile = function () {
            if ($scope.resultsLength > 0) {
                CommonService.showLoading();
                DocumentNumberingService.ExportDataToExcelFile({
                    TransactionTypeId: $scope.transactionTypeId,
                    ActionBy: $scope.actionBy,
                    SortDirection: $scope.sortOrder,
                    SortColumn: $scope.sortColumn,
                    CreatedOnFrom: getDateRangePickerValue(1, $scope.actionDate),
                    CreatedOnTo: getDateRangePickerValue(2, $scope.actionDate),
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

        $scope.reset = function () {
            $scope.transactionTypeId = null;
            $scope.actionBy = null;
            $scope.actionDate = null;
            $scope.sortColumn = "ActionDate";
            $scope.sortOrder = "desc";
            $scope.search();
        }
        $scope.goToAdd = function () {
            CommonService.showLoading();
            $window.location.href = document.DocumentNumbering + "Add";
        }
        $scope.goToEdit = function (id,viewOnly) {
            CommonService.showLoading();
            $window.location.href = document.DocumentNumbering + "Edit?documentNumberingId="+id+"&viewOnly="+viewOnly;
        }
        $scope.goToDetails = function (id) {
            CommonService.showLoading();
            $window.location.href = document.DocumentNumbering + "Details?documentNumberingId=" + id;
        }
    }])