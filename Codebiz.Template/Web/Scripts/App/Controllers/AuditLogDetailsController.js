MetronicApp.controller('AuditLogDetailsController', ['$scope', '$uibModalInstance', 'NgTableParams', 'AuditLogsServices', 'CommonService', 'LogId', '$timeout',
    function ($scope, $uibModalInstance, NgTableParams, AuditLogsServices, CommonService, LogId, $timeout) {

       
        function init() {

            AuditLogsServices.GetAuditLogDetails({
                id: LogId
            }).then(function (data) {
                $scope.auditDetails = data;
                //$scope.department.DepartmentId = data.DepartmentId;
                //$scope.department.Code = data.Code;
                //$scope.department.Name = data.Name;
            }
            );
        }

        $scope.auditDetails = {
            Id: null,
            ActivityId :null,
            Date : null,
            User :null,
            Thread : null,
            Level : null,
            Logger : null,
            Message : null,
            Exception : null,
            LogEventTitle : null,
        };

        init();

        //CANCEL BUTTON
        $scope.cancel = function (data) {
            $uibModalInstance.dismiss('cancel');
        };
    }]);