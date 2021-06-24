angular.module('MetronicApp')
    .controller('ConsumerTypeController',
        ['$scope', 'NgTableParams', '$uibModal', 'ConsumerTypeService', '$q', 'CommonService',
            function ConsumerTypeController($scope, NgTableParams, $uibModal, ConsumerTypeService, $q, CommonService) {
                this.$onInit = function () {
                    $scope.reset();
                };

                // Consumer Type Modal
                $scope.consumerTypeModal = function (consumerTypeId, forDetails) {
                    $uibModal.open({
                        animation: true,
                        ariaLabelledBy: 'modal-title',
                        ariaDescribedBy: 'modal-body',
                        templateUrl: '_ModalPage.html',
                        controller: 'ConsumerTypeAddOrUpdateController',
                        size: 'md',
                        keyboard: false,
                        backdrop: "static",
                        windowClass: 'modal_style',
                        modalOverflow: true,
                        resolve: {
                            ConsumerTypeId: function () {
                                return consumerTypeId;
                            },
                            ForDetails: function () {
                                return forDetails;
                            }
                        }
                    }).result.then(function (data) {
                        $scope.reset();
                    }, function () {

                    });
                };

                $scope.exportDataToExcelFile = function () {
                    if ($scope.resultsLength > 0) {
                        CommonService.showLoading();

                        ConsumerTypeService.ExportDataToExcelFile({
                            SortDirection: $scope.sortOrder,
                            sortColumn: $scope.sortColumn,
                            Name: $scope.name,
                            Code: $scope.code
                        }).then(function (data) {
                            CommonService.hideLoading();
                            window.location.href = document.FileUpload + "DownloadExcelFile?fileName=" + data.data.FileName;

                        }, function (error /*Error event should handle here*/) {
                            console.log("Error");
                        }, function (data /*Notify event should handle here*/) {
                            console.log("Error");
                        });
                    }
                };

                //Search Consumer type
                $scope.search = function () {
                    var initialSettings = {
                        getData: function (params) {
                            for (var i in params.sorting()) {
                                $scope.sortColumn = i;
                                $scope.sortOrder = params.sorting()[i];
                            }
                            var d = $q.defer();

                            ConsumerTypeService.ConsumerTypeIndex({
                                Page: params.page(),
                                PageSize: params.count(),
                                SortDirection: $scope.sortOrder,
                                sortColumn: $scope.sortColumn,
                                Name: $scope.name,
                                Code: $scope.code
                            }).then(function (data) {
                                $scope.resultsLength = data.totalRecordCount;
                                params.total($scope.resultsLength);
                                d.resolve(data.data);
                            });
                            return d.promise;
                        }
                    };
                    $scope.tableParams = new NgTableParams(10, initialSettings);
                };

                //Reset Table and Clear Fields
                $scope.reset = function () {
                    $scope.sortColumn = "CreatedDate";
                    $scope.sortOrder = "desc";
                    $scope.name = "";
                    $scope.code = "";
                    $scope.search();
                };

                //Search when click enter
                $scope.searchWhenEnter = function ($event) {
                    var keyCode = $event.which || $event.keyCode;
                    if (keyCode === 13) {
                        $scope.search();
                    }
                };

                //#region Activate/Deactivate
                $scope.toggleActiveStatus = function (id, status, name) {
                    CommonService.reactivateDeactivate(function () {
                        ConsumerTypeService.ToggleConsumerTypeActiveStatus({ id: id, name: name, status: status })
                            .then(function (data) {
                                if (data.success) {
                                    CommonService.successMessage(data.message);
                                } else {
                                    CommonService.warningMessage(data.message);
                                }
                          
                                $scope.search();

                            }), function (error /*Error event should handle here*/) {
                                console.log(error);
                                CommonService.errorMessage("Unexpected error occured.");
                            }, function (data /*Notify event should handle here*/) {
                            };
                    }, name,status);
                }
                  
                //#endregion

                //Delete Consumer type
                $scope.deleteConsumerType = function (consumerTypeId, name) {

                    CommonService.deleteChanges(function () {
                        ConsumerTypeService.Delete({ consumerTypeId: consumerTypeId, name: name }).then(function (data) {
                            if (data.success) {
                                CommonService.successMessage(data.message);
                            }
                            else {
                                CommonService.warningMessage(data.message);
                            }
                            $scope.search();
                        });
                    }, name);
                };

            }]);