MetronicApp.controller('DocumentNumberingModalAddOrUpdateController', ['$scope', 'CommonService', 'DocumentNumberings', '$uibModalInstance', 'ToUpdateDocumentNumbering', '$filter',
    function ($scope, CommonService, DocumentNumberings, $uibModalInstance, ToUpdateDocumentNumbering, $filter) {
        $scope.documentNumbering= {
            DocNumberingDetailId:0,
            Name:"",
            FirstNo: null,
            NextNo: null,
            LastNo: null,
            Prefix: "",
            Suffix: "",
            NoOfDigits: null,
            IsLocked: false,
            IsDefault: false,
            IsYearPrefix : false
        };

        this.$onInit = function () {
            if (ToUpdateDocumentNumbering != null) {
                //console.log(ToUpdateDocumentNumbering);
                $scope.forUpdate = true;
                $scope.documentNumbering.DocNumberingId = ToUpdateDocumentNumbering.DocNumberingId;
                $scope.documentNumbering.NoOfDigits = ToUpdateDocumentNumbering.NoOfDigits;
                $scope.documentNumbering.Name  = ToUpdateDocumentNumbering.Name;
                $scope.documentNumbering.FirstNo  = ToUpdateDocumentNumbering.FirstNo;
                $scope.documentNumbering.NextNo = ToUpdateDocumentNumbering.NextNo;
                $scope.documentNumbering.LastNo = ToUpdateDocumentNumbering.LastNo;
                $scope.documentNumbering.Prefix = ToUpdateDocumentNumbering.Prefix ? "" : ToUpdateDocumentNumbering.Prefix;
                $scope.documentNumbering.Suffix = ToUpdateDocumentNumbering.Suffix;
                $scope.documentNumbering.IsLocked = ToUpdateDocumentNumbering.IsLocked;
                $scope.documentNumbering.DocNumberingDetailId = ToUpdateDocumentNumbering.DocNumberingDetailId;
                $scope.documentNumbering.IsDefault = ToUpdateDocumentNumbering.IsDefault;
                $scope.documentNumbering.IsYearPrefix = ToUpdateDocumentNumbering.IsYearPrefix;
            }
            else {
                $scope.forUpdate = false;
            }
           
        }

        $scope.addDocumentNumbering = function () {
            $scope.formSubmitted = true;
            if (!$scope.hasDuplicateName && !$scope.invalidLastNo && !$scope.invalidNoOfDigits && !$scope.hasDuplicatePrefix && !$scope.hasDuplicateSuffix && !$scope.invalidFirstNo & $scope.modalForm.$valid) {
                $uibModalInstance.close($scope.documentNumbering);
            }
        }
        $scope.cancel = function () {
            if (!$scope.modalForm.$pristine) {
                CommonService.cancelChanges(function () {
                    $uibModalInstance.dismiss('cancel');
                })
            }
            else {
                $uibModalInstance.dismiss('cancel');
            }
        }
        //Validate name
        $scope.validateName = function (name) {
            if (DocumentNumberings.findIndex(r => name == null ? false : r.Name.toUpperCase() == name.toUpperCase() && r.DocNumberingDetailId != $scope.documentNumbering.DocNumberingDetailId) != -1) {
                $scope.hasDuplicateName = true;
            }
            else {
                $scope.hasDuplicateName = false;
            }
        }
        //Validate no of digits
        $scope.validateNoOfDigits = function (noOfDigits) {
            if (noOfDigits < 1 || noOfDigits > 15) {
                $scope.invalidNoOfDigits = true;
            }
            else {
                $scope.invalidNoOfDigits = false;
            }
        }
        //Validate Prefix
        $scope.validatePrefix = function (prefix) {
            if (DocumentNumberings.findIndex(r => r.Prefix != null)!=-1) {
                if (DocumentNumberings.findIndex(r => prefix == null ? false : (r.Prefix ==null ? " ":r.Prefix.toUpperCase()) == prefix.toUpperCase()) != -1) {
                    $scope.hasDuplicatePrefix = true;
                }
                else {
                    $scope.hasDuplicatePrefix = false;
                }
            }
            else {
                $scope.hasDuplicatePrefix = false;
            }
        }
        //Validate Suffix
        $scope.validateSuffix = function (suffix) {
            if (DocumentNumberings.findIndex(r => r.Suffix != null) != -1) {

                if (DocumentNumberings.findIndex(r => suffix == null ? false : (r.Suffix == null ? " " : r.Suffix.toUpperCase()) == suffix.toUpperCase()) != -1) {
                    $scope.hasDuplicateSuffix = true;
                }
                else {
                    $scope.hasDuplicateSuffix = false;
                }
            }
            else {
                $scope.hasDuplicateSuffix = false;
            }
        }
        //Validate first no, next no and last no
        $scope.validateFirstNoNextNoAndLastNo = function (firstNo, nextNo, lastNo) {
            if (lastNo != "") {
                if (lastNo < 1) {
                    $scope.invalidLastNo = true;

                }
                else if (parseInt(lastNo) < parseInt(firstNo) || parseInt(lastNo) < parseInt(nextNo)) {
                    $scope.invalidLastNo = true;

                }
                else {

                    $scope.invalidLastNo = false;

                }
             
            }
            else {
                $scope.invalidLastNo = false;
            }
    
    
        }
        $scope.validatefirstNo = function (firstNo) {
            if (parseInt(firstNo) < 1) {
                $scope.documentNumbering.NextNo = ""; 
                $scope.invalidFirstNo = true;
            }
            else {
                $scope.documentNumbering.NextNo = firstNo;
                $scope.invalidFirstNo = false;

            }
        }
        $scope.setPrefixData = function (boolValue) {
            $scope.documentNumbering.Prefix = "";
        }

        function validateAll() {
            var data = $scope.documentNumbering;
            // Duplicate name validation
            if (DocumentNumberings.findIndex(r => r.Name.toUpperCase() == data.Name.toUpperCase()) != -1) {
                $scope.hasDuplicateName = true;
            }
            else {
                $scope.hasDuplicateName = false;
            }
        }
    }])