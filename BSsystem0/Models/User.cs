namespace BSsystem0.Models
{
    public class User
    {
        public int Id { get; set; }

        public string? username { get; set; }
        public string? Name { get; set; }

        public string? PhoneNumber { get; set; }
        public string ?Email { get; set; }
        public string? Password { get; set; }

        public ICollection<Appointment> Appointments { get; set; }

        public ICollection<Review> Reviews { get; set; }


    }
    public class UserLogin
    {

        public string username { get; set; }

        public string Password { get; set; }


    }
    public class UserRegister
    {

        public string? username { get; set; }

        public string? Name { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        

        //Date x = new Date();
    }

}

