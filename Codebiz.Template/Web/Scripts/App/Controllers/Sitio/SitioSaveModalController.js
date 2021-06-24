MetronicApp.controller('SitioSaveModalController', ['$scope', '$uibModalInstance', 'NgTableParams', 'SitioService', 'CommonService', 'SitioId', '$timeout',
    function ($scope, $uibModalInstance, NgTableParams, SitioService, CommonService, SitioId, $timeout) {

        $scope.formSubmitted = false;

        //Other defaults

        $scope.sitio = {
            Code: "",
            Name: "",
            SitioId: null,
            ProvinceId: null,
            CityTownId: null,
            BarangayId: null,
        }

        this.$onInit = function () {
            $scope.GetProvince(null);
            if (SitioId == null || SitioId == 0) {
                $scope.addOrEdit = "Add";
                $scope.saveOrUpdate = "Save";
                $scope.GetProvince(null);
            }
            else {
                $scope.addOrEdit = "Edit";
                $scope.saveOrUpdate = "Update";
                GetSitioDetails();
            }
        }

        $scope.saveSitio = function () {
            $scope.formSubmitted = true;
            if ($scope.addSitioForm.$valid) {
                if (SitioId == null || SitioId == 0) {
                    CommonService.saveChanges(function() {
                        SitioService.AddSitio({
                            model: $scope.sitio
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
                        SitioService.UpdateSitio({
                            model: $scope.sitio
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
            if ($scope.addSitioForm.$dirty) {
                CommonService.cancelChanges(function () {
                    $uibModalInstance.dismiss('cancel');
                });
            }
            else {
                $uibModalInstance.dismiss('cancel');
            }
        }

        //Province Lookup
        $scope.GetProvince = function (id) {
            CommonService.GetProvinceLookUp({
            }, "ProvinceId").then(function (data) {
                $scope.provinces = data.data;
                if (SitioId != null && SitioId != 0 &&
                    id != null && id != undefined) {
                    $scope.sitio.ProvinceId = id
                }
            }, function (error /*Error event should handle here*/) {
                console.log("Error");
            }, function (data /*Notify event should handle here*/) {
                console.log("Error");
            });
        }

        //City Town Lookup
        $scope.GetCityTown = function (id) {
            $scope.addSitioForm.$dirty = true;
            if ($scope.sitio.ProvinceId > 0) {
                CommonService.GetCityTown({
                    ProvinceId: $scope.sitio.ProvinceId
                }, "CityTownId").then(function (data) {
                    $scope.cityTowns = data.data;
                    if (SitioId != null && SitioId != 0 &&
                        id != null && id != undefined) {
                        $scope.sitio.CityTownId = id
                        $scope.addSitioForm.$dirty = false;
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
            $scope.addSitioForm.$dirty = true;
            if ($scope.sitio.CityTownId > 0) {
                CommonService.GetBarangay({
                    cityTownId: $scope.sitio.CityTownId
                }, "BarangayId").then(function (data) {
                    $scope.barangays = data.data;
                    if (SitioId != null && SitioId != 0 &&
                        id != null && id != undefined) {
                        $scope.sitio.BarangayId = id
                        $scope.addSitioForm.$dirty = false;
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

        function GetSitioDetails() {
            SitioService.GetEditSitios({
                sitioId: SitioId
            }).then(function (data) {
                $scope.sitio = data.data;

                $scope.GetProvince($scope.sitio.ProvinceId);
                $scope.GetCityTown($scope.sitio.CityTownId);
                $scope.GetBarangay($scope.sitio.BarangayId);

            }, function (error /*Error event should handle here*/) {
                console.log("Error");
            }, function (data /*Notify event should handle here*/) {
            });
        }
    }]);