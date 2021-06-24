using Logging;
using PnpPais.Infrastructure.Utilities;
using Quartz;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PnpPais.BackgroundWorker
{
    public class SendLeaveSectionEmailNotificationJob : IJob
    {
        private readonly ILeaveSectionEmailNotificationSender _leaveSectionEmailNotificationSender;

        public SendLeaveSectionEmailNotificationJob(ILeaveSectionEmailNotificationSender leaveSectionEmailNotificationSender)
        {
            _leaveSectionEmailNotificationSender = leaveSectionEmailNotificationSender;
        }

        public void Execute(IJobExecutionContext context)
        {
            Logger.Info("LeaveSectionEmailNotificationSender started. " + DateTime.Now);

            try
            {
                Logger.Info("Auto send of email notification for leave section users started. ");

                var mailPath = System.IO.Directory.GetCurrentDirectory();

                _leaveSectionEmailNotificationSender.SendEmail(mailPath);
            }
            catch (Exception ex)
            {
                Logger.Error("LeaveSectionEmailNotificationSender encountered unexpected error.", exception: ex);
            }


            Logger.Info("LeaveSectionEmailNotificationSender ended. " + DateTime.Now);
        }
    }
}
