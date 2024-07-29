namespace BSsystem0.Models
{
    public class AppointmentServiceReviewVM
    {
        public Appointment Appointment { get; set; }
        public Service Service { get; set; }
        
        public List<Review> Reviews { get; set; }
    }
}
