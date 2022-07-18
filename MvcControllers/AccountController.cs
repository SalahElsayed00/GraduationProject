using GraduationProject.Data;
using GraduationProject.Models;
using GraduationProject.Utilities.StaticStrings;
using GraduationProject.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GraduationProject.MvcControllers
{
    [Authorize(Roles = Roles.Admin, AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class AccountController : Controller
    {
		private readonly ApplicationDbContext _context;

		public AccountController(ApplicationDbContext context)
		{
			_context = context;
		}

        [HttpGet]
        [AllowAnonymous]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInVM model)
        {
            if (!ModelState.IsValid)
                return View();

			//var admin = new Admin
			//{
			//	Name = model.Email,
			//	Email = model.Email,
			//	GenderId = Enums.GenderType.Male,
			//	PhoneNumber = "12345678901",
			//	NationalId = "12345678909876",
			//	ProfileImage = new byte[] { 0xFF },
			//	NationalIdImage = new byte[] { 0xFF }
			//};
			//PasswordHasher<Admin> hasher = new PasswordHasher<Admin>();
			//admin.PasswordHash = hasher.HashPassword(admin, model.Password);

			//await _context.Admins.AddAsync(admin);
			//await _context.SaveChangesAsync();
			//return RedirectToAction(nameof(HomeController.Index), "Home");

			var admin = _context.Admins
				.Select(a => new Admin
				{
					Id = a.Id,
					Name = a.Name,
					Email = a.Email,
					PasswordHash = a.PasswordHash
				})
				.FirstOrDefault(a => a.Email == model.Email);

			if (admin is null)
			{
				ModelState.AddModelError("", "Email not found");
				return View(model);
			}

			PasswordHasher<Admin> hasher = new PasswordHasher<Admin>();
			var result = hasher.VerifyHashedPassword(admin, admin.PasswordHash, model.Password);
			if(result != PasswordVerificationResult.Success)
			{
				ModelState.AddModelError("", "Password is wrong");
				return View(model);
			}

			var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString()),
                new Claim(ClaimTypes.Name, admin.Name),
                new Claim(ClaimTypes.Role, Roles.Admin),
            };

            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
            var properties = new AuthenticationProperties
            {
                IsPersistent = model.RememberMe,
                ExpiresUtc = DateTimeOffset.Now.AddDays(14)
            };

            await HttpContext.SignInAsync(principal, properties);
            if (string.IsNullOrEmpty(model.ReturnUrl))
                return RedirectToAction(nameof(HomeController.Index), "Home");

            return LocalRedirect(model.ReturnUrl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }
    }
}
