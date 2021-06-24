MetronicApp.controller('PurposeAddOrUpdateController', ['$scope', '$q', 'NgTableParams', 'CommonService', '$uibModal', '$timeout', '$location', '$window', 'PurposeService',
    function ($scope, $q, NgTableParams, CommonService, $uibModal, $timeout, $location, $window, PurposeService) {
        $scope.m = {
            Id:0,
            TransactionTypeId: null,
            Purposes: [{Purpose:"",Id:0,CanDelete:true, IsActive:true}]
        };
        this.$onInit = function () {
            if ($location.search().Id != null) {
                $scope.forUpdate = true;
                GetTransactionTypes(true);
                $timeout(function () {
                    PurposeService.GetDetailsById({ id: $location.search().Id }).then(function (d) {
                        $scope.m = d.result;
                    });
                }, 600);
           
            }
            else {
                GetTransactionTypes(false);
            }
        }
   
        $scope.addPurpose = function () {
            $scope.m.Purposes.push({ Purpose: "", Id: 0, CanDelete:true, IsActive:true });
            $scope.f.$pristine = false;
        }
        $scope.deleteRow = function (index) {
            $scope.m.Purposes.splice(index, 1);
            $scope.f.$pristine = false;
        }
        $scope.save = function () {
            $scope.formSubmitted = true;
            checkIfHasInvalid();
            if ($scope.f.$valid && !$scope.hasInvalid) {
                CommonService.saveOrUpdateChanges(function () {
                    PurposeService.AddOrUpdate({ model: $scope.m }).then(function (d) {
                        if (d.success) {
                            CommonService.successMessage(d.message);
                            $timeout(function () {
                                window.location.href = document.Purpose;
                            }, 1000)
                        }
                        else {
                            CommonService.warningMessage(d.message);
                        }
                    })
                }, $scope.m.Id)
         
            }
        }
        $scope.cancel = function () {
            if (!$scope.f.$pristine) {
                CommonService.cancelChanges(function () {
                    window.location.href = document.Purpose;
                })
            }
            else {
                window.location.href = document.Purpose;
            }
        }
        $scope.checkIfPurposeExisting = function () {
            for (var i = 0; i <= $scope.m.Purposes.length - 1; i++) {
                var existingIndex = $scope.m.Purposes.findIndex(r => r.Purpose == $scope.m.Purposes[i].Purpose && r.$$hashKey != $scope.m.Purposes[i].$$hashKey);
                $scope.m.Purposes[i].Invalid = false;
                if (existingIndex != -1) {
                    $scope.m.Purposes[existingIndex].Invalid = true;
                    $scope.m.Purposes[i].Invalid = true;
                }
            }
           
        }
        function GetTransactionTypes(forUpdate) {
            if (!forUpdate) {
                PurposeService.GetAllTransactionTypeForPurposeUnused({
                }).then(function (data) {
                    $scope.transactionTypes = data.result;
                });
            }
            else {
                PurposeService.GetTransactionTypesForPurose({
                }).then(function (data) {
                    $scope.transactionTypes = data.result;
                });
            }          
        }
        function checkIfHasInvalid() {
            $scope.checkIfPurposeExisting();
            $scope.hasInvalid = $scope.m.Purposes.findIndex(r => r.Invalid)!=-1;
        }

    }])