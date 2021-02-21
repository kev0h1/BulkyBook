using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulkyBook.DataAccess.Repository
{
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {
        private ApplicationDbContext _db;

        public CoverTypeRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }
        public void Update(CoverType coverType)
        {
            var obj = _db.CoverTypes.FirstOrDefault(f => f.Id == coverType.Id);
            if (obj == null) return;
            obj.Name = coverType.Name;
            //_db.SaveChanges();
        }
    }
}
