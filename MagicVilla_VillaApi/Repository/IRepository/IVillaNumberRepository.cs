using MagicVilla_VillaApi.Model;

namespace MagicVilla_VillaApi.Repository.IRepository
{
    public interface IVillaNumberRepository : IRepository<VillaNumber>
    {
        public Task UpdateAsync(VillaNumber entity);
    }
}
