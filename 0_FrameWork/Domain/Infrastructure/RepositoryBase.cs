using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _0_FrameWork.Domain.Infrastructure
{
    public class RepositoryBase<TKey, T> : IRepository <TKey, T> where T : class
    {
        private readonly DbContext _dbContext;
        public RepositoryBase(DbContext dbContext){ _dbContext = dbContext; }
       
        public void Create(T entity) => _dbContext.Add(entity);
        public void Update(T entity) => _dbContext.Update(entity);
        public void SaveChanges() => _dbContext.SaveChanges();
        public List<T> GetAll()=> _dbContext.Set<T>().ToList();
        public T GetById(TKey id)=> _dbContext.Find<T>(id);
        public bool IsExist(System.Linq.Expressions.Expression<Func<T, bool>> expression) => _dbContext.Set<T>().Any(expression);




    }

}
