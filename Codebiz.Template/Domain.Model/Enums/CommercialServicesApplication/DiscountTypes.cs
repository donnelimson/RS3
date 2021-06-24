using System.ComponentModel;

namespace Codebiz.Domain.Common.Model.Enums.CommercialServicesApplication
{
    public enum DiscountTypes
    {
        [Description("Residential Senior Citizen")]
        ResidentialSeniorCitizen = 1,

        [Description("DSWD - Accredited Senior Citizen Centers/Residential Care Institutions")]
        DswdAccreditedSeniorCitizen = 2,
    }
}
