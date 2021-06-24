
angular.module("MetronicApp").controller('PersonnelSearchModalController', ['$scope', '$rootScope', '$q', '$uibModalInstance', 'NgTableParams', 'PersonnelService', 'RankService','CommonService',
    function ($scope, $rootScope, $q, $uibModalInstance, NgTableParams, PersonnelService, RankService, CommonService) {

        var self = this;

        $scope.$on('$viewContentLoaded', function () {
            //App.initComponents(); // init core components
        });

        // set defaults
        $scope.total = 0;
        $scope.page = 1;
        $scope.pageSize = 10;
        $scope.sortColumn = "";
        $scope.sortOrder = "";
        $scope.searchTerm = "";
        $scope.selectedAccountNumber = "";
        $scope.trasactionOrderType = null;
        $scope.currentPersonnel = null;
        $scope.Image = null;
        $scope.errors = [];

        $scope.ranks = [];
        self.ranksFilterLookup = [];

        self.pStatusFilterLookup = [];

        var initialParams = {
            count: $scope.pageSize // initial page size
        };
        var initialSettings = {
            getData: function (params) {

                for (var i in params.sorting()) {
                    $scope.sortColumn = i;
                    $scope.sortOrder = params.sorting()[i];
                }

                var d = $q.defer();
                var filter = angular.copy(params.filter());

                if (!filter.AccountNumber &&
                    !filter.BadgeNumber &&
                    !filter.Rank &&
                    !filter.RankId &&
                    !filter.LastName &&
                    !filter.FirstName &&
                    !filter.MiddleName &&
                    !filter.PStatusId
                    ) {
                    return [];
                }

                filter.page = params.page();
                filter.pageSize = params.count();
                filter.sortColumn = $scope.sortColumn;
                filter.sortOrder = $scope.sortOrder;
                //filter.trasactionOrderType = $scope.trasactionOrderType;

                PersonnelService.FindPersonnels(filter).then(function (data) {
                    console.log(data.data);
                    params.total(data.totalRecordCount);
                    d.resolve(data.data);

                    //_.forEach(data.data, function (value) {
                    //    console.log(value.PersonnelId);
                    //    $scope.DisplayPhoto(value.PersonnelId);
                    //});

                }, function (error /*Error event should handle here*/) {
                    console.log("Error");
                }, function (data /*Notify event should handle here*/) {
                });

                return d.promise;
            }
        };

       $scope.DisplayPhoto = function (id) {
       
           PersonnelService.PersonnelPhoto({
               personnelId:id
           }).then(function (data) {
               console.log(data);
               //$scope.Image = data.Image;
               return data.Image;
           }, function (error /*Error event should handle here*/) {
               console.log("Error");
           }, function (data /*Notify event should handle here*/) {
           });
       }


       //function _arrayBufferToBase64(buffer) {
       //    var binary = '';
       //    var bytes = new Uint8Array(buffer);
       //    var len = bytes.byteLength;
       //    for (var i = 0; i < len; i++) {
       //        binary += String.fromCharCode(bytes[i]);
       //    }
       //    return window.btoa(binary);
       //}

        $scope.tableParams = new NgTableParams(initialParams, initialSettings);

        $scope.setPersonnel = function (accountNumber, item, $event) {
            //$($event.target).parent("tr").addClass('active');
            //console.log(item);
            if (item.CanBeSelected){// || item.CanBeResigned) {
                $scope.currentPersonnel = item;
                $scope.errors = [];

                $scope.selectedAccountNumber = accountNumber;
                var dsdsd = $scope.tableParams.data;
                _.forEach($scope.tableParams.data, function (value) {
                    value.IsSelected = false;
                    if (value.AccountNumber === accountNumber) {
                        value.IsSelected = true;
                    }
                });
            }

            if(item.CanBeSelected === false) {
                $scope.errors.push("INACTIVE Status Personnels cannot be selected.");
            }
            //if (item.CanBeResign === false) {
            //    $scope.errors.push("Personnel must have to rendered his service due of recent STUDY LEAVE. Personnels cannot be selected.");
            //}
        }

        $scope.getRankFilterLookup = function () {
            if (self.ranksFilterLookup && self.ranksFilterLookup.length > 0) {
                return self.ranksFilterLookup;
            }

            var d = $q.defer();
            RankService.GetAllActiveRanks().then(function (data) {

                $scope.ranks = data.data;

                self.ranksFilterLookup = [];
                _.forEach($scope.ranks, function (value) {
                    self.ranksFilterLookup.push({ id: value.RankId, title: value.Code });
                });

                d.resolve(self.ranksFilterLookup);
            });
            return d;
        }

        $scope.getPStatusFilterLookup = function () {
            if (self.pStatusFilterLookup && self.pStatusFilterLookup.length > 0) {
                return self.pStatusFilterLookup;
            }

            var d = $q.defer();
            CommonService.GetPStatusLookup().then(function (data) {

                var data = data.data;

                self.pStatusFilterLookup = [];
                _.forEach(data, function (value) {
                    self.pStatusFilterLookup.push({ id: value.Key, title: value.Value });
                });

                d.resolve(self.pStatusFilterLookup);
            });
            return d;
        }

        //$scope.selectPersonnel = function (accountNumber, item) {
        //    console.log("selecting...");
        //    //$scope.currentPersonnel = item || $scope.currentPersonnel;

        //    if ($scope.currentPersonnel != null && $scope.currentPersonnel.CanBeSelected) {
        //        $uibModalInstance.close(accountNumber);
        //    } else {
        //        $scope.errors.push("INACTIVE Personnel cannot be selected.");
        //    }
        //    console.log("errors...");
        //    console.log($scope.errors);
        //}


        $scope.selectPersonnel = function (item) {
            console.log("selecting...");
            //$scope.currentPersonnel = item || $scope.currentPersonnel;

            if ($scope.currentPersonnel != null && $scope.currentPersonnel.CanBeSelected) {
                $uibModalInstance.close(item);
            } else {
                $scope.errors.push("INACTIVE Personnel cannot be selected.");
            }
            console.log("errors...");
            console.log($scope.errors);
        }

        $scope.clearFilter = function () {
            $scope.tableParams.filter({});
        }

        $scope.cancel = function (accountNumber) {
            $uibModalInstance.dismiss('cancel');
        }
    }]);
