(function () {
    var app = angular.module('MetronicApp');
    app.requires.push.apply(app.requires, ['FIN.DATASERVICE']);
    app.controller('PostingPeriodCtrl', ['$scope', 'NgTableParams', '$uibModal', '$window', '$timeout', '$q', 'PostingPeriodService', 'CommonService',
            function ($scope, NgTableParams, $uibModal, $window, $timeout, $q, $ppService, $cs) {

                var loadTable = function () {
                    $ppService
                        .GetFinancialPeriodDetailList()
                        .then(function (res) {
                            $scope.tableParams = new NgTableParams({}, { dataset: res.DataResult });
                        });
                };

                loadTable();

                // new record
                $scope.onbtnNewClick = function (s, e) {
                    $uibModal.open({
                        animation: true,
                        ariaLabelledBy: 'modal-title',
                        ariaDescribedBy: 'modal-body',
                        templateUrl: 'PostingPeriod.html',
                        controller: 'PostingPeriodEditCtrl',
                        size: 'lg',
                        keyboard: false,
                        backdrop: "static",
                        windowClass: 'modal_dialog',
                        modalOverflow: true,
                        resolve: {
                            modalData: function () {
                                return {
                                    'recordmode': 'A',
                                    'data': { 'PeriodCode': '' }
                                };
                            }
                        }
                    }).result.then(function (d) {
                        $cs.successMessage('Posting Period Added Successfully');
                        loadTable();
                    }, function () { });
                };
            }])
       .controller('PostingPeriodEditCtrl', ['$scope', 'NgTableParams', '$uibModalInstance', '$window', '$timeout', '$q', '$filter', 'PostingPeriodService', 'CommonService',
            function ($scope, NgTableParams, $uibModalInstance, $window, $timeout, $q, $filter, $ppService, $cs) {

                let fy = $filter('date')(new Date(), 'yyyy');

                $scope.formSubmitted = false;

                $scope.m = {
                    PeriodCategoryCode: fy,
                    PeriodCategoryName: `FY${fy}`,
                    PeriodNum: 12,
                    SubType: "M",

                    FromRefDate: $filter('date')(new Date(`01/01/${fy}`), 'MM/dd/yyyy'),
                    ToRefDate: $filter('date')(new Date(`12/31/${fy}`), 'MM/dd/yyyy'),

                    FromDueDate: $filter('date')(new Date(`01/01/${fy}`), 'MM/dd/yyyy'),
                    ToDueDate: $filter('date')(new Date(`12/31/${fy}`), 'MM/dd/yyyy'),

                    FromTaxDate: $filter('date')(new Date(`01/01/${fy}`), 'MM/dd/yyyy'),
                    ToTaxDate: $filter('date')(new Date(`12/31/${fy}`), 'MM/dd/yyyy'),

                    FinancialYearStartDate: $filter('date')(new Date(`01/01/${fy}`), 'MM/dd/yyyy'),
                    FinancialYear: parseInt(fy)
                }

                $scope.onbtnSaveClick = () => {
                    $scope.formSubmitted = true;

                    if ($scope.f.$valid) {
                        $ppService.CreatePostingPeriod($scope.m).then(function (d) {
                            $uibModalInstance.close($scope.m);
                        });
                    }
                };

                $scope.onbtnCancelClick = () => { $uibModalInstance.dismiss('cancel'); };

                $scope.onSelectPeriodType = () => {
                    switch ($scope.m.SubType) {
                        case 'Y':
                            $scope.m.PeriodNum = 1;
                            break;
                        case 'Q':
                            $scope.m.PeriodNum = 4;
                            break;
                        case 'M':
                            $scope.m.PeriodNum = 12;
                            break;
                        case 'D':
                            $scope.m.PeriodNum = '';
                            break;
                        default:
                            $scope.m.PeriodNum = 12;
                            break;
                    }
                };
            }]);
}());