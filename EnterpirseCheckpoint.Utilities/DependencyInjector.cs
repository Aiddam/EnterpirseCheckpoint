using Autofac;
using AutoMapper;

namespace EnterpirseCheckpoint.Utilities
{
    public static class DependencyInjector
    {
        public static void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder
                .RegisterAssemblyTypes(typeof(DependencyInjector).Assembly)
                .AsSelf()
                .AsImplementedInterfaces();

            containerBuilder.Register(c =>
            {
                var context = c.Resolve<IComponentContext>();

                // Створення конфігурації з використанням сканування асемблеї для знаходження профілів
                var config = new MapperConfiguration(cfg =>
                {
                    // Сканування асемблеї для автоматичного виявлення профілів
                    cfg.AddMaps(typeof(DependencyInjector).Assembly); // Змінено на AddMaps
                });

                return config.CreateMapper();
            }).As<IMapper>().SingleInstance();
        }
    }
}
