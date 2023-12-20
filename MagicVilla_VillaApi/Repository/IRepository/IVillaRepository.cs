using MagicVilla_VillaApi.Model;
using System.Linq.Expressions;

namespace MagicVilla_VillaApi.Repository.IRepository
{
    public interface IVillaRepository
    {
        public Task<Villa> GetAsync(Expression<Func<Villa, bool>>? filter = null,bool Trecked = true);
        public Task<IEnumerable<Villa>> GetAllAsync(Expression<Func<Villa, bool>>? filter = null);

        public Task CreateAsync(Villa entity);

        public Task RemoveAsync(Villa entity);
        public Task UpdateAsync(Villa entity);

        public Task SaveAsync();
    }
}
