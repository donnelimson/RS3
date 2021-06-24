using System.ComponentModel;

namespace Codebiz.Domain.Common.Model.Enums.CommercialServicesApplication
{
    public enum JobOrderTaskToBePerformEnum
    {
        [Description("Testing")]
        Testing = 1,

        [Description("Installation")]
        Installation = 2,

        [Description("Inspection")]
        Inspection = 3,

        [Description("Fixing")]
        Fixing = 4,

        [Description("Pull-out")]
        Pullout = 5,

        [Description("Reconnection")]
        Reconnection = 6,

        [Description("Temporary Disconnection")]
        TemporaryDisconnection = 7,

        [Description("Permanent Disconnection")]
        PermanentDisconnection = 8,

        [Description("Erection")]
        Erection = 9
    }

    public enum JONatureTypeEnums
    {
        [Description("Pole")]
        Pole = 1,

        [Description("KWH Meter")]
        KWHMeter = 2,

        [Description("Transformer")]
        Transformer = 3,

        [Description("Line")]
        Line = 4,

        [Description("Service Drop")]
        ServiceDrop = 5,

        [Description("Special Lighting")]
        SpecialLighting = 6,
    }
}
