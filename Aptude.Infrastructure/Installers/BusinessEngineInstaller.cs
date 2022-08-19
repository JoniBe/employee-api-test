using Aptude.Business.Factory;
using Aptude.Core.Contracts;
using Aptude.Infrastructure.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Aptude.Infrastructure.Installers
{
    public class BusinessEngineInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IBusinessEngineFactory, BusinessEngineFactory>();

            var engineTypes =
                typeof(BusinessEngineFactory).Assembly
                                             .ExportedTypes
                                             .Where(x => typeof(IBusinessEngine).IsAssignableFrom(x) &&
                                                         !x.IsInterface &&
                                                         !x.IsAbstract).ToList();

            engineTypes.ForEach(engineType =>
            {
                services.AddScoped(engineType.GetInterface($"I{engineType.Name}"), engineType);
            });
        }
    }
}
