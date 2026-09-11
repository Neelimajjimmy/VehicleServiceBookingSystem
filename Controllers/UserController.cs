using Microsoft.AspNetCore.Mvc;
using project3VehicleServiceBookingApp.Models;

namespace project3VehicleServiceBookingApp.Controllers
{
    public class UserController : Controller
    {
        AppDB db=new AppDB();
        public IActionResult userIndex()
        {
            string id = HttpContext.Session.GetString("userid");
            List<ServiceType> services = new List<ServiceType>();
            services=db.getAllServices().Where(x=>x.status=="active").ToList();
            ViewBag.services = services;

            UserInfo data = db.getUserSummary(Convert.ToInt32(id));
            ViewBag.userdata=data;
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

        [HttpGet]
        public IActionResult CreateVehicle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateVehicle(AddVehicleDto ob)
        {
            int id = Convert.ToInt32(HttpContext.Session.GetString("userid"));
            string msg=db.insertVehicle(ob, id);

            return RedirectToAction("vehicleList");
        }

        [HttpGet]
        public IActionResult vehicleList() {

            int id = Convert.ToInt32(HttpContext.Session.GetString("userid"));
            List<Vehicle> vehicles = db.getAllVehicles(id);
            ViewBag.vehicles = vehicles;
            return View();
        }
    }
}
