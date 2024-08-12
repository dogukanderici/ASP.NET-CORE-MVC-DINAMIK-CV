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
    public class SkillManager : ISkillService
    {
        private ISkillDal _skillDal;
        public SkillManager(ISkillDal skillDal)
        {
            _skillDal = skillDal;
        }

        public IDataResult<List<Skill>> TGetList(Expression<Func<Skill, bool>> filter = null)
        {
            var result = _skillDal.GetDataList();

            return new SuccessDataResult<List<Skill>>(result, Messages.QuerySuccess);
        }

        public IDataResult<Skill> GetById(int id)
        {
            var result = _skillDal.GetData(s => s.SkillId == id);

            return new SuccessDataResult<Skill>(result, Messages.QuerySuccess);
        }

        public IResult TAdd(Skill entity)
        {
            _skillDal.AddData(entity);

            return new SuccessResult(Messages.AddedData);
        }

        public IResult TDelete(int id)
        {
            _skillDal.DeleteData(id);

            return new SuccessResult(Messages.DeletedData);
        }

        public IResult TUpdate(Skill entity)
        {
            _skillDal.UpdateData(entity);

            return new SuccessResult(Messages.UpdatedData);
        }
    }
}
