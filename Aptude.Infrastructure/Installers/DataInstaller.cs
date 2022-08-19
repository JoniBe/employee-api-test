using Aptude.Data;
using Aptude.Infrastructure.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;


namespace Aptude.Infrastructure.Installers
{
    public class DataInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            //One instance per request
            services.AddDbContext<AptudeDbContext>(options =>
                    options.UseInMemoryDatabase("AptudeDB"));
        }
    }
}
