using Logging;
using Codebiz.Domain.Common.Model.Enums;
using Codebiz.Domain.Common.Model;
using Domain.Repository;
using Infrastructure;
using Infrastructure.Services;
using Infrastructure.Utilities;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundWorker
{
    public class ImporterJob : IJob
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfigSettingService _configSettingService;
        private readonly IAppUserServices _appUserServices;


        public ImporterJob(IUnitOfWork unitOfWork,
            IConfigSettingService configSettingService,
            IAppUserServices appUserServices
            )
        {
            _unitOfWork = unitOfWork;

            _configSettingService = configSettingService;
            _appUserServices = appUserServices;
        }

        public void Execute(IJobExecutionContext context)
        {
            var cid = context.FireInstanceId;
            Logger.Info("job was executed at {0}, instance : {1}",DateTime.Now.ToLongTimeString(), cid);
            System.Threading.Thread.Sleep(1000*70);
            Logger.Info("job was finished at {0}, instance : {1}", DateTime.Now.ToLongTimeString(), cid);
            System.Threading.Thread.Sleep(1000*70);
        }

    }
}
