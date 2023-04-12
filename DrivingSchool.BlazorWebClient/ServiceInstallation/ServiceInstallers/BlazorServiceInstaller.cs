using MudBlazor.Services;

namespace DrivingSchool.BlazorWebClient.ServiceInstallation.ServiceInstallers;

public class BlazorServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddRazorPages();
        services.AddServerSideBlazor();
        services.AddMudServices();
    }
}