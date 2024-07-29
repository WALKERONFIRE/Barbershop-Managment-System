namespace BSsystem0.Models
{
    public class Barber
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string PhoneNumber { get; set; }

        public string? Image { get; set; }
        public string? WorkHours { get; set; }

        public int? ShopId { get; set; }

        public Shop Shop { get; set; }
   
        public ICollection<Appointment> Appointments { get; set; }

    }
}
