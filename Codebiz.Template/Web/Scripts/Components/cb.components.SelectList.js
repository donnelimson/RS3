
(function () {
    var app = angular.module('cb.components.SelectList', ['SL.DATASERVICE'])
    app.controller('SelectListCtrl', ['$scope', '$rootScope', '$window', '$location', 'SelectListService', function ($scope, $rootScope, $window, $location, $slsvc) {

        $scope.onSelectChange = function (e) {
            var data = {
                Type: $scope.cbObjType,
                Data: null
            };

            if ($scope.cbSelectChange != undefined && $scope.cbSelectChange != "" && $scope.cbSelectChange == "true") {
                console.log($scope.ngModel);
                if ($scope.ngModel != undefined && $scope.ngModel != null && $scope.ngModel != "") {
                    switch ($scope.cbObjType) {
                        case 'PYG':
                            $slsvc.GetPaymentGroupByID($scope.ngModel).then(function (d) {
                                data.Data = d.DataResult;
                                $rootScope.$broadcast("onUpdateSelectList", data);
                                $rootScope.$broadcast("onComputeDueDate", data);
                            })
                            break;
                    }
                }
                else {
                    $rootScope.$broadcast("onUpdateSelectList", data);
                }
            } else {
                data.SelectedValue = $scope.ngModel
                data.SelectedText = $scope.SelectList.filter(x => x.LookupID === $scope.ngModel)[0].LookupName
                $rootScope.$broadcast(`onSelectListChange_${$scope.ComponentID}`, data);
            }
        }

        $scope.onLinkIconClick = function (e) {
            switch ($scope.cbObjType) {
                case 'WHS':
                    var lookup = $scope.SelectList.filter(x => x.LookupID === $scope.ngModel)[0]
                    $window.open(`${document.baseUrlNoArea}Inventory/Warehouse#!/WarehouseDetail/${lookup.LookupCode}`, '_blank');
                    break;

                case 'ITG':
                    var lookup = $scope.SelectList.filter(x => x.LookupID === $scope.ngModel)[0]
                    $window.open(`${document.baseUrlNoArea}Inventory/ItemGroup#!/ItemGroupDetail/${lookup.LookupCode}/${$scope.ngModel}`, '_blank');
                    break;

                case 'PYG':
                    $window.open(`${document.baseUrlNoArea}BP/BusinessPartnerAccount/PaymentGroup#!/PaymentGroupDetail/View/${$scope.ngModel}/Details`, '_blank');
                    break;

                case 'SHT':
                    $window.open(`${document.baseUrlNoArea}ShippingTypes`, '_blank');
                    break;

                case 'PRL':
                    $window.open(`${document.baseUrlNoArea}Inventory/PriceList`, '_blank');
                    break;
            }
        }

        switch ($scope.cbObjType) {
            case 'WHS':
                $slsvc.WarehouseSL().then(function (d) {
                    if (d.StatusCode === '0') {
                        $scope.SelectList = d.DataResult;
                        setValue();

                        //$scope.SelectList.push({
                        //    'LookupID': '-1',
                        //    'LookupCode': 'AddNew',
                        //    'LookupName': '*Add New*'
                        //})
                    }
                })
                break;
            case 'ITG':
                $slsvc.ItemGroupSL().then(function (d) {
                    if (d.StatusCode === '0') {
                        $scope.SelectList = d.DataResult;
                        setValue();
                    }
                })
                break;
            case 'PYG':
                $slsvc.PaymentGroupSL().then(function (d) {
                    if (d.StatusCode === '0') {
                        $scope.SelectList = d.DataResult;
                        setValue();
                    }
                })
                break;
            case 'PRL':
                $slsvc.PriceListSL().then(function (d) {
                    if (d.StatusCode === '0') {
                        $scope.SelectList = d.DataResult;
                        setValue();
                    }
                })
                break;
            case 'VTG':
                $slsvc.TaxGroupSL(JSON.parse($scope.cbParams).Category).then(function (d) {
                    if (d.StatusCode === '0') {
                        $scope.SelectList = d.DataResult;
                        setValue();
                    }
                })
                break;
            case 'SHT':
                $slsvc.ShippingTypeSL().then(function (d) {
                    if (d.StatusCode === '0') {
                        $scope.SelectList = d.DataResult;
                        setValue();
                    }
                })
                break;
            case 'PROJ':
                $slsvc.GetProjectLookUp().then(function (d) {
                    if (d.StatusCode === '0') {
                        $scope.SelectList = d.DataResult;
                        setValue();
                    }
                })
                break;
            case 'IND':
                $slsvc.GetIndustryLookUp().then(function (d) {
                    if (d.StatusCode === '0') {
                        $scope.SelectList = d.DataResult;
                        setValue();
                    }
                })
                break;
            case 'CDISC':
                $slsvc.CashDiscountLookupList().then(function (d) {
                    if (d.StatusCode === '0') {
                        $scope.SelectList = d.DataResult;
                        setValue();
                    }
                })
                break;

            case 'HBN': // HouseBank
                $slsvc.GetHouseBankLookup({ 'Page': '1', 'PageSize': '100', 'SortBy': 'LookupName' }).then(function (d) {
                    if (d.StatusCode === '0') {
                        $scope.SelectList = d.DataResult;
                        setValue();
                    }
                })
                break;


            case 'BNK': // Bank
                $slsvc.GetBanks().then(function (d) {
                    if (d.StatusCode === '0') {
                        $scope.SelectList = d.DataResult;
                        setValue();
                    }
                })
                break;
        }

        function setValue() {
            if ($scope.ngModel == undefined && $scope.cbDefaultValue != undefined && $scope.cbDefaultValue != "")
                $scope.ngModel = parseInt($scope.cbDefaultValue);
        }
    }])
        .directive('cbSelectList', ['$http', function ($http) {
            return {
                restrict: 'EA',
                controller: 'SelectListCtrl',
                scope: {
                    ComponentID: '@id',
                    cbSelectViewData: '@cbSelectViewData',
                    cbSelectAllowedAdd: '@cbSelectAllowedAdd',
                    cbDefaultValue: '@cbDefaultValue',
                    cbLabelItemId: '@cbLabelItemId',
                    cbLabelText: '@cbLabelText',
                    cbLabelClass: '@cbLabelClass',
                    cbSelectItemId: '@cbSelectItemId',
                    cbSelectText: '@cbLabelText',
                    cbSelectValue: '@cbSelectValue',
                    cbSelectClass: '@cbSelectClass',
                    cbSelectChange: '@cbSelectChange',
                    cbObjType: '@cbObjType',
                    cbParams: '@cbParams',
                    ngModel: '=ngModel',
                    ngOptions: '@ngOptions',
                    ngDisabled: '=ngDisabled',
                    ngRequired: '=ngRequired',
                    ngMessage: '=ngMessage'
                },
                link: function (scope, element, attrs, ngModel) {
                },
                templateUrl: `${document.baseUrlNoArea}Scripts/Components/templates/SelectListTmpl.html`
            }
        }]);
}());