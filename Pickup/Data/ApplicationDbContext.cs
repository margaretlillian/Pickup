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
        public DbSet<ItemDonatedSold> ItemsDonatedSold { get; set; }
        public DbSet<ItemCategory> ItemCategories { get; set; }
        public DbSet<ItemsAndPickupOrDelivery> ItemsPickupsDeliveries { get; set; }
        public DbSet<Blacklist> BlacklistedDonors { get; set; }
        public DbSet<BlackoutDays> BlackoutDays { get; set; }


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

            builder.Entity<ItemsAndPickupOrDelivery>()
                .HasKey(pickup => new { pickup.PickupDeliveryID, pickup.ItemID });
        }

    }
}
