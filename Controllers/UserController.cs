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
        [HttpGet]
        public IActionResult bookService(int id)
        {
            BookService bs=new BookService();
            int uid = Convert.ToInt32(HttpContext.Session.GetString("userid"));
            List<Vehicle> vehicles = db.getAllVehicles(uid);
            ViewBag.vehicles = vehicles;
           
            bs.stid= id;
          ServiceType s = db.GetServiceById(id);
            ViewBag.sname = s.name;
            return View(bs);
        }
        [HttpPost]
        public IActionResult bookService(BookService bs)
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("userid"));
            bs.userid = uid;
            List<Vehicle> vehicles = db.getAllVehicles(uid);
            ViewBag.vehicles = vehicles;
            if (ViewBag.vehicles.Count > 0)
            {
                string msg = db.bookService(bs);
                return RedirectToAction("userBookings");
            }
            return View(bs);
        }
        [HttpGet]
        public IActionResult userBookings()
        {
            int id = Convert.ToInt32(HttpContext.Session.GetString("userid"));
            List<Booking> bkings = new List<Booking>();
            bkings=db.GetUserBookings(id);
            ViewBag.userBooks = bkings;
            return View();
        }
        }
}
