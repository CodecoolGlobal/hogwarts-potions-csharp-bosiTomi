using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.DataAccess;
using HogwartsPotions.Helpers;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using HogwartsPotions.Service.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HogwartsPotions.Controllers
{
    public class StudentController : Controller
    {
        private readonly UserManager<Student> _userManager;
        private readonly SignInManager<Student> _signInManager;
        public StudentController(UserManager<Student> userManager, SignInManager<Student> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            ViewBag.HouseTypes = new HouseType[] { HouseType.Gryffindor, HouseType.Hufflepuff, HouseType.Ravenclaw, HouseType.Slytherin };
            ViewBag.PetTypes = new PetType[] { PetType.Rat, PetType.Cat, PetType.None, PetType.Owl };
            return View();
        }
        public async Task<IActionResult> ValidateLogin(string username, string password)
        {
            var message = "Please enter the correct credentials!";

            if (username == null || password == null)
            {
                HttpContext.Session.SetString("message", message);
                return RedirectToAction("Index");
            }

            LoginForm loginForm = new LoginForm{ Username = username, Password = password };

            await HttpContext.SignOutAsync("Identity.Application");
            var result = await _signInManager.PasswordSignInAsync(loginForm.Username, loginForm.Password, isPersistent: true, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                SessionHelper.SetObjectAsJson(HttpContext.Session, "username", loginForm.Username);
                return RedirectToAction("Index", "Home");
            }

            HttpContext.Session.SetString("message", message);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Register(RegisterForm registerForm)
        {
            Student user = new Student()
            {
                UserName = registerForm.Username,
                HouseType = registerForm.HouseType,
                PetType = registerForm.PetType
            };
            var result = await _userManager.CreateAsync(user, registerForm.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Student");
            }
            var message = "";
            foreach (var error in result.Errors)
            {
                message += error.Description;
            }
            HttpContext.Session.SetString("message", message);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("username");
            await HttpContext.SignOutAsync("Identity.Application");
            return RedirectToAction("Index", "Student");
        }
    }
}
