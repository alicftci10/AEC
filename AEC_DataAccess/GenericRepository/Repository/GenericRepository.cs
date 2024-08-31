using AEC_DataAccess.GenericRepository.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AEC_DataAccess.DBContext;

namespace AEC_DataAccess.GenericRepository.Repository
{
    public class GenericRepository<T> : IRepository<T>, IDisposable where T : class
    {
        private readonly AecommerceDbContext _context;
        private readonly DbSet<T> _dbSet;
        private IDbContextTransaction _transaction;


        public GenericRepository(AecommerceDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            if (_context.Database.CurrentTransaction != null)
            {
                _transaction = _context.Database.CurrentTransaction;
            }
            else
            {
                _transaction = _context.Database.BeginTransaction();
            }

        }

        public T Add(T entity)
        {
            _dbSet.Add(entity);
            SaveChanges();
            return entity;
        }

        public T Update(T entity)
        {
            _dbSet.Update(entity);
            SaveChanges();
            return entity;
        }

        public T Delete(int pId)
        {
            var delete = _dbSet.Find(pId);
            _dbSet.Remove(delete);
            SaveChanges();
            return delete;
        }

        public T GetSelect(int pId)
        {
            return _dbSet.Find(pId);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }


        public void SaveChanges()
        {
            try
            {
                _context.SaveChanges();
                _transaction.Commit();
            }
            catch (Exception ex)
            {
                //Log yazma
                _transaction.Rollback();
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
