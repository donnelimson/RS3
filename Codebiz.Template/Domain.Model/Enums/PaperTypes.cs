
using System.ComponentModel;


namespace Codebiz.Domain.Common.Model.Enums
{
    public enum PaperTypes
    {
        [Description("Blank Paper")]
        BlankPaper = 1,
        [Description("Overflow Prenumbered Check Stock")]
        OPCS = 2,
        [Description("Overflow Blank Paper")]
        OBP = 3,
        [Description("Default")]
        Default = 4,
    }
}
