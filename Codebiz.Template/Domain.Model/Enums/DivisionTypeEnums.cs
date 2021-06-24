using System.ComponentModel;

namespace Codebiz.Domain.Common.Model.Enums
{
    public enum DivisionTypeEnums
    {

        [Description("Metering")]
        Metering = 1,

        [Description("Transformer")]
        Transformer = 2,

        [Description("Planning")]
        Planning = 3,

        [Description("Construction")]
        Construction = 4,
    }
}
