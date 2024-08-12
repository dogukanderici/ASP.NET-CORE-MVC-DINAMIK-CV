using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IWriterRoleService : IGenericService<WriterRole>
    {
        IEnumerable<string> TGetList2(Expression<Func<WriterRole, bool>> filter = null);
    }
}
