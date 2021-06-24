
angular.module("MetronicApp")
    //File Upload Configuration
    .config([
           '$httpProvider', 'fileUploadProvider',
            function ($httpProvider, fileUploadProvider) {
                delete $httpProvider.defaults.headers.common['X-Requested-With'];
                fileUploadProvider.defaults.redirect = window.location.href.replace(
                    /\/[^\/]*$/,
                    '/cors/result.html?%s'
                );

                angular.extend(fileUploadProvider.defaults, {
                    // Enable image resizing, except for Android and Opera,
                    // which actually support image resizing, but fail to
                    // send Blob objects via XHR requests:
                    //disableImageResize: /Android(?!.*Chrome)|Opera/
                    //    .test(window.navigator.userAgent),
                    autoUpload: true,
                    maxFileSize: 20000000,//20MB
                    acceptFileTypes: /(\.|\/)(jpe?g|png|mp4|docx|pdf)$/i
                });
            }
    ])

    .controller('OrderingController', ['$scope', '$q', 'OrderingService', 'NgTableParams','$uibModal', '$window','hotkeys','CommonService',
            function ($scope, $q, OrderingService, NgTableParams, $uibModal, $window,hotkeys,CommonService) {
                //Table Initialize 
        
                $scope.orderId;
                $scope.datePicker = { date: { startDate: null, endDate: null } };

                $scope.search = function () {
      
                    var initialSettings = {
                        getData: function (params) {
                            for (var i in params.sorting()) {
                                $scope.sortColumn = i;
                                $scope.sortOrder = params.sorting()[i];
                            }
                            var d = $q.defer();

                            if ($scope.datePicker.date.startDate != null) {
                                OrderingService.SearchOrder({
                                    page:params.page(),
                                    pageSize: params.count(),
                                    sortOrder: $scope.sortOrder,
                                    sortColumn: $scope.sortColumn,
                                    orderCode: $scope.orderCode,
                                    status: $scope.status,
                                    createdBy: $scope.createdBy,
                                    dateFrom: $scope.datePicker.date.startDate._d,
                                    dateTo: $scope.datePicker.date.endDate._d
                                }).then(function (data) {
                                    params.total(data.totalRecordCount);
                                    d.resolve(data.data);
                                });
                                return d.promise;
                            }
                            else {
                                OrderingService.SearchOrder({
                                    page: 1,
                                    pageSize: 10,
                                    sortOrder: $scope.sortOrder,
                                    sortColumn: $scope.sortColumn,
                                    orderCode: $scope.orderCode,
                                    status: $scope.status,
                                    createdBy: $scope.createdBy,
                                }).then(function (data) {
                                    params.total(data.totalRecordCount);
                                    d.resolve(data.data);
                                });
                                return d.promise;
                            }
                            $scope.tableParamsOrder = new NgTableParams(10, initialSettings);
                        }
                    };
                }
                    $scope.search();
         
                $scope.printPurchaseOrder = function (purchaseOrderCode,supplierId,orderId) {
                    $uibModal.open({
                        animation: true,
                        ariaLabelledBy: 'modal-title',
                        ariaDescribedBy: 'modal-body',
                        templateUrl: 'PurchaseOrderViewerModal.html',
                        controller: 'ItemDetailsController',
                        controllerAs:'controller',
                        size: 'lg',
                        keyboard: true,
                        backdrop: "static",
                        windowClass: 'app-modal-window',
                        resolve: {
                            PurchaseOrderCode: function () {
                                return purchaseOrderCode;
                            },
                            SupplierId: function () {
                                return supplierId;
                            },
                            OrderId: function () {
                                return orderId;
                            },
                            Items: function () {
                                return $scope.dataToInsert;
                            }
                        }

                    }).result.then(function (data) {

                    }, function () {

                    });
                
                }

    
 ]);


