angular.module('MetronicApp')

    .controller('AuditLogsController',
        ['$scope', '$http', 'AuditLogsServices', 'NgTableParams', '$uibModal', '$window', '$timeout', '$q', 'CommonService',
            function ($scope, $http, AuditLogsServices, NgTableParams, $uibModal, $window, $timeout, $q, CommonService) {
                //Other defaults
                $scope.createdOnDatePicker = {
                    opened: false
                };
                //Init
                this.$onInit = function () {
                    $scope.search(true);
                    getEventTitles();
                },
                    //#region open save/update modal
                    $scope.openAuditLogDetails = function (Id) {
                        $uibModal.open({
                            animation: true,
                            ariaLabelledBy: 'modal-title',
                            ariaDescribedBy: 'modal-body',
                            templateUrl: 'DetailsModal.html',
                            controller: 'AuditLogDetailsController',
                            controllerAs: 'controller',
                            size: 'xlg',
                            keyboard: false,
                            backdrop: "static",
                            windowClass: 'modal_style',
                            resolve: {
                                LogId: function () {
                                    return Id;
                                }
                            }
                        }).result.then(function (data) {
                            $scope.reset();
                        },
                            function () {

                            });
                    },
                    //#endregion
                    //#region View
                    $scope.fileUploads = [];
                $scope.search = function (init) {
                    var initialSettings = {
                        getData: function (params) {
                            for (var i in params.sorting()) {
                                $scope.sortColumn = i;
                                $scope.sortOrder = params.sorting()[i];
                            }
                            var d = $q.defer();
                            var filter = params.filter();

                            AuditLogsServices.SearchAuditLogs({
                                LogId: $scope.LogId,
                                ActivityId: $scope.ActivityId,
                                User: $scope.User,
                                Level: $scope.Level,
                                Thread: $scope.Thread,
                                Exception: $scope.Exception,
                                Message: $scope.Message,
                                LogEventTitleId: $scope.eventTitle,
                                page: params.page(),
                                pageSize: params.count(),
                                sortOrder: $scope.sortOrder,
                                sortColumn: $scope.sortColumn,
                                IsActive: $scope.IsActive,
                                CreatedBy: $scope.CreatedBy,
                                createdOnFrom: getDateRangePickerValue(1, $scope.createdDate),
                                createdOnTo: getDateRangePickerValue(2, $scope.createdDate),
                            }).then(function (data) {
                                params.total(data.totalRecordCount);
                                for (var i = 0; i <= data.data.length - 1; i++) {
                                    data.data[i].FileUploads = [];
                                    data.data[i].FileNames = [];
                                    data.data[i].FileUploads.push(data.data[i].FileUploadAttachments == null ? '' : data.data[i].FileUploadAttachments.split(','));
                                    data.data[i].FileNames.push(data.data[i].Attachments == null ? '' : data.data[i].Attachments.split(','));
                                }
                                //for (var i = 0; i <= data.data.length - 1; i++) {
                                //    data.data[i].ChangesLog = [];
                                //    data.data[i].ChangesLog.push(data.data[i].Changes == null ? '' : data.data[i].Changes.split(','));
                                //}
                                d.resolve(data.data);
                            });
                            return d.promise;
                        }
                    };

                    $scope.tableParams = new NgTableParams(10, initialSettings);

                },

                    //Reset filter fields
                    $scope.reset = function () {
                        $scope.ActivityId = "";
                        $scope.User = "";
                        $scope.Level = null;
                        $scope.Thread = "";
                        $scope.Exception = "";
                        $scope.LogId = "";
                        $scope.Message = "";
                        $scope.CreatedBy = "";
                        $scope.createdDate = null;
                        $scope.eventTitle = null;
                        $scope.sortOrder = null;
                        $scope.sortColumn = null;

                        $scope.search(true);
                    },

                    $scope.searchWhenEnter = function ($event) {
                        var keyCode = $event.which || $event.keyCode;
                        if (keyCode === 13) {
                            $scope.search();
                        }
                    };
                $scope.filePreview = function (url, name) {
                    var isPdfOrWord = name.substring(name.length, name.length - 3) == 'pdf' || (name.substring(name.length, name.length - 4) == 'docx' || name.substring(name.length, name.length - 3) == 'doc');
                    if (isPdfOrWord) {
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
                                    return url;
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
                $scope.seeMore = function (changes) {
                    $uibModal.open({
                        animation: true,
                        ariaLabelledBy: 'modal-title',
                        ariaDescribedBy: 'modal-body',
                        templateUrl: 'SeeMoreView.html',
                        controller: 'AuditLogSeeMoreController',
                        controllerAs: 'controller',
                        size: 'xlg',
                        keyboard: false,
                        backdrop: "static",
                        windowClass: 'modal_style',
                        resolve: {
                            Changes: function () {
                                return changes;
                            }
                        }
                    }).result.then(function (data) {
                    });
                }

                function getEventTitles() {
                    AuditLogsServices.GetEventTitles({
                    }).then(function (data) {
                        $scope.eventTitles = data.data;
                    }, function (error /*Error event should handle here*/) {
                        console.log("Error");
                    }, function (data /*Notify event should handle here*/) {
                    });
                }

            }])
    .controller('AuditLogSeeMoreController',
        ['$scope', '$http', 'AuditLogsServices', 'NgTableParams', '$uibModal', '$window', 'Changes', '$q', '$uibModalInstance',
            function ($scope, $http, AuditLogsServices, NgTableParams, $uibModal, $window, Changes, $q, $uibModalInstance) {
                $scope.Changes = angular.fromJson(Changes);
                for (key in $scope.Changes) {
                    var obj = $scope.Changes[key];
                    if (obj != null) {
                        if (obj.length != null) {
                            if (angular.isArray(obj)) {
                                obj.IsArray = true;
                            }
                        }
                        
                    }
                }
                $scope.cancel = function () {
                    $uibModalInstance.dismiss();
                }

            }])
