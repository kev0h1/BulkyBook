using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BulkyBook.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dBSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dBSet = _db.Set<T>();
        }
        public void Add(T entity)
        {
            dBSet.Add(entity);
        }

        public T Get(int id)
        {
            return dBSet.Find(id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = dBSet;
            if(filter != null)
            {
                query = query.Where(filter);
            }
            if(includeProperties != null)
            {
                foreach(var includeProp in includeProperties.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            if(orderBy != null)
            {
                return orderBy(query).ToList();
            }
            return query.ToList();
        }
        

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = dBSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
      
            return query.FirstOrDefault();
        }

        public void Remove(int id)
        {
            T entity = dBSet.Find(id);
            Remove(entity);
        }

        public void Remove(T entity)
        {
            dBSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dBSet.RemoveRange(entity);
        }
    }
}
