using Microsoft.AspNetCore.Mvc;
using project3VehicleServiceBookingApp.Models;

namespace project3VehicleServiceBookingApp.Controllers
{
    public class UserController : Controller
    {
        AppDB db=new AppDB();
        public IActionResult userIndex()
        {
            return View();
        }
        [HttpGet]
        public IActionResult userReg()
        {
            return View();
        }
        [HttpPost]
        public IActionResult userReg(Customer cs)
        {
            string usermsg=db.addCustomer(cs);
            TempData["usermsg"]= usermsg;
            return RedirectToAction("Login", "Login");
        }
    }
}
