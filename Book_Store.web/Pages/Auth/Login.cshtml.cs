using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Book_Store.CoreLayer.DTOs.Users;
using Book_Store.CoreLayer.Services.Users;
using Book_Store.CoreLayer.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Book_Store.web.Pages.Auth
{
    [ValidateAntiForgeryToken]
    [BindProperties]
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;

        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }

        [Required(ErrorMessage ="ایمیل را وارد کنید")]
        public string Email { get; set; }

        [Required(ErrorMessage = "کلمه عبور را وارد کنید")]
        public string Password { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost(int role)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var user = _userService.LoginUser(new LoginUserDto()
            {
                Password=Password,
                Email=Email
            });

            if(user==null)
            {
                ModelState.AddModelError("Email", "کاربری با مشخصات وارد شده یافت نشد");
                return Page();
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                new Claim(ClaimTypes.Name,user.FullName),
                new Claim(ClaimTypes.Role,user.Role.ToString())
            };

            var identity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
            var claimPrincipal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties()
            {
                IsPersistent=true,
                ExpiresUtc=DateTime.Now.AddDays(5)
            };
            HttpContext.SignInAsync(claimPrincipal,properties);
            return RedirectToPage("../Index");
        }
    }
}
