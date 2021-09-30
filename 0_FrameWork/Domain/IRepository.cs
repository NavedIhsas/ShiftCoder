using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace _0_FrameWork.Domain
{
    public interface IRepository<TKey,T> where T:class
    {
        T GetById(TKey id);
        void Create(T entity);
        void Update(T entity);
        bool IsExist(Expression<Func<T, bool>> expression);
        void SaveChanges();
    }
}
