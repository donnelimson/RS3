angular.module('MetronicApp')
    .controller('RestrictionController', ['$scope', 'EmployeeService', 'restrictions', '$uibModalInstance', 'CommonService', '$filter',
        function ($scope, EmployeeService, restrictions, $uibModalInstance, CommonService, $filter) {

            $scope.selectedData = angular.copy(restrictions);

            this.$onInit = function () {
                getRestrictionCode(restrictions);
            }

            $scope.selectItem = function (data, restriction) {
                if (restriction) {
                    $scope.selectedData.push(data.Id.toString());
                }
                else {
                    var indexToRemove = $scope.selectedData.findIndex(x => x == data.Id);
                    if (indexToRemove != -1) {
                        $scope.selectedData.splice(indexToRemove, 1);
                    }
                }

                $scope.selectedData = $filter('orderBy')($scope.selectedData, false);
            }


            $scope.cancel = function () {
                if ($scope.form.$dirty) {
                    CommonService.cancelChanges(function () {
                        $uibModalInstance.dismiss('cancel');
                    });
                } else {
                    $uibModalInstance.dismiss('cancel');
                }
            }

            $scope.onbtnSelectClick = function (s, e) {
                if ($scope.selectedData.length !== 0) {
                    $uibModalInstance.close($scope.selectedData);
                }
                else {
                    CommonService.warningMessage("Nothing is selected, please select data.");
                }
            };

            function getRestrictionCode(item) {
                EmployeeService.GetLicenseRestrictionCode({}).then(function (data) {
                    $scope.Restriction = data.data;
                    if (item.length > 0) {
                       
                        for (var i = 0; i < $scope.Restriction.length; i++) {
                            var index = $scope.Restriction.findIndex(r => r.Id == item[i]);
                            if (index != -1) {
                                $scope.Restriction[index].IsCheck = true;
                            }
                        }
                    }
                }, function (error /*Error event should handle here*/) {
                    console.log("Error");
                }, function (data /*Notify event should handle here*/) {
                });
            }

        }
    ]);