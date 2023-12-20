using MagicVilla_VillaApi.Model;
using MagicVilla_VillaApi.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MagicVilla_VillaApi.Repository
{
    public class VillaRepository : IVillaRepository
    {

        private readonly ApplicationDbContext _db;
        public VillaRepository(ApplicationDbContext db)
        {
            _db = db;

        }

        public async Task CreateAsync(Villa entity)
        {
            await _db.Villas.AddAsync(entity);
            await SaveAsync();
        }

        public async Task<IEnumerable<Villa>> GetAllAsync(Expression<Func<Villa, bool>>? filter = null)
        {
            IQueryable<Villa> query = _db.Villas;
            if(filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task<Villa> GetAsync(Expression<Func<Villa, bool>>? filter = null, bool Trecked = true)
        {
            IQueryable<Villa> query = _db.Villas;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (!Trecked)
            {
                query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(Villa entity)
        {
            _db.Villas.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
           await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Villa entity)
        {
            entity.UpdateDate = DateTime.Now;
            _db.Villas.Update(entity);
            await SaveAsync();
        }
    }
}
