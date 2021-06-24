using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Enums.CommercialServicesApplication
{
    public enum RelationshipTypes
    {
        [Description("Legal Spouse")]
        LegalSpouse = 1,

        [Description("Father")]
        Father = 2,

        [Description("Mother")]
        Mother = 3,

        [Description("Brother")]
        Brother = 4,

        [Description("Sister")]
        Sister = 5,

        [Description("Son")]
        Son = 6,

        [Description("Daughter")]
        Daughter = 7,

        [Description("Others")]
        Others = 8,




    }
}
