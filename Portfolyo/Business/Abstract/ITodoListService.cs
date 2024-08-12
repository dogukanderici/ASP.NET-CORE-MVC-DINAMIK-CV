using Core.Utilities.Result;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ITodoListService : IGenericService<TodoList>
    {
        IDataResult<List<TodoList>> GetLast5Todo();
    }
}
