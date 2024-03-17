using Autofac;

namespace EnterpriseCheckpoint.DataAccess
{
    public static class DependencyInjector
    {
        public static void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder
                .RegisterAssemblyTypes(typeof(DependencyInjector).Assembly)
                .AsSelf()
                .AsImplementedInterfaces();
        }
    }
}
