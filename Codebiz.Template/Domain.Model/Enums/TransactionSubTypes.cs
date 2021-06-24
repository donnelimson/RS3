using System.ComponentModel;

namespace Codebiz.Domain.Common.Model.Enums
{
    public enum TransactionSubTypes
    {
        [Description("Residential Senior Citizen")]
        ResidentialSeniorCitizen = 1,
        [Description("DSWD - Accredited Senior Citizen Centers/Residential Care Institutions")]
        DswdAccreditedSeniorCitizen = 2,
        [Description("Small")]
        Small = 3,
        [Description("Big")]
        Big = 4,
        [Description("Sale")]
        Sale = 5,
        [Description("Waived")]
        Waived = 6,
        [Description("Death")]
        Death = 7,
        [Description("Change Status")]
        ChangeStatus = 8,
    }
}
