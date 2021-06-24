(function () {
    //angular.module("JE_APP", ['FIN.DATASERVICE', 'FIN.MODELS'])

    'use strict';
    var app = angular.module('MetronicApp')
    app.requires.push.apply(app.requires, ['FIN.DATASERVICE', 'FIN.MODELS']);
}());