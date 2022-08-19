using Aptude.Core.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;


namespace Aptude.Business.Factory
{
    public class BusinessEngineFactory : IBusinessEngineFactory
    {
        private readonly IServiceProvider _Services;

        public BusinessEngineFactory(IServiceProvider services)
        {
            this._Services = services;
        }

        public T GetBusinessEngine<T>() where T : IBusinessEngine
        {
            return _Services.GetService<T>();
        }
    }
}
