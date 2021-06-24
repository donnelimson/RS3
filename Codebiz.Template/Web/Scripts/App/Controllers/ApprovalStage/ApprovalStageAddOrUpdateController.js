angular.module('MetronicApp')
    .controller('ApprovalStageAddOrUpdateController',
        ['$scope', 'ApprovalStageService', 'NgTableParams', '$uibModal', '$window', 'CommonService', '$timeout', '$location', '$q',
            function ($scope, ApprovalStageService, $ngTable, $uibModal, $window, CommonService, $timeout, $location, $q) {

                //#region Variable Defaults

                $scope.approvalStage = {
                    ApprovalStageId: 0,
                    Name: "",
                    Description: "",
                    RequiredApprovals: null,
                    RequiredRejections: null,
                    LabelId: null,
                    ApprovalStageApprovers: [],
                };

                var paramaterFromUrl = $location.url().replace(/\//g, ".").split(".");
                if (paramaterFromUrl != undefined && paramaterFromUrl != null && paramaterFromUrl.length > 0) {
                    var data = paramaterFromUrl[paramaterFromUrl.length - 1];
                    $scope.approvalStage.ApprovalStageId = data == "" || data == null ? 0 : parseInt(data);
                }
                
                $scope.viewOnly = $location.absUrl().includes('View')
                $scope.isUpdate = $location.absUrl().includes('Edit');
                $scope.formSubmitted = false;

                //#endregion

                //#region Init

                this.$onInit = function () {
                    if ($scope.isUpdate || $scope.viewOnly) {
                        getApprovalStageDetails();
                    }
                    else {
                        getApprovalStageLabels(null);
                        $scope.ApproverListParams = new $ngTable({}, { dataset: $scope.approvalStage.ApprovalStageApprovers });
                    }
                };

                //#endregion

                //#region Approver

                $scope.addApprovers = function () {
                    var modalData = {
                        LookupType: 'USER',
                        Selections: $scope.approvalStage.ApprovalStageApprovers
                    }

                    $uibModal.open({
                        animation: true,
                        ariaLabelledBy: 'modal-title',
                        ariaDescribedBy: 'modal-body',
                        templateUrl: `${document.baseUrlNoArea}Lookups/ChooseFromList/GetLookupMultiple?objType=${modalData.LookupType}`,
                        controller: 'ChooseFromListMultipleSelectCtrl',
                        size: 'lg',
                        keyboard: false,
                        backdrop: "static",
                        windowClass: 'modal_dialog',
                        modalOverflow: true,
                        resolve: {
                            modalData: function () {
                                return modalData;
                            }
                        }
                    }).result.then(function (data) {
                        $scope.approvalStage.ApprovalStageApprovers = data.selectedData;
                        $scope.ApproverListParams = new $ngTable({}, { dataset: $scope.approvalStage.ApprovalStageApprovers });
                        $scope.approvalStageForm.$dirty = true;
                    }, function (error /*Error event should handle here*/) {
                        console.log("Error.");
                    }, function (data /*Notify event should handle here*/) {
                        console.log("Error");
                    });
                };

                $scope.deleteApprover = function (index) {
                    $scope.approvalStage.ApprovalStageApprovers.splice(index, 1);
                    $scope.ApproverListParams.reload();
                    $scope.approvalStageForm.$dirty = true;
                };

                //#endregion

                //#region Save/Cancel

                $scope.save = function () {
                    $scope.formSubmitted = true;

                    if ($scope.approvalStage.ApprovalStageApprovers.length <= 0) {
                        return false;
                    }

                    if ($scope.approvalStageForm.$valid) {
                        CommonService.saveOrUpdateChanges(function () {
                            ApprovalStageService.AddApprovalStage({
                                model: $scope.approvalStage
                            }).then(function (data) {
                                if (data.success) {
                                    CommonService.successMessage(data.message);
                                    $timeout(function () {
                                        $window.location.href = document.ApprovalStage;
                                    }, 800);
                                }
                                else {
                                    CommonService.warningMessage(data.message);
                                }

                            }), function (error /*Error event should handle here*/) {
                                console.log(error);
                                CommonService.errorMessage("Unexpected error occured.");
                            }, function (data /*Notify event should handle here*/) {
                            };
                        }, $scope.approvalStage.ApprovalStageId)
                    }
                    else {
                        if ((angular.element('.ng-invalid').length)) {
                            angular.element('.ng-invalid')[1].focus();
                        }
                    }
                }

                $scope.cancel = function () {
                    if ($scope.approvalStageForm.$dirty) {
                        CommonService.cancelChanges(function () {
                            window.location.href = document.ApprovalStage + "Index";
                        })
                    }
                    else {
                        window.location.href = document.ApprovalStage + "Index";
                    }
                }

                //#endregion


                function getApprovalStageDetails() {
                    $q.all([
                        ApprovalStageService.GetApprovalStageDetailsById({ id: $scope.approvalStage.ApprovalStageId }).then(d => { return d.data }),
                        ApprovalStageService.GetApprovalStageLabels({}).then(d => { return d.data })
                    ]).then(function (d) {
                        $scope.approvalStage = d[0];
                        $scope.ApproverListParams = new $ngTable({}, { dataset: $scope.approvalStage.ApprovalStageApprovers });
                        $scope.labels = d[1].filter(a => a.IsActive)
                    })
                }

                function getApprovalStageLabels(id) {
                    ApprovalStageService.GetApprovalStageLabels().then(function (data) {
                        $scope.labels = data.data.filter(a => a.IsActive || a.Id == id);
                        if (id != null) {
                            $scope.approvalStage.LabelId = id;
                            $scope.approvalStageForm.Label.$pristine = false;
                        }
                    }, function (error /*Error event should handle here*/) {
                        console.log("Error");
                    }, function (data /*Notify event should handle here*/) {
                    });
                }

                //#endregion

            }]);