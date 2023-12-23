using MagicVilla_VillaApi.Model;

namespace MagicVilla_VillaApi.Repository.IRepository
{
    public interface IVillaRepository : IRepository<Villa> 
    {
        public Task UpdateAsync(Villa entity);
    }
}
