MetronicApp.controller('BrandAddOrUpdateController', ['$scope', '$q', 'NgTableParams', 'CommonService', '$uibModal', '$timeout', '$location', '$window', 'BrandService', '$controller','PriceListService',
    function ($scope, $q, NgTableParams, CommonService, $uibModal, $timeout, $location, $window,BrandService, $controller, PriceListService) {
        $scope.f = {
            Searcher: "",
            Page: 1,
            PageSize: 50,
        }
        $scope.items = [];
        let formatter = Intl.NumberFormat('en-US', { minimumFractionDigits: 2 });
        this.$onInit = function () {
            if ($location.search().id != null) {
                BrandService.GetDetailsById({ id: $location.search().id }).then(function (d) {
                    $scope.m = d.result;
                    for (var i = 0; i <= $scope.m.Items.length - 1; i++) {
                        $scope.m.Items[i].ItemCost = formatter.format($scope.m.Items[i].ItemCost);
                    }
                    $scope.isUpdate = true;
                })
            }
        }
        $scope.m = {
            Id: 0,
            LongDescription: "",
            ShortDescription: "",
            ItemCode: "",
            Items: [],
            IsActive: false
        };
        $scope.cancel = function () {
            if (!$scope.f.$pristine) {
                CommonService.cancelChanges(function () {
                    window.location.href = document.Brand + 'Index';
                })
            }
            else
                window.location.href = document.Brand + 'Index';
        }
        $scope.deleteItem = function (index) {
            $scope.m.Items.splice(index, 1);
        }
        $scope.addItem = function () {
            var modalData = {
                LookupType: 'ITM',
                SelectedData : $scope.m.Items
            }
            $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: `${document.baseUrlNoArea}ChooseFromList/GetLookup?objType=${modalData.LookupType}`,
                controller: 'ChooseFromListController',
                size: 'md',
                keyboard: false,
                backdrop: "static",
                windowClass: 'modal_style',
                modalOverflow: true,
                resolve: {
                    Data: function () {
                        return modalData;
                    },
                }
            }).result.then(function (data) {
                $scope.m.Items = [];
                //console.log(data)
                angular.forEach(data, function (a,b) {
                    $scope.m.Items.push(a);
                })
              
            });
        }
        $scope.save = function () {

            CommonService.saveOrUpdateChanges(function () {
                for (var i = 0; i <= $scope.m.Items.length - 1; i++) {
                    $scope.m.Items[i].ItemCost = (typeof $scope.m.Items[i].ItemCost === 'string') ? $scope.m.Items[i].ItemCost.replaceAll(',', '') : $scope.m.Items[i].ItemCost;
                }
                BrandService.AddOrUpdate({ model: $scope.m }).then(function (d) {
                    if (d.success) {
                        CommonService.successMessage(d.message);
                        $timeout(function () {
                            window.location.href = document.Brand + 'Index';

                        }, 1000);
                    }
                    else {
                        CommonService.warningMessage(d.message);
                    }
                })
            }, $scope.m.Id)
        }

    }])