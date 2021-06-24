using System.ComponentModel;

namespace Codebiz.Domain.Common.Model.Enums.Currency
{
    public enum DecimalEnums
    {
        [Description("No Decimal")]
        NoDecimal = 1,

        [Description("1 Digit")]
        OneDigits = 2,

        [Description("2 Digits")]
        TwoDigits = 3,

        [Description("3 Digits")]
        ThreeDigits = 4,

        [Description("4 Digits")]
        FourDigits = 5,

        [Description("5 Digits")]
        FiveDigits = 6,

        [Description("6 Digits")]
        SixDigits = 7
    }
}
