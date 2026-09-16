using Microsoft.AspNetCore.Mvc;
using project3VehicleServiceBookingApp.Models;

namespace project3VehicleServiceBookingApp.Controllers
{
    public class AdminController : Controller
    {
        AppDB db = new AppDB();
        
        public IActionResult Index()
        {
            DashboardData data = db.getDashboardSummary();
            List<AdminBooking> bookings =new List<AdminBooking>();
            bookings = db.GetAllBookings();
            ViewBag.abkings = bookings;
            return View(data);
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
            return RedirectToAction("servicesList");
        }

        public IActionResult servicesList() {
            List<ServiceType> services=db.getAllServices();
            ViewBag.services = services;
            return View();
        }

        [HttpGet]
        public IActionResult EditService(int id)
        {
            ServiceType st = db.GetServiceById(id);
            return View(st);
        }

        [HttpPost]
        public IActionResult EditService(ServiceType ob)
        {
            string emsg = db.editService(ob);
            return RedirectToAction("servicesList");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            Login user = db.GetUserDetails(HttpContext.Session.GetString("userid"));
            PasswordChangeDto pw = new PasswordChangeDto
            {
                password = user.password
            };
            return View(pw);
        }

        [HttpPost]
        public IActionResult ChangePassword(PasswordChangeDto ob)
        {
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

        public IActionResult checkOldPwd(PasswordChangeDto pw)
       {
            Login user = db.GetUserDetails(HttpContext.Session.GetString("userid"));
            if (user.password == pw.password)
                return Json(true);
            else
                return Json("Old password is incorrect");

        }



        public IActionResult checkNewPwd(PasswordChangeDto pw)
        {
            if (pw.newpwd == pw.password)
                return Json("New password cannot be same as old password");
            else
                return Json(true);

        }

        
    }
}
