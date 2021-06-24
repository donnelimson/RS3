(function () {
    var app = angular.module(
        'JE.DOCUMENT', [
        'cb.components.linktextbox',
        'cb.components.SelectList',
        'cb.je.doctemplate',
        'cb.je.FormGrid',
        'CFL.DATASERVICE',
        'BP.SETUP.SERVICE',
        'FIN.DOC.FACTORY'
    ])
    app
        .controller('JENDetailCtrl', [
            '$scope',
            '$rootScope',
            '$routeParams',
            '$location',
            '$filter',
            '$window',
            '$q',
            //'modalData',
            'CommonService',
            'BusinessPartnerService',
            'jvo.service',
            'jen.service',
            'jen.doc.model',
            function ($scope, $rootScope, $routeParams, $location, $filter, $window, $q,$cs, $bp, $jv, $je, $jeDocModel ) {

                if ($routeParams.TransID) {
                    $scope.transID = $routeParams.TransID
                }
                //if (modalData.TransID) {
                //    $scope.transID = modalData.TransID
                //}
                

                $scope.batchNum = $routeParams.BatchNum

                $scope.docData = {}

                let d = $q.defer()

                if ($scope.transID > 0) {

                    /*edit*/

                    let model = $je.GetJENByID($scope.transID).then(res => {
                        if (res.StatusCode === '0') {
                            return res.DataResult
                        }
                    })

                    $scope.docData.model = model

                    d.resolve({ "Action": "VIEW" })

                    $scope.docData.DocAction = d.promise
                }
                else {
                    /*new*/

                    let currDate = $filter('date')(new Date(), 'MM/dd/yyyy')

                    let model = {
                        "TransID": -1,
                        "RefDate": currDate,
                        "RefDate2": currDate,
                        "BatchNum": $scope.batchNum,
                        "ObjType": $jeDocModel.ObjType,
                        "JournalEntry_Lines": []
                    }

                    d.resolve({ "Action": "ADD" })

                    $scope.docData.model = model

                    $scope.docData.DocAction = d.promise
                }

                $scope.onBtnCancelClick = function (e) {
                    $window.location.href = `#!/JVOBatchDetail/${args}`
                }

                $scope.$on('onDocumentSaveAction', function (e, args) {
                    let m = args.docHeader;
                    transID = m.TransID
                    if (args.docDetail.length > 0 && m.ObjType === 'JVO') {
                        m.JournalEntry_Lines = args.docDetail.filter(x => !(x.ShortName === '' && x.GLAcctCode === ''))
                    }

                    if (transID > 0) {
                        if ($cs.saveChanges(() => {
                            $jv.SaveOrUpdate(m).then(res => {
                                $scope.$broadcast('onDocumentSavePostAction', {
                                    'objType': 'JEN', // change this per doctype
                                    'data': d.DataResult
                                })
                                $window.location.href = `#!/JVOBatchDetail/${m.BatchNum}`
                            })
                        }));
                    }
                    else {
                        if ($cs.saveChanges(() => {
                            $jv.Add(m).then(function (d) {

                                if (d.StatusCode === '0') {
                                    $scope.$broadcast('onDocumentSavePostAction', {
                                        'objType': 'JEN', // change this per doctype
                                        'data': d.DataResult
                                    })
                                    $window.location.href = `#!/JVOBatchDetail/${d.DataResult.BatchNum}`
                                }
                            })
                        }));
                    }
                })
            }])
        //.directive('cbJeFormTmpl', ['$http', function ($http) {
        //    return {
        //        restrict: 'EA',
        //        controller: 'JENDetailCtrl',
        //        scope: {},
        //        link: function ($scope, $element, $attrs, $ctrl) { },
        //        //templateUrl: `JEFormTmpl.html`,
        //        templateUrl: `DocumentDetailTemplate.html`,
        //    }
        //}])
}());


(function () {
    let app = angular.module('MetronicApp')

    app.requires.push.apply(app.requires, ['ngRoute', 'JE.DOCUMENT'])

    app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
        $routeProvider
            .when('/JENDetail/:TransID', {
                templateUrl: 'DocumentDetailTemplate.html',
                controller: 'JENDetailCtrl'
            })

            .otherwise({
                templateUrl: 'DocumentDetailTemplate.html',
                controller: 'JENDetailCtrl'
            })

        $locationProvider.html5Mode(false).hashPrefix('!');
    }])
}());