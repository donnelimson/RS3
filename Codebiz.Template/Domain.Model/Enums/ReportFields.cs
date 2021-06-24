using System.ComponentModel;

namespace Codebiz.Domain.Common.Model.Enums
{
    public enum ReportFields
    {
        [Description("Transaction Data | NULL | NULL | D-1 | Detail Record Indicator | TableName | FieldName")]
        D_1 = 1,

        [Description("Transaction Data | NULL | NULL | D-2 | Transaction Date | Transaction | TransactionDate")]
        D_2 = 2,

        [Description("Transaction Data | NULL | NULL | D-3 | Transaction Code | TableName | FieldName")]
        D_3 = 3,

        [Description("Transaction Data | NULL | NULL | D-4 | Transaction Reference No. | TableName | FieldName")]
        D_4 = 4,

        [Description("Transaction Data | NULL | NULL | D-5 | Account No./PN No./Client Stock Ref No./Certificate No. | Account | AccountNo")]
        D_5 = 5,

        [Description("Transaction Data | NULL | NULL | D-6 | Old Account No./PN No./Client Stock Ref. No. | TableName | FieldName")]
        D_6 = 6,

        [Description("Transaction Data | NULL | NULL | D-7 | Transaction Amount (Php) | Transaction | TransactionAmount")]
        D_7 = 7,

        [Description("Transaction Data | NULL | NULL | D-8 | Transaction Amount (FX) | TableName | FieldName")]
        D_8 = 8,

        [Description("Transaction Data | NULL | NULL | D-9 | FX Currency Code | TableName | FieldName")]
        D_9 = 9,

        [Description("Transaction Data | NULL | NULL | D-10 | Nature/Purpose of Transaction | TableName | FieldName")]
        D_10 = 10,

        [Description("Transaction Data | NULL | NULL | D-11 | Inception/Effectivity Date | TableName | FieldName")]
        D_11 = 11,

        [Description("Transaction Data | NULL | NULL | D-12 | Maturity Date/Expiry Date | TableName | FieldName")]
        D_12 = 12,

        [Description("Transaction Data | NULL | NULL | D-13 | Amount of Claim/Dividend | TableName | FieldName")]
        D_13 = 13,

        [Description("Transaction Data | NULL | NULL | D-14 | No. of shares/units | TableName | FieldName")]
        D_14 = 14,

        [Description("Transaction Data | NULL | NULL | D-15 | Net Asset Value | TableName | FieldName")]
        D_15 = 15,

        [Description("Transaction Data | NULL | NULL | D-16 | Name of Correspondent Bank | TableName | FieldName")]
        D_16 = 16,

        [Description("Transaction Data | NULL | NULL | D-17-1 | Address 1 | TableName | FieldName")]
        D_17_1 = 17,

        [Description("Transaction Data | NULL | NULL | D-17-2 | Address 2 | TableName | FieldName")]
        D_17_2 = 18,

        [Description("Transaction Data | NULL | NULL | D-17-3 | Address 3 | TableName | FieldName")]
        D_17_3 = 19,

        [Description("Transaction Data | NULL | NULL | D-18 | Country Code of Correspondent Bank | TableName | FieldName")]
        D_18 = 20,

        [Description("Account Holder | NULL | NULL | D-A-1 | Party Type Flag | TableName | FieldName")]
        D_A_1 = 21,

        [Description("Account Holder | NULL | NULL | D-A-2 | Customer Reference Number | TableName | FieldName")]
        D_A_2 = 22,

        [Description("Account Holder | NULL | NULL | D-A-3 | Name Flag | TableName | FieldName")]
        D_A_3 = 23,

        [Description("Account Holder | NULL | NULL | D-A-4-1 | Last Name | Member | LastName")]
        D_A_4_1 = 24,

        [Description("Account Holder | NULL | NULL | D-A-4-2 | First Name | Member | FirstName")]
        D_A_4_2 = 25,

        [Description("Account Holder | NULL | NULL | D-A-4-3 | Middle Name | Member | MiddleName")]
        D_A_4_3 = 26,

        [Description("Account Holder | NULL | NULL | D-A-5-1 | Address 1 | TableName | FieldName")]
        D_A_5_1 = 27,

        [Description("Account Holder | NULL | NULL | D-A-5-2 | Address 2 | TableName | FieldName")]
        D_A_5_2 = 28,

        [Description("Account Holder | NULL | NULL | D-A-5-3 | Address 3 | TableName | FieldName")]
        D_A_5_3 = 29,

        [Description("Account Holder | NULL | NULL | D-A-6 | Birthdate/Registration Date | TableName | FieldName")]
        D_A_6 = 30,

        [Description("Account Holder | NULL | NULL | D-A-7 | Place of Birth/Registration | TableName | FieldName")]
        D_A_7 = 31,

        [Description("Account Holder | NULL | NULL | D-A-8 | Nationality | TableName | FieldName")]
        D_A_8 = 32,

        [Description("Account Holder | NULL | NULL | D-A-9 | ID Type | TableName | FieldName")]
        D_A_9 = 33,

        [Description("Account Holder | NULL | NULL | D-A-10 | Identification No. | TableName | FieldName")]
        D_A_10 = 34,

        [Description("Account Holder | NULL | NULL | D-A-11 | Telephone No. | TableName | FieldName")]
        D_A_11 = 35,

        [Description("Account Holder | NULL | NULL | D-A-12 | Nature of Business | TableName | FieldName")]
        D_A_12 = 36,

        [Description("Beneficiary | NULL | NULL | D-B-1 | Party Type Flag | TableName | FieldName")]
        D_B_1 = 37,

        [Description("Beneficiary | NULL | NULL | D-B-2 | Customer Reference Number | TableName | FieldName")]
        D_B_2 = 38,

        [Description("Beneficiary | NULL | NULL | D-B-3 | Name Flag | TableName | FieldName")]
        D_B_3 = 39,

        [Description("Beneficiary | NULL | NULL | D-B-4-1 | Last Name | TableName | FieldName")]
        D_B_4_1 = 40,

        [Description("Beneficiary | NULL | NULL | D-B-4-2 | First Name | TableName | FieldName")]
        D_B_4_2 = 41,

        [Description("Beneficiary | NULL | NULL | D-B-4-3 | Middle Name | TableName | FieldName")]
        D_B_4_3 = 42,

        [Description("Beneficiary | NULL | NULL | D-B-5-1 | Address 1 | TableName | FieldName")]
        D_B_5_1 = 43,

        [Description("Beneficiary | NULL | NULL | D-B-5-2 | Address 2 | TableName | FieldName")]
        D_B_5_2 = 44,

        [Description("Beneficiary | NULL | NULL | D-B-5-3 | Address 3 | TableName | FieldName")]
        D_B_5_3 = 45,

        [Description("Beneficiary | NULL | NULL | D-B-6 | Account Number | TableName | FieldName")]
        D_B_6 = 46,

        [Description("Beneficiary | NULL | NULL | D-B-7 | Birthdate/Registration Date | TableName | FieldName")]
        D_B_7 = 47,

        [Description("Beneficiary | NULL | NULL | D-B-8 | Place of Birth/Registration | TableName | FieldName")]
        D_B_8 = 48,

        [Description("Beneficiary | NULL | NULL | D-B-9 | Nationality | TableName | FieldName")]
        D_B_9 = 49,

        [Description("Beneficiary | NULL | NULL | D-B-10 | ID Type | TableName | FieldName")]
        D_B_10 = 50,

        [Description("Beneficiary | NULL | NULL | D-B-11 | Identification No. | TableName | FieldName")]
        D_B_11 = 51,

        [Description("Beneficiary | NULL | NULL | D-B-12 | Telephone No. | TableName | FieldName")]
        D_B_12 = 52,

        [Description("Beneficiary | NULL | NULL | D-B-13 | Nature of Business | TableName | FieldName")]
        D_B_13 = 53,

        [Description("Counterparty | NULL | NULL | D-C-1 | Party Type Flag | TableName | FieldName")]
        D_C_1 = 54,

        [Description("Counterparty | NULL | NULL | D-C-2 | Customer Reference Number | TableName | FieldName")]
        D_C_2 = 55,

        [Description("Counterparty | NULL | NULL | D-C-3 | Name Flag | TableName | FieldName")]
        D_C_3 = 56,

        [Description("Counterparty | NULL | NULL | D-C-4-1 | Last Name | TableName | FieldName")]
        D_C_4_1 = 57,

        [Description("Counterparty | NULL | NULL | D-C-4-2 | First Name | TableName | FieldName")]
        D_C_4_2 = 58,

        [Description("Counterparty | NULL | NULL | D-C-4-3 | Middle Name | TableName | FieldName")]
        D_C_4_3 = 59,

        [Description("Counterparty | NULL | NULL | D-C-5-1 | Address 1 | TableName | FieldName")]
        D_C_5_1 = 60,

        [Description("Counterparty | NULL | NULL | D-C-5-2 | Address 2 | TableName | FieldName")]
        D_C_5_2 = 61,

        [Description("Counterparty | NULL | NULL | D-C-5-3 | Address 3 | TableName | FieldName")]
        D_C_5_3 = 62,

        [Description("Counterparty | NULL | NULL | D-C-6 | Account Number | TableName | FieldName")]
        D_C_6 = 63,

        [Description("Other Participant | NULL | NULL | D-O-1 | Party Type Flag | TableName | FieldName")]
        D_O_1 = 64,

        [Description("Other Participant | NULL | NULL | D-O-2 | Customer Reference Number | TableName | FieldName")]
        D_O_2 = 65,

        [Description("Other Participant | NULL | NULL | D-O-3 | Name Flag | TableName | FieldName")]
        D_O_3 = 66,

        [Description("Other Participant | NULL | NULL | D-O-4-1 | Last Name | TableName | FieldName")]
        D_O_4_1 = 67,

        [Description("Other Participant | NULL | NULL | D-O-4-2 | First Name | TableName | FieldName")]
        D_O_4_2 = 68,

        [Description("Other Participant | NULL | NULL | D-O-4-3 | Middle Name | TableName | FieldName")]
        D_O_4_3 = 69,

        [Description("Other Participant | NULL | NULL | D-O-5-1 | Address 1 | TableName | FieldName")]
        D_O_5_1 = 70,

        [Description("Other Participant | NULL | NULL | D-O-5-2 | Address 2 | TableName | FieldName")]
        D_O_5_2 = 71,

        [Description("Other Participant | NULL | NULL | D-O-5-3 | Address 3 | TableName | FieldName")]
        D_O_5_3 = 72,

        [Description("Other Participant | NULL | NULL | D-O-6 | Account Number | TableName | FieldName")]
        D_O_6 = 73,

        [Description("Issuer | NULL | NULL | D-I-1 | Party Type Flag | TableName | FieldName")]
        D_I_1 = 74,

        [Description("Issuer | NULL | NULL | D-I-2 | Customer Reference Number | TableName | FieldName")]
        D_I_2 = 75,

        [Description("Issuer | NULL | NULL | D-I-3 | Name Flag | TableName | FieldName")]
        D_I_3 = 76,

        [Description("Issuer | NULL | NULL | D-I-4-1 | Last Name | TableName | FieldName")]
        D_I_4_1 = 77,

        [Description("Issuer | NULL | NULL | D-I-4-2 | First Name | TableName | FieldName")]
        D_I_4_2 = 78,

        [Description("Issuer | NULL | NULL | D-I-4-3 | Middle Name | TableName | FieldName")]
        D_I_4_3 = 79,

        [Description("Issuer | NULL | NULL | D-I-5-1 | Address 1 | TableName | FieldName")]
        D_I_5_1 = 80,

        [Description("Issuer | NULL | NULL | D-I-5-2 | Address 2 | TableName | FieldName")]
        D_I_5_2 = 81,

        [Description("Issuer | NULL | NULL | D-I-5-3 | Address 3 | TableName | FieldName")]
        D_I_5_3 = 82,

        [Description("Issuer | NULL | NULL | D-I-6 | Account Number | TableName | FieldName")]
        D_I_6 = 83,

        [Description("Transactor | NULL | NULL | D-T-1 | Party Type Flag | TableName | FieldName")]
        D_T_1 = 84,

        [Description("Transactor | NULL | NULL | D-T-2 | Customer Reference Number | TableName | FieldName")]
        D_T_2 = 85,

        [Description("Transactor | NULL | NULL | D-T-3 | Name Flag | TableName | FieldName")]
        D_T_3 = 86,

        [Description("Transactor | NULL | NULL | D-T-4-1 | Last Name | TableName | FieldName")]
        D_T_4_1 = 87,

        [Description("Transactor | NULL | NULL | D-T-4-2| First Name | TableName | FieldName")]
        D_T_4_2 = 88,

        [Description("Transactor | NULL | NULL | D-T-4-3 | Middle Name | TableName | FieldName")]
        D_T_4_3 = 89,

        [Description("Transactor | NULL | NULL | D-T-5-1 | Address 1 | TableName | FieldName")]
        D_T_5_1 = 90,

        [Description("Transactor | NULL | NULL | D-T-5-2 | Address 2 | TableName | FieldName")]
        D_T_5_2 = 91,

        [Description("Transactor | NULL | NULL | D-T-5-3 | Address 3 | TableName | FieldName")]
        D_T_5_3 = 92,

        [Description("Transactor | NULL | NULL | D-T-6 | Account Number | TableName | FieldName")]
        D_T_6 = 93,

        [Description("Subject of Suspicion | NULL | NULL | D-S-1 | Party Type Flag | TableName | FieldName")]
        D_S_1 = 94,

        [Description("Subject of Suspicion | NULL | NULL | D-S-2 | Customer Reference Number | TableName | FieldName")]
        D_S_2 = 95,

        [Description("Subject of Suspicion | NULL | NULL | D-S-3 | Name Flag | TableName | FieldName")]
        D_S_3 = 96,

        [Description("Subject of Suspicion | NULL | NULL | D-S-4-1 | Last Name | TableName | FieldName")]
        D_S_4_1 = 97,

        [Description("Subject of Suspicion | NULL | NULL | D-S-4-2 | First Name | TableName | FieldName")]
        D_S_4_2 = 98,

        [Description("Subject of Suspicion | NULL | NULL | D-S-4-3| Middle Name | TableName | FieldName")]
        D_S_4_3 = 99,

        [Description("Subject of Suspicion | NULL | NULL | D-S-5-1| Address 1 | TableName | FieldName")]
        D_S_5_1 = 100,

        [Description("Subject of Suspicion | NULL | NULL | D-S-5-2 | Address 2 | TableName | FieldName")]
        D_S_5_2 = 101,

        [Description("Subject of Suspicion | NULL | NULL | D-S-5-3 | Address 3 | TableName | FieldName")]
        D_S_5_3 = 102,

        [Description("Subject of Suspicion | NULL | NULL | D-S-6 | Account Number | TableName | FieldName")]
        D_S_6 = 103,

        [Description("Subject of Suspicion | NULL | NULL | D-S-7 | Birthdate/Registration Date | TableName | FieldName")]
        D_S_7 = 104,

        [Description("Subject of Suspicion | NULL | NULL | D-S-8 | Place of Birth/Registration | TableName | FieldName")]
        D_S_8 = 105,

        [Description("Subject of Suspicion | NULL | NULL | D-S-9 | Nationality | TableName | FieldName")]
        D_S_9 = 106,

        [Description("Subject of Suspicion | NULL | NULL | D-S-10 | ID Type | TableName | FieldName")]
        D_S_10 = 107,

        [Description("Subject of Suspicion | NULL | NULL | D-S-11| Identification No. | TableName | FieldName")]
        D_S_11 = 108,

        [Description("Subject of Suspicion | NULL | NULL | D-S-12 | Telephone No. | TableName | FieldName")]
        D_S_12 = 109,

        [Description("Subject of Suspicion | NULL | NULL | D-S-13 | Nature of Business | TableName | FieldName")]
        D_S_13 = 110,

        [Description("Subject of Suspicion | NULL | NULL | D-D-1 | Reason | TableName | FieldName")]
        D_D_1 = 111,

        [Description("Subject of Suspicion | NULL | NULL | D-D-2 | Narrative | TableName | FieldName")]
        D_D_2 = 112
    }
}