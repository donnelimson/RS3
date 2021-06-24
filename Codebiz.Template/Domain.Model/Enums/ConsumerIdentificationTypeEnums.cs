using System.ComponentModel;

namespace Codebiz.Domain.Common.Model
{
    public enum TenureEnums
    {
        [Description("Owned")]
        Owned = 1,

        [Description("Lease")]
        Lease = 2,
    }
}
