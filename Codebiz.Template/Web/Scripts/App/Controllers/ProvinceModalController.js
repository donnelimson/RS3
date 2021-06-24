MetronicApp.controller('ProvinceSaveModalController', ['$scope', '$uibModalInstance', 'ProvinceService', 'CommonService', 'ProvinceId',
    function($scope, $uibModalInstance, ProvinceService, CommonService, ProvinceId) {

        $scope.formSubmitted = false;

        //Other defaults
        $scope.province = {
            Name: "",
            Abbreviation: "",
            RegionId: null
        }
        var provinceDetails = angular.copy($scope.province);

        function init() {
            CommonService.GetRegionLookUp({
            }).then(function(data) {
                $scope.regions = data.data;

                    if (ProvinceId == null || ProvinceId == 0) {
                        $scope.addOrEdit = "Add";
                        $scope.isEditing = false;

                    } else {
                        $scope.addOrEdit = "Edit";
                        $scope.isEditing = true;

                        ProvinceService.GetEditProvinces({
                            provinceId: ProvinceId
                        }).then(function(data) {
                                var data = data.data;
                                provinceDetails = data;

                                $scope.province.ProvinceId = data.ProvinceId;
                                $scope.province.Name = data.Name;
                                $scope.province.Abbreviation = data.Abbreviation;
                                $scope.province.RegionId = data.RegionId;
                            },
                            function(error /*Error event should handle here*/) {
                                console.log("Error");
                            },
                            function(data /*Notify event should handle here*/) {
                            });
                    }


                },
                function(error /*Error event should handle here*/) {
                    console.log("Error");
                },
                function(data /*Notify event should handle here*/) {
                });
        
        }

        init();

        $scope.saveProvince = function() {
            $scope.formSubmitted = true;
            if ($scope.saveProvinceForm.$valid) {
                if (ProvinceId == 0 || ProvinceId == null) {
                    CommonService.saveChanges(function() {
                        ProvinceService.AddProvince({
                            model: $scope.province
                        }).then(function(data) {
                            if (data.success) {
                                CommonService.successMessage(data.message);
                                $uibModalInstance.close();
                            } else {
                                CommonService.successMessage(data.message);
                            }
                        }), function(error /*Error event should handle here*/) {
                            console.log(error);
                            CommonService.errorMessage("Unexpected error occured.");
                        }, function(data /*Notify event should handle here*/) {
                        }
                    })

                } else {
                    CommonService.updateChanges(function() {
                        ProvinceService.UpdateProvince({
                            model: $scope.province
                        }).then(function(data) {
                            if (data.success) {
                                CommonService.successMessage(data.message);
                                $uibModalInstance.close();
                            } else {
                                CommonService.warningMessage(data.message);
                            }
                        }), function(error /*Error event should handle here*/) {
                            console.log(error);
                            CommonService.errorMessage("Unexpected error occured.");
                        }, function(data /*Notify event should handle here*/) {
                        }
                    })
                }
            }
        }

        $scope.cancel = function(data) {
            if (angular.equals($scope.province, provinceDetails)) {
                $uibModalInstance.dismiss('cancel');
            } else {
                CommonService.cancelChanges(function() {
                    $uibModalInstance.dismiss('cancel');
                });
            }
        }

    }]);