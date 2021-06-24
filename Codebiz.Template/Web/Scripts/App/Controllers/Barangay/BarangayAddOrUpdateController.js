MetronicApp.controller('BarangayAddOrUpdateController', ['$scope', 'CommonService', 'BarangayService', 'BarangayId','$uibModalInstance',
    function ($scope, CommonService, BarangayService, BarangayId, $uibModalInstance  ) {
        
        $scope.cityTowns = [];
        $scope.provinces = [];
        $scope.isUpdate = false;
        $scope.cancel = function () {
            if (!$scope.barangayForm.$pristine) {
                CommonService.cancelChanges(function () {
                    $uibModalInstance.dismiss('cancel');
                })
            }
            else {
                $uibModalInstance.dismiss('cancel');
            }

        }
    
        this.$onInit=function() {
            $scope.cityTownId = null;
            $scope.provinceId = null;
            var fieldName = "ProvinceId";
            CommonService.GetProvinceLookUp({
            }, fieldName).then(function (data) {
                $scope.provinces = data.data;
            });
            if (BarangayId == null) {
                BarangayId = 0;
            }
            else {
                $scope.isUpdate = true;
                BarangayService.GetBarangayDetails({
                    barangayId: BarangayId
                }).then(function (data) {
                    $scope.dataResult = data.result;
                    CommonService.GetCityTown({
                        provinceId: data.result.ProvinceId
                    }).then(function (data) {
                        $scope.cityTowns = data.data;
                        $scope.cityTownId = $scope.dataResult.CityTownId;
                    });
                    $scope.name = data.result.Name;
                    $scope.barangayCode = data.result.BarangayCode;
                    $scope.provinceId = data.result.ProvinceId;               
                    $scope.latitude = data.result.Latitude;
                    $scope.longitude = data.result.Longitude;
              
                })
            
            }
    
        }
        $scope.selectProvince = function () {
            var fieldName = "CityTownId";
            CommonService.GetCityTown({
                provinceId: $scope.provinceId
            }, fieldName).then(function (data) {
                $scope.cityTowns = data.data;
            });
        }
        $scope.save = function () {
            $scope.formSubmitted = true;
            if ($scope.barangayForm.$valid) {
                if (BarangayId == 0) {
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
                BarangayId: BarangayId,
                Name: $scope.name,
                CityTownId: $scope.cityTownId,
                Latitude: $scope.latitude,
                Longitude: $scope.longitude,
                BarangayCode: $scope.barangayCode
            };
            BarangayService.AddOrUpdate({
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
       
    }]);
