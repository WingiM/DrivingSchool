﻿using System.Reflection;

namespace DrivingSchool.ServiceInstallation;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection InstallServices(this IServiceCollection services, IConfiguration configuration,
        params Assembly[] assemblies)
    {
        var serviceInstallers = assemblies
            .SelectMany(x => x.DefinedTypes)
            .Where(x => x.IsAssignableTo(typeof(IServiceInstaller)))
            .Select(Activator.CreateInstance)
            .Cast<IServiceInstaller>();
        foreach (var installer in serviceInstallers)
        {
            installer.Install(services, configuration);
        }

        return services;
    }
}