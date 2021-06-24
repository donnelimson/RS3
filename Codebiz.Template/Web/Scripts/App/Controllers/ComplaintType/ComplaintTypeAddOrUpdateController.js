angular.module('MetronicApp')
    .controller('ComplaintTypeAddOrUpdateController',
        ['$scope', 'ComplaintTypeService', '$window', 'CommonService', '$timeout',
            function ($scope, ComplaintTypeService, $window, CommonService, $timeout) {

                //#region Variable Defaults
                var self = this;

                $scope.formSubmitted = false;
                $scope.hasError = false;
                $scope.message = "";

                $scope.data = {
                    ComplaintTypeId: null,
                    NatureTypeId: null,
                    ComplaintSubTypes: []
                };

                $scope.natureTypes = [];

                var ComplaintId = angular.element("#ComplaintTypeId").val() || 0;

                $scope.isUpdate = ComplaintId > 0;
                $scope.ComplaintSubTypeIndex = 0;

                //#endregion

                //#region Init

                this.$onInit = function () {

                    if (ComplaintId === 0) {
                        $scope.isUpdate = false;
                        getNatureTypes();
                    }
                    else {
                        getComplaintTypeDetailsForUpdate();
                    }

                    initEmptyRow();
                };

                //#endregion

                //#region Save/Cancel

                $scope.cancel = function () {
                    if (self.complaintTypeForm.$dirty) {
                        CommonService.cancelChanges(function () {
                            $window.location.href = document.ComplaintType;
                        });
                    }
                    else {
                        $window.location.href = document.ComplaintType;
                    }
                };

                $scope.save = function () {
                    $scope.formSubmitted = true;

                    $scope.validateComplaints();
                    $scope.checkIfExist();

                    var hasDuplicated = $scope.data.ComplaintSubTypes.findIndex(r => r.IsExist);

                    if (self.complaintTypeForm.$valid && !$scope.hasError && hasDuplicated == -1) {
                        CommonService.saveOrUpdateChanges(function () {
                            CommonService.showLoading();

                            ComplaintTypeService.AddUpdateComplaintType({
                                model: $scope.data
                            }).then(function (data) {
                                if (data.success) {
                                    CommonService.hideLoading();
                                    CommonService.successMessage(data.message);

                                    $timeout(function () {
                                        $window.location.href = document.ComplaintType;
                                    }, 800);
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
                        }, ComplaintId);
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

                $scope.validateComplaints = function () {
                    if ($scope.data.ComplaintSubTypes.length <= 0) {
                        $scope.hasError = true;
                        $scope.message = "Please add at least 1 complaint.";
                    }
                    else {
                        $scope.hasError = false;
                        $scope.message = "";
                    }
                }

                //#endregion

                //#region Table

                $scope.addRow = function (index) {
                    $scope.data.ComplaintSubTypes.push($scope.emptyRow);
                    initEmptyRow();
                    $scope.validateComplaints();
                    self.complaintTypeForm.$dirty = true;
                }

                $scope.deleteRow = function (index) {
                    $scope.data.ComplaintSubTypes.splice(index, 1);
                    self.complaintTypeForm.$dirty = true;
                };


                $scope.checkIfExist = function() {
                    for (var i = 0; i < $scope.data.ComplaintSubTypes.length; i++) {
                        var data = $scope.data.ComplaintSubTypes[i];
                        var exist = $scope.data.ComplaintSubTypes.findIndex(r => r.Complaint === data.Complaint && r.SubTypeId != data.SubTypeId);

                        if (exist != -1) {
                            data.IsExist = true;
                        }
                        else {
                            data.IsExist = false;
                        }
                    }

                    self.complaintTypeForm.$dirty = true;
                }

                function initEmptyRow() {
                    $scope.emptyRow = {
                        SubTypeId: $scope.ComplaintSubTypeIndex--,
                        Complaint: "",
                        IsActive: true,
                        IsExist: false
                    };
                }

                //#endregion

                function getNatureTypes(id) {
                    ComplaintTypeService.GetNatureTypes({
                        IsEdit: $scope.isUpdate
                    }, "NatureTypeId").then(function (data) {
                        $scope.natureTypes = data.data;
                        if (id != null && id != undefined) {
                            $scope.data.NatureTypeId = id
                            self.complaintTypeForm.$dirty = false;
                        }
                    }, function (error /*Error event should handle here*/) {
                        console.log("Error");
                    }, function (data /*Notify event should handle here*/) {
                        console.log("Error");
                    });
                }

                function getComplaintTypeDetailsForUpdate() {

                    $scope.isUpdate = true;
                    ComplaintTypeService.GetComplaintTypeDetailsForUpdateById({
                        id: ComplaintId
                    }).then(function (data) {
                        $scope.data = data.data;
                        $scope.data.ComplaintTypeId = ComplaintId;
                        getNatureTypes($scope.data.NatureTypeId);

                    }), function (error /*Error event should handle here*/) {
                        CommonService.hideLoading();
                        console.log(error);
                        CommonService.errorMessage("Unexpected error occured.");
                    }, function (data /*Notify event should handle here*/) {
                    };
                }
            }])