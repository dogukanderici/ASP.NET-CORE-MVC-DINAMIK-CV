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
    public class ContactManager : IContactService
    {
        private IContactDal _contactDal;
        public ContactManager(IContactDal contactDal)
        {
            _contactDal = contactDal;
        }
        public IDataResult<List<Contact>> TGetList(Expression<Func<Contact, bool>> filter = null)
        {
            var result = _contactDal.GetDataList();

            return new SuccessDataResult<List<Contact>>(result, Messages.QuerySuccess);
        }

        public IDataResult<Contact> GetById(int id)
        {
            var result = _contactDal.GetData(c => c.ContactId == id);

            return new SuccessDataResult<Contact>(result, Messages.QuerySuccess);
        }

        public IResult TAdd(Contact entity)
        {
            _contactDal.AddData(entity);

            return new SuccessResult(Messages.AddedData);
        }

        public IResult TDelete(int id)
        {
            _contactDal.DeleteData(id);

            return new SuccessResult(Messages.DeletedData);
        }

        public IResult TUpdate(Contact entity)
        {
            if (entity.ContactId < 1)
            {
                _contactDal.AddData(entity);
            }
            else
            {
                _contactDal.UpdateData(entity);
            }

            return new SuccessResult(Messages.UpdatedData);
        }
    }
}
