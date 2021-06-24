(function () {

    let app = angular.module('IMV.DOCUMENT', [
        'cb.invntryreval.doctemplate',
        'cb.doc.InvntryRevalGrid',
        'TRN.INVNTRY.SERVICE'
    ])
    app
        .controller('IMVListCtrl', ['$scope', function ($scope) {}])
        .controller('IMVDetailCtrl', [
            '$scope',
            '$rootScope',
            '$routeParams',
            '$location',
            '$filter',
            '$window',
            '$q',
            'CommonService',
            'imv.service',
            function ($scope, $rootScope, $routeParams, $location, $filter, $window, $q, $cs, $imv) {

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
                        //InvntryRevalIncAcct
                        //InvntryRevalDecAcct
                        m.InventoryRevaluationLines = args.docDetail.filter(x => x.ItemCode !== '' && x.InvntryRevalIncAcct !== '' && x.InvntryRevalDecAcct !== '')
                    }

                    if (docID > 0) {
                        if ($cs.saveChanges(() => {
                            $imv.SaveOrUpdate(m).then(res => {
                                $scope.$broadcast('onDocumentSavePostAction', {
                                    'objType': 'IMV', // change this per doctype
                                    'data': d.DataResult
                                })
                                $window.location.href = '#!/IMVList'
                            })
                        }));
                    }
                    else {
                        if ($cs.saveChanges(() => {
                            $imv.Add(m).then(function (d) {
                                $scope.$broadcast('onDocumentSavePostAction', {
                                    'objType': 'IMV', // change this per doctype
                                    'data': d.DataResult
                                })
                                $window.location.href = '#!/IMVList'
                            })
                        }));
                    }
                })
            }])
})();


(function () {

    let app = angular.module('MetronicApp')
    app.requires.push.apply(app.requires, ['ngRoute', 'IMV.DOCUMENT'])
    app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
        $routeProvider
            .when('/IMVList', {
                templateUrl: 'DocumentListTemplate.html',
                controller: 'IMVListCtrl'
            })

            .when('/IMVDetail/:DocID', {
                templateUrl: 'DocumentDetailTemplate.html',
                controller: 'IMVDetailCtrl'
            })

            .otherwise({
                //templateUrl: 'DocumentListTemplate.html',
                //controller: 'IQPListCtrl'
                templateUrl: 'DocumentDetailTemplate.html',
                controller: 'IMVDetailCtrl'
            })
        $locationProvider.html5Mode(false).hashPrefix('!');
    }])
}());