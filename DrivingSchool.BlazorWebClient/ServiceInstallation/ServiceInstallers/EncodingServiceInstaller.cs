using System.Globalization;
using System.Text;

namespace DrivingSchool.BlazorWebClient.ServiceInstallation.ServiceInstallers;

public class EncodingServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        var cultureInfo = new CultureInfo(configuration["CultureInfo"] ?? "ru-RU");
        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
    }
}