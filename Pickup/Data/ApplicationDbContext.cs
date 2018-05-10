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
        public DbSet<DonorCustomer> DonorsCustomers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<PickupOrDelivery> PickupsDeliveries { get; set; }
        public DbSet<Furniture> Furniture { get; set; }
        public DbSet<FurnitureCategory> FurnitureCategories { get; set; }
        public DbSet<FurniturePickupOrDelivery> FurnitureDonationPickups { get; set; }
        public DbSet<Blacklist> BlacklistedDonors { get; set; }

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

            builder.Entity<FurniturePickupOrDelivery>()
                .HasKey(pickup => new { pickup.DonationPickupID, pickup.FurnitureID });
        }

    }
}
