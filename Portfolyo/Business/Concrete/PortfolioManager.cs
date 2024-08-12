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
    public class PortfolioManager : IPortfolioService
    {

        private IPortfolioDal _portfolioDal;
        public PortfolioManager(IPortfolioDal portfolioDal)
        {
            _portfolioDal = portfolioDal;
        }

        public IDataResult<List<Portfolio>> TGetList(Expression<Func<Portfolio, bool>> filter = null)
        {
            var result = _portfolioDal.GetDataList();

            return new SuccessDataResult<List<Portfolio>>(result, Messages.QuerySuccess);
        }

        public IDataResult<List<Portfolio>> GetLastFiveDataList()
        {
            var result = _portfolioDal.GetDataList(filter: null, orderBy: p => p.PortfolioId);

            return new SuccessDataResult<List<Portfolio>>(result, Messages.QuerySuccess);
        }

        public IDataResult<Portfolio> GetById(int id)
        {
            var result = _portfolioDal.GetData(p => p.PortfolioId == id);

            return new SuccessDataResult<Portfolio>(result, Messages.QuerySuccess);
        }

        public IResult TAdd(Portfolio entity)
        {
            _portfolioDal.AddData(entity);

            return new SuccessResult(Messages.AddedData);
        }

        public IResult TDelete(int id)
        {
            _portfolioDal.DeleteData(id);

            return new SuccessResult(Messages.DeletedData);
        }

        public IResult TUpdate(Portfolio entity)
        {
            _portfolioDal.UpdateData(entity);

            return new SuccessResult(Messages.UpdatedData);
        }
    }
}
