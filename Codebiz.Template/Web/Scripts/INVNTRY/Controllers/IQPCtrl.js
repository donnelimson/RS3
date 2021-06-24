(function () {

    let app = angular.module('IQP.DOCUMENT', [
        'cb.invntryqty.doctemplate',
        'cb.doc.InvntryTrackingGrid',
        'TRN.INVNTRY.SERVICE'
    ])
    app
        .controller('IQPListCtrl', ['$scope', function ($scope) {
        }])
        .controller('IQPDetailCtrl', [
            '$scope',
            '$rootScope',
            '$routeParams',
            '$location',
            '$filter',
            '$window',
            '$q',
            'CommonService',
            'iqp.service',
            function ($scope, $rootScope, $routeParams, $location, $filter, $window, $q, $cs, $iqp) {

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
                        m.InventoryQtyPostingDetails = args.docDetail.filter(x => !(x.ItemCode === '' && (x.InvntryOffsetIncAcct === '' || x.InvntryOffsetDecAcct === '')))
                    }

                    if (docID > 0) {
                        if ($cs.saveChanges(() => {
                            $iqp.SaveOrUpdate(m).then(res => {
                                $scope.$broadcast('onDocumentSavePostAction', {
                                    'objType': 'IQP', // change this per doctype
                                    'data': d.DataResult
                                })
                                $window.location.href = '#!/IQPDetail/-1'
                            })
                        }));
                    }
                    else {
                        if ($cs.saveChanges(() => {
                            $iqp.Add(m).then(function (d) {
                                $scope.$broadcast('onDocumentSavePostAction', {
                                    'objType': 'IQP', // change this per doctype
                                    'data': d.DataResult
                                })
                                $window.location.href = '#!/IQPDetail/-1'
                            })
                        }));
                    }
                })
            }])
})();


(function () {

    let app = angular.module('MetronicApp')
    app.requires.push.apply(app.requires, ['ngRoute', 'IQP.DOCUMENT'])
    app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
        $routeProvider
            .when('/IQPList', {
                templateUrl: 'DocumentListTemplate.html',
                controller: 'IQPListCtrl'
            })

            .when('/IQPDetail/:DocID', {
                templateUrl: 'DocumentDetailTemplate.html',
                controller: 'IQPDetailCtrl'
            })

            .otherwise({
                //templateUrl: 'DocumentListTemplate.html',
                //controller: 'IQPListCtrl'
                templateUrl: 'DocumentDetailTemplate.html',
                controller: 'IQPDetailCtrl'
            })
        $locationProvider.html5Mode(false).hashPrefix('!');
    }])
}());