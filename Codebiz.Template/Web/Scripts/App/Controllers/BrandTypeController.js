(function () {
    'use strict';

    var app = angular.module('MetronicApp');

    app.controller('BrandTypeController', BrandTypeController);

    app.controller('AddOrUpdateBrandTypeController', AddOrUpdateBrandTypeController);


    BrandTypeController.$inject = ['$scope', '$rootScope', '$http', '$window', 'NgTableParams', '$uibModal', 'BrandTypeService'];

    AddOrUpdateBrandTypeController.$inject = ['$scope', '$rootScope', '$http', '$window', 'NgTableParams', 'BrandTypeService', 'CommonService', '$timeout', '$location'];


    function BrandTypeController($scope, $rootScope, $http, $window, NgTableParams, $uibModal, BrandTypeService) {

        //Table Initialize 
        $scope.tableForBrand = new NgTableParams({}, { dataset: [] })
        init();

        $scope.updateBrandType = function (brandTypeId) {
            //$window.location = document.baseURI + '/MemberHouseWiringDetails?accountId=' + accountId;
            window.location.href = "/BrandType/Edit?brandTypeId=" + brandTypeId;
        };

        function init() {

            BrandTypeService.GetAllBrandTypes()
            .then(function (data) {
                console.log(data.data);

                //Load Data To NgTable
                $scope.tableForBrandType = new NgTableParams({},
                    {
                        dataset: angular.copy(data.data) // Or data.data without angular.copy
                    })
            },
            function (error) {
                console.log("Error")
            },
            function (data) { }
            );

        }
        init();
    }
    function AddOrUpdateBrandTypeController($scope, $rootScope, $http, $window, NgTableParams, BrandTypeService, CommonService, $timeout, $location) {

            var self = this;

            $scope.formSubmitted = false;
            $scope.errorMessage = "";
            $scope.BrandTypeId = $location.search().brandTypeId;

            $scope.goToBrandType = function () {
                $window.location = '/BrandType'
            };

            $scope.saveChanges = function () {
                $scope.formSubmitted = true;
                if (self.brandForm.$valid && !$scope.errorMessage) {
                    CommonService.confirmAction(function () {

                        var BrandType = {
                            BrandTypeId: $scope.BrandTypeId,
                            CodeName: $scope.CodeName,
                            ShortDescription: $scope.ShortDescription,
                            LongDescription: $scope.LongDescription
                        };
                        console.log(BrandType)
                        BrandTypeService.AddOrUpdate({
                            model: BrandType
                        }).then(function (data) {
                            console.log(data.success);
                            if (data.success) {
                                CommonService.successMessage(data.message);

                                $timeout(function () {
                                    window.location.href = '/BrandType/';
                                }, 1000);
                            }
                            else {
                                CommonService.errorMessage(data.message);
                            }
                        }, function (error /*Error event should handle here*/) {
                            console.log(error);
                            CommonService.errorMessage("Unexpected error occured.");
                        }, function (data /*Notify event should handle here*/) {

                        });
                    });
                }
            }

            function init() {
                BrandTypeService.GetBrandTypeDetails({
                    brandTypeId: $scope.BrandTypeId
                }).then(function (data) {
                    $timeout(function () {
                        $scope.CodeName = data.data.CodeName,
                        $scope.ShortDescription = data.data.ShortDescription,
                        $scope.LongDescription = data.data.LongDescription
                    }, 500)
                },
                      function (error /*Error event should handle here*/) {
                          console.log("Error");
                      },
                  function (data /*Notify event should handle here*/) {
                  });

            }
            init();

    }
})();