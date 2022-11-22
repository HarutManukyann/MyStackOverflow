using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SteckOverflow.DataModel.Models;
using SteckOverflow.Models;
using System.Diagnostics;
using System.Linq;
using static SteckOverflow.DataModel.ApplicationContext;

namespace SteckOverflow.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {

            return View();

        }
        [HttpPost]
        public IActionResult Login(LoginModel Model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Required Email or password.");
                return View(Model);
            }
            var db = new MyContext();
            var user = db.Users.Where(x => x.Email == Model.Email && x.Password == Model.Password).SingleOrDefault();
            if (user == null)
            {
                ModelState.AddModelError("", "Wrong Email or password.");
                return View(Model);
            }
            HttpContext.Session.SetString("Email", user.Email);
            HttpContext.Session.SetInt32("Id", user.Id);


            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterModel Rmodel)
        {
            var db = new MyContext();
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "You have empty fields.");
                return View(Rmodel);
            }
            UserEntity user = new UserEntity();
            if (db.Users.Any(x => x.Email == Rmodel.Email))
            {
                ModelState.AddModelError("", "User with this email already exists.");
                return View(Rmodel);
            }
            if (Rmodel.Password.Length < 5 && Rmodel.Password.Length > 20)
            {
                ModelState.AddModelError("", "Password length must be 5-20.");
                return View(Rmodel);

            }

            user.FirstName = Rmodel.FirstName;
            user.LastName = Rmodel.LastName;
            user.Email = Rmodel.Email;
            user.Password = Rmodel.Password;

            db.Add(user);
            db.SaveChanges();

            HttpContext.Session.SetString("Email", user.Email);
            HttpContext.Session.SetInt32("Id", user.Id);
            return RedirectToAction("Index", "Home");

        }
        [HttpGet]

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
