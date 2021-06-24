using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Web
{
    /// <summary>
    /// The application version class.
    /// </summary>
    public class AppVersion
    {
        /// <summary>
        /// Gets the build version.
        /// </summary>
        /// <value>
        /// The build version.
        /// </value>
        public static string BuildVersion
        {
            get
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);

                return string.Format("{0} {1}", System.Configuration.ConfigurationManager.AppSettings["Environment"].ToUpper(), fvi.FileVersion);
            }
        }
    }
}