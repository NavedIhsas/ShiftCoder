using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _0_FrameWork.Domain
{
    public interface IRepository<TKey,T> where T:class
    {
        T GetById(TKey id);
        List<T> GetAll();
        void Create(T entity);
        void Update(T entity);
        bool IsExist(Expression<Func<T, bool>> expression);
        void SaveChanges();
    }
}
