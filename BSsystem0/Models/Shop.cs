 namespace BSsystem0.Models
{
    public class Shop
    {
        public int Id { get; set; }

        public string name { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string PhoneNumber { get; set; }
        public string? workhours { get; set; }
        public string? description { get; set; }
        public string? Address { get; set; }

        public string image { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Barber> barbers { get; set; }
    }
}
