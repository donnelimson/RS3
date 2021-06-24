(function () {
    var app = angular.module(
        'JV.DOCUMENT', [
        'cb.components.linktextbox',
        'cb.components.SelectList',
        'cb.je.doctemplate',
        'cb.je.FormGrid',
        'CFL.DATASERVICE',
        'BP.SETUP.SERVICE',
        'FIN.DOC.FACTORY'
    ])
    app
        .controller('JENBatchListCtrl', [
            '$scope',
            '$rootScope',
            '$uibModal',
            '$window',
            '$timeout',
            '$q',
            'CommonService',
            'NgTableParams',
            'jvo.service',
            'jen.service',
            'jen.doc.model',
            function ($scope, $rootScope, $uibModal, $window, $timeout, $q, $cs, $ngTable, $jvService, $jeService, $jeDocModel) {

                $q.all([
                    $jvService.GetJVOBatchList('A').then((res) => res.DataResult)
                ]).then(function (res) {

                    $scope.tableParams = new $ngTable(10, { dataset: res[0] });

                    $scope.onViewClick = function (item) {
                        $window.location.href = `#!/JVOBatchDetail/${item.BatchNum}`
                    }

                    $scope.onEditClick = function (item) {
                        $window.location.href = `#!/JVOBatchDetail/${item.BatchNum}`
                    }

                    $scope.searchWhenEnter = function (e) {
                        if (e.keyCode === 13) {
                            $purCommon.GetListBySearchCriteria($scope.m).then(res => {
                                let d = res.DataResult
                                if (res.StatusCode === '0') {
                                    $scope.tableParams = new $ngTable(10, { dataset: d });
                                }
                            })
                        }
                    }
                })
            }])
        .controller('JENListCtrl', [
            '$scope',
            '$rootScope',
            '$routeParams',
            '$uibModal',
            '$window',
            '$timeout',
            '$q',
            'CommonService',
            'NgTableParams',
            'jvo.service',
            'jen.service',
            'jen.doc.model',
            function ($scope, $rootScope, $routeParams, $uibModal, $window, $timeout, $q, $cs, $ngTable, $jvService, $jeService, $jeDocModel) {

                self = this

                $scope.batchNum = $routeParams.BatchNum

                if ($routeParams.TransID) 
                    $scope.transID = $routeParams.TransID;
                 else 
                    $scope.transID = -1

                if ($scope.batchNum) {

                    $q.all([
                        $jvService.GetJVBatchByBatchNum($scope.batchNum).then((res) => res.DataResult)
                    ]).then(function (res) {

                        if ($scope.batchNum > 0) {
                            $scope.m = res[0]
                            $scope.tableParams = new $ngTable(10, { dataset: res[0].JournalVouchers });
                        }
                        else {
                            $scope.m = {
                                'ObjType': $jeDocModel.ObjType,
                                'BatchNum': $scope.batchNum,
                                'Status': 'O'
                            }
                        }

                        

                        $scope.onViewClick = function (item) {

                            $window.location.href = `#!/JVODetail/${$scope.batchNum}/${item.TransID}`
                        }

                        $scope.onEditClick = function (item) {
                            $window.location.href = `#!/JVODetail/${$scope.batchNum}/${item.TransID}`
                        }

                        $scope.searchWhenEnter = function (e) {
                            if (e.keyCode === 13) {
                                $jvService.GetListBySearchCriteria($scope.m).then(res => {
                                    let d = res.DataResult
                                    if (res.StatusCode === '0') {
                                        $scope.tableParams = new $ngTable(10, { dataset: d });
                                    }
                                })
                            }
                        }
                    })
                }

                $scope.onBtnNewClick = function (e) {
                    $window.location.href = `#!/JVODetail/${$scope.batchNum}/${$scope.transID}`
                }

                $scope.onBtnSaveClick = function (e) {
                    if ($cs.saveChanges(() => {
                        $jvService.SaveOrUpdateJVB($scope.m).then((res) => {
                            if (res.StatusCode === '0') {
                                $window.location.href = `#!/JVOBatchList`
                            }
                        })
                    }));
                }

                $scope.onBtnPostVoucherClick = function (e) {
                    if ($cs.postTransactions(() => {
                        $scope.m.Status = 'C'
                        $jvService.SaveOrUpdateJVB($scope.m).then((res) => {
                            if (res.StatusCode === '0') {
                                $window.location.href = `#!/JVOBatchList`
                            }
                        })
                    }));

                }

                $scope.onBtnCancelClick = function (e) {
                    $window.location.href = `#!/JVOBatchList`
                }
            }])
        .controller('JENDetailCtrl', [
            '$scope',
            '$rootScope',
            '$routeParams',
            '$location',
            '$filter',
            '$window',
            '$q',
            'CommonService',
            'BusinessPartnerService',
            'jvo.service',
            'jen.service',
            'jen.doc.model',
            function ($scope, $rootScope, $routeParams, $location, $filter, $window, $q, $cs, $bp, $jv, $je, $jeDocModel) {

                $scope.transID = $routeParams.TransID

                $scope.batchNum = $routeParams.BatchNum 

                $scope.docData = {}

                let d = $q.defer()

                if ($scope.transID > 0) {

                    /*edit*/

                    let model = $jv.GetJVOByID($scope.transID).then(res => {
                        if (res.StatusCode === '0') {
                            return res.DataResult
                        }
                    })

                    //d.resolve([model, { 'DocNumData': docnumData }])

                    $scope.docData.model = model

                    d.resolve({ "Action": "EDIT" })

                    $scope.docData.DocAction = d.promise
                }
                else {
                    /*new*/

                    let currDate = $filter('date')(new Date(), 'MM/dd/yyyy')

                    let model = {
                        "TransID": -1,
                        "RefDate": currDate,
                        "RefDate2": currDate,
                        "RefDate3": currDate,
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

                $scope.$on('onJEDocSaveAction', function (e, args) {
                    let m = args.docHeader;
                    transID = m.TransID
                    if (args.docDetail.length > 0 && m.ObjType === 'JVO') {
                        m.JournalEntry_Lines = args.docDetail.filter(x => !(x.ShortName === '' && x.GLAcctCode === ''))
                    }

                    if (transID  > 0) {
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

                $scope.$on('onJEDocCancelAction', function (e, args) {
                    $window.location.href = `#!/JVOBatchDetail/${args.BatchNum}`
                })
            }])
}());


(function () {
    let app = angular.module('MetronicApp')

    app.requires.push.apply(app.requires, ['ngRoute', 'JV.DOCUMENT'])

    app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
        $routeProvider
            .when('/JVOBatchList', {
                templateUrl: 'JVBatchList.html',
                controller: 'JENBatchListCtrl'
            })

            .when('/JVOBatchDetail/:BatchNum', {
                templateUrl: 'DocumentListTemplate.html',
                controller: 'JENListCtrl'
            })

            .when('/JVODetail/:BatchNum/:TransID', {
                templateUrl: 'DocumentDetailTemplate.html',
                controller: 'JENDetailCtrl'
            })

            .otherwise({
                templateUrl: 'JVBatchList.html',
                controller: 'JENBatchListCtrl'
            })

        $locationProvider.html5Mode(false).hashPrefix('!');

    }])
}());