(function () {

    var app = angular.module('MetronicApp')

    app.requires.push.apply(app.requires, ['ngRoute', 'INVNTRY.MODELS', 'INVNTRY.DATASERVICE', 'FIN.DATASERVICE'])

    app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
        $routeProvider
            .when('/ItemGroupList', {
                templateUrl: 'ItemGroupListTemplate.html',
                controller: 'ItemGroupListCtrl'
            })
            .when('/ItemGroupDetail/:ItemGroupCode/:ItemGroupID', {
                templateUrl: 'ItemGroupDetailTemplate.html',
                controller: 'ItemGroupDetailCtrl'
            })
            .when('/ItemGroupDetail/:ItemGroupCode', {
                templateUrl: 'ItemGroupDetailTemplate.html',
                controller: 'ItemGroupDetailCtrl'
            })
            .otherwise({
                templateUrl: 'ItemGroupListTemplate.html',
                controller: 'ItemGroupListCtrl'
            })
        $locationProvider.html5Mode(false).hashPrefix('!');

        //$locationProvider.html5Mode(true);
        ////use the HTML5 History API
        //$locationProvider.html5Mode({
        //    enabled: true,
        //    requireBase: true
        //});
        //$locationProvider.html5Mode(false).hashPrefix('!');
    }])
        .controller('ItemGroupListCtrl', ['$scope', 'NgTableParams', '$window', 'CommonService', 'ItemGroupService', '$q', function ($scope, NgTableParams, $window, $cs, $igs, $q) {

            angular.element('#breadcrums').text("List")

            $igs.GetCurrentAppUserId().then(function (data) {
                $scope.appuserId = data.result;
            });
            this.$onInit = function () {

                $scope.reset();
                GetValuationMethods();
                GetProcurementMethods();
            }

            $scope.search = function () {

                var initialSettings = {
                    getData: function (params) {
                        for (var i in params.sorting()) {
                            $scope.sortColumn = i;
                            $scope.sortOrder = params.sorting()[i];
                        }
                        var d = $q.defer();
                        $igs.SearchItemGroup({
                            ActionBy: $scope.actionBy,
                            Code: $scope.itemGroupCode,
                            Name: $scope.itemGroupName,
                            AppuserId: $scope.appuserId,
                            ValuationMethodId: $scope.valuationMethodId,
                            ProcurementMethodId: $scope.procurementMethodId,
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
                $scope.tableParams = new NgTableParams(10, initialSettings);
            }
            $scope.exportDataToExcelFile = function () {
                if ($scope.resultsLength > 0) {
                    $cs.showLoading();
                    $igs.ExportItemGroup({
                        ActionBy: $scope.actionBy,
                        Code: $scope.itemGroupCode,
                        Name: $scope.itemGroupName,
                        AppuserId: $scope.appuserId,
                        ValuationMethodId: $scope.valuationMethodId,
                        ProcurementMethodId: $scope.procurementMethodId,
                        SortDirection: $scope.sortOrder,
                        SortColumn: $scope.sortColumn,
                        CreatedOnFrom: getDateRangePickerValue(1, $scope.createdDate),
                        CreatedOnTo: getDateRangePickerValue(2, $scope.createdDate),
                    }).then(function (data) {
                        $cs.hideLoading();
                        window.location.href = document.FileUpload + "DownloadExcelFile?fileName=" + data.data.FileName;

                    }, function (error /*Error event should handle here*/) {
                        console.log("Error");
                    }, function (data /*Notify event should handle here*/) {
                        console.log("Error");
                    });
                }
            };
            $scope.searchWhenEnter = function ($event) {
                var keyCode = $event.which || $event.keyCode;
                if (keyCode === 13) {
                    $scope.search();
                }
            }
            $scope.reset = function () {
                $scope.itemGroupName = "";
                $scope.itemGroupName = "";
                $scope.sortColumn = "ActionOn";
                $scope.sortOrder = "desc";
                $scope.itemGroupCode = "";
                $scope.actionBy = "";
                $scope.createdDate = null;
                $scope.search();
                $scope.procurementMethodId = null;
                $scope.valuationMethodId = null;
            }
            $scope.reset();
            GetValuationMethods();
            GetProcurementMethods();


            $scope.onEditClick = function (d) {
                $window.location.href = '#!/ItemGroupDetail/' + d.ItemGroupCode + '/' + d.ItemGroupID
            }

            $scope.onDeleteClick = function (d) {
                $igs.CheckItemGroupIfCanDelete({ id: d.ItemGroupID }).then(function (data) {
                    if (!data.result) {
                        $cs.deleteChanges(function () {
                            updateWhsStatus(d, 'D')
                            $cs.successMessage(d.ItemGroupName + ' has been deleted!')
                        }, d.ItemGroupName)
                    }
                    else {
                        $cs.warningMessage('Unable to delete because item group is in use!')
                    }
                })

            }

            $scope.onDeactivate = function (d) {
                $cs.deActivate(function () {
                    updateWhsStatus(d, false)
                    $cs.successMessage('Item Group successfully deactivated')
                }, d.ItemGroupName)
            }

            $scope.onActiveClick = function (d) {
                $cs.reActivate(function () {
                    updateWhsStatus(d, true)
                    $cs.successMessage('Item Group successfully reactivated')

                }, d.ItemGroupName)
            }

            var updateWhsStatus = function (d, s) {
                var update_status = {
                    "ItemGroupCode": d.ItemGroupCode,
                    "Active": s
                }
                $igs.UpdateStatus(update_status).then(function (d) {
                    $scope.search();
                })
            }

            //#region Private Functions

            function GetProcurementMethods() {
                $igs.GetProcurementMethodsLookup({}).then(function (data) {
                    $scope.procurementMethods = data.data;
                }, function (error /*Error event should handle here*/) {
                    console.log("Error");
                }, function (data /*Notify event should handle here*/) {
                });
            }

            function GetValuationMethods() {
                $igs.GetValuationMethodsLookup({}).then(function (data) {
                    $scope.valuationMethods = data.data;
                }, function (error /*Error event should handle here*/) {
                    console.log("Error");
                }, function (data /*Notify event should handle here*/) {
                });
            }

            //#endregion
        }])
        .controller('ItemGroupDetailCtrl', ['$scope', 'NgTableParams', '$uibModal', '$window', '$q', '$routeParams', 'CommonService', 'ItemGroup.GL.Model', 'ItemGroupService', 'CoaService', '$timeout'
            , function ($scope, NgTableParams, $uibModal, $window, $q, $routeParams, $cs, $ig, $igs, $coa, $timeout) {

                var itemGroupCode = $routeParams.ItemGroupCode;
                var itemGroupID = $routeParams.ItemGroupID;

                $scope.isUpdate = itemGroupCode != -1;
                angular.element('#breadcrums').text($scope.isUpdate ? "Update" : "Add")
                //var gl_list = $ig.ItemGroupGLSetupList.slice(0)
                var loadTable = function () {
                    $scope.tableParams = new NgTableParams({ count: 25 }, { dataset: $ig.ItemGroupGLSetupList });

                    GetOrderIntervals();
                    GetCycleGroups();
                    GetValuationMethods();
                    GetProcurementMethods();

                }

                loadTable()
                $scope.checkAcctCode = function (data) {
                    data.AcctName = '';
                    data.acctCodeNotValid = true;
                }
                $scope.m = {

                    "ExpenseAcct": "",
                    "RevenueAcct": "",
                    "InvntryAcct": "",
                    "COGSAcct": "",
                    "AllocationAcct": "",
                    "VarianceAcct": "",
                    "PriceDiffAcct": "",
                    "NegInvntryAcct": "",
                    "InvntryOffsetDecAcct": "",
                    "InvntryOffsetIncAcct": "",
                    "SalReturns": "",
                    "ExpenseAcctF": "",
                    "RevenueAcctF": "",
                    "ExhangeDiffAcct": "",
                    "GoodClearingAcct": "",
                    "GLOffsetDecAcct": "",
                    "GLOffsetIncAcct": "",
                    "WIPInvntryAcct": "",
                    "WIPInvntryVarianceAcct": "",
                    "ExpenseClearingAcct": "",
                    "SalesCreditAcct": "",
                    "PurchaseCreditAcct": "",
                    "SalesCreditAcctF": "",
                    "PurchaseCreditAcctF": "",
                    "ProcurementMethodId": null,
                    "ValuationMethodId": null,
                    "IsActive": true,
                    "OrderMulti": "0.0000",
                    "MinOrderQty": "0.00",
                    "OrderIntervalId": null,
                    "CycleGroupId": null
                }


                if (itemGroupCode !== '-1') {
                    $igs.GetItemGroupByCode(itemGroupCode).then(function (d) {

                        if (d.StatusCode === '0') {
                            $scope.m = d.DataResult;
                            $scope.m.MinOrderQty = $scope.m.MinOrderQty.toFixed(2);
                            $scope.m.OrderMulti = $scope.m.OrderMulti.toFixed(4);
                            if (d.DataResult.OrderMulti == 0) {
                                $scope.m.OrderMulti = "0.0000";
                            }
                            if (d.DataResult.MinOrderQty == 0) {
                                $scope.m.MinOrderQty = "0.00";
                            }

                            $scope.tableParams.data.forEach(function (ig) {

                                if ($scope.m[ig.Code] != '' && $scope.m[ig.Code] != null) {
                                    $coa.GetAccountByAcctCodeSearch($scope.m[ig.Code])
                                        .then(function (res) {
                                            if (res.StatusCode === '0') {
                                                ig.AcctCode = res.DataResult.AcctCode
                                                ig.AcctName = res.DataResult.AcctName
                                            }
                                        })

                                }

                            });
                        }
                        $timeout(function () {
                            $scope.f.$pristine = true;
                        }, 50);

                    });

                }
                else {
                    $ig.ItemGroupGLSetupList.forEach(function (w) {
                        w.AcctCode = ''
                        w.AcctName = ''
                        $scope.tableParams.reload();
                    })
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
                                    "SearchText": code,
                                    'Postable': 'Y'
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
                                $scope.f.$invalid = false;
                            }
                            else {
                                $scope.f.$invalid = true;
                                data.noAcctFound = true;
                            }

                        })
                    }
                }

                $scope.onSearchButtonClick = function (s, e, data) {
                    openCFL(data)
                }

                $scope.isPost = false;

                $scope.btnSave = function (s, e, data) {
                    for (var i = 0; i <= $ig.ItemGroupGLSetupList.length - 1; i++) {
                        if ($ig.ItemGroupGLSetupList[i].AcctCode != null && $ig.ItemGroupGLSetupList[i].AcctCode != '')
                            validateGLAccount($ig.ItemGroupGLSetupList[i]);
                    }
                    $timeout(function () {
                        var ifHasInvalidAcctCode = $ig.ItemGroupGLSetupList.findIndex(r => r.acctCodeNotValid);
                        if (ifHasInvalidAcctCode != -1) {
                            angular.element('#acctCode' + ifHasInvalidAcctCode).focus();
                        }
                        $scope.isPost = true;

                        //if ($scope.f.$error.required.length > 0) {
                        if ($scope.f.$error.required !== undefined) {
                            angular.element('input.ng-invalid').first().focus();
                            return;
                        }
                        var validate = function () {
                            $igs.CheckItemGroupName({ name: $scope.m.ItemGroupName, id: itemGroupID ?? 0 }).then(function (data) {

                                if (data.DataResult) {
                                    $cs.warningMessage("Item group name already exist");
                                    angular.element('#itemGroupName').focus();
                                    $scope.nameAlreadyExist = true;
                                }
                                else {
                                    $scope.nameAlreadyExist = false;
                                }
                                $igs.CheckItemGroupCode({ code: $scope.m.ItemGroupCode, id: itemGroupID ?? 0 }).then(function (data) {
                                    if (data.DataResult) {
                                        $cs.warningMessage("Item group code already exist");
                                        angular.element('#itemGroupCode').focus();
                                        $scope.codeAlreadyExist = true;
                                    }
                                    else {

                                        $scope.codeAlreadyExist = false;
                                    }
                                    if (!$scope.codeAlreadyExist && !$scope.nameAlreadyExist && !$scope.f.$invalid && ifHasInvalidAcctCode == -1) {
                                        $cs.saveOrUpdateChanges(function () {

                                            const t = $scope.tableParams;
                                            $scope.m.ExpenseAcct = t.settings().dataset[0].AcctCode;

                                            $scope.m.RevenueAcct = t.settings().dataset[1].AcctCode;

                                            $scope.m.InvntryAcct = t.settings().dataset[2].AcctCode

                                            $scope.m.COGSAcct = t.settings().dataset[3].AcctCode;

                                            $scope.m.AllocationAcct = t.settings().dataset[4].AcctCode;

                                            $scope.m.VarianceAcct = t.settings().dataset[5].AcctCode;

                                            $scope.m.PriceDiffAcct = t.settings().dataset[6].AcctCode;

                                            $scope.m.NegInvntryAcct = t.settings().dataset[7].AcctCode;

                                            $scope.m.InvntryOffsetDecAcct = t.settings().dataset[8].AcctCode;

                                            $scope.m.InvntryOffsetIncAcct = t.settings().dataset[9].AcctCode;

                                            $scope.m.SalReturns = t.settings().dataset[10].AcctCode;

                                            $scope.m.ExpenseAcctF = t.settings().dataset[11].AcctCode;

                                            $scope.m.RevenueAcctF = t.settings().dataset[12].AcctCode;

                                            $scope.m.ExhangeDiffAcct = t.settings().dataset[13].AcctCode;

                                            $scope.m.GoodClearingAcct = t.settings().dataset[14].AcctCode;

                                            $scope.m.GLOffsetDecAcct = t.settings().dataset[15].AcctCode;

                                            $scope.m.GLOffsetIncAcct = t.settings().dataset[16].AcctCode;

                                            $scope.m.WIPInvntryAcct = t.settings().dataset[17].AcctCode;

                                            $scope.m.WIPInvntryVarianceAcct = t.settings().dataset[18].AcctCode;

                                            $scope.m.ExpenseClearingAcct = t.settings().dataset[19].AcctCode;

                                            $scope.m.SalesCreditAcct = t.settings().dataset[20].AcctCode;

                                            $scope.m.PurchaseCreditAcct = t.settings().dataset[21].AcctCode;

                                            $scope.m.SalesCreditAcctF = t.settings().dataset[22].AcctCode;

                                            $scope.m.PurchaseCreditAcctF = t.settings().dataset[23].AcctCode;

                                            if (itemGroupCode !== '-1') {
                                                $igs.SaveOrUpdate($scope.m).then(function (d) {
                                                    $cs.successMessage('Item Group has been successfully updated');
                                                    loadTable()
                                                    $window.location.href = '#!/ItemGroupList' // back to list
                                                }, function (e) { });
                                            }
                                            else {
                                                $igs.Add($scope.m).then(function (d) {
                                                    $cs.successMessage('tem Group has been successfully added');
                                                    loadTable()
                                                    $window.location.href = '#!/ItemGroupList' // back to list

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
                $scope.btnCancel = function (s, e, data) {

                    if (!$scope.f.$pristine) {
                        $cs.cancelChanges(function () {
                            loadTable();
                            $window.location.href = document.ItemGroup + "Index";// back to list
                        })
                    }
                    else {
                        loadTable();
                        $window.location.href = document.ItemGroup + "Index";// back to list
                    }

                }

                function GetOrderIntervals() {
                    $igs.GetOrderIntervalListLookup({}).then(function (data) {
                        $scope.ordeIntervals = data.DataResult;
                    }, function (error /*Error event should handle here*/) {
                        console.log("Error");
                    }, function (data /*Notify event should handle here*/) {
                    });
                }

                function GetProcurementMethods() {
                    $igs.GetProcurementMethodsLookup({}).then(function (data) {
                        $scope.procurementMethods = data.data;
                        $scope.m.ProcurementMethodId = $scope.procurementMethods[0].Id;
                    }, function (error /*Error event should handle here*/) {
                        console.log("Error");
                    }, function (data /*Notify event should handle here*/) {
                    });
                }

                function GetValuationMethods() {
                    $igs.GetValuationMethodsLookup({}).then(function (data) {
                        $scope.valuationMethods = data.data;
                        $scope.m.ValuationMethodId = $scope.valuationMethods[2].Id;
                    }, function (error /*Error event should handle here*/) {
                        console.log("Error");
                    }, function (data /*Notify event should handle here*/) {
                    });
                }

                function GetCycleGroups() {
                    $igs.GetCycleGroupListLookup({}).then(function (data) {
                        $scope.cycleGroups = data.data;
                    }, function (error /*Error event should handle here*/) {
                        console.log("Error");
                    }, function (data /*Notify event should handle here*/) {
                    });
                }
                function validateGLAccount(data) {
                    $cs.GetGlAccountByAcctCode({ searcher: data.AcctCode }).then(function (res) {
                        if (res.DataResult.length == 1) {
                            data.AcctCode = res.DataResult[0].AcctCode;
                            data.AcctName = res.DataResult[0].AcctName;
                            data.acctCodeNotValid = false;
                        }
                        else {
                            $scope.f.$invalid = true;
                            data.AcctName = '';
                            data.acctCodeNotValid = true;
                        }
                    });
                }

            }])
}());