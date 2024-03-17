using EnterpriseCheckpoint.Models.Models;

namespace Enterprise.Checkpoint.Interfaces.DataAccessInterfaces
{
    public interface IUnitOfWork
    {
        Task<IRepository<T>> GetRepository<T>()
            where T : BaseEntity;
        Task CommitAsync();
    }
}
