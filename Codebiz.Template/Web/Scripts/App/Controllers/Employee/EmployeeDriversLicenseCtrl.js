angular.module('MetronicApp').controller('EmployeeDriversLicenseCtrl',
    ['$scope', 'EmployeeService', 'NgTableParams', '$uibModal', '$q', 'CommonService',
        function ($scope, EmployeeService, NgTableParams, $uibModal, $q, CommonService) {

            //#region Initialization

            this.$onInit = function () {
                $scope.reset();
            }

            //#endregion

            //#region Export Data to Excel

            $scope.exportDataToExcelFile = function () {
                if ($scope.resultsLength > 0) {
                    CommonService.showLoading();
                    EmployeeService.ExportEmployeeWithLicenseNoDataToExcelFile({
                        SortDirection: $scope.sortOrder,
                        SortColumn: $scope.sortColumn,
                        Name: $scope.name,
                        EmployeeNo: $scope.employeeNo,
                        LicenseNo: $scope.licenseNo,
                        ExpDate: $scope.expDate
                    }).then(function (data) {
                        CommonService.hideLoading();
                        window.location.href = document.FileUpload + "DownloadExcelFile?fileName=" + data.DataResult;

                    }, function (error /*Error event should handle here*/) {
                        console.log("Error");
                    }, function (data /*Notify event should handle here*/) {
                        console.log("Error");
                    });
                }
            };

            //#endregion

            //#region Search When Enter

            $scope.searchWhenEnter = function ($event) {
                var keyCode = $event.which || $event.keyCode;
                if (keyCode === 13) {
                    $scope.search();
                }
            };

            //#endregion

            //#region Search 

            $scope.search = function (init) {
                var initialSettings = {
                    getData: function (params) {
                        for (var i in params.sorting()) {
                            $scope.sortColumn = i;
                            $scope.sortOrder = params.sorting()[i];
                        }
                        var d = $q.defer();

                        EmployeeService.SearchEmployeeWithLicenseNo({
                            Page: params.page(),
                            PageSize: params.count(),
                            SortDirection: $scope.sortOrder,
                            SortColumn: $scope.sortColumn,
                            Name: $scope.name,
                            EmployeeNo: $scope.employeeNo,
                            LicenseNo: $scope.licenseNo,
                            ExpDate: $scope.expDate
                        }).then(function (data) {
                            $scope.resultsLength = data.TotalRecordCount;
                            params.total($scope.resultsLength);
                            d.resolve(data.DataResult);
                        });
                        return d.promise;

                    }
                };

                $scope.tableParams = new NgTableParams(10, initialSettings);

            };

            //#endregion

            //#region Reset filter fields

            $scope.reset = function () {
                $scope.name = "";
                $scope.employeeNo = null;
                $scope.licenseNo = "";
                $scope.expDate = null;

                $scope.sortColumn = "ActionOn";
                $scope.sortOrder = "desc";
                $scope.search();
            };

            //#endregion

            //#region Open Modal

            $scope.addOrUpdate = function (id) {
                $uibModal.open({
                    animation: true,
                    ariaLabelledBy: 'modal-title',
                    ariaDescribedBy: 'modal-body',
                    templateUrl: 'SaveForm.html',
                    controller: 'SaveFormController',
                    controllerAs: 'controller',
                    size: 'm',
                    keyboard: false,
                    backdrop: "static",
                    windowClass: 'modal_style',
                    resolve: {
                        EmployeeId: function () {
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
);