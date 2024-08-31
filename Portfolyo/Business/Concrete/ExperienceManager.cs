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
    public class ExperienceManager : IExperienceService
    {
        private IExperienceDal _experienceDal;
        public ExperienceManager(IExperienceDal experienceDal)
        {
            _experienceDal = experienceDal;
        }

        public IDataResult<List<Experience>> TGetList(Expression<Func<Experience, bool>> filter = null)
        {
            var result = _experienceDal.GetDataList();

            return new SuccessDataResult<List<Experience>>(result, Messages.QuerySuccess);
        }

        public IDataResult<Experience> GetById(int id)
        {
            var result = _experienceDal.GetData(e => e.ExperienceId == id);

            return new SuccessDataResult<Experience>(result, Messages.QuerySuccess);
        }

        public IResult TAdd(Experience entity)
        {
            _experienceDal.AddData(entity);

            return new SuccessResult(Messages.AddedData);
        }

        public IResult TDelete(int id)
        {
            _experienceDal.DeleteData(id);

            return new SuccessResult(Messages.DeletedData);
        }

        public IResult TUpdate(Experience entity)
        {
            if (entity.ExperienceId < 1)
            {
                _experienceDal.AddData(entity);
            }
            else
            {
                _experienceDal.UpdateData(entity);
            }

            return new SuccessResult(Messages.UpdatedData);
        }
    }
}
