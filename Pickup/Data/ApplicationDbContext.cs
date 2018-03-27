using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pickup.Models;

namespace Pickup.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Donor> Donors { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<DonationPickup> DonationPickups { get; set; }
        public DbSet<Furniture> Furnitures { get; set; }
        public DbSet<FurnitureDonationPickup> FurnitureDonationPickups { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<FurnitureDonationPickup>()
                .HasKey(pickup => new { pickup.DonationPickupID, pickup.FurnitureID });
        }

    }
}
