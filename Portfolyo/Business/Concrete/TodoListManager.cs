using Business.Abstract;
using Business.Contants;
using Core.Entities.Abstract;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class TodoListManager : ITodoListService
    {
        private ITodoDal _todoListDal;

        public TodoListManager(ITodoDal todoListDal)
        {
            _todoListDal = todoListDal;
        }

        public IDataResult<List<TodoList>> TGetList(Expression<Func<TodoList, bool>> filter = null)
        {
            var result = _todoListDal.GetDataList(filter);

            return new SuccessDataResult<List<TodoList>>(result, Messages.QuerySuccess);
        }

        public IDataResult<TodoList> GetById(int id)
        {
            var result = _todoListDal.GetData(t => t.TodoId == id);

            return new SuccessDataResult<TodoList>(result, Messages.QuerySuccess);
        }
        public IDataResult<List<TodoList>> GetLast5Todo()
        {
            var result = _todoListDal.GetDataList(filter: t => t.Status == false, orderBy: t => t.TodoId);

            return new SuccessDataResult<List<TodoList>>(result, Messages.QuerySuccess);
        }

        public IResult TAdd(TodoList entity)
        {
            _todoListDal.AddData(entity);

            return new SuccessResult(Messages.AddedData);
        }

        public IResult TDelete(int id)
        {
            _todoListDal.DeleteData(id);

            return new SuccessResult(Messages.DeletedData);
        }

        public IResult TUpdate(TodoList entity)
        {
            _todoListDal.UpdateData(entity);

            return new SuccessResult(Messages.UpdatedData);
        }
    }
}
