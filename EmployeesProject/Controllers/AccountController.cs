using EmployeesProject.HashData;
using EmployeesProject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmployeesProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly ModelContext _db;
        private readonly ILogger<AccountController> _logger;

        public AccountController(ModelContext db, ILogger<AccountController> logger)
        {
            _db = db;
            _logger = logger;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User userWithHashedPassword = await _db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

                    if(userWithHashedPassword == null)
                    {
                        ModelState.AddModelError("", "Incorrect login or password");
                        return View(model);
                    }

                    if (SecurePasswordHasherHelper.Verify(model.Password, userWithHashedPassword.Password))
                    {
                        User user = await _db.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == userWithHashedPassword.Password);
                        if (user != null)
                        {
                            await Authenticate(model.Email);

                            return RedirectToAction("Index", "Home");
                        }                    
                    }                
                }
                return View(model);
            }
            catch(Exception ex)
            {
                _logger.LogInformation("Login failed " + ex.Message);
                throw new Exception("Login internal error");
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User user = await _db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                    if (user == null)
                    {
                        string hashed_password = SecurePasswordHasherHelper.Hash(model.Password);

                        _db.Users.Add(new User { Email = model.Email, Password = hashed_password });
                        await _db.SaveChangesAsync();

                        await Authenticate(model.Email);

                        return RedirectToAction("Index", "Home");
                    }
                }
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Registration failed " + ex.Message);
                throw new Exception("Registration internal error");
            }
        }

        private async Task Authenticate(string userName)
        {
            try
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
                ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Authentication failed " + ex.Message);
                throw new Exception("Authentication internal error");
            }
        }

        [HttpGet]
        [Route("api/Account/LogOut")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Logout failed " + ex.Message);
                throw new Exception("Logout internal error");
            }
        }
    }
}