(function () {
    'use strict';

    var app = angular.module('MetronicApp');
    app.controller('ApprovalResultController', ApprovalResultController);

    ApprovalResultController.$inject = ['$scope', 'ApprovalService', 'ApprovalId', 'NgTableParams', '$q', '$uibModalInstance', 'CommonService'];

    function ApprovalResultController($scope, ApprovalService, ApprovalId, NgTableParams, $q, $uibModalInstance, CommonService) {

        $scope.statuses = [];
        $scope.sortOrder = "asc";
        $scope.sortColumn = "Stage";


        this.$onInit = function () {
            getApprovalStatus();
        };

        //#region Private functions
        function getApprovalStatus() {
            ApprovalService.GetApprovalStatus({
            }).then(function (data) {
                lookUpToFilter(data.data, 1);
            }, function (error /*Error event should handle here*/) {
                console.log("Error");
            }, function (data /*Notify event should handle here*/) {
            });
        }
        //#endregion

        function lookUpToFilter(data, selection) {
            if (selection === 1) {
                angular.forEach(data, function (item) {
                    if (jQuery.inArray(item.Name, $scope.statuses.Name) === -1) {
                        $scope.statuses.push({
                            'id': item.Id,
                            'title': item.Name
                        });
                    }
                });
            }
        }

        $scope.init = function () {
            var initialSettings = {
                getData: function (params) {

                    for (var i in params.sorting()) {
                        $scope.sortColumn = i;
                        $scope.sortOrder = params.sorting()[i];
                    }
                    var d = $q.defer();
                    var filter = params.filter();

                    ApprovalService.SearchApprovalResult({
                        approvalId: ApprovalId,
                        Page: params.page(),
                        PageSize: params.count(),
                        SortDirection: $scope.sortOrder,
                        SortColumn: $scope.sortColumn,
                        Name: filter["Name"],
                        Position: filter["Position"],
                        Stage: filter["Stage"],
                        StatusId: filter["Status"]
                    }).then(function (data) {
                        params.total(data.totalRecordCount);
                        d.resolve(data.data);
                    });

                    return d.promise;
                },
                counts: []
            };
            $scope.tableParams = new NgTableParams(10, initialSettings);
        };

        $scope.init();

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    }
})();
