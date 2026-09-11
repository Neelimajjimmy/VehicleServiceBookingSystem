namespace project3VehicleServiceBookingApp.Models
{
    public class Vehicle
    {
        public int vid {  get; set; }
        public int userid { get; set; }
        public string regNo { get; set; }

        public string model { get; set; }

        public string brand { get; set; }
        public string fueltype { get; set; }
        public string mileage { get; set; }

    }

    public class AddVehicleDto
    {
        public string regNo { get; set; }

        public string model { get; set; }

        public string brand { get; set; }
        public string fueltype { get; set; }
        public string mileage { get; set; }
    }
}
