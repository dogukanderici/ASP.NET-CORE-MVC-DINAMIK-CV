using Business.Abstract;
using Business.Contants;
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
    public class WriterRoleManager : IWriterRoleService
    {
        private IWriterRoleDal _writerRoleDal;

        public WriterRoleManager(IWriterRoleDal writerRoleDal)
        {
            _writerRoleDal = writerRoleDal;
        }

        public IEnumerable<string> TGetList2(Expression<Func<WriterRole, bool>> filter = null)
        {
            var result = _writerRoleDal.GetDataList(filter).Select(wr => wr.Name);

            return result;
        }

        public IDataResult<List<WriterRole>> TGetList(Expression<Func<WriterRole, bool>> filter = null)
        {
            var result = _writerRoleDal.GetDataList(filter);

            return new SuccessDataResult<List<WriterRole>>(result, Messages.QuerySuccess);
        }

        public IDataResult<WriterRole> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IResult TAdd(WriterRole entity)
        {
            throw new NotImplementedException();
        }

        public IResult TDelete(int id)
        {
            throw new NotImplementedException();
        }

        public IResult TUpdate(WriterRole entity)
        {
            throw new NotImplementedException();
        }
    }
}
