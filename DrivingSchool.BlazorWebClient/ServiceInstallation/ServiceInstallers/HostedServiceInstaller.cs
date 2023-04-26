using DrivingSchool.BlazorWebClient.HostedServices;

namespace DrivingSchool.BlazorWebClient.ServiceInstallation.ServiceInstallers;

public class HostedServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHostedService<AddInitialUserHostedService>()
            .AddHostedService<AddTicketsToDatabaseHostedService>()
            .AddHostedService<UploadImagesHostedService>();
    }
}