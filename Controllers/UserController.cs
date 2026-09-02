using Microsoft.AspNetCore.Mvc;
using project3VehicleServiceBookingApp.Models;

namespace project3VehicleServiceBookingApp.Controllers
{
    public class UserController : Controller
    {
        AppDB db=new AppDB();
        public IActionResult userIndex()
        {
            List<ServiceType> services = new List<ServiceType>();
            services=db.getAllServices().Where(x=>x.status=="active").ToList();
            ViewBag.services = services;
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

        [HttpGet]
        public IActionResult ChangeUserPassword() {

            Login user = db.GetUserDetails(HttpContext.Session.GetString("userid"));
            PasswordChangeDto pw = new PasswordChangeDto
            {
                password = user.password
            };
            return View(pw);
        }
        [HttpPost]
        public IActionResult ChangeUserPassword(PasswordChangeDto ob) {

            string id = HttpContext.Session.GetString("userid");
            if (ModelState.IsValid)
            {
                string pwmsg = db.changePassword(ob, id);
                TempData["pwmsg"] = pwmsg;
                return RedirectToAction("Login","Login");
            }
            else
            {
                return View(ob);
            }
        }
    }
}
