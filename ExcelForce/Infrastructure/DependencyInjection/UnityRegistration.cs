using ExcelForce.Business;
using ExcelForce.Business.Interfaces;
using ExcelForce.Business.ServiceFactory;
using ExcelForce.Business.Services.ConfigurationInformation;
using ExcelForce.Business.Services.MapExtraction;
using ExcelForce.Business.Services.UserAuthentication;
using ExcelForce.Foundation.Authentication.Models;
using ExcelForce.Foundation.Authentication.Services;
using ExcelForce.Foundation.CoreServices.Authentication;
using ExcelForce.Foundation.CoreServices.FileManagement;
using ExcelForce.Foundation.CoreServices.FileManagement.Interfaces;
using ExcelForce.Foundation.CoreServices.Logger;
using ExcelForce.Foundation.CoreServices.Logger.Interfaces;
using ExcelForce.Foundation.CoreServices.Models;
using ExcelForce.Foundation.CoreServices.Repository;
using ExcelForce.Foundation.CoreServices.Serialization;
using ExcelForce.Foundation.CoreServices.Serialization.Interfaces;
using ExcelForce.Foundation.CoreServices.ServiceCallWrapper;
using ExcelForce.Foundation.CoreServices.ServiceCallWrapper.Interfaces;
using ExcelForce.Foundation.EntityManagement.Interfaces.ServiceInterfaces;
using ExcelForce.Foundation.EntityManagement.Models.Api.SfObject;
using ExcelForce.Foundation.EntityManagement.Models.ExtractMap;
using ExcelForce.Foundation.EntityManagement.Models.SfEntities;
using ExcelForce.Foundation.EntityManagement.Repository;
using ExcelForce.Foundation.EntityManagement.Services;
using ExcelForce.Foundation.ProfileManagement;
using ExcelForce.Foundation.ProfileManagement.Models;
using Unity;

namespace ExcelForce.Infrastructure.DependencyInjection
{
    public class UnityManager
    {
        private static IUnityContainer container;

        public static void Initialize()
        {
            container = container ?? new UnityContainer();

            container = RegisterCoreServiceContainers(container);

            container = RegisterEntityManagementContainers(container);

            container = RegisterBusinessDependencies(container);

            container.RegisterType<IExcelForceServiceFactory, ExcelForceServiceFactory>();
        }

        public static T GetInstance<T>() where T : class
        {
            if (container == null)
                return null;

            return container.Resolve<T>();
        }

        public static void RegisterAdditionalDependencies<T>(T instanceObject)
        {
            container = container ?? new UnityContainer();

            container.RegisterInstance(instanceObject);
        }

        private static IUnityContainer RegisterBusinessDependencies(IUnityContainer container)
        {
            container.RegisterType<IExcelForceRepository<ConnectionProfile, string>
                , ConnectionProfileRepository>();

            container.RegisterType<IExcelForceRepository<ExtractMap, string>
                , ExtractMapRepository>();

            container.RegisterType<IAuthenticationManager<AuthenticationRequest, AuthenticationResponse>
                , SalesforceAuthenticationManager>();

            container.RegisterType<IServiceCallWrapper<AuthenticationResponse, ApiError>
                , ServiceCallWrapper<AuthenticationResponse, ApiError>>();

            container.RegisterType<IServiceCallWrapper<SfObjectApiResponse, ApiError>
              , ServiceCallWrapper<SfObjectApiResponse, ApiError>>();

            container.RegisterType<IConfigurationInformationService
                , ConfigurationInformationService>();

            container.RegisterType<IExtractMapService
                , ExtractMapService>();

            container.RegisterType<IRibbonBaseService
                , RibbonBaseService>();

            container.RegisterType<IUserAuthenticationService
                , UserAuthenticationService>();

            container.RegisterType<ICreateExtractionMapService
                , CreateExtractionMapService>();

            return container;
        }

        private static IUnityContainer RegisterEntityManagementContainers(IUnityContainer container)
        {
            container.RegisterType<ISfAttributeService
                , SfAttributeService>();

            container.RegisterType<ISfObjectService
                , SfObjectService>();

            return container;
        }

        private static IUnityContainer RegisterCoreServiceContainers(IUnityContainer container)
        {
            container.RegisterType<ISfQueryService
                , SfQueryService>();

            container.RegisterType<IContentStreamManager
                , FileContentManager>();

            container.RegisterType<ILoggerManager
                , LoggerManager>();

            container.RegisterType<IContentSerializationManager
               , JsonSerializer>();

            container.RegisterType<IWebApiHttpClient
                , WebApiHttpClient>();

            return container;
        }
    }
}
