MetronicApp.controller('ItemMasterIndexController', ['$scope', '$q', 'NgTableParams', 'CommonService', '$uibModal', '$timeout', '$location', '$window', 'ItemMasterService', '$controller',
    function ($scope, $q, NgTableParams, CommonService, $uibModal, $timeout, $location, $window, ItemMasterService, $controller) {
        this.$onInit = function () {
            $scope.reset();
        }
        $scope.f = {
            ItemCode: "",
            LongDescription: "",
            ShortDescription: "",
            CreatedBy: "",
            CreatedOnFrom: null,
            CreatedOnTo: null,
            CreatedOn:null
        }
        $scope.reset = function () {
            $scope.f.ItemCode = "";
            $scope.f.LongDescription = "";
            $scope.f.ShortDescription = "";
            $scope.f.CreatedBy = "";
            $scope.CreatedOn = null;
            $scope.f.CreatedOnFrom = null;
            $scope.f.CreatedOnTo = null;
            $scope.search();
        }
        $scope.searchWhenEnter = function ($event) {
            var keyCode = $event.which || $event.keyCode;
            if (keyCode === 13) {
                $scope.search();
            }
        }
        $scope.exportDataToExcelFile = function () {
            if ($scope.resultsLength > 0) {
                $scope.f.SortDirection = $scope.sortOrder == null ? 'desc' : $scope.sortOrder;
                $scope.f.SortColumn = $scope.sortColumn == null ? 'CreatedOn' : $scope.sortColumn;
                $scope.f.CreatedOnFrom = getDateRangePickerValue(1, $scope.CreatedOn);
                $scope.f.CreatedOnTo = getDateRangePickerValue(2, $scope.CreatedOn);
                CommonService.showLoading();
                ItemMasterService.ExportDataToExcelFile({
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
                    $scope.f.SortColumn = $scope.sortColumn == null ? 'CreatedOn' : $scope.sortColumn;
                    $scope.f.CreatedOnFrom = getDateRangePickerValue(1, $scope.CreatedOn);
                    $scope.f.CreatedOnTo = getDateRangePickerValue(2, $scope.CreatedOn);
                    ItemMasterService.Search({
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
        $scope.edit = function (id) {
            window.location.href=document.ItemMaster + "Form?id=" + id;
        }
        $scope.generateReport = function (collectionId) {
            //showAndRenderReport(document.PYAcknowledgementReceipt + "GenerateAcknowledgementReceiptReport?collectionId=" + collectionId);
            let frameURL = document.PYAcknowledgementReceipt + "GenerateAcknowledgementReceiptReport?collectionId=" + collectionId;
            $scope.loadReport(frameURL);
        }
        $("#reportFrameModal").on('hidden.bs.modal', function () {
        });
        // showSpinnerWhileIFrameLoads();
    }])