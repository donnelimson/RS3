MetronicApp.controller('DocumentNumberingAddOrUpdateController', ['$scope', '$uibModal', 'NgTableParams', 'CommonService', '$timeout', '$location', '$window', 'DocumentNumberingService',
    function ($scope, $uibModal, NgTableParams, CommonService, $timeout, $location, $window, DocumentNumberingService) {
        $scope.documentNumberingTableParams = new NgTableParams({}, { dataset: [] });
        $scope.resultsLength = 0;
        $scope.excludeLastRow = false;
        $scope.canSetDefault = false;
        var collectionCountNotDeleted = 0;
        var id = 0;
        var previousIndex = null;
        $scope.viewOnly = $location.search().viewOnly == 'true' ? true : false;
        $scope.documentNumbering = {
            DocumentNumberingId: $location.search().documentNumberingId == null ? 0 : $location.search().documentNumberingId,
            TransactionTypeId: null,
            DocumentNumberingTransactions: []

        }

        this.$onInit = function () {
            $scope.isUpdate = $location.search().documentNumberingId == null ? false : true;
            
            if (!$scope.isUpdate) {
                $scope.documentNumbering.DocumentNumberingTransactions = [{ Name: "Manual", FirstNo: "", NextNo: null, LastNo: "", Prefix: "", Suffix: "", NoOfDigits: "", IsLocked: false, IsDefault: true, Id: id, IsEditing: false, Excluded: false }];
                id++;
                getAllTransactionTypeForDocumentNumbering(null);

            } else {
                DocumentNumberingService.GetDetailsById({ id: $scope.documentNumbering.DocumentNumberingId }).then(function (data) {
                   
                    getAllTransactionTypeForDocumentNumbering(data.result.TransactionTypeId);
                    $scope.documentNumbering.DocumentNumberingTransactions = data.result.DocumentNumberingTransactionTypes;

                })
            }

            //     console.log("wew");
        }
        //functions
        function getAllTransactionTypeForDocumentNumbering(id) {
            var fieldName = "TransactionTypeId";
            if (!$scope.isUpdate) {
                DocumentNumberingService.GetAllTransactionTypeForDocumentNumberingNotYetUsed({}, fieldName).then(function (data) {
                    $scope.transactionTypes = data.result
                });
            }
            else {
                DocumentNumberingService.GetAllTransactionTypeForDocumentNumbering({}, fieldName).then(function (data) {
                    $scope.transactionTypes = data.result
                    if (id != null) {
                        $scope.documentNumbering.TransactionTypeId = id;
                    }
                });
            }
        }
        function setItemEditingToTrue() {
            for (var i = 0; i <= $scope.documentNumbering.DocumentNumberingTransactions.length - 1; i++) {
                $scope.documentNumbering.DocumentNumberingTransactions[i].IsEditing = false;
            }
        }
        function checkIfItemNameIsDuplicated(item) {
            item.IsDuplicateName = false;
            if (item.Name == null) {
                item.Name = "";
            }
            var hasDuplicate = $scope.documentNumbering.DocumentNumberingTransactions.findIndex(r => r.Name.toUpperCase() == item.Name.toUpperCase() && (r.Id != item.Id || r.DocNumberingDetailId != item.DocNumberingDetailId) && !r.IsDeleted);
            if (hasDuplicate != -1 && item.Name!=null && item.Name!="") {
                item.IsDuplicateName = true;
                $scope.hasDuplicateName = true;
            }
            var hasAnyDuplicate = $scope.documentNumbering.DocumentNumberingTransactions.findIndex(r => r.IsDuplicateName);
            if (hasAnyDuplicate == -1) {
                $scope.hasDuplicateName = false;
            }
        }

        function validateNoOfDigits(item) {
            item.isInvalidNoOfDigits = false;
            if (item.NoOfDigits != "") {
                if ( (parseInt(item.NoOfDigits) > 15 || parseInt(item.NoOfDigits) < 1) && item.Name != "Manual") {
                    item.isInvalidNoOfDigits = true;
                    $scope.hasInvalidNoOfDigits = true;
                }
                else {
                    item.isInvalidNoOfDigits = false;
                }
            }
            var hasAnyInvalidNoOfDigits = $scope.documentNumbering.DocumentNumberingTransactions.findIndex(r => (parseInt(r.NoOfDigits) > 15 || parseInt(r.NoOfDigits) < 1));
            if (hasAnyInvalidNoOfDigits == -1) {
                $scope.hasInvalidNoOfDigits = false;
            }
     
        }
        function validatePrefix(item) {
            item.IsDuplicatePrefix = false;
            if (item.Prefix == null) {
                item.Prefix = "";
            }
            var indexofDuplicatePrefix = $scope.documentNumbering.DocumentNumberingTransactions.findIndex(r => (r.Prefix == null ? "" : r.Prefix.toUpperCase()) == item.Prefix.toUpperCase() && (r.Id != item.Id || r.DocNumberingDetailId != item.DocNumberingDetailId) && !r.IsDeleted);
            if (indexofDuplicatePrefix != -1 && item.Prefix!="" && item.Prefix!=null) {
                item.IsDuplicatePrefix = true;
                $scope.hasDuplicatePrefix = true;
            }
            else {
                item.IsDuplicatePrefix = false;
            }
            var hasAnyDuplicatePrefix = $scope.documentNumbering.DocumentNumberingTransactions.findIndex(r => r.IsDuplicatePrefix);
            if (hasAnyDuplicatePrefix == -1) {
                $scope.hasDuplicatePrefix = false;
            }
        }
        function validateSuffix(item) {

            item.IsDuplicateSuffix = false;
            if (item.Suffix == null) {
                item.Suffix = "";
            }
            var indexofDuplicateSuffix = $scope.documentNumbering.DocumentNumberingTransactions.findIndex(r => (r.Suffix == null ? "" : r.Suffix.toUpperCase()) == item.Suffix.toUpperCase() && (r.Id != item.I || r.DocNumberingDetailId != item.DocNumberingDetailId) && !r.IsDeleted);
            if (indexofDuplicateSuffix != -1 && item.Suffix != "" && item.Suffix != null) {
                item.IsDuplicateSuffix = true;
                $scope.IsDuplicateSuffix = true;
            }
            else {
                item.IsDuplicateSuffix = false;
            }
            var hasAnyDuplicateSuffix = $scope.documentNumbering.DocumentNumberingTransactions.findIndex(r => r.IsDuplicateSuffix);
            if (hasAnyDuplicateSuffix == -1) {
                $scope.IsDuplicateSuffix = false;
            }
           // console.log(item.IsDuplicateSuffix);
        }
        function validateLastNo(item) {
            item.isInvalidLastNo = false;
            if (item.LastNo != "" && item.LastNo != null) {
                if (parseInt(item.LastNo) < parseInt(item.FirstNo) || parseInt(item.LastNo)< parseInt(item.NextNo)) {
                    item.isInvalidLastNo = true;
                    $scope.hasInvalidLastNo = true;
                }
                else {
                    item.hasInvalidLastNo = false;
           
                }
            }
            var hasAnyInvalidLastNo = $scope.documentNumbering.DocumentNumberingTransactions.findIndex(r => (parseInt(item.LastNo) < parseInt(item.FirstNo) || parseInt(item.LastNo) < parseInt(item.NextNo)) && r.Id != 0);
            if (hasAnyInvalidLastNo == -1) {
                $scope.hasInvalidLastNo = false;
            }
        }
        function validationBeforeSave() {
            var collection = $scope.documentNumbering.DocumentNumberingTransactions;
            $scope.hasDuplicateName = false;
            $scope.hasInvalidLastNo = false;
            $scope.hasInvalidNoOfDigits = false;
            $scope.hasEmptyName = false;
            $scope.hasInvalidFirstNo = false;
            $scope.hasDuplicatePrefix = false;
            $scope.hasDuplicateSuffix = false;
            for (var i = 1; i <= collection.length - 1; i++) {
                collectionCountNotDeleted = 0;
                //first no if zero
                if (collection[i].FirstNo == 0 && collection[i].FirstNo!="") {
                    collection[i].IsInvalidFirstNo = true;
                    $scope.hasInvalidFirstNo = true;
                }
                else {
                    collection[i].IsInvalidFirstNo = false;
                }
                //for duplicate name
                if (collection[i].Name == null) {
                    collection[i].Name = "";                  
                }
                var indexOfDuplicateName = collection.findIndex(r => (r.Name == null ? "" : r.Name.toUpperCase()) == collection[i].Name.toUpperCase() && (r.Id != collection[i].Id || r.DocNumberingDetailId != collection[i].DocNumberingDetailId) && !r.IsDeleted);
      
                if (indexOfDuplicateName != -1 && collection[i].Name != null && collection[i].Name != "") {
                    collection[i].IsDuplicateName = true;
                    $scope.hasDuplicateName = true;
                }
                else {
                    collection[i].IsDuplicateName = false;
                }
                //invalid lastno
                var indexOfInvalidLastNo = collection.findIndex(r => (parseInt(collection[i].LastNo) < parseInt(collection[i].FirstNo) || parseInt(collection[i].LastNo) < parseInt(collection[i].NextNo)) && r.Id != 0);
                if ((indexOfInvalidLastNo != -1 || collection[i].LastNo==0)  && (collection[i].LastNo != "" && collection[i].LastNo != null)) {
                    collection[i].isInvalidLastNo = true;
                    $scope.hasInvalidLastNo = true;
                }
                else {
                    collection[i].isInvalidLastNo = false;
                }
                //invalid no of digits
                var indexOfInvalidNoOfDigits = collection.findIndex(r => (parseInt(collection[i].NoOfDigits) > 15 || parseInt(collection[i].NoOfDigits) < 1) && r.Id != 0);
                if (indexOfInvalidNoOfDigits != -1 && collection[i].NoOfDigits != "" && collection[i].NoOfDigits!=null) {
                    collection[i].isInvalidNoOfDigits = true;
                    $scope.hasInvalidNoOfDigits = true;
                }
                else {
                    collection[i].isInvalidNoOfDigits = false;
                }
                // prefix
                if (collection[i].Prefix == null) {
                    collection[i].Prefix = "";
                }
                var indexofDuplicatePrefix = collection.findIndex(r => (r.Prefix == null ? "" : r.Prefix.toUpperCase()) == collection[i].Prefix.toUpperCase() && (r.Id != collection[i].Id || r.DocNumberingDetailId != collection[i].DocNumberingDetailId) && !r.IsDeleted);

                if (indexofDuplicatePrefix != -1 && collection[i].Prefix != "" && collection[i].Prefix != null) {
                    collection[i].IsDuplicatePrefix = true;
                    $scope.hasDuplicatePrefix = true;
                }
                else {
                    collection[i].IsDuplicatePrefix = false;
                }
              //suffix 
                if (collection[i].Suffix == null) {
                    collection[i].Suffix = "";
                }
                var indexofDuplicateSuffix = collection.findIndex(r => (r.Suffix == null ? "" : r.Suffix.toUpperCase()) == collection[i].Suffix.toUpperCase() && (r.Id != collection[i].Id || r.DocNumberingDetailId != collection[i].DocNumberingDetailId) && !r.IsDeleted);

                if (indexofDuplicateSuffix != -1 && collection[i].Suffix != "" && collection[i].Suffix != null) {
                    collection[i].IsDuplicateSuffix = true;
                    $scope.hasDuplicateSuffix = true;
                }
                else {
                    collection[i].IsDuplicateSuffix = false;
                }
                for (var x = 0; x <= collection.length - 1; x++) {
                    if (!collection[x].IsDeleted) {
                        collectionCountNotDeleted++;
                    }
                }
                //  console.log();
                if (((collection[i].Name == "" || collection[i].Name == null) && (collection[i].FirstNo == "" || collection[i].FirstNo == null) && (collection[i].NoOfDigits == "" || collection[i].NoOfDigits == null)) && collectionCountNotDeleted > 2) {
                    if (i == collection.length - 1) {
                        collection[i].Excluded = true;
                    }
                  

                }
                else {

                    collection[collection.length - 1].Excluded = false;
                }

            }
            //console.log($scope.documentNumbering.DocumentNumberingTransactions);
            //empty

        }
        function checkIfCanSetDefault(item) {
            //var findEditingIndex = $scope.documentNumbering.DocumentNumberingTransactions.findIndex(r => r.IsEditing);
            //if (!$scope.documentNumbering.DocumentNumberingTransactions[findEditingIndex].IsDefault) {
            //    $scope.canSetDefault = true;
            //}
            //else {
            //    $scope.canSetDefault = false;
            //}

            if (item.IsDefault) {
                $scope.canSetDefault = false;
            } else {
                $scope.canSetDefault = true;
            }
        }
        function checkExclude(item, index) {
            var collection = item;
            //   console.log(collection);
            if (((collection.Name != "" && collection.FirstNo != "" && collection.NoOfDigits != "") && collection.length > 2)) {

                if (index == $scope.documentNumbering.DocumentNumberingTransactions.length - 1) {
                    collection.Excluded = true;
                }

            }
            else {
                collection.Excluded = false;
            }

        }
        function removeLastRow() {
            if ($scope.documentNumbering.DocumentNumberingTransactions[$scope.documentNumbering.DocumentNumberingTransactions.length - 1].Excluded) {
                $scope.documentNumbering.DocumentNumberingTransactions.splice($scope.documentNumbering.DocumentNumberingTransactions.length - 1, 1);
            }
      

        }
        function validateFirstNo(row) {
          
            if (row.FirstNo == 0 && row.FirstNo!="") {
                row.IsInvalidFirstNo = true;
                $scope.hasInvalidFirstNo = true;
            }
            else {
                row.IsInvalidFirstNo = false;
            }
            var hasAnyInvalidFirstNo = $scope.documentNumbering.DocumentNumberingTransactions.findIndex(r => r.FirstNo == 0);
            if (hasAnyInvalidFirstNo == -1) {
                $scope.hasInvalidFirstNo = false;
            }
        }
        //scopes

        $scope.clickRow = function (item, index) {
 //           console.log(item);
            $scope.selectedRow = index;
            setItemEditingToTrue();
            checkIfCanSetDefault(item);
            item.IsEditing = true;
            checkIfItemNameIsDuplicated($scope.documentNumbering.DocumentNumberingTransactions[previousIndex == null ? index : previousIndex]);
            checkIfItemNameIsDuplicated(item);
            validateLastNo($scope.documentNumbering.DocumentNumberingTransactions[previousIndex == null ? index : previousIndex]);
            validateNoOfDigits($scope.documentNumbering.DocumentNumberingTransactions[previousIndex == null ? index : previousIndex]);
            previousIndex = index;

        }
        $scope.addDocumentNumbering = function ($event, item, last, index) {
          //  console.log($scope.documentNumbering.DocumentNumberingTransactions);
            checkIfItemNameIsDuplicated(item);
            validatePrefix(item);
            validateNoOfDigits(item);
            validateLastNo(item);
            validateFirstNo(item);
            validateSuffix(item);
            var keyCode = $event.which || $event.keyCode;
            if (keyCode === 13) {
                checkExclude(item, index);
                $scope.documentSubmitted = true;
                if (((item.Name != "" && item.Name != null) && (item.FirstNo != "" && item.FirstNo != null) && (item.NoOfDigits != "" && item.NoOfDigits != null)) && (!$scope.hasDuplicateSuffix && !$scope.hasDuplicatePrefix && !$scope.hasInvalidFirstNo && !$scope.hasDuplicateName && !$scope.hasEmptyName && !$scope.hasInvalidNoOfDigits && !$scope.hasInvalidLastNo) && last) {
                    item.Excluded = false;
                    setItemEditingToTrue();
                    id++;
                    $scope.documentNumbering.DocumentNumberingTransactions.push({ DocNumberingDetailId: 0, Name: "", FirstNo: "", NextNo: null, LastNo: "", Prefix: "", Suffix: "", NoOfDigits: "", IsLocked: false, IsDefault: false, Id: id, IsEditing: true, Excluded: true,IsDuplicateName:false });
                    $scope.documentSubmitted = false;
                    $scope.formSubmitted = false;
                }


            }
        }
        $scope.delete = function (index) {
          //  console.log($scope.documentNumbering.DocumentNumberingTransactions[index]);
            $timeout(function () {
                $scope.documentNumbering.DocumentNumberingTransactions[index].IsDeleted = true;
                $scope.documentNumbering.DocumentNumberingTransactions[$scope.documentNumbering.DocumentNumberingTransactions.length - 1].IsEditing = true;
               // console.log($scope.documentNumbering.DocumentNumberingTransactions)
            })

      
        }
     
        $scope.setNextNo = function (row) {
            row.NextNo = row.FirstNo;
        }
        $scope.setDefault = function () {
            var findDefaultIndex = $scope.documentNumbering.DocumentNumberingTransactions.findIndex(r => r.IsDefault);
            if (findDefaultIndex != -1) {
                $scope.documentNumbering.DocumentNumberingTransactions[findDefaultIndex].IsDefault = false;

            }
            var findEditingIndex = $scope.documentNumbering.DocumentNumberingTransactions.findIndex(r => r.IsEditing);
            $scope.documentNumbering.DocumentNumberingTransactions[findEditingIndex].IsDefault = true;
        }

        $scope.addOrUpdate = function (row) {
            $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: 'documentnumberingmodal.html',
                controller: 'DocumentNumberingModalAddOrUpdateController',
                size: 'md',
                keyboard: false,
                backdrop: "static",
                windowClass: 'modal_style',
                modalOverflow: true,
                resolve: {
                    DocumentNumberings: function () {
                        return $scope.documentNumbering.DocumentNumberingTransactions;
                        
                    },
                    ToUpdateDocumentNumbering: function () {
                        return row;
                    }
                }
            }).result.then(function (data) {
                $scope.DocumentNumberingForm.$pristine = false;
                if (data.DocNumberingDetailId == 0) {
                    data.DocNumberingDetailId = id;
                    $scope.documentNumbering.DocumentNumberingTransactions.push(data);
                    id++;
                }
                else {
                    var index = $scope.documentNumbering.DocumentNumberingTransactions.findIndex(r => r.DocNumberingDetailId == data.DocNumberingDetailId);
                    $scope.documentNumbering.DocumentNumberingTransactions[index] = data;
                }

            });
        }

        $scope.save = function () {

            $scope.formSubmitted = true;;
            validationBeforeSave();


            $timeout(function () {

                if ((!$scope.hasDuplicateSuffix && !$scope.hasDuplicatePrefix && !$scope.hasInvalidFirstNo && !$scope.hasDuplicateName && !$scope.hasEmptyName && !$scope.hasInvalidNoOfDigits && !$scope.hasInvalidLastNo) && $scope.DocumentNumberingForm.$valid) {
                    if (!$scope.isUpdate) {
                        CommonService.saveChanges(function () {
       

                            removeLastRow();
           
                            DocumentNumberingService.AddOrUpdate({
                                documentNumbering: $scope.documentNumbering
                            }).then(function (data) {
                                if (data.ajaxResult.Success) {
                                    CommonService.successMessage(data.ajaxResult.Message);
                                    $timeout(function () {
                                        $window.location.href = document.DocumentNumbering + "Index";
                                    }, 1000)
                                }
                                else {
                                    CommonService.warningMessage(data.ajaxResult.Message);
                                }
                            })
                        })

                    }
                    else {
                        CommonService.updateChanges(function () {
                            removeLastRow();
                          //  console.log($scope.documentNumbering)
                            DocumentNumberingService.AddOrUpdate({
                                documentNumbering: $scope.documentNumbering
                            }).then(function (data) {
                                if (data.ajaxResult.Success) {
                                    CommonService.successMessage(data.ajaxResult.Message);
                                    $timeout(function () {
                                        $window.location.href = document.DocumentNumbering + "Index";
                                    }, 1000)
                                }
                                else {
                                    CommonService.warningMessage(data.ajaxResult.Message);
                                }
                            })
                        })
                    }
                }
            })

        }



        // misc
        $scope.cancelChanges = function () {
            if (!$scope.DocumentNumberingForm.$pristine) {
                CommonService.cancelChanges(function () {
                    $window.location.href = document.DocumentNumbering + "Index";
                })
            }
            else {
                $window.location.href = document.DocumentNumbering + "Index";
            }
        }
        $scope.backToList = function () {
            if (!$scope.DocumentNumberingForm.$pristine) {
                CommonService.discardChanges(function () {
                    $window.location.href = document.DocumentNumbering + "Index";
                })
            }
            else {
                $window.location.href = document.DocumentNumbering + "Index";
            }
        }


    }])