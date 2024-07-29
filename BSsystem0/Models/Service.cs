namespace BSsystem0.Models
{
    public class Service
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public int Price { get; set; }

        public int? PriceAfterOffer { get; set; }
        public string? Image { get; set; }

        public ICollection<Review> Reviews { get; set; }
        public ICollection<Appointment> Appointments { get; set; }


    }
    public class ServiceAdder
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public int Price { get; set; }

        public int? PriceAfterOffer { get; set; }
        public string? Image { get; set; }
    }
}
