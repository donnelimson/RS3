MetronicApp.controller('BillingUnbundledTransactionModalController', ['$scope', '$uibModalInstance', 'BillingUnbundledTransactionService', 'CommonService', 'BillingUnbundledTransactionId', 'ForViewingOnly', '$uibModal', '$timeout', 'NgTableParams', '$q', '$filter',
    function ($scope, $uibModalInstance, BillingUnbundledTransactionService, CommonService, BillingUnbundledTransactionId, ForViewingOnly, $uibModal, $timeout, NgTableParams, $q, $filter) {
        $scope.ForViewingOnly = ForViewingOnly;
        $scope.formSubmitted = false;
        $scope.unbundledTransactionsForVat = [];
        $scope.unbundledTransactionsForDiscount = [];
        $scope.oldclassification = "";

        $scope.billingUnbundledTransaction = {
            BillingUnbundledTransactionId: 0,
            Code: "",
            Name: "",
            DisplayedName: "",
            BillingUnbundledTransactionCategoryId: null,
            Classification: "",
            ComputedBy: "KWH",
            IncludedTo: [],
            ZeroWhenThereIsDemand: false,
            ZeroWhenThereIsNoDemand: false,
            IsICERA: false,
            IsGRAM: false,
            IsLifelineSubsidy: false,
            IsSeniorCitizenDiscount: false,
            BillingUnbundledTransactionForVatIds: [],
            BillingUnbundledTransactionForDiscountIds: []
        }

        function GetBillingCategories(id) {
            BillingUnbundledTransactionService.GetCategories({
            }, "CategoryId").then(function (data) {
                $scope.categories = data.data;
                if (id != null && id != undefined) {
                    $scope.billingUnbundledTransaction.BillingUnbundledTransactionCategoryId = id;
                }
            },
                function (error /*Error event should handle here*/) {
                    console.log("Error");
                },
                function (data /*Notify event should handle here*/) {
                });
        }

        this.$onInit = function () {
            if (BillingUnbundledTransactionId > 0) {
                $scope.isUpdate = true;
                GetDataForUpdtate();
            }
            else {
                GetBillingCategories();
            }
        }

        $scope.onChangedClassification = function () {
            var result = false;
            if (($scope.billingUnbundledTransaction.Classification == "DISCOUNT" &&
                $scope.unbundledTransactionsForDiscount.length > 0)) {
                result = true;
            }
            else if (($scope.billingUnbundledTransaction.Classification == "VAT") &&
                ($scope.unbundledTransactionsForDiscount.length > 0 ||
                    $scope.unbundledTransactionsForVat.length > 0)) {
                result = true;
            }

            if (result) {
                CommonService.confimInfo(function (isConfirm) {
                    if (isConfirm) {
                        clearOnChangedClassification();
                        $scope.oldclassification = $scope.billingUnbundledTransaction.Classification;
                    }
                    else {
                        $timeout(function () {
                            $scope.billingUnbundledTransaction.Classification = $scope.oldclassification;
                        }, 200)
                    }
                }, "Confirm action.", "Vat/Discount reference data will be removed.", "CONTINUE");
            }
            else {
                $scope.oldclassification = $scope.billingUnbundledTransaction.Classification;
            }
        }

        $scope.onChangedComputedBy = function () {
            if ($scope.billingUnbundledTransaction.Classification == "DKWH") {
                $scope.billingUnbundledTransaction.ZeroWhenThereIsDemand = false;
            }
        }

        $scope.checkICERA = function () {
            $scope.billingUnbundledTransaction.IsGRAM = false;
            $scope.billingUnbundledTransaction.IsLifelineSubsidy = false;
            $scope.billingUnbundledTransaction.IsSeniorCitizenDiscount = false;
        }

        $scope.checkGRAM = function () {
            $scope.billingUnbundledTransaction.IsICERA = false;
            $scope.billingUnbundledTransaction.IsLifelineSubsidy = false;
            $scope.billingUnbundledTransaction.IsSeniorCitizenDiscount = false;
        }

        $scope.checkLifelineSubsidy = function () {
            $scope.billingUnbundledTransaction.IsICERA = false;
            $scope.billingUnbundledTransaction.IsGRAM = false;
            $scope.billingUnbundledTransaction.IsSeniorCitizenDiscount = false;
        }

        $scope.checkSeniorCitizenDiscount = function () {
            $scope.billingUnbundledTransaction.IsICERA = false;
            $scope.billingUnbundledTransaction.IsGRAM = false;
            $scope.billingUnbundledTransaction.IsLifelineSubsidy = false;
        }

        $scope.cancel = function () {
            if (!$scope.saveBillingUnbundledTransactionForm.$pristine) {
                CommonService.cancelChanges(function () {
                    $uibModalInstance.dismiss('cancel');
                });

            } else {
                $uibModalInstance.dismiss('cancel');
            }
        }

        //#region Save

        $scope.saveBillingUnbundledTransaction = function () {
            $scope.formSubmitted = true;
            if ($scope.saveBillingUnbundledTransactionForm.$valid) {
                CommonService.saveOrUpdateChanges(function () {
                    BillingUnbundledTransactionService.AddBillingUnbundledTransaction({
                        model: $scope.billingUnbundledTransaction
                    }).then(function (data) {
                        if (data.success) {
                            CommonService.successMessage(data.message);
                            $uibModalInstance.close();
                        } else {
                            CommonService.warningMessage(data.message);
                        }
                    }), function (error /*Error event should handle here*/) {
                        console.log(error);
                        CommonService.errorMessage("Unexpected error occured.");
                    }, function (data /*Notify event should handle here*/) {
                    }
                }, $scope.billingUnbundledTransaction.BillingUnbundledTransactionId);
            }
            else {
                $('.nav-tabs a[href="#generalSetupTab"]').tab('show');
            }
        }

        //#endregion

        //#region Unbundled

        $scope.loadUnbundledTransactions = function (forVat) {
            BillingUnbundledTransactionService.GetUnbundledTransactionByType({
                forVat: forVat
            }).then(function (data) {
                $scope.unbundledTransactions = data.result;
                filterSelections();
            })
        }

        $scope.addUnbundledTransaction = function (forVat, id) {
            //   console.log(id)
            if (id != null) {
                BillingUnbundledTransactionService.GetUnbundledTransactionByIdForLookUp({
                    id: id
                }).then(function (data) {
                    if (forVat) {
                        $scope.unbundledTransactionsForVat.push(data.result);
                        $scope.billingUnbundledTransaction.BillingUnbundledTransactionForVatIds.push(id);
                        //    console.log($scope.billingUnbundledTransaction.BillingUnbundledTransactionForVatIds)
                    }
                    else {
                        $scope.unbundledTransactionsForDiscount.push(data.result);
                        $scope.billingUnbundledTransaction.BillingUnbundledTransactionForDiscountIds.push(id);
                    }

                    filterSelections();
                })

                if (forVat) {
                    loadVatTable();
                }
                else {
                    loadDiscountTable();
                }
            }
        }

        $scope.deleteUnbundledTransactionLookUp = function (forVat, row) {
            if (forVat) {
                $scope.unbundledTransactions.push({ BillingUnbundledTransactionId: row.BillingUnbundledTransactionId, CodeAndName: row.CodeAndName });
                $scope.unbundledTransactions = $filter('orderBy')($scope.unbundledTransactions, 'BillingUnbundledTransactionId');
                var index = $scope.unbundledTransactionsForVat.findIndex(r => r.BillingUnbundledTransactionId == row.BillingUnbundledTransactionId);
                $scope.unbundledTransactionsForVat.splice(index, 1);
                index = $scope.billingUnbundledTransaction.BillingUnbundledTransactionForVatIds.findIndex(r => r == row.BillingUnbundledTransactionId);
                $scope.billingUnbundledTransaction.BillingUnbundledTransactionForVatIds.splice(index, 1);
            }
            else {
                $scope.unbundledTransactions.push({ BillingUnbundledTransactionId: row.BillingUnbundledTransactionId, CodeAndName: row.CodeAndName });
                $scope.unbundledTransactions = $filter('orderBy')($scope.unbundledTransactions, 'BillingUnbundledTransactionId');
                var index = $scope.unbundledTransactionsForDiscount.findIndex(r => r.BillingUnbundledTransactionId == row.BillingUnbundledTransactionId);
                $scope.unbundledTransactionsForDiscount.splice(index, 1);
                index = $scope.billingUnbundledTransaction.BillingUnbundledTransactionForDiscountIds.findIndex(r => r == row.BillingUnbundledTransactionId);
                $scope.billingUnbundledTransaction.BillingUnbundledTransactionForDiscountIds.splice(index, 1);
            }
        }

        function clearOnChangedClassification() {
            if ($scope.billingUnbundledTransaction.Classification == "DISCOUNT") {
                clearForDiscountFields();
                loadVatTable();
            }
            else if ($scope.billingUnbundledTransaction.Classification == "VAT") {
                clearForDiscountFields();
                clearForVatFields();
                loadDiscountTable();
                loadVatTable();
            }
        }

        function loadVatTable() {
            var initialSettings = {
                counts: [],
                getData: function (params) {
                    for (var i in params.sorting()) {
                        $scope.sortColumn = i;
                        $scope.sortOrder = params.sorting()[i];
                    }

                    var d = $q.defer();
                    d.resolve($scope.unbundledTransactionsForVat);
                    return d.promise;
                }
            }

            $scope.forVatTableParams = new NgTableParams($scope.pageSize, initialSettings);
        }
        function loadDiscountTable() {
            var initialSettings = {
                counts: [],
                getData: function (params) {
                    for (var i in params.sorting()) {
                        $scope.sortColumn = i;
                        $scope.sortOrder = params.sorting()[i];
                    }

                    var d = $q.defer();
                    d.resolve($scope.unbundledTransactionsForDiscount);
                    return d.promise;
                }
            }

            $scope.forDiscountTableParams = new NgTableParams($scope.pageSize, initialSettings);
        }

        function clearForVatFields() {
            $scope.unbundledTransactionsForVat = [];
            $scope.billingUnbundledTransaction.BillingUnbundledTransactionForVatId = null;
        }

        function clearForDiscountFields() {
            $scope.unbundledTransactionsForDiscount = [];
            $scope.billingUnbundledTransaction.BillingUnbundledTransactionForDiscountId = null;
        }

        function filterSelections() {
            for (var i = 0; i <= $scope.unbundledTransactionsForVat.length - 1; i++) {
                var index = $scope.unbundledTransactions.findIndex(r => r.BillingUnbundledTransactionId == $scope.unbundledTransactionsForVat[i].BillingUnbundledTransactionId);
                if (index != -1) {
                    $scope.unbundledTransactions.splice(index, 1);
                }
            }
            for (var i = 0; i <= $scope.unbundledTransactionsForDiscount.length - 1; i++) {
                var index = $scope.unbundledTransactions.findIndex(r => r.BillingUnbundledTransactionId == $scope.unbundledTransactionsForDiscount[i].BillingUnbundledTransactionId);
                if (index != -1) {
                    $scope.unbundledTransactions.splice(index, 1);
                }
            }
        }

        //#endregion

        function GetDataForUpdtate() {
            BillingUnbundledTransactionService.GetBillingUnbundledTransactionDetails({
                billingUnbundledTransactionId: BillingUnbundledTransactionId
            }).then(function (data) {
                console.log(data.data)
                $scope.billingUnbundledTransaction = data.data;
                GetBillingCategories(data.data.BillingUnbundledTransactionCategoryId);

                BillingUnbundledTransactionService.GetUnbundledTransactionForVatDetailsById({
                    id: BillingUnbundledTransactionId
                }).then(function (data) {
                    for (var i = 0; i <= data.result.length - 1; i++) {
                        if (data.result[i].BillingUnbundledTransactionForVatId != null) {
                            $scope.unbundledTransactionsForVat.push({ BillingUnbundledTransactionId: data.result[i].BillingUnbundledTransactionForVatId, CodeAndName: data.result[i].CodeAndName, DisplayedName: data.result[i].DisplayedName });
                        }
                    }

                    loadVatTable();
                    $scope.saveBillingUnbundledTransactionForm.$pristine = true;
                })

                BillingUnbundledTransactionService.GetUnbundledTransactionForDiscountDetailsById({
                    id: BillingUnbundledTransactionId
                }).then(function (data) {
                    for (var i = 0; i <= data.result.length - 1; i++) {
                        if (data.result[i].BillingUnbundledTransactionForDiscountId != null) {
                            $scope.unbundledTransactionsForDiscount.push({ BillingUnbundledTransactionId: data.result[i].BillingUnbundledTransactionForDiscountId, CodeAndName: data.result[i].CodeAndName, DisplayedName: data.result[i].DisplayedName });
                        }
                    }

                    loadDiscountTable();
                    $scope.saveBillingUnbundledTransactionForm.$pristine = true;
                })
            },
                function (error /*Error event should handle here*/) {
                    console.log("Error");
                },
                function (data /*Notify event should handle here*/) {
                });
        }
    }]);