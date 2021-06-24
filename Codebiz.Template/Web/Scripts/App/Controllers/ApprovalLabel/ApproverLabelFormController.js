angular.module('MetronicApp')
    .controller('ApproverLabelFormController',
        ['$scope', 'ApproverLabelService', '$window', 'CommonService',
            function ($scope, ApproverLabelService, $window, CommonService) {

                //#region Variable Defaults

                $scope.formSubmitted = false;
                $scope.hasError = false;
                $scope.message = "";

                $scope.approverLabels = [];

                //#endregion

                //#region Init

                this.$onInit = function () {
                    getDetailsToUpdate();
                    initEmptyRow();
                };

                //#endregion

                //#region Save/Cancel

                $scope.cancel = function () {
                    if ($scope.approverLabelForm.$dirty) {
                        CommonService.cancelChanges(function () {
                            $window.location.href = document.ApproverLabel;
                        });
                    }
                    else {
                        $window.location.href = document.ApproverLabel;
                    }
                };

                $scope.save = function () {
                    $scope.formSubmitted = true;

                    $scope.validateData();
                    $scope.checkIfExist();

                    var hasDuplicated = $scope.approverLabels.findIndex(r => r.IsExist);

                    if ($scope.approverLabelForm.$valid && !$scope.hasError && hasDuplicated == -1) {
                        CommonService.saveOrUpdateChanges(function () {
                            CommonService.showLoading();

                            ApproverLabelService.UpdateData({
                                model: $scope.approverLabels
                            }).then(function (data) {
                                if (data.success) {
                                    CommonService.hideLoading();
                                    CommonService.successMessage(data.message);

                                    $window.location.href = document.ApproverLabel;
                                }
                                else {
                                    CommonService.hideLoading();
                                    CommonService.warningMessage(data.message);
                                }
                            }), function (error /*Error event should handle here*/) {
                                CommonService.hideLoading();
                                console.log(error);
                                CommonService.errorMessage("Unexpected error occured.");
                            }, function (data /*Notify event should handle here*/) {
                            };
                        }, 0);
                    }
                    else {
                        if (angular.element('.ng-invalid').length) {
                            angular.element('.ng-invalid')[1].focus();
                        }
                        else if (angular.element('input.has-error').length) {
                            angular.element('input.has-error')[0].focus();
                        }
                    }
                };

                $scope.validateData = function () {
                    if ($scope.approverLabels.length <= 0) {
                        $scope.hasError = true;
                        $scope.message = "Please add at least 1 approver label.";
                    }
                    else {
                        $scope.hasError = false;
                        $scope.message = "";
                    }
                }

                //#endregion

                //#region Table

                $scope.addRow = function (index) {
                    $scope.approverLabels.push($scope.emptyRow);
                    initEmptyRow();
                    $scope.validateData();
                    $scope.approverLabelForm.$dirty = true;
                }

                $scope.checkIfInUse = function (index, boolValue) {    
                    //if ($scope.approverLabels[index].IsApproverLabelInUse) {
                    //    CommonService.warningMessage("Unable to delete " + $scope.approverLabels[index].Name + ". " + $scope.approverLabels[index].Name + " is in use");
                    //    $scope.approverLabels[index].IsActive = true;
                    //}
                    //else {
                    //    $scope.approverLabels[index].IsActive = boolValue;
                    //}
                }

                $scope.deleteRow = function (index) {
                    if ($scope.approverLabels[index].IsApproverLabelInUse) {
                        CommonService.warningMessage("Unable to delete " + $scope.approverLabels[index].Name + ". " + $scope.approverLabels[index].Name + " is in use");
                    }
                    else {
                        $scope.approverLabels.splice(index, 1);
                        $scope.approverLabelForm.$dirty = true;
                        $scope.checkIfExist();
                    }
                };

                $scope.checkIfExist = function () {
                    for (var i = 0; i < $scope.approverLabels.length; i++) {
                        var data = $scope.approverLabels[i];
                        var exist = $scope.approverLabels.findIndex(r => r.Name.toUpperCase() === data.Name.toUpperCase() && r.ApproverLableId != data.ApproverLableId);

                        if (exist != -1) {
                            data.IsExist = true;
                        }
                        else {
                            data.IsExist = false;
                        }
                    }

                   $scope.approverLabelForm.$dirty = true;
                }

                function initEmptyRow() {
                    $scope.emptyRow = {
                        ApproverLableId: 0,
                        Name: "",
                        IsActive: true,
                        IsExist: false,
                        CanEdit: true,
                        IsApproverLabelInUse: false
                    };
                }

                //#endregion

                function getDetailsToUpdate() {

                    ApproverLabelService.GetDetailsToUpdate({}).then(function (data) {
                        $scope.approverLabels = data.data;

                    }), function (error /*Error event should handle here*/) {
                        CommonService.hideLoading();
                        console.log(error);
                        CommonService.errorMessage("Unexpected error occured.");
                    }, function (data /*Notify event should handle here*/) {
                    };
                }
            }])