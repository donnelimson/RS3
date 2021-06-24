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
    public class CreateLongPayOrderJob : IJob
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILongPayService _longPayService;
        private readonly IConfigSettingService _configSettingService;
        private readonly IAppUserServices _appUserServices;

        public CreateLongPayOrderJob(IUnitOfWork unitOfWork,
            ILongPayService longPayService,
            IConfigSettingService configSettingService,
            IAppUserServices appUserServices
            )
        {
            _unitOfWork = unitOfWork;
            _longPayService = longPayService;
            _configSettingService = configSettingService;
            _appUserServices = appUserServices;
        }

        public void Execute(IJobExecutionContext context)
        {
            Logger.Info("CreateLongPayOrderJob started. " + DateTime.Now);

            try
            {
                var systemUserAppUserId = _configSettingService.GetInt32ValueById((int)ConfigurationSettings.SystemUserAppUserId);
                var systemUser = _appUserServices.GetById(systemUserAppUserId);
                if (systemUser == null)
                    throw new ArgumentNullException("systemUser");

                var creationOfLongPayOrderDayOfMonth = _configSettingService.GetInt32ValueById((int)ConfigurationSettings.CreationOfLongPayOrderDayOfMonth);
                if (creationOfLongPayOrderDayOfMonth < 1 || creationOfLongPayOrderDayOfMonth > 31)
                    throw new Exception("Invalid CreationOfLongPayOrderDayOfMonth settings.");

                if (DateTime.Now.Day == creationOfLongPayOrderDayOfMonth)
                {
                    Logger.Info("Auto creation of long pay order started. ");

                    var dateToExtract = DateTime.Now.AddMonths(1);

                    _longPayService.CreateLongPayOrder(dateToExtract.Year, dateToExtract.Month, systemUser.AppUserId);

                    _unitOfWork.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("ImplementOrderJob encountered unexpected error.", exception: ex);
            }
            

            Logger.Info("CreateLongPayOrderJob ended. " + DateTime.Now);
        }
    }
}
