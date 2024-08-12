using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portfolyo.ValidationRules.FluentValidation;
using System.Globalization;

namespace Portfolyo.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        //private IAuthService _authService;
        //public AuthController(IAuthService authService)
        //{
        //    _authService = authService;
        //}

        private readonly UserManager<WriterUser> _userManager;
        private readonly SignInManager<WriterUser> _signInManager;

        public AuthController(UserManager<WriterUser> userManager, SignInManager<WriterUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            UserForLoginDtoValidator validator = new UserForLoginDtoValidator();
            ValidationResult validateResult = validator.Validate(userForLoginDto);

            if (validateResult.IsValid)
            {
                WriterUser wUser = new WriterUser
                {
                    Email = userForLoginDto.Email
                };

                var result = await _signInManager.PasswordSignInAsync(userForLoginDto.Email, userForLoginDto.Password, true, true);

                var user = await _userManager.GetUserAsync(User);
                var roleResult = await _userManager.GetRolesAsync(user);

                if (result.Succeeded)
                {
                    if (roleResult.Count() > 0)
                    {
                        foreach(var role in roleResult)
                        {
                            if (role == "Admin")
                            {
                                return RedirectToAction("Index", "Dashboard");
                            }
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index", "ErrorPage");
                    }

                    return RedirectToAction("Index", "WriterDashboard", new { area = "User" });
                }
                else
                {
                    ModelState.AddModelError("", "Hatalı Kullanıcı Adı veya Şifre");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        // Kendi Class'larımız ile Yaptığımız Login İşlemi
        //[HttpPost]
        //public IActionResult Login(UserForLoginDto userForLoginDto)
        //{
        //    var userToCheck = _authService.Login(userForLoginDto);

        //    if (!userToCheck.Success)
        //    {
        //        return View();
        //    }

        //    var token = _authService.CreateAccessToken(userToCheck.Data);

        //    return View();
        //}

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // Kendi Class'larımız ile Yaptığımız Register İşlemi
        //[HttpPost]
        //public IActionResult Register(UserForRegisterDto userForRegisterDto)
        //{
        //    UserForRegisterDtoValidator validator = new UserForRegisterDtoValidator();
        //    ValidationResult validationResult = validator.Validate(userForRegisterDto);

        //    if (validationResult.IsValid)
        //    {
        //        var userToCheck = _authService.Register(userForRegisterDto);

        //        if (!userToCheck.Success)
        //        {
        //            return View();
        //        }

        //        return RedirectToAction("Index", "Dashboard");
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}

        // Microsoft.AspNetCore.Identity Kullanılarak Yapılan Register İşlemi
        [HttpPost]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            UserForRegisterDtoValidator validator = new UserForRegisterDtoValidator();
            ValidationResult validationResult = validator.Validate(userForRegisterDto);

            if (validationResult.IsValid)
            {
                WriterUser wUser = new WriterUser
                {
                    Name = userForRegisterDto.Name,
                    Surname = userForRegisterDto.Surname,
                    UserName = userForRegisterDto.Username,
                    Email = userForRegisterDto.Email,
                    ImageUrl = userForRegisterDto.ImageUrl

                };

                var result = await _userManager.CreateAsync(wUser, userForRegisterDto.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("login", "Auth");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }

                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Auth");
        }
    }
}
