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
        }
}
