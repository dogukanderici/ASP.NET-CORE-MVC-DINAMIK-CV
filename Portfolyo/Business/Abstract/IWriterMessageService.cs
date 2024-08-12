using Core.Utilities.Result;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IWriterMessageService : IGenericService<WriterMessage>
    {
        IDataResult<List<WriterMessage>> GetLast5UnreadMessage(Expression<Func<WriterMessage, bool>> filter = null);
    }
}
