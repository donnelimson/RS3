angular.module('MetronicApp')

.controller('ApprovalStageDetailsController',
        ['$scope', 'ApprovalStageService', 'NgTableParams', '$uibModalInstance', '$window', 'CommonService', '$timeout', '$q', 'ApprovalStageId',
function ($scope, ApprovalStageService, NgTableParams, $uibModalInstance, $window, CommonService, $timeout, $q, ApprovalStageId) {

    //#region Variable Defaults
    
    //Filter defaults
    $scope.sortOrder = "asc";
    $scope.sortColumn = "Name";

    $scope.approvalStageDetails = {};
    
    //#endregion

    //#region Init

    this.$onInit = function () {
        getApprovalStageLabels();
        getApprovalStageDetails();
      
    };

    //#region Scope functions

    $scope.close = function () {
        $uibModalInstance.close();
    };

    //#endregion

    //#region Private functions
    function getApprovalStageLabels() {
        ApprovalStageService.GetApprovalStageLabels().then(function (data) {
            $scope.labels = data.data;

        });
    }
    function getApprovalStageDetails() {
        ApprovalStageService.GetApprovalStageDetailsById({
            id: ApprovalStageId
        }).then(function (data) {
      
            $scope.approvalStageDetails = data.data;
            getApprovers();
            $scope.LabelId = data.data.LabelId;

        }, function (error /*Error event should handle here*/) {
            console.log("Error");
        }, function (data /*Notify event should handle here*/) {
        });
    }

    function getApprovers() {
        var initialSettings = {
            counts:[],
            getData: function (params) {
                for (var i in params.sorting()) {
                    $scope.sortColumn = i;
                    $scope.sortOrder = params.sorting()[i];
                }
                var d = $q.defer();

                ApprovalStageService.GetApproversByApprovalStageId({
                    page: params.page(),
                    pageSize: params.count(),
                    sortOrder: $scope.sortOrder,
                    sortColumn: $scope.sortColumn,
                    approvalStageId: $scope.approvalStageDetails.ApprovalStageId
                }).then(function (data) {
                    params.total(data.totalRecordCount);
                    d.resolve(data.data);
                });
                return d.promise;
            }
        };

        $scope.tableParams = new NgTableParams({count : 5}, initialSettings);
    }

    //#endregion
}]);