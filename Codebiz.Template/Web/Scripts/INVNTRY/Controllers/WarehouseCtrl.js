(function () {

    var app = angular.module('MetronicApp')

    app.requires.push.apply(app.requires, ['ngRoute', 'INVNTRY.MODELS', 'INVNTRY.DATASERVICE', 'FIN.DATASERVICE'])

    app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
        $routeProvider
            .when('/WarehouseList', {
                templateUrl: 'WarehouseListTemplate.html',
                controller: 'WarehouseListCtrl'
            })
            .when('/WarehouseDetail/:WhsCode/:ViewOnly', {
                templateUrl: 'WarehouseDetailTemplate.html',
                controller: 'WarehouseDetailCtrl'
            })

            .when('/WarehouseDetail/Code/:WhsCode', {
                templateUrl: 'WarehouseDetailTemplate.html',
                controller: 'WarehouseDetailCtrl'
            })
            .when('/WarehouseDetail/ID/:WarehouseID/:ViewOnly', {
                templateUrl: 'WarehouseDetailTemplate.html',
                controller: 'WarehouseDetailCtrl'
            })
            .otherwise({
                templateUrl: 'WarehouseListTemplate.html',
                controller: 'WarehouseListCtrl'
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
        .controller('WarehouseListCtrl', ['$scope', 'NgTableParams', '$window', 'CommonService', 'WarehouseService', '$q', '$window', function ($scope, $ngTable, $window, $cs, $ws, $q, $window) {

            //#region Search/Export
            $scope.search = function (init) {
                var initialSettings = {
                    getData: function (params) {
                        for (var i in params.sorting()) {
                            $scope.sortColumn = i;
                            $scope.sortOrder = params.sorting()[i];
                        }
                        var d = $q.defer();
                        var filter = params.filter();

                        $ws.GetList({
                            page: params.page(),
                            pageSize: params.count(),
                            sortOrder: $scope.sortOrder,
                            sortColumn: $scope.sortColumn,
                            code: $scope.Code,
                            name: $scope.Name,
                            CreatedBy: $scope.CreatedBy,
                            CreatedOnFrom: getDateRangePickerValue(1, $scope.createdDate),
                            CreatedOnTo: getDateRangePickerValue(2, $scope.createdDate),
                            SortDirection: $scope.sortOrder,
                        }).then(function (data) {
                            $scope.wareHouse = data.DataResult;
                            $scope.resultsLength = data.TotalRecordCount;
                            params.total($scope.resultsLength);
                            d.resolve(data.DataResult);
                        });
                        return d.promise;
                    }
                };
                $scope.tableParams = new $ngTable(10, initialSettings);
            };

            $scope.exportDataToExcelFile = function () {
                if ($scope.resultsLength > 0) {
                    if ($scope.sortColumn === "ActionOn") {
                        $scope.sortColumn = "ActionOn";
                    }

                    $cs.showLoading();
                    $ws.ExportDataToExcelFile({
                        Code: $scope.Code,
                        Name: $scope.Name,
                        SortDirection: $scope.sortOrder,
                        SortColumn: $scope.sortColumn,
                        CreatedBy: $scope.CreatedBy,
                        CreatedOnFrom: getDateRangePickerValue(1, $scope.createdDate),
                        CreatedOnTo: getDateRangePickerValue(2, $scope.createdDate),
                        IsForExcelExport: true,
                        SortDirection: $scope.sortOrder,
                    }).then(function (data) {
                        $cs.hideLoading();
                        window.location.href = document.FileUpload + "DownloadExcelFile?fileName=" + data.DataResult.FileName;
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
            };

            //#endregion

            //Init
            $scope.$onInit = function () {
                $scope.Code = "";
                $scope.Name = "";
                $scope.CreatedBy = "";
                $scope.createdDate = null;

                $scope.sortOrder = "desc";
                $scope.sortColumn = "ActionOn";

                $scope.search(true);
            };

            $scope.$onInit();

            $scope.reset = function () {
                $scope.Code = "";
                $scope.Name = "";
                $scope.CreatedBy = "";
                $scope.createdDate = null;

                $scope.sortOrder = "desc";
                $scope.sortColumn = "ActionOn";

                $scope.search(true);
            };

            angular.element('#breadcrums').text("List");


            $scope.onEditClick = function (d) {
                $window.location.href = '#!/WarehouseDetail/' + d.WhsCode+'/'+false;
            };

            $scope.onDeleteClick = function (d) {
                $cs.deleteChanges(function () {
                    updateWhsStatus(d, 'D')
                    $cs.successMessage('Warehouse Deleted');
                    loadTable();
                }, d.WhsName)
            };

            $scope.onDeactivate = function (d) {
                $cs.deActivate(function () {
                    updateWhsStatus(d, 'N');
                    $cs.successMessage('Warehouse Deactivated');
                }, d.WhsName);
            };

            $scope.onActiveClick = function (d) {
                $cs.reActivate(function () {
                    updateWhsStatus(d, 'Y');
                    $cs.successMessage('Warehouse Reactivated');
                }, d.WhsName);
            };

            var updateWhsStatus = function (d, s) {
                var update_status = {
                    "WhsCode": d.WhsCode,
                    "Active": s
                };
                var res = $ws.UpdateStatus(update_status).then(function (d) {
                    if (d.StatusMessage == "Success") {
                        $scope.$onInit();
                    }
                    return d;
                });
            };

            //loadTable();
        }])

        .controller('WarehouseDetailCtrl', ['$scope', 'NgTableParams', '$uibModal', '$window', '$timeout', '$q', '$routeParams', 'CommonService', 'Warehouse.GL.Model', 'WarehouseService', 'CoaService'
            , function ($scope, NgTableParams, $uibModal, $window, $timeout, $q, $routeParams, $cs, $wh, $ws, $coa) {
                var whsCode = $routeParams.WhsCode
                $scope.viewOnly = $routeParams.ViewOnly == 'false' ? false:true;
                $scope.addEdit = whsCode;
                $scope.linkOnly = $routeParams.LinkOnly == 'false' ? false : true;

                angular.element('#breadcrums').text("Detail")
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
                    "Active": "Y",
                    "IsNettable": "true",
                    "IsDropShip": "false"
                }

                $scope.HasValidAccount = true;
                $scope.AccountNotFound = [];
                $scope.m.AcctCode = [];

                var loadTable = function () {
                    $scope.setUpList = angular.copy($wh.WarehouseGLSetupList);

                    if (whsCode !== '-1') {

                        $ws.GetWarehouseByCode(whsCode).then(function (d) {
                            if (d.StatusCode === '0') {
                                $scope.m = d.DataResult
                                $scope.m.AcctCode = [];
                                console.log($scope.m)

                                $scope.setUpList.forEach(function (whs) {
                                    if ($scope.m[whs.Code] !== '') {
                                        $coa.GetAccountByAcctCodeSearch($scope.m[whs.Code])
                                            .then(function (res) {
                                                if (res.StatusCode == '0') {
                                                    if (res.DataResult != null) {
                                                        whs.AcctCode = res.DataResult.AcctCode
                                                        whs.AcctName = res.DataResult.AcctName
                                                    }
                                                }
                                            })
                                    }
                                });
                            }
                        });

                        $scope.tableParams = new NgTableParams({}, { counts: [], dataset: $scope.setUpList });
                    }
                    else {
                        $wh.WarehouseGLSetupList.forEach(function (w) {
                            w.AcctCode = ''
                            w.AcctName = ''
                        });

                    }
                }

                loadTable();

                var openCFL = function (data,index,code) {
                    $uibModal.open({
                        templateUrl: 'ChooseFromListPartial.html',
                        controller: 'ChooseFromListCtrl',
                        controllerAs: 'ctrl',
                        windowClass: 'modal_dialog',
                        backdrop: 'static',
                        keyboard: false,
                        resolve: {
                            modalData: function () {
                                return {
                                    'LookupType': 'ACT',
                                    'Tittle': 'Choose From List (G/L Accounts)',
                                    'SearchText': code,
                                    'Postable': 'Y'
                                };
                            }
                        } // data passed to the controller
                    }).result.then(function (d) {
                        //e.target.value = d.AcctCode
                        data.AcctCode = d.AcctCode
                        data.AcctName = d.AcctName
                        $scope.m.AcctCode[index] = d.AcctCode;
                        $scope.AccountNotFound[index] = false;

                    }, function () { });
                }

                checkIfHasInvalidAccount = function () {
                    for (var i = 0; i < $scope.setUpList.length; i++) {
                        if ($scope.AccountNotFound[i]) {
                            $scope.HasValidAccount = false;
                            break;
                        }
                        else {
                            $scope.HasValidAccount = true;
                        }
                    }
                }

                $scope.searchCFL = function($event,index,data) {
                    var evt = $event || window.event;
                    var keyCode = evt.which || evt.keyCode;
                    if (keyCode === 13 || keyCode === 9) {
                        if ($scope.m.AcctCode[index] !== null && $scope.m.AcctCode[index] !== "") {
                            $ws.SearchAccount($scope.m.AcctCode[index])
                                .then(function (d) {
                                    if (d.DataResult.length < 1) {
                                        $scope.AccountNotFound[index] = true;
                                        if ($scope.m.AcctCode[index] != undefined) {
                                            data.AcctCode = "";
                                            data.AcctName = "";
                                        }
                                      
                                    }
                                    else {
                                        $scope.AccountNotFound[index] = false;
                                        data.AcctCode = d.DataResult[0].AcctCode;
                                        data.AcctName = d.DataResult[0].AcctName;
                                    }
                            });
                        }
                        else {
                            //$scope.memberDetails.MemberName = "";
                            //$scope.vehicleRequest.CoopVehicleId = null;
                            //$scope.memberDetails.MembershipType = "";
                            //$scope.memberDetails.MemberPhotoThumbnailUrl = "";
                        }
                    }
                }

                $scope.searchCFLBlur = function (index, data) {
                    if ($scope.m.AcctCode[index] !== undefined) {
                        $ws.SearchAccount($scope.m.AcctCode[index])
                            .then(function (d) {
                                if (d.DataResult.length < 1) {
                                    $scope.AccountNotFound[index] = true;
                                    if ($scope.m.AcctCode[index] != undefined) {
                                        data.AcctCode = "";
                                        data.AcctName = "";
                                    }

                                }
                                else {
                                    $scope.AccountNotFound[index] = false;
                                    data.AcctCode = d.DataResult[0].AcctCode;
                                    data.AcctName = d.DataResult[0].AcctName;
                                }
                            });
                    }
                };

                $scope.onTabKeyDown = function (s, e, data, index) {
                   if (e.keyCode == 13 || e.keyCode === 9) {
                        $cs.GetGlAccountByAcctCode({ searcher: data.AcctCode }).then(function (res) {
                            data.noAcctFound = false;
                            if (res.DataResult.length == 1) {
                                data.AcctCode = res.DataResult[0].AcctCode
                                data.AcctName = res.DataResult[0].AcctName
                                data.acctCodeNotValid = false;
                            }

                            else if (res.DataResult.length >= 2) {
                                openCFL(data,index,data.AcctCode);
                                $scope.f.$invalid = false;
                            }
                            else {
                                $scope.f.$invalid = true;
                                data.noAcctFound = true;
                            }

                        })
                    }
                }

                $scope.onSearchButtonClick = function (s, e, data,index) {
                    openCFL(data,index)
                }

                $scope.isPost = false;
                $scope.btnSave = function (s, e, data) {
                    $scope.isPost = true;
                    checkIfHasInvalidAccount();
                    //if ($scope.f.$error.required.length > 0) {
                    if ($scope.f.$error.required !== undefined || !$scope.HasValidAccount) {
                        if (angular.element('.ng-invalid').length) {
                            angular.element('.ng-invalid')[1].focus();
                        }
                        return;
                    }

                    if (whsCode !== '-1') {
                        $cs.updateChanges(function () {
                            addWarehouse();
                        })
                    }
                    else {
                        $cs.saveChanges(function () {
                            addWarehouse();

                        })
                    }
                }
                $scope.btnCancel = function (s, e, data) {
                    if (!$scope.f.$pristine) {
                        $cs.cancelChanges(function () {
                            loadTable();
                            $window.location.href = '#!/WarehouseList' // back to list
                        })
                    }
                    else {
                        loadTable();
                        $window.location.href = '#!/WarehouseList' // back to list
                    }
                 
                }

                function addWarehouse() {

                    $scope.m.ExpenseAcct = $scope.setUpList.filter((a) => a.Code === 'ExpenseAcct')[0].AcctCode

                    $scope.m.RevenueAcct = $scope.setUpList.filter((a) => a.Code === 'RevenueAcct')[0].AcctCode

                    $scope.m.InvntryAcct = $scope.setUpList.filter((a) => a.Code === 'InvntryAcct')[0].AcctCode

                    $scope.m.COGSAcct = $scope.setUpList.filter((a) => a.Code === 'COGSAcct')[0].AcctCode

                    $scope.m.AllocationAcct = $scope.setUpList.filter((a) => a.Code === 'AllocationAcct')[0].AcctCode

                    $scope.m.VarianceAcct = $scope.setUpList.filter((a) => a.Code === 'VarianceAcct')[0].AcctCode

                    $scope.m.PriceDiffAcct = $scope.setUpList.filter((a) => a.Code === 'PriceDiffAcct')[0].AcctCode

                    $scope.m.NegInvntryAcct = $scope.setUpList.filter((a) => a.Code === 'NegInvntryAcct')[0].AcctCode

                    $scope.m.InvntryOffsetDecAcct = $scope.setUpList.filter((a) => a.Code === 'InvntryOffsetDecAcct')[0].AcctCode

                    $scope.m.InvntryOffsetIncAcct = $scope.setUpList.filter((a) => a.Code === 'InvntryOffsetIncAcct')[0].AcctCode

                    $scope.m.SalReturns = $scope.setUpList.filter((a) => a.Code === 'SalReturns')[0].AcctCode

                    $scope.m.ExpenseAcctF = $scope.setUpList.filter((a) => a.Code === 'ExpenseAcctF')[0].AcctCode

                    $scope.m.RevenueAcctF = $scope.setUpList.filter((a) => a.Code === 'RevenueAcctF')[0].AcctCode

                    $scope.m.ExhangeDiffAcct = $scope.setUpList.filter((a) => a.Code === 'ExhangeDiffAcct')[0].AcctCode

                    $scope.m.GoodClearingAcct = $scope.setUpList.filter((a) => a.Code === 'GoodClearingAcct')[0].AcctCode

                    $scope.m.GLOffsetDecAcct = $scope.setUpList.filter((a) => a.Code === 'GLOffsetDecAcct')[0].AcctCode

                    $scope.m.GLOffsetIncAcct = $scope.setUpList.filter((a) => a.Code === 'GLOffsetIncAcct')[0].AcctCode

                    $scope.m.WIPInvntryAcct = $scope.setUpList.filter((a) => a.Code === 'WIPInvntryAcct')[0].AcctCode

                    $scope.m.WIPInvntryVarianceAcct = $scope.setUpList.filter((a) => a.Code === 'WIPInvntryVarianceAcct')[0].AcctCode

                    $scope.m.ExpenseClearingAcct = $scope.setUpList.filter((a) => a.Code === 'ExpenseClearingAcct')[0].AcctCode

                    $scope.m.SalesCreditAcct = $scope.setUpList.filter((a) => a.Code === 'SalesCreditAcct')[0].AcctCode

                    $scope.m.PurchaseCreditAcct = $scope.setUpList.filter((a) => a.Code === 'PurchaseCreditAcct')[0].AcctCode

                    $scope.m.SalesCreditAcctF = $scope.setUpList.filter((a) => a.Code === 'SalesCreditAcctF')[0].AcctCode

                    $scope.m.PurchaseCreditAcctF = $scope.setUpList.filter((a) => a.Code === 'PurchaseCreditAcctF')[0].AcctCode


                    if (whsCode !== '-1') {
                        $ws.SaveOrUpdate($scope.m).then(function (d) {
                            $cs.successMessage('Warehouse Updated Sucessfully');
                            loadTable()
                            $scope.isPost = false;
                            $window.location.href = '#!/WarehouseList' // back to list
                        }, function (e) { });
                    }
                    else {
                        $ws.Add($scope.m).then(function (d) {
                            $cs.successMessage('Warehouse Added Sucessfully');
                            loadTable()
                            $scope.isPost = false;
                            $window.location.href = '#!/WarehouseList' // back to list
                        }, function (e) { });
                    }
                }
              
            }])
}());