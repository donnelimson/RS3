angular.module('MetronicApp').controller('CurrencyController',
    ['$scope', 'CurrencyService', 'NgTableParams', '$q', 'CommonService', '$location', '$window',
        function ($scope, CurrencyService, NgTableParams, $q, CommonService, $location, $window) {

            $scope.sortColumn = "ActionOn";
            $scope.sortOrder = "desc";

            $scope.currencies = {
                Code: "",
                Currency: "",
                ActionBy: "",
                ActionDate: null
            }

            this.$onInit = function () {
                $scope.search();
            };

            $scope.goToCreateOrEdit = function (id, isView) {
                if (id != 0 && !isView) {
                    $location.path("Edit/" + id);
                }
                else if (id != 0 && isView) {
                    $location.path("View/" + id);
                }
                else {
                    $location.path("New");
                }
            }

            //#region Export Data to Excel File
            $scope.exportDataToExcelFile = function () {
                if ($scope.resultsLength > 0) {
                    CommonService.showLoading();
                    CurrencyService.ExportDataToExcelFile({
                        SortDirection: $scope.sortOrder,
                        SortColumn: $scope.sortColumn,
                        Code: $scope.code,
                        Currency: $scope.currency,
                        ActionBy: $scope.actionBy,
                        ActionOn: $scope.createdDate
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

            //#region Reset Filter Fields

            $scope.reset = function () {
                $scope.code = "";
                $scope.currency = "";
                $scope.actionBy = "";
                $scope.createdDate = null;

                $scope.sortColumn = "ActionOn";
                $scope.sortOrder = "desc";
                $scope.search();
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

                        CurrencyService.CurrencyIndex({
                            Page: params.page(),
                            PageSize: params.count(),
                            SortDirection: $scope.sortOrder,
                            SortColumn: $scope.sortColumn,
                            Code: $scope.code,
                            Currency: $scope.currency,
                            ActionBy: $scope.actionBy,
                            createdOnFrom: getDateRangePickerValue(1, $scope.createdDate),
                            createdOnTo: getDateRangePickerValue(2, $scope.createdDate),
                        }).then(function (data) {
                            $scope.currencies = data.DataResult
                            $scope.resultsLength = data.TotalRecordCount;
                            params.total($scope.resultsLength);
                            d.resolve(data.DataResult);
                        });
                        return d.promise;

                    }
                };

                $scope.tableParams = new NgTableParams(10, initialSettings);
            };

            $scope.searchWhenEnter = function ($event) {
                var keyCode = $event.which || $event.keyCode;
                if (keyCode === 13) {
                    $scope.search();
                }
            }

            //#endregion

            //#region Delete CUrrency

            $scope.deleteCurrency = function (item) {
                CommonService.deleteChanges(function () {
                    CurrencyService.DeleteCurrency({
                        id: item.CurrencyId,
                        name: item.Currency
                    }).then(function (data) {
                        if (data.success) {
                            CommonService.successMessage(data.message);
                            $scope.search(true);
                        }
                        else {
                            CommonService.warningMessage(data.message);
                        }
                    });
                });
            }

            //#endregion
        }
    ]
)
    .controller('AddOrUpdateCurrencyController',
        ['$scope', 'CurrencyService', '$route', 'CommonService', '$location', '$window',
            function ($scope, CurrencyService, $route, CommonService, $location, $window) {

                //#region default variable

                var CurrencyId = $route.current.params.id;
                $scope.IsView = $route.current.$$route.params.isView;
                $scope.formSubmitted = false;
                $scope.details = "";
                $scope.addEditOrView = "Add";
                $scope.saveOrUpdate = "Save";

                $scope.currencyData = {
                    CurrencyId: 0,
                    Code: "",
                    Currency: "",
                    InternationalCode: "",
                    InternationalDescription: "",
                    HundredthName: "",
                    English: "",
                    EHN: "",
                    ISOCurrencyCodeId: null,
                    IncAmtDffAllwd: null,
                    OutAmtDffAllwd: null,
                    IncPrcntDffAllwd: null,
                    OutPrcntDffAllwd: null,
                    RoundingId: null,
                    DecimalId: null,
                    IsRounding: false
                }

                //#endregion

                //#region Initialization

                this.$onInit = function () {

                    if (CurrencyId != 0 && CurrencyId != null) {
                        getCurrencyDetails(CurrencyId, $scope.IsView)
                        $scope.details = "details";
                        $scope.saveOrUpdate = "Update";
                    }
                    getDecimals(null);
                    getRoundings(null);
                    getISOCurrencyCodes(null);
                }

                //#endregion

                //#region  Save Currency

                $scope.saveCurrency = function () {
                    $scope.formSubmitted = true;
                    if ($scope.currencyForm.$valid) {
                        if (CurrencyId == 0 || CurrencyId == null) {
                            CommonService.saveChanges(function () {
                                CurrencyService.AddCurrency({
                                    model: $scope.currencyData
                                }).then(function (data) {
                                    if (data.success) {
                                        CommonService.successMessage(data.message);
                                        $location.path("View");
                                    } else {
                                        CommonService.warningMessage(data.message);
                                    }
                                }), function (error /*Error event should handle here*/) {
                                    console.log(error);
                                    CommonService.errorMessage("Unexpected error occured.");
                                }, function (data /*Notify event should handle here*/) {
                                }
                            })
                        }
                        else {
                            CommonService.updateChanges(function () {
                                CurrencyService.EditCurrency({
                                    model: $scope.currencyData
                                }).then(function (data) {
                                    if (data.success) {
                                        CommonService.successMessage(data.message);
                                        $location.path("View");
                                    } else {
                                        CommonService.warningMessage(data.message);
                                    }
                                }), function (error /*Error event should handle here*/) {
                                    console.log(error);
                                    CommonService.errorMessage("Unexpected error occured.");
                                }, function (data /*Notify event should handle here*/) {
                                }
                            })
                        }
                    }
                }

                //#endregion

                //#region Cancel and Back to list button

                $scope.cancel = function () {
                    if (!$scope.currencyForm.$pristine) {
                        CommonService.cancelChanges(function () {
                            window.location.href = document.Currency + "Index";
                        });
                    }
                    else {
                        window.location.href = document.Currency + "Index";
                    }
                }

                //#endregion

                //#region Private Function Lookups

                function getRoundings(id) {
                    CommonService.GetRoundingLookUp({

                    }, "RoundingId").then(function (data) {
                        $scope.roundings = data.data;
                        if (CurrencyId != null && CurrencyId != 0) {
                            $scope.currencyData.RoundingId = id
                        }
                    }, function (error /*Error event should handle here*/) {
                        console.log("Error");
                    }, function (data /*Notify event should handle here*/) {
                        console.log("Error");
                    });
                }

                function getDecimals(id) {
                    CommonService.GetAllDecimals({
                    }, "DecimalId").then(function (data) {
                        $scope.decimals = data.data;
                        if (CurrencyId != null && CurrencyId != 0) {
                            $scope.currencyData.DecimalId = id
                        }
                    }, function (error /*Error event should handle here*/) {
                        console.log("Error");
                    }, function (data /*Notify event should handle here*/) {
                        console.log("Error");
                    });
                }

                function getISOCurrencyCodes(id) {
                    CommonService.GetAllISOCurrencyCodes({
                    }, "ISOCurrencyCodeId").then(function (data) {
                        $scope.isoCurrentCodes = data.data;
                        if (CurrencyId != null && CurrencyId != 0) {
                            $scope.currencyData.ISOCurrencyCodeId = id
                        }
                    }, function (error /*Error event should handle here*/) {
                        console.log("Error");
                    }, function (data /*Notify event should handle here*/) {
                        console.log("Error");
                    });
                }

                //#endregion

                //#region Get Details

                function getCurrencyDetails(id, isView) {
                    CurrencyService.GetCurrencyDetails({
                        id: id
                    }).then(function (data) {
                        $scope.currencyData = data.DataResult;
                        if (isView) {
                            $scope.addEditOrView = "View";
                        }
                        else {
                            $scope.addEditOrView = "Edit";
                        }
                        getDecimals($scope.currencyData.DecimalId);
                        getRoundings($scope.currencyData.RoundingId);
                        getISOCurrencyCodes($scope.currencyData.ISOCurrencyCodeId);
                    }, function (error /*Error event should handle here*/) {
                        console.log("Error");
                    }, function (data /*Notify event should handle here*/) {
                    });
                }

                //#endregion

            }
        ]
    );