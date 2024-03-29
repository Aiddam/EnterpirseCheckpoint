﻿using Enterprise.Checkpoint.Interfaces.DataAccessInterfaces;
using EnterpriseCheckpoint.DataAccess.DbContexts;
using EnterpriseCheckpoint.DataAccess.Repositories;
using EnterpriseCheckpoint.Models.Models;

namespace EnterpriseCheckpoint.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MainDbContext _context;
        private readonly Dictionary<Type, object> _repositories;

        public UnitOfWork(MainDbContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
        }

        public Task<IRepository<T>> GetRepository<T>() where T : BaseEntity
        {
            var type = typeof(T);
            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(UnitOfWork)
                    .Assembly
                    .GetTypes()
                    .Where(t => t.IsAssignableTo(typeof(IRepository<T>)))
                    .FirstOrDefault()
                    ??
                    typeof(BaseRepository<T>);
                var repositoryInstance = Activator.CreateInstance(repositoryType, _context)
                    ?? throw new ArgumentException($"Unable to resolve repository with type {typeof(T).Name}");
                _repositories[type] = repositoryInstance;
            }
            return Task.FromResult((IRepository<T>)_repositories[type]);
        }

        public Task CommitAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
