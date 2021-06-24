MetronicApp.controller(
    'SearchEmployeeMultipleController', ['$scope', '$uibModalInstance', 'LookUpService', 'NgTableParams', '$q', 'CommonService', 'Data',
    function ($scope, $uibModalInstance, LookUpService, NgTableParams, $q, CommonService, Data) {
        $scope.appUsersTable = new NgTableParams({}, { dataset: [] });
        $scope.usersSelected = [];
        $scope.appUserList = [];
        $scope.positions = [];
        $scope.areas = [];
        $scope.dataType = Data.IsEmployee ? "Employee" : "User";

        this.$onInit = function () {

            $scope.sortColumn = "AppUserId";
            $scope.sortOrder = "asc";
            $scope.getAppUsers();
        }

        $scope.close = function () {
            $uibModalInstance.dismiss('cancel');
        };

        $scope.goSelect = function (boolValue) {
            if ($scope.usersSelected.length != 0) {
                $uibModalInstance.close($scope.usersSelected);
            }
            else {
                CommonService.warningMessage("Nothing is selected, please select an employee");
            }
        };

        $scope.getAppUsers = function () {
            var initialSettings = {counts:[],
                getData: function (params) {
                    for (var i in params.sorting()) {
                        $scope.sortColumn = i;
                        $scope.sortOrder = params.sorting()[i];
                    }

                    var d = $q.defer();
                    var filter = params.filter();

                    var searchFilter = {
                        Page: params.page(),
                        PageSize: params.count(),
                        SortDirection: $scope.sortOrder,
                        SortColumn: $scope.sortColumn,
                    };

                    if (Data.IsJO) {
                        LookUpService.SearchAppUserByDivision({
                            filter: searchFilter,
                            name: filter["Name"],
                            office: filter["Area"],
                            position: filter["Position"],
                            departmentId: Data.DepartmentId,
                            divisionId: Data.DivisionId,
                            divisionCategoryId: Data.DivisionCategoryId,
                            userId: Data.UserId
                        }).then(function (data) {
                            loadUserTable(params, d, data);
                        });
                    }
                    else if (Data.IsJOProcess) {
                        LookUpService.SearchAppUserByHeadOfficer({
                            filter: searchFilter,
                            name: filter["Name"],
                            office: filter["Area"],
                            position: filter["Position"],
                            divisionCategoryId: Data.DivisionCategoryId,
                            userId: Data.UserId
                        }).then(function (data) {
                            loadUserTable(params, d, data);
                           
                        });
                    }
                    else {
                        LookUpService.SearchAppUsersModal({
                            filter: searchFilter,
                            name: filter["Name"],
                            office: filter["Area"],
                            position: filter["Position"],
                            isDriver: Data.IsDriver
                        }).then(function (data) {
                            loadUserTable(params, d, data);
                        });
                    }
                    
                    return d.promise;
                }
            };

            $scope.appUsersTable = new NgTableParams($scope.pageSize, initialSettings);
        }
     
        function loadUserTable(params, d, data) {
            params.total(data.filteredRecordCount);
            $scope.totalRecordCount = data.totalRecordCount;
            $scope.dataResult = data.data;
            $scope.filteredRecordCount = data.filteredRecordCount;
            $scope.appUserList = data.data;
            $scope.currentPage = params.page();
            $scope.tableCount = params.count();
            $scope.offSetCount = data.data.length;
            $scope.usersSelected = [];

            for (var i = 0; i <= Data.AppUsers.length - 1; i++) {
                var index = data.data.findIndex(a => a.AppUserId == Data.AppUsers[i].AppUserId);
                if (index != -1) {
                    data.data[index].IsSelected = true;
                    data.data[index].IsCheckedIn = true;
                    $scope.usersSelected.push(data.data[index]);
                }
            }

            for (var i = 0; i <= $scope.usersSelected.length - 1; i++) {
                var index = data.data.findIndex(a => a.AppUserId == $scope.usersSelected[i].AppUserId);
                if (index != -1) {
                    data.data[index].IsSelected = true;
                }
            }

            d.resolve(data.data);
            checkIfAllSelected();
        }
          
        function checkIfAllSelected() {
            for (var i = 0; i <= $scope.appUserList.length - 1; i++) {
                var index = $scope.usersSelected.findIndex(r => r.AppUserId == $scope.appUserList[i].AppUserId);
                if (index == -1) {
                    $scope.selectedAll = false;
                    break;
                }
                else {
                    $scope.selectedAll = true;
                }
            }
        }

        $scope.clear = function () {
            $scope.usersSelected = [];
            checkIfAllSelected();
            $scope.getAppUsers();
        }

        $scope.addRemoveUsers = function (row) {
            var index = $scope.usersSelected.findIndex(r => r.AppUserId === row.AppUserId);
            if (index == -1) {

                $scope.usersSelected.push({ AppUserId: row.AppUserId, FullName: row.FullName, Office: row.Office, Position: row.Position, IsSelected: true, AppUserPhotoThumbnailUrl: row.AppUserPhotoThumbnailUrl})
            }
            else {
                var indexToSetFalse = $scope.appUserList.findIndex(r => r.AppUserId == row.AppUserId);
                $scope.appUserList[indexToSetFalse].IsSelected = false;
                $scope.usersSelected.splice(index, 1);
            }

            checkIfAllSelected();
        }
 
        $scope.selectAllAppUsers = function (selectAll) {
            if (selectAll) {
                for (var i = 0; i <= $scope.appUserList.length - 1; i++) {

                    var index = $scope.usersSelected.findIndex(x => x.AppUserId == $scope.appUserList[i].AppUserId && (x.IsCheckedIn || x.IsSelected));
                    if (index == -1) {
                        $scope.appUserList[i].IsSelected = true;
                        $scope.usersSelected.push({ AppUserId: $scope.appUserList[i].AppUserId, FullName: $scope.appUserList[i].FullName, Office: $scope.appUserList[i].Office, Position: $scope.appUserList[i].Position, IsSelected: true, AppUserPhotoThumbnailUrl: $scope.appUserList[i].AppUserPhotoThumbnailUrl})
                    }
                }
            }
            else {
                for (var i = 0; i <= $scope.appUserList.length - 1; i++) {
                    var index = $scope.usersSelected.findIndex(x => x.AppUserId == $scope.appUserList[i].AppUserId && (x.IsCheckedIn || x.IsSelected));
                    if (index != -1) {
                        $scope.appUserList[i].IsSelected = false;
                        $scope.usersSelected.splice(index, 1);
                    }

                }
            }

            checkIfAllSelected();
        }

        $scope.doubleClick = function (row) {
           
            $scope.addRemoveUsers(row);
            $scope.goSelect();
        }
    }]);