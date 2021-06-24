(function () {
    'use strict';

    var app = angular.module('MetronicApp');
    app.controller('SubStationController', SubStationController);
    app.controller('SubStationAddOrUpdateController', SubStationAddOrUpdateController);

    SubStationController.$inject = ['$scope', 'NgTableParams', 'SubStationService', '$location', '$window', '$q', 'CommonService', '$timeout'];
    SubStationAddOrUpdateController.$inject = ['$scope', 'NgTableParams', 'SubStationService', '$location', '$window', '$q', 'CommonService', '$timeout'];

    function SubStationController($scope, NgTableParams, SubStationService, $location, $window, $q, CommonService, $timeout) {

        //#region Init

        this.$onInit = function () {
            $scope.reset();
            getSubStation();
        };

        //#endregion

        //#region Private functions
        function getSubStation() {
            SubStationService.GetSubStationLookUp({
            },"SubStationId").then(function (data) {
                $scope.subStation = data.data;
            }, function (error /*Error event should handle here*/) {
                console.log("Error");
            }, function (data /*Notify event should handle here*/) {
            });
        }
        //#endregion

        //Other defaults
        $scope.createdOnDatePicker = {
            opened: false
        };
        $scope.openCreatedOnDatePicker = function () {
            $scope.createdOnDatePicker.opened = true;
        }
        $scope.gotoCreate = function () {
            window.location.href = document.SubStation + "Form";
        }
        $scope.addOrUpdate = function (id) {
            CommonService.showLoading();
            $timeout(function () {
                if (id === 0) {
                    $window.location.href = document.SubStation + "Form";
                }
                else {
                    $window.location.href = document.SubStation + "Edit/" + id;
                }
            }, 500);
        };
        //Search SubStation
        $scope.search = function () {
            var initialSettings = {
                getData: function (params) {
                    for (var i in params.sorting()) {
                        $scope.sortColumn = i;
                        $scope.sortOrder = params.sorting()[i];
                    }
                    var d = $q.defer();

                    SubStationService.SearchSubStation({
                        Page: params.page(),
                        PageSize: params.count(),
                        SortDirection: $scope.sortOrder,
                        SortColumn: $scope.sortColumn,
                        SubStationId: $scope.subStationId,
                        CreatedBy: $scope.createdBy,
                        CreatedOnFrom: getDateRangePickerValue(1, $scope.createdDate),
                        CreatedOnTo: getDateRangePickerValue(2, $scope.createdDate),
                    }).then(function (data) {
                        $scope.resultsLength = data.totalRecordCount;
                        params.total($scope.resultsLength);
                        d.resolve(data.data);
                    });
                    return d.promise;
                }
            };
            $scope.tableParams = new NgTableParams(10, initialSettings);
        }

         $scope.exportDataToExcelFile = function () {
            if ($scope.resultsLength > 0) {
                CommonService.showLoading();

                SubStationService.ExportDataToExcelFile({
                    SortDirection: $scope.sortOrder,
                    sortColumn: $scope.sortColumn,
                    SubStationId: $scope.subStationId,
                    CreatedBy: $scope.createdBy,
                    CreatedOnFrom: getDateRangePickerValue(1, $scope.createdDate),
                    CreatedOnTo: getDateRangePickerValue(2, $scope.createdDate),
                }).then(function (data) {
                    CommonService.hideLoading();
                    window.location.href = document.FileUpload + "DownloadExcelFile?fileName=" + data.data.FileName;

                }, function (error /*Error event should handle here*/) {
                    console.log("Error");
                }, function (data /*Notify event should handle here*/) {
                    console.log("Error");
                });
            }
        };

        $scope.delete = function (id,name) {
            CommonService.deleteChanges(function () {
                SubStationService.DeleteSubStationById({ id: id })
                    .then(function (data) {
                        CommonService.successMessage(data.message);
                        $scope.search();
                    }), function (error /*Error event should handle here*/) {
                        console.log(error);
                        CommonService.errorMessage("Unexpected error occured.");
                    }, function (data /*Notify event should handle here*/) {
                    };
            },name);
        };

        $scope.deactivate = function (id, name) {
            CommonService.deActivate(function () {
                SubStationService.DeactivateReactivate({ id: id })
                    .then(function (data) {
                        CommonService.successMessage(data.message);
                        $scope.search();
                    }), function (error /*Error event should handle here*/) {
                        console.log(error);
                        CommonService.errorMessage("Unexpected error occured.");
                    }, function (data /*Notify event should handle here*/) {
                    };
            },name);
        };

        $scope.reactivate = function (id,name) {
            CommonService.reActivate(function () {
                SubStationService.DeactivateReactivate({ id: id })
                    .then(function (data) {
                        CommonService.successMessage(data.message);
                        $scope.search();
                    }), function (error /*Error event should handle here*/) {
                        console.log(error);
                        CommonService.errorMessage("Unexpected error occured.");
                    }, function (data /*Notify event should handle here*/) {
                    };
            },name);
        };

        //Reset Table and Clear Fields
        $scope.reset = function () {
            $scope.sortColumn = "CreatedDate";
            $scope.sortOrder = "desc";
            $scope.subStationId = null;
            $scope.createdBy = "";
            $scope.createdDate = null;
            $scope.search();
        };

        $scope.searchWhenEnter = function ($event) {
            var keyCode = $event.which || $event.keyCode;
            if (keyCode === 13) {
                $scope.search();
            }
        };
    }
    function SubStationAddOrUpdateController($scope, NgTableParams, SubStationService, $location, $window, $q, CommonService, $timeout) {

        this.$onInit = function () {
            initEmptyRow();
            getSubStationDetailsForUpdate();
        };

        $scope.subStation = {
            SubStationId: null,
            Description: "",
            Feeders: []
        };

        var subStationCopy = angular.copy($scope.subStation);

        $scope.formSubmitted = false;
        $scope.isUpdate = false;
        $scope.alreadyExists = false;
        $scope.currentIndex = -1;
        var currentName = "";

        $scope.addOrEditSubStation = function (feederRow, index) {
            addOrEdit(feederRow, index);
        };
        $scope.checkIfNameIsEmpty = function (feederRow, index) {
            if (feederRow.Name == "") {
                $scope.subStation.Feeders.splice(index, 1);
            }
        };
        $scope.addOrEditSubStationOnEnter = function ($event, feederRow, index) {
            var keyCode = $event.which || $event.keyCode;
            if (keyCode === 13) {
                addOrEdit(feederRow, index);
            }
        };

        $scope.checkIfNameExists = function () {
            if (checkIfNameExists($scope.emptyRow.Name)) {
                $scope.alreadyExists = true;
            }
            else {
                $scope.alreadyExists = false;
            }
        };
        $scope.editRow = function (index) {
            if (!$scope.alreadyExists) {
                $scope.subStation.Feeders[index].IsEditing = true;
                var elem = document.getElementById("feedersInput" + index);
                if (elem) elem.focus();
            }
        };
        $scope.deleteRow = function (index) {
            $scope.subStation.Feeders.splice(index, 1);
            var emptyElem = angular.element("#emptyRow");
            if (emptyElem) emptyElem.attr("readonly", false);
            $scope.currentIndex = -1;
            $scope.alreadyExists = false;
        };

        $scope.focusElement = function (index) {
            var angularElem = angular.element("#feedersInput" + index);
            angularElem.focus();
        };

        $scope.save = function () {
            $scope.formSubmitted = true;
            if (!$scope.isUpdate) {
                if ($scope.form.$valid && $scope.subStation.Feeders.length && !$scope.alreadyExists) {
                    CommonService.saveChanges(function () {
                        CommonService.showLoading();
                        SubStationService.AddSubStation({
                            model: $scope.subStation
                        }).then(function (data) {
                            if (data.success) {
                                CommonService.hideLoading();
                                CommonService.successMessage(data.message);
                                $timeout(function () {
                                    $window.location.href = document.SubStation;
                                }, 600);
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
                    });
                }
                else {
                    if (angular.element('.ng-invalid').length) {
                        angular.element('.ng-invalid')[1].focus();
                    }
                    else if (angular.element('input.has-error').length) {
                        angular.element('input.has-error')[0].focus();
                    }
                }
            }
            else {
                if ($scope.form.$valid && $scope.subStation.Feeders.length && !$scope.alreadyExists) {
                    CommonService.updateChanges(function () {
                        SubStationService.UpdateSubStation({
                            model: $scope.subStation
                        }).then(function (data) {
                            if (data.success) {
                                CommonService.hideLoading();
                                CommonService.successMessage(data.message);
                                $timeout(function () {
                                    $window.location.href = document.SubStation;
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
                    });
                }
                else {
                    if (angular.element('.ng-invalid').length) {
                        angular.element('.ng-invalid')[1].focus();
                    }
                    else if (angular.element('input.has-error').length) {
                        angular.element('input.has-error')[0].focus();
                    }
                }
            }
        };

        $scope.cancel = function () {
            if (!angular.equals($scope.subStation, subStationCopy)) {
                CommonService.cancelChanges(function () {
                    $window.location.href = document.SubStation;
                });
            }
            else {
                $window.location.href = document.SubStation;
            }
        };

        $scope.backToList = function () {
            $window.location.href = document.SubStation;
        };

        function initEmptyRow() {
            $scope.emptyRow = {
                FeederId: 0,
                Name: "",
                IsEditing: false
            };
        }
        function getSubStationDetailsForUpdate() {
            var id = angular.element("#SubStationId").val() || 0;

            if (id === 0) {
                $scope.isUpdate = false;
            }
            else {
                $scope.isUpdate = true;

                SubStationService.GetSubStationDetailsForUpdateById({
                    id: id
                }).then(function (data) {
                    $scope.subStation = data.data;
                    $scope.subStation.SubStationId = id;
                    subStationCopy = angular.copy($scope.subStation);

                }), function (error /*Error event should handle here*/) {
                    CommonService.hideLoading();
                    console.log(error);
                    CommonService.errorMessage("Unexpected error occured.");
                }, function (data /*Notify event should handle here*/) {
                };
            }
        }
        function addOrEdit(feederRow, index) {
            if (index !== -1) {
                if (feederRow.Name.length) {
                    var emptyElem = angular.element("#emptyRow");

                    if (checkIfNameExists(feederRow.Name, index)) {
                        $scope.alreadyExists = true;
                        currentName = feederRow.Name;
                        $scope.currentIndex = index;
                        if (emptyElem) emptyElem.attr("readonly", true);
                    }
                    else {
                        $scope.alreadyExists = false;
                        $scope.currentIndex = -1;
                        feederRow.IsEditing = false;

                        if (emptyElem) emptyElem.attr("readonly", false);
                    }
                }
            }
            else {
                if ($scope.emptyRow.Name.length) {
                    if (checkIfNameExists(feederRow.Name, index)) {
                        $scope.alreadyExists = true;
                        currentName = feederRow.Name;
                    }
                    else {
                        $scope.subStation.Feeders.push($scope.emptyRow);
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
            for (var i = 0; i < $scope.subStation.Feeders.length; i++) {
                if ($scope.subStation.Feeders[i].Name === name && index !== i) {
                    return true;
                }
            }
        }
        function setFormFieldsDirty() {
            angular.forEach($scope.form.$error, function (field) {
                angular.forEach(field, function (errorField) {
                    errorField.$setDirty();
                });
            });
        }
    }
})();