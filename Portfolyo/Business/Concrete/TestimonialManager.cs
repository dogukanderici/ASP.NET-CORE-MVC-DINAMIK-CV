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
    public class TestimonialManager : ITestimonialService
    {
        private ITestimonialDal _testimonialDal;
        public TestimonialManager(ITestimonialDal testimonialDal)
        {
            _testimonialDal = testimonialDal;
        }

        public IDataResult<List<Testimonial>> TGetList(Expression<Func<Testimonial, bool>> filter = null)
        {
            var result = _testimonialDal.GetDataList();

            return new SuccessDataResult<List<Testimonial>>(result, Messages.QuerySuccess);
        }

        public IDataResult<Testimonial> GetById(int id)
        {
            var result = _testimonialDal.GetData(t => t.TestimonialId == id);

            return new SuccessDataResult<Testimonial>(result, Messages.QuerySuccess);
        }

        public IResult TAdd(Testimonial entity)
        {
            _testimonialDal.AddData(entity);

            return new SuccessResult(Messages.AddedData);
        }

        public IResult TDelete(int id)
        {
            _testimonialDal.DeleteData(id);

            return new SuccessResult(Messages.DeletedData);
        }

        public IResult TUpdate(Testimonial entity)
        {
            if (entity.TestimonialId < 1)
            {
                _testimonialDal.AddData(entity);
            }
            else
            {
                _testimonialDal.UpdateData(entity);
            }

            return new SuccessResult(Messages.UpdatedData);
        }
    }
}
