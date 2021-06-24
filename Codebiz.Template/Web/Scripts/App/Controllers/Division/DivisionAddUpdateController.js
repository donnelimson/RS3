angular.module('MetronicApp')
    .controller('DivisionAddUpdateController',
        ['$scope', 'DivisionService', '$route', 'CommonService', '$location', '$timeout',
            function ($scope, DivisionService, $route, CommonService, $location, $timeout, ) {

                var DivisionId = $route.current.params.id == null ? 0 : $route.current.params.id;

                $scope.formSubmitted = false;

                $scope.division = {
                    DivisionId: null,
                    CategoryIds: null,
                    DivisionName: "",
                    DivisionCode: ""
                };

                $scope.IsView = $route.current.$$route.params.isView;
                $scope.divisionHasError = false;
                $scope.addEditOrView = "Add";
                $scope.saveOrUpdate = "Save";
                $scope.categories = [];


                this.$onInit = function () {
                    getCategories();

                    if (DivisionId != 0 && DivisionId != null) {
                        $scope.addEditOrView = $scope.IsView ? "View" : "Edit";
                        $scope.saveOrUpdate = "Update";
                        getDivisionDetails();
                    }
                };

                $scope.saveDivision = function () {
                    $scope.formSubmitted = true;
                    if ($scope.saveDivisionForm.$valid) {
                        $scope.formSubmitted = true;
                        CommonService.saveOrUpdateChanges(function () {
                            DivisionService.AddUpdateDivision({
                                model: $scope.division
                            }).then(function (data) {
                                if (data.success) {
                                    CommonService.successMessage(data.message);
                                    $timeout(function () {
                                        $location.path("View");
                                    }, 800);
                                } else {
                                    CommonService.warningMessage(data.message);
                                }
                            }), function (error /*Error event should handle here*/) {
                                console.log(error);
                                CommonService.errorMessage("Unexpected error occured.");
                            }, function (data /*Notify event should handle here*/) {
                            };
                        }, DivisionId);
                    }
                };

                $scope.cancel = function () {
                    if ($scope.addEditOrView === "View")
                    {
                        $location.path("View");
                    }
                    else {
                        if ($scope.saveDivisionForm.CategoryIds.$touched ||
                            $scope.saveDivisionForm.Code.$dirty ||
                            $scope.saveDivisionForm.DivisionName.$dirty) {
                            CommonService.cancelChanges(function () {
                                angular.element(document.getElementById("cancel").click());
                            });
                        }
                        else {
                            $location.path("View");
                        }
                    }
                };


                function getDivisionDetails() {
                    DivisionService.GetDetailsForUpdate({
                        divisionId: DivisionId
                    }, DivisionId).then(function (data) {
                        $scope.division = data.DataResult;
                    }, function (error /*Error event should handle here*/) {
                        console.log("Error");
                    }, function (data /*Notify event should handle here*/) {
                    });
                }
             
                function getCategories() {
                    DivisionService.GetCategories({
                    }, "CategoryId").then(function (data) {
                        $scope.categories = data.data;
                    }, function (error /*Error event should handle here*/) {
                        console.log("Error");
                    }, function (data /*Notify event should handle here*/) {
                    });
                }
            }]);