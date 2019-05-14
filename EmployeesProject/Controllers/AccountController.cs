using Dapper;
using EmployeesProject.HashData;
using EmployeesProject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmployeesProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=viktor;Trusted_Connection=True;MultipleActiveResultSets=true";
        private readonly ILogger<AccountController> _logger;

        public AccountController(/*string connectionString,*/ ILogger<AccountController> logger)
        {
            //_connectionString = connectionString;
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
                    User userWithHashedPassword = null;

                    using (IDbConnection db = new SqlConnection(_connectionString))
                    {
                        userWithHashedPassword = await db.QueryFirstOrDefaultAsync<User>("SELECT * FROM Users WHERE Email = @Email", new { model.Email });
                    }

                    if (userWithHashedPassword == null)
                    {
                        ModelState.AddModelError("", "Incorrect login or password");
                        return View(model);
                    }

                    if (SecurePasswordHasherHelper.Verify(model.Password, userWithHashedPassword.Password))
                    {
                        User user = null;

                        using (IDbConnection db = new SqlConnection(_connectionString))
                        {
                            user = await db.QueryFirstOrDefaultAsync<User>("SELECT * FROM Users WHERE Email = @Email AND Password = '"+ userWithHashedPassword.Password + "'", new { model.Email });
                        }

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
                _logger.LogInformation(ex, "Login failed");
                throw;
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
                    User user = null;

                    using (IDbConnection db = new SqlConnection(_connectionString))
                    {
                        user = await db.QueryFirstOrDefaultAsync<User>("SELECT * FROM Users WHERE Email = @Email", new { model.Email });
                    }

                    if (user == null)
                    {
                        string hashed_password = SecurePasswordHasherHelper.Hash(model.Password);

                        using (IDbConnection db = new SqlConnection(_connectionString))
                        {
                            var sqlQuery = "INSERT INTO Users (Email, Password) VALUES(@Email, @Password)";
                            await db.ExecuteAsync(sqlQuery, model);
                        }

                        await Authenticate(model.Email);

                        return RedirectToAction("Index", "Home");
                    }
                }
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Registration failed");
                throw;
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
                _logger.LogInformation(ex, "Authentication failed");
                throw;
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
                _logger.LogInformation(ex, "Logout failed");
                throw;
            }
        }
    }
}