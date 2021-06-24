MetronicApp.controller('PurokModalController', ['$scope', '$uibModalInstance', 'PurokService', 'CommonService', 'PurokId','$timeout',
    function ($scope, $uibModalInstance, PurokService, CommonService, PurokId, $timeout) {

        $scope.formSubmitted = false;

        //Other defaults
        $scope.purok = {
            Code: "",
            Name: "",
            PurokId: null,
            BarangayId: null,
            CityTownId: null,
            ProvinceId: null
        }
      
        this.$onInit = function () {

            if (PurokId == null || PurokId == 0) {
                $scope.addOrEdit = "Add";
                $scope.saveOrUpdate = "Save"
                $scope.GetProvince(null);
            }
            else {
                $scope.addOrEdit = "Edit";
                $scope.saveOrUpdate = "Update"
                PurokService.GetEditPurok({
                    purokId: PurokId
                }).then(function (data) {
                    $scope.purok = data.data;
               
                    $scope.GetProvince($scope.purok.ProvinceId);
                    $scope.GetCityTown($scope.purok.CityTownId);
                    $scope.GetBarangay($scope.purok.BarangayId);

                }, function (error /*Error event should handle here*/) {
                    console.log("Error");
                }, function (data /*Notify event should handle here*/) {
                });
            }
        }

        $scope.savePurok = function() {
            $scope.formSubmitted = true;
            if ($scope.savePurokForm.$valid) {
                if (PurokId == 0 || PurokId == null) {
                    CommonService.saveChanges(function() {
                        PurokService.AddPurok({
                            model: $scope.purok
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
                } else {
                    CommonService.updateChanges(function() {
                        PurokService.UpdatePurok({
                            model: $scope.purok
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

        $scope.cancel = function () {

            if (!$scope.savePurokForm.Code.$pristine || !$scope.savePurokForm.Name.$pristine || !$scope.savePurokForm.ProvinceId.$pristine
                || !$scope.savePurokForm.CityTownId.$pristine || !$scope.savePurokForm.BarangayId.$pristine) {
                CommonService.cancelChanges(function () {
                    $uibModalInstance.dismiss('cancel');
                });
            } else {
                $uibModalInstance.dismiss('cancel');
            }
        }

        //Province Lookup
        $scope.GetProvince = function (id) {
            CommonService.GetProvinceLookUp({
            }, "ProvinceId").then(function (data) {
                $scope.provinces = data.data;
                if (PurokId != null && PurokId != 0 &&
                    id != null && id != undefined) {
                    $scope.purok.ProvinceId = id;
                    $scope.savePurokForm.ProvinceId.$pristine = true;
                }
            }, function (error /*Error event should handle here*/) {
                console.log("Error");
            }, function (data /*Notify event should handle here*/) {
                console.log("Error");
            });
        
            $scope.cityTowns = [];
            $scope.barangays = [];
        }

        //City Town Lookup
        $scope.GetCityTown = function (id) {
            if ($scope.purok.ProvinceId > 0) {
                CommonService.GetCityTown({
                    ProvinceId: $scope.purok.ProvinceId
                }, "CityTownId").then(function (data) {
                    $scope.cityTowns = data.data;
                    if (PurokId != null && PurokId != 0 &&
                        id != null && id != undefined) {
                        $scope.purok.CityTownId = id
                        $scope.savePurokForm.CityTownId.$pristine = true;
                    }
                }, function (error /*Error event should handle here*/) {
                    console.log("Error");
                }, function (data /*Notify event should handle here*/) {
                    console.log("Error");
                });
            }
            else {
                $scope.cityTowns = [];
                $scope.barangays = [];
            }
        }

        //Barangay Lookup
        $scope.GetBarangay = function (id) {
            if ($scope.purok.CityTownId > 0) {
                CommonService.GetBarangay({
                    cityTownId: $scope.purok.CityTownId
                }, "BarangayId").then(function (data) {
                    $scope.barangays = data.data;
                    if (PurokId != null && PurokId != 0 &&
                        id != null && id != undefined) {
                        $scope.purok.BarangayId = id
                        $scope.savePurokForm.BarangayId.$pristine = true;
                    }
                },
                    function (error /*Error event should handle here*/) {
                        console.log("Error");
                    },
                    function (data /*Notify event should handle here*/) {
                    });
            }
            else {
                $scope.barangays = [];
            }
        }

    }]);