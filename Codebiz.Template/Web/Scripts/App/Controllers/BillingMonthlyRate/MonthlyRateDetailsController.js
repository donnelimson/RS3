angular.module('MetronicApp')
    .controller('MonthlyRateDetailsController',
        ['$scope', 'NgTableParams', 'MonthlyRateService', '$location', '$window', '$q', 'CommonService', '$filter', '$timeout', '$uibModal',
            function MonthlyRateDetailsController($scope, NgTableParams, MonthlyRateService, $location, $window, $q, CommonService, $filter, $timeout, $uibModal) {

                var monthlyRateId = $location.search().monthlyRateId;
                //Go to Index
                $scope.goToIndex = function () {
                    window.location.href = document.BillingMonthlyRate;
                };

                this.$onInit = function () {
                    GetMonthlyRates();
                };

                function GetMonthlyRates() {
                    MonthlyRateService.GetMonthlyRateDetails({
                        monthlyRateId: monthlyRateId
                    }).then(function (data) {
                      //  console.log(data.data)
                        $scope.monthlyRatesDetails = data.data;
                    }, function (error /*Error event should handle here*/) {
                        console.log("Error");
                    }, function (data /*Notify event should handle here*/) {
                    });
                }
                
            }]);