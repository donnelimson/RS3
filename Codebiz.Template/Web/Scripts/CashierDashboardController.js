var app = MetronicApp;
app.controller('CashierDashboardController', ['$scope', '$q', 'NgTableParams', 'CommonService', '$uibModal', '$timeout', '$location', '$window', 'CounterSetupService', '$interval', '$filter',
    function ($scope, $q, NgTableParams, CommonService, $uibModal, $timeout, $location, $window, CounterSetupService, $interval, $filter) {
        $scope.officeId = null;
        $scope.display = null;
        var counterDataCopy = null;
        this.$onInit = function () {
            CounterSetupService.GetOfficesLookUp().then(function (d) {
                $scope.offices = d.data;
            })
            CounterSetupService.GetCashierDashboardDTO({ officeId: $scope.officeId }).then(function (d) {
                $scope.counterData = d.result;
                 
            })
        }
        $scope.loadMedia = function (id) {
            $scope.display = null;
            CounterSetupService.GetDisplayForCashierDashboard({ officeId: $scope.officeId }).then(function (d) {
                $scope.display = d.result;
            })
        }
        window.setInterval(function () {
            $scope.time = ($filter('date')(Date.now(), "hh:mm a"));
            $scope.date = ($filter('date')(Date.now(), "EEEE MMMM dd, yyyy"));
            CounterSetupService.GetCashierDashboardDTO({ officeId: $scope.officeId }).then(function (d) {
                $scope.counterData = d.result;
                compareResult($scope.counterData, counterDataCopy);
                counterDataCopy = angular.copy($scope.counterData);
            })

            $scope.$apply();
        }, 1000);

        function compareResult(origSource, copySource) {
            if (origSource != null && (copySource != null && copySource.length!=0)) {
                for (var i = 0; i <= origSource.length - 1; i++) {
                    var keys = Object.keys(origSource[i]);
                    for (var x = 0; x <= keys.length - 1; x++) {
                        if (origSource[i] != null && copySource[i] != null) {
                            if (origSource[i][keys[x]] != copySource[i][keys[x]]) {
                                CounterSetupService.AnnounceCounter({ text: "Counter " + origSource[i][keys[0]] + " serving " + origSource[i][keys[1]] });
                            }
                        }
                      
                    }
                }
            }
        }
    }])