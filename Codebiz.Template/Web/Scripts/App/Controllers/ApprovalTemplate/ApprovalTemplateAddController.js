MetronicApp.controller('ApprovalTemplateAddController', ['$scope', '$q', 'NgTableParams', 'CommonService', 'ApprovalTemplateService','$uibModal','$timeout','$location','$window',
    function ($scope, $q, $ngTable, CommonService, ApprovalTemplateService, $uibModal, $timeout, $location, $window) {
        $scope.viewOnly = $location.search().view == undefined || $location.search().view == null ? false : parseInt($location.search().view) == 1;
        $scope.formSubmitted = false;
        $scope.m = {
            ApprovalTemplateId: $location.search().approvalTemplateId == undefined || $location.search().approvalTemplateId == null ? 0 : parseInt($location.search().approvalTemplateId),
            Name: "",
            Originators: [],
            Transactions: [],
            Stages: []
        }

        this.$onInit = function () {
            $scope.forUpdate = $scope.m.ApprovalTemplateId == 0 ? false : true;

            if ($scope.forUpdate) {
                loadApprovalTemplate();
            }
            else {
                getTransactionsData();
                $scope.OriginatorListParams = new $ngTable({}, { dataset: $scope.m.Originators });
            }
        }

        $scope.onchangedTransaction = function () {
            $scope.atForm.$dirty = true;
        };

        //#region Originators

        $scope.addOriginators = function () {
            var modalData = {
                LookupType: 'USER',
                Selections: $scope.m.Originators
            }

            showChooseFromListMultipleModal(
                modalData,
                `${document.baseUrlNoArea}Lookups/ChooseFromList/GetLookupMultiple?objType=${modalData.LookupType}`,
                "Originators"
            );
        };

        $scope.deleteOriginator = function (index) {
            $scope.m.Originators.splice(index, 1);
            $scope.OriginatorListParams.reload();
            $scope.atForm.$dirty = true;
        };

        //#endregion

        //#region Stage

        $scope.addStages = function () {
            var modalData = {
                LookupType: 'APS',
                Selections: $scope.m.Stages
            }

            showChooseFromListMultipleModal(
                modalData,
                `${document.baseUrlNoArea}Lookups/ChooseFromList/GetLookupMultiple?objType=${modalData.LookupType}`,
                "Stages"
            );
        };

        $scope.deleteStage = function (index) {
            $scope.m.Stages.splice(index, 1);
            $scope.atForm.$dirty = true;
        };

        $scope.viewStageDetail = function (approvalStageId) {
            $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: 'ApprovalStageDetailsModal.html',
                controller: 'ApprovalStageDetailsController',
                size: 'lg',
                keyboard: false,
                backdrop: "static",
                windowClass: 'modal_style',
                resolve: {
                    ApprovalStageId: function () {
                        return approvalStageId;
                    }
                }
            }).result.then(function () {

            });
        };

        //#endregion

        //#region Save/Cancel

        $scope.save = function () {
            $scope.formSubmitted = true;

            if ($scope.m.Transactions.filter(p => p.IsSelected).length <= 0) {
                $('.nav-tabs a[href="#transactionsTab"]').tab('show');
            }

            else if ($scope.m.Originators.length <= 0) {
                $('.nav-tabs a[href="#originatorsTab"]').tab('show');
            }

            else if ($scope.m.Stages.length <= 0) {
                $('.nav-tabs a[href="#stagesTab"]').tab('show');
            }

            else if ($scope.atForm.$valid) {
                ApprovalTemplateService.CheckApprovalTemplateIfHasConflict({
                    model: $scope.m
                }).then(function (data) {
                    if (data.result.length != 0 && data.result[0].Transactions.length != 0) {
                        dataHasConflict(data);
                    }
                    else {
                        CommonService.saveOrUpdateChanges(function () {
                            saveData();
                        }, $scope.m.ApprovalTemplateId);
                    }
                })
            }
        }

        $scope.cancel = function () {
            if ($scope.atForm.$dirty) {
                CommonService.cancelChanges(function () {
                    window.location.href = document.ApprovalTemplate + "Index";
                })
            }
            else {
                window.location.href = document.ApprovalTemplate + "Index";
            }
        }

        //#endregion

        function dataHasConflict(data) {
            $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: '_WarningMessageModal.html',
                controller: 'WarningMessageModalController',
                controllerAs: 'controller',
                size: 'lg',
                keyboard: false,
                backdrop: "static",
                windowClass: 'modal_style',
                resolve: {
                    BodyText: function () {
                        return "Approval Template has confilict with the ff setup(s):";
                    },
                    Data: function () {
                        return data.result
                    },
                    Subject: function () {
                        return "";
                    },
                    DataCount: function () {
                        return 0;
                    },

                }
            }).result.then(function (success) {
                if (success) {
                    saveData();
                }
            },
            function () {
            });
        }

        function saveData() {
            ApprovalTemplateService.SaveApprovalTemplate({
                model: $scope.m
            }).then(function (data) {
                if (data.succeed) {
                    CommonService.successMessage(data.message);
                    $timeout(function () {
                        window.location.href = document.ApprovalTemplate + "Index";
                    }, 1000);
                }
                else {
                    CommonService.warningMessage(data.message);
                }
            })
        }

        function showChooseFromListMultipleModal(modalData, url, type) {
            $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: url,
                controller: 'ChooseFromListMultipleSelectCtrl',
                size: 'lg',
                keyboard: false,
                backdrop: "static",
                windowClass: 'modal_dialog',
                modalOverflow: true,
                resolve: {
                    modalData: function () {
                        return modalData;
                    }
                }
            }).result.then(function (data) {
                if (type == "Originators") {
                    $scope.m.Originators = data.selectedData;
                    $scope.OriginatorListParams = new $ngTable({}, { dataset: $scope.m.Originators });
                }
                else if (type == "Stages") {
                    $scope.m.Stages = data.selectedData;
                }

                $scope.atForm.$dirty = true;
            }, function (error /*Error event should handle here*/) {
                console.log("Error.");
            }, function (data /*Notify event should handle here*/) {
                console.log("Error");
            });
        }

        function loadApprovalTemplate() {
            $q.all([
                ApprovalTemplateService.GetApprovalTemplateDetails({ approvalTemplateId: $scope.m.ApprovalTemplateId }).then(d => { return d.data }),
                ApprovalTemplateService.GetAllTransactionForApproval({}).then(d => { return d.results })
            ]).then(function (d) {
                $scope.m = d[0];
                $scope.OriginatorListParams = new $ngTable({}, { dataset: $scope.m.Originators });

                var transactions = d[1];
                for (var i = 0; i < transactions.length; i++) {
                    var transaction = transactions[i];
                    var item = $scope.m.Transactions.find(p => p.TransactionTypeId == transaction.TransactionTypeId);
                    if (item == undefined && item == null) {
                        $scope.m.Transactions.push(transaction);
                    }
                }
            })
        }

        function getTransactionsData() {
            ApprovalTemplateService.GetAllTransactionForApproval({
            }).then(function (data) {
                $scope.m.Transactions = data.results;
            });
        }
    }]);
