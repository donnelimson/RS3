angular.module('MetronicApp').controller('SalesEmployeeController',
    ['$scope', 'SalesEmployeeService', 'NgTableParams', '$uibModal', '$q', 'CommonService',
        function ($scope, SalesEmployeeService, NgTableParams, $uibModal, $q, CommonService) {


            //#region Default Variables

            $scope.commissionsGroups = null;
            $scope.offices = null;

             //#endregion

            //#region Initialization

            this.$onInit = function () {
                $scope.search();
                $scope.sortColumn = "CreatedOn";
                $scope.sortOrder = "desc";

                GetCommissionGroups();
                getOffices();
            }

            //#endregion

            $scope.searchWhenEnter = function ($event) {
                var keyCode = $event.which || $event.keyCode;
                if (keyCode === 13) {
                    $scope.search();
                }
            };

            //#region Export Data to Excel

            $scope.exportDataToExcelFile = function () {
                if ($scope.resultsLength > 0) {
                    CommonService.showLoading();
                    SalesEmployeeService.ExportDataToExcelFile({
                        SortDirection: $scope.sortOrder,
                        SortColumn: $scope.sortColumn,
                        SalesEmployee: $scope.companyName,
                        Position: $scope.position,
                        AreaOfficeId: $scope.areaOfficeId,
                        CommissionGroupId: $scope.commissionGroupId,
                        Commission: $scope.commission
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

            //#region Search 

            $scope.search = function () {
                var initialSettings = {
                    getData: function (params) {
                        for (var i in params.sorting()) {
                            $scope.sortColumn = i;
                            $scope.sortOrder = params.sorting()[i];
                        }
                        var d = $q.defer();

                        SalesEmployeeService.GetSalesEmployee({
                            Page: params.page(),
                            PageSize: params.count(),
                            SortDirection: $scope.sortOrder,
                            SortColumn: $scope.sortColumn,
                            SalesEmployee: $scope.salesEmployee,
                            Position: $scope.position,
                            AreaOfficeId: $scope.areaOfficeId,
                            CommissionGroupId: $scope.commissionGroupId,
                            Commission: $scope.commission
                        }).then(function (data) {
                            $scope.resultsLength = data.TotalRecordCount;
                            console.log(data);
                            params.total($scope.resultsLength);
                            d.resolve(data.DataResult);
                        });
                        return d.promise;

                    }
                };

                $scope.tableParams = new NgTableParams(10, initialSettings);

            }

            //#endregion

            //#region Reset filter fields

            $scope.reset = function () {
                $scope.salesEmployee = "";
                $scope.areaOfficeId = null;
                $scope.position = "";
                $scope.commissionGroupId = null;
                $scope.commission = "";
                $scope.search();
            }

            //#endregion

            //#region Delete

            $scope.deleteSalesEmployee = function (item) {
                CommonService.deleteChanges(function () {
                    SalesEmployeeService.DeleteSalesEmployee({
                        id: item.SalesEmployeeId
                    }).then(function (data) {
                        if (data.DataResult.Success) {
                            CommonService.successMessage(data.DataResult.Message);
                            $scope.search();
                        }
                        else {
                            CommonService.warningMessage(data.message);
                        }
                    });

                });
            }

             //#endregion

            //#region Private functions

            function GetCommissionGroups() {
                SalesEmployeeService.GetCommissionGroups({
                }, "CommissionGroupId").then(function (data) {
                    $scope.commissionsGroups = data.data;
                }, function (error /*Error event should handle here*/) {
                    console.log("Error");
                }, function (data /*Notify event should handle here*/) {
                });
            }

            function getOffices() {
                CommonService.GetOfficesLookUp({
                }, "OfficeId").then(function (data) {
                    $scope.offices = data.data;
                }, function (error /*Error event should handle here*/) {
                    console.log("Error");
                }, function (data /*Notify event should handle here*/) {
                });
            }

            //#endregion

            //#region Open Modal

            $scope.addOrUpdate = function (id) {
                $uibModal.open({
                    animation: true,
                    ariaLabelledBy: 'modal-title',
                    ariaDescribedBy: 'modal-body',
                    templateUrl: 'SaveModalForm.html',
                    controller: 'SaveFormController',
                    controllerAs: 'controller',
                    size: 'md',
                    keyboard: false,
                    backdrop: "static",
                    windowClass: 'modal_style',
                    resolve: {
                        SalesEmployeeId: function () {
                            return id;
                        }
                    }
                }).result.then(function (data) {
                    $scope.reset();
                },
                    function () {
                    });
            };

            //#endregion
        }
    ]
)

