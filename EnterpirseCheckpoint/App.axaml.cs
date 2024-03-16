using Autofac;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using EnterpirseCheckpoint.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EnterpirseCheckpoint;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var serviceContainer = GetServiceContainer();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = serviceContainer.Resolve<MainWindow>();
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = serviceContainer.Resolve<MainView>();
        }

        base.OnFrameworkInitializationCompleted();
    }

    private static IContainer GetServiceContainer()
    {
        var containerBuilder = new ContainerBuilder();
        containerBuilder
            .RegisterInstance(GetConfiguration())
            .AsImplementedInterfaces();

        DependencyInjector.Load(containerBuilder);

        return containerBuilder.Build();
    }

    private static IConfiguration GetConfiguration()
    {
        return new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true)
            .Build();
    }
}
