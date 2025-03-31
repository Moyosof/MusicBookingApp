
using MusicBookingApp.Application.Repositories.Base;
using MusicBookingApp.Infrastructure.Data;

namespace MusicBookingApp.Infrastructure.Repositories.Base
{
    public abstract class BaseRepository<TEntity>(DataContext context) : IRepository<TEntity>
        where TEntity : class
    {
        protected readonly DataContext Context = context;

        public virtual async Task Add(TEntity entity)
        {
            await Context.AddAsync(entity);
        }

        public virtual async Task<TEntity?> GetById(string id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public virtual void Update(TEntity entity)
        {
            Context.Update(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            Context.Remove(entity);
        }

        public virtual IQueryable<TEntity> GetQueryable()
        {
            return Context.Set<TEntity>().AsQueryable();
        }

        public virtual async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}
