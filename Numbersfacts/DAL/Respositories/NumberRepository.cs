using Microsoft.EntityFrameworkCore;
using Numbersfacts.DAL.Interfaces;
using Numbersfacts.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numbersfacts.DAL.Respositories
{
    public class NumberRepository : IBaseRespository<Fact>
    {
        private readonly ApplicationDbContext _db;

        public NumberRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Fact entity)
        {
            await _db.Fact.AddAsync(entity);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(Fact entity)
        {
            _db.Fact.Remove(entity);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<Fact> Get(string year)
        {
            return await _db.Fact.FirstOrDefaultAsync(x => x.Year == year);
        }

        public async Task<List<Fact>> Select()
        {
            var facts = await _db.Fact.ToListAsync();
            return facts;
        }

        public async Task<bool> Update(Fact entity)
        {
            _db.Fact.Update(entity);
            await _db.SaveChangesAsync();

            return true;
        }
        public async Task<Fact> GetID(int id)
        {
            return await _db.Fact.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
