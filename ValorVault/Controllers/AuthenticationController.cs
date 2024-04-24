﻿using Microsoft.AspNetCore.Mvc;
using ValorVault.Models;
using ValorVault.Services.UserService;
using ValorVault.UserDtos;

namespace ValorVault.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Authenticate(User userBase)
        {
            try
            {
                var success = _userService.SignInUser(new LoginUserDto { Email = userBase.email, Password = userBase.user_password }).Result;

                if (!success)
                {
                    ViewBag.ErrorMessage = "Email або пароль невірні";
                    return View("Login");
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Login");
            }
        }
    }
}