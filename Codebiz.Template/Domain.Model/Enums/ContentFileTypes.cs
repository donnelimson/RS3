using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Enums
{
    public enum ContentFileTypes : int
    {
        [Description("Employee Photo")]
        EmployeePhoto = 1,
        [Description("Product Photo")]
        ProductPhoto = 2,



    }

    public enum ConfigurationSettings : int
    {
        [Description("Database Connection String")]
        ConnectionString = 1,

        [Description("DisplayPhoto")]
        DisplayPhoto = 2,

        [Description("Mail Template Path")]
        MailTemplatePath = 3,

        [Description("SmtpDisplayName")]
        SmtpDisplayName = 4,

        [Description("SmtpUsername")]
        SmtpUsername = 5,

        [Description("SmtpPassword")]
        SmtpPassword = 6,

        [Description("SmtpHost")]
        SmtpHost = 7,

        [Description("SmtpPort")]
        SmtpPort = 8,

        [Description("Site Public Base Url")]
        SitePublicBaseUrl = 9, 

        [Description("Site Local Network Base Url")]
        SiteLocalNetworkBaseUrl = 10,

        [Description("Email Alert Recipients")]
        EmailAlertRecipients = 11,

        [Description("Maximum Login Attempts")]
        MaxLoginAttempt = 12,

        #region Folders for files/attachments

        [Description("Employee Photo Folder")]
        EmployeePhotoFolder = 13,
        [Description("Product Photo Folder")]
        ProductPhotoFolder = 14,

        #endregion

    }
}
