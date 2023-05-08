using E_LearningApp.Models.Database;
using E_LearningApp.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_LearningApp.Models.DataAccessLayer
{
    public class BaseDL<T> where T : BaseEntity
    {
        protected readonly AppDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public BaseDL(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public T GetById(int id)
        {
            return _dbSet.FirstOrDefault(entity => entity.Id == id);
        }

        public void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _dbSet.AddOrUpdate(entity);
        }

        /// <summary>
        ///     This method will actually remove the row from the database.
        /// </summary>
        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public List<T> GetAll()
        {
            return GetRecords().ToList();
        }

        public bool Any(Func<T, bool> expression)
        {
            return GetRecords().Any(expression);
        }

        protected IQueryable<T> GetRecords()
        {
            return _dbSet.AsQueryable<T>();
        }

        protected IQueryable<T> GetRecordsWhere(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }
    }
}
