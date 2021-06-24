(function () {
    var app = angular.module('MetronicApp');

    app.requires.push.apply(app.requires, ['INVNTRY.MODELS', 'INVNTRY.DATASERVICE', 'FIN.DATASERVICE']);

    app.controller('TransactionControlNumberCtrl', ['$scope', 'NgTableParams', '$uibModal', '$window', '$timeout', '$q', 'TransactionControlNumberService', 'CommonService',
        function ($scope, NgTableParams, $uibModal, $window, $timeout, $q, TransactionControlNumberService, CommonService) {


                this.$onInit = function () {
                    $scope.reset();
                }

                //#region Reset filter fields

                $scope.reset = function () {
                    $scope.code = "";
                    $scope.nameOrPurpose = "";
                    $scope.project = "";
                    $scope.matCode = "";

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

                            TransactionControlNumberService.GetList({
                                Page: params.page(),
                                PageSize: params.count(),
                                SortDirection: $scope.sortOrder,
                                SortColumn: $scope.sortColumn,

                                Code: $scope.code,
                                NameOrPurpose: $scope.nameOrPurpose,
                                Project: $scope.project,
                                MatCode: $scope.matCode,

                            }).then(function (data) {
                                $scope.resultsLength = data.TotalRecordCount;
                                params.total($scope.resultsLength);
                                d.resolve(data.DataResult);
                            });
                            return d.promise;

                        }
                    };

                    $scope.tableParams = new NgTableParams(10, initialSettings);

                };

                //#endregion

                //#region Export Data to Excel

                $scope.exportDataToExcelFile = function () {
                    if ($scope.resultsLength > 0) {
                        CommonService.showLoading();
                        TransactionControlNumberService.ExportDataToExcelFile({
                            SortDirection: $scope.sortOrder,
                            SortColumn: $scope.sortColumn,

                            Code: $scope.code,
                            NameOrPurpose: $scope.nameOrPurpose,
                            Project: $scope.project,
                            MatCode: $scope.matCode
                        }).then(function (data) {
                            CommonService.hideLoading();
                            window.location.href = document.FileUpload + "DownloadExcelFile?fileName=" + data.DataResult;

                        }, function (error /*Error event should handle here*/) {
                            console.log("Error");
                        }, function (data /*Notify event should handle here*/) {
                            console.log("Error");
                        });
                    }
                };

                //#endregion

                $scope.searchWhenEnter = function ($event) {
                    var keyCode = $event.which || $event.keyCode;
                    if (keyCode === 13) {
                        $scope.search();
                    }
                };

                //#region Delete Transaction Code

                $scope.deleteItem = function (item) {
                    CommonService.deleteChanges(function () {
                        TransactionControlNumberService.DeleteTCN({
                            id: item.TransactionControlNumberID,
                            code: item.Code
                        }).then(function (data) {
                            if (data.DataResult.Success) {
                                CommonService.successMessage(data.DataResult.Message);
                                $scope.search();
                            }
                            else {
                                CommonService.warningMessage(data.DataResult.Message);
                            }
                        });

                    }, item.Code);
                }

                //#endregion

                //#region Reactivate / Deactivate

                $scope.toggleStatus = function (item) {
                    CommonService.reactivateDeactivate(function () {
                        TransactionControlNumberService.ToggleStatus({ id: item.TransactionControlNumberID, code: item.Code, status: item.IsActive })
                            .then(function (data) {
                                if (data.DataResult.Success) {
                                    CommonService.successMessage(data.DataResult.Message);
                                    $scope.search();
                                }
                                else {
                                    CommonService.warningMessage(data.DataResult.Message);
                                }

                                $scope.search();

                            }), function (error /*Error event should handle here*/) {
                                console.log(error);
                                CommonService.errorMessage("Unexpected error occured.");
                            }, function (data /*Notify event should handle here*/) {
                            };
                    }, item.Code, item.IsActive);
                }

                //#endregion

                $scope.addOrUpdate = function (id) {
                    CommonService.showLoading();
                    if (id === 0) {
                        $window.location.href = document.TransactionControlNumber + "Create";
                    }
                    else {
                        $window.location.href = document.TransactionControlNumber + "Edit/" + id;
                    }
                };

            }
        ])
        .controller('TransactionControlNumberFormCtrl', ['$scope', 'NgTableParams', '$uibModal', '$window', '$timeout', '$q', 'TransactionControlNumberService', 'CommonService', 'ItemGroup.GL.Model', 'CoaService',
            function ($scope, NgTableParams, $uibModal, $window, $timeout, $q, $tcs, $cs, $ig, $coa) {

                var transactionControlNumberId = angular.element("#TransactionControlNumberId").val();
                $scope.isUpdate = transactionControlNumberId != 0;
                $scope.forView = angular.element("#ForView").val();
                $scope.formSubmitted = false;

                $scope.data = {
                    ExpenseAcct: "",
                    RevenueAcct: "",
                    InvntryAcct: "",
                    COGSAcct: "",
                    AllocationAcct: "",
                    VarianceAcct: "",
                    PriceDiffAcct: "",
                    NegInvntryAcct: "",
                    InvntryOffsetDecAcct: "",
                    InvntryOffsetIncAcct: "",
                    SalReturns: "",
                    ExpenseAcctF: "",
                    RevenueAcctF: "",
                    ExhangeDiffAcct: "",
                    GoodClearingAcct: "",
                    GLOffsetDecAcct: "",
                    GLOffsetIncAcct: "",
                    WIPInvntryAcct: "",
                    WIPInvntryVarianceAcct: "",
                    ExpenseClearingAcct: "",
                    SalesCreditAcct: "",
                    PurchaseCreditAcct: "",
                    SalesCreditAcctF: "",
                    PurchaseCreditAcctF: "",

                    Code: "",
                    NameOrPurpose: "",
                    MatCode: "",
                    ProjectId: null
                }

                this.$onInit = function () {

                    $scope.tableParams = new NgTableParams({ count: 25 }, { dataset: $ig.ItemGroupGLSetupList });

                   
                    if (!$scope.isUpdate) {

                    } GetProjectLookUp(null);
                   
                }

                if (transactionControlNumberId != 0) {
                    $tcs.GetTCNById(transactionControlNumberId).then(function (d) {
                        if (d.StatusCode === '0') {
                            $scope.data = d.DataResult;
                            $scope.tableParams.data.forEach(function (ig) {
                                if ($scope.data[ig.Code] != '' && $scope.data[ig.Code] != null) {
                                    $tcs.GetAccountByAcctCodeSearch($scope.data[ig.Code])
                                        .then(function (res) {
                                            if (res.StatusCode === '0') {
                                                ig.AcctCode = res.DataResult.AcctCode
                                                ig.AcctName = res.DataResult.AcctName
                                            }
                                        })
                                }
                            });
                        }
                        GetProjectLookUp($scope.data.ProjectId);
                        $timeout(function () {
                            $scope.form.$pristine = true;
                        }, 50);

                    });
                }
                else {
                    $ig.ItemGroupGLSetupList.forEach(function (w) {
                        w.AcctCode = ''
                        w.AcctName = ''
                    })
                }

                $scope.btnCancel = function (s, e, data) {

                    if (!$scope.form.$pristine) {
                        $cs.cancelChanges(function () {
                            $window.location.href = document.TransactionControlNumber + "Index";// back to list
                        })
                    }
                    else {
                        $window.location.href = document.TransactionControlNumber + "Index";// back to list
                    }

                }

                $scope.btnSave = function (s, e, data) {

                    $scope.formSubmitted = true;
                    for (var i = 0; i <= $ig.ItemGroupGLSetupList.length - 1; i++) {
                        if ($ig.ItemGroupGLSetupList[i].AcctCode != null && $ig.ItemGroupGLSetupList[i].AcctCode != '') {
                            validateGLAccount($ig.ItemGroupGLSetupList[i]);
                        }
                        else {
                            $ig.ItemGroupGLSetupList[i].AcctName = ''
                        }
                    }
                    $timeout(function () {
                        var ifHasInvalidAcctCode = $ig.ItemGroupGLSetupList.findIndex(r => r.acctCodeNotValid);
                        if (ifHasInvalidAcctCode != -1) {
                            angular.element('#acctCode' + ifHasInvalidAcctCode).focus();
                        }
                        $scope.isPost = true;

                        if ($scope.form.$error.required !== undefined) {
                            angular.element('input.ng-invalid').first().focus();
                            return;
                        }
                        var validate = function () {
                            $tcs.CheckTCNName({ name: $scope.data.NameOrPurpose, id: transactionControlNumberId }).then(function (data) {

                                if (data.DataResult) {
                                    $cs.warningMessage("Name/Purpose already exist");
                                    angular.element('#NameOrPurpose').focus();
                                    $scope.nameAlreadyExist = true;
                                }
                                else {
                                    $scope.nameAlreadyExist = false;
                                }
                                $tcs.CheckTCNCode({ code: $scope.data.Code, id: transactionControlNumberId }).then(function (data) {
                                    if (data.DataResult) {
                                        $cs.warningMessage("Code already exist");
                                        angular.element('#Code').focus();
                                        $scope.codeAlreadyExist = true;
                                    }
                                    else {

                                        $scope.codeAlreadyExist = false;
                                    }
                                    if (!$scope.codeAlreadyExist && !$scope.nameAlreadyExist && !$scope.form.$invalid && ifHasInvalidAcctCode == -1) {
                                        $cs.saveOrUpdateChanges(function () {

                                            const t = $scope.tableParams;
                                            $scope.data.ExpenseAcct = t.settings().dataset[0].AcctCode;

                                            $scope.data.RevenueAcct = t.settings().dataset[1].AcctCode;

                                            $scope.data.InvntryAcct = t.settings().dataset[2].AcctCode

                                            $scope.data.COGSAcct = t.settings().dataset[3].AcctCode;

                                            $scope.data.AllocationAcct = t.settings().dataset[4].AcctCode;

                                            $scope.data.VarianceAcct = t.settings().dataset[5].AcctCode;

                                            $scope.data.PriceDiffAcct = t.settings().dataset[6].AcctCode;

                                            $scope.data.NegInvntryAcct = t.settings().dataset[7].AcctCode;

                                            $scope.data.InvntryOffsetDecAcct = t.settings().dataset[8].AcctCode;

                                            $scope.data.InvntryOffsetIncAcct = t.settings().dataset[9].AcctCode;

                                            $scope.data.SalReturns = t.settings().dataset[10].AcctCode;

                                            $scope.data.ExpenseAcctF = t.settings().dataset[11].AcctCode;

                                            $scope.data.RevenueAcctF = t.settings().dataset[12].AcctCode;

                                            $scope.data.ExhangeDiffAcct = t.settings().dataset[13].AcctCode;

                                            $scope.data.GoodClearingAcct = t.settings().dataset[14].AcctCode;

                                            $scope.data.GLOffsetDecAcct = t.settings().dataset[15].AcctCode;

                                            $scope.data.GLOffsetIncAcct = t.settings().dataset[16].AcctCode;

                                            $scope.data.WIPInvntryAcct = t.settings().dataset[17].AcctCode;

                                            $scope.data.WIPInvntryVarianceAcct = t.settings().dataset[18].AcctCode;

                                            $scope.data.ExpenseClearingAcct = t.settings().dataset[19].AcctCode;

                                            $scope.data.SalesCreditAcct = t.settings().dataset[20].AcctCode;

                                            $scope.data.PurchaseCreditAcct = t.settings().dataset[21].AcctCode;

                                            $scope.data.SalesCreditAcctF = t.settings().dataset[22].AcctCode;

                                            $scope.data.PurchaseCreditAcctF = t.settings().dataset[23].AcctCode;

                                            if ($scope.isUpdate) {
                                                $tcs.SaveOrUpdate($scope.data).then(function (d) {
                                                    $cs.successMessage('Item Group  Updated Sucessfully');
                                                    $timeout(function () {
                                                        $window.location.href = document.TransactionControlNumber;
                                                    }, 800);
                                                }, function (e) { });
                                            }
                                            else {
                                                $tcs.Add($scope.data).then(function (d) {
                                                    $cs.successMessage('Item Group  Added Sucessfully');
                                                    $timeout(function () {
                                                        $window.location.href = document.TransactionControlNumber;
                                                    }, 800);
                                                }, function (e) { });
                                            }

                                        }, $scope.isUpdate ? 1 : 0)
                                    } else {
                                        angular.element('input.ng-invalid').first().focus();
                                    }
                                });
                            });

                        }
                        validate();
                    }, 50);
                }

                $scope.onTabKeyDown = function (s, e, data, index) {
                    if (e.keyCode == 13 || e.keyCode == 9) {
                        $cs.GetGlAccountByAcctCode({ searcher: data.AcctCode }).then(function (res) {
                            data.noAcctFound = false;
                            if (res.DataResult.length == 1) {
                                data.AcctCode = res.DataResult[0].AcctCode
                                data.AcctName = res.DataResult[0].AcctName
                                data.acctCodeNotValid = false;
                            }

                            else if (res.DataResult.length >= 2) {
                                openCFL(data, data.AcctCode);
                                $scope.form.$invalid = false;
                            }
                            else {
                                $scope.form.$invalid = true;
                                data.noAcctFound = true;
                            }

                        })
                    }
                }

                $scope.onSearchButtonClick = function (s, e, data) {
                    openCFL(data)
                }

                var openCFL = function (data, code) {
                    $uibModal.open({
                        templateUrl: 'ChooseFromListPartial.html',
                        controller: 'ChooseFromListCtrl',
                        controllerAs: 'ctrl',
                        windowClass: 'modal_style',
                        backdrop: 'static',
                        keyboard: false,
                        resolve: {
                            modalData: function () {
                                return {
                                    'LookupType': 'ACT',
                                    'Tittle': 'Choose From List (G/L Accounts)',
                                    "SearchText": code
                                };
                            }
                        } // data passed to the controller
                    }).result.then(function (d) {
                        data.noAcctFound = false;
                        data.AcctCode = d.AcctCode
                        data.AcctName = d.AcctName
                        data.acctCodeNotValid = false;
                    }, function () { });
                }

                $scope.validateOnBlur = function (data) {
                    $cs.GetGlAccountByAcctCode({ searcher: data.AcctCode }).then(function (res) {
                        data.noAcctFound = false;
                        if (res.DataResult.length == 1) {
                            data.AcctCode = res.DataResult[0].AcctCode
                            data.AcctName = res.DataResult[0].AcctName
                            data.acctCodeNotValid = false;
                        }
                        else {
                            if (data.AcctCode != '') {
                                data.noAcctFound = true;
                            }

                        }
                    })
                }

                function validateGLAccount(data) {
                    $cs.GetGlAccountByAcctCode({ searcher: data.AcctCode }).then(function (res) {
                        if (res.DataResult.length == 1) {
                            data.AcctCode = res.DataResult[0].AcctCode;
                            data.AcctName = res.DataResult[0].AcctName;
                            data.acctCodeNotValid = false;
                        }
                        else {
                            $scope.form.$invalid = true;
                            data.AcctName = '';
                            data.acctCodeNotValid = true;
                        }
                    });
                }

                function GetProjectLookUp(id) {
                    $cs.GetProjectLookUp({}, "FinancialProjectsId").then(function (data) {
                        $scope.projects = data.result;

                        if (id != null) {
                            $scope.data.ProjectId = id;
                        }
                    });
                }

            }
        ]);

}());