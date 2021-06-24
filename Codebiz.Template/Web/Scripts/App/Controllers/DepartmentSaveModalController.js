MetronicApp.controller('DepartmentSaveModalController', ['$scope', '$uibModalInstance', 'NgTableParams', 'DepartmentService', 'CommonService', 'DepartmentId', '$timeout',
    function ($scope, $uibModalInstance, NgTableParams, DepartmentService, CommonService, DepartmentId, $timeout) {

        $scope.formSubmitted = false;
        $scope.department = {
            DepartmentId: null,
            Code: "",
            Name: ""
        }
        var departmentDetails = angular.copy($scope.department);

        //Other defaults
        function init() {
            if (DepartmentId == null || DepartmentId == 0) {
                $scope.addOrEdit = "Add";
            } else {
                $scope.addOrEdit = "Edit";

                DepartmentService.GetDepartmentDetails({
                    departmentId: DepartmentId
                }).then(function(data) {
                    var data = data.data;
                    departmentDetails = data;
                        
                    $scope.department.DepartmentId = data.DepartmentId;
                    $scope.department.Code = data.Code;
                    $scope.department.Name = data.Name;
                    }
                );
            }
        }

        init();

        //SAVE Depar
        $scope.saveDepartment = function() {
            $scope.formSubmitted = true;
            if ($scope.saveDepartmentForm.$valid) {
                if (DepartmentId == 0 || DepartmentId == null) {
                    CommonService.saveChanges(function() {
                        DepartmentService.AddDepartment({
                            model: $scope.department
                        }).then(function(data) {
                            if (data.success) {
                                CommonService.successMessage(data.message);
                                $uibModalInstance.close();
                            } else {
                                CommonService.warningMessage(data.message);
                            }
                        }), function(error /*Error event should handle here*/) {
                            console.log(error);
                            CommonService.errorMessage("Unexpected error occured.");
                        }, function(data /*Notify event should handle here*/) {
                        }
                    });

                } else {
                    CommonService.updateChanges(function() {
                        DepartmentService.UpdateDepartment({
                            model: $scope.department
                        }).then(function(data) {
                            if (data.success) {
                                CommonService.successMessage(data.message);
                                $uibModalInstance.close();
                            } else {
                                CommonService.warningMessage(data.message);
                            }
                        }), function(error /*Error event should handle here*/) {
                            console.log(error);
                            CommonService.errorMessage("Unexpected error occured.");
                        }, function(data /*Notify event should handle here*/) {
                        }
                    })
                }
            }
        }

        //CANCEL BUTTON
        $scope.cancel = function (data) {
            if (angular.equals($scope.department, departmentDetails)) {
                $uibModalInstance.dismiss('cancel');

            } else {
                CommonService.cancelChanges(function () {
                    $uibModalInstance.dismiss('cancel');
                });
            }
        }
    }]);