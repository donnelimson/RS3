MetronicApp.controller('BankController', ['$scope', '$q', 'NgTableParams', 'CommonService', '$uibModal', '$timeout', '$location', '$window','BankService',
    function ($scope, $q, NgTableParams, CommonService, $uibModal, $timeout, $location, $window,BankService) {
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
                    BankService.Search({
                        Countrycode: $scope.countryCode,
                        BankCode: $scope.bankCode,
                        BankName: $scope.bankName,
                        BICSwiftCode: $scope.bicSwiftCode,
                        AccountNo: $scope.accountNo,
                        Branch: $scope.branch,
                        NextCheckNo: $scope.nextCheckNo,
                        Page: params.page(),
                        PageSize: params.count(),
                        SortDirection: $scope.sortOrder,
                        SortColumn: $scope.sortColumn,
                    }).then(function (data) {
                        $scope.resultsLength = data.TotalRecordCount;
                        params.total(data.TotalRecordCount);
                        d.resolve(data.DataResult);
                    });
                    return d.promise;
                }
            };
            $scope.bankTableParams = new NgTableParams(10, initialSettings);
        }
        $scope.reset = function () {
            $scope.countryCode = "";
            $scope.bankCode = "";
            $scope.bankName = "";
            $scope.bicSwiftCode = "";
            $scope.accountNo = "";
            $scope.branch = "";
            $scope.nextCheckNo = "";
            $scope.sortColumn = "ModifiedOn";
            $scope.sortOrder = "desc";
            $scope.search();
        }
        $scope.exportDataToExcelFile = function () {
            if ($scope.resultsLength > 0) {
                CommonService.showLoading();
                BankService.ExportDataToExcelFile({
                    Countrycode: $scope.countryCode,
                    BankCode: $scope.bankCode,
                    BankName: $scope.bankName,
                    BICSwiftCode: $scope.bicSwiftCode,
                    AccountNo: $scope.accountNo,
                    Branch: $scope.branch,
                    NextCheckNo: $scope.nextCheckNo,
                    SortDirection: $scope.sortOrder,
                    SortColumn: $scope.sortColumn,
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
        $scope.delete = function (id,name) {

            CommonService.deleteChanges(function () {
                BankService.Delete({ id: id, name: name }).then(function (data) {
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
        $scope.activateOrDeactivate = function (id, name, status) {
            CommonService.reactivateDeactivate(function () {
                CreditCardService.ActivateDeactivate({ creditCardId: id, name: name, status: status })
                    .then(function (data) {
                        if (data.success) {
                            CommonService.successMessage(data.message);
                        } else {
                            CommonService.warningMessage(data.message);
                        }

                        $scope.search();

                    }), function (error /*Error event should handle here*/) {
                        console.log(error);
                        CommonService.errorMessage("Unexpected error occured.");
                    }, function (data /*Notify event should handle here*/) {
                    };
            }, name, status);
        }
        $scope.deleteCreditCard = function (id, name) {

            CommonService.deleteChanges(function () {
                CreditCardService.Delete({ creditCardId: id, name: name }).then(function (data) {
                    if (data.success) {
                        CommonService.successMessage(data.message);
                    }
                    else {
                        CommonService.warningMessage(data.message);
                    }
                    $scope.search();
                });
            }, name);
        };
        $scope.addOrUpdate = function (id) {
            if (id == 0) {
                $location.path("AddOrUpdate");
            }
            else {
                $location.path("AddOrUpdate/"+id);  
            }
        
        }

    }])