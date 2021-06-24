(function () {

    let app = angular.module('TRN.INVNTRY.SERVICE', [])

    let IGEService = function ($rootScope, $http, $q, $filter) {

        this.GetList = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + `api/DOC/INVNTRY/GetList/IGE`,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetIGEByID = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + `api/DOC/INVNTRY/IGE/GetIGEByID/${q}`,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.Add = function (q) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + `api/DOC/INVNTRY/IGE/Add`,
                data: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.SaveOrUpdate = function (q) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + `api/DOC/INVNTRY/IGE/SaveOrupdate`,
                data: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.CloseIGE = function (q) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + `api/DOC/INVNTRY/CloseIGE`,
                data: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };
    }

    let IGNService = function ($rootScope, $http, $q, $filter) {

        this.GetList = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + `api/DOC/INVNTRY/GetList/IGN`,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetIGNByID = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + `api/DOC/INVNTRY/IGN/GetIGNByID/${q}`,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.Add = function (q) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + `api/DOC/INVNTRY/IGN/Add`,
                data: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.SaveOrUpdate = function (q) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + `api/DOC/INVNTRY/IGN/SaveOrupdate`,
                data: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.CloseIGN = function (q) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + `api/DOC/INVNTRY/CloseIGN`,
                data: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };
    }

    let InvntryTrnCommonService = function ($rootScope, $http, $q, $filter) {
        this.GetListBySearchCriteria = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + `api/DOC/INVNTRY/GetListBySearchCriteria`,
                params: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };
    }

    let InvntryTrnDocNumService = function ($rootScope, $http, $q, $filter) {
        this.GetDocNum = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + `api/DOC/INVNTRY/TRN/GetDocNum/${q}`,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };
    }

    let invntryTrnDocFormGrid = function ($rootScope, $http, $q, $filter) {
        this.GetColumns = function (docForm, docType) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + `api/DocUtil/DocFormGrid/GetColumns/INVNTRY/${docForm}/${docType}`,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        }
    }

    let IQIService = function ($rootScope, $http, $q, $filter) {

        this.GetList = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + `api/DOC/INVNTRY/GetList/IQI`,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetIQIByID = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + `api/DOC/INVNTRY/IQI/GetIQIByID/${q}`,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.Add = function (q) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + `api/DOC/INVNTRY/IQI/Add`,
                data: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.SaveOrUpdate = function (q) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + `api/DOC/INVNTRY/IQI/SaveOrupdate`,
                data: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

      
    }

    let IQCService = function ($rootScope, $http, $q, $filter) {

        this.GetList = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + `api/DOC/INVNTRY/GetList/IQC`,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetIQCByID = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + `api/DOC/INVNTRY/IQC/GetIQCByID/${q}`,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.Add = function (q) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + `api/DOC/INVNTRY/IQC/Add`,
                data: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.SaveOrUpdate = function (q) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + `api/DOC/INVNTRY/IQC/SaveOrupdate`,
                data: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };
    }

    let IQPService = function ($rootScope, $http, $q, $filter) {

        this.GetList = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + `api/DOC/INVNTRY/GetList/IQP`,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetIQPByID = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + `api/DOC/INVNTRY/IQP/GetIQPByID/${q}`,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };


        this.GetItemsForPosting = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + `api/DOC/INVNTRY/IQP/GetItems/${q}`,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.Add = function (q) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + `api/DOC/INVNTRY/IQP/Add`,
                data: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.SaveOrUpdate = function (q) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + `api/DOC/INVNTRY/IQP/SaveOrupdate`,
                data: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };
    }

    let IMVService = function ($rootScope, $http, $q, $filter) {

        this.GetList = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + `api/DOC/INVNTRY/GetList/IMV`,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetIMVByID = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + `api/DOC/INVNTRY/IQP/GetIQPByID/${q}`,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };
        
        this.Add = function (q) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + `api/DOC/INVNTRY/IMV/Add`,
                data: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.SaveOrUpdate = function (q) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + `api/DOC/INVNTRY/IMV/SaveOrupdate`,
                data: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };
    }

    app
        .service('invntry.docnum.service', ['$rootScope', '$http', '$q', '$filter', InvntryTrnDocNumService])
        .service('InvntryTrn.common.service', ['$rootScope', '$http', '$q', '$filter', InvntryTrnCommonService])
        .service('ige.service', ['$rootScope', '$http', '$q', '$filter', IGEService])
        .service('ign.service', ['$rootScope', '$http', '$q', '$filter', IGNService])
        .service('doc.formgrid.service', ['$rootScope', '$http', '$q', '$filter', invntryTrnDocFormGrid])
        .service('iqi.service', ['$rootScope', '$http', '$q', '$filter', IQIService])
        .service('iqc.service', ['$rootScope', '$http', '$q', '$filter', IQCService])
        .service('iqp.service', ['$rootScope', '$http', '$q', '$filter', IQPService])
        .service('imv.service', ['$rootScope', '$http', '$q', '$filter', IMVService])
        
        
}());