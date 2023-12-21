using MagicVilla_VillaApi.Model;
using MagicVilla_VillaApi.Repository.IRepository;

namespace MagicVilla_VillaApi.Repository
{
    public class VillaNumberRepository : Repository<VillaNumber>, IVillaNumberRepository
    {
        private readonly ApplicationDbContext _db; 
        public VillaNumberRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }
        public async Task UpdateAsync(VillaNumber entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.VillaNumbers.Update(entity);
            await SaveAsync();
        }
    }
}
