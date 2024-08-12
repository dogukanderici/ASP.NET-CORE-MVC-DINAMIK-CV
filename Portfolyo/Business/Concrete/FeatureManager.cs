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
    public class FeatureManager : IFeatureService
    {
        private IFeatureDal _featureDal;
        public FeatureManager(IFeatureDal featureDal)
        {
            _featureDal = featureDal;
        }
        public IDataResult<List<Feature>> TGetList(Expression<Func<Feature, bool>> filter = null)
        {
            var result = _featureDal.GetDataList();

            return new SuccessDataResult<List<Feature>>(result, Messages.QuerySuccess);
        }
        public IDataResult<Feature> GetById(int id)
        {
            var result = _featureDal.GetData(f => f.FeatureId == id);
            return new SuccessDataResult<Feature>(result, Messages.QuerySuccess);
        }

        public IResult TAdd(Feature entity)
        {
            _featureDal.AddData(entity);

            return new SuccessResult(Messages.AddedData);
        }

        public IResult TDelete(int id)
        {
            _featureDal.DeleteData(id);

            return new SuccessResult(Messages.DeletedData);
        }

        public IResult TUpdate(Feature entity)
        {
            _featureDal.UpdateData(entity);

            return new SuccessResult(Messages.UpdatedData);
        }
    }
}
