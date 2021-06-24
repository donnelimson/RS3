MetronicApp.controller('DocumentNumberingDetailsController', ['$scope', '$q', 'NgTableParams', '$location', '$window', 'DocumentNumberingService',
    function ($scope, $q, NgTableParams, $location, $window, DocumentNumberingService) {
        $scope.documentNumberingTableParams = new NgTableParams({}, { dataset: [] });
        this.$onInit = function () {
            getAllTransactionTypeForDocumentNumbering();
            getDetails();
        }
        //functions
        function initValues() {
            $scope.sortOrder = "asc";
            $scope.sortColumn = "DocumentNumberingTransactionTypeId";
        }
        function getDetails() {
            initValues();
            var initialSettings = {
                getData: function (params) {
                    for (var i in params.sorting()) {
                        $scope.sortColumn = i;
                        $scope.sortOrder = params.sorting()[i];
                    }
                    var d = $q.defer();
                    DocumentNumberingService.GetDetails({
                        DocumentNumberingId: $location.search().documentNumberingId,
                        Page: params.page(),
                        PageSize: params.count(),
                        SortDirection: $scope.sortOrder,
                        SortColumn: $scope.sortColumn,
                    }).then(function (data) {
                        $scope.transactionTypeId = data.result[0].TransactionTypeId;
                        params.total(data.filteredRecordCount);
                        d.resolve(data.result);
                    });
                    return d.promise;
                }
            };
            $scope.documentNumberingTableParams = new NgTableParams(10, initialSettings);
        }
        function getAllTransactionTypeForDocumentNumbering() {
            var fieldName = "TransactionTypeId";
            DocumentNumberingService.GetAllTransactionTypeForDocumentNumbering({}, fieldName).then(function (data) {
                $scope.transactionTypes = data.result
            });
        }
    
        //scopes
        $scope.backToList = function () {
         $window.location.href = document.DocumentNumbering + "Index";
        }


    }])