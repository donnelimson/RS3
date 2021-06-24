(function () {
    //angular.module("FIN_APP", ['jsTree.directive', 'FIN.DATASERVICE'])
    'use strict';
    var app = angular.module('MetronicApp')
    app.requires.push.apply(app.requires, ['jsTree.directive','COA.SETUP', 'FIN.DATASERVICE', 'COA.SETUP'])
}());
