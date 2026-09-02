using Microsoft.AspNetCore.Mvc;
using project3VehicleServiceBookingApp.Models;

namespace project3VehicleServiceBookingApp.Controllers
{
    public class PwdValidationController : Controller
    {
        AppDB db=new AppDB();
        public IActionResult Index()
        {
            return View();
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
