using ExcelForce.Foundation.Authentication.Models;
using ExcelForce.Foundation.CoreServices.Models;
using ExcelForce.Foundation.CoreServices.ServiceCallWrapper;
using ExcelForce.Foundation.CoreServices.ServiceCallWrapper.Interfaces;
using ExcelForce.Foundation.Persistence.Persitence;
using ExcelForce.Infrastructure.DataPersistence;
using ExcelForce.Models;
using Unity;

namespace ExcelForce.Foundation.DI
{
    public static class UnityIntializer
    {
        public static void Initialize()
        {
            IUnityContainer container = new UnityContainer();

            container.RegisterType<IServiceCallWrapper<AuthenticationResponse, ApiError>
                , ServiceCallWrapper<AuthenticationResponse, ApiError>>();

            container.RegisterType<IPersistenceContainer
                , ExcelForcePersistenceContainer>();


        }
    }
}
