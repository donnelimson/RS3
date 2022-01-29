using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Codebiz.Domain.ERP.Context;
using Domain.Context;
using Infrastructure;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Mvc;

namespace Web
{
    public class SiteModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AppCommonContext>().InstancePerLifetimeScope();
            builder.RegisterType<ERPDataContext>().InstancePerLifetimeScope();
            //builder.RegisterType<AccountDataContext>().InstancePerLifetimeScope();
            //builder.RegisterType<ItemDataContext>().InstancePerLifetimeScope();

            builder.Register(c => new ApplicationContext()
            {
                Contexts = new List<DbContext>()
                {
                    c.Resolve<AppCommonContext>(),
                    c.Resolve<ERPDataContext>()
                    //c.Resolve<ItemDataContext>()
                }
            }).As<ApplicationContext>();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            /*api*/
            builder.RegisterApiControllers(typeof(MvcApplication).Assembly);

            //Hi Devs, To Register Repositories and Services classes/interfaces go to the InfrastructureModule.cs instead! Thanks. - PAUL :)
            builder.RegisterModule(new InfrastructureModule());

            //builder.RegisterType<FileUploadHelper>().InstancePerLifetimeScope();          
        }

        private static void InjectInvoker(IActivatingEventArgs<object> obj)
        {
            var invoker = obj.Context.ResolveOptional<IActionInvoker>();
            if (invoker != null)
            {
                ((Controller)obj.Instance).ActionInvoker = invoker;
            }
        }
    }
}