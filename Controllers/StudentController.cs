using System;
using System.Collections.Generic;
using HogwartsPotions.DataAccess;
using HogwartsPotions.Helpers;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using HogwartsPotions.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HogwartsPotions.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
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

            if (_studentService.ValidateLogin(loginForm))
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
            if (_studentService.Register(user))
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
