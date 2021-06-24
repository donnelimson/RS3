using System.ComponentModel;

namespace Codebiz.Domain.Common.Model.Enums
{
    public enum ValuationMethodEnums
    {
        [Description("Standard")]
        Standard = 1,

        [Description("Fifo")]
        Fifo = 2,

        [Description("Moving Average")]
        MovingAvarage = 3,
    }
}
