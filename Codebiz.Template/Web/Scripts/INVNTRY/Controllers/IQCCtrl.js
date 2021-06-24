(function () {

    let app = angular.module('IQC.DOCUMENT', [
        'cb.invntryqty.doctemplate',
        'cb.doc.InvntryTrackingGrid',
        'TRN.INVNTRY.SERVICE'
    ])
    app
        .controller('IQCListCtrl', ['$scope', function ($scope) {
        }])
        .controller('IQCDetailCtrl', [
            '$scope',
            '$rootScope',
            '$routeParams',
            '$location',
            '$filter',
            '$window',
            '$q',
            'CommonService',
            'iqc.service',
            function ($scope, $rootScope, $routeParams, $location, $filter, $window, $q, $cs, $iqc) {

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
                            $iqc.SaveOrUpdate(m).then(res => {
                                $scope.$broadcast('onDocumentSavePostAction', {
                                    'objType': 'IQC', // change this per doctype
                                    'data': d.DataResult
                                })
                                $window.location.href = '#!/IQCDetail/-1'
                            })
                        }));
                    }
                    else {
                        if ($cs.saveChanges(() => {
                            $iqc.Add(m).then(function (d) {
                                $scope.$broadcast('onDocumentSavePostAction', {
                                    'objType': 'IQC', // change this per doctype
                                    'data': d.DataResult
                                })
                                $window.location.href = '#!/IQCDetail/-1'
                            })
                        }));
                    }
                })
            }])
})();


(function () {

    let app = angular.module('MetronicApp')
    app.requires.push.apply(app.requires, ['ngRoute', 'IQC.DOCUMENT'])
    app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
        $routeProvider
            .when('/IQCList', {
                templateUrl: 'DocumentListTemplate.html',
                controller: 'IQCListCtrl'
            })

            .when('/IQCDetail/:DocID', {
                templateUrl: 'DocumentDetailTemplate.html',
                controller: 'IQCDetailCtrl'
            })

            .otherwise({
                //templateUrl: 'DocumentListTemplate.html',
                //controller: 'IQCListCtrl'
                templateUrl: 'DocumentDetailTemplate.html',
                controller: 'IQCDetailCtrl'
            })
        $locationProvider.html5Mode(false).hashPrefix('!');
    }])
}());