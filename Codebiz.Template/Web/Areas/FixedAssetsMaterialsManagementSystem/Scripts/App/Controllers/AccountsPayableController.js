
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

    .controller('AccountsPayableController', ['$scope', '$q', 'AccountsPayableService', 'NgTableParams',
        function ($scope, $q, AccountsPayableService, NgTableParams) {
            $scope.tableParamsAccountsPayable = new NgTableParams({}, { dataset: [] });

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
                            AccountsPayableService.SearchAccountsPayable({
                                page: params.page(),
                                pageSize: params.count(),
                                sortOrder: $scope.sortOrder,
                                sortColumn: $scope.sortColumn,
                                accountsPayableCode: $scope.accountsPayableCode,
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
                            AccountsPayableService.SearchAccountsPayable({
                                page: 1,
                                pageSize: 10,
                                sortOrder: $scope.sortOrder,
                                sortColumn: $scope.sortColumn,
                                accountsPayableCode: $scope.accountsPayableCode,
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
                $scope.tableParamsAccountsPayable = new NgTableParams(10, initialSettings);
            }
            $scope.search();

            $scope.reset = function () {
                $scope.accountsPayableCode = "";
                $scope.goodsReceiptCode = "";
                $scope.status = "";
                $scope.createdBy = "";
                $scope.search();
            }
        }
    ]);


