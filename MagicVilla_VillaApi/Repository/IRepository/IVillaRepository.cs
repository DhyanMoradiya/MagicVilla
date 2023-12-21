using MagicVilla_VillaApi.Model;
using System.Linq.Expressions;

namespace MagicVilla_VillaApi.Repository.IRepository
{
    public interface IVillaRepository : IRepository<Villa> 
    {
        public Task UpdateAsync(Villa entity);
    }
}
