(function () {
    'use strict';

    var app = angular.module('MetronicApp');
    app.controller('SearchBillingUnbundledTransactionModalController', SearchBillingUnbundledTransactionModalController);

    SearchBillingUnbundledTransactionModalController.$inject = ['$scope', '$http', 'BillingUnbundledTransactionService', 'NgTableParams', '$uibModal', '$window', 'CommonService', '$timeout', '$q', '$uibModalInstance', 'categoryId', 'unbundledTransactionCategory', 'parentIndex', 'index', 'CommonService'];

    function SearchBillingUnbundledTransactionModalController($scope, $http, BillingUnbundledTransactionService, NgTableParams, $uibModal, $window, CommonService, $timeout, $q, $uibModalInstance, categoryId, unbundledTransactionCategory, parentIndex, index, $cs) {
        $scope.selectedBillingUnbundledTransactionId = null;
        //$scope.dataToRetrieve = monthlyRateUnbundledTransactions;
        $scope.dataToRetrieve = unbundledTransactionCategory[parentIndex].UnbundledTransactions
        $scope.selectBillingUnbundledTransaction = function () {
            if ($scope.selectedBillingUnbundledTransactionId != null) {
                BillingUnbundledTransactionService.GetbillingUnbundledTransactionLookUp({
                    id: $scope.selectedBillingUnbundledTransactionId
                }).then(function (data) {
                    $scope.$close(data);
                });
            }
            else {
                $cs.warningMessage("Nothing is selected, Please select billing unbundled transaction.");
            }
     
        }

        //Init
        this.$onInit = function () {
            getCategoryDetails();
            $scope.clear();
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

        $scope.clear = function () {
            $scope.Code = "";
            $scope.Name = "";

            $scope.sortColumn = "CreatedDate";
            $scope.sortOrder = "desc";

            $scope.init();
        };

        $scope.init = function () {
            var initialSettings = {
                getData: function (params) {

                    for (var i in params.sorting()) {
                        $scope.sortColumn = i;
                        $scope.sortOrder = params.sorting()[i];
                    }
                    var d = $q.defer();
                    var filter = params.filter();

                    BillingUnbundledTransactionService.GetBillingUnbundledTransactionByCategory({
                        categoryId: categoryId,
                        Page: params.page(),
                        PageSize: params.count(),
                        SortDirection: $scope.sortOrder,
                        SortColumn: $scope.sortColumn,
                        Code: filter["Code"],
                        Name: filter["Name"],
                    }).then(function (data) {
                        $scope.totalRecords = data.totalRecordCount;
                        params.total($scope.totalRecords);
                        for (var i = 0; i <= $scope.dataToRetrieve.length - 1; i++) {
                            var index = data.data.findIndex(r=>r.BillingUnbundledTransactionId == $scope.dataToRetrieve[i].BillingUnbundledTransactionId);
                            if (index != -1) {
                                data.data[index].Selected = true;
                            }
                       
                        }
                        d.resolve(data.data);
                    });
                    return d.promise;
                },
                counts: []
            };
            $scope.tableParams = new NgTableParams(10, initialSettings);
        }
        $scope.init();

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };

        $scope.getBillingUnbundledTransactionId = function (billingUnbundledTransactionId) {
            $scope.selectedBillingUnbundledTransactionId = billingUnbundledTransactionId;
        };
    }
})();
