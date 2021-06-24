angular.module('MetronicApp')
.controller('SupportingDocumentsDetailsController',
        ['$scope', 'SupportingDocumentsService', '$window',
function ($scope, SupportingDocumentsService, $window) {

    //#region Variable Defaults

    $scope.supportingDocumentsDetails = {};

    //#endregion

    this.$onInit = function () {
        getSupportingDocumentsDetails();
    };

    //#endregion

    //#region Scope functions

    $scope.backToList = function () {
        $window.location.href = document.SupportingDocuments;
    };

    //#endregion

    //#region Private functions

    function getSupportingDocumentsDetails() {
        var id = angular.element("#SupportingDocumentId").val() || 0;

        if (id !== 0) {
            SupportingDocumentsService.GetSupportingDocumentDetailsById({
                id: id
            }).then(function (data) {
                $scope.supportingDocumentsDetails = data.data;

            }, function (error /*Error event should handle here*/) {
                console.log("Error");
            }, function (data /*Notify event should handle here*/) {
            });
        }
    }

    //#endregion

}]);