using Aptude.Core.Base;

namespace Aptude.Data.Models
{
    public class Repository<TEntity> : RepositoryBase<TEntity, AptudeDbContext> where TEntity : class, new()
    {
        public Repository(AptudeDbContext context) : base(context) { }
    }
}
