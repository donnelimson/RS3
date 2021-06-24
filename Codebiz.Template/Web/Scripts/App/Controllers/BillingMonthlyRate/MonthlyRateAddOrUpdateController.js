angular.module('MetronicApp')
    .controller('MonthlyRateAddOrUpdateController',
        ['$scope', 'NgTableParams', 'MonthlyRateService', '$location', '$window', '$q', 'CommonService', '$filter', '$timeout', '$uibModal','BillingPeriodService',
            function MonthlyRateAddOrUpdateController($scope, NgTableParams, MonthlyRateService, $location, $window, $q, CommonService, $filter, $timeout, $uibModal, BillingPeriodService) {
                var MonthlyRateId = $location.search().monthlyRateId == null ? 0 : $location.search().monthlyRateId;

                $scope.isUpdate = MonthlyRateId > 0;
                $scope.formSubmitted = false;
                $scope.Id = 0;
                $scope.data = {
                    ConsumerClassId: null,
                    BillingPeriod: "",
                    Description: "",
                    UnbundledTransactionsByCategory: [],
                    IsClone: false,
                    MonthlyRateId:0
                }

                //#region Initialization
                this.$onInit = function () {
                    $scope.isClone = $location.url().includes("Clone");
                    $scope.data.IsClone = $scope.isClone;
                    if (MonthlyRateId > 0) {
                        GetMonthlyRateForUpdate();
                    }
                    else {
                        GetConsumerClass();
                        GetUnbundledTransactionCategory();
                    }
                }
                //$(window).focus(function () {
                //    if (localStorage.getItem("rate") == 1) {
                //        localStorage.setItem("rate", 0);
                //        $scope.changeBillingPeriod(true);
                //    }
                //});
                $scope.changeBillingPeriod = function (onload) {
                    BillingPeriodService.GetByBillingPeriodCode({ billingPeriod: $scope.data.BillingPeriod }).then(function (d) {
                        if (d.result != null) {
                            $scope.data.Description = d.result.Description;
                            $scope.form.billingPeriodDatePicker.$invalid = false;
                        }
                        //else {
                        //    if (!fromFocus) {
                        //        CommonService.messageInfo(function () {
                        //           /localStorage.setItem("rate", 1);
                        //            $window.open(document.CSABillingPeriod);
                        //            //  window.location.href = document.CSABillingPeriod;
                        //        }, "Billing Period", "No billing period found", "CREATE", "NO");
                        //        $scope.data.Description = "";

                        //    }

                        //}
                        else {
                            $scope.data.Description = "";
                            $scope.form.billingPeriodDatePicker.$invalid = true;
                        }
                        if (onload) {
                            $scope.form.$pristine = true;
                        }
                
                    })
                }

                $scope.GenerateMonthlyRateDescrition = function (consumerClassId) {
                  
                    if (!$scope.isUpdate || $scope.isClone) {
                        MonthlyRateService.GenerateMonthlyRateDescrition({
                            consumerClassId: consumerClassId,
                            id: $scope.data.MonthlyRateId,
                            isClone: $scope.isClone
                        }).then(function (data) {
                            $scope.data.BillingPeriod = data.data.BillingPeriod;
                            $scope.changeBillingPeriod(true);
                         //   $scope.form.$pristine = true;
                        });
                    }
                };

                //#region Grid

                function initEmptyRow() {
                    $scope.emptyRow = {
                        Id: $scope.Id--,
                        BillingUnbundledTransactionId: null,
                        Description: "",
                        RateOrAmount: "RATE",
                        Rate: null,
                        Amount: null,
                        IsExist: false
                    }
                }

                $scope.unbundledChanged = function (category, data) {   
                    var index = category.UnbundledTransactionList.findIndex(r => r.BillingUnbundledTransactionId == data.BillingUnbundledTransactionId);
                    if (category.UnbundledTransactionList[index].ComputedByKVA) {
                        data.RateOrAmount = "KVARATING";

                    } else {
                        data.RateOrAmount = "";
                    }
                    $scope.checkIfExist(category);
                }

                $scope.rateOrAmount = function (categoryIndex, itemIndex, item) {
                    if (item.RateOrAmount == "RATE" || item.RateOrAmount == "") {
                        item.Amount = null;
                    }

                    if (item.RateOrAmount == "AMOUNT" || item.RateOrAmount == "") {
                        item.Rate = null;
                    }

                    $scope.form.$dirty = true;
                }

                $scope.addRow = function (category, index) {
                    initEmptyRow();
                    category.UnbundledTransactions.push($scope.emptyRow);
                    $scope.form.$dirty = true;
                }

                $scope.deleteRow = function (category, index) {
                    category.UnbundledTransactions.splice(index, 1);
                    $scope.form.$dirty = true;
                };

                $scope.checkIfExist = function (category) {
                    for (var i = 0; i < category.UnbundledTransactions.length; i++) {
                        var data = category.UnbundledTransactions[i];
                        var exist = category.UnbundledTransactions.findIndex(r => r.BillingUnbundledTransactionId != null && r.BillingUnbundledTransactionId === data.BillingUnbundledTransactionId && r.Id != data.Id);

                        if (exist != -1) {
                            data.IsExist = true;
                        }
                        else {
                            data.IsExist = false;
                        }
                    }

                    $scope.form.$dirty = true;
                }

                //#endregion

                //#region Save

                $scope.saveChanges = function () {
                    $scope.formSubmitted = true;
                    for (var i = 0; i <= $scope.data.UnbundledTransactionsByCategory.length-1; i++) {
                        if ($scope.data.UnbundledTransactionsByCategory[i].UnbundledTransactions.length == 0) {
                            $scope.form.$valid = false;
                            break;
                        }
                    }
                    if ($scope.form.$valid) {
                        CommonService.saveOrUpdateChanges(function () {
                            MonthlyRateService.Save({
                                model: $scope.data
                            }).then(function (data) {
                                if (data.success) {
                                    CommonService.successMessage(data.message);
                                    $timeout(function () {
                                        $window.location.href = document.BillingMonthlyRate;
                                    }, 600);
                                }
                                else {
                                    CommonService.warningMessage(data.message);
                                }
                            }, function (error /*Error event should handle here*/) {
                                CommonService.errorMessage("Unexpected error occured.");
                            }, function (data /*Notify event should handle here*/) {
                            });
                        }, $scope.isClone ? 0 : MonthlyRateId);
                    } else {
                        if ($scope.form.billingPeriodDatePicker.$invalid) {
                            $(document).ready(function () {
                                $("#billingPeriodDatePicker").datepicker({
                                }).focus();
                            });
                        }
                        else {
                            angular.element('.ng-invalid')[1].focus();
                        }
         
                    }
                }

                $scope.cancelMonthlyRates = function () {
                    if (!$scope.form.$pristine) {
                        CommonService.cancelChanges(function () {
                            $window.location.href = document.BillingMonthlyRate;
                        });
                    }
                    else {
                        $window.location.href = document.BillingMonthlyRate;
                    }
                };

                //#endregion

                function GetConsumerClass(id) {
                    CommonService.GetConsumerClass({
                    }, "ConsumerClassId").then(function (data) {
                        $scope.consumerClasses = data.data;
                        if (id != null && id != undefined) {
                            $scope.data.ConsumerClassId = id;
                        }
                    }, function (error /*Error event should handle here*/) {
                        console.log("Error");
                    }, function (data /*Notify event should handle here*/) {
                    });
                }

                function GetUnbundledTransactionCategory() {
                    MonthlyRateService.GetUnbundledTransactionCategory({
                    }).then(function (data) {
                        $scope.unbundledTransactionCategory = data.data;
                        for (var i = 0; i < data.data.length; i++) {
                            $scope.data.UnbundledTransactionsByCategory.push({
                                BillingUnbundledTransactionCategoryId: data.data[i].BillingUnbundledTransactionCategoryId,
                                Description: data.data[i].Description,
                                UnbundledTransactionList: data.data[i].UnbundledTransactionList,
                                UnbundledTransactions: []
                            });
                        }
                    }, function (error /*Error event should handle here*/) {
                        console.log("Error");
                    }, function (data /*Notify event should handle here*/) {
                    });
                }

                function GetMonthlyRateForUpdate() {
                    MonthlyRateService.GetMonthlyRateDetails({
                        monthlyRateId: MonthlyRateId
                    }).then(function (data) {
                       // console.log(data.data)
                        $scope.data = data.data;
                        GetConsumerClass(data.data.ConsumerClassId);
                        if ($scope.isClone) {
                            $scope.data.IsClone = $scope.isClone;
                            $scope.GenerateMonthlyRateDescrition(data.data.ConsumerClassId);
                        }
                        $scope.form.$pristine = true;
                    }, function (error /*Error event should handle here*/) {
                        console.log("Error");
                    }, function (data /*Notify event should handle here*/) {
                    });
                }
            }]);