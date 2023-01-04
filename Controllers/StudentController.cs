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
            ViewBag.HouseTypes = new HouseType[] { HouseType.Gryffindor, HouseType.Hufflepuff, HouseType.Ravenclaw, HouseType.Slytherin }; //TODO viewmodel-t létrehozni viewbag helyett (enumból lekérni felsorolás helyett.)
            ViewBag.PetTypes = new PetType[] { PetType.Rat, PetType.Cat, PetType.None, PetType.Owl };
            return View();
        }
        public IActionResult ValidateLogin(string username, string password)
        {
            var message = "Please enter the correct credentials!";

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                HttpContext.Session.SetString("message", message);
                return RedirectToAction("Index");
            }

            LoginForm loginForm = new LoginForm() { Username = username, Password = password };

            if (_context.ValidateLogin(loginForm))
            {
                SessionHelper.SetObjectAsJson(HttpContext.Session, "username", username);
                return RedirectToAction("Index", "Home");
            }

            HttpContext.Session.SetString("message", message);
            return RedirectToAction("Index");
        }

        public IActionResult Register()
        {
            string username = Request.Form["register-username"]; //TODO parameteresen adni át a username / passwordot, létrehozni uj objektumot a register-formnak
            string password = Request.Form["register-password"];
            string houseType = Request.Form["register-houseType"];
            string petType = Request.Form["register-petType"];
            Student user = new Student() { Name = username, Password = password, HouseType = (HouseType)Enum.Parse(typeof(HouseType), houseType), PetType = (PetType)Enum.Parse(typeof(PetType), petType) };
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
