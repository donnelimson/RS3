MetronicApp.controller('JobOrderManagementAddOrUpdateController', ['$scope', '$q', 'NgTableParams', 'CommonService', '$location', '$window', 'JobOrderManagementService', '$route',
    function ($scope, $q, NgTableParams, CommonService, $location, $window, JobOrderManagementService, $route) {

        //#region Variables

        var jobOrderType = $route.current.$$route.params.jobOrderType;

        $scope.formSubmitted = false;
        $scope.Id = 0;
        $scope.jobOrderdata = {
            Id : $scope.Id,
            JobOrderType: jobOrderType,
            DataResult: []
        }

        //#endregion

        //#region Initialization

        this.$onInit = function () {
            initEmptyRow(null);
            getAllJOType();
            $scope.sortOrder = "desc";
            $scope.sortColumn = "JobOrderData";
        }

        //#endregion

        //#region Save / Cancel Changes

        $scope.saveJOType = function () {
            $scope.formSubmitted = true;

            $scope.validateJO();
            $scope.checkIfExist();

            var hasDuplicated = $scope.jobOrderdata.DataResult.findIndex(r => r.IsExist);

            if ($scope.form.$valid && !$scope.hasError && hasDuplicated == -1) {
                CommonService.saveChanges(function () {
                    saveChanges(false, false), function (error /*Error event should handle here*/) {
                        console.log(error);
                        CommonService.errorMessage("Unexpected error occured.");
                    }, function (data /*Notify event should handle here*/) {
                    };
                }, 0)
            }
        }

        function saveChanges(forDelete, forEdit) {
            if (jobOrderType === 'Task To Be Performs') {
                JobOrderManagementService.SaveTaskToBePerform({
                    model: $scope.jobOrderdata,
                    IsForDelete: forDelete,
                    IsForEdit: forEdit,
                }).then(function (data) {
                    if (data.success) {
                        CommonService.successMessage(data.message);
                        $location.path("");
                    }
                    else {
                        CommonService.warningMessage(data.message);
                    }
                })
            }

            if (jobOrderType === 'Nature Types') {
                JobOrderManagementService.SaveNatureType({
                    model: $scope.jobOrderdata,
                    IsForDelete: forDelete,
                    IsForEdit: forEdit,
                }).then(function (data) {
                    if (data.success) {
                        CommonService.successMessage(data.message);
                        $location.path("");
                    }
                    else {
                        CommonService.warningMessage(data.message);
                    }
                })
            }
        }

        $scope.cancel = function () {
            if ($scope.form.$dirty) {
                CommonService.cancelChanges(function () {
                    $window.location.href = "#";
                });
            }
            else {
                $window.location.href = "#";
            }
        }

        //#endregion

        //#region Table

        $scope.addRow = function (index) {
            $scope.jobOrderdata.DataResult.push($scope.emptyRow);
            initEmptyRow();
        }

        $scope.deleteRow = function (index) {
            $scope.jobOrderdata.DataResult.splice(index, 1);
            $scope.hideAddRows = false;
            $scope.form.$dirty = true;
            $scope.checkIfExist();
        };

        $scope.checkIfExist = function () {
            for (var i = 0; i < $scope.jobOrderdata.DataResult.length; i++) {
                var data = $scope.jobOrderdata.DataResult[i];
                var exist = $scope.jobOrderdata.DataResult.findIndex(r => r.JobOrderData === data.JobOrderData && r.JobOrderDataId != data.JobOrderDataId);

                if (exist != -1) {
                    data.IsExist = true;
                }
                else {
                    data.IsExist = false;
                }
            }

            $scope.form.$dirty = true;
        }

        $scope.validateJO = function () {
            if ($scope.jobOrderdata.DataResult.length <= 0) {
                $scope.hasError = true;
                $scope.message = "Please add at least 1 " + jobOrderType;
            }
            else {
                $scope.hasError = false;
                $scope.message = "";
            }
        }

        function initEmptyRow() {
            if (jobOrderType === 'Task To Be Performs') {
                $scope.emptyRow = {
                    JobOrderDataId: $scope.Id,
                    JobOrderData: "",
                    IsActive: true,
                    IsEdit: true
                }

                GetJobOrderNatureTypesForSelect();
            }

            if (jobOrderType === 'Nature Types') {
                $scope.emptyRow = {
                    JobOrderDataId: $scope.Id,
                    JobOrderData: "",
                    ForJORequest: false,
                    ForJOComplaint: false,
                    IsActive: true,
                    IsEdit: true
                }
            }

            $scope.clicked = false;
            $scope.Id = $scope.Id - 1;
        }

         //#endregion

        //#region Table Functions

        function getAllJOType() {
            var initialSettings = {

                getData: function (params) {
                    for (var i in params.sorting()) {
                        $scope.sortColumn = i;
                        $scope.sortOrder = params.sorting()[i];
                    }
                 

                    if (jobOrderType === 'Task To Be Performs') {
                        $scope.jobOrderType = 'Task to be perform';
                        $scope.jobOrderTypeLower = 'task to be perform';
                        $scope.jobOrderTitle = 'TASK TO BE PERFORM';
                        GetJobOrderTaskToPerforms(params);
                    }

                    if (jobOrderType === 'Nature Types') {
                        $scope.jobOrderType = 'Nature type';
                        $scope.jobOrderTypeLower = 'nature type';
                        $scope.jobOrderTitle = 'NATURE TYPE';
                        GetJobOrderNatureTypes(params);
                    }
                 
                },
                counts: []
            };

            $scope.tableParams = new NgTableParams(10, initialSettings);
        }

        function GetJobOrderTaskToPerforms(params) {

            var d = $q.defer();

            JobOrderManagementService.GetJobOrderTaskToPerforms({
                SortDirection: $scope.sortOrder,
                SortColumn: $scope.sortColumn,
                JobOrderType: jobOrderType
            }).then(function (data) {
                params.total(data.TotalRecordCount);
                $scope.jobOrderdata.DataResult = data.DataResult;
                $scope.jobOrderTypeCopy = data;
                d.resolve($scope.jobOrderdata.DataResult);
            });

            return d.promise;
        }

        function GetJobOrderNatureTypes(params) {
            var d = $q.defer();
            JobOrderManagementService.GetJobOrderNatureTypes({
                SortDirection: $scope.sortOrder,
                SortColumn: $scope.sortColumn,
                JobOrderType: jobOrderType
            }).then(function (data) {
                params.total(data.TotalRecordCount);
                $scope.jobOrderTypeCopy = data;
                $scope.jobOrderdata.DataResult = data.DataResult;
                d.resolve($scope.jobOrderdata.DataResult);
            });
            return d.promise;
        }

        function GetJobOrderNatureTypesForSelect() {
            var d = $q.defer();
            JobOrderManagementService.GetJobOrderNatureTypesForSelect({
            }).then(function (data) {
                $scope.NatureTypes = data.DataResult;
                d.resolve($scope.NatureTypes);
            });
        }

        //#endregion
    }
])