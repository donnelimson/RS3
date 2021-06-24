using System;

namespace Codebiz.Domain.ERP.Infrastructure.BP
{
    public interface IBusinessPartnerDataService : IDisposable
    {
        BusinessPartnerGroupService BusinessPartnerGroupService { get; }
        BusinessPartnerService BusinessPartnerService { get; }
        PaymentTermService PaymentTermService { get; }
        SalesPersonService SalesPersonService { get; }
        ShipTypeService ShipTypeService { get; }
    }
}