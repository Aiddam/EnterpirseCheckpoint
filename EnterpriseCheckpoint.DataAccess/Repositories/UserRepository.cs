using Enterprise.Checkpoint.Interfaces.DataAccessInterfaces;
using EnterpriseCheckpoint.Models.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EnterpriseCheckpoint.DataAccess.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<User>> ReadEntitiesByPredicate(Expression<Func<User, bool>> predicate, IEnumerable<KeyValuePair<Expression<Func<User, object>>, bool>>? orderBy = null, CancellationToken cancellationToken = default)
        {
            var query = _dbSet
                .Include(u => u.Employee)
                .Where(predicate);

            if (orderBy is not null)
            {
                foreach (var order in orderBy)
                {
                    query = order.Value ? query.OrderBy(order.Key) : query.OrderByDescending(order.Key);
                }
            }

            return await query.ToListAsync(cancellationToken);
        }

        public override async Task<User?> ReadEntityByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(u => u.Employee)
                .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        }
    }
}
