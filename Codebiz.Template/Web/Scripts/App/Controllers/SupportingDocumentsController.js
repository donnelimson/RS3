angular.module('MetronicApp').controller('SupportingDocumentController',
    ['$scope', 'CommonService', '$location', '$timeout',
        function ($scope, CommonService, $location, $timeout) {

            //#region Variable Defaults

            $scope.fromApplicant = false;
            $scope.isAttachmentsIsValid = false;
            $scope.hasRequiredDocuments = false;
            $scope.withdocumentType = true;
            $scope.documentTypes = [];
            $scope.queue = [];
            $scope.isUpdate = $location.absUrl().includes('Edit');

            //#endregion

            //#region Init

            this.$onInit = function () {

            };

            //#endregion

            //#region Scope functions

            $scope.removeTagIfExisting = function (file) {
                $scope.formTouched = true;
                var documentCount = 0;
                if (file.documentTypeId != null) {
                    for (var i = 0; i < $scope.queue.length; i++) {
                        if ($scope.queue[i].documentTypeId == file.documentTypeId) {
                            documentCount++;

                            if (documentCount > 1)
                                break;
                        }
                    }
                }

                if (documentCount > 1) {
                    CommonService.warningMessage("Duplicate document type!");
                    file.documentTypeId = null;
                }

                $scope.validateType();
                $scope.checkIfHasInvalidFile();
            };

            $scope.$watch("queue.length", function (newVal, oldVal) {
                var newOldDiff = newVal - oldVal;
                if (newOldDiff === 1) {
                    $scope.formTouched = true;
                    var lastIndex = $scope.queue.length - 1;
                    var nameCount = $scope.queue.filter(a => a.name === $scope.queue[lastIndex].name).length;

                    if (nameCount > 1) {
                        CommonService.warningMessage("File already exists!");
                        $scope.queue.splice(lastIndex, 1);
                    }

                    validateForm();
                }
                else if (newOldDiff > 1) {

                    if (oldVal > 0) {
                        $scope.formTouched = true;
                        var originalLength = $scope.queue.length;
                        var uniqueIndex = [];
                        for (var i = 0; i <= $scope.queue.length - 1; i++) {
                            var name = $scope.queue[i].name;
                            var duplicateDocs = $scope.queue.map(function (x) {
                                return x.name
                            }).indexOf(name);
                            uniqueIndex.push(duplicateDocs)
                        }
                        Array.prototype.multiIndexOf = function (el) {
                            var idxs = [];
                            for (var i = this.length - 1; i >= 0; i--) {
                                if (this[i] === el) {
                                    idxs.unshift(i);
                                }
                            }
                            return idxs;
                        };
                        for (var i = 0; i <= uniqueIndex.length - 1; i++) {
                            var sameDoc = uniqueIndex.multiIndexOf(i);
                            if (sameDoc.length >= 2) {
                                $scope.queue.splice(sameDoc[1], 1);
                                uniqueIndex.splice(sameDoc[1], 1);
                            }
                        }
                        var newLength = $scope.queue.length;
                        if (newLength < originalLength) {
                            CommonService.warningMessage("File(s) already exists!");
                        }
                    }

                    validateForm();
                }
            });

            $scope.removeDocument = function (index, isCancel, file) {
                $scope.formTouched = true;
                $scope.queue.splice(index, 1);

                if (isCancel) {
                    file.$cancel();

                }
                else {
                    file.$destroy();
                }
                $scope.checkIfHasInvalidFile();
                $scope.validateType();

                validateForm();
            };

            $scope.validateType = function () {
                var documentTypes = $scope.documentTypes;
                var queue = $scope.queue;
                $scope.hasdocumentIsNotValid = false;
                for (var i = 0; i < queue.length; i++) {
                    queue[i].documentIsNotValid = (queue[i].documentTypeId == undefined || queue[i].documentTypeId == null) ?
                        true : false;

                    if (queue[i].documentIsNotValid) {
                        $scope.hasdocumentIsNotValid = true;
                    }
                }

                //Required
                var requiredFields = documentTypes.filter((document) => document.IsRequired);
                $scope.hasRequiredDocuments = requiredFields.length > 0;
                if ($scope.withDocumentType && documentTypes.length == 0) {
                    $scope.noSetupFound = true;
                }
                for (var i = 0; i < requiredFields.length; i++) {

                    var documentType = requiredFields[i];
                    documentType.IsAttached = documentType.IsRequired && queue.filter((queue) => queue.documentTypeId === documentType.DocumentId).length > 0;
                }

                //Either
                var groupDocuments = _.mapValues(_.groupBy(documentTypes.filter((document) => !document.IsRequired), 'Group'),
                    clist => clist.map(document => _.omit(document, 'Group')));

                for (const [key, value] of Object.entries(groupDocuments)) {
                    var isCompleteAttached = false;
                    for (var i = 0; i < value.length; i++) {
                        if (!isCompleteAttached)
                            isCompleteAttached = queue.filter((queue) => queue.documentTypeId === value[i].DocumentId).length > 0;
                    }

                    for (var i = 0; i < value.length; i++) {
                        var documentType = documentTypes.filter((data) => data.DocumentId === value[i].DocumentId);
                        if (documentType != undefined && documentType != null) {
                            documentType[0].IsAttached = isCompleteAttached;
                        }
                    }
                }

                var requiredDocCount = documentTypes.filter((data) => (data.IsRequired || (data.Group != '' && data.Group != null && data.Group != undefined))).length;
                var requiredDocAttachCount = documentTypes.filter((data) => (data.IsRequired || (data.Group != '' && data.Group != null && data.Group != undefined)) && data.IsAttached).length;
                $scope.isAttachmentsIsValid = requiredDocAttachCount == requiredDocCount;
            };

            $scope.checkIfHasInvalidFile = function () {
                if ($scope.queue.length != 0) {
                    var cont = true;
                    angular.forEach($scope.queue, function (file) {
                        if (cont) {
                            if (file.error) {
                                $scope.hasInvalidFile = true;
                                cont = false;
                            }
                            else {
                                $scope.hasInvalidFile = false;
                            }
                        }
                        else {
                            return;
                        }
                    });
                }
                else {
                    $scope.hasInvalidFile = false;
                }
            }

            function validateForm() {
                if (angular.isFunction($scope.setFormDirty))
                    $scope.setFormDirty()
            }

            function init() {

                $timeout(function () {
                    $scope.validateType();
                }, 1000)
            }

            init();

            //#endregion
        }]);