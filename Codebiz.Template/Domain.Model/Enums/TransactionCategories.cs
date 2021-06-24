using System.ComponentModel;

namespace Codebiz.Domain.Common.Model.Enums
{
    public enum TransactionCategories
    {
        [Description("Deposit")]
        Deposit = 1,

        [Description("Withdrawal")]
        Withdrawal = 2,

        [Description("Loan")]
        Loan = 3
    }
}