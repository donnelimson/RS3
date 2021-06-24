using Atlas;
using Logging;
using Quartz;
using Quartz.Spi;

namespace BackgroundWorker
{
    public class ImportService : IAmAHostedProcess
    {
        public IScheduler Scheduler { get; set; }
        public IJobFactory JobFactory { get; set; }
        public IJobListener AutofacJobListener { get; set; }
        private const int MinuteInterval = 1;

        #region Implementation of IAmAHostedProcess
        public void Pause()
        {
            Logger.Info("AML Background Worker pause processing");
        }

        public void Resume()
        {
            Logger.Info("AML Background Worker resume processing");
        }

        public void Start()
        {
            Scheduler.JobFactory = JobFactory;

            Scheduler.ListenerManager.AddJobListener(AutofacJobListener);
            Scheduler.Start();

            Logger.Info("AML Background Worker start processing");
        }

        public void Stop()
        {
            Logger.Info("AML Background Worker stop processing");
        }
        #endregion
    }
}