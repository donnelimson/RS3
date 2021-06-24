using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Enums.CommercialServicesApplication
{
    public enum DocumentTypes
    {
        [Description("Statement of Account (atleast 1 year)")]
        StatementOfAccountAtleastOneYear = 1,

        [Description("Statement of Account (atleast 6 months)")]
        StatementOfAccountAtleastSixMonths = 2,

        [Description("Senior Citizen's ID")]
        SeniorCitizensId = 3,

        [Description("Barangay Certificate")]
        BarangayCertificate = 4,

        [Description("Clearance from Finance Dept. or Area Office")]
        ClearanceFromFinanceDeptOrAreaOffice = 5,

        [Description("Clearance from Finance Dept.")]
        ClearanceFromFinanceDept = 6,

        [Description("Authorization")]
        Authorization = 7,

        [Description("Medical Certificate")]
        MedicalCertificate = 8,

        [Description("ID of the Guardian")]
        IdOfTheGuardian = 9,

        [Description("Clearance")]
        Clearance = 10,

        [Description("1x1 Picture")]
        OneByOnePicture = 11,

        [Description("Location Map")]
        LocationMap = 12,

        [Description("Death Certificate")]
        DeathCertificate = 13,

        [Description("Deed of Sale")]
        DeedOfSale = 14,




    }
}
