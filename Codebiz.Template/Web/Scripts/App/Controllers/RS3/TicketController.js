MetronicApp.controller('TicketIndexController', ['$scope', 'TicketService', 'CommonService', '$window', '$timeout','NgTableParams','$q',
    function ($scope, TicketService, CommonService, $window, $timeout, NgTableParams, $q) {
     
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
        $scope.viewTicket = function (id) {
            window.location.href = document.Ticket + 'ViewTicket?Id=' + id;
        }
    }]);
MetronicApp.controller('TicketAddOrUpdateController', ['$scope', 'TicketService', 'CommonService', '$window', '$timeout', 'NgTableParams', '$q', '$uibModal', '$controller', '$location',
    function ($scope, TicketService, CommonService, $window, $timeout, NgTableParams, $q, $uibModal, $controller,$location) {
        $controller('SupportingDocumentController', { $scope: $scope });
        $scope.withdocumentType = false;
      
        $scope.options = {
            url: document.FileUpload + "UploadTicketAttachments"
        };
        this.$onInit = function () {
            //console.log($location.search().Id )
            $scope.priorities = PRIORITIES;
            $scope.isUpdate = $location.search().Id != null;
            TicketService.GetTicketDetailsById({ id: $location.search().Id }).then(function (d) {
             
                $scope.m = d.result;
                $scope.queue = d.result.Attachments;
             //   console.log(d.result.Attachments)
                if ($scope.m.ClientId == null) {
                    $scope.IsGuess = true;
                }
            });
        }
        $scope.m = {
            ClientId: null,
            TechnicianId:null
        }
        $scope.searchUser = function (isClient) {
            var modalData = {
                LookupType: 'APU',
                RoleId: isClient ? 4 : 3, //client //tech
                Module: isClient ? 'CLIENTS' : 'TECHNICIAN'
            }
            $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: `${document.baseUrlNoArea}ChooseFromList/GetLookup?objType=${modalData.LookupType}`,
                controller: 'ChooseFromListController',
                size: 'md',
                keyboard: false,
                backdrop: "static",
                windowClass: 'modal_style',
                modalOverflow: true,
                resolve: {
                    Data: function () {
                        return modalData;
                    },
                }
            }).result.then(function (data) {
                if (isClient) {
                    $scope.m.ClientId = data.Id;
                    $scope.m.ClientEmail = data.Email;
                    $scope.m.Client = data.Name;
                }
                else {
                    $scope.m.TechnicianId = data.Id;
                    $scope.m.TechnicianEmail = data.Email;
                    $scope.m.Technician = data.Name;
                }
            });

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
        $scope.cancelChanges = function () {
            if (!$scope.f.$pristine) {
                CommonService.cancelChanges(function () {
                    window.location.href = document.Ticket;
                })
            }
            else {
                window.location.href = document.Ticket;
            }
        }
        $scope.guessChange = function () {
            $scope.m.Address = "";
            $scope.m.Client = "";
            $scope.m.ClientEmail = "";
        }
        $scope.openSupportingDocumentPreview = function (thumbnailUrl, name, url, isPdf, isWord) {
            //console.log(url)
            if (isPdf || isWord) {
                $window.open(url, '_blank');
            }
            else {
                $uibModal.open({
                    animation: true,
                    ariaLabelledBy: 'modal-title',
                    ariaDescribedBy: 'modal-body',
                    templateUrl: 'SupportingDocumentsPreviewModal.html',
                    controller: 'SupportingDocumentsPreviewController',
                    size: 'md',
                    keyboard: false,
                    backdrop: "static",
                    windowClass: 'modal_style',
                    modalOverflow: true,
                    resolve: {
                        thumbnailUrl: function () {
                            return thumbnailUrl;
                        },
                        name: function () {
                            return name;
                        }
                    }
                }).result.then(function (data) {
                }, function () {

                });
            }
        }
    }]);
MetronicApp.controller('TicketViewController', ['$scope', 'TicketService', 'CommonService', '$window', '$timeout', 'NgTableParams', '$q', '$uibModal', '$controller','$location',
    function ($scope, TicketService, CommonService, $window, $timeout, NgTableParams, $q, $uibModal, $controller, $location) {
        $controller('SupportingDocumentController', { $scope: $scope });

        $scope.priorities = PRIORITIES;
        $scope.options = {
            url: document.FileUpload + "UploadTicketAttachments"
        };
    
    }]);