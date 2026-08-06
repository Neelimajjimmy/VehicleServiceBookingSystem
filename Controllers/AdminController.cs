using Microsoft.AspNetCore.Mvc;
using project3VehicleServiceBookingApp.Models;

namespace project3VehicleServiceBookingApp.Controllers
{
    public class AdminController : Controller
    {
        AppDB db = new AppDB();
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult registerAdmin()
        {
            return View();

        }

        [HttpPost]
        public IActionResult registerAdmin(AdminRegister ar)
        {
           string msg= db.addAdmin(ar);
            TempData["adminmsg"] = msg;
            return RedirectToAction("Login", "Login");
        }

        [HttpGet]
        public IActionResult createService()
        {
            return View();
        }

        [HttpPost]
        public IActionResult createService(AddServiceTypeDto st)
        {
            string smsg=db.insertService(st);
            return View();
        }
    }
}
