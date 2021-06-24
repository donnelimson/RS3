angular.module('MetronicApp')
.controller('SupportingDocumentsAddOrUpdateController',
    ['$scope', 'SupportingDocumentsService', '$window', 'CommonService', '$timeout',
function ($scope, SupportingDocumentsService, $window, CommonService, $timeout) {

    //#region Variable Defaults

    var self = this;

    var supportingDocumentId = angular.element("#SupportingDocumentId").val() || 0;
    var transactionTypesId = null;

    $scope.supportingDocument = {
        SupportingDocumentId: null,
        TransactionTypeId: null,
        TransactionSubTypeId: null,
        TransactionTypeSubType: "",
        Documents: []
    };

    var supportingDocumentCopy = angular.copy($scope.supportingDocument);

    $scope.transactionTypes = [];
    $scope.transactionSubTypes = [];
    $scope.isUpdate = false;
    $scope.alreadyExists = false;
    $scope.currentIndex = -1;
    var currentName = "";

    //#endregion

    //#region Init

    this.$onInit = function () {
        getSupportingDocumentDetailsForUpdate();
        initEmptyRow();
    };

    //#endregion

    //#region Scope functions

    $scope.addOrEditSupportingDocument = function (documentRow, index) {
        addOrEdit(documentRow, index);
    };
    $scope.checkIfNameIsEmpty = function (documentRow, index) {
        if (documentRow.DocumentName === "") {
            $scope.supportingDocument.Documents.splice(index, 1);
        }
    };
    $scope.addOrEditSupportingDocumentOnEnter = function ($event, documentRow, index) {
        var keyCode = $event.which || $event.keyCode;
        if (keyCode === 13) {
            addOrEdit(documentRow, index);
        }
    };

    $scope.checkIfNameExists = function () {
        if (checkIfNameExists($scope.emptyRow.DocumentName)) {
            $scope.alreadyExists = true;
        }
        else {
            $scope.alreadyExists = false;
        }
    };

    $scope.editRow = function (index) {
        if (!$scope.alreadyExists) {
            $scope.supportingDocument.Documents[index].IsEditing = true;
            var elem = document.getElementById("documentInput" + index);
            if (elem) elem.focus();
        }
    };

    $scope.deleteRow = function (index) {
        $scope.supportingDocument.Documents.splice(index, 1);
        var emptyElem = angular.element("#emptyRow");
        if (emptyElem) emptyElem.attr("readonly", false);
        $scope.currentIndex = -1;
        $scope.alreadyExists = false;
    };

    $scope.focusElement = function (index) {
        var angularElem = angular.element("#documentInput" + index);
        angularElem.focus();
    };

    $scope.save = function () {
        $scope.formSubmitted = true;

        if (self.supportingDocumentsForm.$valid && $scope.supportingDocument.Documents.length && !$scope.alreadyExists && !$scope.isWhiteSpace) {
            $scope.supportingDocument.TransactionTypeSubType = getTransactionTypeSubTypeConcatIds();
            CommonService.saveOrUpdateChanges(function () {
                CommonService.showLoading();
                if (!$scope.isUpdate) {
                    SupportingDocumentsService.AddSupportingDocument({
                        model: $scope.supportingDocument
                    }).then(function (data) {
                        if (data.success) {
                            CommonService.hideLoading();
                            CommonService.successMessage(data.message);
                            $timeout(function () {
                                $window.location.href = document.SupportingDocuments;
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
                }
                else {
                    SupportingDocumentsService.UpdateSupportingDocument({
                        model: $scope.supportingDocument
                    }).then(function (data) {
                        if (data.success) {
                            CommonService.hideLoading();
                            CommonService.successMessage(data.message);
                            $timeout(function () {
                                $window.location.href = document.SupportingDocuments;
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
                }

            }, supportingDocumentId);
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

    $scope.cancel = function () {
        if (!angular.equals($scope.supportingDocument, supportingDocumentCopy)) {
            CommonService.cancelChanges(function () {
                $window.location.href = document.SupportingDocuments;
            });
        }
        else {
            $window.location.href = document.SupportingDocuments;
        }
    };

    $scope.backToList = function () {
        $window.location.href = document.SupportingDocuments;
    };

    $scope.transactionSubTypeLookUp = function () {
        self.supportingDocumentsForm.TransactionSubTypeId.$dirty = false;
        $scope.supportingDocument.TransactionSubTypeId = null;

        getTransactionSubTypes();
    };

    //#endregion

    //#region Private functions

    function getRequestTransactionTypes() {
        CommonService.GetAllTransactionTypeForSupportingDocument({
        }).then(function (data) {
            //if ($scope.isUpdate) {
            //    $scope.transactionTypes = data.data;
            //    $timeout(function () {
            //        $scope.supportingDocument.TransactionTypeId = transactionTypesId;
            //    }, 800);
            //}
            //else {
            //    $scope.transactionTypes = data.data.filter(a => !a.IsUsedInSupportingDocuments);
            //}

            $scope.transactionTypes = data.data;
            $scope.supportingDocument.TransactionTypeId = transactionTypesId;
        }, function (error /*Error event should handle here*/) {
            console.log("Error");
        }, function (data /*Notify event should handle here*/) {
        });
    }

    function getTransactionSubTypes() {
        var transactionTypeId = $scope.supportingDocument.TransactionTypeId;
        var transactionSubTypeId = $scope.supportingDocument.TransactionSubTypeId;

        if (!angular.isUndefined(transactionTypeId) && transactionTypeId !== 0) {
            CommonService.GetRequestTransactionSubTypesByTransactionTypeId({
                id: transactionTypeId
            }).then(function (data) {
                //$scope.transactionSubTypes = !$scope.isUpdate ? data.data.filter(a => !a.IsUsedInSupportingDocuments) : data.data;
                $scope.transactionSubTypes = data.data.filter(a => a.IsUsedInSupportingDocuments);
                $scope.supportingDocument.TransactionSubTypeId = transactionSubTypeId;

            }, function (error /*Error event should handle here*/) {
                console.log("Error");
            }, function (data /*Notify event should handle here*/) {
            });
        }
    }

    function getSupportingDocumentDetailsForUpdate() {
        var id = angular.element("#SupportingDocumentId").val() || 0;

        if (id === 0) {
            $scope.isUpdate = false;
            getRequestTransactionTypes();
        }
        else {
            $scope.isUpdate = true;

            SupportingDocumentsService.GetSupportingDocumentDetailsForUpdateById({
                id: id
            }).then(function (data) {
                $scope.supportingDocument = data.data;
                transactionTypesId = data.data.TransactionTypeId;
                getRequestTransactionTypes();
                getTransactionSubTypes();
                $scope.supportingDocument.SupportingDocumentId = id;
                supportingDocumentCopy = angular.copy($scope.supportingDocument);

            }), function (error /*Error event should handle here*/) {
                CommonService.hideLoading();
                console.log(error);
                CommonService.errorMessage("Unexpected error occured.");
            }, function (data /*Notify event should handle here*/) {
            };
        }
    }

    function initEmptyRow() {
        $scope.emptyRow = {
            DocumentId: 0,
            DocumentName: "",
            IsRequired: false,
            IsEditing: false,
            Group: null
        };
    }

    function addOrEdit(documentRow, index) {
        if (index !== -1) {
            if (documentRow.DocumentName.length) {
                var emptyElem = angular.element("#emptyRow");

                if (checkIfNameExists(documentRow.DocumentName, index)) {
                    $scope.alreadyExists = true;
                    currentName = documentRow.DocumentName;
                    $scope.currentIndex = index;
                    if (emptyElem) emptyElem.attr("readonly", true);
                }
                else {
                    $scope.alreadyExists = false;
                    $scope.currentIndex = -1;
                    documentRow.IsEditing = false;

                    if (emptyElem) emptyElem.attr("readonly", false);
                }
            }
        }
        else {
            if ($scope.emptyRow.DocumentName.length) {
                if (checkIfNameExists(documentRow.DocumentName, index)) {
                    $scope.alreadyExists = true;
                    currentName = documentRow.DocumentName;
                }
                else {
                    $scope.supportingDocument.Documents.push($scope.emptyRow);
                    initEmptyRow();
                    $scope.alreadyExists = false;
                }
            }
            else {
                $scope.emptyRow.IsEditing = false;
            }
        }
    }

    function checkIfNameExists(name, index) {
        for (var i = 0; i < $scope.supportingDocument.Documents.length; i++) {
            if ($scope.supportingDocument.Documents[i].DocumentName === name && index !== i) {
                return true;
            }
        }
    }

    function setFormFieldsDirty() {
        angular.forEach(self.supportingDocumentsForm.$error, function (field) {
            angular.forEach(field, function (errorField) {
                errorField.$setDirty();
            });
        });
    }

    function getTransactionTypeSubTypeConcatIds() {
        var concatIds = "";

        if ($scope.supportingDocument.TransactionTypeId > 0) {
            concatIds += $scope.supportingDocument.TransactionTypeId.toString();

            if ($scope.supportingDocument.TransactionSubTypeId > 0) {
                concatIds += $scope.supportingDocument.TransactionSubTypeId.toString();
            }
            else {
                concatIds += "0";
            }
        }

        return concatIds;
    }

    //#endregion

}])

.directive('focusOn', function ($timeout) {
    return {
        restrict: 'A',
        link: function ($scope, $element, $attr) {
            $scope.$watch($attr.focusOn, function (_focusVal) {
                $timeout(function () {
                    _focusVal ? $element[0].focus() :
                        $element[0].blur();
                });
            });
        }
    };
})

.directive('scrollToBottom', function ($timeout, $window) {
    return {
        scope: {
            scrollToBottom: "="
        },
        restrict: 'A',
        link: function (scope, element, attr) {
            scope.$watchCollection('scrollToBottom', function (newVal) {
                if (newVal) {
                    $timeout(function () {
                        element[0].scrollTop = element[0].scrollHeight;
                    }, 0);
                }

            });
        }
    };
});