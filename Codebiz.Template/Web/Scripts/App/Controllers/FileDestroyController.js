angular.module('MetronicApp')
    .controller('FileDestroyController', [
        '$scope', '$http', 'FileCounter',
        function ($scope, $http, FileCounter) {
           // console.log($scope.file)
            var file = $scope.file,
                state;
        
            if (file.url) {
                file.$state = function () {
                    return state;
                };
                file.$destroy = function () {
                    state = 'pending';
                    FileCounter.currentFileCount--;
                    return $http({
                        url: file.deleteUrl,
                        method: file.deleteType
                    }).then(
                        function () {
                            state = 'resolved';
                            $scope.clear(file);
                        },
                        function () {
                            state = 'rejected';
                        }

                    );
                };
            } else if (!file.$cancel && !file._index) {
                file.$cancel = function () {
                    FileCounter.currentFileCount--;
                    $scope.clear(file);
                };
            }

            if ($scope.isUpdate && !FileCounter.isInitialize || $scope.isForRenewal) {
                FileCounter.currentFileCount = $scope.queue.length;
                FileCounter.isInitialize = true;
            }

            $scope.$watch("FileCounter.count", function () {
                var data = $scope.queue.filter(r => r.error == undefined);
                if (data != undefined) {
                    if (data.findIndex(r => r.$progress != null) != -1) {
                        angular.element("#addbtn").prop("disabled", true);
                        angular.element("#supportingDocuments").mLoading({
                            text: "Uploading file(s)...",
                            mask: true,
                        });
                        if (data.findIndex(r=>r.type == "image/gif" && r.$progress!=null && !$scope.mediaOnly)!=-1) {
                            angular.element("#addbtn").prop("disabled", false);
                            angular.element("#supportingDocuments").mLoading('hide');
                            FileCounter.currentFileCount = $scope.queue.length;
                        }
                   
                    }
                    else {
                        angular.element("#addbtn").prop("disabled", false);
                        angular.element("#supportingDocuments").mLoading('hide');
                        FileCounter.currentFileCount = $scope.queue.length;
                    }
                }
            });
        }
    ]);