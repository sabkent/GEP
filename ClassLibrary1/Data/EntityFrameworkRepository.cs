using System.Data.Entity;

namespace ClassLibrary1.Data
{
    public class EntityFrameworkRepository<TEntity, TKey> where TEntity : class
    {
        private readonly DbContext _dbContext;

        public EntityFrameworkRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }
    }
}
