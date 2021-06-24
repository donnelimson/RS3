using AutoMapper;
using Codebiz.Domain.ERP.Context;
using Codebiz.Domain.ERP.Infrastructure.BP.DTO;
using Codebiz.Domain.ERP.Model.Data.Accounts;
using Codebiz.Domain.ERP.Model.DataModel.BusinessPartners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.ERP.Infrastructure.BP
{
    public class BusinessPartnerDataService : IBusinessPartnerDataService
    {
        private Lazy<PaymentTermService> _lazy_PaymentTermService;

        private Lazy<BusinessPartnerService> _lazy_BusinessPartnerService;

        private Lazy<BusinessPartnerGroupService> _lazy_BusinessPartnerGroupService;

        private Lazy<SalesPersonService> _lazy_SalesPersonService;

        private Lazy<ShipTypeService> _lazy_ShipTypeService;

        private readonly AccountDataContext _ctx;

        public BusinessPartnerGroupService BusinessPartnerGroupService { get => _lazy_BusinessPartnerGroupService.Value; }

        public BusinessPartnerService BusinessPartnerService { get => _lazy_BusinessPartnerService.Value; }

        public PaymentTermService PaymentTermService { get => _lazy_PaymentTermService.Value; }

        public SalesPersonService SalesPersonService { get => _lazy_SalesPersonService.Value; }

        public ShipTypeService ShipTypeService { get => _lazy_ShipTypeService.Value; }

        public BusinessPartnerDataService()
        {
            _ctx = new AccountDataContext();

            _lazy_PaymentTermService = new Lazy<PaymentTermService>(() => new PaymentTermService(_ctx));

            _lazy_BusinessPartnerService = new Lazy<BusinessPartnerService>(() => new BusinessPartnerService(_ctx));

            _lazy_BusinessPartnerGroupService = new Lazy<BusinessPartnerGroupService>(() => new BusinessPartnerGroupService(_ctx));

            _lazy_SalesPersonService = new Lazy<SalesPersonService>(() => new SalesPersonService(_ctx));

            _lazy_ShipTypeService = new Lazy<ShipTypeService>(() => new ShipTypeService(_ctx));

          

        }
 
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }


                disposedValue = true;
            }
        }
 
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
