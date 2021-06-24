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
    public class CreateSalaryStepIncrementOrderJob : IJob
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISalaryStepIncrementOrderCreator _salaryStepIncrementOrderCreator;
        private readonly IConfigSettingService _configSettingService;
        private readonly IAppUserServices _appUserServices;

        public CreateSalaryStepIncrementOrderJob(IUnitOfWork unitOfWork,
            ISalaryStepIncrementOrderCreator salaryStepIncrementOrderCreator,
            IConfigSettingService configSettingService,
            IAppUserServices appUserServices
            )
        {
            _unitOfWork = unitOfWork;
            _salaryStepIncrementOrderCreator = salaryStepIncrementOrderCreator;
            _configSettingService = configSettingService;
            _appUserServices = appUserServices;
        }

        public void Execute(IJobExecutionContext context)
        {
            Logger.Info("CreateSalaryStepIncrementOrderJob started. " + DateTime.Now);

            try
            {
                var systemUserAppUserId = _configSettingService.GetInt32ValueById((int)ConfigurationSettings.SystemUserAppUserId);
                var systemUser = _appUserServices.GetById(systemUserAppUserId);
                if (systemUser == null)
                    throw new ArgumentNullException("systemUser");

                var creationOfSalaryStepIncrementOrderDayOfMonth = _configSettingService.GetInt32ValueById((int)ConfigurationSettings.CreationOfSalaryStepIncrementOrderDayOfMonth);
                if (creationOfSalaryStepIncrementOrderDayOfMonth < 1 || creationOfSalaryStepIncrementOrderDayOfMonth > 31)
                    throw new Exception("Invalid creationOfSalaryStepIncrementOrderDayOfMonth settings.");

                if (DateTime.Now.Day == creationOfSalaryStepIncrementOrderDayOfMonth)
                {
                    Logger.Info("Auto creation of salary step increment order started. ");

                    var dateToExtract = DateTime.Now.AddMonths(1);

                    _salaryStepIncrementOrderCreator.Create(dateToExtract.Year, dateToExtract.Month, systemUser.AppUserId);

                    _unitOfWork.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("ImplementOrderJob encountered unexpected error.", exception: ex);
            }
            

            Logger.Info("CreateSalaryStepIncrementOrderJob ended. " + DateTime.Now);
        }
    }
}
