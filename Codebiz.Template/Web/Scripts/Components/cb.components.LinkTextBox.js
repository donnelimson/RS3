(function () {
    var app = angular.module('cb.components.linktextbox', ['BP.SETUP.SERVICE'])
    app
        .controller('LinkTextboxCtrl', ['$scope', '$rootScope', '$uibModal', '$window', '$timeout', '$q', 'cfl.bp.service', 'cfl.invntry.service', 'cfl.fin.service','ChooseFromListService',
            function ($scope, $rootScope, $uibModal, $window, $timeout, $q, $cflBpService, $cflInvntryService, $cflFinServices,$cfl) {
                let self = this


                self.openModal = function (linkType, linkSubType, searchText) {

                    $uibModal.open({
                        templateUrl: `${document.baseUrlNoArea}Lookups/ChooseFromList/GetLookup?objType=${$scope.cbLinkType}`,
                        controller: 'ChooseFromListCtrl',
                        controllerAs: 'ctrl',
                        windowClass: 'modal_dialog',
                        backdrop: 'static',
                        keyboard: false,
                        resolve: {
                            modalData: function () {
                                return {
                                    //'LookupType': `${$scope.cbLinkType}`,
                                    //'cardType': `${$scope.cbLinkSubtype}`,
                                    'LookupType': `${linkType}`,
                                    'SearchText': `${searchText}`,
                                    'CardType': `${linkSubType}`,
                                    'Title': 'Choose From List'
                                };
                            }
                        } // data passed to the controller
                    }).result.then(function (d) {
                        $rootScope.$broadcast(`onUpdateLinkTextBox_${$scope.ComponentID}`, d)

                        switch ($scope.cbLinkType) {
                            case 'ACT':
                                $scope.ngModel = d.AcctCode
                                break;

                            case 'CRD':
                                $scope.ngModel = d.CardCode
                                break;

                            case 'ITM':
                                $scope.ngModel = d.ItemCode
                                break;

                            case 'WHS':
                                $scope.ngModel = d.WhsCode
                                break;

                            case 'ITG':
                                $scope.ngModel = d.ItemGroupCode
                                break;
                        }
                    }, function () { });
                }

                $scope.onBtnSearch = (e) => {
                    self.openModal($scope.cbLinkType, $scope.cbLinkSubtype, '')
                }

                $scope.onKeyDown = (e) => {
                    var sk = this;
                    if (e.keyCode === 9 || e.keyCode === 13) { // tab || enter
                        let searchText = e.target.value
                        if (searchText !== '') {
                            $cfl.BusinessPartnerLookupByCode(searchText).then(res => {
                                if (res.StatusCode === '0') {
                                    //$(this).trigger('keydown')
                                    var d = res.DataResult
                                    $rootScope.$broadcast(`onUpdateLinkTextBox_${$scope.ComponentID}`, d)
                                    e.isDefaultPrevented = function () { return false; }
                                } else {
                                    self.openModal($scope.cbLinkType, $scope.cbLinkSubtype, searchText)
                                }
                            })
                        }
                        else {
                            self.openModal($scope.cbLinkType, $scope.cbLinkSubtype, '')
                        }
                    }
                }

                $scope.onLinkIconClick = function (e) {
                    if ($scope.cbLinkType) {
                        switch ($scope.cbLinkType) {
                            case "CRD":
                                $cflBpService.GetBusinessPartnerByCode($scope.ngModel).then(function (d) {
                                    if (d.StatusCode === '0') {
                                        let res = d.DataResult
                                        if (res) {
                                            if (res.CardType === 'C')
                                                $window.open(`${document.baseUrlNoArea}BP/BusinessPartnerAccount/Customer#!/CustomerDetail/View/${$scope.ngModel}/Details`, '_blank');

                                            if (res.CardType === 'S')
                                                $window.open(`${document.baseUrlNoArea}BP/BusinessPartnerAccount/Vendor#!/VendorDetail/View/${$scope.ngModel}/Details`, '_blank');
                                        }
                                    }
                                });

                                break;

                            case "ACT":
                                $window.open(`${document.baseUrlNoArea}FIN/Finance/ChartOfAccount`, '_blank');
                                break;

                            case "WHS":
                                $cflInvntryService.GetWarehouseByCode($scope.ngModel).then(function (d) {
                                    if (d.StatusCode === '0') {
                                        $window.open(`${document.baseUrlNoArea}Inventory/Warehouse#!/WarehouseDetail/${$scope.ngModel}`, '_blank');
                                    }
                                });
                                break;

                            case "ITM":
                                $cflInvntryService.GetItemMasterByCode($scope.ngModel).then(function (d) {
                                    if (d.StatusCode === '0') {
                                        $window.open(`${document.baseUrlNoArea}Inventory/ItemMaster#!/ItemMasterDetail/${$scope.ngModel}`, '_blank');
                                    }
                                });
                                break;

                            case "ITG":
                                $cflInvntryService.GetItemGroupByCode($scope.ngModel).then(function (d) {
                                    if (d.StatusCode === '0') {
                                        $window.open(`${document.baseUrlNoArea}Inventory/ItemGroup#!/ItemGroupDetail/${$scope.ngModel}/${d.ItemGroupID}`, '_blank');
                                    }
                                });
                                break;
                            default:
                        }
                    }
                }
            }])
        .directive('cbLinkTextbox', ['$http', function ($http) {
            return {
                restrict: 'EA',
                controller: 'LinkTextboxCtrl',
                scope: {
                    ComponentID: '@id',
                    cbLabelItemId: '@cbLabelItemId',
                    cbLabelText: '@cbLabelText',
                    cbLabelClass: '@cbLabelClass',
                    cbInputItemId: '@cbInputItemId',
                    cbInputClass: '@cbInputClass',
                    cbLinkType: '@cbLinkType',
                    cbLinkSubtype: '@cbLinkSubtype',
                    ngRequired: '@ngRequired',
                    ngModel: '=ngModel',
                    ngDisabled: '=ngDisabled',
                    cbSubmitted: '@cbSubmitted'
                },
                link: function (scope, element, attrs) {},
                templateUrl: `${document.baseUrlNoArea}Scripts/Components/templates/LinkTextBoxTmpl.html`
            }
        }])
}());