using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BSsystem0.Data
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(100)]
        public string Name { get; set; }

    }
}
