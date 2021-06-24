(function () {
    let CoaService = function ($rootScope, $http, $q, $filter) {

        this.GetGLAccountList = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/FIN/ChartOfAccount/GetChartOfAccountList',
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        }

        this.GetGLAccountListByGroupMask = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/FIN/ChartOfAccount/GetChartOfAccountList/' + q,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        }

        this.GetListSearch = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/FIN/ChartOfAccount/GetListSearch',
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        }

        this.GetAccountByAcctCodeSearch = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/FIN/ChartOfAccount/GetAccountByAcctCodeSearch/' + q,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        }

        this.GetGLAccountPostableList = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/FIN/ChartOfAccount/GetChartOfAccountPostableList',
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetGLAccountByAcctCode = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/FIN/ChartOfAccount/GetChartOfAccountByAcctCode/' + q,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.AddGLAccount = function (gl) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + 'api/FIN/ChartOfAccount/Add',
                data: gl,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.SaveOrUpdateGLAccount = function (gl) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + 'api/FIN/ChartOfAccount/SaveOrUpdate',
                data: gl,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };
    };

    let TaxGroupService = function ($rootScope, $http, $q, $filter) {

        this.GetList = function (args) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + 'api/FIN/Setup/TaxGroup/GetList',
                data: args,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetTaxGroupByID = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + `api/FIN/Setup/TaxGroup/GetTaxGroupByID/${q}`,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetTaxGroupByCode = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/FIN/Setup/TaxGroup/GetTaxGroupByCode/' + q,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetTaxGroupListByCategory = function (q) {
            console.log(q)
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/FIN/Setup/TaxGroup/GetTaxGroupListByCategory/' + q,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };
        this.GetTaxGroupInputLookUp = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/FIN/Setup/TaxGroup/GetTaxGroupInputLookUp/' + q,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };


        this.Add = function (data) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + 'api/FIN/Setup/TaxGroup/Add',
                data: data,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.SaveOrUpdate = function (data) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + 'api/FIN/Setup/TaxGroup/SaveOrUpdate',
                data: data,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.ExportDataToExcelFile = function (data) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + 'api/FIN/Setup/TaxGroup/Export',
                data: data,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.DeleteTaxGroup = function (data) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/FIN/TaxGroup/DeleteTaxGroup/' + data.id,
                params: data,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.ToggleStatus = function (data) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/FIN/TaxGroup/ToggleTaxGroupActiveStatus/' + data.id,
                params: data,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

    };

    let JENService = function ($rootScope, $http, $q, $filter) {

        this.GetList = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/FIN/JEN/GetList',
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        }

        this.Add = function (je) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + 'api/FIN/JEN/Add',
                data: je,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.SaveOrUpdate = function (je) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + 'api/FIN/JEN/SaveOrUpdate',
                data: je,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetJENByID = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + `api/FIN/TRN/JEN/GetJENByID/${q}`,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        }

    };

    let JVOService = function ($rootScope, $http, $q, $filter) {

        this.GetList = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/FIN/TRN/JVO/GetList',
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        }

        this.GetJVOBatchList = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + `api/FIN/TRN/JVO/GetJVOBatchList/${q}`,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        }

        this.GetJVOListByBatchNum = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + `api/FIN/TRN/JVO/GetJVOListByBatchNum/${q}`,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        }

        this.GetJVBatchByBatchNum = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + `api/FIN/TRN/JVO/GetJVBatchByBatchNum/${q}`,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        }

        this.GetJVOByID = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + `api/FIN/TRN/JVO/GetJVOByID/${q}`,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        }

        this.Add = function (je) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + 'api/FIN/TRN/JVO/Add',
                data: je,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.SaveOrUpdate = function (je) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + 'api/FIN/TRN/JVO/SaveOrUpdate',
                data: je,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.SaveOrUpdateJVB = function (jb) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + `api/FIN/TRN/JVB/SaveOrUpdate`,
                data: jb,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        }
    };

    var PostingPeriodService = function ($rootScope, $http, $q, $filter) {

        this.GetList = function (data) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/FIN/Setup/PostingPeriod/GetList',
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.CreatePostingPeriod = function (data) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + 'api/FIN/Setup/PostingPeriod/CreatePostingPeriod',
                data: data
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.Add = function (d) { };

        this.SaveOrUpdate = function (d) { };

        this.GetFinancialPeriodDetailList = function () {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/FIN/Setup/PostingPeriod/GetFinancialPeriodDetailList',
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetCurrentFinancialPeriod = function () {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: `${document.baseUrlNoArea}/api/FIN/Setup/GLAccountSetup/GetCurrentFinancialPeriod`,
                })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };


    };

    var GLSetupService = function ($rootScope, $http, $q, $filter) {

        this.GetFinancialPeriodByCode = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/FIN/Setup/GLAccountSetup/GetFinancialPeriodByCode/' + q,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };



        this.GetFinancialPeriodLookupList = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/FIN/Setup/GLAccountSetup/GetFinancialPeriodLookupList',
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
                url: document.baseUrlNoArea + 'api/FIN/Setup/GLAccountSetup/Add',
                data: q,
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
                url: document.baseUrlNoArea + 'api/FIN/Setup/GLAccountSetup/SaveOrUpdate',
                data: q,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };
    };

    let JENCommonService = function ($rootScope, $http, $q, $filter) {
        this.GetDocNum = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + `api/DOC/JEN/GetDocNum/${q}`,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };
    }

    let JENDocNumService = function ($rootScope, $http, $q, $filter) {
        this.GetDocNum = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + `api/DOC/JVO/GetDocNum/${q}`,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };
    }

    let docFormGrid = function ($rootScope, $http, $q, $filter) {
        this.GetColumns = function (docForm, docType) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + `api/DocUtil/DocFormGrid/GetColumns/JEN/${docForm}/${docType}`,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        }
    };

    //#region Transaction Code

    let TransactionControlNumberService = function ($rootScope, $http, $q, $filter) {

        this.GetList = function (args) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + 'api/FIN/TCN/GetList',
                data: args,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.ExportDataToExcelFile = function (data) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + 'api/FIN/TCN/Export',
                data: data,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.DeleteTCN = function (data) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/FIN/TCN/DeleteTCN/' + data.id,
                params: data,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.ToggleStatus = function (data) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/FIN/TCN/ToggleStatus/' + data.id,
                params: data,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.CheckTCNName = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/FIN/TCN/CheckTCNName/{name}/{id}',
                params: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.CheckTCNCode = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/FIN/TCN/CheckTCNCode/{code}/{id}',
                params: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetTCNById = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/FIN/TCN/GetTCNById/' + q,
                params: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetAccountByAcctCodeSearch = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/FIN/TCN/GetAccountByAcctCodeSearch/' + q,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        }

        this.GetTCNByCode = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.TransactionControlNumber + 'GetTCNByCode',
                params: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        }

        this.Add = function (q) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + 'api/FIN/TCN/Add',
                data: q,
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
                url: document.baseUrlNoArea + 'api/FIN/TCN/SaveOrUpdate',
                data: q,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };
    };

    //#endregion

    let financialReportService = function ($rootScope, $http, $q, $filter) {
        this.GetTransactionJournalReport = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + `api/FIN/TRN/JEN/TransactionJournalReport`,
                params: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;

        }

        this.GetPNLReport = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + `api/FIN/TRN/JEN/PNLReport`,
                params: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;

        }
    }

    let internalReconService = function ($rootScope, $http, $q, $filter) {

        this.GetITRByID = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + `api/FIN/TRN/ITR/GetITRByID/${q}`,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        }

        this.GetITRLinesByID = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + `api/FIN/TRN/ITR/GetITRLinesByID/${q}`,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        }

        this.GetITRList = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + `api/FIN/TRN/ITR/GetITRList`,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        }

        this.GetOpenInvoiceBP = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + `api/FIN/TRN/ITR/GetOpenInvoiceBP`,
                params: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        }


        this.GetOpenTransactionGL = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + `api/FIN/TRN/ITR/GetOpenTransactionGL`,
                params: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        }

        this.Add = function (je) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + 'api/FIN/TRN/ITR/Add',
                data: je,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.SaveOrUpdate = function (je) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + 'api/FIN/TRN/ITR/SaveOrUpdate',
                data: je,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.Cancel = function (reconId) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + 'api/FIN/TRN/ITR/Cancel',
                data: { "reconID": reconId },
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };
    }

    let wtaxService = function ($rootScope, $http, $q, $filter) {

        this.GetWtaxById = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + `api/FIN/Setup/Wtax/GetWtaxById/${q}`,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        }

        this.Add = function (je) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + 'api/FIN/Setup/Wtax/Add',
                data: je,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.SaveOrUpdate = function (je) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + 'api/FIN/Setup/Wtax/SaveOrUpdate',
                data: je,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };
    }

    let ProjectService = function (CommonService) {
        this.SearchFinancialProjects = function (args) {
            return CommonService.PostData(args, document.baseUrlNoArea + 'api/FIN/Projects/GetList');
        }
        this.ExportDataToExcelFile = function (args) {
            return CommonService.GetData(args, document.FinancialProjects + 'ExportDataToExcelFile');
        }
        this.AddUpdate = function (args, add) {
            if (add) {
                return CommonService.PostData(args, document.FinancialProjects + 'Add');
            }
            else {
                return CommonService.PostData(args, document.FinancialProjects + 'Update');
            }
        }
        this.Delete = function (args) {
            return CommonService.PostData(args, document.FinancialProjects + 'Delete');
        }
        this.GetDetailsForUpdate = function (args, id) {
            return CommonService.GetData(args, document.baseUrlNoArea + 'api/FIN/Projects/GetDetailsForUpdate/' + id);
        }
    }

    angular.module("FIN.DATASERVICE", [])
        .service("CoaService", ['$rootScope', '$http', '$q', '$filter', CoaService])
        .service("TaxGroupService", ['$rootScope', '$http', '$q', '$filter', TaxGroupService])
        .service("WTaxService", ['$rootScope', '$http', '$q', '$filter', wtaxService])
        .service("PostingPeriodService", ['$rootScope', '$http', '$q', '$filter', PostingPeriodService])
        .service('jen.service', ['$rootScope', '$http', '$q', '$filter', JENService])
        .service('jvo.service', ['$rootScope', '$http', '$q', '$filter', JVOService])
        .service('jen.common.service', ['$rootScope', '$http', '$q', '$filter', JENCommonService])
        .service('jen.docnum.service', ['$rootScope', '$http', '$q', '$filter', JENDocNumService])
        .service('GLSetupService', ['$rootScope', '$http', '$q', '$filter', GLSetupService])
        .service('jen.formgrid.service', ['$rootScope', '$http', '$q', '$filter', docFormGrid])
        .service("TransactionControlNumberService", ['$rootScope', '$http', '$q', '$filter', TransactionControlNumberService])
        .service("fin.report.service", ['$rootScope', '$http', '$q', '$filter', financialReportService])
        .service("fin.itr.service", ['$rootScope', '$http', '$q', '$filter', internalReconService])
        .service("ProjectService", ['CommonService', ProjectService])
}());