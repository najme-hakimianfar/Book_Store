using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Book_Store.CoreLayer.DTOs.Users;
using Book_Store.CoreLayer.Services.Users;
using Book_Store.CoreLayer.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Book_Store.web.Pages.Auth
{
    [BindProperties]
    [ValidateAntiForgeryToken]
    public class RegisterModel : PageModel
    {
        private readonly IUserService _userService;

        #region properties


        [Required(ErrorMessage = "ایمیل را وارد کنید")]
        public string Email { get; set; }


        [Required(ErrorMessage = "نام و نام خانوادگی را وارد کنید")]
        public string FullName { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "{0}را وارد کنید")]
        [MinLength(6, ErrorMessage = "{0}باید بیشتر از 5 کاراکتر باشد")]
        public string Password { get; set; }

        public string Address { get; set; }

        public string Tel { get; set; }
        #endregion

        public RegisterModel(IUserService userService)
        {
            _userService = userService;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var result = _userService.RegisterUser(new UserRegisterDto()
            {
                Email=Email,
                FullName=FullName,
                Password=Password,
                Address=Address,
                Tel=Tel
            });
            if(result.Status==OperationResultStatus.Error)
            {
                ModelState.AddModelError("Email", result.Message);
                return Page();
            }
            return RedirectToPage("Login");
        }
    }
}
