using System.ComponentModel;

namespace Codebiz.Domain.Common.Model.Enums.CommercialServicesApplication
{
    public enum MailingSubscriptions
    {
        [Description("Hard Copy")]
        HardCopy = 1,

        [Description("via E-mail")]
        Email = 2,

        [Description("via SMS")]
        SMS = 3,
    }
}
