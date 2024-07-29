using System.ComponentModel.DataAnnotations;

namespace BSsystem0.Models
{
    public class AddAppointmentVM
    {

        public Appointment app { get; set; }
        public int AppId { get; set; }
        public DateTime DateCreated { get; set; }

        public Service ss { get; set; }
        public int ServiceId { get; set; }
        public string Name { get; set; }

        public string? Description { get; set; }

        public int Price { get; set; }

        public int? PriceAfterOffer { get; set; }
        [Display(Name = "Shop")]
        public List<Shop> Shops { get; set; }
        public int ShopId { get; set; }

        public string name { get; set; }
        //public string username { get; set; }
        //public string password { get; set; }
        //public string PhoneNumber { get; set; }
        //public string? workhours { get; set; }
        //public string? description { get; set; }
        //public string? Address { get; set; }

        [Display(Name = "Shop")]

        public List<Barber> Barbers { get; set; }
        public int BarberId { get; set; }


    }
}

