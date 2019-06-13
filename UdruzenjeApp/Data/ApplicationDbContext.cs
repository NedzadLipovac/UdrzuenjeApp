using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UdruzenjeApp.Models;

namespace UdruzenjeApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,IdentityRole<int>,int>
    {



     
        public DbSet<Dogadjaj> dogadjaj { get; set; }
        public DbSet<Anketa> anketa { get; set; }
        public DbSet<Obavijest> obavijest { get; set; }
        public DbSet<Grad> grad { get; set; }
        public DbSet<Drzava> drzava { get; set; }
        public DbSet<Pitanje> pitanje { get; set; }
        public DbSet<Odgovor> odgovor { get; set; }
        public DbSet<PredlozeniDogadjaj> predlozeniDogadjaj { get; set; }


        //User ID = UdruzenjeApp1; Password=Pa$$word1;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { optionsBuilder.UseSqlServer("Server=app.fit.ba,1433;Database=UdruzenjeApp;Trusted_Connection=false;User ID=UdruzenjeApp;Password=Pa$$word1;MultipleActiveResultSets=true"); }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {



        }

        public ApplicationDbContext():base()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "User");
                entity.Property(e => e.Id).HasColumnName("UserID");

            });

            builder.Entity<IdentityRole<int>>(entity =>
            {
                entity.ToTable(name: "Role");
                entity.Property(e => e.Id).HasColumnName("RoleID");

            });

            builder.Entity<IdentityUserClaim<int>>(entity =>
            {
                entity.ToTable("UserClaim");
                entity.Property(e => e.UserId).HasColumnName("UserID");
                entity.Property(e => e.Id).HasColumnName("UserClaimID");

            });

            builder.Entity<IdentityUserLogin<int>>(entity =>
            {
                entity.ToTable("UserLogin");
                entity.Property(e => e.UserId).HasColumnName("UserID");

            });

            builder.Entity<IdentityRoleClaim<int>>(entity =>
            {
                entity.ToTable("RoleClaim");
                entity.Property(e => e.Id).HasColumnName("RoleClaimID");
                entity.Property(e => e.RoleId).HasColumnName("RoleID");
            });

            builder.Entity<IdentityUserRole<int>>(entity =>
            {
                entity.ToTable("UserRole");
                entity.Property(e => e.UserId).HasColumnName("UserID");
                entity.Property(e => e.RoleId).HasColumnName("RoleID");

            });


            builder.Entity<IdentityUserToken<int>>(entity =>
            {
                entity.ToTable("UserToken");
                entity.Property(e => e.UserId).HasColumnName("UserID");

            });
        }
    }
}
