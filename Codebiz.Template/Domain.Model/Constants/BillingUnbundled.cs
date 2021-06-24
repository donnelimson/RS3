namespace Codebiz.Domain.Common.Model
{
    public class BillingUnbundledClassification
    {
        public const string DISCOUNT = "DISCOUNT";       
        public const string VAT = "VAT";
        public const string TRANSFORMERLOSS = "TRANSFORMER LOSS";
        public const string TRANSFORMERRENTAL = "TRANSFORMER RENTAL";
    }

    public class BillingUnbundledIncludedToSOA
    {
        public const string CONTTESTABLE = "CONTTESTABLE";
        public const string NETMETERING = "NET METERING";
    }

    public class BillingUnbundledComputedBy
    {
        public const string KWH = "KWH";
        public const string DKWH = "DKWH";
        public const string KVARATING = "KVA RATING";
    }
}
