using Business.Abstract;
using Business.Contants;
using Core.Entities.Concrete;
using Core.Utilities.Result;
using Core.Utilities.Security.Jwt;
using Core.Utilities.Security.Jwt.Hashing;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private ITokenHelper _tokenHelper;
        private IUserService _userService;
        private readonly UserManager<WriterUser> _userManager;

        public AuthManager(ITokenHelper tokenHelper, IUserService userService, UserManager<WriterUser> userManager)
        {
            _tokenHelper = tokenHelper;
            _userService = userService;
            _userManager = userManager;
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var accessToken = _tokenHelper.CreateAccessToken(user);

            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetUser(userForLoginDto.Email);

            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.Data.PasswordSalt, userToCheck.Data.PasswordHash))
            {
                return new ErrorDataResult<User>(Messages.InvalidPassword);
            }

            return new SuccessDataResult<User>(userToCheck.Data, Messages.LoginSuccess);
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto)
        {
            var userToCheck = _userService.GetUser(userForRegisterDto.Email);

            if (userToCheck.Data != null)
            {
                return new ErrorDataResult<User>(Messages.UserAlreadyExists);
            }

            byte[] passwordSalt, passwordHash;

            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordSalt, out passwordHash);

            var user = new User
            {
                Name = userForRegisterDto.Name,
                Surname = userForRegisterDto.Surname,
                UserName = (userForRegisterDto.Username == null ? (userForRegisterDto.Name + ' ' + userForRegisterDto.Surname) : userForRegisterDto.Username),
                Email = userForRegisterDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };

            _userService.AddNewUser(user);

            return new SuccessDataResult<User>(Messages.UserRegistered);
        }
    }
}
