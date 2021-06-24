
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
.controller('GoodsReceiptIndexController', ['$scope', 'GoodsReceiptService', 'NgTableParams', '$uibModal','$q',
        function ($scope, GoodsReceiptService, NgTableParams, $uibModal,$q) {
            $scope.tableParamsGoodsReceipt = new NgTableParams({}, { dataset: [] });
            $scope.editUrl = '@Url.Action("Edit","GoodsReceipt", new { goodsReceiptId = "goodsReceiptIdJson" })';
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
                            GoodsReceiptService.SearchGoodsReceipt({
                                page: params.page(),
                                pageSize: params.count(),
                                sortOrder: $scope.sortOrder,
                                sortColumn: $scope.sortColumn,
                                goodsReceiptCode: $scope.goodsReceiptCode,
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
                            GoodsReceiptService.SearchGoodsReceipt({
                                page: 1,
                                pageSize: 10,
                                sortOrder: $scope.sortOrder,
                                sortColumn: $scope.sortColumn,
                                goodsReceiptCode: $scope.goodsReceiptCode,
                                status: $scope.status,
                                createdBy: $scope.createdBy,
                            }).then(function (data) {
                                params.total(data.totalRecordCount);
                                d.resolve(data.data);
                            });
                            return d.promise;
                        }
                       
                    }
                };
                $scope.tableParamsGoodsReceipt = new NgTableParams(10, initialSettings);
            }
            $scope.search();

            $scope.reset = function () {
                $scope.status = "";
                $scope.createdBy = "";
                $scope.goodsReceiptCode = "";
                $scope.datePicker.date = "";
                $scope.datePicker.date.startDate = null;
                $scope.datePicker.date.endDate = null;
                $scope.search();
            }

            

            $scope.sendtoAccountsPayable = function (goodsReceiptId,totalQuantity,totalCost,goodsReceiptCode) {               
                GoodsReceiptService.GetItemsForForward({
                    goodsReceiptId: goodsReceiptId
                }).then(function (data) {
                    $uibModal.open({
                        animation: true,
                        ariaLabelledBy: 'modal-title',
                        ariaDescribedBy: 'modal-body',
                        templateUrl: 'GoodsReceiptForwardedModal.html',
                        controller: 'GoodsReceiptForwardController',
                        controllerAs: 'controller',
                        size: 'lg',
                        keyboard: true,
                        backdrop: "static",
                        windowClass: 'modal_style',
                        resolve: {
                            GoodsReceiptCode: function () {
                                return goodsReceiptCode;
                            },
                            Items: function () {
                                return data.data;
                            },
                            SupplierDescription: function () {
                                return data.data[0].SupplierDescription;
                            },
                            GoodsReceiptId:function(){
                                return goodsReceiptId;
                            }

                        }
                    }).result.then(function (data) {
                    }, function () {
                    });
                });
      
            }

        }
]);
