/*
 Created by: CJ Felix
 Date Created: 1/18/2018
 Date Modified: 
 Modified By: 
 */
(function () {
    'use strict';

    angular.module('MetronicApp').factory('HttpServices', HttpServices);

    HttpServices.$inject = ['$http'];

    function HttpServices($http) {

        var helperServices = {           
            post: post,          
            get: get
        };

        return helperServices;

        function get(strUrl, params) {

            return $http({
                method: 'GET',
                url: strUrl,
                params: params,
                cache: false
            }).then(onSuccess)
                .catch(onError);
        }     

        function post(strUrl, dataObject) {

            return $http({
                method: 'POST',
                url: strUrl,
                data: dataObject,
                headers: { 'Content-Type': 'application/json' }
            })
                .then(onSuccess)
                .catch(onError);
        }     

        function onSuccess(response) {
            return response;
        }

        function onError(response) {
            console.log(response);
            return response;
        }
    }

})();