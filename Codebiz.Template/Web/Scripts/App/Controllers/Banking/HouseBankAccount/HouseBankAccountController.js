MetronicApp.controller('HouseBankAccountController', ['$scope', '$q', 'NgTableParams', 'CommonService', '$uibModal', '$timeout', '$location', '$window', 'HouseBankAccountService',
    function ($scope, $q, NgTableParams, CommonService, $uibModal, $timeout, $location, $window, HouseBankAccountService) {
        this.$onInit = function () {
            $scope.reset();
        }
        $scope.search = function () {
            var initialSettings = {
                getData: function (params) {
                    for (var i in params.sorting()) {
                        $scope.sortColumn = i;
                        $scope.sortOrder = params.sorting()[i];
                    }
                    var d = $q.defer();
                    HouseBankAccountService.Search({
                        BankCode: $scope.bankCode,
                        Country: $scope.country,
                        Branch:$scope.branch,
                        AccountNo: $scope.accountNo,
                        BankAccountName: $scope.bankAccountName,
                        CreatedBy: $scope.actionBy,
                        Page: params.page(),
                        PageSize: params.count(),
                        SortDirection: $scope.sortOrder,
                        SortColumn: $scope.sortColumn,
                        CreatedOnFrom: getDateRangePickerValue(1, $scope.createdDate),
                        CreatedOnTo: getDateRangePickerValue(2, $scope.createdDate),
                    }).then(function (data) {
                        
                        $scope.resultsLength = data.TotalRecordCount;
                        params.total(data.TotalRecordCount);
                        d.resolve(data.DataResult);
                    });
                    return d.promise;
                }
            };
            $scope.houseBankAccountTableParams = new NgTableParams(10, initialSettings);
        }
        $scope.reset = function () {
            $scope.bankCode = "";
            $scope.country = "";
            $scope.branch = "";
            $scope.accountNo = "";
            $scope.bankAccountName = "";
            $scope.actionBy = "";
            $scope.createdDate = null;
            $scope.sortColumn = "ActionOn";
            $scope.sortOrder = "desc";
            $scope.search();
        }
        $scope.exportDataToExcelFile = function () {
            if ($scope.resultsLength > 0) {
                CommonService.showLoading();
                HouseBankAccountService.ExportDataToExcelFile({
                    BankCode: $scope.bankCode,
                    Country: $scope.country,
                    Branch: $scope.branch,
                    AccountNo: $scope.accountNo,
                    BankAccountName: $scope.bankAccountName,
                    ActionBy: $scope.actionBy,
       
                    SortDirection: $scope.sortOrder,
                    SortColumn: $scope.sortColumn,
                    CreatedOnFrom: getDateRangePickerValue(1, $scope.createdDate),
                    CreatedOnTo: getDateRangePickerValue(2, $scope.createdDate),
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
                HouseBankAccountService.Delete({ id: id, name: name }).then(function (data) {
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
        $scope.searchWhenEnter = function ($event) {
            var keyCode = $event.which || $event.keyCode;
            if (keyCode === 13) {
                $scope.search();
            }
        }

        $scope.addOrUpdate = function (id) {
            if (id == 0) {
                $location.path("AddOrUpdate");
            }
            else {
                $location.path("AddOrUpdate/" + id);
            }

        }


    }])