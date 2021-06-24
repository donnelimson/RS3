(function () {
    var app = angular.module('MetronicApp');
    app.requires.push.apply(app.requires, ['FIN.DATASERVICE']);
    app.controller('TaxGroupCtrl', ['$scope', 'NgTableParams', '$uibModal', '$window', '$timeout', '$q', 'TaxGroupService', 'CommonService',
            function ($scope, NgTableParams, $uibModal, $window, $timeout, $q, TaxGroupService, CommonService) {

                $scope.createdOnDatePicker = {
                    opened: false
                };

                this.$onInit = function () {
                    $scope.reset();
                };

                $scope.searchWhenEnter = function ($event) {
                    var keyCode = $event.which || $event.keyCode;
                    if (keyCode === 13) {
                        $scope.search();
                    }
                };

                //#region Export Data to Excel

                $scope.exportDataToExcelFile = function () {
                    if ($scope.resultsLength > 0) {
                        CommonService.showLoading();
                        TaxGroupService.ExportDataToExcelFile({
                            SortDirection: $scope.sortOrder,
                            SortColumn: $scope.sortColumn,
                            Code: $scope.code,
                            Name: $scope.name,
                            CreatedOnFrom: getDateRangePickerValue(1, $scope.createdDate),
                            CreatedOnTo: getDateRangePickerValue(2, $scope.createdDate),
                        }).then(function (data) {
                            CommonService.hideLoading();
                            window.location.href = document.FileUpload + "DownloadExcelFile?fileName=" + data.DataResult;

                        }, function (error /*Error event should handle here*/) {
                            console.log("Error");
                        }, function (data /*Notify event should handle here*/) {
                            console.log("Error");
                        });
                    }
                };

                //#endregion

                //#region Search 

                $scope.search = function () {
                    var initialSettings = {
                        getData: function (params) {
                            for (var i in params.sorting()) {
                                $scope.sortColumn = i;
                                $scope.sortOrder = params.sorting()[i];
                            }
                            var d = $q.defer();

                            TaxGroupService.GetList({
                                Page: params.page(),
                                PageSize: params.count(),
                                SortDirection: $scope.sortOrder,
                                SortColumn: $scope.sortColumn,
                                Code: $scope.code,
                                Name: $scope.name,
                                CreatedBy: $scope.actionBy,
                                CreatedOnFrom: getDateRangePickerValue(1, $scope.createdDate),
                                CreatedOnTo: getDateRangePickerValue(2, $scope.createdDate),
                            }).then(function (data) {
                                $scope.resultsLength = data.TotalRecordCount;
                                params.total($scope.resultsLength);
                                d.resolve(data.DataResult);
                            });
                            return d.promise;

                        }
                    };

                    $scope.tableParams = new NgTableParams(10, initialSettings);

                };

                //#endregion

                //#region Reset filter fields

                $scope.reset = function () {
                    $scope.code = "";
                    $scope.name = "";
                    $scope.actionBy = "";
                    $scope.createdDate = null;

                    $scope.sortColumn = "ActionOn";
                    $scope.sortOrder = "desc";

                    $scope.search();
                };

                //#endregion

                // new record
                $scope.onbtnNewClick = function (s, e) {
                    $uibModal.open({
                        animation: true,
                        ariaLabelledBy: 'modal-title',
                        ariaDescribedBy: 'modal-body',
                        templateUrl: 'TaxGroupForm.html',
                        controller: 'TaxGroupEditCtrl',
                        controllerAs: 'ctrl',
                        backdrop: "static",
                        size: 'md',
                        windowClass: 'modal_style',
                        keyboard: false,
                        resolve: {
                            modalData: function () {
                                return {
                                    'recordmode': 'A',
                                    'data': { 'Code': '' }
                                };d
                            },
                            ForDetails: function () {
                                return false;
                            },
                            ForEdit: function () {
                                return false;
                            }
                        } // data passed to the controller
                    }).result.then(function (d) {
                        if (d.StatusCode === '0')
                        $scope.search();
                    }, function () { });

                };

                var UpdateRecord = function (d) {

                    $scope.selectedRecord = d;

                    $uibModal.open({
                        animation: true,
                        ariaLabelledBy: 'modal-title',
                        ariaDescribedBy: 'modal-body',
                        templateUrl: 'TaxGroupForm.html',
                        controller: 'TaxGroupEditCtrl',
                        controllerAs: 'ctrl',
                        backdrop: "static",
                        size: 'md',
                        windowClass: 'modal_style',
                        keyboard: false,
                        resolve: { // data passed to the controller
                            modalData: function () {
                                return {
                                    'recordmode': 'E',
                                    'data': d
                                };
                            },
                            ForDetails: function () {
                                return false;
                            },
                            ForEdit: function () {
                                return true;
                            }
                        }
                    })
                        .result.then(function (d) {
                            if (d.StatusCode === '0')
                               $scope.search();
                        }, function () { });
                };

                var ViewRecord = function (d) {

                    $scope.selectedRecord = d;

                    $uibModal.open({
                        animation: true,
                        ariaLabelledBy: 'modal-title',
                        ariaDescribedBy: 'modal-body',
                        templateUrl: 'TaxGroupForm.html',
                        controller: 'TaxGroupEditCtrl',
                        controllerAs: 'ctrl',
                        backdrop: "static",
                        size: 'md',
                        windowClass: 'modal_style',
                        keyboard: false,
                        resolve: { // data passed to the controller
                            modalData: function () {
                                return {
                                    'recordmode': 'E',
                                    'data': d
                                };
                            },
                            ForDetails: function () {
                                return true;
                            },
                            ForEdit: function () {
                                return false;
                            }
                        }
                    })
                        .result.then(function (d) {
                            if (d.StatusCode === '0')
                                search();
                            CommonService.successMessage("Tax Code was successfully Updated.");
                        }, function () { });
                };

                $scope.onEditClick = function (s, e) {
                    s.Active = 'Y';
                
                    UpdateRecord(s);
                };

                $scope.onViewClick = function (s, e) {
                    s.Active = 'Y';
                    ViewRecord(s);
                };

                //#region Delete

                $scope.onDeleteClick = function (item) {
                    CommonService.deleteChanges(function () {
                        TaxGroupService.DeleteTaxGroup({
                            id: item.TaxGroupID,
                            code: item.VatCode
                        }).then(function (data) {
                            if (data.DataResult.Success) {
                                CommonService.successMessage(data.DataResult.Message);
                                $scope.search();
                            }
                            else {
                                CommonService.warningMessage(data.DataResult.Message);
                            }
                        }), function (error /*Error event should handle here*/) {
                            console.log(error);
                            CommonService.errorMessage("Unexpected error occured.");
                        }, function (data /*Notify event should handle here*/) {
                        };
                    }, item.VatName);
                }

                //#endregion

                //#region Reactivate / Deactivate

                $scope.toggleStatus = function (item) {
                    CommonService.reactivateDeactivate(function () {
                        TaxGroupService.ToggleStatus({ id: item.TaxGroupID, code: item.VatCode, status: item.IsActive })
                            .then(function (data) {
                                if (data.DataResult.Success) {
                                    CommonService.successMessage(data.DataResult.Message);
                                    $scope.search();
                                }
                                else {
                                    CommonService.warningMessage(data.DataResult.Message);
                                }

                            }), function (error /*Error event should handle here*/) {
                                console.log(error);
                                CommonService.errorMessage("Unexpected error occured.");
                            }, function (data /*Notify event should handle here*/) {
                            };
                    }, item.VatName, item.IsActive);
                }

                //#endregion
            }])
        .controller('TaxGroupEditCtrl', ['$scope', '$uibModal', '$filter', '$uibModalInstance', 'TaxGroupService', 'modalData', 'CommonService', 'ForDetails', 'ForEdit',
            function ($scope, $uibModal, $filter, $uibModalInstance, $tgs, modalData, CommonService, ForDetails, ForEdit) {

                $scope.isSubmit = false;
                $scope.forDetails = ForDetails;
                $scope.invalidTaxAccount = false;

                $scope.saveOrUpdate = ForEdit ? 'Update' : 'Save';

                $scope.tg = modalData.data;

                $scope.tg.EffecDate = $filter('date')($scope.tg.EffecDate, 'MM/dd/yyyy');
                $scope.recordMode = modalData.recordmode;

                $scope.onTabEnterSearchTaxAccount = function ($event) {
                    var keyCode = $event.which || $event.keyCode;
                    if (keyCode === 13 || keyCode === 9) {
                        glAccountValidateSearchText($scope.tg.Account);
                    }
                }  

                $scope.onbtnGLAcctSearchClick = function (searchText) {
                    $uibModal.open({
                        templateUrl: 'ChooseFromListPartial.html',
                        controller: 'ChooseFromListCtrl',
                        controllerAs: 'ctrl',
                        windowClass: 'modal_dialog',
                        backdrop: 'static',
                        keyboard: false,
                        resolve: {
                            modalData: function () {
                                return {
                                    'LookupType': 'ACT',
                                    'Tittle': 'Choose From List (G/L Accounts)',
                                    "SearchText": searchText
                                };
                            }
                        } // data passed to the controller
                    }).result.then(function (d) {
                        $scope.tg.Account = d.AcctCode;
                    }, function () { });
                };

                $scope.onbtnSaveClick = function (s, e) {
                    $scope.isSubmit = true;
                    if ($scope.f.$valid) {
                        CommonService.saveOrUpdateChanges(function () {
                            if (ForEdit) {
                                $tgs.SaveOrUpdate($scope.tg).then(function (d) {
                                    if (d.DataResult.Success) {
                                        CommonService.successMessage(d.DataResult.Message);
                                        $uibModalInstance.close(d);
                                    }
                                    else {
                                        CommonService.warningMessage(d.DataResult.Message);
                                    }
                                }, function (e) { });
                            }
                            else {
                                $tgs.Add($scope.tg).then(function (d) {
                                    if (d.DataResult.Success) {
                                        CommonService.successMessage(d.DataResult.Message);
                                        $uibModalInstance.close(d);
                                    }
                                    else {
                                        CommonService.warningMessage(d.DataResult.Message);
                                    }
                                }, function (e) { });
                            }
                        }, $scope.tg.TaxGroupID ? 1 : 0)
                    }
                };

                $scope.onbtnCancelClick = function (s, e) {
                    if ($scope.f.$dirty) {
                        CommonService.cancelChanges(function () {
                            $uibModalInstance.dismiss('cancel');
                        });
                    }
                    else {
                        $uibModalInstance.dismiss('cancel');
                    }
                };

                function glAccountValidateSearchText(searchText) {
                    $scope.invalidTaxAccount = false;
                    if (searchText != undefined && searchText != "") {
                        CommonService.GetGlAccountByAcctCode({
                            searcher: searchText
                        }).then(function (res) {
                            if (res.DataResult.length == 1 && res.DataResult[0].AcctCode == searchText) {
                                $scope.tg.Account = res.DataResult[0].AcctCode;
                            }
                            else {
                                $scope.invalidTaxAccount = true;
                                $scope.onbtnGLAcctSearchClick(searchText);
                            }
                        });
                    }
                }
            }]);
}());