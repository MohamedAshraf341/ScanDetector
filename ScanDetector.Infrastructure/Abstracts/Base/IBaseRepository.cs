namespace ScanDetector.Infrastructure.Abstracts.Base
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        T Update(T entity);
        Task<int> CountAsync();
        void Delete(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        IQueryable<T> GetTableNoTracking();
    }
}
