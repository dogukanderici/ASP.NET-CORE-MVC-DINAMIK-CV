using Business.Abstract;
using Business.Contants;
using Core.Entities.Concrete;
using Core.Utilities.Result;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IResult AddNewUser(User user)
        {
            _userDal.AddData(user);

            return new SuccessResult(Messages.AddedData);
        }

        public IDataResult<List<User>> GetAllUsers()
        {
            var result = _userDal.GetDataList();

            return new SuccessDataResult<List<User>>(result, Messages.QuerySuccess);
        }

        public IDataResult<User> GetUser(string mail)
        {
            var result = _userDal.GetData(u => u.Email == mail);

            return new SuccessDataResult<User>(result, Messages.QuerySuccess);
        }
    }
}
