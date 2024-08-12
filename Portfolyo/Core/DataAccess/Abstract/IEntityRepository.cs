using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.Abstract
{
    public interface IEntityRepository<T>
        where T : class, new()
    {
        List<T> GetDataList(Expression<Func<T, bool>> filter = null, Expression<Func<T, decimal>> orderBy = null);
        T GetData(Expression<Func<T, bool>> filter);

        void AddData(T entity);
        void UpdateData(T entity);
        void DeleteData(int id);
    }
}