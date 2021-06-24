MetronicApp.controller('WorkOrderSaveModalController', ['$scope', '$uibModalInstance', 'NgTableParams', 'WorkOrderService', 'CommonService', 'WorkOrderId', '$timeout',
    function ($scope, $uibModalInstance, NgTableParams, WorkOrderService, CommonService, WorkOrderId, $timeout) {

        $scope.formSubmitted = false;

        $scope.workOrder = {
            WorkOrderId: null,
            Code: "",
            Name: ""
        };
        var workOrderDetails = angular.copy($scope.workOrder);

        //Initialize
        function init() {
            if (WorkOrderId === null || WorkOrderId === 0) {
                $scope.addOrEdit = "Add";
            } else {
                $scope.addOrEdit = "Edit";

                WorkOrderService.GetWorkOrderDetails({
                    workOrderId: WorkOrderId
                }).then(function (data) {
                    var data = data.data;
                    workOrderDetails = data;
                    $scope.workOrder.WorkOrderId = data.WorkOrderId;
                    $scope.workOrder.Code = data.Code;
                    $scope.workOrder.Name = data.Name;
                }
                );
            }
        }
        init();

        $scope.saveWorkOrder = function () {
            $scope.formSubmitted = true;
            if ($scope.saveWorkOrderForm.$valid) {
                if (WorkOrderId === 0 || WorkOrderId === null) {
                    CommonService.saveChanges(function () {
                        WorkOrderService.AddWorkOrder({
                            model: $scope.workOrder
                        }).then(function (data) {
                            if (data.success) {
                                CommonService.successMessage(data.message);
                                $uibModalInstance.close();
                            } else {
                                CommonService.warningMessage(data.message);
                            }
                        }), function (error /*Error event should handle here*/) {
                            console.log(error);
                            CommonService.errorMessage("Unexpected error occured.");
                        }, function (data /*Notify event should handle here*/) {
                        };
                    });

                } else {
                    CommonService.updateChanges(function () {
                        WorkOrderService.UpdateWorkOrder({
                            model: $scope.workOrder
                        }).then(function (data) {
                            if (data.success) {
                                CommonService.successMessage(data.message);
                                $uibModalInstance.close();
                            } else {
                                CommonService.warningMessage(data.message);
                            }
                        }), function (error /*Error event should handle here*/) {
                            console.log(error);
                            CommonService.errorMessage("Unexpected error occured.");
                        }, function (data /*Notify event should handle here*/) {
                        };
                    });
                }
            }
        },

        //CANCEL BUTTON
        $scope.cancel = function (data) {
            if (angular.equals($scope.workOrder, workOrderDetails)) {
                $uibModalInstance.dismiss('cancel');

            } else {
                CommonService.cancelChanges(function () {
                    $uibModalInstance.dismiss('cancel');
                });
            }
        };
    }]);