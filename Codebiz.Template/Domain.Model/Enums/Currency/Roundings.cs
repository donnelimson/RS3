using System.ComponentModel;

namespace Codebiz.Domain.Common.Model.Enums.Currency
{
    public enum Roundings
    {
        [Description("No Rounding")]
        NoRounding = 1,

        [Description("Round To One")]
        RoundToOne = 2,

        [Description("Round To Ten")]
        RoundToTen = 3,

        [Description("Round To Five Hundredth")]
        RoundToFiveHundredth = 4,

        [Description("Round To Ten Hundredth")]
        RoundToTenHundredth = 5
    }
}
