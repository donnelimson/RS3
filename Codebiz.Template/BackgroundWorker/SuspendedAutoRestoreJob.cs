using Logging;
using PnpPais.Domain.Model.Enums;
using PnpPais.Domain.Model;
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
    public class SuspendedAutoRestoreJob : IJob
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISuspendedAutoRestore _suspendedAutoRestore;
        private readonly IConfigSettingService _configSettingService;
        private readonly IAppUserServices _appUserServices;

        public SuspendedAutoRestoreJob(IUnitOfWork unitOfWork,
            ISuspendedAutoRestore suspendedAutoRestore,
            IConfigSettingService configSettingService,
            IAppUserServices appUserServices
            )
        {
            _unitOfWork = unitOfWork;
            _suspendedAutoRestore = suspendedAutoRestore;
            _configSettingService = configSettingService;
            _appUserServices = appUserServices;
        }

        public void Execute(IJobExecutionContext context)
        {
            Logger.Info("SuspendedAutoRestoreJob started", logEventTitle: LogEventTitles.SuspendedAutoRestore);

            try
            {
                var systemUserAppUserId = _configSettingService.GetInt32ValueById((int)ConfigurationSettings.SystemUserAppUserId);
                var systemUser = _appUserServices.GetById(systemUserAppUserId);
                if (systemUser == null)
                    throw new ArgumentNullException("systemUser");

                _suspendedAutoRestore.Restore(systemUser.AppUserId);

                _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.Error("SuspendedAutoRestoreJob encountered unexpected error.", exception: ex);
            }
            

            Logger.Info("SuspendedAutoRestoreJob ended.", logEventTitle: LogEventTitles.SuspendedAutoRestore);
        }
    }
}
