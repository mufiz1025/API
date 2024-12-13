using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace api.data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
        {
            
        }
        
        public DbSet<Stocks> Stocks { get; set; }
        public DbSet<Comment> Comment { get; set; }

        public DbSet<PortFolio> PortFolios {get ; set ;}

        


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PortFolio>(x => x.HasKey(P => new { P.AppUserId , P.StockId}));

            builder.Entity<PortFolio>()
            .HasOne(u => u.AppUser)
            .WithMany(u => u.PortFolios)
            .HasForeignKey(p => p.AppUserId);

             builder.Entity<PortFolio>()
            .HasOne(u => u.stocks)
            .WithMany(u => u.PortFolios)
            .HasForeignKey(p => p.StockId);

           
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);


            
        }
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                {
                        optionsBuilder.ConfigureWarnings(warnings =>
                             warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
                }
    }
}