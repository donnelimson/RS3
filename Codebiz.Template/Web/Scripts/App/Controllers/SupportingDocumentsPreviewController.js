angular.module('MetronicApp')
    .controller('SupportingDocumentsPreviewController',
        ['$scope', '$uibModalInstance', 'thumbnailUrl', 'name',
            function ($scope, $uibModalInstance, thumbnailUrl, name) {
                $scope.thumbnailUrl = thumbnailUrl;
                $scope.name = name;

                $scope.cancel = function () {
                    $uibModalInstance.dismiss('cancel');
                };
            }
        ]
    )