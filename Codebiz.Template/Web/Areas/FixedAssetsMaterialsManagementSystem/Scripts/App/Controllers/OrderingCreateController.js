
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

    .controller('OrderingCreateController', ['$scope', '$http', 'OrderingService', 'NgTableParams', '$uibModal', '$window', 'hotkeys', 'CommonService',
            function ($scope, $http, OrderingService, NgTableParams, $uibModal, $window, hotkeys, CommonService) {
                //Table Initialize 
                var indexToEdit = 0;
                $scope.supplierTable = new NgTableParams({}, { dataset: [] })
                $scope.dataToInsert = [{}];
                $scope.grandTotal = 0;
                $scope.grandQuantity = 0;
                $scope.orderId;
                var indexer = 0;


                function getSavedOrders() {
                    OrderingService.GetSavedOrders()
                        .then(function (data) {
                            $scope.dataToInsert = data.data;
                            $scope.dataToInsert.push({});
                            indexer = data.data.length - 1;
                            $scope.grandTotal = 0;
                            $scope.grandQuantity = 0;

                            for (var i = 0; i < $scope.dataToInsert.length - 1; i++) {
                                $scope.grandTotal += $scope.dataToInsert[i].TotalCost;
                                $scope.grandQuantity += $scope.dataToInsert[i].Quantity;
                            }
                            $scope.orderId = data.data[0].OrderId;
                            if ($scope.orderId == null || $scope.orderId == 0) {
                                OrderingService.GetMaxOrderId()
                                    .then(function (data) {
                                        $scope.orderId = data.data;
                                    });
                            }
                        });
                }
                getSavedOrders();
                $scope.getSupplier = function () {
                    OrderingService.GetSuppliers()
                        .then(function (data) {
                            $scope.mapSupplier = data.data;
                        },
                        function (error /*Error event should handle here*/) {

                        },
                        function (data /*Notify event should handle here*/) {
                        });
                }
                $scope.getSupplier();


                function getOrderIdForShortcut() {
                    OrderingService.GetOrderIdForShortCut().
                        then(function (data) {
                            $scope.orderId = data.data;
                        });
                }
                getOrderIdForShortcut();


                hotkeys.bindTo($scope).add({
                    combo: 'shift+s',
                    callback: function () {
                        $scope.savetoDB($scope.orderId);
                    }
                });


                hotkeys.bindTo($scope).add({
                    combo: 'shift+c',
                    callback: function () {
                        $scope.CompleteOrder($scope.orderId);
                    }
                });
                var hasNegative = false;

                function checkNegativeValues() {
                    for (var i = 0; i <= $scope.dataToInsert.length - 1; i++) {
                        if ($scope.dataToInsert[i].Quantity < 0) {
                            hasNegative = true;
                            break;
                        }
                    }
                }

                $scope.CompleteOrder = function (orderId) {
                    checkNegativeValues();
                    if ($scope.dataToInsert.length == 1 || $scope.grandQuantity == 0) {
                        CommonService.warningMessage("Can't save nor complete empty template or no quantity order");
                    }
                    else if (hasNegative == true) {
                        CommonService.warningMessage("Can't save nor complete order that has negative quantity");
                        hasNegative = false;
                    }

                    else {


                        swal({
                            title: "Confirm action.",
                            text: "Are you sure to complete this draft? This action is irreversible",
                            type: "warning",
                            showCancelButton: true,
                            confirmButtonColor: "#1ab394",
                            confirmButtonText: "Yes proceed",
                            closeOnConfirm: true
                        }, function () {
                            OrderingService.SaveOrder({
                                data: $scope.dataToInsert,
                                orderId: orderId
                            }).then(function (data) {
                                toastr["warning"]("Email Sending", "Purchase Order");
      
                                CommonService.showLoading();
                                OrderingService.CompleteOrder({
                                    orderId: orderId
                                }).then(function (data) {

                                    $window.location.href = '/Ordering/SuccessfulComplete';
                                });
                            });

                        });
                    }
                }



                $scope.savetoDB = function (orderId) {
                    checkNegativeValues();
                    if ($scope.dataToInsert.length == 1 || $scope.grandQuantity == 0) {
                        CommonService.warningMessage("Can't save nor complete empty template or no quantity order");
                    }
                    else if (hasNegative == true) {
                        CommonService.warningMessage("Can't save nor complete order that has negative quantity");
                        hasNegative = false;
                    }
                    else {
                        swal({
                            title: "Confirm action.",
                            text: "Are you sure to save this draft?",
                            type: "warning",
                            showCancelButton: true,
                            confirmButtonColor: "#1ab394",
                            confirmButtonText: "Yes proceed",
                            closeOnConfirm: true
                        }, function () {
                            //$("#formLoadingScreen").modal("show");

                            OrderingService.SaveOrder({
                                data: $scope.dataToInsert,
                                orderId:  $scope.orderId
                            }).then(function (data) {
                                CommonService.showLoading();
                                $window.location.href = '/Ordering/SuccessfulSave';

                            });
                        });
                    }

                }



                $scope.cancelOrder = function (orderId) {
                    swal({
                        title: "Confirm action.",
                        text: "Are you sure to cancel this order?",
                        type: "warning",
                        showCancelButton: true,
                        confirmButtonColor: "#1ab394",
                        confirmButtonText: "Yes proceed",
                        closeOnConfirm: true
                    }, function () {
                        $("#formLoadingScreen").modal("show");
                        OrderingService.CancelOrder({
                            data: $scope.dataToInsert,
                            orderId: orderId
                        }).then(function (data) {

                            $window.location.href = '/Ordering/SuccessfulSave';

                        });
                    });
                }

                $scope.getItems = function (row) {
                    $uibModal.open({
                        animation: true,
                        ariaLabelledBy: 'modal-title',
                        ariaDescribedBy: 'modal-body',
                        templateUrl: 'OrderingModalItem.html',
                        controller: 'ItemDetailsController',
                        controllerAs: 'controller',
                        size: 'lg',
                        keyboard: false,
                        backdrop: "static",
                        windowClass: 'modal_style',
                        resolve: {
                            SupplierId: function () {
                                return row.SupplierId;
                            },
                            PurchaseOrderCode: function () {
                                return 0;
                            },
                            Items: function () {
                                return $scope.dataToInsert;
                            },
                            OrderId: function () {
                                return $scope.orderId
                            }

                        }
                    }).result.then(function (data) {
                     
                        $scope.dataToInsert.splice(indexer, 1);
             
                        for (var i = 1; i <= data.length - 1; i++) {
                            $scope.dataToInsert.push({ SupplierId: data[i].SupplierId, MasterItemId: data[i].MasterItemId, MasterItemDescription: data[i].Description, BrandId:data[i].BrandId, BrandName:data[i].BrandName, Cost: parseFloat(data[i].Cost.toFixed(2)), Quantity: data[i].Quantity, TotalCost: data[i].Cost });
                            indexer++;
                        }

                
                        $scope.dataToInsert.push({});
                        $scope.grandTotal = 0;
                        $scope.grandQuantity = 0;
                        for (var i = 0; i < $scope.dataToInsert.length - 1; i++) {
                            $scope.grandTotal += $scope.dataToInsert[i].TotalCost;
                            $scope.grandQuantity += $scope.dataToInsert[i].Quantity;
                        }
                        $scope.getItems(row);
                    }, function () {
                   
                      
                    });
                };



                $scope.getTotalCost = function (row) {
                    row.TotalCost = row.Quantity * row.Cost;
                    $scope.grandQuantity = 0;
                    $scope.grandTotal = 0;
                    $scope.dataToInsert[indexToEdit] = ({ SupplierId: row.SupplierId, MasterItemId: row.MasterItemId, MasterItemDescription: row.MasterItemDescription, Cost: row.Cost, Quantity: row.Quantity, TotalCost: row.TotalCost });
                    for (var i = 0; i < $scope.dataToInsert.length - 1; i++) {
                        $scope.grandTotal += $scope.dataToInsert[i].TotalCost;
                        $scope.grandQuantity += $scope.dataToInsert[i].Quantity;
                    }
                };


                $scope.getIndexOfRow = function (index) {
                    indexToEdit = index;
                }


                $scope.delMaterial = function (row, index) {
                  
                    _.remove($scope.supplierTable.settings().dataset,

                        function (item) {
                            return row === item;
                        });
            
                    $scope.grandQuantity = 0;
                    $scope.grandTotal = 0;
                    indexer--;
                    $scope.dataToInsert.splice(index, 1);
                    $scope.supplierTable.reload().then(function (data) {
                        if (data.length === 0 && $scope.supplierTable.total() > 0) {
                            $scope.supplierTable.page($scope.supplierTable.page() - 1);
                            $scope.supplierTable.reload();
                        }
                    });
                    for (var i = 0; i < $scope.dataToInsert.length - 1; i++) {
                        $scope.grandTotal += $scope.dataToInsert[i].TotalCost;
                        $scope.grandQuantity += $scope.dataToInsert[i].Quantity;
                    }
                };

                $scope.getItemDetails = function (supplierId) {
                    $uibModal.open({
                        animation: true,
                        ariaLabelledBy: 'modal-title',
                        ariaDescribedBy: 'modal-body',
                        templateUrl: 'OrderingModalItem.html',
                        controller: 'ItemDetailsController',
                        controllerAs: 'controller',
                        size: 'lg',
                        keyboard: false,
                        backdrop: "static",
                        windowClass: 'modal_style',
                        resolve: {
                            SupplierId: function () {
                                return supplierId;
                            },


                        }
                    }).result.then(function (data) {

                    }, function () {

                    });
                }
            }
    ]);


