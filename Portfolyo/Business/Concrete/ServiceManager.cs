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
    public class ServiceManager : IServiceService
    {
        private IServiceDal _serviceDal;
        public ServiceManager(IServiceDal serviceDal)
        {
            _serviceDal = serviceDal;
        }

        public IDataResult<List<Service>> TGetList(Expression<Func<Service, bool>> filter = null)
        {
            var result = _serviceDal.GetDataList();

            return new SuccessDataResult<List<Service>>(result, Messages.QuerySuccess);
        }

        public IDataResult<Service> GetById(int id)
        {
            var result = _serviceDal.GetData(s => s.SercviceId == id);

            return new SuccessDataResult<Service>(result, Messages.QuerySuccess);
        }

        public IResult TAdd(Service entity)
        {
            _serviceDal.AddData(entity);

            return new SuccessResult(Messages.AddedData);
        }

        public IResult TDelete(int id)
        {
            _serviceDal.DeleteData(id);

            return new SuccessResult(Messages.DeletedData);
        }

        public IResult TUpdate(Service entity)
        {
            if (entity.SercviceId < 1)
            {
                _serviceDal.AddData(entity);
            }
            else
            {
                _serviceDal.UpdateData(entity);
            }

            return new SuccessResult(Messages.UpdatedData);
        }
    }
}
