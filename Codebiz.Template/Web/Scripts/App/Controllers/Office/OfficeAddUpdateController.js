MetronicApp.controller('OfficeAddUpdateController', ['$scope', 'OfficeService', 'CommonService', '$window', '$timeout',
    function ($scope, OfficeService, CommonService, $window, $timeout,) {

        $scope.formSubmitted = false;

        //#region Other defaults

        $scope.office = {
            Code: "",
            Name: "",
            OfficeId: null,
            BlkLotNo: "",
            Street: "",
            BarangayId: null,
            PurokId: null,
            SitioId: null,
            IsMainOffice: false,
            IsEditing: false,
            DepartmentData: [],
            Departments: []
        }

        var OfficeId = angular.element("#OfficeId").val();
        var officeDetails = angular.copy($scope.office);
        $scope.officeIndex = document.Office + "Index";
        $scope.Id = 0;
        $scope.lastIndex = 0;
        $scope.departmentHasError = false;

        //#endregion

        //#region Initialization

        this.$onInit = function () {
            initEmptyRow(null);
            checkIfHasMainOffice();
            $scope.GetProvince(null);
            if (OfficeId == null || OfficeId == 0) {
                $scope.addOrEdit = "Add";
                $scope.isUpdate = false;
            }
            else {
                $scope.addOrEdit = "Edit";
                $scope.isUpdate = true;
                getOfficeDetails();
            }
        }

        //#endregion

        //#region Add Departments

        $scope.checkIfHasMainOffice = function (hasMainOffice, data) {
            $scope.office.IsMainOffice = false;
            if (data) {
                if (hasMainOffice) {
                    CommonService.confimInfo(function () {
                        $scope.$apply(function () {
                            $scope.office.IsMainOffice = true;
                        });
                    }, "Office Has Main office. Change Main office?", "Selected office will be the new main office.", "Select");
                }
                else {
                    $scope.office.IsMainOffice = true;
                }
            }
        }

        $scope.addWhenChange = function (data) {
            var department = data.Department;
            department = data.IsEditing
                ? data.Department != null ? data.departments.find(queue => queue.DepartmentId === department.DepartmentId) : null
                : $scope.emptyRow.Department != null ? data.departments.find(queue => queue.DepartmentId === $scope.emptyRow.Department.DepartmentId) : null

            if (department != null && department.DepartmentId != null) {


                isDepartmentExist = department.DepartmentId == undefined ? false : checkIfDepartmentExists(department);

                if (data.IsEditing) {

                    $scope.departmentHasErrorInEdit = department.Name == undefined || department.Name === "" || isDepartmentExist;
                    $scope.departmentMessageInEdit = department.Name == undefined || department.Name === ""
                        ? "Department is required!"
                        : isDepartmentExist ? "Department already exist!" : "";

                    if (!$scope.departmentHasErrorInEdit) {
                        data.DepartmentId = department.DepartmentId;
                        data.Department.Name = department.Name;
                        data.IsEditing = false;
                        $scope.hideAddRows = false;
                        $scope.exist = false;
                        initEmptyRow();
                    }
                }
                else {

                    $scope.departmentHasError = department.DepartmentId === null || isDepartmentExist;
                    $scope.departmentMessage = department.DepartmentId === null ? "Department is required!" : isDepartmentExist ? "Department already exist!" : "";

                    if (!$scope.departmentHasError) {
                        data.DepartmentId = data.Department.DepartmentId;
                        $scope.office.DepartmentData.push(data);
                        $scope.lastIndex = $scope.office.DepartmentData.length - 1;
                        data.IsEditing = false;
                        initEmptyRow();
                    }
                }
            }

        }

        $scope.deleteRow = function (index) {
            $scope.office.DepartmentData.splice(index, 1);
            $scope.hideAddRows = false;
        };

        $scope.editRow = function (index) {
            if (!$scope.departmentHasErrorInEdit &&
                !$scope.departmentHasError &&
                !$scope.office.DepartmentData[index].IsEditing) {

                $scope.office.DepartmentData[index].IsEditing = true;
                $scope.hideAddRows = true;

                if (index != $scope.lastIndex) {
                    $scope.office.DepartmentData[$scope.lastIndex].IsEditing = false;
                    $scope.lastIndex = index;
                }

                $scope.GetDepartments($scope.office.DepartmentData[$scope.lastIndex], $scope.office.DepartmentData[$scope.lastIndex].DepartmentId);
            }
        }

        //#endregion

        //#region SAVE OFFICE

        $scope.saveOffice = function () {
            $scope.departmentHasError = $scope.office.DepartmentData.length == 0;
            $scope.formSubmitted = true;
            if ($scope.saveOfficeForm.$valid && !$scope.departmentHasError) {
                if (OfficeId == 0 || OfficeId == null) {
                    CommonService.saveChanges(function () {
                        OfficeService.AddOffice({
                            model: $scope.office
                        }).then(function (data) {
                            if (data.success) {
                                CommonService.successMessage(data.message);
                                $timeout(function () {
                                    $window.location.href = $scope.officeIndex;
                                }, 800);
                            } else {
                                CommonService.warningMessage(data.message);
                            }
                        }), function (error /*Error event should handle here*/) {
                            console.log(error);
                            CommonService.errorMessage("Unexpected error occured.");
                        }, function (data /*Notify event should handle here*/) {
                        }
                    })

                } else {
                    CommonService.updateChanges(function () {
                        OfficeService.UpdateOffice({
                            model: $scope.office
                        }).then(function (data) {
                            if (data.success) {
                                CommonService.successMessage(data.message);
                                $timeout(function () {
                                    $window.location.href = $scope.officeIndex;
                                }, 800);
                            } else {
                                CommonService.warningMessage(data.message);
                            }
                        }), function (error /*Error event should handle here*/) {
                            console.log(error);
                            CommonService.errorMessage("Unexpected error occured.");
                        }, function (data /*Notify event should handle here*/) {
                        }
                    })
                }
            }
        }

        //#endregion

        //#region BACK TO LIST / CANCEL BUTTON

        $scope.backToList = function () {
            window.location.href = document.Office + "Index";
        };

        $scope.cancel = function () {
            if (angular.equals($scope.office, officeDetails)) {
                $window.location.href = $scope.officeIndex;
            } else {
                CommonService.cancelChanges(function () {
                    $window.location.href = $scope.officeIndex;
                });
            }
        }

        //#endregion

        //#region Lookup

        //Province Lookup
        $scope.GetProvince = function (id) {
            CommonService.GetProvinceLookUp({
            }, "ProvinceId").then(function (data) {
                $scope.provinces = data.data;
                if (OfficeId != null && OfficeId != 0 &&
                    id != null && id != undefined) {
                    $scope.office.ProvinceId = id
                }
            }, function (error /*Error event should handle here*/) {
                console.log("Error");
            }, function (data /*Notify event should handle here*/) {
                console.log("Error");
            });

            $scope.cityTowns = [];
            $scope.barangays = [];
            $scope.puroks = [];
            $scope.sitios = [];
        }

        //City Town Lookup
        $scope.GetCityTown = function (id) {
            if ($scope.office.ProvinceId > 0) {
                CommonService.GetCityTown({
                    ProvinceId: $scope.office.ProvinceId
                }, "CityTownId").then(function (data) {
                    $scope.cityTowns = data.data;
                    if (OfficeId != null && OfficeId != 0 &&
                        id != null && id != undefined) {
                        $scope.office.CityTownId = id
                    }
                }, function (error /*Error event should handle here*/) {
                    console.log("Error");
                }, function (data /*Notify event should handle here*/) {
                    console.log("Error");
                });
            }
            else {
                $scope.cityTowns = [];
                $scope.barangays = [];
                $scope.puroks = [];
                $scope.sitios = [];
            }
        }

        //Barangay Lookup
        $scope.GetBarangay = function (id) {
            if ($scope.office.CityTownId > 0) {
                CommonService.GetBarangay({
                    cityTownId: $scope.office.CityTownId
                }, "BarangayId").then(function (data) {
                    $scope.barangays = data.data;
                    if (OfficeId != null && OfficeId != 0 &&
                        id != null && id != undefined) {
                        $scope.office.BarangayId = id
                    }
                },
                    function (error /*Error event should handle here*/) {
                        console.log("Error");
                    },
                    function (data /*Notify event should handle here*/) {
                    });
            }
            else {
                $scope.barangays = [];
                $scope.puroks = [];
                $scope.sitios = [];
            }
        }

        //Purok Lookup
        $scope.GetPurok = function (id) {
            if ($scope.office.BarangayId > 0) {
                CommonService.GetPurok({
                    barangayId: $scope.office.BarangayId
                }, "PurokId").then(function (data) {
                    $scope.puroks = data.data;
                    if (OfficeId != null && OfficeId != 0 &&
                        id != null && id != undefined) {
                        $scope.office.PurokId = id
                    }
                }, function (error /*Error event should handle here*/) {
                    console.log("Error");
                }, function (data /*Notify event should handle here*/) {
                    console.log("Error");
                });
            }
            else {
                $scope.puroks = [];
                $scope.sitios = [];
            }
        }

        //Sitio Lookup
        $scope.GetSitio = function (id) {
            if ($scope.office.BarangayId > 0) {
                CommonService.GetSitio({
                    id: $scope.office.BarangayId
                }, "SitioId").then(function (data) {
                    $scope.sitios = data.data;
                    if (OfficeId != null && OfficeId != 0 &&
                        id != null && id != undefined) {
                        $scope.office.SitioId = id
                    }
                }, function (error /*Error event should handle here*/) {
                    console.log("Error");
                },
                    function (data /*Notify event should handle here*/) {
                        console.log("Error");
                    });
            }
            else {
                $scope.sitios = [];
            }
        }

        $scope.GetDepartments = function (dataValue, id) {
            CommonService.GetDepartmentLookUp({
            }, "emptyRowDepartment").then(function (data) {
                dataValue.departments = data.data;
                if (id != null && id != undefined) {
                    dataValue.DepartmentId = id;
                }
            }, function (error /*Error event should handle here*/) {
                console.log("Error");
            },
                function (data /*Notify event should handle here*/) {
                    console.log("Error");
                });
        }

        //#endregion

        //#region Private Functions

        function initEmptyRow() {
            $scope.emptyRow = {
                id: $scope.Id,
                Department: null,
                IsEditing: false
            }

            $scope.Id = $scope.Id + 1;
            $scope.GetDepartments($scope.emptyRow);

        }

        function checkIfHasMainOffice() {
            OfficeService.CheckIfHasMainOffice({
                id: OfficeId === null || OfficeId === undefined || OfficeId === "" ? 0 : OfficeId
            }).then(function (data) {
                $scope.HasMainOffice = data.data;
            }, function (error /*Error event should handle here*/) {
                console.log("Error");
            }, function (data /*Notify event should handle here*/) {
            });
        }

        function checkIfDepartmentExists(department) {
            var result = false;
            for (var i = 0; i < $scope.office.DepartmentData.length; i++) {
                if ($scope.office.DepartmentData[i].DepartmentId === department.DepartmentId) {
                    result = true;
                    break;
                }
            }

            return result
        }

        function getOfficeDetails() {
            OfficeService.GetEditOffices({
                officeId: OfficeId
            }).then(function (data) {
                $scope.office = data.data;
                officeDetails = angular.copy($scope.office);

                $scope.GetProvince($scope.office.ProvinceId);
                $scope.GetCityTown($scope.office.CityTownId);
                $scope.GetBarangay($scope.office.BarangayId);
                $scope.GetPurok($scope.office.PurokId);
                $scope.GetSitio($scope.office.SitioId);
               
            }, function (error /*Error event should handle here*/) {
                console.log("Error");
            }, function (data /*Notify event should handle here*/) {
            });
        }

        //#endregion

    }]);