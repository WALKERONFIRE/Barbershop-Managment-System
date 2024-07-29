using BSsystem0.Data;

namespace BSsystem0.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        public DateTime DateCreated { get; set; }

        public int ServiceId { get; set; }

        public Service Service { get; set; }

        public int ShopId { get; set; }

        public Shop Shop { get; set; }

        public int BarberId { get; set; }

        public Barber Barber { get; set; }


        public string? ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }


    }
    public class AppointmentDTO
    {
        public List<AppointmentData> AppointmentData { get; set; }
    }

    public class AppointmentData
    {
        public int AppointmentId { get; set; }

        public int? UserId { get; set; }
        public int? ServiceId { get; set; }

        public int? ShopId { get; set; }
        public DateTime? DateCreated { get; set; }


    }

}
