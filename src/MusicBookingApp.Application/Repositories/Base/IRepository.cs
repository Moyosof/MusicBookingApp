namespace MusicBookingApp.Application.Repositories.Base
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        Task Add(TEntity entity);
        Task<TEntity?> GetById(string id);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        IQueryable<TEntity> GetQueryable();
        Task SaveAsync();
    }
}
