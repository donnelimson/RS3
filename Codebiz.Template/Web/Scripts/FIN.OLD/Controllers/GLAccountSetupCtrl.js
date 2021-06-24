(function () {
    var app = angular.module('MetronicApp')
    app.requires.push.apply(app.requires, ['FIN.MODELS', 'FIN.DATASERVICE'])
    app
        .controller('GLAccountSetupCtrl', ['$scope', '$window', '$q', 'NgTableParams', '$uibModal', 'CommonService', 'GL.SETUP.MODEL', 'GLSetupService', 'CoaService', function ($scope, $window, $q, $ngTable, $uibModal, $cs, $glModel, $glSetupSvc, $coaSvc) {

            (() => {
                $scope.GeneralGLSetupTableParams = new $ngTable({ count: 50 }, { dataset: $glModel.GeneralGLSetupList });

                $scope.InvntryGLSetupTableParams = new $ngTable({ count: 50 }, { dataset: $glModel.InvntryGLSetupList });

                $scope.ARGLSetupTableParams = new $ngTable({ count: 50 }, { dataset: $glModel.ARGLSetupList });

                $scope.APGLSetupTableParams = new $ngTable({ count: 50 }, { dataset: $glModel.APGLSetupList });


                $glSetupSvc.GetFinancialPeriodLookupList().then(function (d) {
                    if (d.StatusCode === '0') {
                        $scope.PeriodCategoryList = d.DataResult
                    }
                })

                $scope.m = {};
            })();


            $scope.isPeriodSelected = false;

            var openCFL = function (data) {
                $uibModal.open({
                    templateUrl: 'ChooseFromListPartial.html',
                    controller: 'ChooseFromListCtrl',
                    controllerAs: 'ctrl',
                    windowClass: 'clsPopup',
                    backdrop: 'static',
                    keyboard: false,
                    resolve: {
                        modalData: function () {
                            return {
                                'LookupType': 'ACT',
                                'Tittle': 'Choose From List (G/L Accounts)'
                            };
                        }
                    } // data passed to the controller
                }).result.then(function (d) {
                    //e.target.value = d.AcctCode
                    data.AcctCode = d.AcctCode
                    data.AcctName = d.AcctName
                }, function () { });
            }

            $scope.onTabKeyDown = function (s, e, data) {
                if (e.keyCode === 9) {   // tab key
                    e.preventDefault(); // dont exec ui focus
                    openCFL(data)
                }
            }

            $scope.onSearchButtonClick = function (s, e, data) {
                openCFL(data)
            }

            var saveGLSetup = function () {

                let g = $scope.GeneralGLSetupTableParams
                let i = $scope.InvntryGLSetupTableParams
                let ar = $scope.ARGLSetupTableParams
                let ap = $scope.APGLSetupTableParams

                /**
                 *  General
                 **/
                $scope.m.GAcctLink_1 = g.data.filter((a) => a.Code === 'CCDepositFee')[0].AcctCode;
                $scope.m.GAcctLink_2 = g.data.filter((a) => a.Code === 'RoundingAcct')[0].AcctCode;
                $scope.m.GAcctLink_3 = g.data.filter((a) => a.Code === 'AutoReconDiffAcct')[0].AcctCode;
                $scope.m.GAcctLink_4 = g.data.filter((a) => a.Code === 'PeriodCloseAcct')[0].AcctCode;
                $scope.m.GAcctLink_5 = g.data.filter((a) => a.Code === 'RealizedExchangeDiffGainAcct')[0].AcctCode;
                $scope.m.GAcctLink_6 = g.data.filter((a) => a.Code === 'RealizedExchangeDiffLossAcct')[0].AcctCode;
                $scope.m.GAcctLink_7 = g.data.filter((a) => a.Code === 'OpenningBalanceAcct')[0].AcctCode;

                /**
                 * Invntry
                 **/
                $scope.m.IAcctLink_1 = i.data.filter((a) => a.Code === 'InvntryAcct')[0].AcctCode;
                $scope.m.IAcctLink_2 = i.data.filter((a) => a.Code === 'COGSAcct')[0].AcctCode;
                $scope.m.IAcctLink_3 = i.data.filter((a) => a.Code === 'AllocationAcct')[0].AcctCode;
                $scope.m.IAcctLink_4 = i.data.filter((a) => a.Code === 'VarianceAcct')[0].AcctCode;
                $scope.m.IAcctLink_5 = i.data.filter((a) => a.Code === 'PriceDiffAcct')[0].AcctCode;
                $scope.m.IAcctLink_6 = i.data.filter((a) => a.Code === 'NegInvntryAdjAcct')[0].AcctCode;
                $scope.m.IAcctLink_7 = i.data.filter((a) => a.Code === 'InvntryOffsetDecAcct')[0].AcctCode;
                $scope.m.IAcctLink_8 = i.data.filter((a) => a.Code === 'InvntryOffsetIncAcct')[0].AcctCode;
                $scope.m.IAcctLink_9 = i.data.filter((a) => a.Code === 'ExchangeDiffAcct')[0].AcctCode;
                $scope.m.IAcctLink_10 = i.data.filter((a) => a.Code === 'GoodsClearingAcct')[0].AcctCode;
                $scope.m.IAcctLink_11 = i.data.filter((a) => a.Code === 'GLDecAcct')[0].AcctCode;
                $scope.m.IAcctLink_12 = i.data.filter((a) => a.Code === 'GLIncAcct')[0].AcctCode;
                $scope.m.IAcctLink_13 = i.data.filter((a) => a.Code === 'WIPInvntryAcct')[0].AcctCode;
                $scope.m.IAcctLink_14 = i.data.filter((a) => a.Code === 'WIPInvntryVarianceAcct')[0].AcctCode;
                $scope.m.IAcctLink_15 = i.data.filter((a) => a.Code === 'ExpenseClearingAcct')[0].AcctCode;


                /**
                 * AR
                 **/
                $scope.m.SAcctLink_1 = ar.data.filter((a) => a.Code === 'DomesticReceivableAcct')[0].AcctCode;
                $scope.m.SAcctLink_2 = ar.data.filter((a) => a.Code === 'ForeignReceivableAcct')[0].AcctCode;
                $scope.m.SAcctLink_3 = ar.data.filter((a) => a.Code === 'ChecksReceivedAcct')[0].AcctCode;
                $scope.m.SAcctLink_4 = ar.data.filter((a) => a.Code === 'CashOnHandAcct')[0].AcctCode;
                $scope.m.SAcctLink_5 = ar.data.filter((a) => a.Code === 'OverPaymentAcct')[0].AcctCode;
                $scope.m.SAcctLink_6 = ar.data.filter((a) => a.Code === 'UnderPaymentAcct')[0].AcctCode;
                $scope.m.SAcctLink_7 = ar.data.filter((a) => a.Code === 'DPClearingAcct')[0].AcctCode;
                $scope.m.SAcctLink_8 = ar.data.filter((a) => a.Code === 'ExchangeDiffGain')[0].AcctCode;
                $scope.m.SAcctLink_9 = ar.data.filter((a) => a.Code === 'ExchangeDiffLoss')[0].AcctCode;
                $scope.m.SAcctLink_10 = ar.data.filter((a) => a.Code === 'CashDiscount')[0].AcctCode;
                $scope.m.SAcctLink_11 = ar.data.filter((a) => a.Code === 'RevenueAcct')[0].AcctCode;
                $scope.m.SAcctLink_12 = ar.data.filter((a) => a.Code === 'RevenueAcctForeign')[0].AcctCode;
                $scope.m.SAcctLink_13 = ar.data.filter((a) => a.Code === 'SalesCreditAcct')[0].AcctCode;
                $scope.m.SAcctLink_14 = ar.data.filter((a) => a.Code === 'DPInterimAcct')[0].AcctCode;
                $scope.m.SAcctLink_15 = ar.data.filter((a) => a.Code === 'DunningInterestAcct')[0].AcctCode;
                $scope.m.SAcctLink_16 = ar.data.filter((a) => a.Code === 'DunningFeeAcct')[0].AcctCode;


                /**
                 * AP
                 **/
                $scope.m.PAcctLink_1 = ap.data.filter((a) => a.Code === 'DomesticPayableAcct')[0].AcctCode;
                $scope.m.PAcctLink_2 = ap.data.filter((a) => a.Code === 'ForeignPayableAcct')[0].AcctCode;
                $scope.m.PAcctLink_3 = ap.data.filter((a) => a.Code === 'ExchangeDiffGain')[0].AcctCode;
                $scope.m.PAcctLink_4 = ap.data.filter((a) => a.Code === 'ExchangeDiffLoss')[0].AcctCode;
                $scope.m.PAcctLink_5 = ap.data.filter((a) => a.Code === 'BankTransferAcct')[0].AcctCode;
                $scope.m.PAcctLink_6 = ap.data.filter((a) => a.Code === 'CashDiscountAcct')[0].AcctCode;
                $scope.m.PAcctLink_7 = ap.data.filter((a) => a.Code === 'CashDiscountClearingAcct')[0].AcctCode;
                $scope.m.PAcctLink_8 = ap.data.filter((a) => a.Code === 'ExpenseForeignAcct')[0].AcctCode;
                $scope.m.PAcctLink_8 = ap.data.filter((a) => a.Code === 'CreditAcct')[0].AcctCode;
                $scope.m.PAcctLink_10 = ap.data.filter((a) => a.Code === 'CreditForeignAcct')[0].AcctCode;
                $scope.m.PAcctLink_11 = ap.data.filter((a) => a.Code === 'OverPaymentAcct')[0].AcctCode;
                $scope.m.PAcctLink_12 = ap.data.filter((a) => a.Code === 'UnderPaymentAcct')[0].AcctCode;
                $scope.m.PAcctLink_13 = ap.data.filter((a) => a.Code === 'DPClearingAcct')[0].AcctCode;
                $scope.m.PAcctLink_14 = ap.data.filter((a) => a.Code === 'DPInterimAcct')[0].AcctCode;
                $scope.m.PAcctLink_15 = ap.data.filter((a) => a.Code === 'ExpenseAndInvntryAcct')[0].AcctCode;


                $glSetupSvc.SaveOrUpdate($scope.m)
                    .then(function (d) {
                        if (d.StatusCode === '0') {
                            $cs.successMessage('G/L Account Updated Successfully');
                            $scope.m.PeriodCategoryCode = ''
                            $scope.isPeriodSelected = false;
                        }
                            
                    })
            }

            var setGLSetup = function (m) {

                let g = $scope.GeneralGLSetupTableParams
                let i = $scope.InvntryGLSetupTableParams
                let ar = $scope.ARGLSetupTableParams
                let ap = $scope.APGLSetupTableParams

                /**
                 *  General
                 **/

                let g1 = g.data.filter((a) => a.Code === 'CCDepositFee')[0]
                if ($scope.m.GAcctLink_1) {
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.GAcctLink_1).then(function (d) {
                        if (d.StatusCode === '0') {
                            g1.AcctCode = d.DataResult.AcctCode;
                            g1.AcctName = d.DataResult.AcctName;
                        }
                    })
                } else {
                    g1.AcctCode = null;
                    g1.AcctName = null;
                }

                let g2 = g.data.filter((a) => a.Code === 'RoundingAcct')[0]
                if ($scope.m.GAcctLink_2) {
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.GAcctLink_2).then(function (d) {
                        if (d.StatusCode === '0') {
                            g2.AcctCode = d.DataResult.AcctCode;
                            g2.AcctName = d.DataResult.AcctName;
                        }
                    })
                } else {
                    g2.AcctCode = null;
                    g2.AcctName = null;
                }

                let g3 = g.data.filter((a) => a.Code === 'AutoReconDiffAcct')[0]
                if ($scope.m.GAcctLink_3) {
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.GAcctLink_3).then(function (d) {
                        if (d.StatusCode === '0') {
                            g3.AcctCode = d.DataResult.AcctCode;
                            g3.AcctName = d.DataResult.AcctName;
                        }
                    })
                } else {
                    g3.AcctCode = null;
                    g3.AcctName = null;
                }

                let g4 = g.data.filter((a) => a.Code === 'PeriodCloseAcct')[0]
                if ($scope.m.GAcctLink_4) {
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.GAcctLink_4).then(function (d) {
                        if (d.StatusCode === '0') {
                            g4.AcctCode = d.DataResult.AcctCode;
                            g4.AcctName = d.DataResult.AcctName;
                        }
                    })
                } else {
                    g4.AcctCode = null;
                    g4.AcctName = null;
                }

                let g5 = g.data.filter((a) => a.Code === 'RealizedExchangeDiffGainAcct')[0]
                if ($scope.m.GAcctLink_5) {
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.GAcctLink_5).then(function (d) {
                        if (d.StatusCode === '0') {
                            g5.AcctCode = d.DataResult.AcctCode;
                            g5.AcctName = d.DataResult.AcctName;

                        }
                    })
                } else {
                    g5.AcctCode = null;
                    g5.AcctName = null;
                }

                let g6 = g.data.filter((a) => a.Code === 'RealizedExchangeDiffLossAcct')[0]
                if ($scope.m.GAcctLink_6) {
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.GAcctLink_6).then(function (d) {
                        if (d.StatusCode === '0') {
                            g6.AcctCode = d.DataResult.AcctCode;
                            g6.AcctName = d.DataResult.AcctName;
                        }
                    })
                } else {
                    g6.AcctCode = null;
                    g6.AcctName = null;
                }

                let a7 = g.data.filter((a) => a.Code === 'OpenningBalanceAcct')[0]
                if ($scope.m.GAcctLink_7) {
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.GAcctLink_7).then(function (d) {
                        if (d.StatusCode === '0') {
                            a7.AcctCode = d.DataResult.AcctCode;
                            a7.AcctName = d.DataResult.AcctName;
                        }
                    })
                } else {
                    a7.AcctCode = null;
                    a7.AcctName = null;
                }


                /**
                 * Invntry
                 **/

                let i1 = i.data.filter((a) => a.Code === 'InvntryAcct')[0]
                if ($scope.m.IAcctLink_1) {
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.IAcctLink_1).then(function (d) {
                        if (d.StatusCode === '0') {

                            i1.AcctCode = d.DataResult.AcctCode
                            i1.AcctName = d.DataResult.AcctName
                        }
                    })
                } else {
                    i1.AcctCode = null;
                    i1.AcctName = null;
                }

                let i2 = i.data.filter((a) => a.Code === 'COGSAcct')[0]
                if ($scope.m.IAcctLink_2) {
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.IAcctLink_2).then(function (d) {
                        if (d.StatusCode === '0') {

                            i2.AcctCode = d.DataResult.AcctCode
                            i2.AcctName = d.DataResult.AcctName
                        }
                    })
                } else {
                    i2.AcctCode = null;
                    i2.AcctName = null;
                }

                let i3 = i.data.filter((a) => a.Code === 'AllocationAcct')[0]
                if ($scope.m.IAcctLink_3) {
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.IAcctLink_3).then(function (d) {
                        if (d.StatusCode === '0') {

                            i3.AcctCode = d.DataResult.AcctCode
                            i3.AcctName = d.DataResult.AcctName
                        }
                    })
                } else {
                    i3.AcctCode = null;
                    i3.AcctName = null;

                }

                let i4 = i.data.filter((a) => a.Code === 'VarianceAcct')[0]
                if ($scope.m.IAcctLink_4) {
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.IAcctLink_4).then(function (d) {
                        if (d.StatusCode === '0') {
                            i4.AcctCode = d.DataResult.AcctCode
                            i4.AcctName = d.DataResult.AcctName
                        }
                    })
                } else {
                    i4.AcctCode = null;
                    i4.AcctName = null;

                }

                let i5 = i.data.filter((a) => a.Code === 'PriceDiffAcct')[0]
                if ($scope.m.IAcctLink_5) {
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.IAcctLink_5).then(function (d) {
                        if (d.StatusCode === '0') {

                            i5.AcctCode = d.DataResult.AcctCode
                            i5.AcctName = d.DataResult.AcctName
                        }
                    })
                } else {
                    i5.AcctCode = null;
                    i5.AcctName = null;

                }

                let i6 = i.data.filter((a) => a.Code === 'NegInvntryAdjAcct')[0]
                if ($scope.m.IAcctLink_6) {
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.IAcctLink_6).then(function (d) {
                        if (d.StatusCode === '0') {

                            i6.AcctCode = d.DataResult.AcctCode
                            i6.AcctName = d.DataResult.AcctName
                        }
                    })
                } else {
                    i6.AcctCode = null;
                    i6.AcctName = null;
                }

                let i7 = i.data.filter((a) => a.Code === 'InvntryOffsetDecAcct')[0]
                if ($scope.m.IAcctLink_7) {
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.IAcctLink_7).then(function (d) {
                        if (d.StatusCode === '0') {

                            i7.AcctCode = d.DataResult.AcctCode
                            i7.AcctName = d.DataResult.AcctName
                        }
                    })
                } else {
                    i7.AcctCode = null;
                    i7.AcctName = null;
                }

                let i8 = i.data.filter((a) => a.Code === 'InvntryOffsetIncAcct')[0]
                if ($scope.m.IAcctLink_8) {
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.IAcctLink_8).then(function (d) {
                        if (d.StatusCode === '0') {

                            i8.AcctCode = d.DataResult.AcctCode
                            i8.AcctName = d.DataResult.AcctName
                        }
                    })
                } else {
                    i8.AcctCode = null;
                    i8.AcctName = null;

                }

                let i9 = i.data.filter((a) => a.Code === 'ExchangeDiffAcct')[0]
                if ($scope.m.IAcctLink_9) {
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.IAcctLink_9).then(function (d) {
                        if (d.StatusCode === '0') {
                            i9.AcctCode = d.DataResult.AcctCode
                            i9.AcctName = d.DataResult.AcctName
                        }
                    })
                }
                else {
                    i9.AcctCode = null;
                    i9.AcctName = null;
                }

                let i10 = i.data.filter((a) => a.Code === 'GoodsClearingAcct')[0]
                if ($scope.m.IAcctLink_10) {
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.IAcctLink_10).then(function (d) {
                        if (d.StatusCode === '0') {

                            i10.AcctCode = d.DataResult.AcctCode
                            i10.AcctName = d.DataResult.AcctName
                        }
                    })
                } else {
                    i10.AcctCode = null;
                    i10.AcctName = null;
                }

                let i11 = i.data.filter((a) => a.Code === 'GLDecAcct')[0]
                if ($scope.m.IAcctLink_11) {
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.IAcctLink_11).then(function (d) {
                        if (d.StatusCode === '0') {

                            i11.AcctCode = d.DataResult.AcctCode
                            i11.AcctName = d.DataResult.AcctName
                        }
                    })

                } else {
                    i11.AcctCode = null;
                    i11.AcctName = null;

                }

                let i12 = i.data.filter((a) => a.Code === 'GLIncAcct')[0]
                if ($scope.m.IAcctLink_12) {
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.IAcctLink_12).then(function (d) {
                        if (d.StatusCode === '0') {

                            i12.AcctCode = d.DataResult.AcctCode
                            i12.AcctName = d.DataResult.AcctName
                        }
                    })
                } else {
                    i12.AcctCode = null;
                    i12.AcctName = null;

                }

                let i13 = i.data.filter((a) => a.Code === 'WIPInvntryAcct')[0]
                if ($scope.m.IAcctLink_13) {
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.IAcctLink_13).then(function (d) {
                        if (d.StatusCode === '0') {

                            i13.AcctCode = d.DataResult.AcctCode
                            i13.AcctName = d.DataResult.AcctName
                        }
                    })

                } else {
                    i13.AcctCode = null;
                    i13.AcctName = null;
                }


                let i14 = i.data.filter((a) => a.Code === 'WIPInvntryVarianceAcct')[0]
                if ($scope.m.IAcctLink_14) {
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.IAcctLink_14).then(function (d) {
                        if (d.StatusCode === '0') {
                            i14.AcctCode = d.DataResult.AcctCode
                            i14.AcctName = d.DataResult.AcctName
                        }
                    })
                } else {
                    i14.AcctCode = null;
                    i14.AcctName = null;
                }

                let i15 = i.data.filter((a) => a.Code === 'ExpenseClearingAcct')[0]
                if ($scope.m.IAcctLink_15) {

                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.IAcctLink_15).then(function (d) {
                        if (d.StatusCode === '0') {

                            i15.AcctCode = d.DataResult.AcctCode
                            i15.AcctName = d.DataResult.AcctName
                        }
                    })
                } else {
                    i15.AcctCode = null;
                    i15.AcctName = null;
                }

                /**
                 * AR
                 **/

                let ar1 = ar.data.filter((a) => a.Code === 'DomesticReceivableAcct')[0];
                if ($scope.m.SAcctLink_1)
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.SAcctLink_1).then(function (d) {
                        if (d.StatusCode === '0') {
                            ar1.AcctCode = d.DataResult.AcctCode
                            ar1.AcctName = d.DataResult.AcctName
                        }
                    })
                else {
                    ar1.AcctCode = null;
                    ar1.AcctName = null;
                }

                let ar2 = ar.data.filter((a) => a.Code === 'ForeignReceivableAcct')[0];
                if ($scope.m.SAcctLink_2)
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.SAcctLink_2).then(function (d) {
                        if (d.StatusCode === '0') {
                            ar2.AcctCode = d.DataResult.AcctCode
                            ar2.AcctName = d.DataResult.AcctName
                        }
                    })
                else {
                    ar2.AcctCode = null;
                    ar2.AcctName = null;
                }

                let ar3 = ar.data.filter((a) => a.Code === 'ChecksReceivedAcct')[0];
                if ($scope.m.SAcctLink_3)
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.SAcctLink_3).then(function (d) {
                        if (d.StatusCode === '0') {
                            ar3.AcctCode = d.DataResult.AcctCode
                            ar3.AcctName = d.DataResult.AcctName
                        }
                    })
                else {
                    ar3.AcctCode = null;
                    ar3.AcctName = null;

                }

                let ar4 = ar.data.filter((a) => a.Code === 'CashOnHandAcct')[0];
                if ($scope.m.SAcctLink_4)
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.SAcctLink_4).then(function (d) {
                        if (d.StatusCode === '0') {
                            ar4.AcctCode = d.DataResult.AcctCode
                            ar4.AcctName = d.DataResult.AcctName
                        }
                    })
                else {
                    ar4.AcctCode = null;
                    ar4.AcctName = null;
                }

                let ar5 = ar.data.filter((a) => a.Code === 'OverPaymentAcct')[0];
                if ($scope.m.SAcctLink_5)
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.SAcctLink_5).then(function (d) {
                        if (d.StatusCode === '0') {
                            ar5.AcctCode = d.DataResult.AcctCode
                            ar5.AcctName = d.DataResult.AcctName
                        }
                    })
                else {
                    ar5.AcctCode = null;
                    ar5.AcctName = null;
                }

                let ar6 = ar.data.filter((a) => a.Code === 'UnderPaymentAcct')[0];
                if ($scope.m.SAcctLink_6)
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.SAcctLink_6).then(function (d) {
                        if (d.StatusCode === '0') {
                            ar6.AcctCode = d.DataResult.AcctCode
                            ar6.AcctName = d.DataResult.AcctName
                        }
                    })
                else {
                    ar6.AcctCode = null;
                    ar6.AcctName = null;
                }

                let ar7 = ar.data.filter((a) => a.Code === 'DPClearingAcct')[0];
                if ($scope.m.SAcctLink_7)
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.SAcctLink_7).then(function (d) {
                        if (d.StatusCode === '0') {
                            ar7.AcctCode = d.DataResult.AcctCode
                            ar7.AcctName = d.DataResult.AcctName
                        }
                    })
                else {
                    ar7.AcctCode = null;
                    ar7.AcctName = null;
                }

                let ar8 = ar.data.filter((a) => a.Code === 'ExchangeDiffGain')[0];
                if ($scope.m.SAcctLink_8)
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.SAcctLink_8).then(function (d) {
                        if (d.StatusCode === '0') {
                            ar8.AcctCode = d.DataResult.AcctCode
                            ar8.AcctName = d.DataResult.AcctName
                        }
                    })
                else {
                    ar8.AcctCode = null;
                    ar8.AcctName = null;
                }

                let ar9 = ar.data.filter((a) => a.Code === 'ExchangeDiffLoss')[0];
                if ($scope.m.SAcctLink_9)
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.SAcctLink_9).then(function (d) {
                        if (d.StatusCode === '0') {
                            ar9.AcctCode = d.DataResult.AcctCode
                            ar9.AcctName = d.DataResult.AcctName
                        }
                    })
                else {
                    ar9.AcctCode = null;
                    ar9.AcctName = null;
                }

                let ar10 = ar.data.filter((a) => a.Code === 'CashDiscount')[0];
                if ($scope.m.SAcctLink_10)
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.SAcctLink_10).then(function (d) {
                        if (d.StatusCode === '0') {
                            ar10.AcctCode = d.DataResult.AcctCode
                            ar10.AcctName = d.DataResult.AcctName
                        }
                    })
                else {
                    ar10.AcctCode = null;
                    ar10.AcctName = null;
                }

                let ar11 = ar.data.filter((a) => a.Code === 'RevenueAcct')[0];
                if ($scope.m.SAcctLink_11)
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.SAcctLink_11).then(function (d) {
                        if (d.StatusCode === '0') {
                            ar11.AcctCode = d.DataResult.AcctCode
                            ar11.AcctName = d.DataResult.AcctName
                        }
                    })
                else {
                    ar11.AcctCode = null;
                    ar11.AcctName = null;
                }

                let ar12 = ar.data.filter((a) => a.Code === 'RevenueAcctForeign')[0];
                if ($scope.m.SAcctLink_12)
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.SAcctLink_12).then(function (d) {
                        if (d.StatusCode === '0') {
                            ar12.AcctCode = null
                            ar12.AcctName = null
                        }
                    })
                else {
                    ar12.AcctCode = null;
                    ar12.AcctName = null;
                }

                let ar13 = ar.data.filter((a) => a.Code === 'SalesCreditAcct')[0];
                if ($scope.m.SAcctLink_13)
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.SAcctLink_13).then(function (d) {
                        if (d.StatusCode === '0') {
                            ar13.AcctCode = null;
                            ar13.AcctName = null;
                        }
                    })
                else {
                    ar13.AcctCode = null;
                    ar13.AcctName = null;
                }

                let ar14 = ar.data.filter((a) => a.Code === 'DPInterimAcct')[0];
                if ($scope.m.SAcctLink_14)
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.SAcctLink_14).then(function (d) {
                        if (d.StatusCode === '0') {
                            ar14.AcctCode = null;
                            ar14.AcctName = null;
                        }
                    })
                else {
                    ar14.AcctCode = null;
                    ar14.AcctName = null;

                }

                let ar15 = ar.data.filter((a) => a.Code === 'DunningInterestAcct')[0];
                if ($scope.m.SAcctLink_15)
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.SAcctLink_15).then(function (d) {
                        if (d.StatusCode === '0') {
                            ar15.AcctCode = d.DataResult.AcctCode
                            ar15.AcctName = d.DataResult.AcctName
                        }
                    })
                else {
                    ar15.AcctCode = null;
                    ar15.AcctName = null;
                }

                let ar16 = ar.data.filter((a) => a.Code === 'DunningFeeAcct')[0];
                if ($scope.m.SAcctLink_16)
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.SAcctLink_16).then(function (d) {
                        if (d.StatusCode === '0') {
                            ar16.AcctCode = d.DataResult.AcctCode
                            ar16.AcctName = d.DataResult.AcctName
                        }
                    })
                else {
                    ar16.AcctCode = null;
                    ar16.AcctName = null;
                }


                /**
                 * AP
                 **/

                let ap1 = ap.data.filter((a) => a.Code === 'DomesticPayableAcct')[0];
                if ($scope.m.PAcctLink_1)
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.PAcctLink_1).then(function (d) {
                        if (d.StatusCode === '0') {
                            ap1.AcctCode = d.DataResult.AcctCode
                            ap1.AcctName = d.DataResult.AcctName
                        }
                    })
                else {
                    ap1.AcctCode = null;
                    ap1.AcctName = null;
                }

                let ap2 = ap.data.filter((a) => a.Code === 'ForeignPayableAcct')[0];
                if ($scope.m.PAcctLink_2)
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.PAcctLink_2).then(function (d) {
                        if (d.StatusCode === '0') {
                            ap2.AcctCode = d.DataResult.AcctCode
                            ap2.AcctName = d.DataResult.AcctName
                        }
                    })
                else {
                    ap2.AcctCode = null;
                    ap2.AcctName = null;
                }

                let ap3 = ap.data.filter((a) => a.Code === 'ExchangeDiffGain')[0];
                if ($scope.m.PAcctLink_3)
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.PAcctLink_3).then(function (d) {
                        if (d.StatusCode === '0') {
                            ap3.AcctCode = d.DataResult.AcctCode
                            ap3.AcctName = d.DataResult.AcctName
                        }
                    })
                else {
                    ap3.AcctCode = null;
                    ap3.AcctName = null;

                }

                let ap4 = ap.data.filter((a) => a.Code === 'ExchangeDiffLoss')[0];
                if ($scope.m.PAcctLink_4)
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.PAcctLink_4).then(function (d) {
                        if (d.StatusCode === '0') {
                            ap4.AcctCode = d.DataResult.AcctCode
                            ap4.AcctName = d.DataResult.AcctName
                        }
                    })
                else {
                    ap4.AcctCode = null;
                    ap4.AcctName = null;
                }

                let ap5 = ap.data.filter((a) => a.Code === 'BankTransferAcct')[0];
                if ($scope.m.PAcctLink_5)
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.PAcctLink_5).then(function (d) {
                        if (d.StatusCode === '0') {
                            ap5.AcctCode = d.DataResult.AcctCode
                            ap5.AcctName = d.DataResult.AcctName
                        }
                    })
                else {
                    ap5.AcctCode = null;
                    ap5.AcctName = null;
                }

                let ap6 = ap.data.filter((a) => a.Code === 'CashDiscountAcct')[0];
                if ($scope.m.PAcctLink_6)
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.PAcctLink_6).then(function (d) {
                        if (d.StatusCode === '0') {
                            ap6.AcctCode = d.DataResult.AcctCode
                            ap6.AcctName = d.DataResult.AcctName
                        }
                    })
                else {
                    ap6.AcctCode = null;
                    ap6.AcctName = null;
                }

                let ap7 = ap.data.filter((a) => a.Code === 'CashDiscountClearingAcct')[0];
                if ($scope.m.PAcctLink_7)
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.PAcctLink_7).then(function (d) {
                        if (d.StatusCode === '0') {
                            ap7.AcctCode = d.DataResult.AcctCode
                            ap7.AcctName = d.DataResult.AcctName
                        }
                    })
                else {
                    ap7.AcctCode = null;
                    ap7.AcctName = null;
                }

                let ap8 = ap.data.filter((a) => a.Code === 'ExpenseForeignAcct')[0];
                if ($scope.m.PAcctLink_8)
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.PAcctLink_8).then(function (d) {
                        if (d.StatusCode === '0') {
                            ap8.AcctCode = d.DataResult.AcctCode
                            ap8.AcctName = d.DataResult.AcctName
                        }
                    })
                else {
                    ap8.AcctCode = null;
                    ap8.AcctName = null;
                }

                let ap9 = ap.data.filter((a) => a.Code === 'CreditAcct')[0];
                if ($scope.m.PAcctLink_9)
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.PAcctLink_9).then(function (d) {
                        if (d.StatusCode === '0') {
                            ap9.AcctCode = d.DataResult.AcctCode
                            ap9.AcctName = d.DataResult.AcctName
                        }
                    })
                else {
                    ap9.AcctCode = null;
                    ap9.AcctName = null;
                }

                let ap10 = ap.data.filter((a) => a.Code === 'CreditForeignAcct')[0];
                if ($scope.m.PAcctLink_10)
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.PAcctLink_10).then(function (d) {
                        if (d.StatusCode === '0') {
                            ap10.AcctCode = d.DataResult.AcctCode
                            ap10.AcctName = d.DataResult.AcctName
                        }
                    })
                else {
                    ap10.AcctCode = null;
                    ap10.AcctName = null;
                }

                let ap11 = ap.data.filter((a) => a.Code === 'OverPaymentAcct')[0];
                if ($scope.m.PAcctLink_11)
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.PAcctLink_11).then(function (d) {
                        if (d.StatusCode === '0') {
                            ap11.AcctCode = d.DataResult.AcctCode
                            ap11.AcctName = d.DataResult.AcctName
                        }
                    })
                else {
                    ap11.AcctCode = null;
                    ap11.AcctName = null;
                }

                let ap12 = ap.data.filter((a) => a.Code === 'UnderPaymentAcct')[0];
                if ($scope.m.PAcctLink_12)
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.PAcctLink_12).then(function (d) {
                        if (d.StatusCode === '0') {
                            ap12.AcctCode = d.DataResult.AcctCode
                            ap12.AcctName = d.DataResult.AcctName
                        }
                    })
                else {
                    ap12.AcctCode = null;
                    ap12.AcctName = null;
                }

                let ap13 = ap.data.filter((a) => a.Code === 'DPClearingAcct')[0];
                if ($scope.m.PAcctLink_13)
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.PAcctLink_13).then(function (d) {
                        if (d.StatusCode === '0') {
                            ap13.AcctCode = d.DataResult.AcctCode
                            ap13.AcctName = d.DataResult.AcctName
                        }
                    })
                else {
                    ap13.AcctCode = null;
                    ap13.AcctName = null;
                }

                let ap14 = ap.data.filter((a) => a.Code === 'DPInterimAcct')[0];
                if ($scope.m.PAcctLink_14)
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.PAcctLink_14).then(function (d) {
                        if (d.StatusCode === '0') {
                            ap14.AcctCode = d.DataResult.AcctCode
                            ap14.AcctName = d.DataResult.AcctName
                        }
                    })
                else {
                    ap14.AcctCode = null;
                    ap14.AcctName = null;
                }

                let ap15 = ap.data.filter((a) => a.Code === 'ExpenseAndInvntryAcct')[0];
                if ($scope.m.PAcctLink_15)
                    $coaSvc.GetAccountByAcctCodeSearch($scope.m.PAcctLink_15).then(function (d) {
                        if (d.StatusCode === '0') {
                            ap15.AcctCode = d.DataResult.AcctCode
                            ap15.AcctName = d.DataResult.AcctName
                        }
                    })
                else {
                    ap15.AcctCode = null;
                    ap15.AcctName = null;
                }
            }

            $scope.onSelectPeriod = function () {
                if ($scope.m.PeriodCategoryCode) {
                    $glSetupSvc.GetFinancialPeriodByCode($scope.m.PeriodCategoryCode)
                        .then(function (d) {
                            if (d.StatusCode === '0') {

                                $scope.m = d.DataResult
                                console.log($scope.m)

                                $scope.isPeriodSelected = $scope.m.PeriodCategoryCode !== ''
                                setGLSetup($scope.m)
                            }
                        })
                }
            }

            $scope.onBtnSaveClick = function () {
                $cs.confimInfo(() => {
                    saveGLSetup()


                }, 'G/L Account', 'Do you want to Update G/L Account setup', 'Ok')

            }
        }])
}());