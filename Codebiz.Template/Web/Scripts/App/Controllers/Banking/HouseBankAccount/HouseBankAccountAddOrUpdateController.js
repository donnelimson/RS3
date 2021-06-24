MetronicApp.controller('HouseBankAccountAddOrUpdateController', ['$scope', '$q', 'NgTableParams', 'CommonService', '$timeout', '$location', '$window', 'HouseBankAccountService', '$uibModal', '$route', 'BankService',
    function ($scope, $q, NgTableParams, CommonService, $timeout, $location, $window, HouseBankAccountService, $uibModal, $route, BankService) {
        $scope.model = {
            HouseBankAccountId: 0,
            BankId: null,
            Branch: "",
            AccountNo: "",
            BankAccountName: "",
            NextCheckNo: "",
            GLAccountId: null,
            GLAccountInterimId: null,
            Block: "",
            StreetNo: "",
            Street: "",
            City: "",
            IBAN: "",
            County: "",
            State: "",
            Zipcode: "",
            ControlKey: "",
            ABARoutingNumber: "",
            FractionalNumber: "",
            UserNo3: "",
            UserNo4: "",
            PaperTypeId: null,
            MaximumLines: "",
            TemplateNameId: null,
            IsLockChecksPrinting: false,
        };
        this.$onInit = function () {
            $scope.forUpdate = $route.current.params.id == null ? false : true;
            CommonService.GetAllPaperTypes({}, "PaperTypeId").then(function (data) {
                $scope.paperTypes = data.data;
            });
            CommonService.GetAllCountries({}, "CountryId").then(function (data) {
                $scope.countries = data.data;
            });
            CommonService.GetAllTemplateNames({}, "TemplateNameId").then(function (data) {
                $scope.templateNames = data.data;
            });
            $timeout(function () {
                if ($scope.forUpdate) {
                    HouseBankAccountService.GetHouseBankAccountDetails({ id: $route.current.params.id }).then(function (data) {
                        $scope.model = data.DataResult;
                        $scope.country = data.DataResult.Country;
                        $scope.BICSwiftCode = data.DataResult.BICSwiftCode;
                        $scope.GLInterimAccountNo = data.DataResult.GLAccountInterimNo;
                        $scope.GLAccountNo = data.DataResult.GLAccountNo;
                        $scope.BankCode = data.DataResult.BankCode;
                    })
                }
            }, 300)

        }

        $scope.save = function () {

            if ($scope.GLAccountNo != null) {
                $scope.getGLAccount($scope.GLAccountNo, false, true);
            }
            if ($scope.GLInterimAccountNo != null) {
                $scope.getGLAccount($scope.GLInterimAccountNo, true, true);
            }
            $scope.getBank($scope.BankCode, true);
            $scope.formSubmitted = true;



        }
        $scope.backToList = function () {
            if (!$scope.hbaForm.$pristine) {
                CommonService.cancelChanges(function () {
                    angular.element(document.getElementById("cancel").click());
                });
            }
            else {
                $location.path("View");
            }
        }
        $scope.cancel = function () {

            if (!$scope.hbaForm.$pristine) {
                CommonService.cancelChanges(function () {
                    angular.element(document.getElementById("cancel").click());
                });
            }
            else {
                $location.path("View");
            }
        }
        $scope.openBankModal = function () {
            var modalData = {
                LookupType: 'BNK',
                Tittle: 'Banks'
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
                $scope.bankCodeNotExist = false;
                fillUpBankDetail(data, false)

            }, function () {

            });
        }
        $scope.openGLAccountModal = function (forInterim, fromEvent) {
            var modalData = {
                LookupType: 'ACT',
                SearchText: fromEvent ? forInterim ? $scope.GLInterimAccountNo : $scope.GLAccountNo : ''
            }
            $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: `${document.baseUrlNoArea}Lookups/ChooseFromList/GetLookup?objType=${modalData.LookupType}`,
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
                if (!forInterim) {
                    $scope.GLAccountNotExist = false;
                    $scope.model.GLAccountId = data.GLAccountID;
                    $scope.GLAccountNo = data.AcctCode;
                } else {
                    $scope.GLAccountInterimNotExist = false;
                    $scope.model.GLAccountInterimId = data.GLAccountID;
                    $scope.GLInterimAccountNo = data.AcctCode;
                }

            }, function () {

            });
        }

        $scope.getBank = function (bank, isFromSave, $event) {
            $scope.bankCodeExist = false;
            $scope.bankCodeNotExist = false;
            var evt = $event || window.event;
            var keyCode = evt.which || evt.keyCode;
            if (keyCode === 13 || keyCode === 9 || isFromSave) {
                BankService.GetBankShortCut({ code: bank }).then(function (data) {
                    if (data.DataResult == null) {
                        $scope.bankCodeNotExist = true;
                        $scope.country = "";
                        $scope.BICSwiftCode = "";
                        $scope.model.BankId = null;
                        if (isFromSave) {
                            CommonService.warningMessage("Bank Code doesn't exist!")
                            angular.element("#bankCode").focus();
                        }
                    }
                    else {
                        $scope.bankCodeNotExist = false;
                        fillUpBankDetail(data.DataResult);
                    }
                    if (isFromSave) {
                        if ($scope.model.BankId != null) {
                            checkIfAccountExist($scope.model.BankId, $scope.model.HouseBankAccountId);

                        }

                    }
                })
            }

        };
        $scope.getGLAccount = function (GLAccount, isInterim, isFromSave, $event) {
            if (isInterim) {
                $scope.GLAccountInterimNotExist = false;
            }
            if (!isInterim) {
                $scope.GLAccountNotExist = false;
            }

            var evt = $event || window.event;
            var keyCode = evt.which || evt.keyCode;
            if (keyCode === 13 || keyCode === 9 || isFromSave) {
                if (GLAccount != null && GLAccount != "") {
                    CommonService.GetGlAccountByAcctCode({ searcher: GLAccount }).then(function (data) {
                        var isValid = true;

                        if (data.DataResult.length == 1) {
                            defineInterim(isInterim, isValid);
                        }
                        else if (data.DataResult.length >= 2) {
                            $scope.openGLAccountModal(isInterim, true);

                        }
                        else {
                            defineInterim(isInterim, isValid = false);
                        }
                        //if (fromSaveProcess) {
                        //    save();
                        //}
                    })
                }
            }


        };
        function fillUpBankDetail(data) {
            $scope.model.BankId = data.BankId;
            $scope.country = data.BankCountry;
            $scope.BankCode = data.BankCode;
            $scope.BICSwiftCode = data.BICSwiftCode;
        };
        function defineInterim(isInterim, isValid) {

            if (!isValid) {
                if (!isInterim) {
                    $scope.GLAccountNotExist = true;
                    angular.element("#glAccountNo").focus();
                }
                else {
                    $scope.GLAccountInterimNotExist = true;
                    angular.element("#glAccountIterimNo").focus();
                }

            }
            else {
                if (!isInterim) {
                    $scope.GLAccountNotExist = false;
                }
                else {
                    $scope.GLAccountInterimNotExist = false
                }

            }
        }
        function saveChanges() {

            if ($scope.hbaForm.$valid && !$scope.GLAccountNotExist && !$scope.GLAccountInterimNotExist && !$scope.bankCodeNotExist && !$scope.accountExist) {
                CommonService.saveOrUpdateChanges(function () {
                    HouseBankAccountService.AddOrUpdate({ model: $scope.model }).then(function (data) {
                        if (data.ajaxResult.Success) {
                            CommonService.successMessage(data.ajaxResult.Message);
                            $location.path("View");
                        } else {
                            CommonService.warningMessage(data.ajaxResult.Message);
                        }

                    })
                }, $scope.model.HouseBankAccountId)
            }
            else {

                if ((angular.element('.ng-invalid').length)) {
                    angular.element('.ng-invalid')[1].focus();
                }
                if ($scope.accountExist) {
                    angular.element("#accountNo").focus();

                }
            }
        };
        function checkIfAccountExist(bankId, id) {
            HouseBankAccountService.CheckAccountNoIfExist({ accountNo: $scope.model.AccountNo, bankId: bankId, id: id }).then(function (data) {
                if (data.DataResult) {
                    $scope.accountExist = true;
                    CommonService.warningMessage("Account No. already exist!")
                }
                else {
                    $scope.accountExist = false;
                }
                saveChanges();
            })

        }


    }])