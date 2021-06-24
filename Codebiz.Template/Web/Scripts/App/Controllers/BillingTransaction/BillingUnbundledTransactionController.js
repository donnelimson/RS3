angular.module('MetronicApp').controller('BillingUnbundledTransactionController',
        ['$scope', 'BillingUnbundledTransactionService', 'NgTableParams', '$uibModal', '$q', 'CommonService',
            function ($scope, BillingUnbundledTransactionService, NgTableParams, $uibModal, $q, CommonService) {

                //Other defaults
                $scope.categories = [];

                //Init
                this.$onInit = function () {
                    getCategories();
                    $scope.reset();
                }

                //Export Office Data to exel 
                $scope.exportDataToExcelFile = function () {
                    if ($scope.resultsLength > 0) {
                        CommonService.showLoading();

                        BillingUnbundledTransactionService.ExportDataToExcelFile({
                            Code: $scope.Code,
                            Name: $scope.Name,
                            DisplayName: $scope.DisplayName,
                            CategoryId: $scope.category,
                            IsActive: $scope.IsActive,
                            CreatedOnFrom: getDateRangePickerValue(1, $scope.createdDate),
                            CreatedOnTo: getDateRangePickerValue(2, $scope.createdDate),
                            SortDirection: $scope.sortOrder,
                            SortColumn: $scope.sortColumn
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

                //#region Scopes

                //Search Unbundled Transaction
                $scope.search = function () {
                    var initialSettings = {
                        getData: function (params) {
                            for (var i in params.sorting()) {
                                $scope.sortColumn = i;
                                $scope.sortOrder = params.sorting()[i];
                            }
                            var d = $q.defer();
                            var filter = params.filter();

                            BillingUnbundledTransactionService.GetBillingUnbundledTransactions({
                                Page: params.page(),
                                PageSize: params.count(),
                                SortDirection: $scope.sortOrder,
                                SortColumn: $scope.sortColumn,
                                Code: $scope.Code,
                                Name: $scope.Name,
                                DisplayName: $scope.DisplayName,
                                CategoryId: $scope.category,
                            }).then(function (data) {
                                params.total(data.totalRecordCount);
                                $scope.resultsLength = data.totalRecordCount;
                                d.resolve(data.data);
                            });
                            return d.promise;
                        }
                    };

                    $scope.tableParams = new NgTableParams(10, initialSettings);

                }

                //Reset filter fields
                $scope.reset = function () {
                    $scope.Code = "";
                    $scope.Name = "";
                    $scope.DisplayName = "";
                    $scope.category = null;


                    $scope.sortColumn = "CreatedDate";
                    $scope.sortOrder = "desc";
                    $scope.search();

                }

                $scope.openBillingUnbundledTransactionSaveModal = function (billingUnbundledTransactionId,forViewing) {
                    $uibModal.open({
                        animation: true,
                        ariaLabelledBy: 'modal-title',
                        ariaDescribedBy: 'modal-body',
                        templateUrl: 'BillingUnbundledTransactionSaveModal.html',
                        controller: 'BillingUnbundledTransactionModalController',
                        controllerAs: 'controller',
                        size: 'lg',
                        keyboard: false,
                        backdrop: "static",
                        windowClass: 'modal_style',
                        resolve: {
                            BillingUnbundledTransactionId: function () {
                                return billingUnbundledTransactionId;
                            },
                            ForViewingOnly: function () {
                                return forViewing;
                            },
                        }
                    }).result.then(function (data) {
                        $scope.reset();
                    },
                        function () {
                        });
                }

                $scope.toggleActiveStatus = function(item,name) {
                    if (!item.IsActive) {
                        CommonService.reActivate(function() {
                            BillingUnbundledTransactionService.ToggleBillingUnbundledTransactionActiveStatus({ id: item.BillingUnbundledTransactionId })
                                    .then(function(data) {
                                        CommonService.successMessage(data.message);
                                        $scope.search();

                                    }), function(error /*Error event should handle here*/) {
                                    console.log(error);
                                    CommonService.errorMessage("Unexpected error occured.");
                                }, function(data /*Notify event should handle here*/) {
                                }
                            },
                            item.Name)
                    } else {
                        CommonService.deActivate(function() {
                            BillingUnbundledTransactionService.ToggleBillingUnbundledTransactionActiveStatus({ id: item.BillingUnbundledTransactionId })
                                    .then(function(data) {
                                        CommonService.successMessage(data.message);
                                        $scope.search();

                                    }), function(error /*Error event should handle here*/) {
                                    console.log(error);
                                    CommonService.errorMessage("Unexpected error occured.");
                                }, function(data /*Notify event should handle here*/) {
                                }
                            },
                            item.Name)
                    }
                }

                $scope.deleteBillingUnbundledTransaction = function (billingUnbundledTransactionId,name) {
                    CommonService.deleteChanges(function() {
                        BillingUnbundledTransactionService.billingUnbundledTransactionDelete({ id: billingUnbundledTransactionId }).then(function (data) {
                            if (data.success) {
                                CommonService.successMessage(data.message);
                                $scope.search(true);
                            }
                        })
                    },name)
                }

                $scope.searchWhenEnter = function ($event) {
                    var keyCode = $event.which || $event.keyCode;
                    if (keyCode === 13) {
                        $scope.search();
                    }
                }

               // #endregion

                //#region Private functions

                function getCategories() {
                    BillingUnbundledTransactionService.GetCategories({
                    }).then(function (data) {
                        $scope.categories = data.data;
                    },
                        function (error /*Error event should handle here*/) {
                            console.log("Error");
                        },
                        function (data /*Notify event should handle here*/) {
                        })
                }


                //#endregion

}]);