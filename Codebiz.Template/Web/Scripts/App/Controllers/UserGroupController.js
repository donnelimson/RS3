(function () {
    'use strict';

    var app = angular.module('MetronicApp');
    app.controller('UserGroupController', UserGroupController);

    UserGroupController.$inject = ['$scope', 'NgTableParams', 'UserGroupService', '$location', '$window', '$q', 'CommonService'];

    function UserGroupController($scope, NgTableParams, UserGroupService, $location, $window, $q, CommonService) {

        this.$onInit = function () {
            $scope.reset();
        },


            //#region Export to Excel
            $scope.exportDataToExcelFile = function () {
                if ($scope.resultsLength > 0) {
                    CommonService.showLoading();
                    UserGroupService.ExportDataToExcelFile({
                        SortDirection: $scope.sortOrder,
                        SortColumn: $scope.sortColumn,
                        Description: $scope.description,
                        Name: $scope.name,
                        CreatedBy: $scope.createdBy,
                        CreatedOnFrom: getDateRangePickerValue(1, $scope.createdDate),
                        CreatedOnTo: getDateRangePickerValue(2, $scope.createdDate)
                    }).then(function (data) {
                        CommonService.hideLoading();
                        window.location.href = document.FileUpload + "DownloadExcelFile?fileName=" + data.data.FileName;

                    }, function (error /*Error event should handle here*/) {
                        console.log("Error");
                    }, function (data /*Notify event should handle here*/) {
                        console.log("Error");
                    });
                }
            },
            //#endregion

            //Search UserGroup
            $scope.search = function () {
                var initialSettings = {
                    getData: function (params) {
                        for (var i in params.sorting()) {
                            $scope.sortColumn = i;
                            $scope.sortOrder = params.sorting()[i];
                        }
                        var d = $q.defer();
                        var filter = params.filter();

                        UserGroupService.UserGroupIndex({
                            page: params.page(),
                            pageSize: params.count(),
                            sortOrder: $scope.sortOrder,
                            sortColumn: $scope.sortColumn,
                            Description: $scope.description,
                            Name: $scope.name,
                            CreatedBy: $scope.createdBy,
                        }).then(function (data) {
                            $scope.resultsLength = data.totalRecordCount;
                            params.total($scope.resultsLength);
                            d.resolve(data.data);
                        });
                        return d.promise;
                    }
                };
                $scope.tableParams = new NgTableParams(10, initialSettings);
            };

        //Reset Table and Clear Fields
        $scope.reset = function () {
            $scope.description = "";
            $scope.name = "";
            $scope.sortOrder = "desc";
            $scope.sortColumn = "CreatedOn";
            $scope.createdBy = "";
            $scope.createdDate = null;
            $scope.search();
        };

        //Search when click enter
        $scope.searchWhenEnter = function ($event) {
            var keyCode = $event.which || $event.keyCode;
            if (keyCode === 13) {
                $scope.search();
            }
        };

        //Edit UserGroup
        $scope.goToEdit = function (id) {
            window.location.href = document.UserGroup + "Edit?id=" + id;
        };

        //Delete UserGroup
        $scope.delete = function (id, name) {
            CommonService.deleteChanges(function () {
                UserGroupService.Delete({ id: id }).then(function () {
                    CommonService.successMessage("Usergroup was successfully deleted.");
                    $scope.search();
                });
            },name);
        };

        //Deactivate UserGroup
        $scope.deactivate = function (id, name) {
            CommonService.deActivate(function () {
                UserGroupService.DeactivateActivate({ id: id }).then(function () {
                    CommonService.successMessage("Usergroup was successfully deactivated.");
                    $scope.search();
                });
            },name);
        };

        //Reactivate UserGroup
        $scope.reactivate = function (id, name) {
            CommonService.reActivate(function () {
                UserGroupService.DeactivateActivate({ id: id }).then(function () {
                    CommonService.successMessage("Usergroup was successfully reactivated.");
                    $scope.search();
                });
            },name);
        };

    }
})();
