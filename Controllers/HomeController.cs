using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChefsDishes.Models;
using Microsoft.EntityFrameworkCore;

namespace ChefsDishes.Controllers
{
    public class HomeController : Controller
    {

        private MyContext dbContext;

        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            List<Chef> AllChefs = dbContext.Chefs
                .Include(chef => chef.DishesMade)
                .ToList();
            return View(AllChefs);
        }

        [HttpGet("dishes")]
        public IActionResult Dishes()
        {
            List<Dish> AllDishesWithChefs = dbContext.Dishes
                .Include(dish => dish.Creator)
                .ToList();
            return View(AllDishesWithChefs);
        }

        [HttpGet("new")]
        public IActionResult NewChef()
        {
            return View();
        }

        [HttpPost("new")]
        public IActionResult NewChef(Chef newChef)
        {
            // System.Console.WriteLine("New Chef");
            // System.Console.WriteLine(newChef.Fname);
            // System.Console.WriteLine(newChef.Lname);
            // System.Console.WriteLine(newChef.Birthday);
            dbContext.Add(newChef);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet("dishes/new")]
        public IActionResult NewDish()
        {
            List<Chef> AllChefs = dbContext.Chefs.ToList();
            ViewBag.AllChefs = AllChefs;
            return View();
        }

        [HttpPost("dishes/new")]
        public IActionResult NewDish(Dish newDish)
        {
            // System.Console.WriteLine("New Dish");
            // System.Console.WriteLine(newDish.Name);
            // System.Console.WriteLine(newDish.Chef);
            // System.Console.WriteLine(newDish.Tastiness);
            // System.Console.WriteLine(newDish.Calories);
            // System.Console.WriteLine(newDish.Description);
            Chef newChef = dbContext.Chefs.FirstOrDefault(chef => chef.UserId == Int32.Parse(newDish.Chef));
            newDish.Creator = newChef;
            dbContext.Add(newDish);
            dbContext.SaveChanges();
            return RedirectToAction("Dishes");
        }
    }
}
