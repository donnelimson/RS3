using Autofac;
using Codebiz.Domain.ERP.Repository;
using Codebiz.Domain.Repository;
using Domain.Repository;
using Domain.Repository.Billing.MeterReadingRemarks;
using Domain.Repository.Collection;
using Domain.Repository.CSA;
using Domain.Repository.CSA.Billing;
using Domain.Repository.Financial;
using Domain.Repository.Financials;

using Infrastructure.Repository;
using Infrastructure.Repository.Common;
using Infrastructure.Repository.MD;
using Infrastructure.Repository.Sales;
using Infrastructure.Services;
using Infrastructure.Services.Common;
using Infrastructure.Services.MD;
using Infrastructure.Services.Sale;
using Infrastructure.Utilities;
using System.Security.Cryptography;

namespace Infrastructure
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(RepositoryBase<>))
                .As(typeof(IRepositoryBase<>));


            RegisterRepositories(builder);
            RegisterServices(builder);

            builder.Register(c => new HashHelper(new SHA1CryptoServiceProvider())).As<IHashHelper>().SingleInstance();
            builder.RegisterType<EmailHelper>().As<IEmailHelper>().InstancePerLifetimeScope();
            builder.RegisterType<PasswordHelper>().As<IPasswordHelper>().SingleInstance();
        }

        private void RegisterServices(ContainerBuilder builder)
        {
            #region User

            builder.RegisterType<AppUserServices>().As<IAppUserServices>().InstancePerLifetimeScope();
            builder.RegisterType<UserGroupServices>().As<IUserGroupServices>().InstancePerLifetimeScope();
   //         builder.RegisterType<LoginHistoryServices>().As<ILoginHistoryServices>().InstancePerLifetimeScope();
  
            #endregion

     
            #region Permission

            builder.RegisterType<PermissionServices>().As<IPermissionServices>().InstancePerLifetimeScope();
            builder.RegisterType<PermissionGroupServices>().As<IPermissionGroupServices>().InstancePerLifetimeScope();

            #endregion

            #region Navigation

            builder.RegisterType<NavLinkServices>().As<INavLinkServices>().InstancePerLifetimeScope();

            #endregion

            #region Configuration

            builder.RegisterType<ConfigSettingService>().As<IConfigSettingService>().InstancePerLifetimeScope();
            builder.RegisterType<ConfigSettingDataTypeService>().As<IConfigSettingDataTypeService>().InstancePerLifetimeScope();
            builder.RegisterType<ConfigSettingGroupService>().As<IConfigSettingGroupService>().InstancePerLifetimeScope();

            #endregion

            #region Log

            builder.RegisterType<LogService>().As<ILogService>().InstancePerLifetimeScope();
            builder.RegisterType<LogEventTitleService>().As<ILogEventTitleService>().InstancePerLifetimeScope();

            #endregion


            #region Management





            builder.RegisterType<LoginHistoryServices>().As<ILoginHistoryServices>().InstancePerLifetimeScope();



            #endregion



        
            builder.RegisterType<ContentFileService>().As<IContentFileService>().InstancePerLifetimeScope();
            builder.RegisterType<FileTypeService>().As<IFileTypeService>().InstancePerLifetimeScope();
            builder.RegisterType<FileServices>().As<IFileServices>().InstancePerLifetimeScope();

            /*ERP*/
            //builder.RegisterType<FinanceDataService>().As<IFinanceDataService>().InstancePerLifetimeScope();
            //builder.RegisterType< ERP.InventoryDataService >().As<ERP.IInventoryDataService>().InstancePerLifetimeScope();
            //builder.RegisterType<SalesTransactionDataService>().As<ISalesTransactionDataService>().InstancePerLifetimeScope();
            //builder.RegisterType<BusinessPartnerDataService>().As<IBusinessPartnerDataService>().InstancePerLifetimeScope();





            #region Vehicles


            #endregion

            #region RS3
            builder.RegisterType<RoleService>().As<IRoleService>().InstancePerLifetimeScope();


            #endregion
            #region Naiahs
            builder.RegisterType<ItemMasterService>().As<IItemMasterService>().InstancePerLifetimeScope();
            builder.RegisterType<PriceListService>().As<IPriceListService>().InstancePerLifetimeScope();
            builder.RegisterType<BrandService>().As<IBrandService>().InstancePerLifetimeScope();
            builder.RegisterType<SaleTransactionService>().As<ISaleTransactionService>().InstancePerLifetimeScope();

            #endregion

        }

        private void RegisterRepositories(ContainerBuilder builder)
        {
            #region User

            builder.RegisterType<AppUserRepository>().As<IAppUserRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UserGroupRepository>().As<IUserGroupRepository>().InstancePerLifetimeScope();
            builder.RegisterType<LoginHistoryRepository>().As<ILoginHistoryRepository>().InstancePerLifetimeScope();

            #endregion

            #region Notification
            #endregion

            #region Permission

            builder.RegisterType<AccessLevelRepository>().As<IAccessLevelRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AccessLevelRepository>().As<IAccessLevelRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PermissionRepository>().As<IPermissionRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PermissionGroupRepository>().As<IPermissionGroupRepository>().InstancePerLifetimeScope();

            #endregion

            #region Navigation

            builder.RegisterType<NavLinkRepository>().As<INavLinkRepository>().InstancePerLifetimeScope();

            #endregion

            #region Configuration

            builder.RegisterType<ConfigSettingRepository>().As<IConfigSettingRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ConfigSettingDataTypeRepository>().As<IConfigSettingDataTypeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ConfigSettingGroupRepository>().As<IConfigSettingGroupRepository>().InstancePerLifetimeScope();

            #endregion

            #region Log

            builder.RegisterType<LogEventTitleRepository>().As<ILogEventTitleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<LogRepository>().As<ILogRepository>().InstancePerLifetimeScope();

            #endregion

            #region Management


            #endregion

            #region Approval

            #endregion

            builder.RegisterType<ContentFileRepository>().As<IContentFileRepository>().InstancePerLifetimeScope();
            builder.RegisterType<FileTypeRepository>().As<IFileTypeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<LoginHistoryRepository>().As<ILoginHistoryRepository>().InstancePerLifetimeScope();

            #region RS3
            builder.RegisterType<RoleRepository>().As<IRoleRepository>().InstancePerLifetimeScope();


            #endregion
            #region Naiahs
            builder.RegisterType<ItemMasterRepository>().As<IItemMasterRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PriceListRepository>().As<IPriceListRepository>().InstancePerLifetimeScope();
            builder.RegisterType<BrandRepository>().As<IBrandRepository>().InstancePerLifetimeScope();
            builder.RegisterType<SaleTransactionRepository>().As<ISaleTransactionRepository>().InstancePerLifetimeScope();

            #endregion





        }
    }
}