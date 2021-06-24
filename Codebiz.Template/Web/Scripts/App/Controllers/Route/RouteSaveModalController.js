MetronicApp.controller('RouteSaveModalController', ['$scope', '$uibModalInstance','RouteService', 'CommonService', 'DepartmentId','$timeout',
    function ($scope, $uibModalInstance, RouteService, CommonService, RouteId,$timeout) {
        var firstLoad = true;
        $scope.formSubmitted = false;
        $scope.route = {
            RouteId: null,
            Description: "",
            RouteCode: "",
            Routes: "",
            BookNo: "",
            CityTownId: null,
            BarangayId: null,
            ProvinceId: null
        };
        $scope.selectedCityCode = "";
        $scope.selectedBarangayCode = "";
        $scope.encodedBookNo = "";
        $scope.encodedRoutes = "";
        $scope.isUpdate = false;
        $scope.provinces = [];
        $scope.municipalities = [];
        $scope.barangays = [];

        function init() {
            if (RouteId === null || RouteId === 0) {
                $scope.isUpdate = false;
                getProvinces();
            } else {
                $scope.isUpdate = true; 

                RouteService.GetRouteDetails({
                    routeId: RouteId
                }).then(function (data) {
                    $scope.route = data.data;
                    getProvinces($scope.route.ProvinceId);

                    $timeout(function () {
                        $scope.saveRouteForm.$pristine = true;
                    },100)
             
                }
                );

            }
        }
        init();

        //#region Scope Functions
        $scope.updateMunicipality = function (id) {
            $scope.selectedBarangayCode = "";
            $scope.selectedCityCode = "";

            if ($scope.route.ProvinceId > 0) {
                CommonService.GetCityTown({
                    provinceId: $scope.route.ProvinceId
                }, "MunicipalityId").then(function (data) {
                    $scope.municipalities = data.data;

                    if (id != null || id != undefined) {
                        $scope.route.CityTownId = id;
                        $scope.updatePoleBarangay($scope.route.BarangayId);
                    }
                }, function (error /*Error event should handle here*/) {
                    console.log("Error");
                }, function (data /*Notify event should handle here*/) {
                    console.log("Error");
                });
            }
            else {
                $scope.municipalities = [];
                $scope.barangays = [];
                $scope.selectedBarangayCode = "";
                $scope.route.CityTownId = null;
                $scope.route.BarangayId = null;
            }
        };


        $scope.updatePoleBarangay = function (id) {
            var cityTownId = $scope.route.CityTownId;
            var fieldName = "BarangayId";
            $scope.selectedBarangayCode = "";
            $scope.selectedCityCode = "";

            if (cityTownId > 0) {
                $scope.selectedCityTown = $scope.municipalities.find(x => x.CityTownId === $scope.route.CityTownId);
                $scope.selectedCityTownCode = $scope.selectedCityTown !== undefined ? $scope.selectedCityTown.CityCode : "";
                $scope.selectedCityCode = $scope.selectedCityTownCode || "";
                if ($scope.route.BarangayId !== null && !firstLoad) {
                    $scope.route.RouteCode = $scope.selectedCityCode + $scope.selectedBarangayCode;
                }
                else {
                    if (!firstLoad) {
                        $scope.route.RouteCode = $scope.selectedCityCode;
                    }
           
                }
            }

            if (cityTownId > 0) {
                CommonService.GetBarangay({
                    cityTownId: cityTownId
                }, fieldName).then(function (data) {
                    $scope.barangays = data.data;
                    if (id != null || id != undefined) {
                        $scope.route.BarangayId = id;
                       // $scope.updateRouteCode();
                    }
                }, function (error /*Error event should handle here*/) {
                    console.log("Error");
                }, function (data /*Notify event should handle here*/) {
                    console.log("Error");
                });
            }
            else {
                $scope.route.RouteCode = "";
                $scope.selectedCityCode = "";
                $scope.selectedBarangayCode = "";
                $scope.barangays = [];
                $scope.puroks = [];
                $scope.sitios = [];
                $scope.route.BarangayId = null;
                $scope.route.PurokId = null;
                $scope.route.SitioId = null;
            }
            firstLoad = false;
        };

        $scope.updateRouteCode = function () {
            var barangayId = $scope.route.BarangayId;
            if (barangayId > 0) {
                $scope.selectedBarangay = $scope.barangays.find(x => x.BarangayId === $scope.route.BarangayId);
                $scope.selectedBarangayCode = $scope.selectedBarangay !== undefined ? $scope.selectedBarangay.BarangayCode : "";
                $scope.selectedBarangayCode = $scope.selectedBarangayCode || "";


                if ($scope.route.CityTownId !== null ) {
                    $scope.route.RouteCode = $scope.selectedCityCode + $scope.selectedBarangayCode;
                }
                else {
                    $scope.route.RouteCode = $scope.selectedBarangayCode;
                }
            }
        };

        $scope.updateRoutes = function () {
            $scope.encodedRoutes = angular.element("#Routes").val();
            $scope.route.Routes = $scope.route.RouteCode.toString() + $scope.encodedRoutes.toString();
        };

        $scope.updateBookNo = function () {
            $scope.encodedBookNo = angular.element("#BookNo").val();
            $scope.route.BookNo = $scope.selectedCityCode.toString() + $scope.encodedBookNo.toString();
        };

        $scope.saveRoute = function () {
            $scope.formSubmitted = true;
            if ($scope.saveRouteForm.$valid && $scope.route.BookNo !== '' && $scope.route.RouteCode!== '') {
                if (RouteId === 0 || RouteId === null) {
                    CommonService.saveChanges(function () {
                        addUpdateRoute($scope.route);
                    });

                }
                else {
                    CommonService.updateChanges(function () {
                        addUpdateRoute($scope.route);
                    });
                }
            }
        };

        $scope.cancel = function (data) {
            if (!$scope.saveRouteForm.$pristine) {
                CommonService.cancelChanges(function () {
                    $uibModalInstance.dismiss('cancel');
                });
            } else {
                $uibModalInstance.dismiss('cancel');
            }
        };

        //#endregion

        //#region Private Functions
        function getProvinces(id) {
            CommonService.GetProvinceLookUp({
            }).then(function (data) {
                $scope.provinces = data.data;
                if (id != null || id != undefined) {
                    $scope.route.ProvinceId = id;
                    $scope.updateMunicipality($scope.route.CityTownId);
                }

            }, function (error /*Error event should handle here*/) {
                console.log("Error");
            }, function (data /*Notify event should handle here*/) {
            });
        }

        function addUpdateRoute(routes) {
            RouteService.AddUpdate({
                model: routes
            }).then(function (data) {
                if (data.success) {
                    CommonService.successMessage(data.message);
                    $uibModalInstance.close();
                } else {
                    CommonService.warningMessage(data.message);
                }
            }), function (error /*Error event should handle here*/) {
                console.log(error);
                CommonService.errorMessage("Unexpected error occured.");
            }, function (data /*Notify event should handle here*/) {
            };
        }
        //#endregion
    }]);