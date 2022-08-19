using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Aptude.Infrastructure.Contracts
{
    public interface IInstaller
    {
        void InstallServices(IServiceCollection services, IConfiguration configuration);

    }
}
