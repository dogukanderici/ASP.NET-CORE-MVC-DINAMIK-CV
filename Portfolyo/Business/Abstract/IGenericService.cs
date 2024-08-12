using Core.Entities.Abstract;
using Core.Utilities.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IGenericService<T>
    {
        IResult TAdd(T entity);
        IResult TUpdate(T entity);
        IResult TDelete(int id);
        IDataResult<List<T>> TGetList(Expression<Func<T, bool>> filter = null);
        IDataResult<T> GetById(int id);
    }
}
