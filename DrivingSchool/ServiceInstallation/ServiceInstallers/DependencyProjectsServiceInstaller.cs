using DrivingSchool.Data;
using DrivingSchool.Domain;
using DrivingSchool.GridFS;

namespace DrivingSchool.ServiceInstallation.ServiceInstallers;

public class DependencyProjectsServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDomain(configuration)
            .AddData(configuration)
            .AddFileSystem(configuration);
    }
}