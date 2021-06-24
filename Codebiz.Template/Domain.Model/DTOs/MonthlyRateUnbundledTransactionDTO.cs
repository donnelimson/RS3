namespace Codebiz.Domain.Common.Model.DTOs
{
    public class MonthlyRateUnbundledTransactionDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public float? Rate { get; set; }
        public float? Amount { get; set; }
    }
}
