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
    public class AboutManager : IAboutService
    {
        private IAboutDal _aboutDal;
        public AboutManager(IAboutDal aboutDal)
        {
            _aboutDal = aboutDal;
        }
        public IDataResult<List<About>> TGetList(Expression<Func<About, bool>> filter = null)
        {
            var result = _aboutDal.GetDataList();

            return new SuccessDataResult<List<About>>(result, Messages.QuerySuccess);
        }

        public IDataResult<About> GetById(int id)
        {
            var result = _aboutDal.GetData(a => a.AboutId == id);

            return new SuccessDataResult<About>(result, Messages.QuerySuccess);
        }

        public IResult TAdd(About entity)
        {
            _aboutDal.AddData(entity);

            return new SuccessResult(Messages.AddedData);
        }

        public IResult TDelete(int id)
        {
            _aboutDal.DeleteData(id);

            return new SuccessResult(Messages.DeletedData);
        }

        public IResult TUpdate(About entity)
        {
            if (entity.AboutId < 1)
            {
                _aboutDal.AddData(entity);
            }
            else
            {
                _aboutDal.UpdateData(entity);
            }

            return new SuccessResult(Messages.UpdatedData);
        }
    }
}
