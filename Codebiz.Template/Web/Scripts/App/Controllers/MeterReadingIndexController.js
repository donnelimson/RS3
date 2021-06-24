MetronicApp.controller('MeterReadingIndexController', ['$scope', '$q', 'NgTableParams', 'CommonService', 'MeterReadingService', '$uibModal', '$timeout', '$location',
    function ($scope, $q, NgTableParams, CommonService, MeterReadingService, $uibModal, $timeout, $location) {
        $scope.tableForMeterReading = new NgTableParams({}, { dataset: [] });

        //init
        this.$onInit = function () {
            getOffices();
        }
        //functions
        function getOffices() {
            CommonService.GetOfficesLookUp().then(function (data) {
                $scope.offices = data.data;
            });
        }
        //scopes
   
        
    }])