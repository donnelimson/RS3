angular.module('MetronicApp')
    .controller('PoleAddOrUpdateController',
        ['$scope', '$location', '$timeout', '$window', 'CommonService', 'PoleService', '$uibModal',
            function PoleAddOrUpdateController($scope, $location, $timeout, $window, CommonService, PoleService, $uibModal) {

                var poleId = $location.search().poleId;
                var self = this;

                $scope.goToIndex = function () {
                    window.location.href = document.Pole;
                };
                $scope.ProvinceId = null;
                $scope.CityTownId = null;
                $scope.formSubmitted = false;
                $scope.ItemCode = "";
                $scope.ItemName = "";
                $scope.Longitude = null;
                $scope.Latitude = null;
                $scope.items = {
                    ItemCode: "",
                    ItemName: ""
                }

                //#region INIT 
                init();
                function init() {
                    $scope.saveOrUpdate = "Save";
                    $scope.addOrEdit = "Add";
                    //Province Look Up
                    GetProvince(null);

                    //Get Details for Edit
                    if (poleId != null) {
                        $scope.saveOrUpdate = "Update";
                        $scope.addOrEdit = "Edit";
                        PoleService.GetPoleDetails({
                            poleId: poleId
                        }).then(function (data) {
                            var pole = data.data;
                            $scope.PoleNumber = pole.PoleNo;
                            $scope.Code = pole.Code;
                            $scope.Name = pole.Name;
                            $scope.ProvinceId = pole.ProvinceId;
                            $scope.Longitude = pole.Longitude;
                            $scope.Latitude = pole.Latitude;
                            $scope.ItemCode = pole.ItemCode;
                            $scope.ItemName = pole.ItemName;
                            $scope.items.ItemCode = pole.ItemCode;
                            $scope.items.ItemName = pole.ItemName;

                            GetProvince(pole.ProvinceId);
                            $scope.GetCitytown(pole.ProvinceId, pole.CityTownId);
                            $scope.GetBarangays(pole.CityTownId, pole.BarangayId);
                        }, function (error /*Error event should handle here*/) {
                            console.log("Error");
                        }, function (data /*Notify event should handle here*/) {
                        });
                    }
                }
                //#endregion

                //#region Scope Functions
                //#region Google Map
                $scope.viewMap = function () {
                    $uibModal.open({
                        animation: true,
                        ariaLabelledBy: 'modal-title',
                        ariaDescribedBy: 'modal-body',
                        templateUrl: 'MapModal.html',
                        controller: 'MapController',
                        controllerAs: 'map',
                        size: 'lg',
                        keyboard: false,
                        backdrop: "static",
                        windowClass: 'modal_style',
                        resolve: {
                            lat: function () {
                                return $scope.Latitude;
                            },
                            lng: function () {
                                return $scope.Longitude;
                            },
                            forView: function () {
                                return false;
                            }
                        }
                    }).result.then(function (data) {
                        $scope.Longitude = data.lng;
                        $scope.Latitude = data.lat;
                    }, function () {

                    });
                }
                //#endregion

                //#region GeneratePolenumber
                $scope.generatePolenumber = function (code, cityTownId) {
                    if (cityTownId != null) {
                        PoleService.GeneratePolenumber({
                            code: code,
                            cityTownId: cityTownId
                        }).then(function (data) {
                            $scope.PoleNumber = data.data.PoleNumber;
                        });
                    }
                 
                };
                //#endregion

                //#region Add/Update Pole
                $scope.saveChanges = function () {
                    poleId = poleId== null ? 0 : poleId;
                    $scope.formSubmitted = true;
                    if (self.form.$valid) {
                        CommonService.saveOrUpdateChanges(function () {
                            AddUpdate();
                        }, poleId)
                    }
                  
                };
                //#endregion

                $scope.GetBarangays = function (cityTownId,id) {
                    if (cityTownId != null || cityTownId != undefined) {
                        CommonService.GetBarangay({
                            cityTownId: cityTownId
                        }, "BarangayId").then(function (data) {
                            $scope.barangays = data.data;
                            if (id != null || id != undefined) {
                                $scope.BarangayId = id;
                            }
                        },
                            function (error /*Error event should handle here*/) {
                                console.log("Error");
                            },
                            function (data /*Notify event should handle here*/) {
                            });
                    }
                 
                }
                $scope.GetCitytown = function (provinceId,id) {
                    if (provinceId != null || provinceId != undefined) {
                        CommonService.GetCityTown({
                            provinceId: provinceId
                        }, "CityTownId").then(function (data) {
                            $scope.cityTowns = data.data;
                            if (id != null || id != undefined) {
                                $scope.CityTownId = id;
                            }
                        },function (error /*Error event should handle here*/) {
                                console.log("Error");
                            },
                            function (data /*Notify event should handle here*/) {
                            });
                    }

                }

                function GetProvince(id) {
                    CommonService.GetProvinceLookUp({
                    }, "ProvinceId").then(function (data) {
                        $scope.province = data.data;
                        if (id != null || id != undefined) {
                            $scope.ProvinceId = id;
                        }
                    },function (error /*Error event should handle here*/) {
                            console.log("Error");
                        },
                        function (data /*Notify event should handle here*/) {
                        });
                }


                $scope.SearchItems = function (data) {
                    $uibModal.open({
                        animation: true,
                        ariaLabelledBy: 'modal-title',
                        ariaDescribedBy: 'modal-body',
                        templateUrl: 'SearchItems.html',
                        controller: 'SearchItemController',
                        size: 'lg',
                        keyboard: false,
                        backdrop: "static",
                        windowClass: 'modal_style',
                        modalOverflow: true,
                        resolve: {
                            Data: function () {
                                return {
                                    Items: $scope.items,
                                    Title: "Poles"
                                }
                            }
                        }
                    }).result.then(function (data) {
                        $scope.items = data;
                        $scope.ItemCode = $scope.items.ItemCode;
                        $scope.ItemName = $scope.items.ItemName;

                    }, function () {

                    });
                };

                //#region Cancel changes
                $scope.reset = function () {
                    if ($scope.controller.form.$dirty) {
                        CommonService.cancelChanges(function () {
                            $window.location.href = document.Pole;
                        });
                    } else {
                        $window.location.href = document.Pole;
                    }
                }
                //#endregion

                //#endregion

                //#region Private Functions
                function AddUpdate() {
                    var pole = {
                        PoleId: poleId,
                        PoleNo: $scope.PoleNumber,
                        Code: $scope.Code,
                        Name: $scope.Name,
                        CityTownId: $scope.CityTownId,
                        ProvinceId: $scope.ProvinceId,
                        BarangayId: $scope.BarangayId,
                        Longitude: $scope.Longitude,
                        Latitude: $scope.Latitude,
                        ItemName: $scope.ItemName,
                        ItemCode: $scope.ItemCode
                    };
                    PoleService.Form({
                        model: pole
                    }).then(function (data) {
                        if (data.success) {
                            CommonService.successMessage(data.message);
                            $timeout(function () {
                                $window.location.href = document.Pole;
                            }, 600);
                        }
                        else {
                            CommonService.warningMessage(data.message);
                        }
                    }, function (error /*Error event should handle here*/) {
                        console.log(error);
                        CommonService.errorMessage("Unexpected error occured.");
                    }, function (data /*Notify event should handle here*/) {
                    });
                }
                //#endregion
            }]);