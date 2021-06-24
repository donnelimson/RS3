(function () {
    var WarehouseService = function ($rootScope, $http, $q, $filter) {

        this.GetList = function (q) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + 'api/INVNTRY/Setup/Warehouse/GetList',
                data: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetListLookup = function (q) {
            console.log(q);
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/INVNTRY/Setup/Warehouse/GetList/Lookup',
                data: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.SearchAccount = function (q) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + 'api/FIN/ChartOfAccount/GetList/' + q,
                data: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.ExportDataToExcelFile = function (q) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + 'api/INVNTRY/Setup/Warehouse/Export',
                data: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetWarehouseByID = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/INVNTRY/Setup/Warehouse/GetWarehouseByID/' + q,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetWarehouseByCode = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/INVNTRY/Setup/Warehouse/GetWarehouseByCode/' + q,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetWarehouseGLAccountSetup = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/INVNTRY/Setup/Warehouse/GetWarehouseGLAccountSetup/' + q,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetItemWarehouseLookupByWhsName = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/INVNTRY/Setup/Warehouse/GetItemWarehouseLookupByWhsName/' + q.Name + '/' + q.Id ,
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
                url: document.baseUrlNoArea + 'api/INVNTRY/Setup/Warehouse/Add',
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
                url: document.baseUrlNoArea + 'api/INVNTRY/Setup/Warehouse/SaveOrUpdate',
                data: q,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.UpdateStatus = function (q) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + 'api/INVNTRY/Setup/Warehouse/UpdateStatus',
                data: q,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.SearchWarehouseLookUp = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/INVNTRY/Setup/Warehouse/SearchWarehouseLookUp',
                params: q,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };
        
    };

    var ItemGroupService = function ($rootScope, $http, $q, $filter) {

        this.SearchItemGroup = function (q) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + 'api/INVNTRY/Setup/ItemGroup/GetList',
                data: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetListLookup = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/INVNTRY/Setup/ItemGroup/GetList/Lookup',
                data: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetOrderIntervalListLookup = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/INVNTRY/OrderInterval/OrderIntervalLookUp',
                data: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetCycleGroupListLookup = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/INVNTRY/CycleGroup/CycleGroupLookUp',
                data: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetProcurementMethodsLookup = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.ItemGroup + 'GetProcurementMethods',
                data: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetValuationMethodsLookup = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.ItemGroup + 'GetValuationMethods',
                data: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.CheckItemGroupName = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/INVNTRY/Setup/ItemGroup/CheckItemGroupName/{name}/{id}',
                params: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.CheckItemGroupCode = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/INVNTRY/Setup/ItemGroup/CheckItemGroupCode/{code}/{id}',
                params: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.ExportItemGroup = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.ItemGroup + 'ExportItemGroupList',
                params: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.CheckItemGroupIfCanDelete = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.ItemGroup + 'CheckItemGroupIfCanDelete',
                params: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetCurrentAppUserId = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.ItemGroup + 'GetCurrentAppUserId',
                params: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetItemGroupByCode = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/INVNTRY/Setup/ItemGroup/GetList/' + q,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };
        this.GetItemGroupById = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/INVNTRY/Setup/ItemGroup/GetById/' + q,
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
                url: document.baseUrlNoArea + 'api/INVNTRY/Setup/ItemGroup/Add',
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
                url: document.baseUrlNoArea + 'api/INVNTRY/Setup/ItemGroup/SaveOrUpdate',
                data: q,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.UpdateStatus = function (q) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + 'api/INVNTRY/Setup/ItemGroup/UpdateStatus',
                data: q,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetItemGroupLookUp = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.ItemGroup + 'GetItemGroupLookUp',
                params: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetItemGroupByID = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: `${document.baseUrlNoArea}api/INVNTRY/Setup/ItemGroup/GetItemGroupByID/${q}`,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        }

    };

    var ItemMasterService = function ($rootScope, $http, $q, $filter) {

        this.CheckIfItemIsInUse = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.ItemMasterData + 'CheckIfItemIsInUse',
                params: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetList = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/INVNTRY/Setup/ItemMaster/GetList',
                data:q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetAllItemMasterLookUp = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/INVNTRY/Setup/ItemMaster/GetList/GetAllItemMasterLookup',
                data: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        }; 

        this.GetSingleItemMasterLookUp = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.ItemMasterData + 'GetSingleItemMasterLookUp',
                params:q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };
 
        this.SearchItemMaster = function (q) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.ItemMasterDataAPI + 'GetItemMasterList',
                data: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.ExportDataToExcelFile = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.ItemMasterData + 'ExportDataToExcelFile',
                params: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.Delete = function (q) {
            var d = $q.defer();
            $http({
                method: 'post',
                url: document.ItemMasterData + 'Delete',
                data: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetItemByCode = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/INVNTRY/Setup/ItemMaster/GetListByCode/' + q,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetItemUnitOfMeasuresByItemId = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/INVNTRY/Setup/ItemMaster/GetItemUnitOfMeasures/' + q.id + '/' + q.type,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetWarehouseByCode = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/INVNTRY/Setup/ItemWarehouse/GetListByCode/' + q,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetItemWarehouseByCode = function (i,w) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: `${document.baseUrlNoArea}api/INVNTRY/Setup/ItemWarehouse/GetItemWarehouseByCode/${i}/${w}`,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetItemWarehouseList = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/INVNTRY/Setup/ItemWarehouse/GetList/{itemMasterId}/{wareHouseId}',
                params:q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetVendor = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/BP/BusinessPartner/GetVendorByNameOrCode/{searcher}',
                params: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetNextNum = function () {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/INVNTRY/Setup/ItemMaster/GetNextNum',
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetItemByID = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/INVNTRY/Setup/ItemMaster/GetListByID/' + q,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetRateByTaxGroupId = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + `api/FIN/Setup/TaxGroup/GetRateById/${q}`,
                params: q,
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
                url: document.baseUrlNoArea + 'api/INVNTRY/Setup/ItemMaster/Add',
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
                url: document.baseUrlNoArea + 'api/INVNTRY/Setup/ItemMaster/SaveOrUpdate',
                data: q,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.AddOrUpdate = function (q) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.ItemMasterData + 'AddOrUpdate',
                data: q,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };
 
        /*document related*/

        this.GetItemByCodeDocLookup = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + `api/INVNTRY/Setup/ItemMaster/GetItemByCodeDocLookup/${q}`,
                params: q,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };
        this.GetAvailableStockByItemAndWarehouseId = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/INVNTRY/Setup/ItemWarehouse/GetAvailableStockByItemAndWarehouseId/' + q.itemId + '/' + q.warehouseId,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        }
    };

    var PriceListService = function ($rootScope, $http, $q, $filter) {

        this.GetList = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/INVNTRY/Setup/PriceList/GetList',
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetListLookup = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/INVNTRY/Setup/PriceList/GetList/Lookup',
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetItemGroupByCode = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/INVNTRY/Setup/PriceList/GetList/' + q,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetItemGroupByID  = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.baseUrlNoArea + 'api/INVNTRY/Setup/PriceList/GetListByID/' + q,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.ComputeBasePrice = function (q) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + `api/INVNTRY/Setup/PriceList/ComputeBasePrice`,

                data: q,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetPriceListSelectList = function (q) {
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

        this.Add = function (q) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + 'api/INVNTRY/Setup/PriceList/Add',
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
                url: document.baseUrlNoArea + 'api/INVNTRY/Setup/PriceList/SaveOrUpdate',
                data: q,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.UpdateStatus = function (q) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + 'api/INVNTRY/Setup/PriceList/UpdateStatus',
                data: q,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };
        this.GetPriceListLookUp = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.PriceList + 'GetPriceListLookUp',
                params:q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetPriceListForItemMaster = function (q) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + 'api/INVNTRY/Setup/PriceList/GetPriceListForItemMaster',
                data: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };

        this.GetDefaultPriceOfItemMaster = function (q) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.PriceList + 'GetDefaultPriceOfItemMaster',
                data: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };



    };

    var InventoryTransferService = function ($rootScope, $http, $q, $filter) {

        this.SearchInventoryTransfer = function (q) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.baseUrlNoArea + 'api/INVNTRY/Setup/InventoryTransfer/GetList',
                data: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };
        this.CheckIfItemWarehouseExist = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.ItemMasterData + 'CheckIfItemWarehouseExist',
                params: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };
        this.ExportInventoryTransfer = function (q) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.InventoryTransfer + 'ExportInventoryTransferToExcel',
                data: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };
        this.Transfer = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.InventoryTransfer + 'TransferInventory',
                params: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };
        this.CanTransfer = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.InventoryTransfer + 'CanTransferInventory',
                params: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };
        this.DeleteInventoryTransfer = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.InventoryTransfer + 'Delete',
                params: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };
        this.InventoryTransferRecommendForApproval = function (q) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.InventoryTransfer + 'RecommendForApproval',
                data: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };
        this.SaveOrUpdateInventoryTransfer = function (q) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.InventoryTransfer + 'SaveOrUpdate',
                data: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };
        this.GetInventoryTransferDetail = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.InventoryTransfer + 'GetInventoryTransferById',
                params: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };
        this.GetItemWarehousListByWarehouseId = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.ItemMasterData + 'GetItemWarehousListByWarehouseId',
                params: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };
      
    };

    var InventoryReceivingService = function ($rootScope, $http, $q, $filter)
    {
        this.Search = function (q) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.InventoryReceiving + 'Search',
                data: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };
        this.DeleteInventoryReceiving = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.InventoryReceiving + 'Delete',
                params: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };
        this.GetItems = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.InventoryReceiving + 'GetItemInventoryTransfersByInventoryTransferAndWarehouse',
                params: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };
        this.SaveOrUpdateInventoryReceiving = function (q) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.InventoryReceiving + 'SaveOrUpdateInventoryReceiving',
                data: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };
        this.GetInventoryReceiving = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.InventoryReceiving + 'GetInventoryReceivingById',
                params: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };
        this.ExportInventoryReceiving = function (q) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.InventoryReceiving + 'ExportInventoryReceivingToExcel',
                data: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };
        this.InventoryReceivingRecommendForApproval = function (q) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: document.InventoryReceiving + 'RecommendForApproval',
                data: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };
        this.CanReceive = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.InventoryReceiving + 'CanReceiveInventoryReceiving',
                params: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };
        this.Receive = function (q) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: document.InventoryReceiving + 'ReceiveInventory',
                params: q
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        };
        
    };

    var ItemSerialTransactionService = function ($rootScope, $http, $q, $filter) {

        this.GetAvailableSerialNo = (q) => {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: `${document.baseUrlNoArea}api/INVNTRY/Transaction/ItemSerialNumber/GetAvailableSerialNo/${q}`,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        }
    }

    var ItemPackagingService = function ($rootScope, $http, $q, $filter) {
        this.GetItemPackagingLookup = (itemCode, uomType) => {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: `${document.baseUrlNoArea}api/INVNTRY/Setup/ItemMaster/GetItemPackagingLookup/${uomType}/${itemCode}`,
            })
                .then(function successCallback(response) {
                    d.resolve(response.data);
                }, function errorCallback(error) {
                    d.reject(error);
                });
            return d.promise;
        }
    }

    angular.module("INVNTRY.DATASERVICE", [])
        .service("WarehouseService", ['$rootScope', '$http', '$q', '$filter', WarehouseService])
        .service("ItemGroupService", ['$rootScope', '$http', '$q', '$filter', ItemGroupService])
        .service('ItemMasterService', ['$rootScope', '$http', '$q', '$filter', ItemMasterService])
        .service('PriceListService', ['$rootScope', '$http', '$q', '$filter', PriceListService])
        .service('InventoryTransferService', ['$rootScope', '$http', '$q', '$filter', InventoryTransferService])
        .service('InventoryReceivingService', ['$rootScope', '$http', '$q', '$filter', InventoryReceivingService])
        .service('ItemSerialTransactionService', ['$rootScope', '$http', '$q', '$filter', ItemSerialTransactionService])
        .service('ItemPackagingService', ['$rootScope', '$http', '$q', '$filter', ItemPackagingService]);
}());