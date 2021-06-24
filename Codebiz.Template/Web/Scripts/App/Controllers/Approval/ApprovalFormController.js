MetronicApp.controller('ApprovalFormController',
    ['$scope', 'ApprovalService', '$uibModal', 'CommonService', '$window', '$timeout', 'CommonService', 'MemberAccountsService',
        function ($scope, ApprovalService, $uibModal, commonService, $window, $timeout, CommonService, MemberAccountsService) {

            //#region Default Variables

            $scope.formSubmitted = false;
            $scope.decisions = [];

            $scope.ApprovalId = document.getElementById("ApprovalId").value;
            $scope.TransactionTypeId = document.getElementById("TransactionTypeId").value;
            $scope.ApprovalApproverId = document.getElementById("ApprovalApproverId").value;
            $scope.Decision = null;
            $scope.Remarks = "";

            //#endregion

            //#region Initialization

            this.$onInit = function () {
                getApprovalInformationDetails();
                getDecisions();
            };

            //#endregion

            $scope.backToList = function () {
                $window.location.href = document.Approval;
            }

            $scope.cancel = function () {
                if (!$scope.approvalForm.$dirty) {
                    $timeout(function () {
                        $window.location.href = document.Approval;
                    }, 100);
                }
                else {
                    commonService.cancelChanges(function () {
                        $timeout(function () {
                            $window.location.href = document.Approval;
                        }, 100);
                    });
                }
            };

            $scope.gotoDetails = function (data) {
                ApprovalService.GoToDetails(data);
            };

            $scope.approvalResult = function (ApprovalId) {
                $uibModal.open({
                    animation: true,
                    ariaLabelledBy: 'modal-title',
                    ariaDescribedBy: 'modal-body',
                    templateUrl: '_ApprovalResultModal.html',
                    controller: 'ApprovalResultController',
                    size: 'xlg',
                    keyboard: false,
                    backdrop: "static",
                    windowClass: 'modal_style',
                    modalOverflow: true,
                    resolve: {
                        ApprovalId: function () {
                            return ApprovalId;
                        },
                    }
                }).result.then(function () {
                    init();
                }, function () {
                });
            };

            $scope.process = function () {
                $scope.formSubmitted = true;
                if ($scope.approvalForm.$valid) {
                    CommonService.confimInfo(function () {
                        ApprovalService.Process({
                            model: {
                                ApprovalId: $scope.ApprovalId,
                                ApprovalApproverId: $scope.ApprovalApproverId,
                                TransactionTypeId: $scope.TransactionTypeId,
                                ApprovalStatusId: $scope.Decision.Id,
                                Remarks: $scope.Remarks,
                            }
                        }).then(function (data) {
                            if (data.success) {
                                commonService.successMessage(data.message);
                                $timeout(function () {
                                    $window.location.href = document.Approval;
                                }, 800);
                            } else {
                                commonService.warningMessage(data.message);
                            }
                        }), function (error /*Error event should handle here*/) {
                            console.log("Error");
                            commonService.errorMessage("Unexpected error occured.");
                        }, function (data /*Notify event should handle here*/) {
                        }
                    }, $scope.Decision.Description + " transaction?", "Transaction will be " + $scope.Decision.Description.toLowerCase() + ".", "Process");
                }
                else if (angular.element('.ng-invalid').length) {
                    $timeout(function () {
                        angular.element(document.getElementById('approveAccount')).trigger('click');
                    });
                    angular.element('.ng-invalid')[1].focus();
                }
            }

            //#region Private functions

            function getApprovalInformationDetails() {
                ApprovalService.GetApprovalInformation({
                    approvalId: $scope.ApprovalId
                }).then(function (data) {
                    if (data.success) {
                        $scope.approvalInformation = data.data;
                        if (data.data.AccountId != null)
                            getAccountDetails(data.data.AccountId)
                    }
                    else {
                        commonService.warningMessage(data.message);
                    }
                }, function (error /*Error event should handle here*/) {
                    console.log("Error");
                }, function (data /*Notify event should handle here*/) {
                });
            }

            function getDecisions() {
                ApprovalService.GetApprovalStatus({
                }).then(function (data) {
                    $scope.decisions = data.data;
                },
                    function (error /*Error event should handle here*/) {
                        console.log("Error");
                    },
                    function (data /*Notify event should handle here*/) {
                    });
            }

            function getAccountDetails(accountId) {
                MemberAccountsService.GetAccountDetails({
                    accountId: accountId
                }).then(function (data) {
                    $scope.accountDetails = data.data;
                    $scope.accountDetails.ForAccountDetails = true;
                }, function (error /*Error event should handle here*/) {
                    console.log("Error");
                }, function (data /*Notify event should handle here*/) {
                });
            }

            //#endregion

        }])