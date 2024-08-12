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
    public class WriterUserManager : IWriterUserService
    {
        private IWriterUserDal _writerUserDal;

        public WriterUserManager(IWriterUserDal writerUserDal)
        {
            _writerUserDal = writerUserDal;
        }
        public IDataResult<List<WriterUser>> TGetList(Expression<Func<WriterUser, bool>> filter = null)
        {
            var result = _writerUserDal.GetDataList(filter);

            return new SuccessDataResult<List<WriterUser>>(result, Messages.QuerySuccess);
        }

        public IDataResult<WriterUser> GetById(int id)
        {
            var result = _writerUserDal.GetData(wu => wu.Id == id);

            return new SuccessDataResult<WriterUser>(result, Messages.QuerySuccess);
        }

        public IResult TAdd(WriterUser entity)
        {
            _writerUserDal.AddData(entity);

            return new SuccessResult(Messages.AddedData);
        }

        public IResult TDelete(int id)
        {
            _writerUserDal.DeleteData(id);

            return new SuccessResult(Messages.DeletedData);
        }

        public IResult TUpdate(WriterUser entity)
        {
            _writerUserDal.UpdateData(entity);

            return new SuccessResult(Messages.UpdatedData);
        }
    }
}
