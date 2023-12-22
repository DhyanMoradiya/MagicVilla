using System.Linq.Expressions;

namespace MagicVilla_VillaApi.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        public Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool Trecked = true, string? IncludeProperties = null);
        public Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? IncludeProperties = null);

        public Task CreateAsync(T entity);

        public Task RemoveAsync(T entity);

        public Task SaveAsync();
    }
}
