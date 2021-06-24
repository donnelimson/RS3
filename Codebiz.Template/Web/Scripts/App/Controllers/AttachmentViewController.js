(function () {
    'use strict';
    var app = angular.module('MetronicApp');
    app.controller('AttachmentViewController', AttachmentViewController);
    AttachmentViewController.$inject = ['$scope', '$uibModal', '$window', 'Data', '$q', '$uibModalInstance', 'CommonService', 'AttachmentService'];

    function AttachmentViewController($scope, $uibModal, $window, Data, $q, $uibModalInstance, CommonService, AttachmentService) {

        $scope.attachments = [];

        this.$onInit = function () {
            switch (Data.Type) {
                case 'DA-History':
                    AttachmentService.GetAttachmentsByHistoryId({
                        id: Data.Id
                    }).then(function (data) {
                        $scope.attachments = data.data;
                    }), function (error /*Error event should handle here*/) {
                        CommonService.errorMessage("Unexpected error occured.");
                    }, function (data /*Notify event should handle here*/) {
                    }
                    break;
                case 'DA-Main':
                    AttachmentService.GetAttachmentsByDiscountApplicationId({
                        id: Data.Id
                    }).then(function (data) {
                        $scope.attachments = data.data;
                    }), function (error /*Error event should handle here*/) {
                        CommonService.errorMessage("Unexpected error occured.");
                    }, function (data /*Notify event should handle here*/) {
                    }
                    break;
                default:
                    break;
            }
        };

        $scope.openSupportingDocumentPreview = function (thumbnailUrl, name, url, isPdf) {
            if (isPdf) {
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
                    windowClass: 'modal-preview',
                    modalOverflow: true,
                    resolve: {
                        thumbnailUrl: function () {
                            return thumbnailUrl;
                        },
                        name: function () {
                            return name;
                        }
                    }
                }).result.then(function (data) {
                }, function () {

                });
            }
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    }
})();
