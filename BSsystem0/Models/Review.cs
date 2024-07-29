namespace BSsystem0.Models
{
    public class Review
    {
        public int Id { get; set; }

        public DateTime DateCreated { get; set; }

        public string description { get; set; }

        public int ServiceId { get; set; }

        public Service Service { get; set; }




    }
}
