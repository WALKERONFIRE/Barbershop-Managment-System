using BSsystem0.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BSsystem0.Models
{
    public class ApplicationDBcontext : DbContext
    {
        private readonly ApplicationDbContext _identityContext;

        public ApplicationDBcontext(DbContextOptions<ApplicationDBcontext> options, ApplicationDbContext identityContext) : base(options)
        {
            _identityContext = identityContext;

        }
        //public DbSet<User> Users { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Barber> Barbers { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<IdentityRole> Roles { get; set; }
        public DbSet<IdentityUserRole<string>> UserRoles { get; set; }
        public DbSet<IdentityUserClaim<string>> UserClaims { get; set; }
        public DbSet<IdentityUserLogin<string>> UserLogins { get; set; }
        public DbSet<IdentityRoleClaim<string>> RoleClaims { get; set; }
        public DbSet<IdentityUserToken<string>> UserTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().HasData(_identityContext.Users);
            modelBuilder.Entity<IdentityRole>().HasData(_identityContext.Roles);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(_identityContext.UserRoles);
            modelBuilder.Entity<IdentityUserClaim<string>>().HasData(_identityContext.UserClaims);
            modelBuilder.Entity<IdentityUserLogin<string>>().HasData(_identityContext.UserLogins);
            modelBuilder.Entity<IdentityRoleClaim<string>>().HasData(_identityContext.RoleClaims);
            modelBuilder.Entity<IdentityUserToken<string>>().HasData(_identityContext.UserTokens);

            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                // Map to the same table as IdentityUser
                entity.ToTable("Users");

                // Map the primary key
                entity.HasKey(u => u.Id);

                // Map the other properties
                entity.Property(u => u.UserName).HasColumnName("UserName");
                entity.Property(u => u.NormalizedUserName).HasColumnName("NormalizedUserName");
                entity.Property(u => u.Email).HasColumnName("Email");
                entity.Property(u => u.Name).HasColumnName("Name");
                entity.Property(u => u.PhoneNumber).HasColumnName("PhoneNumber");
                entity.Property(u => u.NormalizedEmail).HasColumnName("NormalizedEmail");
                entity.Property(u => u.EmailConfirmed).HasColumnName("EmailConfirmed");
                entity.Property(u => u.PasswordHash).HasColumnName("PasswordHash");
                modelBuilder.Entity<Appointment>()
             .HasOne(a => a.ApplicationUser)
             .WithMany()
             .HasForeignKey(a => a.ApplicationUserId);
            });

           
            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.HasKey(login => new { login.LoginProvider, login.ProviderKey, login.UserId });
            });
            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.HasKey(userRole => new { userRole.UserId, userRole.RoleId });
            });
            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.HasKey(token => new { token.UserId, token.LoginProvider, token.Name });
            });
            modelBuilder.Ignore<IdentityUser>();
            modelBuilder.Ignore<IdentityRole>();
            modelBuilder.Ignore<IdentityUserRole<string>>();
            modelBuilder.Ignore<IdentityUserClaim<string>>();
            modelBuilder.Ignore<IdentityUserLogin<string>>();
            modelBuilder.Ignore<IdentityRoleClaim<string>>();
            modelBuilder.Ignore<IdentityUserToken<string>>();
        }

    }
}
