using Business.Abstract;
using Business.Contants;
using Core.Utilities.Result;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AnnouncementManager : IAnnouncementService
    {
        private IAnnouncementDal _announcementDal;

        public AnnouncementManager(IAnnouncementDal announcementDal)
        {
            _announcementDal = announcementDal;
        }

        public IDataResult<Announcement> GetById(int id)
        {
            var result = _announcementDal.GetData(a => a.AnnouncementId == id);

            return new SuccessDataResult<Announcement>(result, Messages.QuerySuccess);
        }
        public IDataResult<List<Announcement>> GetLastFiveDataList()
        {
            var result = _announcementDal.GetDataList(filter: null, orderBy:a=>a.AnnouncementId);

            return new SuccessDataResult<List<Announcement>>(result, Messages.QuerySuccess);
        }

        public IResult TAdd(Announcement entity)
        {
            throw new NotImplementedException();
        }

        public IResult TDelete(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<Announcement>> TGetList(Expression<Func<Announcement, bool>> filter = null)
        {
            var result = _announcementDal.GetDataList();

            return new SuccessDataResult<List<Announcement>>(result, Messages.QuerySuccess);
        }

        public IResult TUpdate(Announcement entity)
        {
            _announcementDal.UpdateData(entity);

            return new SuccessResult(Messages.UpdatedData);
        }
    }
}
