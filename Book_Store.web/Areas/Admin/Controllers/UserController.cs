using Book_Store.CoreLayer.DTOs.Books;
using Book_Store.CoreLayer.DTOs.Users;
using Book_Store.CoreLayer.Services.Users;
using Book_Store.CoreLayer.Utilities;
using Book_Store.web.Areas.Admin.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index(int pageId = 1,string name=null)
        {
       
            var model = _userService.GetUsersByFilter(pageId,5,name);
            return View(model);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(CreateUserViewModel createUserViewModel)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            var hashPass = Encoder.EncodeToMd5(createUserViewModel.Password);
            var result = _userService.CreateUser(new RegisterDto()
            {
                FullName=createUserViewModel.FullName,
                Email=createUserViewModel.Email,
                Role=createUserViewModel.Role,
                Address=createUserViewModel.Address,
                Password=hashPass,
                Tel=createUserViewModel.Tel
                
            });

            if (result.Status == OperationResultStatus.Error)
            {
                ModelState.AddModelError("Email", result.Message);
                return View();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
                return RedirectToAction("Index");
            var hashPass = Encoder.EncodeToMd5(user.Password);

            var model = new EditUserViewModel()
            {

                FullName = user.FullName,
                Email = user.Email,
                Password = hashPass,
                Address = user.Address,
                Role = user.Role,
                Tel = user.Tel
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,EditUserViewModel editUserViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var hashPass = Encoder.EncodeToMd5(editUserViewModel.Password);

            var result = _userService.EditUser(new EditUserDto()
            {
                FullName = editUserViewModel.FullName,
                Email = editUserViewModel.Email,
                Role = editUserViewModel.Role,
                Address = editUserViewModel.Address,
                Password = hashPass,
                Tel = editUserViewModel.Tel,
                UserId=id
                
            });

            if (result.Status == OperationResultStatus.Error)
            {
                ModelState.AddModelError("Email", result.Message);
                return View();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int userId)
        {
            _userService.DeleteUser(userId);
            return RedirectToAction("index");
        }
    }
}
