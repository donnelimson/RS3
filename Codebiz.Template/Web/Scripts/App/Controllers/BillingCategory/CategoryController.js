angular.module('MetronicApp').controller('CategoryController',
    ['$scope', 'BillingCategoryService', 'NgTableParams', '$q', 'CommonService', '$timeout','$window',
        function ($scope, BillingCategoryService, NgTableParams, $q, CommonService, $timeout, $window) {

            this.$onInit = function () {
                $scope.search();
                $scope.reset();
            }

            //#region $scopes

            //Export Office Data to exel 
            $scope.exportDataToExcelFile = function () {
                if ($scope.sortColumn === "ModifiedIndex") {
                    $scope.sortColumn = "Code";
                }
                if ($scope.resultsLength > 0) {
                    CommonService.showLoading();

                    BillingCategoryService.ExportDataToExcelFile({
                        Code: $scope.Code,
                        Name: $scope.Name,
                        DisplayedName: $scope.DisplayName,
                        IsActive: $scope.IsActive,
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

            //Search Category
            $scope.search = function () {
                var initialSettings = {
                    getData: function (params) {
                        for (var i in params.sorting()) {
                            $scope.sortColumn = i;
                            $scope.sortOrder = params.sorting()[i];
                        }
                        var d = $q.defer();
                        var filter = params.filter();

                        BillingCategoryService.GetCategories({
                            Page: params.page(),
                            PageSize: params.count(),
                            SortDirection: $scope.sortOrder,
                            SortColumn: $scope.sortColumn,
                            Code: $scope.Code,
                            Name: $scope.Name,
                            DisplayedName: $scope.DisplayName,
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

            $scope.gotoSaveCategoryPage = function () {
                CommonService.showLoading();
                $timeout(function () {
                    $window.location.href = document.BillingCategory + "AddEditCategory";
                }, 500);
            }

            $scope.reset = function () {
                $scope.Code = "";
                $scope.Name = "";
                $scope.DisplayName = "";
                $scope.sortOrder = null;
                $scope.sortColumn = null;

                $scope.sortColumn = "ModifiedIndex";
                $scope.sortOrder = "asc";
                $scope.search();
            }

            $scope.searchWhenEnter = function ($event) {
                var keyCode = $event.which || $event.keyCode;
                if (keyCode === 13) {
                    $scope.search();
                }
            }

            $scope.toggleActiveStatus = function (item) {
                CommonService.reactivateDeactivate(function () {
                    BillingCategoryService.ToggleCategoryActiveStatus({ id: item.CategoryId, name: item.Name, status: item.IsActive })
                        .then(function (data) {
                            if (data.success) {
                                CommonService.successMessage(data.message);
                                $scope.search();
                            }
                            else {
                                CommonService.warningMessage(data.message);
                            }
                        }), function (error /*Error event should handle here*/) {
                            console.log(error);
                            CommonService.errorMessage("Unexpected error occured.");
                        }, function (data /*Notify event should handle here*/) {
                        }
                }, item.Name, item.IsActive)
            }

            //#endregion

        }
    ]
);