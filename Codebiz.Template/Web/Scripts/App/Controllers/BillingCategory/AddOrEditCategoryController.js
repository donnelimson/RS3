angular.module('MetronicApp').controller('AddOrEditCategoryController',
    ['$scope', 'BillingCategoryService', 'NgTableParams', '$q', 'CommonService', '$window', '$timeout',
        function ($scope, BillingCategoryService, NgTableParams, $q, CommonService, $window, $timeout) {

            $scope.isUpdate = false;
            $scope.lastIndex = 0;
            $scope.Id = 0;

            var categoriesCopy = [];

            this.$onInit = function () {
                $scope.reset();
                initEmptyRow();
                $scope.hideAddRows = false;
            }

            //#region $scopes

            $scope.search = function () {
                var initialSettings = {

                    getData: function (params) {
                        for (var i in params.sorting()) {
                            $scope.sortColumn = i;
                            $scope.sortOrder = params.sorting()[i];
                        }
                        var d = $q.defer();

                        BillingCategoryService.GetAllCategories({
                            SortDirection: $scope.sortOrder,
                            SortColumn: $scope.sortColumn
                        }).then(function (data) {
                            params.total(data.totalRecordCount);
                            $scope.categories = data.data;
                            $scope.lastIndex = $scope.categories.length - 1;
                            categoriesCopy = angular.copy($scope.categories);
                            d.resolve($scope.categories);
                        });
                        return d.promise;
                    },
                    counts: []

                };

                $scope.tableParams = new NgTableParams(10, initialSettings);
            }

            $scope.reset = function () {

                $scope.hideAddRows = false;
                $scope.selected = {};

                $scope.sortColumn = "ModifiedIndex";
                $scope.sortOrder = "asc";

                $scope.search();
            }

            $scope.cancel = function () {
                if (!$scope.saveCategoryForm.$pristine) {
                    CommonService.cancelChanges(function () {
                        $window.location.href = document.BillingCategory;
                    });
                }
                else {
                    $window.location.href = document.BillingCategory;
                }
            }

            $scope.backToList = function () {
                $window.location.href = document.BillingCategory + "Index";
            }

            //#endregion

            //#region Validations

            $scope.checkIfNameExists = function () {
                if (checkIfNameExists($scope.emptyRow.Name)) {
                    $scope.alreadyExists = true;
                }
                else {
                    $scope.alreadyExists = false;
                }
            }

            function checkIfNameExists(name, id) {
                for (var i = 0; i < $scope.categories.length; i++) {
                    if ($scope.categories[i].Name === name && $scope.categories[i].CategoryId != id) {
                        return true;
                    }
                }

                return false
            }

            function checkIfCodeExists(code, id) {
                for (var i = 0; i < $scope.categories.length; i++) {
                    if ($scope.categories[i].Code === code && $scope.categories[i].CategoryId != id) {
                        return true;
                    }
                }

                return false
            }

            //#endregion

            //#region Add Row

            $scope.addWhenEnter = function ($event, category) {
                var keyCode = $event.which || $event.keyCode;
                if (keyCode == 13) {
                    addOrEdit(category);

                }
            }

            function addOrEdit(category) {

                var isCodeExist = checkIfCodeExists(category.IsEditing ? category.Code : $scope.emptyRow.Code, category.CategoryId);
                var isNameExist = checkIfNameExists(category.IsEditing ? category.Name : $scope.emptyRow.Name, category.CategoryId);

                if (category.IsEditing) {
                    $scope.codeHasErrorInEdit = (category.Code === undefined || category.Code === "") || isCodeExist;
                    $scope.codeMessageInEdit = (category.Code === undefined || category.Code === "") ? "Code is required!" : isCodeExist ? "Code already exist!" : "";

                    $scope.nameHasErrorInEdit = (category.Name === undefined || category.Name === "") || isNameExist;
                    $scope.nameMessageInEdit = (category.Name === undefined || category.Name === "") ? "Name is required!" : isNameExist ? "Name already exist!" : "";

                    if (!$scope.codeHasErrorInEdit && !$scope.nameHasErrorInEdit) {
                        category.IsEditing = false;
                        $scope.hideAddRows = false;
                        $scope.exist = false;
                        initEmptyRow();
                        categoriesCopy = $scope.categories;
                    }
                    else $scope.exist = true;

                }
                else {
                    $scope.codeHasError = $scope.emptyRow.Code === "" || isCodeExist;
                    $scope.codeMessage = $scope.emptyRow.Code === "" ? "Code is required!" : isCodeExist ? "Code already exist!" : "";

                    $scope.nameHasError = $scope.emptyRow.Name === "" || isNameExist;
                    $scope.nameMessage = $scope.emptyRow.Name === "" ? "Name is required!" : isNameExist ? "Name already exist!" : "";

                    if (!$scope.codeHasError && !$scope.nameHasError) {
                        $scope.categories.push($scope.emptyRow);
                        categoriesCopy = $scope.categories;
                        initEmptyRow();
                    }
                }

            }

            //#endregion

            //#region Edit Row

            $scope.editRow = function (index) {
                if (!$scope.codeHasErrorInEdit &&
                    !$scope.nameHasErrorInEdit || !$scope.codeHasError && !$scope.nameHasError &&
                    !$scope.categories[$scope.lastIndex].IsEditing) {

                    var lastdata = angular.copy(categoriesCopy[$scope.lastIndex]);

                    categoriesCopy = angular.copy($scope.categories);

                    if (!$scope.exist) {
                        $scope.categories[index].IsEditing = true;
                        $scope.hideAddRows = true;

                        if (index != $scope.lastIndex) {
                            $scope.categories[$scope.lastIndex] = lastdata;

                            $scope.categories[$scope.lastIndex].IsEditing = false;


                            $scope.lastIndex = index;
                        }
                    }
                }
            }

            $scope.cancelEditRow = function () {

                var lastdata = angular.copy(categoriesCopy[$scope.lastIndex]);

                if ((!$scope.codeHasErrorInEdit &&
                    !$scope.nameHasErrorInEdit || !$scope.codeHasError && !$scope.nameHasError) &&
                    $scope.categories[$scope.lastIndex].IsEditing) {

                    $scope.categories[$scope.lastIndex] = lastdata;
                    $scope.categories[$scope.lastIndex].IsEditing = false;
                    $scope.hideAddRows = false;
                }
            }

            //#endregion

            //#region Delete Row

            $scope.deleteRow = function (index) {
                if ($scope.categories[index].IsInUse) {
                    CommonService.warningMessage("Unable to delete " + $scope.categories[index].Name + "." + $scope.categories[index].Name + " is in use");
                }
                else {
                    $scope.categories.splice(index, 1);
                }
                
            };

            //#endregion

            //#region Save Category

            $scope.saveCategory = function (index) {
                if (!isBlank($scope.emptyRow.Code) || !isBlank($scope.emptyRow.Name) || !isBlank($scope.emptyRow.DislayedName)) {
                    CommonService.warningMessage("You have unsaved changes, kindly press enter to save it");
                }
                else {
                    CommonService.confimInfo(function () {
                        CommonService.showLoading();

                        BillingCategoryService.AddCategory({
                            model: $scope.categories
                        }).then(function (data) {
                            if (data.success) {
                                CommonService.hideLoading();
                                CommonService.successMessage(data.message);
                                $timeout(function () {
                                    $window.location.href = document.BillingCategory;
                                }, 800);
                            }
                            else {
                                CommonService.hideLoading();
                                CommonService.warningMessage(data.message);
                            }

                        }), function (error /*Error event should handle here*/) {
                            CommonService.hideLoading();
                            console.log("Error");
                            CommonService.errorMessage("Unexpected error occured.");
                        }, function (data /*Notify event should handle here*/) {
                        };

                    }, "Save Record?", "Selected Item/s will be save.", "Save");
                }
            }

            //#endregion

            //#region Private Functions

            function isBlank(str) {
                return (!str || /^\s*$/.test(str));
            }

            function initEmptyRow() {
                $scope.emptyRow = {
                    Id: $scope.Id,
                    Code: "",
                    Name: "",
                    DisplayedName: "",
                    IsBill: false,
                    IsRequired: false,
                    IsEditing: false
                };

                $scope.Id++;
            }

          

            //#endregion

            //#region Trash Functions

            $scope.isClicked = function () {
                $scope.clicked = true;
            }

            $scope.cancelAddRow = function () {

                getPositions($scope.emptyRow);
                $scope.emptyRow.Position = [];
                $scope.positionHasError = false;
                $scope.clicked = false;
            }

            //#endregion
        }
    ])

    .directive('focusOn', function ($timeout) {
        return {
            restrict: 'A',
            link: function ($scope, $element, $attr) {
                $scope.$watch($attr.focusOn, function (_focusVal) {
                    $timeout(function () {
                        _focusVal ? $element[0].focus() :
                            $element[0].blur();
                    });
                });
            }
        };
    });

 