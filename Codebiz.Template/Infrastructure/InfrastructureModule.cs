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
using Domain.Repository.NatureType;
using Infrastructure.Repository;
using Infrastructure.Repository.Common;
using Infrastructure.Services;
using Infrastructure.Services.Common;
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
            builder.RegisterType<EmployeePhotoService>().As<IEmployeePhotoService>().InstancePerLifetimeScope();
            builder.RegisterType<EmployeeService>().As<IEmployeeService>().InstancePerLifetimeScope();

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

            builder.RegisterType<RegionService>().As<IRegionService>().InstancePerLifetimeScope();
            builder.RegisterType<ProvinceService>().As<IProvinceService>().InstancePerLifetimeScope();
            builder.RegisterType<CityTownService>().As<ICityTownService>().InstancePerLifetimeScope();
            builder.RegisterType<BarangayService>().As<IBarangayService>().InstancePerLifetimeScope();
            builder.RegisterType<PurokServices>().As<IPurokServices>().InstancePerLifetimeScope();
            builder.RegisterType<SitioServices>().As<ISitioServices>().InstancePerLifetimeScope();
       

            //Job Order
      
            builder.RegisterType<NatureTypeService>().As<IJobOrderNatureTypeService>().InstancePerLifetimeScope();

            //Country


            //Shipping Types



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


        }

        private void RegisterRepositories(ContainerBuilder builder)
        {
            #region User

            builder.RegisterType<AppUserRepository>().As<IAppUserRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UserGroupRepository>().As<IUserGroupRepository>().InstancePerLifetimeScope();
            builder.RegisterType<LoginHistoryRepository>().As<ILoginHistoryRepository>().InstancePerLifetimeScope();
            builder.RegisterType<EmployeePhotoRepository>().As<IEmployeePhotoRepository>().InstancePerLifetimeScope();
            builder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>().InstancePerLifetimeScope();

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

            builder.RegisterType<RegionRepository>().As<IRegionRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ProvinceRepository>().As<IProvinceRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CityTownRepository>().As<ICityTownRepository>().InstancePerLifetimeScope();
            builder.RegisterType<BarangayRepository>().As<IBarangayRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PurokRepository>().As<IPurokRepository>().InstancePerLifetimeScope();
            builder.RegisterType<SitioRepository>().As<ISitioRepository>().InstancePerLifetimeScope();
      

            #endregion

            #region Approval

            #endregion

            builder.RegisterType<ContentFileRepository>().As<IContentFileRepository>().InstancePerLifetimeScope();
            builder.RegisterType<FileTypeRepository>().As<IFileTypeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<LoginHistoryRepository>().As<ILoginHistoryRepository>().InstancePerLifetimeScope();






       
        }
    }
}