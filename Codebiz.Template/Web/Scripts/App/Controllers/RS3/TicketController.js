MetronicApp.controller('TicketIndexController', ['$scope', 'TicketService', 'CommonService', '$window', '$timeout','NgTableParams','$q',
    function ($scope, TicketService, CommonService, $window, $timeout, NgTableParams,$q) {
        $scope.priorities = PRIORITIES;
        $scope.ticketStatuses = TICKETSTATUSES;
        this.$onInit = function () {
            $scope.reset();
        }
        $scope.reset = function () {
            $scope.f = {
                TicketNo: "",
                Technician: "",
                Client: "",
                Priority: null,
                CreatedBy: "",
                CreatedOnFrom: null,
                Status: null,
                SortDirection: 'desc',
                SortColumn: 'CreatedOn',
            };
            $scope.createdDate = null;
            $scope.search();
        }
        $scope.search = function () {
            var initialSettings = {
                getData: function (params) {
                    for (var i in params.sorting()) {
                        $scope.sortColumn = i;
                        $scope.sortOrder = params.sorting()[i];
                    }
                    var d = $q.defer();
                    $scope.f.Page = params.page();
                    $scope.f.PageSize = params.count();
                    $scope.f.SortDirection = $scope.sortOrder == null ? 'desc' : $scope.sortOrder;
                    $scope.f.SortColumn = $scope.sortColumn == null ? 'CreatedOn' : $scope.sortColumn;
                    $scope.f.CreatedOnFrom = getDateRangePickerValue(1, $scope.createdDate);
                    $scope.f.CreatedOnTo = getDateRangePickerValue(2, $scope.createdDate);
                    TicketService.Search({
                        filter: $scope.f
                    }).then(function (data) {
                        $scope.resultsLength = data.totalRecordCount;
                        params.total(data.totalRecordCount);
                        d.resolve(data.result);
                    });
                    return d.promise;
                }
            };
            $scope.tableParams = new NgTableParams(10, initialSettings);
        }
        $scope.searchWhenEnter = function ($event) {
            var keyCode = $event.which || $event.keyCode;
            if (keyCode === 13) {
                $scope.search();
            }
        }
    }]);
MetronicApp.controller('TicketAddOrUpdateController', ['$scope', 'TicketService', 'CommonService', '$window', '$timeout', 'NgTableParams', '$q',
    function ($scope, TicketService, CommonService, $window, $timeout, NgTableParams, $q) {
        $scope.priorities = PRIORITIES;
        $scope.options = {
            url: document.FileUpload + "UploadTicketAttachments"
        };
        this.$onInit = function () {
          
        }
        $scope.save = function () {
            $scope.formSubmitted = true;
            if ($scope.f.$valid) {
                CommonService.saveOrUpdateChanges(function () {
                    $scope.m.Attachments = $scope.queue;
                    TicketService.AddOrUpdate({ viewModel: $scope.m }).then(function (d) {
                        if (d.Success) {
                            CommonService.successMessage(d.Message);
                            $timeout(function () {
                                window.location.href = document.Ticket;
                            }, 1000);
                        }
                        else {
                            CommonService.warningMessage(d.Message);
                        }
                    })
                }, $scope.m.Id == null ? 0 : $scope.m.Id);
            }
        }
        $scope.guessChange = function () {
            if (!$scope.IsGuess) {
                $scope.m.Address = "";
                $scope.m.Client = "";
                $scope.m.ClientEmail = "";
            }
        }
        
    }]);