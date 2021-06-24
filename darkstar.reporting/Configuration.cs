using System.Globalization;

namespace darkstar.reporting
{
    public class Configuration
    {
        public Configuration()
        {
            this.CultureInfo = CultureInfo.CurrentCulture;
        }

        public CultureInfo CultureInfo { get; set; }
    }
}