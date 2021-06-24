using Atlas;
using Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundWorker
{
    public class JobListener : IJobListener
    {
        private readonly IContainerProvider _containerProvider;
        private IUnitOfWorkContainer _container;

        public JobListener(IContainerProvider containerProvider)
        {
            if (containerProvider == null)
                throw new ArgumentNullException("containerProvider");

            _containerProvider = containerProvider;
        }

        public void JobToBeExecuted(IJobExecutionContext context)
        {
            _container = _containerProvider.CreateUnitOfWork();
            _container.InjectUnsetProperties(context.JobInstance);


            //Logger.Info("{0}-{1} started", context.JobDetail.Key.Name, context.FireInstanceId);
        }

        public void JobExecutionVetoed(IJobExecutionContext context)
        {
            /*noop*/

            //Logger.Info("{0}-{1} vetoed", context.JobDetail.Key.Name, context.FireInstanceId);
        }

        public void JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException)
        {
            _container.Dispose();

            //Logger.Info("{0}-{1} finished", context.JobDetail.Key.Name, context.FireInstanceId);
            //if (jobException != null)
            //    Logger.Error("{0}-{1} finished with error. {2}", context.JobDetail.Key.Name, context.FireInstanceId, jobException.InnerException.ToString());
        }

        public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public string Name
        {
            get { return "JobListener"; }
        }
    }
}
