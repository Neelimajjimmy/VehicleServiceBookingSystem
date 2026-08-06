using Microsoft.AspNetCore.Mvc;
using project3VehicleServiceBookingApp.Models;
namespace project3VehicleServiceBookingApp.Controllers
{
    public class LoginController : Controller
    {
        AppDB db=new AppDB();
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Login user)
        {
            string c=db.Login(user);
            if (c == "1")
            {
                string id = db.getRegId(user);
                string logtype=db.getLogtype(user);
                HttpContext.Session.SetString("userid", id);
                if (logtype == "admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return RedirectToAction("userIndex", "User");
                }
            }
            else
            {
                TempData["msg"] = "Invalid Login";
            }
                return View();
        }
    }
}
