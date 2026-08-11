 namespace project3VehicleServiceBookingApp.Models
{
    public class ServiceType
    { 
        public int id { set; get; }

        public string? name { set; get; }

        public string? description { set; get; }
        public decimal price { set; get; }

        public string? status { set; get; }
    }

    public class AddServiceTypeDto
    {
        public string? name { set; get; }

        public string? description { set; get; }
        public decimal price { set; get; }
    }

    public class EditServiceTypeDto
    {
        public string? name { set; get; }

        public string? description { set; get; } 

        public decimal price { set; get; }

        public string? status { set; get; }
    }
}
