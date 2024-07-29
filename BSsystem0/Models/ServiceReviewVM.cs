namespace BSsystem0.Models
{
    public class ServiceReviewVM
    {
        public Service ss { get; set; }
        public string Name { get; set; }

        public string? Description { get; set; }

        public int Price { get; set; }

        public int? PriceAfterOffer { get; set; }
        public string? Image { get; set; }

        public List<Review> Reviews { get; set; }
        public int ReviewId { get; set; }
        public DateTime DateCreated { get; set; }

        public string description { get; set; }

        public int ServiceId { get; set; }

       
    }
}
