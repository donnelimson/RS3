using Logging;
using PnpPais.Domain.Model.Enums;
using PnpPais.Domain.Repository;
using PnpPais.Infrastructure;
using PnpPais.Infrastructure.Services;
using PnpPais.Infrastructure.Utilities;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PnpPais.BackgroundWorker
{
    public class CreateRcaOrderJob : IJob
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRcaOrderCreator _rcaOrderCreator;
        private readonly IConfigSettingService _configSettingService;
        private readonly IAppUserServices _appUserServices;

        public CreateRcaOrderJob(IUnitOfWork unitOfWork,
            IRcaOrderCreator rcaOrderCreator,
            IConfigSettingService configSettingService,
            IAppUserServices appUserServices
            )
        {
            _unitOfWork = unitOfWork;
            _rcaOrderCreator = rcaOrderCreator;
            _configSettingService = configSettingService;
            _appUserServices = appUserServices;
        }

        public void Execute(IJobExecutionContext context)
        {
            Logger.Info("CreateRcaOrderJob started. " + DateTime.Now);

            try
            {
                var systemUserAppUserId = _configSettingService.GetInt32ValueById((int)ConfigurationSettings.SystemUserAppUserId);
                var systemUser = _appUserServices.GetById(systemUserAppUserId);
                if (systemUser == null)
                    throw new ArgumentNullException("systemUser");

                var creationOfRcaOrderDayOfMonth = _configSettingService.GetInt32ValueById((int)ConfigurationSettings.CreationOfRcaOrderDayOfMonth);
                if (creationOfRcaOrderDayOfMonth < 1 || creationOfRcaOrderDayOfMonth > 31)
                    throw new Exception("Invalid creationOfRcaOrderDayOfMonth settings.");

                if (DateTime.Now.Day == creationOfRcaOrderDayOfMonth)
                {
                    Logger.Info("Auto creation of rca order started. ");

                    var dateToExtract = DateTime.Now.AddMonths(1);

                    _rcaOrderCreator.CreateRcaOrder(dateToExtract.Year, dateToExtract.Month, systemUser.AppUserId);

                    _unitOfWork.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("ImplementOrderJob encountered unexpected error.", exception: ex);
            }
            

            Logger.Info("CreateRcaOrderJob ended. " + DateTime.Now);
        }
    }
}
