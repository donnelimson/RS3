using Atlas;
using Autofac;
using Domain.Context;
using Infrastructure;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System.Data.Entity;
using System.Linq;

namespace BackgroundWorker
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            LoadQuartz(builder);
            LoadServices(builder);

            builder.RegisterType<AppCommonContext>().As<DbContext>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            //Hi Devs, To Register Repositories and Services classes/interfaces go to the InfrastructureModule.cs instead! Thanks. - PAUL :)
            builder.RegisterModule(new InfrastructureModule());
        }

        private static void LoadQuartz(ContainerBuilder builder)
        {
            builder.Register(c => new StdSchedulerFactory().GetScheduler()).As<IScheduler>().InstancePerLifetimeScope();
            builder.Register(c => new JobFactory(ContainerProvider.Instance.ApplicationContainer)).As<IJobFactory>();

            builder.RegisterAssemblyTypes(System.Reflection.Assembly.GetExecutingAssembly())
                   .Where(p => typeof(IJob).IsAssignableFrom(p))
                   .PropertiesAutowired();
            builder.Register(c => new JobListener(ContainerProvider.Instance)).As<IJobListener>();
        }

        private static void LoadServices(ContainerBuilder builder)
        {
            builder.RegisterType<ImportService>().As<IAmAHostedProcess>().PropertiesAutowired();
        }
    }
}
