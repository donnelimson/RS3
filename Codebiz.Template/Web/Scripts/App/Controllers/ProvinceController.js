angular.module('MetronicApp')
    .controller('ProvinceController',
        ['$scope', 'ProvinceService', 'NgTableParams', '$uibModal', '$q', 'CommonService',
            function ($scope, ProvinceService, NgTableParams, $uibModal, $q, CommonService) {

                $scope.SearchTerm = "";
                //Init
                this.$onInit = function () {
                    $scope.reset();
                };

                $scope.exportDataToExcelFile = function () {
                    if ($scope.resultsLength > 0) {
                        CommonService.showLoading();
                        ProvinceService.ExportDataToExcelFile({
                            SortDirection: $scope.sortOrder,
                            SortColumn: $scope.sortColumn,
                            SearchTerm: $scope.SearchTerm,
                            Name: $scope.Name,
                            Abbreviation: $scope.Abbreviation,
                            Region: $scope.Region,
                            IsActive: $scope.IsActive,
                            CreatedBy: $scope.CreatedBy,
                            CreatedDate: $scope.CreatedDate
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

                //Search User
                $scope.search = function () {
                    var initialSettings = {
                        getData: function (params) {
                            for (var i in params.sorting()) {
                                $scope.sortColumn = i;
                                $scope.sortOrder = params.sorting()[i];
                            }

                            var d = $q.defer();

                            ProvinceService.GetProvinces({
                                Page: params.page(),
                                PageSize: params.count(),
                                SortDirection: $scope.sortOrder,
                                SortColumn: $scope.sortColumn,
                                SearchTerm: $scope.SearchTerm,
                                Name: $scope.Name,
                                Abbreviation: $scope.Abbreviation,
                                Region: $scope.Region,
                                IsActive: $scope.IsActive,
                                CreatedBy: $scope.CreatedBy,
                                CreatedDate: $scope.CreatedDate
                            }).then(function (data) {
                                $scope.resultsLength = data.totalRecordCount;
                                params.total($scope.resultsLength);
                                d.resolve(data.data);
                            });
                            return d.promise;

                        }
                    };

                    $scope.tableParams = new NgTableParams(10, initialSettings);
                };

                //Reset filter fields
                $scope.reset = function () {
                    $scope.SearchTerm = "";
                    $scope.sortColumn = "CreatedDate";
                    $scope.sortOrder = "desc";
                    $scope.search();
                };

                $scope.openProvinceAddModal = function (provinceId) {
                    $uibModal.open({
                        animation: true,
                        ariaLabelledBy: 'modal-title',
                        ariaDescribedBy: 'modal-body',
                        templateUrl: 'ProvinceSaveModal.html',
                        controller: 'ProvinceSaveModalController',
                        controllerAs: 'controller',
                        size: 'md',
                        keyboard: false,
                        backdrop: "static",
                        windowClass: 'modal_style',
                        resolve: {
                            ProvinceId: function () {
                                return provinceId;
                            }
                        }

                    }).result.then(function (data) {
                        $scope.reset();
                    },
                        function () {

                        });
                };

                $scope.toggleActiveStatus = function (item) {
                    if (!item.IsActive) {
                        CommonService.reActivate(function () {
                            ProvinceService.ToggleProvinceActiveStatus({ id: item.ProvinceId })
                                .then(function (data) {
                                    CommonService.successMessage(data.message);
                                    $scope.search();

                                }), function (error /*Error event should handle here*/) {
                                    console.log(error);
                                    CommonService.errorMessage("Unexpected error occured.");
                                }, function (data /*Notify event should handle here*/) {
                                };
                        }, item.Name);
                    }
                    else {
                        CommonService.deActivate(function () {
                            ProvinceService.ToggleProvinceActiveStatus({ id: item.ProvinceId })
                                .then(function (data) {
                                    CommonService.successMessage(data.message);
                                    $scope.search();

                                }), function (error /*Error event should handle here*/) {
                                    console.log(error);
                                    CommonService.errorMessage("Unexpected error occured.");
                                }, function (data /*Notify event should handle here*/) {
                                };
                        }, item.Name);
                    }
                };

                $scope.searchWhenEnter = function ($event) {
                    var keyCode = $event.which || $event.keyCode;
                    if (keyCode === 13) {
                        $scope.search();
                    }
                };

            }]);