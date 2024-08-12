using Business.Abstract;
using Business.Contants;
using Core.Entities.Abstract;
using Core.Entities.Concrete;
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
    public class MessageManager : IMessageService
    {
        private IMessageDal _messageDal;
        public MessageManager(IMessageDal messageDal)
        {
            _messageDal = messageDal;
        }

        public IDataResult<List<WriterMessage>> TGetList(Expression<Func<WriterMessage, bool>> filter = null)
        {
            var result = _messageDal.GetDataList(filter);

            return new SuccessDataResult<List<WriterMessage>>(result, Messages.QuerySuccess);
        }

        public IDataResult<WriterMessage> GetById(int id)
        {
            var result = _messageDal.GetData(m => m.WiterMessageID == id);

            return new SuccessDataResult<WriterMessage>(result, Messages.QuerySuccess);
        }

        public IResult TAdd(WriterMessage entity)
        {
            _messageDal.AddData(entity);

            return new SuccessResult(Messages.AddedData);
        }

        public IResult TDelete(int id)
        {
            _messageDal.DeleteData(id);

            return new SuccessResult(Messages.DeletedData);
        }

        public IResult TUpdate(WriterMessage entity)
        {
            _messageDal.UpdateData(entity);

            return new SuccessResult(Messages.UpdatedData);
        }
    }
}
