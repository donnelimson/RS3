(function () {
    'use strict';

    var app = angular.module('MetronicApp');
    app.controller('MonthlyRateUnbundledTransactionFormModalController', MonthlyRateUnbundledTransactionFormModalController);

    MonthlyRateUnbundledTransactionFormModalController.$inject = ['$scope', '$http', 'BillingUnbundledTransactionService', 'NgTableParams', '$uibModal', '$window', 'CommonService', '$timeout', '$q', '$uibModalInstance', 'categoryId', 'MonthlyRateService', 'index', 'isEdit', 'parentIndex', 'unbundledTransactionCategory'];

    function MonthlyRateUnbundledTransactionFormModalController($scope, $http, BillingUnbundledTransactionService, NgTableParams, $uibModal, $window, CommonService, $timeout, $q, $uibModalInstance, categoryId, MonthlyRateService, index, isEdit, parentIndex, unbundledTransactionCategory) {
        $scope.formSubmitted = false;
      
  
        if (isEdit) {
            $scope.unbundledTransactions = angular.copy(unbundledTransactionCategory[parentIndex].UnbundledTransactions[index])
        }
        //Init
        this.$onInit = function () {
            if (index==null) {
                $scope.IsAmmount = false;              
            }
            else {
                if (unbundledTransactionCategory[parentIndex].UnbundledTransactions[index].Rate == null || unbundledTransactionCategory[parentIndex].UnbundledTransactions[index].Rate == '') {
                    $scope.IsAmmount = true;
                    $scope.unbundledTransactions.Amount = Number(parseFloat($scope.unbundledTransactions.Amount).toFixed(2)).toLocaleString('en', {
                        minimumFractionDigits: 2
                    });
                }
                else {
                   
                    $scope.IsAmmount = false;
     
                    $scope.unbundledTransactions.Rate = Number(parseFloat($scope.unbundledTransactions.Rate).toFixed(4)).toLocaleString('en', {
                        minimumFractionDigits: 4
                    });
                }
              
            }
         
            getCategoryDetails();
        }

        //#region Private functions

        function getCategoryDetails() {
            BillingUnbundledTransactionService.GetBillingUnbundledTransactionCategoryDetails({
                categoryId: categoryId
            }).then(function (data) {
                var data = data.data;
                $scope.billingUnbundledTransactionCategoryDetails = data;
            },
            function (error /*Error event should handle here*/) {
                console.log("Error");
            },
            function (data /*Notify event should handle here*/) {
            });
        }

        //#endregion

        $scope.showModalWhenPress = function ($event, categoryId) {
            var keyCode = $event.which || $event.keyCode;
            if (keyCode === 13) {
                $uibModal.open({
                    animation: true,
                    ariaLabelledBy: 'modal-title',
                    ariaDescribedBy: 'modal-body',
                    templateUrl: '_SearchBillingUnbundledTransactionModal.html',
                    controller: 'SearchBillingUnbundledTransactionModalController',
                    size: 'md',
                    keyboard: false,
                    backdrop: 'static',
                    windowClass: 'style',
                    resolve: {
                        categoryId: function () {
                            return categoryId;
                        },
                        unbundledTransactionCategory: function () {
                            return unbundledTransactionCategory;
                        },
                        parentIndex: function () {
                            return parentIndex;
                        },
                        index: function () {
                            return index;
                        }
                    }
                }).result.then(function (data) {
                    $scope.unbundledTransactions = data.data;
                });
            }
        };
    

        $scope.searchUnbundledTransaction = function (categoryId) {
            $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: '_SearchBillingUnbundledTransactionModal.html',
                controller: 'SearchBillingUnbundledTransactionModalController',
                size: 'md',
                keyboard: false,
                backdrop: 'static',
                windowClass: 'style',
                resolve: {
                    categoryId: function () {
                        return categoryId;
                    },
                    unbundledTransactionCategory: function () {
                        return unbundledTransactionCategory;
                    },
                    parentIndex: function () {
                        return parentIndex;
                    },
                    index: function () {
                        return index;
                    },
                }
            }).result.then(function (data) {
                $scope.unbundledTransactions = data.data;
            });
        };

        $scope.getUnbundledNameByCode = function (code, categoryId) {
                MonthlyRateService.GetUnbundledNameByCode({
                    code: code,
                    categoryId: categoryId,
                }).then(function (data) {
                    $scope.unbundledTransactions = data.data;
                });
            }

        //Save 
        $scope.saveChanges = function () {
            $scope.formSubmitted = true;
            $scope.isAlreadyExist = false;
                var indexModal = unbundledTransactionCategory[parentIndex].UnbundledTransactions.findIndex(r=>r.BillingUnbundledTransactionId == $scope.unbundledTransactions.BillingUnbundledTransactionId);
                if (indexModal != -1 && indexModal !== index) {
                    CommonService.warningMessage("Unbundled transaction already exist");
                    unbundledTransactionCategory[parentIndex].UnbundledTransactions[indexModal].Selected = true
                    $scope.isAlreadyExist = true;
                    return;
                }

            if ($scope.form.$valid && !$scope.isAlreadyExist && !$scope.invalidAmountValue && !$scope.invalidRateValue) {
                //$scope.unbundledTransactions.Rate = $scope.Rate;
                //$scope.unbundledTransactions.Amount = $scope.Amount;
                if ($scope.unbundledTransactions.Amount != null) {
                    var amount = $scope.unbundledTransactions.Amount.replace(",", "");
                    $scope.unbundledTransactions.Amount = amount;
                }
           
                $scope.$close($scope.unbundledTransactions);
            }
        };

        $scope.cancel = function () {
            if (!$scope.form.$pristine) {
                CommonService.cancelChanges(function () {
                    $uibModalInstance.dismiss('cancel');
                })
            }
            else {
                $uibModalInstance.dismiss('cancel');
            }
         
        };

    }
})();
