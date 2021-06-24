using System.ComponentModel;

namespace Codebiz.Domain.Common.Model.Enums
{
    public enum CommisionGroupEnums
    {
        [Description("High Consumptions")]
        HighConsumptions = 1,

        [Description("Medium Consumptions")]
        MediumConsumptions = 2,


        [Description("Low Consumptions")]
        LowConsumptions = 3,

        [Description("User Defined Consumptions")]
        UserDefinedConsumptions = 4,
    }
}
