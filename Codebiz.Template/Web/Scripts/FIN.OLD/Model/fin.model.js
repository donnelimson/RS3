(function () {

    const general_gl_list = [
        { 'Code': 'CCDepositFee', 'Name': 'Credit Card Deposit Fee', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'RoundingAcct', 'Name': 'Rounding Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'AutoReconDiffAcct', 'Name': 'Automatic Reconcilation Difference', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'PeriodCloseAcct', 'Name': 'Period End Closing Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'RealizedExchangeDiffGainAcct', 'Name': 'Realized Exchange Diff Gain', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'RealizedExchangeDiffLossAcct', 'Name': 'Realized Exchange Diff Loss', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'OpenningBalanceAcct', 'Name': 'Opening Balance Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'BankChargeAcct', 'Name': 'Bank Charges', 'AcctCode': '', 'AcctName': '' }
    ];
    const invntry_gl_setup_list = [
        { 'Code': 'InvntryAcct', 'Name': 'Inventory Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'COGSAcct', 'Name': 'Cost Of Goods Sold Account(COGS)', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'AllocationAcct', 'Name': 'Allocation Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'VarianceAcct', 'Name': 'Variance Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'PriceDiffAcct', 'Name': 'Price Difference Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'NegInvntryAdjAcct', 'Name': 'Negative Inventory Adjustment Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'InvntryOffsetDecAcct', 'Name': 'Inventory Offset Decrease Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'InvntryOffsetIncAcct', 'Name': 'Inventory Offset Increase Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'SalesReturnAcct', 'Name': 'Sales Returns Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'ExchangeDiffAcct', 'Name': 'Exchange Rate Differences Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'GoodsClearingAcct', 'Name': 'Goods Clearing Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'GLDecAcct', 'Name': 'G/L Decrease Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'GLIncAcct', 'Name': 'G/L Increase Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'WIPInvntryAcct', 'Name': 'WIP Inventory Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'WIPInvntryVarianceAcct', 'Name': 'WIP Inventory Variance Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'ExpenseClearingAcct', 'Name': 'Expense Clearing Account', 'AcctCode': '', 'AcctName': '' }
    ];
    const ar_gl_setup_list = [
        { 'Code': 'DomesticReceivableAcct', 'Name': 'Domestic Account Receivable', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'ForeignReceivableAcct', 'Name': 'Foreign Account Receivable', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'ChecksReceivedAcct', 'Name': 'Checks Received', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'CashOnHandAcct', 'Name': 'Cash On Hand', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'OverPaymentAcct', 'Name': 'OverPayment A/R Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'UnderPaymentAcct', 'Name': 'UnderPayment A/R Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'DPClearingAcct', 'Name': 'Down Payment Clearing Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'ExchangeDiffGain', 'Name': 'Realized Exchange Diff. Gain', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'ExchangeDiffLoss', 'Name': 'Realized Exchange Diff. Loss', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'CashDiscount', 'Name': 'Cash Discount', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'RevenueAcct', 'Name': 'Revenue Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'RevenueAcctForeign', 'Name': 'Revenue Account Foreign', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'SalesCreditAcct', 'Name': 'Sales Credit Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'DPInterimAcct', 'Name': 'Down Payment Interim Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'DunningInterestAcct', 'Name': 'Dunning Interest', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'DunningFeeAcct', 'Name': 'Dunning Fee', 'AcctCode': '', 'AcctName': '' },
    ];
    const ap_gl_setup_list = [
        { 'Code': 'DomesticPayableAcct', 'Name': 'Domestic Account Payable', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'ForeignPayableAcct', 'Name': 'Foreign Account Payable', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'ExchangeDiffGain', 'Name': 'Realized Exchange Diff. Gain', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'ExchangeDiffLoss', 'Name': 'Realized Exchange Diff. Loss', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'BankTransferAcct', 'Name': 'Bank Transfer', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'CashDiscountAcct', 'Name': 'Cash Discount', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'CashDiscountClearingAcct', 'Name': 'Cash Discount Clearing', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'ExpenseAcct', 'Name': 'Expense Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'ExpenseForeignAcct', 'Name': 'Expense Account Foreign', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'CreditAcct', 'Name': 'Purchase Credit Account ', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'CreditForeignAcct', 'Name': 'Purchase Credit Account Foreign', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'OverPaymentAcct', 'Name': 'OverPayment A/P Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'UnderPaymentAcct', 'Name': 'UnderPayment A/P Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'DPClearingAcct', 'Name': 'Down Payment Clearing Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'DPInterimAcct', 'Name': 'Down Payment Interim Account', 'AcctCode': '', 'AcctName': '' },
        { 'Code': 'ExpenseAndInvntryAcct', 'Name': 'Expense and Inventory Account', 'AcctCode': '', 'AcctName': '' },

    ];
    const witholdingtax_category = [
        { Id: 'P', Description: 'Payment' },
        { Id: 'I', Description: 'Invoice'}
    ];
    const witholdingtax_basetype = [
        { Id: 'G', Description: 'Gross' },
        { Id: 'N', Description: 'Net' }
    ];
    const witholdingtax_rpt = [
        { Id: 'C', Description: 'Company' },
        { Id: 'P', Description: 'Person' }
    ];
    const coa_acct_type_list = [
        {'Code': 'S', 'Name': 'Sales'},
        {'Code': 'E', 'Name': 'Expenditures'},
        {'Code': 'O', 'Name': 'Others'}
    ]
    const coa_level_list = [
        { 'Code': 2, 'Name': 'Level 2' },
        { 'Code': 3, 'Name': 'Level 3' },
        { 'Code': 4, 'Name': 'Level 4' },
        { 'Code': 5, 'Name': 'Level 5' },
        { 'Code': 6, 'Name': 'Level 6' },
        { 'Code': 7, 'Name': 'Level 7' },
        { 'Code': 8, 'Name': 'Level 8' },
    ]

    const journal_trn_type_list = [
        { 'Code': 'SDN', 'Name': 'Sales Deliveries', 'TransactionCategory' : 'SAL'},
        { 'Code': 'SRD', 'Name': 'Sales Returns', 'TransactionCategory': 'SAL'},
        { 'Code': 'SIN', 'Name': 'Sales Invoice', 'TransactionCategory': 'SAL' },
        { 'Code': 'SCR', 'Name': 'Sales Credit Note', 'TransactionCategory': 'SAL' },
        { 'Code': 'PDN', 'Name': 'Purchase Deliviries', 'TransactionCategory': 'PUR' },
        { 'Code': 'PRD', 'Name': 'Purchase Returns', 'TransactionCategory': 'PUR' },
        { 'Code': 'PIN', 'Name': 'Purchase Invoice', 'TransactionCategory': 'PUR' },
        { 'Code': 'PCR', 'Name': 'Purchase Credit Note', 'TransactionCategory': 'PUR' },
        { 'Code': 'SPY', 'Name': 'Incoming Payment', 'TransactionCategory': 'PY' },
        { 'Code': 'DEP', 'Name': 'Deposits', 'TransactionCategory': 'PY' },
        { 'Code': 'PPY', 'Name': 'Outgoing Payment', 'TransactionCategory': 'PY' },
        { 'Code': 'CHQ', 'Name': 'Checks for Payment', 'TransactionCategory': 'PY' },
        { 'Code': 'OBG', 'Name': 'Openning Balance', 'TransactionCategory': 'JE' },
        { 'Code': 'CBG', 'Name': 'Closing Balance', 'TransactionCategory': 'JE' },
        { 'Code': 'ITR', 'Name': 'Internal Reconcilation', 'TransactionCategory': 'JE' },
        { 'Code': 'JEN', 'Name': 'Journal Entry', 'TransactionCategory': 'JE' },
        { 'Code': 'IGE', 'Name': 'Goods Issue', 'TransactionCategory': 'INV' },
        { 'Code': 'IGN', 'Name': 'Goods Receipt', 'TransactionCategory': 'INV' },
        { 'Code': 'IQI', 'Name': 'Inventory Initial Quantity(OB)', 'TransactionCategory': 'INV' },
        { 'Code': 'IQP', 'Name': 'Inventory Posting', 'TransactionCategory': 'INV' },
        { 'Code': 'IMV', 'Name': 'Inventory Valuation', 'TransactionCategory': 'INV' },
        { 'Code': 'ITF', 'Name': 'Inventory Transfer', 'TransactionCategory': 'INV' },
        { 'Code': 'EB', 'Name': 'Electric Bill', 'TransactionCategory': 'EB' },
        { 'Code': 'ALL', 'Name': 'All Transactions', 'TransactionCategory': 'ALL' }
    ]

    angular.module("FIN.MODELS", [])
        .factory('GL.SETUP.MODEL', function () {
            return {
                "GeneralGLSetupList": general_gl_list,
                "InvntryGLSetupList": invntry_gl_setup_list,
                "ARGLSetupList": ar_gl_setup_list,
                "APGLSetupList": ap_gl_setup_list,
        
            };
        })
        .factory('COA.SETUP.MODEL', function () {
            return {
                'AccountTypeList': coa_acct_type_list,
                'AccountLevelList': coa_level_list
            }
        })
        .factory('JE.MODEL', function () {

            var je = {
                "JournalEntryID": "0",
                "ObjType": "",
                "BaseEntry": "",
                "BaseNum": "",
                "BaseType": "",
                "TargetEntry": "",
                "TargetNum": "",
                "TargetType": "",
                "TransType": "",
                "RefDate": "",
                "RefDate2": "",
                "RefDate3": "",
                "Ref1": "",
                "Ref2": "",
                "Ref3": "",
                "Project": "",
                "Remarks": "",
                "SysTotal": "0",
                "SysTotalLC": "0",
                "SysTotalFC": "0",
                "VersionNo": "",
                "Datasource": "",
                "LogIntance": "",
                "CreatedBy": "",
                "CreatedDate": "",
                "UpdatedBy": "",
                "UpdatedDate": "",
                "JournalEntry_Lines": [],
                "_ClearLine": function () {
                    this.JournalEntry_Lines = []
                },
                "_AddLine": function (a) {
                    this.JournalEntry_Lines.push(a)
                },
                "_DeleteLine": function (a) {
                    this.JournalEntry_Lines.splice(a, 1)
                }
            }

            var je_line = {
                "JournalEntry_LineID": "0",
                "JournalEntryID": "",
                "ObjType": "",
                "BaseEntry": "",
                "BaseNum": "",
                "BaseType": "",
                "TargetEntry": "",
                "TargetNum": "",
                "TargetType": "",
                "TransType": "",
                "GLAcctCode": "",
                "LineID": "",
                "VisID": "",
                "ShortName": "",
                "Debit": "0",
                "Credit": "0",
                "DebitSC": "0",
                "CreditSC": "0",
                "DebitFC": "0",
                "CreditFC": "0",
                "ContraAccount": "",
                "DebitCredit": "",
                "BaseAmt": "",
                "VatAmt": "",
                "RefDate": "",
                "RefDate2": "",
                "RefDate3": "",
                "Ref1": "",
                "Ref2": "",
                "Ref3": "",
                "Project": "",
                "OcrCode": "",
                "OcrCode2": "",
                "OcrCode3": "",
                "OcrCode4": "",
                "OcrCode5": "",
                "LineRemarks": "",
                "VersionNo": "",
                "Datasource": "",
                "LogIntance": "",
                "CreatedBy": "",
                "CreatedDate": "",
                "UpdatedBy": "",
                "UpdatedDate": ""
            }

            return {
                "JournalEntry": je,
                "JournalEntry_Line": je_line
            }
        })
        .factory('WT.MODEL', function () {
            {
                return {
                    "WithholdingTaxCategory": witholdingtax_category,
                    "WithholdingTax_BaseType": witholdingtax_basetype,
                    "WithholdingTax_RPT": witholdingtax_rpt
                }

            }
        })
        .factory('journal.trntype.model', function () {
            return {
                "JournalTransTypeList": journal_trn_type_list 
            }
        })
}());