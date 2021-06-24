using System.ComponentModel;

namespace Codebiz.Domain.Common.Model.Enums
{
    public enum ReportingReason
    {
        [Description("There is no underlying legal/trade obligation, purpose or economic justification.")]
        Reason1 = 1,

        [Description("The client is not properly identified.")]
        Reason2 = 2,

        [Description("The amount involved is not commensurate with the business or financial capacity of the client")]
        Reason3 = 3,

        [Description("Taking into account all known circumstances, it may be perceived that the client’s transaction is structured in order to avoid being the subject of reporting requirements under the AMLA")]
        Reason4 = 4,

        [Description("Any circumstance relating to the transaction which is observed to deviate from the profile of the client and/or the client’s past transactions with the covered person")]
        Reason5 = 5,

        [Description("The transaction is in any way related to an unlawful activity or any money laundering activity or offense under the AMLA, that is about to be, is being or has been committed")]
        Reason6 = 6,

        [Description("Any transaction that is similar, analogous or identical to any of the foregoing")]
        Reason7 = 7
    }

    public class ReportingReasonData
    {
        public static ReportingReason GetValue(int value)
        {
            switch (value)
            {
                case 1:
                    return ReportingReason.Reason1;
                case 2:
                    return ReportingReason.Reason2;
                case 3:
                    return ReportingReason.Reason3;
                case 4:
                    return ReportingReason.Reason4;
                case 5:
                    return ReportingReason.Reason5;
                case 6:
                    return ReportingReason.Reason6;
                case 7:
                    return ReportingReason.Reason7;
            }

            return 0;
        }
    }
}