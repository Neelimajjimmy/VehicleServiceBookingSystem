namespace project3VehicleServiceBookingApp.Models
{
    public class BookService
    {
        public int userid { get; set; }
        public int vhid { get; set; }
        public int stid { get; set; }
        public DateTime? sdate {  get; set; }
       
    }

    public class Booking
    {
        public int bid { get; set; }
        public string? sname { get; set; }
        public string? model { get; set; }

        public DateTime bdate { get; set; }
        public DateTime sdate { get; set; }

        public string? st {  get; set; }

    }

    public class AdminBooking {

        public int bid { get; set; }
        public string? sname { get; set; }

        public string? uname { get; set; }
        public string? model { get; set; }

        public DateTime sdate { get; set; }

        public string? st { get; set; }

    }
}
