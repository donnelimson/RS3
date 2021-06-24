using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.ERP.Model.Enums.VEHICLES
{
    public enum VehicleAccessoriesEnum
    {
        [Description("Head Lights - Left")]
        HeadLightLeft = 1,

        [Description("Head Lights - Right")]
        HeadLightRight = 2,

        [Description("Head Lights - Dim")]
        HeadLightDim = 3,

        [Description("Head Lights - Bright")]
        HeadLightBright = 4,

        [Description("H2O, Temp, Gauge")]
        H2oTempGauge = 5,

        [Description("Oil Pressure Gauge")]
        OilPressureGuage = 6,

        [Description("Amp Meter")]
        AmpMeter = 7,

        [Description("Fuel Gauge")]
        ClutchFluid = 8,

        [Description("Speed Meter")]
        SpeedMeter = 9,

        [Description("Flashers - Front Left")]
        FlashersFrontLeft = 10,

        [Description("Flashers - Front Right")]
        FlashersFrontRight = 11,

        [Description("Flashers - Rear Left")]
        FlashersRearLeft = 12,

        [Description("Flashers - Rear Right")]
        FlashersRearRight = 13,

        [Description("Tail Light - Right")]
        TailLightRight = 14,

        [Description("Tail Light - Left")]
        TailLightLeft = 15,

        [Description("Stop Light - Right")]
        StopLightRight = 16,

        [Description("Stop Light - Left")]
        StopLightLeft = 17,

        [Description("Hazards Lights")]
        HazardsLights = 18,

        [Description("Horn")]
        Horn = 19,

        [Description("Wiper")]
        Wiper = 20,
      
    }
}
