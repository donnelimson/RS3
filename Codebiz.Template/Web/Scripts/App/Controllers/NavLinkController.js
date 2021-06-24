angular.module('MetronicApp')
    .controller('NavLinkController',
        ['$scope', 'NavLinkServices', 'NgTableParams', '$window', '$uibModal', 'CommonService', '$q', '$timeout',
            function ($scope, NavLinkServices, NgTableParams, $window, $uibModal, CommonService, $q, $timeout) {

                $scope.createdOnDatePicker = {
                    opened: false
                };
                //Init
                this.$onInit = function () {
                    $scope.reset();
                },
                    $scope.navLinkIndex = document.NavLink;

                //#region Export to Excel
                $scope.exportDataToExcelFile = function () {
                    if ($scope.resultsLength > 0) {
                        CommonService.showLoading();
                        NavLinkServices.ExportDataToExcelFile({
                            SortDirection: $scope.sortOrder,
                            SortColumn: $scope.sortColumn,
                            SearchTerm: $scope.SearchTerm,
                            CreatedBy: $scope.CreatedBy,
                            CreatedOnFrom: getDateRangePickerValue(1, $scope.createdDate),
                            CreatedOnTo: getDateRangePickerValue(2, $scope.createdDate)
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
                //#endregion

                //#region View
                $scope.search = function (init) {
                    var initialSettings = {
                        getData: function (params) {
                            for (var i in params.sorting()) {
                                $scope.sortColumn = i;
                                $scope.sortOrder = params.sorting()[i];
                            }
                            var d = $q.defer();
                            var filter = params.filter();

                            NavLinkServices.SearchNavLink({
                                searchTerm: $scope.SearchTerm,
                                page: params.page(),
                                pageSize: params.count(),
                                sortOrder: $scope.sortOrder,
                                sortColumn: $scope.sortColumn,
                                IsActive: $scope.IsActive,
                                CreatedBy: $scope.CreatedBy,
                                createdOnFrom: getDateRangePickerValue(1, $scope.createdDate),
                                createdOnTo: getDateRangePickerValue(2, $scope.createdDate),
                            }).then(function (data) {
                                $scope.resultsLength = data.totalRecordCount;
                                params.total($scope.resultsLength);
                                d.resolve(data.data);
                            });
                            return d.promise;
                        }
                    };

                    $scope.tableParams = new NgTableParams(10, initialSettings);

                },

                    //Reset filter fields
                    $scope.reset = function () {
                        $scope.SearchTerm = "";
                        $scope.sortOrder = "desc";
                        $scope.sortColumn = "CreatedDate";
                        $scope.CreatedBy = "";
                        $scope.createdDate = null;
                        $scope.search(true);
                    },

                    $scope.searchWhenEnter = function ($event) {
                        var keyCode = $event.which || $event.keyCode;
                        if (keyCode === 13) {
                            $scope.search();
                        }
                    };
                //#endregion

                //#region Delete
                $scope.deleteNavLink = function (navLinkId, name) {
                    CommonService.deleteChanges(function () {
                        NavLinkServices.DeleteNavLink({ id: navLinkId }).then(function () {
                            CommonService.successMessage("NavLink was successfully deleted.");
                            $scope.search(true);
                        });
                    },name);
                };
                 //#endregion

            }]);