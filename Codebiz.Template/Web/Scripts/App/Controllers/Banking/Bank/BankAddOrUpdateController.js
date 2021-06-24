MetronicApp.controller('BankAddOrUpdateController', ['$scope', '$q', 'NgTableParams', 'CommonService', '$timeout', '$location', '$window', 'BankService', '$uibModal', '$route', 'HouseBankAccountService',
    function ($scope, $q, NgTableParams, CommonService, $timeout, $location, $window, BankService, $uibModal, $route, HouseBankAccountService) {
        $scope.model = {
            BankId: 0,
            CountryCodeId: null,
            BankCode: "",
            BankName: "",
            BICSwiftCode: "",
            IsPostOffice: false,
            HouseBankAccountId: null
        };
        this.$onInit = function () {
            $scope.accountNo = "";
            CommonService.GetAllCountryCode({}, "CountryCodeId").then(function (data) {
                $scope.countryCodes = data.data;
            })
            $scope.forUpdate = $route.current.params.id == null ? false : true;
            if ($scope.forUpdate) {
                BankService.GetBankDetails({ id: $route.current.params.id }).then(function (data) {
                    //  $scope.GLAccountCode = data.DataResult.GLAccountCode;
                    $scope.model = data.DataResult;
                    $scope.accountNo = data.DataResult.AccountNo;
                    $scope.branch = data.DataResult.Branch;
                    $scope.nextCheckNo = data.DataResult.NextCheckNo;
                })
            }
        }

        $scope.save = function () {
            $scope.formSubmitted = true;
            $scope.swiftCodeInvalid = false;
            $scope.bankCodeInvalid = false;
            $scope.bankNameInvalid = false;
            duplicateValidatioThenSave();

        }
        $scope.backToList = function () {
            if (!$scope.bankForm.$pristine) {
                CommonService.cancelChanges(function () {
                    angular.element(document.getElementById("cancel").click());
                });
            }
            else {
                $location.path("View");
            }
        }
        $scope.cancel = function () {

            if (!$scope.bankForm.$pristine) {
                CommonService.cancelChanges(function () {
                    angular.element(document.getElementById("cancel").click());
                });
            }
            else {
                $location.path("View");
            }
        }
        $scope.openHouseBankAccountModal = function (fromEvent) {
            var modalData = {
                LookupType: 'HBA',
                Tittle: 'House Bank Accounts',
                SearchText: fromEvent ? $scope.accountNo : ''
            }
            $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: 'ChooseFromListPartial.html',
                controller: 'ChooseFromListCtrl',
                size: 'md',
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
                fillUpHouseBankAccountDetail(data);

            }, function () {

            });
        }
        $scope.getHouseBankAccount = function (accountNo, isFromSave, $event) {
            $scope.houseBankAccountNotExist = false;
            var evt = $event || window.event;
            var keyCode = evt.which || evt.keyCode;
            if (keyCode === 13 || keyCode === 9 || isFromSave) {
                HouseBankAccountService.GetHouseBankAccountShortCut({ accountNo: accountNo }).then(function (data) {

                    if (data.DataResult.length == 1) {
                        $scope.houseBankAccountNotExist = false;
                        fillUpHouseBankAccountDetail(data.DataResult[0]);
                    }
                    else if (data.DataResult >= 2) {
                        $scope.openHouseBankAccountModal(true);
                    }
                    else {
                        $scope.houseBankAccountNotExist = true;
                        $scope.branch = "";
                        $scope.nextCheckNo = "";
                        $scope.model.HouseBankAccountId = null;
                        if (isFromSave) {
                            CommonService.warningMessage("House Bank Account doesn't exist!")
                            angular.element("#hbAccountNo").focus();
                        }
                    }
                    if (isFromSave) {
                        save();
                    }
                })
            }

        };
        function duplicateValidatioThenSave() {
            if ($scope.model.BICSwiftCode) {
                BankService.CheckBICSwiftCode({ code: $scope.model.BICSwiftCode, id: $scope.model.BankId }).then(function (data) {
                    if (data.DataResult) {
                        $scope.swiftCodeInvalid = true;
                        CommonService.warningMessage("BIC/Swift already exist");
                        angular.element("#swiftCode").focus();
                    }
                });

            }
            if ($scope.accountNo) {
                $scope.getHouseBankAccount($scope.accountNo, true);

            }
            else {
                save();
            }


            if ((angular.element('.ng-invalid').length)) {
                angular.element('.ng-invalid')[1].focus();



                //BankService.CheckBankName({ name: $scope.model.BankName, id: $scope.model.BankId }).then(function (data) {
                //    if (data.DataResult) {
                //        $scope.bankNameInvalid = true;
                //        CommonService.warningMessage("Bank name already exist");
                //    }

                //});

            }

        }
        function fillUpHouseBankAccountDetail(data) {
            $scope.model.HouseBankAccountId = data.HouseBankAccountId;
            $scope.accountNo = data.HouseBankAccountNo;
            $scope.branch = data.Branch;
            $scope.nextCheckNo = data.NextCheckNo;
        }
        function save() {
            if ($scope.model.BankCode != "" && $scope.model.CountryCodeId != null && !$scope.houseBankAccountNotExist) {
                BankService.CheckBankCode({ code: $scope.model.BankCode, id: $scope.model.BankId, countryCodeId: $scope.model.CountryCodeId }).then(function (data) {
                    if (data.DataResult) {
                        $scope.bankCodeInvalid = true;
                        CommonService.warningMessage("Bank code already exist");
                    }
                    if ($scope.bankForm.$valid && !$scope.swiftCodeInvalid && !$scope.bankCodeInvalid) {
                        CommonService.saveOrUpdateChanges(function () {
                            BankService.AddOrUpdate({ model: $scope.model }).then(function (data) {
                                if (data.ajaxResult.Success) {
                                    CommonService.successMessage(data.ajaxResult.Message);
                                    $location.path("View");
                                } else {
                                    CommonService.warningMessage(data.ajaxResult.Message);
                                }

                            })
                        }, $scope.model.BankId)
                    }
                });
            }
        }

    }])