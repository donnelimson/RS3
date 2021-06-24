angular.module('MetronicApp').controller('AppUserImageCropController',
    ['$scope', '$uibModalInstance', '$timeout', '$compile', 'CommonService', 'AppUserService', 'appUserPhotos', 'imageType',
        function ($scope, $uibModalInstance, $timeout, $compile, CommonService, AppUserService, appUserPhotos, imageType) {
            // DEFAULTS
            $scope.imagesToDelete = [];
            $scope.currentPhoto = "";
            $scope.defaultPhoto = "";
            $scope.photoOrSignature = "";
            $scope.currentCroppedPhoto = "";
            $scope.selectedImageFileName = "";
            $scope.hasNoPhoto = true;
            $scope.hasSelectedImage = false;
            $scope.hasAttachedJcropToExisting = false;
            $scope.isExtensionInvalid = false;
            $scope.initImageStyle = {};
            $scope.fileName = "";
            $scope.fileExt = "";
            $scope.isPhoto = false;
            $scope.isSignature = false;
            $scope.hasTakenPhoto = false;
            $scope.isFileSizeTooBig = false;
            $scope.webcamError = false;

            var jcrop_api_init = null;
            var jcrop_api_new = null;
            var jcrop_api_exist = null;

            const initImageId = "#no_image";
            const newImageElem = ".jcrop_target img";
            const croppedPhotoDivId = "#croppedPhoto";
            const cropUploadBtnId = "#cropPictureBtn";

            //#region Webcam
            var _video = null;
            $scope.patData = null;

            $scope.patOpts = { x: 0, y: 0, w: 25, h: 25 };

            // Setup a channel to receive a video property
            // with a reference to the video element
            // See the HTML binding in main.html
            $scope.channel = {
                videoHeight: 360,
                videoWidth: 480
            };

            $scope.onError = function (err) {
                $scope.$apply(
                    function () {
                        $scope.webcamError = err;
                    }
                );
            };

            $scope.onSuccess = function () {
                // The video element contains the captured camera data
                _video = $scope.channel.video;
                $scope.$apply(function () {
                    $scope.patOpts.w = _video.width;
                    $scope.patOpts.h = _video.height;
                    //$scope.showDemos = true;
                });
            };

            $scope.onStream = function (stream) {
                // You could do something manually with the stream.
            };

            $scope.takePicture = function () {
                if (_video) {
                    var patCanvas = document.querySelector('#snapshot');
                    if (!patCanvas) return;

                    patCanvas.width = _video.width;
                    patCanvas.height = _video.height;
                    var ctxPat = patCanvas.getContext('2d');

                    var idata = getVideoData($scope.patOpts.x, $scope.patOpts.y, $scope.patOpts.w, $scope.patOpts.h);
                    ctxPat.putImageData(idata, 0, 0);

                    sendSnapshotToServer(patCanvas.toDataURL());
                    $scope.patData = idata;

                    $scope.currentPhoto = $scope.snapshotData;

                    angular.element("#modal-body").mLoading({
                        text: "Please wait...",
                        mask: true
                    });

                    $timeout(function () {
                        var newImageElement = document.getElementById("no_image");
                        displayImage(newImageElement);
                        jcrop_api_new = attachJcrop(newImageElement);

                        angular.element(document.querySelector(croppedPhotoDivId)).css({
                            "background-image": "url(" + newImageElement.src + ")"
                        });

                        angular.element("#modal-body").mLoading('hide');
                    }, 1000);

                    $scope.hasTakenPhoto = true;
                    $scope.hasNoPhoto = false;
                }
            };

            $scope.downloadSnapshot = function downloadSnapshot(dataURL) {
                window.location.href = dataURL;
            };

            $scope.retakePhoto = function () {
                $scope.currentPhoto = "";
                patData = null;
                $scope.hasTakenPhoto = false;
                $scope.hasNoPhoto = true;
                jcrop_api_new.destroy();
                angular.element(document.querySelector(croppedPhotoDivId)).css("background-image", "none");
            };

            var getVideoData = function getVideoData(x, y, w, h) {
                var hiddenCanvas = document.createElement('canvas');
                hiddenCanvas.width = _video.width;
                hiddenCanvas.height = _video.height;
                var ctx = hiddenCanvas.getContext('2d');
                ctx.drawImage(_video, 0, 0, _video.width, _video.height);
                return ctx.getImageData(x, y, w, h);
            };

            var sendSnapshotToServer = function sendSnapshotToServer(imgBase64) {
                $scope.snapshotData = imgBase64;
            };

            //#endregion

            // SCOPE FUNCTIONS
            // Initialize at start
            $scope.init = function () {
                    if (appUserPhotos.length > 1) {
                        $scope.currentPhoto = appUserPhotos[0].thumbnailUrl;
                        $scope.currentCroppedPhoto = appUserPhotos[1].thumbnailUrl;
                        $scope.fileName = appUserPhotos[0].name;
                    }

                    if (imageType === 1) {
                        $scope.isPhoto = true;
                        $scope.defaultPhoto = "~/assets/global/img/no-avatar.png";
                        $scope.photoOrSignature = "Photo";
                    }
                    else if (imageType === 2) {

                        $scope.isSignature = true;
                        $scope.defaultPhoto = "~/assets/global/img/signature.jpg";
                        $scope.photoOrSignature = "Signature";
                    }
                angular.element("#modal-body").mLoading({
                    text: "Please wait...",
                    mask: true
                });
                $timeout(function () {
                    initJcrop();
                    angular.element("#modal-body").mLoading('hide');
                }, 1000);
            };

            // Attach Jcrop to selected image file
            $scope.attachJcropToNewImage = function (event) {
                var file = event.target.files;
                if (file.length > 0) {
                    if (file[0].size > 2097152) {
                        $scope.isFileSizeTooBig = true;
                    }
                    else {
                        $scope.isFileSizeTooBig = false;
                    }

                    var ext = event.target.files[0].name.match(/\.(.+)$/)[1];
                    if (angular.lowercase(ext) === 'jpg' || angular.lowercase(ext) === 'jpeg' || angular.lowercase(ext) === 'png' || angular.lowercase(ext) === 'bmp') {
                        if (jcrop_api_new !== null) {
                            jcrop_api_new.destroy();
                        }
                        if (jcrop_api_exist !== null) {
                            jcrop_api_exist.destroy();
                        }
                        if (jcrop_api_init !== null) {
                            jcrop_api_init.destroy();
                        }

                        angular.element("#modal-body").mLoading({
                            text: "Please wait...",
                            mask: true
                        });

                        $timeout(function () {
                            var newImageElement = document.querySelector(newImageElem);
                            displayImage(newImageElement);

                            jcrop_api_new = attachJcrop(newImageElem);
                            changeCroppedPhotoPreview(newImageElem);

                            angular.element("#modal-body").mLoading('hide');
                        }, 1000);

                        $scope.hasSelectedImage = true;
                        $scope.hasNoPhoto = false;
                        $scope.isExtensionInvalid = false;
                        $scope.fileName = event.target.files[0].name;
                        $scope.selectedImageFileName = "<b>Selected file: </b><u>" + $scope.fileName + "</u>";
                    }
                    else {
                        $scope.isExtensionInvalid = true;
                    }
                }
            };

            // Remove selected file and destroy attached Jcrop
            $scope.removePhoto = function () {
                try {
                    if (jcrop_api_new !== null) {
                        jcrop_api_new.destroy();
                    }
                    if (jcrop_api_init !== null) {
                        jcrop_api_init.destroy();
                    }
                    if (jcrop_api_exist !== null) {
                        jcrop_api_exist.destroy();
                    }

                    $scope.isFileSizeTooBig = false;

                    changeCroppedPhotoPreview(initImageId);
                    angular.element("#modal-body").mLoading({
                        text: "Please wait...",
                        mask: true
                    });

                    $timeout(function () {
                        initJcrop();
                        angular.element("#modal-body").mLoading('hide');
                    }, 500);
                    angular.element(document.querySelector(cropUploadBtnId)).prop("disabled", true);
                    $scope.hasSelectedImage = false;
                    $scope.hasNoPhoto = true;
                    $scope.isExtensionInvalid = false;
                    if (appUserPhoto.length > 0) {
                        $scope.fileName = appUserPhoto[0].name;
                    }
                    else {
                        $scope.fileName = "";
                    }
                    $scope.selectedImageFileName = "";
                }
                catch (err) {

                }
              
            };

            // Attach Jcrop to existing/current uploaded photo
            $scope.attachJcropToExisting = function () {
                if ($scope.currentPhoto !== "") {
                    angular.element("#modal-body").mLoading({
                        text: "Please wait...",
                        mask: true
                    });

                    $timeout(function () {
                        jcrop_api_exist = attachJcrop(initImageId);
                        changeCroppedPhotoPreview(initImageId);
                        $scope.hasNoPhoto = false;
                        angular.element(document.querySelector(cropUploadBtnId)).prop("disabled", false);
                        angular.element("#modal-body").mLoading('hide');
                    }, 500);
                    $scope.hasAttachedJcropToExisting = true;
                }
            };

            // Upload photo (both cropped and uncropped
            $scope.uploadPhoto = function () {
                var jCropHolder;
                var AppUserPhotos = {};

                if (!$scope.hasSelectedImage) {
                    var initImageElement = angular.element(document.querySelector(initImageId));
                    let jCropImageSrc = getBase64Image(initImageElement.prop('src')).then(function (jCropImageSrc) {
                        angular.element("#modal-body").mLoading({
                            text: "Cropping photo...",
                            mask: true
                        });
                        var fileNameSplit = $scope.fileName.split(".");
                        var fileNameSplitLength = $scope.fileName.split(".").length;
                        $scope.fileExt = "." + fileNameSplit[fileNameSplitLength - 1];
                        $timeout(function () {
                            jCropHolder = angular.element(document.querySelector(".fileinput-new .jcrop-holder")).children();
                            AppUserPhotos = {
                                Photo: jCropImageSrc,
                                OriginalWidth: initImageElement.css("width").replace("px", ""),
                                OriginalHeight: initImageElement.css("height").replace("px", ""),
                                XCoord: jCropHolder.css("left").replace("px", ""),
                                YCoord: jCropHolder.css("top").replace("px", ""),
                                CropWidth: jCropHolder.css("width").replace("px", ""),
                                CropHeight: jCropHolder.css("height").replace("px", ""),
                                ImageName: $scope.fileName.replace($scope.fileExt, ''),
                                ImagesToDelete: $scope.imagesToDelete,
                                IsPhoto: $scope.isPhoto ? true : false
                            };
                            if (imageType == 1) {
                                AppUserService.UploadAppUserPhoto({
                                    model: AppUserPhotos
                                }).then(function (data) {
                                    $scope.appUsersPhoto = AppUserPhotos;
                                    if (data.success) {
                                        //data.data.files[0].
                                        //Id = appUserId;
                                        //data.data.files[1].MemberId = memberId;
                                        //console.log(data)
                                        $uibModalInstance.close(data);
                                    }
                                    else {
                                        CommonService.errorMessage(data.message);
                                    }
                                }, function (error /*Error event should handle here*/) {
                                    console.log(error);
                                    CommonService.errorMessage("Unexpected error occured.");
                                }, function (data /*Notify event should handle here*/) {
                                });
                            }
                            else if (imageType == 2) {
                                AppUserService.UploadAppUserSignature({
                                    model: AppUserPhotos
                                }).then(function (data) {
                                    $scope.appUsersPhoto = AppUserPhotos;
                                    if (data.success) {
                                        //data.data.files[0].
                                        //Id = appUserId;
                                        //data.data.files[1].MemberId = memberId;
                                        //console.log(data)
                                        $uibModalInstance.close(data);
                                    }
                                    else {
                                        CommonService.errorMessage(data.message);
                                    }
                                }, function (error /*Error event should handle here*/) {
                                    console.log(error);
                                    CommonService.errorMessage("Unexpected error occured.");
                                }, function (data /*Notify event should handle here*/) {
                                });
                            }
                          
                            angular.element("#modal-body").mLoading('hide');
                        }, 1000);
                    }, function (reason) {
                        console.log(reason); // Error!
                    });
                }

                else {
                    var newImageElement = angular.element(document.querySelector(newImageElem));
                    angular.element("#modal-body").mLoading({
                        text: "Cropping photo...",
                        mask: true
                    });
                    var fileNameSplit = $scope.fileName.split(".");
                    var fileNameSplitLength = $scope.fileName.split(".").length;
                    $scope.fileExt = "." + fileNameSplit[fileNameSplitLength - 1];
                    $timeout(function () {
                        jCropHolder = angular.element(".fileinput-preview .jcrop-holder").children();
                        AppUserPhotos = {
                            Photo: newImageElement.prop('src').replace(/^data:image\/[a-z]+;base64,/, ""),
                            OriginalWidth: newImageElement.css("width").replace("px", ""),
                            OriginalHeight: newImageElement.css("height").replace("px", ""),
                            XCoord: jCropHolder.css("left").replace("px", ""),
                            YCoord: jCropHolder.css("top").replace("px", ""),
                            CropWidth: jCropHolder.css("width").replace("px", ""),
                            CropHeight: jCropHolder.css("height").replace("px", ""),
                            ImageName: $scope.fileName.replace($scope.fileExt, ''),
                            ImagesToDelete: $scope.imagesToDelete,
                            IsPhoto: $scope.isPhoto ? true : false
                        };

                        if (imageType == 1) {
                            AppUserService.UploadAppUserPhoto({
                                model: AppUserPhotos
                            }).then(function (data) {
                                $scope.appUsersPhoto = AppUserPhotos;
                                if (data.success) {
                                    //data.data.files[0].
                                    //Id = appUserId;
                                    //data.data.files[1].MemberId = memberId;
                                    //console.log(data)
                                    $uibModalInstance.close(data);
                                }
                                else {
                                    CommonService.errorMessage(data.message);
                                }
                            }, function (error /*Error event should handle here*/) {
                                console.log(error);
                                CommonService.errorMessage("Unexpected error occured.");
                            }, function (data /*Notify event should handle here*/) {
                            });
                        }
                        else if (imageType == 2) {
                            AppUserService.UploadAppUserSignature({
                                model: AppUserPhotos
                            }).then(function (data) {
                                $scope.appUsersPhoto = AppUserPhotos;
                                if (data.success) {
                                    //data.data.files[0].
                                    //Id = appUserId;
                                    //data.data.files[1].MemberId = memberId;
                                    //console.log(data)
                                    $uibModalInstance.close(data);
                                }
                                else {
                                    CommonService.errorMessage(data.message);
                                }
                            }, function (error /*Error event should handle here*/) {
                                console.log(error);
                                CommonService.errorMessage("Unexpected error occured.");
                            }, function (data /*Notify event should handle here*/) {
                            });
                        }
                        angular.element("#modal-body").mLoading('hide');
                    }, 1000);
                }
            };

            // Dismiss modal
            $scope.cancel = function () {
                $uibModalInstance.dismiss('cancel');
            };

            // FUNCTION LISTENERS
            // Initialize existing/current uploaded photo
            $scope.$on('changeCurrentPhoto', function (event, data) {
                $scope.currentPhoto = data.AppUserPhotos[0].thumbnailUrl;
                $scope.currentCroppedPhoto = data.AppUserPhotos[1].thumbnailUrl;
                angular.element(document.querySelector(croppedPhotoDivId)).css("background-image", "url(" + data.AppUserPhotos[1].thumbnailUrl + ")");
            });


            // PRIVATE FUNCTIONS

            function init() {
                $scope.init();
            }

            // Get coordinates and dimensions of selected crop area
            function showCoords(c) {
                var originalPhoto;
                var croppedPreview = angular.element(document.querySelector(croppedPhotoDivId));
                if ($scope.hasSelectedImage) {
                    originalPhoto = angular.element(document.querySelector(".fileinput-preview .jcrop-holder"));
                }
                else {
                    originalPhoto = angular.element(document.querySelector(".fileinput-new .jcrop-holder"));
                }

                var oH = originalPhoto.height();
                var oW = originalPhoto.width();

                var pH = 0;
                var pW = 0;

                if ($scope.isPhoto) {
                    pH = 190;
                    pW = 190;
                }
                else if ($scope.isSignature) {
                    pH = 100;
                    pW = 267;
                }

                var sH = c.h;
                var sW = c.w;

                var differenceH = pH - sH;
                var differenceW = pW - sW;

                var rW = pW / c.w;
                var rH = pH / c.h;

                croppedPreview.css("background-size", oW * rW + "px" + " " + oH * rH + "px");
                croppedPreview.css("background-position", rW * Math.round(c.x) * -1 + "px" + " " + rH * Math.round(c.y) * -1 + "px");
            }

            // Attached Jcrop at start
            function initJcrop() {
                var initImageElement = document.querySelector(initImageId);
                displayImage(initImageElement);

                if ($scope.currentPhoto === "") {
                    jcrop_api_init = attachJcrop(initImageId);
                    changeCroppedPhotoPreview(initImageId);
                }
                else {
                    angular.element(initImageId).prop('src', $scope.currentPhoto);

                    if ($scope.currentCroppedPhoto === "") {
                        changeCroppedPhotoPreview(initImageId);
                    }
                    else {
                        if ($scope.isPhoto) {
                            angular.element(document.querySelector(croppedPhotoDivId)).css({
                                "background-image": "url(" + $scope.currentCroppedPhoto + ")",
                                "background-size": "190px 190px"
                            });
                        }
                        else if ($scope.isSignature) {
                            angular.element(document.querySelector(croppedPhotoDivId)).css({
                                "background-image": "url(" + $scope.currentCroppedPhoto + ")",
                                "background-size": "267px 100px"
                            });
                        }
                    }
                }
            }

            // Convert image url to base 64
            function getBase64Image(imgUrl) {
                return new Promise(
                    function (resolve, reject) {

                        var img = new Image();
                        img.src = imgUrl;
                        img.setAttribute('crossOrigin', 'anonymous');

                        img.onload = function () {
                            var canvas = document.createElement("canvas");
                            canvas.width = img.width;
                            canvas.height = img.height;
                            var ctx = canvas.getContext("2d");
                            ctx.drawImage(img, 0, 0);
                            var dataURL = canvas.toDataURL("image/png");
                            resolve(dataURL.replace(/^data:image\/(png|jpg);base64,/, ""));
                        };
                        img.onerror = function () {
                            reject("The image could not be loaded.");
                        };

                    });

            }

            function changeCroppedPhotoPreview(sourceElement) {
                angular.element(document.querySelector(croppedPhotoDivId)).css({
                    "background-image": "url(" + angular.element(document.querySelector(sourceElement)).prop('src') + ")"
                });
            }

            function releaseCheck() {
                this.setOptions({ setSelect: [0, 0, 100, 100] });
            }

            function attachJcrop(imgElement) {
                if ($scope.isPhoto) {
                    return $.Jcrop(imgElement, {
                        bgColor: 'transparent',
                        onSelect: showCoords,
                        onChange: showCoords,
                        setSelect: [0, 0, 100, 100],
                        bgOpacity: .4,
                        aspectRatio: 1,
                        allowSelect: false,
                        onRelease: releaseCheck,
                        addClass: 'jcrop-centered'
                    });

                    //angular.element(document.querySelector(initImageId)).css({
                    //    "background-image": "url(" + $scope.currentCroppedPhoto + ")",
                    //    "background-size": "190px 190px",
                    //});
                }
                else if ($scope.isSignature) {
                    return $.Jcrop(imgElement, {
                        bgColor: 'transparent',
                        onSelect: showCoords,
                        onChange: showCoords,
                        minSize: [100, 100],
                        setSelect: [0, 0, 100, 100],
                        bgOpacity: .4,
                        aspectRatio: 1,
                        allowSelect: false,
                        aspectRatio: 267 / 100,
                        onRelease: releaseCheck,
                        addClass: 'jcrop-centered'
                    });
                }
            }

            function changeImageSize(imgElement, newWidth, newHeight) {
                imgElement.setAttribute("width", newWidth);
                imgElement.setAttribute("height", newHeight);
                $compile(imgElement);
            }

            function displayImage(imgElement) {
                let heighToUse = 350;
                let widthToUse = 0;
                let originalImageWidth = 0;
                let originalImageHeight = 0;
                let imageRatio = 0;

                originalImageWidth = imgElement.naturalWidth;
                originalImageHeight = imgElement.naturalHeight;
                imageRatio = originalImageWidth / originalImageHeight;
                widthToUse = heighToUse * imageRatio;

                if (widthToUse > 600) {
                    heighToUse = 600 / imageRatio;
                    widthToUse = 600;
                }

                changeImageSize(imgElement, widthToUse, heighToUse);
            }

            init();
        }]);