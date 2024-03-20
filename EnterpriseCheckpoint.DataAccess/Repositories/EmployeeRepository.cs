using EnterpriseCheckpoint.Models.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EnterpriseCheckpoint.DataAccess.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>
    {
        public EmployeeRepository(DbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Employee>> ReadEntitiesByPredicate(Expression<Func<Employee, bool>> predicate, IEnumerable<KeyValuePair<Expression<Func<Employee, object>>, bool>>? orderBy = null, CancellationToken cancellationToken = default)
        {
            var query = _dbSet
                .Include(e => e.User)
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
    }
}
