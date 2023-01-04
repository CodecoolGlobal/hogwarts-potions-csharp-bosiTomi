using System;
using System.Collections.Generic;
using HogwartsPotions.Helpers;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HogwartsPotions.Controllers
{
    public class StudentController : Controller
    {
        private readonly HogwartsContext _context;
        public StudentController(HogwartsContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.HouseTypes = new HouseType[] { HouseType.Gryffindor, HouseType.Hufflepuff, HouseType.Ravenclaw, HouseType.Slytherin };
            ViewBag.PetTypes = new PetType[] { PetType.Rat, PetType.Cat, PetType.None, PetType.Owl };
            return View();
        }
        public IActionResult ValidateLogin(string username, string password)
        {
            var message = "Please enter the correct credentials!";

            if (username == null || password == null)
            {
                HttpContext.Session.SetString("message", message);
                return RedirectToAction("Index");
            }

            LoginForm loginForm = new LoginForm{ Username = username, Password = password };

            if (_context.ValidateLogin(loginForm))
            {
                SessionHelper.SetObjectAsJson(HttpContext.Session, "username", username);
                return RedirectToAction("Index", "Home");
            }

            HttpContext.Session.SetString("message", message);
            return RedirectToAction("Index");
        }
        public IActionResult Register(RegisterForm registerForm)
        {
            Student user = new Student()
            {
                Name = registerForm.Username,
                Password = registerForm.Password,
                HouseType = registerForm.HouseType,
                PetType = registerForm.PetType
            };
            if (_context.Register(user))
            {
                return RedirectToAction("Index", "Student");
            }
            var message = "User already exists!";
            HttpContext.Session.SetString("message", message);
            return RedirectToAction("Index");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index", "Student");
        }
    }
}
