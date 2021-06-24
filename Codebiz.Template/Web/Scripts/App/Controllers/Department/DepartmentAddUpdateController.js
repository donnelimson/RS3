MetronicApp.controller('DepartmentAddUpdateController', ['$scope', 'DepartmentService', 'CommonService', '$window', '$timeout',
    function ($scope, DepartmentService, CommonService, $window, $timeout) {

        var DepartmentId = angular.element("#DepartmentId").val();
        $scope.formSubmitted = false;

        //#region Other defaults

        $scope.department = {
            Code: "",
            Name: "",
            Details: []
        }

        $scope.positions = [];
        $scope.divisions = [];

        $scope.departmentIndex = document.Department + "Index";
        $scope.departmentDetails = angular.copy($scope.department);
        $scope.Id = 0;

         //#endregion

        //#region Initialization

        this.$onInit = function () {
            initEmptyRow(null);
            getPositions();
            getDivisions();

            if (DepartmentId == null || DepartmentId == 0) {
                $scope.addOrEdit = "Add";
                $scope.saveOrUpdate = "Save";
            }
            else {
                $scope.addOrEdit = "Edit";
                $scope.saveOrUpdate = "Update";
                getDepartmentDetails();
            }
        }

        //#endregion

        $scope.backToList = function () {
            $window.location.href = document.Department + "Index";
        }

        //#region Table Details

        $scope.addRow = function () {
            $scope.departmentHasError = false;
            $scope.department.Details.push($scope.emptyRow);
            $scope.required = true;
            initEmptyRow();
        };

        $scope.checkIfValid = function (positionRow, index, fromDivision) {
            $scope.department.Details[index].IsExist = false;
            checkIfPositionExists(positionRow.PositionId, index);
            $scope.required = false;
            if (fromDivision) {
                DepartmentService.GetDivisionCategoryByDivisionId({ id: positionRow.DivisionId },index).then(function (d) {
                    if (d.result != null) {
                      $scope.department.Details[index].CategoryId = null;
                     $scope.department.Details[index].Categories = d.result;
                    }
                })
          
            }
        }

        $scope.deleteRow = function (index) {
            $scope.department.Details.splice(index, 1);
            $scope.hideAddRows = false;
            $scope.IsExist = false;
        };

        //#endregion

        //#region Save button

        $scope.saveDepartment = function () {
            $scope.formSubmitted = true;
            $scope.departmentHasError = $scope.department.Details.length == 0;
            if ($scope.saveDepartmentForm.$valid && !$scope.departmentHasError && !$scope.IsExist) {
                if (DepartmentId == 0 || DepartmentId == null) {
                    CommonService.saveChanges(function () {
                        DepartmentService.AddDepartment({
                            model: $scope.department
                        }).then(function (data) {
                            if (data.success) {
                                CommonService.successMessage(data.message);
                                $timeout(function () {
                                    $window.location.href = $scope.departmentIndex;
                                }, 800);
                            } else {
                                CommonService.warningMessage(data.message);
                            }
                        }), function (error /*Error event should handle here*/) {
                            console.log(error);
                            CommonService.errorMessage("Unexpected error occured.");
                        }, function (data /*Notify event should handle here*/) {
                        }
                    })

                } else {
                    CommonService.updateChanges(function () {
                        DepartmentService.UpdateDepartment({
                            model: $scope.department
                        }).then(function (data) {
                            if (data.success) {
                                CommonService.successMessage(data.message);
                                $timeout(function () {
                                    $window.location.href = $scope.departmentIndex;
                                }, 800);
                            } else {
                                CommonService.warningMessage(data.message);
                            }
                        }), function (error /*Error event should handle here*/) {
                            console.log(error);
                            CommonService.errorMessage("Unexpected error occured.");
                        }, function (data /*Notify event should handle here*/) {
                        }
                    })
                }
            }
        }

        $scope.cancel = function () {
            if (angular.equals($scope.department, $scope.departmentDetails)) {
                $window.location.href = $scope.departmentIndex;
            } else {
                CommonService.cancelChanges(function () {
                    $window.location.href = $scope.departmentIndex;
                });
            }
        }

        //#endregion

        function initEmptyRow() {

            $scope.emptyRow = {
                id: $scope.Id,
                PositionId: null
            }

            $scope.Id = $scope.Id + 1;
        }

        function checkIfPositionExists(positionId, index) {
            $scope.IsExist = false;
            for (var i = 0; i < $scope.department.Details.length; i++) {
                if ($scope.department.Details[i].PositionId === positionId && index !== i) {
                    $scope.department.Details[index].IsExist = true;
                    $scope.IsExist = true;
                }
            }
        }

        function getPositions(dataValue, id) {
            if ($scope.positions.length <= 0) {
                CommonService.GetAllPositions({
                }, "").then(function (data) {
                    $scope.positions = data.data;
                    if (id != null && id != undefined) {
                        dataValue.PositionId = id;
                    }

                }, function (error /*Error event should handle here*/) {
                    console.log("Error");
                },
                    function (data /*Notify event should handle here*/) {
                        console.log("Error");
                    });
            }
            else {
                dataValue.PositionId = id;
            }
        }

        function getDivisions(dataValue, id) {
            if ($scope.divisions.length <= 0) {
                CommonService.GetDivisions({
                }, "").then(function (data) {
                    $scope.divisions = data.data;
                    if (id != null && id != undefined) {
                        dataValue.Division = id;
                    }
                }, function (error /*Error event should handle here*/) {
                    console.log("Error");
                },
                function (data /*Notify event should handle here*/) {
                    console.log("Error");
                });
            }
            else {
                dataValue.Division = id;
            }
        }

        function getDepartmentDetails() {
            DepartmentService.GetDepartmentDetails({
                departmentId: DepartmentId
            }).then(function (data) {
                $scope.department = data.data;
                $scope.departmentDetails = angular.copy($scope.department);
              //  populateCategories();
              
            }, function (error /*Error event should handle here*/) {
                console.log("Error");
            }, function (data /*Notify event should handle here*/) {
            });
        }
    

    }]);