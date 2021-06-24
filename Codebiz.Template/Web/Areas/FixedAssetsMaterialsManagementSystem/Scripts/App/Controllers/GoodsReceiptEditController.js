
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

    .controller('GoodsReceiptEditController', ['$scope', 'GoodsReceiptService', 'NgTableParams', '$uibModal', '$window','hotkeys','CommonService',
        function ($scope, GoodsReceiptService, NgTableParams, $uibModal, $window,hotkeys,CommonService) {

            $scope.receiptTable = new NgTableParams({}, { dataset: [] })
            $scope.dataToInsert = [{}];
            $scope.grandTotal = 0;
            $scope.grandQuantity = 0;
            $scope.copied = 0;
            $scope.supplierId = 0;
            $scope.counter = 0;
            $scope.goodsReceiptId;
            $scope.proceed = true;
            function init() {
                GoodsReceiptService.GetSavedGoodsReceipt()
                    .then(function (data) {
                        $scope.dataToInsert = data.data;
                        for (var i = 0; i <= $scope.dataToInsert.length - 1; i++) {
                            $scope.grandTotal += $scope.dataToInsert[i].TotalCost;
                            $scope.grandQuantity += $scope.dataToInsert[i].Quantity;
                        }
                    },

                        function (error /*Error event should handle here*/) {
                            console.log("Error");
                        },
                        function (data /*Notify event should handle here*/) {
                        });
            }
            init();
     



            $scope.supplierChange = function (supplierId) {
                if ($scope.counter != 0) {
                    swal({
                        title: "Do you want to Continue?",
                        text: "Are you sure to change supplier? Unsaved Data will be lost",
                        type: "warning",
                        showCancelButton: true,
                        confirmButtonColor: "#1ab394",
                        confirmButtonText: "Yes proceed",
                        closeOnConfirm: true
                    }, function () {
                        $scope.supplierId = supplierId;
                        $scope.dataToInsert = [];
                    });
                }
                else {
                    $scope.counter++;
                }
              
            }
            function getGoodsReceiptIdForShortCut() {
                GoodsReceiptService.GetGoodsRecieptIdForShortcutEdit().
                then(function (data) {
                    $scope.goodsReceiptId = data.data
       
                });
            }
            getGoodsReceiptIdForShortCut();

            hotkeys.bindTo($scope).add({
                combo: 'shift+s',
                callback: function () {
                    $scope.saveEditedGoodsReceipt($scope.goodsReceiptId);
                }
            });


            hotkeys.bindTo($scope).add({
                combo: 'shift+c',
                callback: function () {
                    $scope.completeGoodsReceipt($scope.goodsReceiptId, $scope.supplierId);
                }
            });
            $scope.saveEditedGoodsReceipt = function (goodsReceiptId) {
                if ($scope.dataToInsert.length < 1 || $scope.grandQuantity <= 0) {

                    $scope.proceed = false;

                }
                for (var i = 0; i <= $scope.dataToInsert.length - 1; i++) {

                    if ($scope.dataToInsert[i].Quantity < 1 || $scope.dataToInsert[i].Cost < 1) {
                        $scope.proceed = false;
                        break;
                    }
                    else {
                        $scope.proceed = true;
                    }

                }

                if ($scope.proceed == true) {
                    swal({
                        title: "Confirm action.",
                        text: "Are you sure to save this draft?",
                        type: "warning",
                        showCancelButton: true,
                        confirmButtonColor: "#1ab394",
                        confirmButtonText: "Yes proceed",
                        closeOnConfirm: true
                    }, function () {
                        $scope.formSubmitted = true;
                        GoodsReceiptService.EditGoodsReceipt({
                            items: $scope.dataToInsert,
                            goodsReceiptId: goodsReceiptId,
                            totalQuantity: $scope.grandQuantity,
                            totalCost: $scope.grandTotal,
                        }).then(function (data) {
                            $window.location.href = '/GoodsReceipt/SuccessSaveGoodsReceipt';
                        })

                    });
                }
                if ($scope.proceed == false) {
                    CommonService.warningMessage("Can't save nor complete template with no or zero values");
                }
            }

            $scope.completeGoodsReceipt = function (goodsReceiptId, supplierId) {
                if ($scope.dataToInsert.length < 1 || $scope.grandQuantity <= 0) {

                    $scope.proceed = false;

                }
                for (var i = 0; i <= $scope.dataToInsert.length - 1; i++) {

                    if ($scope.dataToInsert[i].Quantity < 1 || $scope.dataToInsert[i].Cost < 1) {
                        $scope.proceed = false;
                        break;
                    }
                    else {
                        $scope.proceed = true;
                        $scope.saveEditedGoodsReceipt(goodsReceiptId);
                    }
                }
                    if ($scope.proceed == true) {
                        swal({
                            title: "Confirm action.",
                            text: "Are you sure to approve this? This action is irreversible",
                            type: "warning",
                            showCancelButton: true,
                            confirmButtonColor: "#1ab394",
                            confirmButtonText: "Yes proceed",
                            closeOnConfirm: true
                        }, function () {

                            GoodsReceiptService.CompleteGoodsReceipt({
                                goodsReceiptId: goodsReceiptId,
                                supplierId: supplierId
                            }).then(function (data) {
                                $window.location.href = '/GoodsReceipt/SuccessSaveGoodsReceipt';
                            });
                        });

                    }
                    if($scope.proceed==false){
                        CommonService.warningMessage("Can't save nor complete template with no or zero values");
                    }
                }
            
            

            $scope.moveQuantityPrice = function (row) {
                if (row.Quantity < 1) {
                    row.hasNegativeQuantityValue = true;
                }
                else {
                    row.hasNegativeQuantityValue = false;
                    computation();
                }
                if (row.Cost < 1) {
                    row.hasNegativeCostValue = true;
                }
                else {
                    row.hasNegativeCostValue = false;
                }
           
            }
            function computation() {
                $scope.grandTotal = 0;
                $scope.grandQuantity = 0;
                for (var i = 0; i <= $scope.dataToInsert.length - 1; i++) {
                    $scope.dataToInsert[i].TotalCost = $scope.dataToInsert[i].Cost * $scope.dataToInsert[i].Quantity;
                    $scope.grandTotal += $scope.dataToInsert[i].TotalCost;
                    $scope.grandQuantity += $scope.dataToInsert[i].Quantity;
                }
            }

            $scope.addItem = function (supplierId) {
                $uibModal.open({
                    animation: true,
                    ariaLabelledBy: 'modal-title',
                    ariaDescribedBy: 'modal-body',
                    templateUrl: 'OrderingModalItem.html',
                    controller: 'ItemDetailsController',
                    controllerAs: 'controller',
                    size: 'lg',
                    keyboard: true,
                    backdrop: "static",
                    windowClass: 'modal_style',
                    resolve: {
                        SupplierId: function () {
                            return supplierId;
                        },
                        PurchaseOrderCode: function () {
                            return 0;
                        },
                        OrderId: function () {
                            return 0;
                        },
                        Items: function () {
                            return $scope.dataToInsert;
                        }
                    }
                }).result.then(function (data) {
                    console.table(data);    
                    for (var i = 1; i <= data.length - 1; i++) {                       
                        $scope.dataToInsert.push({ SupplierId: $scope.dataToInsert[0].SupplierId, SupplierDescription: $scope.dataToInsert[0].SupplierDescription, MasterItemId: data[i].MasterItemId, MasterItemDescription: data[i].Description, Quantity: data[i].Quantity, Cost: parseFloat(data[i].Cost.toFixed(2)), TotalCost: data[i].Cost * data[i].Quantity });
                    }
                    computation();

                }, function () {

                });
            }

            $scope.addItemForCreate = function () {
                $uibModal.open({
                    animation: true,
                    ariaLabelledBy: 'modal-title',
                    ariaDescribedBy: 'modal-body',
                    templateUrl: 'OrderingModalItem.html',
                    controller: 'ItemDetailsController',
                    controllerAs: 'controller',
                    size: 'lg',
                    keyboard: true,
                    backdrop: "static",
                    windowClass: 'modal_style',
                    resolve: {
                        SupplierId: function () {
                            return $scope.supplierId;
                        },
                        PurchaseOrderCode: function () {
                            return 0;
                        },
                        OrderId: function () {
                            return 0;
                        },
                        Items: function () {
                            return $scope.dataToInsert;
                        }
                    }
                }).result.then(function (data) {
                    for (var i = 1; i <= data.length - 1; i++) {
                        //data.splice(0, 1)
                        $scope.dataToInsert.push({ SupplierId: $scope.supplierId, MasterItemId: data[i].MasterItemId, MasterItemDescription: data[i].Description, Quantity: data[i].Quantity, Cost: data[i].Cost, TotalCost: data[i].Cost * data[i].Quantity });
                        console.table($scope.dataToInsert);
                    }
                    computation();

                }, function () {

                });
            }

            $scope.delMaterial = function (row, index) {
                _.remove($scope.receiptTable.settings().dataset,

                    function (item) {
                        return row === item;
                    });
                $scope.dataToInsert.splice(index, 1);
                computation();
            };
           
            $scope.generateNewGoodsReceipt = function () {
                $uibModal.open({
                    animation: true,
                    ariaLabelledBy: 'modal-title',
                    ariaDescribedBy: 'modal-body',
                    templateUrl: 'CreateGoodsReceipt.html',
                    controller: 'CreateGoodsReceiptModalController',
                    controllerAs: 'controller',
                    size: 'lg',
                    keyboard: false,
                    backdrop: "static",
                    windowClass: 'modal_style',

                }).result.then(function (data) {

                }, function () {

                });
            }


        }

    ])
