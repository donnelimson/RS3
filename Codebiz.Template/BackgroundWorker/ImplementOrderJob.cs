using Logging;
using PnpPais.Domain.Model.Enums;
using PnpPais.Domain.Repository;
using PnpPais.Infrastructure;
using PnpPais.Infrastructure.Services;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PnpPais.BackgroundWorker
{
    public class ImplementOrderJob : IJob
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IOrderRepository _orderRepository;
        private readonly IOrderService _orderService;

        public ImplementOrderJob(IUnitOfWork unitOfWork,

            IOrderRepository orderRepository,
            IOrderService orderService
            )
        {
            _unitOfWork = unitOfWork;

            _orderRepository = orderRepository;
            _orderService = orderService;
        }

        public void Execute(IJobExecutionContext context)
        {
            Logger.Info("ImplementOrderJob started. " + DateTime.Now);

            try
            {
                var ordersToImplement = _orderRepository.GetAll.Where(a => a.IsActive && a.ApprovalStatusId == (int)ApprovalStatuses.Approved 
                    && a.DateImplemented == null
                    && a.OrderDetail.Any(b => b.EffectivityDate <= DateTime.Now && b.DateImplemented == null))
                .ToList();

                foreach (var item in ordersToImplement)
                {
                    _orderService.ImplementOrder(item, item.CreatedByAppUserId);
                }
                
                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.Error("ImplementOrderJob encountered unexpected error.", exception: ex);
            }

            Logger.Info("ImplementOrderJob ended. " + DateTime.Now);
        }
    }
}
