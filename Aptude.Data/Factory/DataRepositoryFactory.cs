using Aptude.Core.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;


namespace Aptude.Data.Factory
{
    public class DataRepositoryFactory : IDataRepositoryFactory
    {
        private readonly IServiceProvider _Services;

        public DataRepositoryFactory() { }

        public DataRepositoryFactory(IServiceProvider services)
        {
            this._Services = services;
        }

        public IDataRepository<TEntity> GetDataRepository<TEntity>() where TEntity : class, new()
        {
            return _Services.GetService<IDataRepository<TEntity>>();
        }

        public IUnitOfWork GetUnitOfWork()
        {
            return _Services.GetService<IUnitOfWork>();
        }

        public IUnitOfWork GetUnitOfWork<T>() where T : IUnitOfWork
        {
            return _Services.GetService<T>();
        }
    }
}
