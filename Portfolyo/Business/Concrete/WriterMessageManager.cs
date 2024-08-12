using Business.Abstract;
using Business.Contants;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class WriterMessageManager : IWriterMessageService
    {
        private IWriterMessageDal _writerMessageDal;
        private readonly UserManager<WriterUser> _userManager;

        public WriterMessageManager(IWriterMessageDal writerMessageDal, UserManager<WriterUser> userManager)
        {
            _writerMessageDal = writerMessageDal;
            _userManager = userManager;
        }

        public IDataResult<List<WriterMessage>> TGetList(Expression<Func<WriterMessage, bool>> filter = null)
        {
            var result = _writerMessageDal.GetDataList(filter);

            return new SuccessDataResult<List<WriterMessage>>(result, Messages.QuerySuccess);
        }

        public IDataResult<WriterMessage> GetById(int id)
        {
            var result = _writerMessageDal.GetData(wm => wm.WiterMessageID == id);

            return new SuccessDataResult<WriterMessage>(result, Messages.QuerySuccess);
        }
        public IDataResult<List<WriterMessage>> GetLast5UnreadMessage(Expression<Func<WriterMessage, bool>> filter = null)
        {
            var result = _writerMessageDal.GetDataList(filter);

            return new SuccessDataResult<List<WriterMessage>>(result, Messages.QuerySuccess);
        }

        public IResult TAdd(WriterMessage entity)
        {
            _writerMessageDal.AddData(entity);

            return new SuccessResult(Messages.AddedData);
        }

        public IResult TDelete(int id)
        {
            throw new NotImplementedException();
        }

        public IResult TUpdate(WriterMessage entity)
        {
            _writerMessageDal.UpdateData(entity);

            return new SuccessResult(Messages.UpdatedData);
        }

    }
}
