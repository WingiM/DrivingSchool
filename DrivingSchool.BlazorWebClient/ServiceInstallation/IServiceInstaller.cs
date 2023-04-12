namespace DrivingSchool.BlazorWebClient.ServiceInstallation;

public interface IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration);
}