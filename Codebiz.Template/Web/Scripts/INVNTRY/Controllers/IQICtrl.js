(function () {

    let app = angular.module('IQI.DOCUMENT', [
        'cb.invntryqty.doctemplate',
        'cb.doc.InvntryTrackingGrid',
        'TRN.INVNTRY.SERVICE'
    ])
    app
        .controller('IQIListCtrl', ['$scope', function ($scope) {
        }])
        .controller('IQIDetailCtrl', [
            '$scope',
            '$rootScope',
            '$routeParams',
            '$location',
            '$filter',
            '$window',
            '$q',
            'CommonService',
            'iqi.service',
            function ($scope, $rootScope, $routeParams, $location, $filter, $window, $q, $cs, $iqi) {

                $scope.docId = $routeParams.DocID

                $scope.docData = {}

                let d = $q.defer()

                d.resolve({
                    "Action": "ADD"
                })

                $scope.docData.DocAction = d.promise

                $scope.$on('onDocumentSaveAction', function (e, args) {

                    let m = args.docHeader;

                    docID = m.DocID
                    if (args.docDetail.length > 0) {
                        //InvntryOffsetIncAcct
                        //InvntryOffsetDecAcct
                        m.InventoryQtyTrackingDetails = args.docDetail.filter(x => !(x.ItemCode === '' && (x.InvntryOffsetIncAcct === '' || x.InvntryOffsetDecAcct === '')))
                    }

                    if (docID > 0) {
                        if ($cs.saveChanges(() => {
                            $iqi.SaveOrUpdate(m).then(res => {
                                $scope.$broadcast('onDocumentSavePostAction', {
                                    'objType': 'IQI', // change this per doctype
                                    'data': d.DataResult
                                })
                                $window.location.href = '#!/IQIDetail/-1'
                            })
                        }));
                    }
                    else {
                        if ($cs.saveChanges(() => {
                            $iqi.Add(m).then(function (d) {
                                $scope.$broadcast('onDocumentSavePostAction', {
                                    'objType': 'IQI', // change this per doctype
                                    'data': d.DataResult
                                })
                                $window.location.href = '#!/IQIDetail/-1'
                            })
                        }));
                    }
                })
            }])
})();


(function () {

    let app = angular.module('MetronicApp')
    app.requires.push.apply(app.requires, ['ngRoute', 'IQI.DOCUMENT'])
    app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
        $routeProvider
            .when('/IQIList', {
                templateUrl: 'DocumentListTemplate.html',
                controller: 'IQIListCtrl'
            })

            .when('/IQIDetail/:DocID', {
                templateUrl: 'DocumentDetailTemplate.html',
                controller: 'IQIDetailCtrl'
            })

            .otherwise({
                templateUrl: 'DocumentDetailTemplate.html',
                controller: 'IQIDetailCtrl'
            })
        $locationProvider.html5Mode(false).hashPrefix('!');
    }])
}());