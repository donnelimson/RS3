(function () {

    const invntry_glsetup_list = [

        { 'Code': 'ExpenseAcct', 'Name': 'Expense Account', 'AcctCode': '', 'AcctName': '', 'IsRequired': true },
        { 'Code': 'RevenueAcct', 'Name': 'Revenue Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'InvntryAcct', 'Name': 'Inventory Account', 'AcctCode': '', 'AcctName': '', 'IsRequired': true },
        { 'Code': 'COGSAcct', 'Name': 'Cost of Goods Sold Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'AllocationAcct', 'Name': 'Allocation Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'VarianceAcct', 'Name': 'Variance Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'PriceDiffAcct', 'Name': 'Price Difference Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'NegInvntryAcct', 'Name': 'Negative Inventory Addjustment Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'InvntryOffsetDecAcct', 'Name': 'Inventory Offset - Decrease Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'InvntryOffsetIncAcct', 'Name': 'Inventory Offset - Increase Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'SalReturns', 'Name': 'Sales Returns Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'RevenueAcctF', 'Name': 'Revenue Account - Foreign', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'ExpenseAcctF', 'Name': 'Expense Account - Foreign', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'ExhangeDiffAcct', 'Name': 'Exchange Rate Difference Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'GoodClearingAcct', 'Name': 'Goods Clearing Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'GLOffsetDecAcct', 'Name': 'G/L Decrease Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'GLOffsetIncAcct', 'Name': 'G/L Increase Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'WIPInvntryAcct', 'Name': 'WIP Inventory Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'WIPInvntryVarianceAcct', 'Name': 'WIP Inventory Variance Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'ExpenseClearingAcct', 'Name': 'Expense Clearing Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'SalesCreditAcct', 'Name': 'Sales Credit Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'PurchaseCreditAcct', 'Name': 'Purchase Credit Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'SalesCreditAcctF', 'Name': 'Sales Credit Account - Foreign', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'PurchaseCreditAcctF', 'Name': 'Purchase Credit Account - Foreign', 'AcctCode': '', 'AcctName': '' },
    ];

    const invntry_item_code_generate_type = [
        { 'Code': 'A', 'Name': 'Auto' },
        { 'Code': 'M', 'Name': 'Manual' }
    ];

    const invntry_item_types = [
        { 'Code': 'I', 'Name': 'Item' },
        { 'Code': 'L', 'Name': 'Labor' },
        { 'Code': 'T', 'Name': 'Travel' }
    ];

    const invntry_item_manage_by = [
        { 'Code': 'N', 'Name': 'None' },
        { 'Code': 'S', 'Name': 'Serial Numbers' },
        { 'Code': 'B', 'Name': 'Batch' }
    ];

    const invntry_item_manage_method = [
        { 'Code': 'E', 'Name': 'On Every Transaction' },
        { 'Code': 'R', 'Name': 'On Release Transaction' }
    ];



    const invntry_item_gl_method = [
        { 'Code': 'G', 'Name': 'Item Group' },
        { 'Code': 'W', 'Name': 'Warehouse' },
        { 'Code': 'I', 'Name': 'Item Level' }
    ];


    const invntry_item_valuation_method = [
        { 'Code': 'S', 'Name': 'Standard' },
        { 'Code': 'F', 'Name': 'FIFO' },
        { 'Code': 'M', 'Name': 'Moving Average' }
    ];

    const invntry_item_management_method = [
        { 'Code': 'N', 'Name': 'None' },
        { 'Code': 'OET', 'Name': 'On Every Transaction' },
        { 'Code': 'ORO', 'Name': 'On Release Only' }
    ];

    const invntry_uom_types = [
        { 'Code': 'INVNTRY', 'Name': 'Inventory' },
        { 'Code': 'SAL', 'Name': 'Sales' },
        { 'Code': 'PUR', 'Name': 'Purchasing' }
    ];

    angular.module('INVNTRY.MODELS', [])
        .factory('Warehouse.GL.Model', function () {
            return {
                'WarehouseGLSetupList': invntry_glsetup_list
            };
        }).factory('ItemGroup.GL.Model', function () {
            return {
                'ItemGroupGLSetupList': invntry_glsetup_list
            };
        }).factory('ItemMaster.Model', function () {
            return {

                'ItemTypeList' : invntry_item_types,

                'ItemCodeGenerateTypeList' : invntry_item_code_generate_type,

                'ItemManageByList': invntry_item_manage_by,

                'ItemManageMethodList': invntry_item_manage_method,

                'ItemGLMethodList': invntry_item_gl_method,

                'ItemValuationMethodList': invntry_item_valuation_method,

                'ItemManagementMethodList': invntry_item_management_method,
                'ItemPackagingUOMTypes': invntry_uom_types
            };
        });
}());