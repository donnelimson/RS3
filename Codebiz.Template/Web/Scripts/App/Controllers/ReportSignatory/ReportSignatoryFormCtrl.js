angular.module('MetronicApp').controller('ReportSignatoryFormCtrl',
    ['$scope', 'ReportSignatoryService', '$uibModal', '$timeout', '$window', '$q', 'CommonService',
        function ($scope, ReportSignatoryService, $uibModal, $timeout, $window, $q, $cs) {

            $scope.categories = [];
            $scope.reports = [];
            $scope.labels = [];

            var reportSignatoryId = angular.element("#ReportSignatoryId").val() == '' ? 0 :  parseInt(angular.element("#ReportSignatoryId").val());
            $scope.isUpdate = reportSignatoryId > 0;
            $scope.signatoryId = 0;

            $scope.m = {
                ReportSignatoryId: reportSignatoryId,
                CategoryId: null,
                ReportId: null,
                Signatories: []
            }

            this.$onInit = function () {

                $q.all([
                    ReportSignatoryService.GetReportCategories({}, "CategoryId").then(d => { return d.data }), // Report Category
                    ReportSignatoryService.GetApproverLabels({}, "LabelId").then(d => { return d.data }), // Approval label
                ]).then(function (res) {

                    $scope.categories = res[0];
                    $scope.labels = res[1];

                    if ($scope.isUpdate) {
                        // Edit
                        getDetails();
                    }

                });
            }

            $scope.btnCancel = function () {
                if ($scope.f.$pristine) {
                    $window.location.href = document.ReportSignatory + "Index";
                } else {
                    $cs.cancelChanges(function () {
                        $window.location.href = document.ReportSignatory + "Index";
                    });
                }
            }

            //#region Row functions

            $scope.addRow = function () {
                $scope.signatoryId--;
                $scope.m.Signatories.push({
                    Id: $scope.signatoryId,
                    LabelId: null,
                    EmployeeId: null,
                    Employee: "",
                    Position: "",
                    IsLabelExist: false,
                    IsEmployeelExist: false,
                });

                $scope.f.$pristine = false;
            }

            $scope.deleteRow = function (indexSelection) {
                $scope.m.Signatories.splice(indexSelection, 1);
                validateEmployee();
                $scope.validateLabel();
            }

            //#endregion

            $scope.validateLabel = function () {

                for (var i = 0; i < $scope.m.Signatories.length; i++) {
                    var data = $scope.m.Signatories[i];

                    if (data.LabelId != null && data.LabelId != undefined)
                        validateDuplicateLabel(data);
                }
            }

            $scope.updateReports = function (id) {
                if ($scope.m.CategoryId != null || $scope.m.CategoryId != undefined) {
                    ReportSignatoryService.GetReportsByCategoryId({
                        id: $scope.m.CategoryId
                    }, "ReportId").then(function (data) {
                        $scope.reports = data.data;

                        if (id != null || id != undefined) {
                            $scope.m.ReportId = id;

                            $scope.f.$pristine = true;
                        }
                    }, function (error /*Error event should handle here*/) {
                        console.log("Error");
                    },
                        function (data /*Notify event should handle here*/) {
                        });
                }

            }

            //#region Search Employee

            $scope.searchEmployee = function (index) {
                var modalData = {
                    LookupType: 'EMP',
                    SearchText: ''
                }
                $uibModal.open({
                    animation: true,
                    ariaLabelledBy: 'modal-title',
                    ariaDescribedBy: 'modal-body',
                    templateUrl: `${document.baseUrlNoArea}Lookups/ChooseFromList/GetLookup?objType=${modalData.LookupType}`,
                    controller: 'ChooseFromListCtrl',
                    size: 'md',
                    keyboard: true,
                    backdrop: "static",
                    windowClass: 'modal_dialog',
                    modalOverflow: true,
                    resolve: {
                        modalData: function () {
                            return modalData;
                        },
                    }
                }).result.then(function (data) {

                    $scope.m.Signatories[index].Employee = data.FullName;
                    $scope.m.Signatories[index].EmployeeId = data.EmployeeId;
                    $scope.m.Signatories[index].Position = data.PositionName;

                    validateEmployee();

                    $scope.f.$pristine = false;

                }, function () {

                });
            };

            //#endregion

            $scope.btnSave = function () {
                $scope.invalidSignatories = $scope.m.Signatories.findIndex(r => r.IsLabelExist || r.IsEmployeelExist) != -1;
                $scope.f.$valid = true;
                $scope.formSubmitted = true;

                if (angular.element('.ng-invalid').length) {
                    angular.element('.ng-invalid')[1].focus();
                    $scope.f.$valid = false
                }
                else if ($scope.invalidSignatories || $scope.m.Signatories.length == 0) {
                    $scope.f.$valid = false
                }

                if ($scope.f.$valid) {
                    $cs.saveOrUpdateChanges(function () {
                        ReportSignatoryService.SaveReportSignatory({ model: $scope.m }).then(function (d) {
                            if (d.success) {
                                $cs.successMessage(d.message);
                                $window.location.href = document.ReportSignatory + "Index"; // back to list
                            }
                            else {
                                $cs.warningMessage(d.message);
                            }
                        })
                    }, $scope.m.ReportSignatoryId)
                }
            }

            //#region Private Functions

            function validateDuplicateLabel(data) {
                var exist = $scope.m.Signatories.findIndex(r => r.LabelId === data.LabelId && r.Id != data.Id);

                if (exist != -1) {
                    data.IsLabelExist = true;
                }
                else {
                    data.IsLabelExist = false;
                }

            }

            function validateEmployee() {

                for (var i = 0; i < $scope.m.Signatories.length; i++) {
                    var data = $scope.m.Signatories[i];
                    var exist = data.EmployeeId != null && data.EmployeeId != undefined ? $scope.m.Signatories.findIndex(r => r.EmployeeId === data.EmployeeId && r.Id != data.Id) : -1;

                    if (exist != -1) {
                        data.IsEmployeelExist = true;
                    }
                    else {
                        data.IsEmployeelExist = false;
                    }
                }

               
            }

            function getDetails() {
                ReportSignatoryService.GetDetailsById({
                    id: $scope.m.ReportSignatoryId
                }).then(function (data) {

                    $scope.m = data.data;
                    $scope.updateReports($scope.m.ReportId);

                }, function (error /*Error event should handle here*/) {
                    console.log("Error");
                }, function (data /*Notify event should handle here*/) {
                });
            }

            //#endregion
        }
    ]);