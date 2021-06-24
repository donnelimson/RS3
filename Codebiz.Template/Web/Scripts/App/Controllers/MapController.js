angular.module('MetronicApp')
.controller('MapController',
    ['$scope', '$uibModalInstance', 'lat', 'lng', '$window', 'forView',
        function ($scope, $uibModalInstance, lat, lng, $window, forView) {
            var TILE_SIZE = 256;

            $scope.showLatLng = true;
            $scope.lat = 15.589564;
            $scope.lng = 120.610383;

            $scope.geopos = {
                lat: lat,
                lng: lng
            }

            $scope.lastGeopos = {
                lat: lat,
                lng: lng
            }

            $scope.image = document.baseUrlNoArea + "assets/global/img/Location.gif";

            $scope.forView = forView;

            function bound(value, opt_min, opt_max) {
                if (opt_min != null) value = Math.max(value, opt_min);
                if (opt_max != null) value = Math.min(value, opt_max);
                return value;
            }

            function degreesToRadians(deg) {
                return deg * (Math.PI / 180);
            }

            function radiansToDegrees(rad) {
                return rad / (Math.PI / 180);
            }

            function MercatorProjection() {
                this.pixelOrigin_ = new google.maps.Point(TILE_SIZE / 2, TILE_SIZE / 2);
                this.pixelsPerLonDegree_ = TILE_SIZE / 360;
                this.pixelsPerLonRadian_ = TILE_SIZE / (2 * Math.PI);
            }

            MercatorProjection.prototype.fromLatLngToPoint = function (latLng,
                opt_point) {
                var me = this;
                var point = opt_point || new google.maps.Point(0, 0);
                var origin = me.pixelOrigin_;

                point.x = origin.x + latLng.lng() * me.pixelsPerLonDegree_;

                // Truncating to 0.9999 effectively limits latitude to 89.189. This is
                // about a third of a tile past the edge of the world tile.
                var siny = bound(Math.sin(degreesToRadians(latLng.lat())), -0.9999,
                    0.9999);
                point.y = origin.y + 0.5 * Math.log((1 + siny) / (1 - siny)) *
                    -me.pixelsPerLonRadian_;
                return point;
            };

            MercatorProjection.prototype.fromPointToLatLng = function (point) {
                var me = this;
                var origin = me.pixelOrigin_;
                var lng = (point.x - origin.x) / me.pixelsPerLonDegree_;
                var latRadians = (point.y - origin.y) / -me.pixelsPerLonRadian_;
                var lat = radiansToDegrees(2 * Math.atan(Math.exp(latRadians)) -
                    Math.PI / 2);
                return new google.maps.LatLng(lat, lng);
            };

            $scope.$on('mapInitialized', function (event, map) {
                $scope.latitude = 15.589564;
                $scope.longhitude = 120.610383;
                if (lat != null && lng != null) {
                    $scope.latitude = lat;
                    $scope.longhitude = lng;
                }

                var mapProp = {
                    center: new google.maps.LatLng($scope.latitude, $scope.longhitude),
                    zoom: 10,
                    draggingCursor: 'move',
                    mapTypeId: google.maps.MapTypeId.ROADMAP,
                    draggableCursor: 'default'
                };

              
                $scope.map = new google.maps.Map(document.getElementById('map'), mapProp);
                var numTiles = 1 << map.getZoom();
             
                var projection = new MercatorProjection();

                if (lat != null && lng != null) {
                    setMarker(lat, lng);
                }

                $scope.place = map.getCenter();
                $scope.worldCoordinate = projection.fromLatLngToPoint($scope.place);
                $scope.pixelCoordinate = new google.maps.Point(
                    $scope.worldCoordinate.x * numTiles,
                    $scope.worldCoordinate.y * numTiles);
                $scope.tileCoordinate = new google.maps.Point(
                    Math.floor($scope.pixelCoordinate.x / TILE_SIZE),
                    Math.floor($scope.pixelCoordinate.y / TILE_SIZE));

                if (!$scope.forView) {
                    google.maps.event.addListener($scope.map, 'click', function (event) {
                        var myLatLng = event.latLng;

                        setMarker(myLatLng.lat(), myLatLng.lng())

                        $scope.$apply(function () {
                            $scope.geopos.lat = myLatLng.lat();
                            $scope.geopos.lng = myLatLng.lng();
                        });

                    });
                }
            });

            $scope.selectLocation = function () {
                $uibModalInstance.close($scope.geopos);
            };

            $scope.close = function () {
                $uibModalInstance.close($scope.lastGeopos);
            };

            $scope.goLocation = function (lat,lng) {
                $window.open("https://www.google.com.ph/maps/place/" + lat + "," + lng);
            }

            function setMarker(latitude, longhitude) {

                if ($scope.marker && $scope.marker.setMap) {
                    $scope.marker.setMap(null);
                }
                $scope.marker = new google.maps.Marker({
                    position: new google.maps.LatLng(latitude, longhitude),
                    map: $scope.map,
                    icon: {
                        url: $scope.image,
                        scaledSize: { width: 35, height: 40 }
                    }
                });
                $scope.map.setCenter($scope.marker.getPosition());
            
            }

        }
    ])
    .controller('ViewMapController',
        ['$scope', '$uibModalInstance', 'forView', 'Data','$window',
            function ($scope, $uibModalInstance, forView, Data, $window) {
                var TILE_SIZE = 256;

                $scope.showLatLng = false;
                $scope.lat = 15.589564;
                $scope.lng = 120.610383;

               

                $scope.forView = forView;

                function bound(value, opt_min, opt_max) {
                    if (opt_min != null) value = Math.max(value, opt_min);
                    if (opt_max != null) value = Math.min(value, opt_max);
                    return value;
                }

                function degreesToRadians(deg) {
                    return deg * (Math.PI / 180);
                }

                function radiansToDegrees(rad) {
                    return rad / (Math.PI / 180);
                }

                function MercatorProjection() {
                    this.pixelOrigin_ = new google.maps.Point(TILE_SIZE / 2, TILE_SIZE / 2);
                    this.pixelsPerLonDegree_ = TILE_SIZE / 360;
                    this.pixelsPerLonRadian_ = TILE_SIZE / (2 * Math.PI);
                }

                MercatorProjection.prototype.fromLatLngToPoint = function (latLng,
                    opt_point) {
                    var me = this;
                    var point = opt_point || new google.maps.Point(0, 0);
                    var origin = me.pixelOrigin_;

                    point.x = origin.x + latLng.lng() * me.pixelsPerLonDegree_;

                    // Truncating to 0.9999 effectively limits latitude to 89.189. This is
                    // about a third of a tile past the edge of the world tile.
                    var siny = bound(Math.sin(degreesToRadians(latLng.lat())), -0.9999,
                        0.9999);
                    point.y = origin.y + 0.5 * Math.log((1 + siny) / (1 - siny)) *
                        -me.pixelsPerLonRadian_;
                    return point;
                };

                MercatorProjection.prototype.fromPointToLatLng = function (point) {
                    var me = this;
                    var origin = me.pixelOrigin_;
                    var lng = (point.x - origin.x) / me.pixelsPerLonDegree_;
                    var latRadians = (point.y - origin.y) / -me.pixelsPerLonRadian_;
                    var lat = radiansToDegrees(2 * Math.atan(Math.exp(latRadians)) -
                        Math.PI / 2);
                    return new google.maps.LatLng(lat, lng);
                };

                $scope.$on('mapInitialized', function (event, map) {

                    var mapProp = {
                        center: new google.maps.LatLng($scope.lat, $scope.lng),
                        zoom: 10,
                        draggingCursor: 'move',
                        mapTypeId: google.maps.MapTypeId.ROADMAP,
                        draggableCursor: 'default',
                        fullscreenControl: true,
                        styles: [{
                            "elementType": "geometry.fill",
                            "stylers": [{
                                "visibility": "off"
                            }]
                        }, {
                            "featureType": "landscape.natural.landcover",
                            "elementType": "geometry.fill",
                            "stylers": [{
                                "visibility": "on"
                            }]
                        }]
                    };


                    $scope.map = new google.maps.Map(document.getElementById('map'), mapProp);
                    var numTiles = 1 << map.getZoom();
                    var projection = new MercatorProjection();

                    for (i = 0; i < Data.length; i++) {
                        $scope.image = Data[i].IsActive ? document.baseUrlNoArea + "assets/global/img/Location.gif" : document.baseUrlNoArea + "assets/global/img/GrayMarker.png";
                        setMarker(Data[i]);
                    }

                    $scope.place = map.getCenter();
                    $scope.worldCoordinate = projection.fromLatLngToPoint($scope.place);
                    $scope.pixelCoordinate = new google.maps.Point(
                        $scope.worldCoordinate.x * numTiles,
                        $scope.worldCoordinate.y * numTiles);
                    $scope.tileCoordinate = new google.maps.Point(
                        Math.floor($scope.pixelCoordinate.x / TILE_SIZE),
                        Math.floor($scope.pixelCoordinate.y / TILE_SIZE));

                    var mapSize = document.getElementById('map');
                    mapSize.style.height = "550px";

                });

                $scope.selectLocation = function () {
                    $uibModalInstance.close($scope.geopos);
                };

                $scope.close = function () {
                    $uibModalInstance.close($scope.lastGeopos);
                };

                function setMarker(data) {
                    if (data.Latitude != null && data.Longhitude != null) {
                        var marker = new google.maps.Marker({
                            position: new google.maps.LatLng(data.Latitude, data.Longhitude),
                            map: $scope.map,
                            icon: {
                                url: $scope.image,
                                scaledSize: { width: 60, height: 70 }
                            },
                            optimized: false
                        });
                        $scope.map.setCenter(marker.getPosition());
                        var active = data.IsActive ? 'YES' : 'NO';

                        var content = '<div>' + "<h5> <b> Pole ID: </b>" + data.PoleNo + '</h5>' + " <b> Location: </b>" + data.Location + '</br>'
                            + "<b>Latitude:  </b>" + data.Latitude + '</br>' + "<b> Longhitude: </b>" + data.Longhitude + '</div>';

                        var infowindow = new google.maps.InfoWindow({
                            content: content
                        });

                        google.maps.event.addListener(marker, 'click', (function (marker, infowindow) {

                            return function () {
                                $window.addEventListener('click', function () {
                                    var clsBtn = document.getElementsByClassName("gm-ui-hover-effect");
                                    clsBtn[0].style.top = '1px';
                                    clsBtn[0].style.right = '1px';
                                });

                                closeLastOpenedInfoWindow();
                                infowindow.open(map, marker);
                                $scope.lastOpenedInfoWindow = infowindow;
                            };
                        })(marker, infowindow));

                        google.maps.event.addListener($scope.map, 'click', function () {
                            infowindow.close();
                        });

                        function closeLastOpenedInfoWindow() {
                            if ($scope.lastOpenedInfoWindow) {
                                $scope.lastOpenedInfoWindow.close();
                            }
                        }
                    }
                }
            }
        ]);