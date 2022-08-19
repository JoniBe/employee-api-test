using Microsoft.EntityFrameworkCore.Storage;

namespace Aptude.Core.Contracts
{
    public interface IUnitOfWork
    {
        IDbContextTransaction CreateTransaction();

        int SaveChanges();
    }
}
