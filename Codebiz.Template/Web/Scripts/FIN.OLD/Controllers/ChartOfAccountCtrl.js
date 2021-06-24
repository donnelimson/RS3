(function () {
    let app = angular.module('COA.SETUP', ['FIN.DATASERVICE','FIN.MODELS'])
    let ctrl = function ($scope, $uib, $coa, $coaModels, $cs) {

        //$coa.GetGLAccountList().then(function (res) {
        //    $scope.treeModel = res.DataResult;
        //});
        $scope.cflResult = {}


        $scope.AcctTypeList = $coaModels.AccountTypeList
        $scope.AccountLevelList = $coaModels.AccountLevelList
        $scope.recordMode = 'B'

        var openCFL = function () {

            $uib.open({
                templateUrl: 'ChooseFromListPartial.html',
                controller: 'ChooseFromListCtrl',
                controllerAs: 'ctrl',
                windowClass: 'clsPopup',
                backdrop: 'static',
                keyboard: false,
                resolve: {
                    modalData: function () {
                        return {
                            'LookupType': 'ACT',
                            'Title': 'Choose From List (G/L Accounts)',
                            'SearchBy': 'Postable',
                            'SearchValue': 'N'
                        };
                    }
                } // data passed to the controller
            }).result
                .then(function (d) {
                    $scope.cflResult = d;
                }, function () { });
        };

        $scope.$watch('cflResult', function () {
            $scope.m.FatherCode = $scope.cflResult.AcctCode;
        });

        $coa.GetGLAccountListByGroupMask('1').then(function (res) {
            $scope.treeModel = res.DataResult;
            $scope.selectedGroupMask = '1';
        });

        $scope.m = {};

        $scope.readyCB = function () {
            $('#jstree1').jstree("open_all");

        };

        $scope.changedCB = function (e, data) {
            $scope.m.AcctCode = data.selected[0];
            //console.log(data);
            $coa.GetGLAccountByAcctCode($scope.m.AcctCode)
                .then((d) => {
                    $scope.m = d.DataResult;
                    //$scope.recordMode = 'E';
                    if (!$scope.$$phase) {
                        $scope.$apply();
                    }
                });
        };

        $scope.openNodeCB = function (e, data) {
            //console.log('open-node event call back');
        };

        $scope.onPostableChange = function (s) {
            $scope.m.Postable = s;
            if (!$scope.$$phase) {
                $scope.$apply();
            }
        };

        $scope.onSearchTree = (e) => {
            if (e.keyCode === 9 || e.keyCode === 13) {   // tab key
                $('#jstree1').jstree(true).search(e.target.value);
                e.preventDefault(); // dont exec ui focus
            }
        };

        $scope.contextMenu = {
            "mnCtxAddSameLvl": {
                "label": "Add Same Level Account",
                "action": function (o) {
                    $scope.m.AcctCode = '';
                    $scope.m.AcctName = '';
                    $scope.GroupMask = $scope.selectedGroupMask;
                    $scope.recordMode = 'A';
                    if (!$scope.$$phase) {
                        $scope.$apply();
                    }
                }
            },

            "mnCtxAddSubLvl": {
                "label": "Add Sub-Level Account",
                "action": function (o) {
                    $scope.m.FatherCode = $scope.m.AcctCode 
                    $scope.m.Level = $scope.m.Level + 1 
                    $scope.m.AcctCode = '';
                    $scope.m.AcctName = '';
                    $scope.GroupMask = $scope.selectedGroupMask;
                    $scope.recordMode = 'A';
                    if (!$scope.$$phase) {
                        $scope.$apply();
                    }
                }
            },
            "mnCtxEdit": {
                "label": "Edit G/L Account",
                "action": function (o) {
                    $scope.recordMode = 'E'
                    if (!$scope.$$phase) {
                        $scope.$apply();
                    }
                }
            },
            "mnCtxMoveTo": {
                "label": "Move To",
                "action": function (o) {
                }
            }
        };

        $scope.onGroupMaskChange = function (g) {
            $coa.GetGLAccountListByGroupMask(g).then(function (res) {
                $scope.treeModel = res.DataResult;
                $scope.selectedGroupMask = g;

            });
        };

        $scope.onSearchParentAccountButtonClick = function (s, e, d) {
            openCFL(d);
        };

        $scope.onbtnNewClick = () => {
            $scope.m.AcctCode = '';
            $scope.m.AcctName = '';
            $scope.GroupMask = $scope.selectedGroupMask;
            $scope.recordMode = 'A';
        };

        $scope.onBtnSave = function ($event) {

            $cs.saveChanges(function () {
                if ($scope.recordMode === 'A') {
                    $coa.AddGLAccount($scope.m).then(function (d) {
                            if (d.StatusCode === '0') {
                                $cs.successMessage('G/L Account Added Sucessfully');
                                $coa.GetGLAccountListByGroupMask($scope.selectedGroupMask).then(function (res) {
                                    $scope.treeModel = res.DataResult;
                                    $scope.recordMode = 'B';
                                });
                            }
                        });
                }
                if ($scope.recordMode === 'E') {
                    $coa.SaveOrUpdateGLAccount($scope.m).then(function (d) {
                            if (d.StatusCode === '0') {
                                $cs.successMessage('G/L Account Updated Sucessfully');
                                $coa.GetGLAccountListByGroupMask($scope.selectedGroupMask).then(function (res) {
                                    $scope.treeModel = res.DataResult;
                                    $scope.recordMode = 'B';
                                });
                            }
                        });
                }
                $event.preventDefault();
            });
        };
    };

    app.controller("ChartOfAccountCtrl", ['$scope', '$uibModal', 'CoaService','COA.SETUP.MODEL', 'CommonService',  ctrl])
}());

//(function () {
//    let app = angular.module('MetronicApp')
//    app.requires.push.apply(app.requires, ['ngRoute', 'COA.SETUP'])
//}());