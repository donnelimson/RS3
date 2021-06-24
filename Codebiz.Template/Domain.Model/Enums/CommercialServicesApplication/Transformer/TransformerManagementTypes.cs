using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Enums.CommercialServicesApplication.Transformer
{
    public enum TransformerManagementTypes
    {
        [Description("New Transformer")]
        NewTransformer = 1,

        [Description("Incoming Distribution Transformer")]
        IncomingDistributionTransformer = 2,

        [Description("Outgoing Distribution Transformer")]
        OutgoingDistributionTransformer = 3,
    }
}
