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
    public class SocialMediaManager : ISocialMediaService
    {
        private ISocialMediaDal _socialMediaDal;

        public SocialMediaManager(ISocialMediaDal socialMediaDal)
        {
            _socialMediaDal = socialMediaDal;
        }

        public IDataResult<List<SocialMedia>> TGetList(Expression<Func<SocialMedia, bool>> filter = null)
        {
            var result = _socialMediaDal.GetDataList();

            return new SuccessDataResult<List<SocialMedia>>(result, Messages.QuerySuccess);
        }
        public IDataResult<SocialMedia> GetById(int id)
        {
            var result = _socialMediaDal.GetData(sm => sm.SocialMediaId == id);

            return new SuccessDataResult<SocialMedia>(result, Messages.QuerySuccess);
        }

        public IResult TAdd(SocialMedia entity)
        {
            _socialMediaDal.AddData(entity);

            return new SuccessResult(Messages.AddedData);
        }

        public IResult TDelete(int id)
        {
            _socialMediaDal.DeleteData(id);

            return new SuccessResult(Messages.DeletedData);
        }

        public IResult TUpdate(SocialMedia entity)
        {
            _socialMediaDal.UpdateData(entity);

            return new SuccessResult(Messages.UpdatedData);
        }
    }
}