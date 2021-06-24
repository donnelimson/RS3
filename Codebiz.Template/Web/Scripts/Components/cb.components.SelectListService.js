(function () {
    var SelectListService = function ($rootScope, $http, $q, $filter) {

        this.ChartOfAccountSL = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/FIN/ChartOfAccount/GetList',
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.ChartOfAccountByFieldValueCFL = function (q, v, st) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + `api/FIN/ChartOfAccount/GetList/${q}/${v}/${st}`,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.TaxGroupSL = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + `api/FIN/Setup/TaxGroup/GetLookup/${q}`,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetTaxGroupLookupByCategorySL = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + `api/FIN/Setup/TaxGroup/GetTaxGroupLookupByCategory/${q}`,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.ChartOfAccountSearchByText = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + `api/FIN/ChartOfAccount/GetList/SearchByText/${q}`,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.BusinessPartnerSL = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/BP/BusinessPartner/GetList',
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.PaymentGroupSL = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/BP/PaymentGroup/GetList/Lookup',
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetPaymentGroupByID = function (id) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + `api/BP/PaymentGroup/GetPaymentGroupByID/${id}`,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.ShippingTypeSL = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/Controller/ShippingTypes/GetList/Lookup',
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.ItemMasterSL = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/ITM/ItemMaster/GetList',
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.WarehouseSL = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/INVNTRY/Setup/Warehouse/GetSelectList',
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.ItemGroupSL = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/INVNTRY/Setup/ItemGroup/GetSelectList',
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.PriceListSL = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/INVNTRY/Setup/PriceList/GetPriceListSelectList',
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.CashDiscountLookupList = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/BusinessPartner/CashDiscount/GetLookupList',
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GLAccount = function (q) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + 'api/FIN/GetGLAccounts',
                data: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetCompanies = function (q) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + 'api/BP/Company/GetList',
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetBanks = function (data) {

            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + 'api/FIN/Banks/GetBankLookUp',
                data: data
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetHouseBankAccounts = function (data) {

            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + 'api/FIN/HouseBankAccount/GetHouseBankAccountLookUp',
                data: data
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetUnbundledTransactions = function (data) {

            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.BillingUnbundledTransaction + 'GetBillingUnbundledTransactionLookUpList',
                data: data
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetMemoFrom = function (data) {

            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.VHCTravelOrder + 'GetListLookUp',
                data: data
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetTravelOrder = function (data) {

            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.VHCTravelOrder + 'GetTravelOrderLookUp',
                data: data
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetDrivers = function (data) {

            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.VHCDrivers + 'GetListLookUp',
                data: data
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetCoopVehicles = function (data) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.VHCCoopVehiclesAPI + 'GetListLookUp',
                data: data
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetHouseBankLookup = function (q) {

            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + `api/BANK/HouseBank/GetHouseBankLookup?Page=${q.Page}&PageSize=${q.PageSize}&SortColumn=${q.SortBy}`,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;

        };

        this.GetProjectLookUp = function (args, id) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/FIN/Projects/GetList/Lookup?forInput=true',
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetIndustryLookUp = function (args, id) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/BusinessPartners/Industry/GetList/Lookup?forInput=true',
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };
    }
    angular.module("SL.DATASERVICE", [])
        .service("SelectListService", ['$rootScope', '$http', '$q', '$filter', SelectListService]);
}());
