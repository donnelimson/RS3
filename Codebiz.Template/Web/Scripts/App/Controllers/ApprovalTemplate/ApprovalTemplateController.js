MetronicApp.controller('ApprovalTemplateController', ['$scope', '$q', 'NgTableParams', 'CommonService', 'ApprovalTemplateService','$uibModal','$window',
    function ($scope, $q, NgTableParams, CommonService, ApprovalTemplateService, $uibModal,$window) {
        $scope.approvalTemplateTable = new NgTableParams({}, { dataset: [] });
        $scope.resultsLength = 0;

        this.$onInit = function () {
            $scope.reset();
        };

        $scope.searchWhenEnter = function ($event) {
            var keyCode = $event.which || $event.keyCode;
            if (keyCode === 13) {
                $scope.search();
            }
        };
        $scope.exportDataToExcelFile = function () {
            if ($scope.resultsLength > 0) {
                CommonService.showLoading();
                ApprovalTemplateService.ExportDataToExcelFile({
                    SortDirection: $scope.sortOrder,
                    SortColumn: $scope.sortColumn,
                    Name: $scope.name,
                    CreatedOnFrom: getDateRangePickerValue(1, $scope.createdDate),
                    CreatedOnTo: getDateRangePickerValue(2, $scope.createdDate)
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
        $scope.create = function () {
            CommonService.showLoading();
            $window.location.href = document.ApprovalTemplate + "Create";
        }
        // Initial action 
        $scope.search = function () {
      
            var initialSettings = {
                getData: function (params) {
                    for (var i in params.sorting()) {
                        $scope.sortColumn = i;
                        $scope.sortOrder = params.sorting()[i];
                    }
                    var d = $q.defer();
                    var filter = params.filter();

                    ApprovalTemplateService.GetAllApprovalTemplate({
                        page: params.page(),
                        pageSize: params.count(),
                        sortDirection: $scope.sortOrder,
                        sortColumn: $scope.sortColumn,
                        Name: $scope.name,
                        CreatedOnFrom: getDateRangePickerValue(1, $scope.createdDate),
                        CreatedOnTo: getDateRangePickerValue(2, $scope.createdDate)
                    }).then(function (data) {
                        $scope.resultsLength = data.totalRecordCount;
                        params.total(data.totalRecordCount);
                        d.resolve(data.data);
                    });
                    return d.promise;
                }
            };
            $scope.approvalTemplateTable = new NgTableParams($scope.pageSize, initialSettings);
        } 
        // For reset button
        $scope.reset = function () {
            $scope.sortOrder = "desc";
            $scope.sortColumn = "CreatedDate";  
            $scope.name = null;
            $scope.createdDate = null;
            $scope.dateFrom = null;
            $scope.dateTo = null;
            $scope.dateRangeOpened = false;
            $scope.search();
        }
        // Tagging of activate or deactivate
        $scope.activateOrDeactivate = function (approvalTemplateId, status, name) {
            CommonService.reactivateDeactivate(function () {
                ApprovalTemplateService.ActivateOrDeactivateApprovalTemplate({
                    approvalTemplateId: approvalTemplateId,
                    status: status,
                    name:name
                }).then(function (data) {
                    if (data.succeed) {
                        CommonService.successMessage(data.message);
                    }
                    else {
                        CommonService.warningMessage(data.message);
                    }
                       
                    $scope.search();
                });
            }, name, status);   
            }     
        // Tagging of delete
        $scope.delete = function (approvalTemplateId, name) {
            CommonService.deleteChanges(function () {
                ApprovalTemplateService.DeleteApprovalTemplate({
                    approvalTemplateId: approvalTemplateId
                }).then(function (data) {
                    if (data.succeed) {
                        CommonService.successMessage(data.message);
                    }
                    else {
                        CommonService.warningMessage(data.message);
                    }
                    $scope.search();
                })

            }, name)
        }
    }]);

