using Unity;
using Unity.RegistrationByConvention;

namespace ExcelForce.Foundation.DI
{
    public static class UnityIntializer
    {
        public static void Initialize()
        {
            IUnityContainer container = new UnityContainer();

            container.RegisterTypes(
                AllClasses.FromAssembliesInBasePath(),
                (c) => WithMappings.FromMatchingInterface(c));
        }
    }
}
