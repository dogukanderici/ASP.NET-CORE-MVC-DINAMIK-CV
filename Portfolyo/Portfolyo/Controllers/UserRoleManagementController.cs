using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Portfolyo.Models;
using System.Data.Entity;

namespace Portfolyo.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserRoleManagementController : Controller
    {
        private readonly RoleManager<WriterRole> _roleManager;
        private readonly UserManager<WriterUser> _userManager;
        private IWriterRoleService _writerRoleService;

        public UserRoleManagementController(RoleManager<WriterRole> roleManager, UserManager<WriterUser> userManager, IWriterRoleService writerRoleService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _writerRoleService = writerRoleService;
        }

        public IActionResult Index()
        {
            ViewBag.v1 = "Rol Listesi";
            ViewBag.v2 = "Kullanıcı Rolleri";

            var roles = _roleManager.Roles;

            return View(roles.ToList());
        }

        [HttpGet]
        public IActionResult CreateUserRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserRole(UserRoleListViewModel userRoleListViewModel)
        {
            var model = new WriterRole
            {
                Name = userRoleListViewModel.RoleName,
                IsActive = userRoleListViewModel.IsActive
            };

            var result = await _roleManager.CreateAsync(model);

            if (!result.Succeeded)
            {
                return RedirectToAction("NotFound404", "ErrorPage");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateRole(string id)
        {

            var result = await _roleManager.FindByIdAsync(id);

            if (result == null)
            {
                return RedirectToAction("NotFound404", "ErrorPage");
            }

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(WriterRole writerRole)
        {
            var resultData = await _roleManager.FindByIdAsync(writerRole.Id.ToString());

            resultData.Name = writerRole.Name;
            resultData.IsActive = writerRole.IsActive;

            var result = await _roleManager.UpdateAsync(resultData);

            if (!result.Succeeded)
            {
                return RedirectToAction("NotFound404", "ErrorPage");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> FindUserRoleList(GetUserListViewModel getUserListViewModel)
        {
            ViewBag.ErrorStatus = false;

            if (getUserListViewModel == null)
            {
                ViewBag.ErrorStatus = true;
                ViewBag.ErrorMessage = "Bir Hata Oluştu!";
                return View();
            }

            if (getUserListViewModel.UserInfo == null)
            {
                return View();
            }

            if (getUserListViewModel.UserInfo.Email == null)
            {
                ViewBag.ErrorStatus = true;
                ViewBag.ErrorMessage = "Kullanıcı E-Posta Bilgisi Boş Olamaz!";
                return View();
            }
            else
            {
                var userResult = await _userManager.FindByEmailAsync(getUserListViewModel.UserInfo.Email);

                if (userResult != null)
                {
                    var userRoleList = await _userManager.GetRolesAsync(userResult);

                    if (userRoleList != null)
                    {
                        var roleResult = _writerRoleService.TGetList();

                        var model = new GetUserListViewModel
                        {
                            UserInfo = userResult,
                            UserRoleInfo = userRoleList,
                            Roles = roleResult.Data
                        };

                        return View(model);
                    }

                    ViewBag.ErrorStatus = true;
                    ViewBag.ErrorMessage = "Kullanıcı Rolleri Bulunamadı";
                    return View();
                }

                ViewBag.ErrorStatus = true;
                ViewBag.ErrorMessage = "Kullanıcı Bulunamadı";
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserRoleList(GetUserListViewModel getUserListViewModel)
        {
            var userResult = await _userManager.FindByEmailAsync(getUserListViewModel.UserInfo.Email);

            ViewBag.ErrorStatus = false;

            if (userResult == null)
            {
                ViewBag.ErrorStatus = true;
                ViewBag.ErrorMessage = "Kullanıcı Bulunamadı";
                return View("FindUserRoleList");
            }

            var userRole = await _userManager.GetRolesAsync(userResult);

            if (userRole == null)
            {
                ViewBag.ErrorStatus = true;
                ViewBag.ErrorMessage = "Kullanıcıya Ait Kayıtlı Rol Bulunamadı";
                return View("FindUserRoleList");
            }

            if (userRole.Contains(getUserListViewModel.UserRoleName))
            {
                ViewBag.ErrorStatus = true;
                ViewBag.ErrorMessage = "Kullanıcı Rolü Zaten Kayıtlı";
                return View("FindUserRoleList");
            }

            var addedRoleForUser = await _userManager.AddToRoleAsync(userResult, getUserListViewModel.UserRoleName);

            if (!addedRoleForUser.Succeeded)
            {
                ViewBag.ErrorStatus = true;
                ViewBag.ErrorMessage = "Bir Sorun İle Karşılaşıldı";
                return View("FindUserRoleList");
            }

            var lockoutResut = await _userManager.SetLockoutEndDateAsync(userResult, DateTime.Now.AddMinutes(1));

            return RedirectToAction("Index", "Dashboard");
        }

        public async Task<IActionResult> DeleteUserRoleList(string email, string role)
        {
            var userResult = await _userManager.FindByEmailAsync(email);

            var removedRoleForUser = await _userManager.RemoveFromRoleAsync(userResult, role);

            if (!removedRoleForUser.Succeeded)
            {
                return RedirectToAction("Index", "ErrorPage");
            }

            var lockoutResut = await _userManager.SetLockoutEndDateAsync(userResult, DateTime.Now.AddMinutes(1));

            return RedirectToAction("Index", "Dashboard");
        }
    }
}
