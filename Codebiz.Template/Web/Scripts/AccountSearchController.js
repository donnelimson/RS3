angular.module('MetronicApp').controller('AccountSearchController',
    ['$scope', 'CommonService', '$location', 'MemberAccountsService', '$uibModal','ShareDataService',
        function ($scope, CommonService, $location, MemberAccountsService,$uibModal,ShareDataService) {

            $scope.form = null;
            $scope.selectedAccountNo = null;
            $scope.accountIsRequired = true;
            $scope.showAccount = true;
            $scope.messages = [];
            $scope.accountType = "";

            function postSearchFunctions() {
                if (ShareDataService.RequiredFilter == TRANSACTION_TYPE.Burial_Assistance.Value) {
                    $scope.getAccountsToChange();
                }
                else if (ShareDataService.RequiredFilter == TRANSACTION_TYPE.Change_of_Name.Value && $scope.messages.length == 0) {
                    $scope.GetAccountDetailsShow($scope.accountDetails);
                }
                else if (ShareDataService.RequiredFilter == TRANSACTION_TYPE.Job_Order_Request.Value || ShareDataService.RequiredFilter == TRANSACTION_TYPE.Job_Order_Complaint.Value) {
                    $scope.revalidateNatureType();
                }
                else if (ShareDataService.RequiredFilter == TRANSACTION_TYPE.Net_Metering.Value) {
                    $scope.SetAccountData();
                }
                else if (ShareDataService.RequiredFilter == TRANSACTION_TYPE.Billing_Adjustment.Value) {
                    $scope.resetValues();
                }
            }
      
            $scope.resetValidations = function () {
                if ($scope.selectedAccountNo == "" || $scope.selectedAccountNo == null) {
                    $scope.accountDetails = [];
                    $scope.messages = [];
                }   
            }

            $scope.resetAccount = function () {
                $scope.selectedAccountNo = "";
                $scope.accountDetails = null;
                $scope.messages = [];
                $scope.accountIsRequired = true;
            }
           
            $scope.searchAndValidateAccount = function (fromEvent) {
                if (angular.isFunction($scope.setFormDirty))
                    $scope.setFormDirty();

                MemberAccountsService.GetAccountInformationByTransaction({
                    accountNo: $scope.selectedAccountNo,
                    requiredFilter: ShareDataService.RequiredFilter,
                    searchTransactionTypeId: ShareDataService.TransactionTypeId,
                    isForUpdate: $scope.forUpdate
                }).then(function (d) {
                    $scope.validations = [];
                    $scope.messages = [];
                    if (d.Data != null) {
                        if (d.Data.IsValid) {
                            if (d.Data.Messages.length > 0) {
                                CommonService.messageInfo(function (confirmAction) {
                                    if (confirmAction) {
                                        $scope.accountDetails = d.Data;
                                        postSearchFunctions();
                                    }
                                    else {
                                        $scope.messages = d.Data.Messages;
                                        $scope.accountDetails = [];
                                    }

                                    $scope.$apply();

                                }, "Record has conflict, do you still want to process?", d.Data.Messages.toString().replace(",", "<br>"), "YES", "NO", true);
                            }
                            else {
                                $scope.accountDetails = d.Data;
                                postSearchFunctions();
                            }
                        }
                        else {
                            $scope.messages = d.Data.Messages;
                            $scope.accountDetails = [];
                        }
                    }
                    else {
                        $scope.searchAccount(fromEvent);
                        $scope.messages.push(VALIDATIONS.NonExisting.Msg);
                    }
                });
            }
            $scope.searchAccount = function (fromEvent) {
                var modalData = {
                    LookupType: 'CSAACT',
                    SearchText: fromEvent ? $scope.selectedAccountNo : '',
                    AccountType: $scope.accountType
                }
                $uibModal.open({
                    animation: true,
                    ariaLabelledBy: 'modal-title',
                    ariaDescribedBy: 'modal-body',
                    templateUrl: `${document.baseUrlNoArea}Lookups/ChooseFromList/GetLookup?objType=${modalData.LookupType}`,
                    controller: 'ChooseFromListCtrl',
                    size: 'lg',
                    keyboard: false,
                    backdrop: "static",
                    windowClass: 'modal_dialog',
                    modalOverflow: true,
                    resolve: {
                        modalData: function () {
                            return modalData;
                        },
                    }
                }).result.then(function (data) {
                    $scope.selectedAccountNo = data.AccountNo;
                    $scope.searchAndValidateAccount();
                }, function () {

                });
            };

            $scope.onTabEnterSearch = function ($event, code = "") {
                if (code == null)
                    code = "";
                $scope.selectedAccountNo = code; 
                var keyCode = $event.which || $event.keyCode;
                if (keyCode === 13 || keyCode === 9) {
                     $scope.searchAndValidateAccount(true);
                }
            }  
        }]);