using Autofac;
using AutoMapper;
using EnterpirseCheckpoint.Utilities.Mappers;
using EnterpirseCheckpoint.ViewModels;
using Enterprise.Checkpoint.Interfaces.Services;
using EnterpriseCheckpoint.Models.Models;
using System;

namespace EnterpirseCheckpoint
{
    public static class DependencyInjector
    {
        public delegate HomeViewModel HomeViewModelFactory(User user);
        public static void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder
                .RegisterAssemblyTypes(typeof(DependencyInjector).Assembly)
                .Where(t => 
                    t.Name.Contains("ViewModel") ||
                    t.Name.Contains("ViewLocator")
                )
                .AsSelf()
                .AsImplementedInterfaces();
            containerBuilder.RegisterType<HomeViewModel>().AsSelf();

            containerBuilder.Register<Func<User, HomeViewModel>>(c =>
            {
                var context = c.Resolve<IComponentContext>();
                return user => new HomeViewModel(context, user);
            });

            containerBuilder.RegisterType<RegistrationViewModel>().AsSelf();

            EnterpriseCheckpoint.Services.DependencyInjector.Load(containerBuilder);
            Utilities.DependencyInjector.Load(containerBuilder);
        }
    }
}
