MetronicApp.controller('CityTownAddOrUpdateController', ['$scope', '$q', 'CityTownService', 'CommonService', 'CityTownId', '$uibModalInstance','$timeout',
    function ($scope, $q, CityTownService, CommonService, CityTownId, $uibModalInstance) {

        $scope.cityTowns = [];
        $scope.provinces = [];


        this.$onInit=function() {
            $scope.regionId = null;
            $scope.provinceId = null;
            GetRegions(null);
        
            if (CityTownId == null) {
                CityTownId = 0;
                $scope.isUpdate = false;
            }
            else {
                $scope.isUpdate = true;
                GetCityTownDetails();
            }
        }
        $scope.cancel = function () {
            if ($scope.cityTownForm.$dirty) {
                CommonService.cancelChanges(function () {
                    $uibModalInstance.dismiss('cancel');
                })
            }
            else {
                $uibModalInstance.dismiss('cancel');
            }

        }
        $scope.updateProvince = function () {
            var fieldName = "ProvinceId";
            CommonService.GetProvinceLookUpByRegionId({
                regionId: $scope.regionId
            }, fieldName).then(function (data) {
                $scope.provinces = data.data;
            });
        }
    
        $scope.save = function () {
            $scope.formSubmitted = true;
            if ($scope.cityTownForm.$valid) {
                if (CityTownId == 0) {
                    CommonService.saveChanges(function () {
                        saveOrUpdate();
                    })
                }
                else {
                    CommonService.updateChanges(function () {
                        saveOrUpdate();
                    })
                }

            }
        }
        function saveOrUpdate() {
            var model = {
                CityTownId: CityTownId,
                Name: $scope.name,
                ProvinceId: $scope.provinceId,
                CityTownCode: $scope.cityTownCode,
                Longitude: $scope.longitude,
                Latitude: $scope.latitude,
                ZipCode: $scope.zipCode,
                abbv: $scope.abbv
            };
            CityTownService.AddOrUpdate({
                model: model
            }).then(function (data) {
                if (data.succeed) {
                    CommonService.successMessage(data.message);
                    $uibModalInstance.close();
                }
                else {
                    CommonService.warningMessage(data.message);
                }

            })
        }

        function GetCityTownDetails() {
            CityTownService.GetCityTownDetails({
                cityTownId: CityTownId
            }).then(function (data) {
                $scope.dataResult = data.result;
              
                $scope.name = data.result.Name;
                $scope.cityTownCode = data.result.CityTownCode;
                $scope.latitude = data.result.Latitude;
                $scope.longitude = data.result.Longitude;
                $scope.abbv = data.result.abbv;
                $scope.zipCode = data.result.ZipCode;

                GetRegions(data.result.RegionId);
                GetProvincesByRegionId(data.result.ProvinceId);

            }, function (error /*Error event should handle here*/) {
                console.log("Error");
            }, function (data /*Notify event should handle here*/) {
                console.log("Error");
            });
        }

        function GetProvincesByRegionId(id) {
            CommonService.GetProvinceLookUpByRegionId({
                regionId: $scope.dataResult.RegionId
            }).then(function (data) {
                $scope.provinces = data.data
                if (id != null) {
                    $scope.provinceId = id;
                }
               
            }, function (error /*Error event should handle here*/) {
                console.log("Error");
            }, function (data /*Notify event should handle here*/) {
                console.log("Error");
            });
        }

        function GetRegions(id) {
            CommonService.GetRegionLookUp({}, "RegionId").then(function (data) {
                $scope.regions = data.data;
                if (id != null) {
                    $scope.regionId = id;
                }
            }, function (error /*Error event should handle here*/) {
                console.log("Error");
            }, function (data /*Notify event should handle here*/) {
                console.log("Error");
            });
        }

    }]);
