angular.module('MetronicApp')
    .controller('ConsumerTypeAddOrUpdateController',
        ['$scope', '$location', '$timeout', '$window', 'ConsumerTypeId', '$uibModalInstance', 'CommonService', 'ConsumerTypeService', 'ForDetails',
            function ConsumerTypeAddOrUpdateController($scope, $location, $timeout, $window, ConsumerTypeId, $uibModalInstance, CommonService, ConsumerTypeService, ForDetails) {

            
                
                //#region initialize variables
                $scope.formSubmitted = false;
                $scope.willOpenShowNatural = false;
                $scope.willCloseShowNatural = true;
                $scope.willOpenShowJuridical = false;
                $scope.willCloseShowJuridical = true;
                $scope.ForDetails = ForDetails;
                var membershipTypeList = [];
                var membershipTypeSubcategoryList = [];
                //#endregion

                //#region Init Function
                this.$onInit=function() {
                    //Get Details for Edit
                    $scope.forUpdate = ConsumerTypeId == 0 ? false : true;
                    getDetailsForEdit();
                    getMembershipType();
                    getMembershipTypeSubcategory();
                }

              
                //#endregion

                //#region Scope Functions
                $scope.selectAllNatural = function (selectAllNatural) {
                    if (selectAllNatural) {
                        for (var i = 0; i <= $scope.naturalMembershipTypeSubcategories.length - 1; i++) {
                            $scope.naturalMembershipTypeSubcategories[i].IsSelected = true;
                            membershipTypeSubcategoryList.push($scope.naturalMembershipTypeSubcategories[i].MembershipTypeSubcategoryId);
                        }
                    }
                    else {
                        for (var i = 0; i <= $scope.naturalMembershipTypeSubcategories.length - 1; i++) {
                            $scope.naturalMembershipTypeSubcategories[i].IsSelected = false;
                            var indexToRemove = membershipTypeSubcategoryList.findIndex(r => r === $scope.naturalMembershipTypeSubcategories[i].MembershipTypeSubcategoryId);
                            if (indexToRemove !== -1) {
                                membershipTypeSubcategoryList.splice(indexToRemove, 1);
                            }
                        }
                    }
                };

                $scope.addRemoveNatural = function (index, selected, id) {
                    if (selected) {
                        $scope.naturalMembershipTypeSubcategories[index].IsSelected = true;
                        $scope.allNaturalIsSelected = true;
                        membershipTypeSubcategoryList.push(id);
                    }
                    else {
                        $scope.naturalMembershipTypeSubcategories[index].IsSelected = false;
                        membershipTypeSubcategoryList.splice(membershipTypeSubcategoryList.indexOf(id), 1);
                    }
                    checkIfAllNaturalIsSelected();

                };

                $scope.selectAllJuridical = function (selectAllJuridical) {
                    if (selectAllJuridical) {
                        for (var i = 0; i <= $scope.juridicalMembershipTypeSubcategories.length - 1; i++) {
                            $scope.juridicalMembershipTypeSubcategories[i].IsSelected = true;
                            membershipTypeSubcategoryList.push($scope.juridicalMembershipTypeSubcategories[i].MembershipTypeSubcategoryId);
                        }
                    }
                    else {
                        for (var i = 0; i <= $scope.juridicalMembershipTypeSubcategories.length - 1; i++) {
                            $scope.juridicalMembershipTypeSubcategories[i].IsSelected = false;
                            var indexToRemove = membershipTypeSubcategoryList.findIndex(r => r === $scope.juridicalMembershipTypeSubcategories[i].MembershipTypeSubcategoryId);
                            if (indexToRemove !== -1) {
                                membershipTypeSubcategoryList.splice(indexToRemove, 1);
                            }


                        }
                    }
                };

                $scope.openTreeNatural = function () {
                    $scope.willOpenShowNatural = false;
                    $scope.willCloseShowNatural = true;
                    $(".childrenNaturalDiv").slideToggle("slow");
                };

                $scope.closeTreeNatural = function () {
                    $scope.willOpenShowNatural = true;
                    $scope.willCloseShowNatural = false;
                    $(".childrenNaturalDiv").slideToggle("slow");
                };

                $scope.openTreeJuridical = function () {
                    $scope.willOpenShowJuridical = false;
                    $scope.willCloseShowJuridical = true;
                    $(".childrenJuridicalDiv").slideToggle("slow");
                };

                $scope.closeTreeJuridical = function () {
                    $scope.willOpenShowJuridical = true;
                    $scope.willCloseShowJuridical = false;
                    $(".childrenJuridicalDiv").slideToggle("slow");
                };

                $scope.saveChanges = function () {

                    $scope.formSubmitted = true;
                    //Update Consumer
                    if ($scope.modalForm.$valid) {


                        CommonService.saveOrUpdateChanges(function () {
                            if ($scope.allJuridicalIsSelected) {
                                membershipTypeList.push(2);
                            }
                            if ($scope.allNaturalIsSelected) {
                                membershipTypeList.push(1);
                            }
                            var consumerType = {
                                ConsumerTypeId: ConsumerTypeId,
                                Code: $scope.Code,
                                Name: $scope.Name,
                                CanBeTagAsSoleUse: $scope.canBeTagAsSoleUse,
                                IsMapa: $scope.isMapa,
                                IsBapa: $scope.isBapa,
                                MembershipTypes: membershipTypeList,
                                MembershipTypeSubcategories: membershipTypeSubcategoryList
                                //ConsumerTypeVoltageId: $scope.ConsumerTypeVoltageId,
                            };

                            ConsumerTypeService.AddOrUpdate({
                                model: consumerType
                            }).then(function (data) {
                                if (data.success) {
                                    CommonService.successMessage(data.message);
                                    $uibModalInstance.close("Save");
                                }
                                else {
                                    CommonService.warningMessage(data.message);
                                }
                            }, function (error /*Error event should handle here*/) {
                                console.log(error);
                                CommonService.errorMessage("Unexpected error occured.");
                            });
                        }, ConsumerTypeId);
                    }


                };
                    
                $scope.cancel = function () {
                    if ($scope.ForDetails) {
                        $uibModalInstance.dismiss('cancel');
                    }
                    else if (!$scope.modalForm.$pristine) {
                        CommonService.cancelChanges(function () {
                            $uibModalInstance.dismiss('cancel');
                        });
                    }
                    else {
                        $uibModalInstance.dismiss('cancel');
                    }
                };

                $scope.addRemoveJuridical = function (index, selected, id) {
                    if (selected) {
                        $scope.juridicalMembershipTypeSubcategories[index].IsSelected = true;
                        $scope.allJuridicalIsSelected = true;
                        membershipTypeSubcategoryList.push(id);
                    }
                    else {
                        $scope.juridicalMembershipTypeSubcategories[index].IsSelected = false;
                        var findIndex = membershipTypeSubcategoryList.indexOf(id);
                        membershipTypeSubcategoryList.splice(findIndex, 1);
                    }
                    //    console.log(membershipTypeSubcategoryList);
                    checkIfAllJuridicalIsSelected();

                };
                //#endregion

                //#region Private Functions
                function getDetailsForEdit() {

                    if (ConsumerTypeId == 0) {
                        //
                    }
                    else {
                        ConsumerTypeService.GetConsumerTypeDetails({
                            consumerTypeId: ConsumerTypeId
                        }).then(function (data) {
                            var data = data.data;
                            $scope.canBeTagAsSoleUse = data.CanBeTagAsSoleUse;
                            $scope.isMapa = data.IsMapa;
                            $scope.isBapa = data.IsBapa;

                            //  membershipTypeList = data.consumerMembershipTypes;
                            // console.log(data.consumerMembershipTypeSubcategories);
                            membershipTypeSubcategoryList = data.consumerMembershipTypeSubcategories;
                            if (data.consumerMembershipTypeSubcategories == null) {
                                membershipTypeSubcategoryList = [];
                            }
                            $scope.Code = data.Code;
                            $scope.Name = data.Name;
                            getMembershipType();
                            getMembershipTypeSubcategory();

                        });
                    }
                }

                function getMembershipType() {
                    CommonService.GetMembershipTypes({}).then(function (data) {
                        $scope.membershipTypes = data.data;
                    });
                }
                function getMembershipTypeSubcategory() {
                    CommonService.GetMembershipTypeSubcategories({}).then(function (data) {
                        $scope.naturalMembershipTypeSubcategories = data.data.splice(0, 2);
                        $scope.juridicalMembershipTypeSubcategories = data.data;
                        $timeout(function () {
                            setValuesToMembershipTypeSubCategories();
                        });

                    });
                }
                function setValuesToMembershipTypeSubCategories() {
                    //console.log($scope.naturalMembershipTypeSubcategories);
                    //console.log(membershipTypeSubcategoryList);
                    if (membershipTypeSubcategoryList != null) {

                        for (var i = 0; i <= membershipTypeSubcategoryList.length - 1; i++) {
                            var indexToSet = $scope.naturalMembershipTypeSubcategories.findIndex(r => r.MembershipTypeSubcategoryId == membershipTypeSubcategoryList[i]);
                            if (indexToSet != -1) {
                                $scope.naturalMembershipTypeSubcategories[indexToSet].IsSelected = true;
                                $scope.allNaturalIsSelected = true;
                            }
                        }
                        for (var i = 0; i <= membershipTypeSubcategoryList.length - 1; i++) {
                            var indexToSet = $scope.juridicalMembershipTypeSubcategories.findIndex(r => r.MembershipTypeSubcategoryId == membershipTypeSubcategoryList[i]);
                            if (indexToSet !== -1) {
                                $scope.juridicalMembershipTypeSubcategories[indexToSet].IsSelected = true;
                                $scope.allJuridicalIsSelected = true;
                            }
                        }
                    }

                }


                function checkIfAllNaturalIsSelected() {
                    for (var i = 0; i <= $scope.naturalMembershipTypeSubcategories.length - 1; i++) {
                        if ($scope.naturalMembershipTypeSubcategories[i].IsSelected) {
                            $scope.allNaturalIsSelected = true;
                            break;
                        }
                        else {
                            $scope.allNaturalIsSelected = false;
                        }
                    }
                }
                function checkIfAllJuridicalIsSelected() {
                    for (var i = 0; i <= $scope.juridicalMembershipTypeSubcategories.length - 1; i++) {
                        if ($scope.juridicalMembershipTypeSubcategories[i].IsSelected) {
                            $scope.allJuridicalIsSelected = true;
                            break;
                        }
                        else {
                            $scope.allJuridicalIsSelected = false;
                        }
                    }
                }
                //#endregion
            }]);